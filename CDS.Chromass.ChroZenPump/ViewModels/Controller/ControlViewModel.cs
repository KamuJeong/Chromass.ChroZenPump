using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chromass.ChroZenPump.APIs;
using Chromass.ChroZenPump;
using ChromassProtocol;
using CommunityToolkit.Mvvm.ComponentModel;
using CDS.Core;

namespace CDS.Chromass.ChroZenPump.ViewModels;
public class ControlViewModel : ObservableObject, IDisposable
{
    private class ControlViewModelSubscriber : WeakEventSubscriber<ControlViewModel, ChroZenPumpDevice>
    {
        public ControlViewModelSubscriber(ControlViewModel subscriber, ChroZenPumpDevice publisher) : base(subscriber, publisher)
        {
        }

        public override void SubScribe()
        {
            Publisher.API.StateUpdated += API_StateUpdated;
            Publisher.API.Controller.Setup.Updated += Setup_Updated;
        }

        private void Setup_Updated(object? sender, PacketUpdatedEventArgs<global::Chromass.ChroZenPump.Packets.Setup> e)
        {
            if (GetSubscriber() is ControlViewModel subscriber)
            {
                subscriber.OnPropertyChanged(string.Empty);
            }
        }

        private void API_StateUpdated(object? sender, StateUpdatedEventArgs e)
        {
            if (GetSubscriber() is ControlViewModel subscriber)
            {
                subscriber.Status = e.State.Status;
                subscriber.Error = e.State.Error;
                subscriber.ElapsedTime = e.State.ElapsedTime;
                subscriber.ActualFlow = e.State.Flow;
                subscriber.Pressure = e.State.Pressure;
                subscriber.ActualA = e.State.A;
                subscriber.ActualB = e.State.B;
                subscriber.ActualC = e.State.C;
                subscriber.ActualD = e.State.D;
            }
        }

        public override void Unsubscribe()
        {
            Publisher.API.StateUpdated -= API_StateUpdated;
            Publisher.API.Controller.Setup.Updated -= Setup_Updated;
        }
    }


    public ControllerViewModel Controller
    {
        get; init;
    }

    private readonly ControlViewModelSubscriber controlViewModelSubscriber;

    public ControlViewModel(ControllerViewModel controller)
    {
        Controller = controller;

        controlViewModelSubscriber = new ControlViewModelSubscriber(this, Controller.Device);
        controlViewModelSubscriber.SubScribe();
    }

    public void Dispose() => controlViewModelSubscriber.Unsubscribe();

    private Statuses status;
    public Statuses Status
    {
        get => status;
        private set
        {
            SetProperty(ref status, value);
            switch (value)
            {
                case Statuses.Initializing:
                    Controller.VisualState = "NotConnected";
                    break;
                case Statuses.Ready:
                    Controller.VisualState = "Normal";
                    break;
                case Statuses.Service:
                    Controller.VisualState = "Service";
                    break;
                case Statuses.Purge:
                    Controller.VisualState = "Purge";
                    break;
                case Statuses.Error:
                    Controller.VisualState = "Error";
                    break;
                default:
                    Controller.VisualState = "Blocked";
                    break;
            }
        }
    }

    private Errors error;
    public Errors Error
    {
        get => error;
        private set => SetProperty(ref error, value);
    }

    private double elapsedTime;
    public double ElapsedTime
    {
        get => elapsedTime;
        private set => SetProperty(ref elapsedTime, value);
    }

    private double actualFlow;
    public double ActualFlow
    {
        get => actualFlow;
        private set => SetProperty(ref actualFlow, value);
    }

    public double SettingFlow
    {
        get => Controller.Device.API.Setup.Flow;
        set => SetProperty(Controller.Device.API.Setup.Flow, value, Controller.Device.API,
            (api, v) => api.Setup.Flow = v);
    }

    private double pressure;
    public double Pressure
    {
        get => pressure;
        private set => SetProperty(ref pressure, value);
    }

    private int a, b, c, d;
    public int ActualA
    {
        get => a;
        private set => SetProperty(ref a, value);
    }

    public int SettingA
    {
        get => Controller.Device.API.Setup.A;
        set => SetProperty(Controller.Device.API.Setup.A, value, Controller.Device.API,
            (api, v) =>
            {
                api.Setup.A = v;
            });
    }

    public int ActualB
    {
        get => b;
        private set => SetProperty(ref b, value);
    }

    public int SettingB
    {
        get => Controller.Device.API.Setup.B;
        set => SetProperty(Controller.Device.API.Setup.B, value, Controller.Device.API,
            (api, v) => api.Setup.B = v);
    }

    public int ActualC
    {
        get => c;
        private set => SetProperty(ref c, value);
    }

    public int SettingC
    {
        get => Controller.Device.API.Setup.C;
        set => SetProperty(Controller.Device.API.Setup.C, value, Controller.Device.API,
            (api, v) => api.Setup.C = v);
    }

    public int ActualD
    {
        get => d;
        private set => SetProperty(ref d, value);
    }
}
