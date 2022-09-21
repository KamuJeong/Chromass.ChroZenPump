using Communicator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chromass.ChroZenPump.APIs;
using ChromassProtocol;

namespace Chromass.ChroZenPump
{
    public class API
    {
        public API(ICommunicator comm)
        {
            Controller = new Controller(comm);
            Information = new Information(Controller.Information);
            Configuration = new Configuration(Controller.Configuration, Controller.SendConfiguration);
            Setup = new Setup(Controller.Setup, Controller.Events, Controller.SendSetup);
            State = new State(Controller.State);

            State.Wrapper.Updated += OnStateChanged;
            Controller.SelfMessage.Updated += OnMessageReceived;
        }

        public bool IsConnected => Controller.IsConnected;
        public Task ConnectAsync(string ip, int port, CancellationToken token) => Controller.ConnectAsync(ip, port, token);
        public void Close() => Controller.Close();

        public Controller Controller { get; init; }

        public Information Information { get; init; }
        public Configuration Configuration { get; init; }
        public Setup Setup { get; init; }
        public State State { get; init; }

        public void SetPurgeFlow(float flow, float a, float b, float c) => Controller.SendEvent(flow, a, b, c);
        public void SetServiceFlow(float flow, float a, float b, float c) => SetPurgeFlow(flow, a, b, c);
        public void Output(SwitchOutputs sw1, SwitchOutputs sw2, MarkOutputs mark) => Controller.SendEvent(sw1, sw2, mark);

        public void Ready() => Controller.SendCommand(PumpCommands.Initialize);
        public void Purege() => Controller.SendCommand(PumpCommands.Purge);
        public void Run() => Controller.SendCommand(PumpCommands.Gradient);
        public void Stop() => Controller.SendCommand(PumpCommands.Stop);
        public void Halt() => Controller.SendCommand(PumpCommands.Halt);
        public void ResetError() => Controller.SendCommand(PumpCommands.Reset);
        public void PressureZero() => Controller.SendCommand(PumpCommands.PressureZero);
        public void Service() => Controller.SendCommand(PumpCommands.Service);

        public event EventHandler<State>? StateUpdated;
        private void OnStateChanged(object? sender, PacketUpdatedEventArgs<Packets.State> e) => StateUpdated?.Invoke(this, State);

        public event EventHandler<Message>? MessageReceived;
        private void OnMessageReceived(object? sender, PacketUpdatedEventArgs<Packets.SelfMessage> e)
                        => MessageReceived?.Invoke(this, new Message(e.Wrapper));

    }
}
