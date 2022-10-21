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


public class MonitorViewModel : ObservableObject, IDisposable
{
    private class MonitorViewModelSubscriber : WeakEventSubscriber<MonitorViewModel, ChroZenPumpDevice>
    {
        public MonitorViewModelSubscriber(MonitorViewModel subscriber, ChroZenPumpDevice publisher) : base(subscriber, publisher)
        {
        }

        public override void SubScribe()
        {
            Publisher.API.MessageReceived += API_MessageReceived;
            Publisher.API.StateUpdated += API_StateUpdated;       
        }

        private void API_StateUpdated(object? sender, StateUpdatedEventArgs e)
        {
            if (GetSubscriber() is MonitorViewModel subscriber)
            {
                subscriber.OnPropertyChanged(String.Empty);
            }
        }

        private void API_MessageReceived(object? sender, MessageUpdatedEventArgs e)
        {
            if (GetSubscriber() is MonitorViewModel subscriber)
            {
                if (e.Message.Type == MessageTypes.State)
                {
                    switch ((Statuses)e.Message.NewValue)
                    {
                        case Statuses.Error:    subscriber.VisualState = "Error"; break;
                        case Statuses.Run:      subscriber.VisualState = "Run"; break;
                        default:                subscriber.VisualState = "Normal"; break;
                    }
                }
            }
        }

        public override void Unsubscribe()
        {
            Publisher.API.MessageReceived -= API_MessageReceived;
            Publisher.API.StateUpdated -= API_StateUpdated;
        }
    }


    public ChroZenPumpDevice Device
    {
        get; init;
    }

    private readonly MonitorViewModelSubscriber monitorViewModelSubscriber;

    public MonitorViewModel(Device device)
    {
        if (device is not ChroZenPumpDevice)
        {
            throw new ArgumentException(null, nameof(device));
        }

        Device = (ChroZenPumpDevice)device;

        monitorViewModelSubscriber = new MonitorViewModelSubscriber(this, Device);
        monitorViewModelSubscriber.SubScribe();
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

    public void Dispose() => monitorViewModelSubscriber.Unsubscribe();
}
