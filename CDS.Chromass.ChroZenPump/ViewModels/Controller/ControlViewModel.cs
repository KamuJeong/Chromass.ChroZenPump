using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chromass.ChroZenPump.APIs;
using Chromass.ChroZenPump;
using ChromassProtocol;
using CommunityToolkit.Mvvm.ComponentModel;

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

        Controller.Device.API.StateUpdated += API_StateUpdated;
        Controller.Device.API.Setup.Wrapper.Updated += API_SetupUpdated;
    }

    private void API_SetupUpdated(object? sender, PacketUpdatedEventArgs<global::Chromass.ChroZenPump.Packets.Setup> e)
        => OnPropertyChanged(string.Empty);

    private void API_StateUpdated(object? sender, State e)
    {
        Status = e.Status;
        Error = e.Error;
        ElapsedTime = e.ElapsedTime;
        ActualFlow = e.Flow;
        Pressure = e.Pressure;
        ActualA = e.A;
        ActualB = e.B;
        ActualC = e.C;
        ActualD = e.D;
    }

    private Statuses status;    
    public Statuses Status
    {
        get => status;
        private set => SetProperty(ref status, value);
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
