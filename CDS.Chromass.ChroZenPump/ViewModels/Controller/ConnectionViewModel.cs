using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Chromass.ChroZenPump;
using Chromass.ChroZenPump.APIs;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;

namespace CDS.Chromass.ChroZenPump.ViewModels;
public class ConnectionViewModel : ObservableObject
{
    public ConnectionViewModel(ControllerViewModel controller)
    {
        Controller = controller;
        if (controller.Device.Uri?.Scheme == "tcp")
        {
            Address = controller.Device.Uri.Host;
            Port = controller.Device.Uri.Port;
        }


//        Controller.Device.API.MessageReceived += API_MessageReceived;
        Connect = new AsyncRelayCommand(ConnectExecuteAsync);
        Cancel = new RelayCommand(CancelExecute);
    }

    private string visualState = "NotConnected";
    public string VisualState
    {
        get => visualState;
        private set => SetProperty(ref visualState, value);
    }


    private void API_MessageReceived(object? sender, Message e)
    {
        if (e.Type == MessageTypes.State)
        {
            switch ((Statuses)e.NewValue)
            {
                case Statuses.Initializing: VisualState = "NotConnected"; break;
                default: VisualState = "Connected"; break;
            }
        }
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
        VisualState = "Connecting";

        Controller.Device.Uri = new Uri($"tcp://{Address}:{Port}");
        if (await Controller.Device.ConnectAsync(CancellationToken.None))
        {
            VisualState = "Connected";
        }
        else
        {
            VisualState = "NotConnected";
        }
    }

    public ICommand Cancel
    {
        get;
    }

    private void CancelExecute()
    {
        Controller.Device.Disconnect();
        VisualState = "NotConnected";
    }
}
