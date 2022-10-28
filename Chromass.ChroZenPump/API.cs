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

        Controller.Information.Updated += OnInformationUpdated;
        Controller.Configuration.Updated += OnConfigurationUpdated;
        Controller.Setup.Updated += OnSetupUpdated;
        Controller.State.Updated += OnStateChanged;
        Controller.SelfMessage.Updated += OnMessageReceived;
        Controller.Calibration.Updated += OnCalibrationUpdated;
    }

    public bool IsConnected => Controller.IsConnected;
    public Task ConnectAsync(string ip, int port, CancellationToken token)
        => Controller.ConnectAsync(ip, port, token);

    public void Close() => Controller.Close();

    protected Controller Controller
    {
        get; init;
    }

    public Information Information
    {
        get; init;
    }

    public Task<bool> LoadInformationAsync() => Controller.AskInformationAsync(1000);

    public Configuration Configuration
    {
        get; init;
    }

    public Task<bool> LoadConfigurationAsync() => Controller.AskConfigurationAsync(1000);

    public Setup Setup
    {
        get; init;
    }

    public Task<bool> LoadSetupAsync() => Controller.AskSetupAsync(1000);

    public State State
    {
        get; init;
    }
    public Calibration Calibration
    {
        get; init;
    }

    public Task<bool> LoadCalibrationAsync() => Controller.AskCalibrationAsync(1000);

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

    public event EventHandler<InformationUpdatedEventArgs>? InformationUpdated;
    private void OnInformationUpdated(object? sender, PacketUpdatedEventArgs<Packets.Information> e)
        => InformationUpdated?.Invoke(this, new InformationUpdatedEventArgs(Information));

    public event EventHandler<ConfigurationUpdatedEventArgs>? ConfigurationUpdated;
    private void OnConfigurationUpdated(object? sender, PacketUpdatedEventArgs<Packets.Configuration> e)
        => ConfigurationUpdated?.Invoke(this, new ConfigurationUpdatedEventArgs(Configuration));

    public event EventHandler<SetupUpdatedEventArgs>? SetupUpdated;
    private void OnSetupUpdated(object? sender, PacketUpdatedEventArgs<Packets.Setup> e) 
        => SetupUpdated?.Invoke(this, new SetupUpdatedEventArgs(Setup));

    public event EventHandler<StateUpdatedEventArgs>? StateUpdated;
    private void OnStateChanged(object? sender, PacketUpdatedEventArgs<Packets.State> e)
        => StateUpdated?.Invoke(this, new StateUpdatedEventArgs(State));

    public event EventHandler<MessageUpdatedEventArgs>? MessageReceived;
    private void OnMessageReceived(object? sender, PacketUpdatedEventArgs<Packets.SelfMessage> e)
        => MessageReceived?.Invoke(this, new MessageUpdatedEventArgs(new Message(e.Wrapper)));

    public event EventHandler<CalibrationUpdatedEventArgs>? CalibrationUpdated;
    private void OnCalibrationUpdated(object? sender, PacketUpdatedEventArgs<Packets.Calibration> e)
        => CalibrationUpdated?.Invoke(this, new CalibrationUpdatedEventArgs(Calibration));
}
