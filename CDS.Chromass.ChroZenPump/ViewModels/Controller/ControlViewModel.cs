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
public class ControlViewModel : ObservableObject
{
    public ControllerViewModel Controller
    {
        get; init;
    }

    public ControlViewModel(ControllerViewModel controller)
    {
        Controller = controller;

        new WeakEventSubscriber<ControlViewModel, StateUpdatedEventArgs>(this,
            (s, e) =>
            {
                Status = e.State.Status;
                Error = e.State.Error;
                ElapsedTime = e.State.ElapsedTime;
                ActualFlow = e.State.Flow;
                Pressure = e.State.Pressure;
                ActualA = e.State.A;
                ActualB = e.State.B;
                ActualC = e.State.C;
                ActualD = e.State.D;
            },
            h => Controller.Device.API.StateUpdated += h,
            h => Controller.Device.API.StateUpdated -= h);

        new WeakEventSubscriber<ControlViewModel, SetupUpdatedEventArgs>(this,
            (s, e) => OnPropertyChanged(string.Empty),
            h => Controller.Device.API.SetupUpdated += h,
            h => Controller.Device.API.SetupUpdated -= h);
    }

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
