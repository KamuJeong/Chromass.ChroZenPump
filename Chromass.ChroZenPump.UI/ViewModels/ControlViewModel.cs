using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chromass.ChroZenPump.APIs;
using ChromassProtocol;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Chromass.ChroZenPump.UI.ViewModels;
public class ControlViewModel : ObservableObject
{
    public APIViewModel APIViewModel
    {
        get; init;
    }

    public ControlViewModel(APIViewModel apiViewModel)
    {
        APIViewModel = apiViewModel;

        APIViewModel.API.State.Wrapper.Updated += API_StateUpdated;
        APIViewModel.API.Setup.Wrapper.Updated += API_SetupUpdated;
    }

    private void API_SetupUpdated(object? sender, PacketUpdatedEventArgs<Packets.Setup> e)
    {
        OnPropertyChanged(string.Empty);
    }

    private void API_StateUpdated(object? sender, PacketUpdatedEventArgs<Packets.State> e)
    {
        Status = APIViewModel.API.State.Status;
        Error = APIViewModel.API.State.Error;
        ElapsedTime = APIViewModel.API.State.ElapsedTime;
        ActualFlow = APIViewModel.API.State.Flow;
        Pressure = APIViewModel.API.State.Pressure;
        ActualA = APIViewModel.API.State.A;
        ActualB = APIViewModel.API.State.B;
        ActualC = APIViewModel.API.State.C;
        ActualD = APIViewModel.API.State.D;
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
        get => APIViewModel.API.Setup.Flow;
        set => SetProperty(APIViewModel.API.Setup.Flow, value, APIViewModel.API,
            (api, v) => api.Setup.Flow = v);
    }

    private double pressure;
    public double Pressure
    {
        get => pressure;
        private set => SetProperty(ref pressure, value);
    }

    private double a, b, c, d;
    public double ActualA
    {
        get => a;
        private set => SetProperty(ref a, value);
    }

    public int SettingA
    {
        get => APIViewModel.API.Setup.A;
        set => SetProperty(APIViewModel.API.Setup.A, value, APIViewModel.API,
            (api, v) => 
            {
                api.Setup.A = v;
            });
    }

    public double ActualB
    {
        get => b;
        private set => SetProperty(ref b, value);
    }

    public int SettingB
    {
        get => APIViewModel.API.Setup.B;
        set => SetProperty(APIViewModel.API.Setup.B, value, APIViewModel.API,
            (api, v) => api.Setup.B = v);
    }

    public double ActualC
    {
        get => c;
        private set => SetProperty(ref c, value);
    }

    public int SettingC
    {
        get => APIViewModel.API.Setup.C;
        set => SetProperty(APIViewModel.API.Setup.C, value, APIViewModel.API,
            (api, v) => api.Setup.C = v);
    }

    public double ActualD
    {
        get => d;
        private set => SetProperty(ref d, value);
    }
}
