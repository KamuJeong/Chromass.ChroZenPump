using Chromass.ChroZenPump.PacketWrappers;
using ChromassProtocol;
using Communicator;

namespace Chromass.ChroZenPump
{
    public class Controller
    {
        private Tcp tcp = new Tcp();

        public InformationWrapper Information { get; } = new InformationWrapper();
        public ConfigurationWrapper Configuration { get; } = new ConfigurationWrapper();
        public SetupWarpper Setup { get; } = new SetupWarpper();
        public EventWrapper[] Events { get; } = new EventWrapper[201];
        public StateWrapper State { get; } = new StateWrapper();

        public SelfMessageWrapper SelfMessage { get; } = new SelfMessageWrapper();
        public CalibrationWrapper Calibration { get; } = new CalibrationWrapper();
        public DiagnosisDataWrapper DiagnosisData { get; } = new DiagnosisDataWrapper();

        public Controller()
        {
            for(int i=0; i<Events.Length; i++)
                Events[i] = new EventWrapper();

            tcp.PacketParsing += Tcp_PacketParsing;
        }

        private void Tcp_PacketParsing(object? sender, PacketParsingEventArgs e)
        {
            if(e.Buffer.Length > 24)
            {
                var header = e.Buffer.Slice(0, 24).ToArray().ConvertTo<Header>();
                if(header.Length > 24 && header.Length < 1024)
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
                            case SetupWarpper.PacketCode:
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
                else
                {
                    e.Parsed(1);
                }
            }

        }

        public Task? ConnectAsync(string ip, int port, CancellationToken token)
        {
            if (tcp.IsConnected)
                throw new InvalidOperationException("already connected");

            return tcp.ConnectAsync(new Uri($"{ip}:{port}"), token);
        }

        public void Close()
        {
            if (tcp.IsConnected)
                tcp.Close();
        }


    }
}