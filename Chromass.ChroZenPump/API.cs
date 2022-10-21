using Communicator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chromass.ChroZenPump.APIs;
using ChromassProtocol;

namespace Chromass.ChroZenPump;

public class API
{
    public API(ICommunicator comm)
    {
        Controller = new Controller(comm);
        Information = new Information(Controller.Information);
        Configuration = new Configuration(Controller.Configuration, Controller.SendConfiguration);
        Setup = new Setup(Controller.Setup, Controller.Events, Controller.SendSetup);
        State = new State(Controller.State);
        Calibration = new Calibration(Controller.Calibration, Controller.SendCalibration);

        State.Wrapper.Updated += OnStateChanged;
        Controller.SelfMessage.Updated += OnMessageReceived;
    }

    public bool IsConnected => Controller.IsConnected;
    public Task ConnectAsync(string ip, int port, CancellationToken token)
        => Controller.ConnectAsync(ip, port, token);

    public void Close() => Controller.Close();

    public Controller Controller
    {
        get; init;
    }

    public Information Information
    {
        get; init;
    }
    public Configuration Configuration
    {
        get; init;
    }
    public Setup Setup
    {
        get; init;
    }
    public State State
    {
        get; init;
    }
    public Calibration Calibration
    {
        get; init;
    }

    public void SetPurgeFlow(float flow, float a, float b, float c) => Controller.SendEvent(flow, a, b, c);
    public void SetServiceFlow(float flow, float a, float b, float c) => SetPurgeFlow(flow, a, b, c);
    public void Output(SwitchOutputs sw1, SwitchOutputs sw2, MarkOutputs mark) => Controller.SendEvent(sw1, sw2, mark);

    public void Ready() => Controller.SendCommand(Commands.Initialize);
    public void Purge() => Controller.SendCommand(Commands.Purge);
    public void Run() => Controller.SendCommand(Commands.Gradient);
    public void Stop() => Controller.SendCommand(Commands.Stop);
    public void Halt() => Controller.SendCommand(Commands.Halt);
    public void ResetError() => Controller.SendCommand(Commands.Reset);
    public void PressureZero() => Controller.SendCommand(Commands.PressureZero);
    public void Service() => Controller.SendCommand(Commands.Service);

    public event EventHandler<StateUpdatedEventArgs>? StateUpdated;
    private void OnStateChanged(object? sender, PacketUpdatedEventArgs<Packets.State> e)
        => StateUpdated?.Invoke(this, new StateUpdatedEventArgs(State));

    public event EventHandler<MessageUpdatedEventArgs>? MessageReceived;
    private void OnMessageReceived(object? sender, PacketUpdatedEventArgs<Packets.SelfMessage> e)
        => MessageReceived?.Invoke(this, new MessageUpdatedEventArgs(new Message(e.Wrapper)));
}
