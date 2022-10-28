using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using CDS.Core;
using CDS.InstrumentModel;
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

        Connect = new AsyncRelayCommand(ConnectExecuteAsync);
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
        if (Controller.VisualState == "NotConnected")
        {
            Controller.VisualState = "Connecting";

            Controller.Device.Uri = new Uri($"tcp://{Address}:{Port}");
            if (!await Controller.Device.ConnectAsync(CancellationToken.None))
            {
                Controller.VisualState = "NotConnected";
            }
        }
        else
        {
            Controller.Device.Disconnect();
            Controller.VisualState = "NotConnected";
        }
    }
}
