using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using CDS.Core;
using Chromass.ChroZenPump;
using Chromass.ChroZenPump.APIs;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;

namespace CDS.Chromass.ChroZenPump.ViewModels;
public class ConnectionViewModel : ObservableObject, IDisposable
{
    private class ConnectionViewModelSubscriber : WeakEventSubscriber<ConnectionViewModel, ChroZenPumpDevice>
    {
        public ConnectionViewModelSubscriber(ConnectionViewModel subscriber, ChroZenPumpDevice publisher) : base(subscriber, publisher)
        {
        }

        public override void SubScribe()
        {
            Publisher.API.MessageReceived += API_MessageReceived;
        }

        private void API_MessageReceived(object? sender, MessageUpdatedEventArgs e)
        {
            if (GetSubscriber() is ConnectionViewModel subscriber)
            {
                if (e.Message.Type == MessageTypes.State)
                {
                    switch ((Statuses)e.Message.NewValue)
                    {
                        case Statuses.Initializing:     subscriber.VisualState = "NotConnected"; break;
                        default:                        subscriber.VisualState = "Connected"; break;
                    }
                }
            }
        }

        public override void Unsubscribe()
        {
            Publisher.API.MessageReceived -= API_MessageReceived;
        }
    }

    private readonly ConnectionViewModelSubscriber connectionViewModelSubscriber;

    public ConnectionViewModel(ControllerViewModel controller)
    {
        Controller = controller;
        if (controller.Device.Uri?.Scheme == "tcp")
        {
            Address = controller.Device.Uri.Host;
            Port = controller.Device.Uri.Port;
        }

        Connect = new AsyncRelayCommand(ConnectExecuteAsync);
        Cancel = new RelayCommand(CancelExecute);

        connectionViewModelSubscriber = new ConnectionViewModelSubscriber(this, controller.Device);
        connectionViewModelSubscriber.SubScribe();
    }

    public ControllerViewModel Controller
    {
        get; init;
    }

    private string? address = "localhost";
    public string? Address
    {
        get => address;
        set => SetProperty(ref address, value);
    }

    private int port = 4242;
    public int Port
    {
        get => port;
        set => SetProperty(ref port, value);
    }

    public ICommand Connect
    {
        get;
    }

    private async Task ConnectExecuteAsync()
    {
        Controller.VisualState = "Connecting";

        Controller.Device.Uri = new Uri($"tcp://{Address}:{Port}");
        if (await Controller.Device.ConnectAsync(CancellationToken.None))
        {
            Controller.VisualState = "Ready";
        }
        else
        {
            Controller.VisualState = "NotConnected";
        }
    }

    public ICommand Cancel
    {
        get;
    }

    private void CancelExecute()
    {
        Controller.Device.Disconnect();
        Controller.VisualState = "NotConnected";
    }

    public void Dispose() => connectionViewModelSubscriber.Unsubscribe();
}
