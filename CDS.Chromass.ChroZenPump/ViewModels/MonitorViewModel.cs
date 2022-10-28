using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CDS.Core;
using CDS.InstrumentModel;
using Chromass.ChroZenPump;
using Chromass.ChroZenPump.APIs;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

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

        new WeakEventSubscriber<MonitorViewModel, StateUpdatedEventArgs>(this, 
            (s, e) => OnPropertyChanged(string.Empty),                        
            h => Device.API.StateUpdated += h, 
            h => Device.API.StateUpdated -= h);
        new WeakEventSubscriber<MonitorViewModel, MessageUpdatedEventArgs>(this, 
            (s, e) =>
            {
                if (e.Message.Type == MessageTypes.State)
                {
                    switch ((Statuses)e.Message.NewValue)
                    {
                        case Statuses.Error:    VisualState = "Error"; break;
                        case Statuses.Run:      VisualState = "Run"; break;
                        default:                VisualState = "Normal"; break;
                    }
                }
            },                        
            h => Device.API.MessageReceived += h, 
            h => Device.API.MessageReceived -= h);
    }

    private string visualState = "Normal";
    public string VisualState
    {
        get => visualState;
        private set => SetProperty(ref visualState, value);
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
