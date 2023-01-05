using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CDS.InstrumentModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CDS.Chromass.ChroZenPump.ViewModels;
public class ControllerViewModel : ObservableObject
{
    [NotNull]
    public ChroZenPumpDevice Device
    {
        get; init;
    }

    public ConnectionViewModel ConnectionViewModel
    {
        get; init;
    }

    public SystemViewModel SystemViewModel
    {
        get; init;
    }

    public ControlViewModel ControlViewModel
    {
        get; init;
    }


    public ControllerViewModel(Device device)
    {
        if (device is not ChroZenPumpDevice)
        {
            throw new ArgumentException(null, nameof(device));
        }

        Device = (ChroZenPumpDevice)device;
        ConnectionViewModel = new ConnectionViewModel(this);
        SystemViewModel = new SystemViewModel(this);
        ControlViewModel = new ControlViewModel(this);
    }

    private string visualState = "NotConnected";
    public string VisualState
    {
        get => visualState;
        internal set => SetProperty(ref visualState, value);
    }
}
