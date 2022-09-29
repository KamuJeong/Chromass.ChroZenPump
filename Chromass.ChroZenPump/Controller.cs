using Chromass.ChroZenPump.Packets;
using Chromass.ChroZenPump.PacketWrappers;
using ChromassProtocol;
using Communicator;

namespace Chromass.ChroZenPump;

public class Controller
{
    private readonly ICommunicator tcp;
    private TaskCompletionSource? taskGreeting;

    public bool IsConnected => tcp.IsConnected && State.Packet.btStatus != Statuses.Initializing;

    public InformationWrapper Information { get; } = new InformationWrapper();
    public ConfigurationWrapper Configuration { get; } = new ConfigurationWrapper();
    public SetupWrapper Setup { get; } = new SetupWrapper();
    public EventWrapper[] Events { get; } = new EventWrapper[201];
    public StateWrapper State { get; } = new StateWrapper();

    public SelfMessageWrapper SelfMessage { get; } = new SelfMessageWrapper();
    public CalibrationWrapper Calibration { get; } = new CalibrationWrapper();
    public DiagnosisDataWrapper DiagnosisData { get; } = new DiagnosisDataWrapper();

    public Controller(ICommunicator comm)
    {
        tcp = comm;

        for (var i = 0; i < Events.Length; i++)
            Events[i] = new EventWrapper();

        tcp.PacketParsing += Tcp_PacketParsing;
    }

    private void Tcp_PacketParsing(object? sender, PacketParsingEventArgs e)
    {
        if (e.IsClosed)
        {
            State.Assemble(this, new StateWrapper().Binary, 0, 0);
        }
        else if (e.Buffer.Length >= 24)
        {
            var header = e.Buffer.Slice(0, 24).ToArray().ConvertTo<Header>();
            if (header.Length > 24 && header.Length < 1024)
            {
                if (e.Buffer.Length >= header.Length)
                {
                    var slot = e.Buffer.Slice(24, header.SlotSize);

                    switch (header.Code)
                    {
                        case InformationWrapper.PacketCode:
                            Information.Assemble(this, slot, header.Index, header.SlotOffset);
                            break;
                        case ConfigurationWrapper.PacketCode:
                            Configuration.Assemble(this, slot, header.Index, header.SlotOffset);
                            break;
                        case SetupWrapper.PacketCode:
                            Setup.Assemble(this, slot, header.Index, header.SlotOffset);
                            break;
                        case StateWrapper.PacketCode:
                            State.Assemble(this, slot, header.Index, header.SlotOffset);
                            break;
                        case EventWrapper.PacketCode:
                            Events[header.Index].Assemble(this, slot, header.Index, header.SlotOffset);
                            break;
                        case SelfMessageWrapper.PacketCode:
                            SelfMessage.Assemble(this, slot, header.Index, header.SlotOffset);
                            tcp.Send(SelfMessage.SendOkPacket());
                            break;
                        case DiagnosisDataWrapper.PacketCode:
                            DiagnosisData.Assemble(this, slot, header.Index, header.SlotOffset);
                            break;
                        case CalibrationWrapper.PacketCode:
                            Calibration.Assemble(this, slot, header.Index, header.SlotOffset);
                            break;
                    }

                    e.Parsed(header.Length);
                }
            }
            else if (header.Length == 24)
            {
                if (header.Code == InformationWrapper.PacketCode)
                {
                    taskGreeting?.SetResult();
                }

                e.Parsed(header.Length);
            }
            else
            {
                e.Parsed(1);
            }
        }
    }

    public async Task ConnectAsync(string ip, int port, CancellationToken token)
    {
        if (tcp.IsConnected)
            throw new InvalidOperationException("already connected");

        await tcp.ConnectAsync(new Uri($"tcp://{ip}:{port}"), token);

        if (tcp.IsConnected)
        {
            if (await GreetingAsync(2000))
            {
                await AskInformationAsync(2000);
                await AskConfigurationAsync(2000);
                await AskSetupAsync(2000);
                await AskCalibrationAsync(2000);
            }
            else
            {
                Close();
            }
        }
    }


    private async Task<bool> GreetingAsync(int mSec)
    {
        taskGreeting = new TaskCompletionSource();
        tcp.Send(new InformationWrapper().SendPacket());
        await await Task.WhenAny(Task.Delay(mSec), taskGreeting.Task);
        return taskGreeting.Task.IsCompleted;
    }

    public async Task<bool> AskInformationAsync(int mSec)
    {
        tcp.Send(new Header
        {
            Length = 24,
            Id = 0,
            Code = Information.Code,
            Index = 0,
            SlotOffset = 0,
            SlotSize = 112

        }.ToBytes());
//        tcp.Send(Information.RequestPacket());
        return await Information.WaitAnUpdateFor(mSec);
    }

    public async Task<bool> AskConfigurationAsync(int mSec)
    {
        tcp.Send(Configuration.RequestPacket());
        return await Configuration.WaitAnUpdateFor(mSec);
    }

    public async Task<bool> AskSetupAsync(int mSec)
    {
        tcp.Send(Setup.RequestPacket());
        if (await Setup.WaitAnUpdateFor(mSec))
        {
            for (var i = 0; i < Setup.Packet.nGradientCount; i++)
            {
                if (!await AskEventAsync(i, 1000))
                    return false;
            }

            for (var i = 0; i < Setup.Packet.nEventCount; i++)
            {
                if (!await AskEventAsync(i + 100, 1000))
                    return false;
            }

            return true;
        }
        return false;
    }

    private async Task<bool> AskEventAsync(int index, int mSec)
    {
        tcp.Send(Events[index].RequestPacket(index: index));
        return await Events[index].WaitAnUpdateFor(mSec);
    }

    public async Task<bool> AskCalibrationAsync(int mSec)
    {
        tcp.Send(Calibration.RequestPacket());
        return await Calibration.WaitAnUpdateFor(mSec);
    }

    public void SendConfiguration()
    {
        tcp.Send(Configuration.SendPacket());
    }

    public void SendSetup()
    {
        tcp.Send(Setup.SendPacket());

        for (var i = 0; i < Setup.Packet.nGradientCount; i++)
        {
            tcp.Send(Events[i].SendPacket(index: i));
        }

        for (var i = 0; i < Setup.Packet.nEventCount; i++)
        {
            tcp.Send(Events[i + 100].SendPacket(index: i + 100));
        }

        SendCommand(Commands.Initialize);
    }

    public void SendCalibration()
    {
        tcp.Send(Calibration.SendPacket());
    }

    public void SendCommand(Commands command)
    {
        var setup = new SetupWrapper();
        setup.Packet.Command = command;
        tcp.Send(setup.SendPacket(size : 1));
    }

    public void SendEvent(float flow, float a, float b, float c)
    {
        Events[200].Packet = Setup.Packet.InitEvent;

        Events[200].Packet.fFlowSpeed = flow;
        Events[200].Packet.fRatio[0] = a;
        Events[200].Packet.fRatio[1] = b;
        Events[200].Packet.fRatio[2] = c;
        Events[200].Packet.fRatio[3] = 100.0f - a - b - c;
        Events[200].Packet.btCurve = EventCurves.Level;

        tcp.Send(Events[200].SendPacket(index: 200));
    }

    public void SendEvent(SwitchOutputs sw1, SwitchOutputs sw2, MarkOutputs mark)
    {
        Events[200].Packet = Setup.Packet.InitEvent;
        Events[200].Packet.btCurve = EventCurves.None;

        Events[200].Packet.btSwitch1 = sw1;
        Events[200].Packet.btSwitch2 = sw2;
        Events[200].Packet.btMarkOut = mark;

        tcp.Send(Events[200].SendPacket(index: 200));
    }

    public void Close()
    {
        if (tcp.IsConnected)
            tcp.Close();
    }
}