using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CDS.Core;
using CDS.InstrumentModel;
using Chromass.ChroZenPump;
using Chromass.ChroZenPump.APIs;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml;

namespace CDS.Chromass.ChroZenPump.ViewModels;
public class MonitorViewModel : ObservableObject
{
    public ChroZenPumpDevice Device
    {
        get; init;
    }

    public MonitorViewModel(Device device)
    {
        if (device is not ChroZenPumpDevice)
        {
            throw new ArgumentException(null, nameof(device));
        }

        Device = (ChroZenPumpDevice)device;

        Device.API.MessageReceived += API_MessageReceived;
        Device.API.StateUpdated += API_StateUpdated;
    }

    private string visualState = "Normal";
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
                case Statuses.Error:    VisualState = "Error";  break;
                case Statuses.Run:      VisualState = "Run";    break;
                default:                VisualState = "Normal";  break;
            }
        }
    }

    private void API_StateUpdated(object? sender, State e)
    {
        OnPropertyChanged(String.Empty);
    }

    public Statuses Status => Device.API.State.Status;

    public Errors Error => Device.API.State.Error;

    public double ElapsedTime => Device.API.State.ElapsedTime;

    public double Flow => Device.API.State.Flow;
    public int A => Device.API.State.A;
    public int B => Device.API.State.B;
    public int C => Device.API.State.C;
    public int D => Device.API.State.D;

    public string FlowDesc => $"{Flow} mL/min [{A}:{B}:{C}:{D}]";

    public double Pressure => Device.API.State.Pressure;
}
