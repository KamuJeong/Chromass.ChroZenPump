using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Chromass.ChroZenPump.UI.ViewModels;

public class SystemViewModel : ObservableObject
{
    public APIViewModel APIViewModel
    {
        get; init;
    }

    public SystemViewModel(APIViewModel apiViewModel)
    {
        APIViewModel = apiViewModel;

        APIViewModel.API.Controller.Information.Updated += (s, e) => OnPropertyChanged(string.Empty);
    }

    public string? Model => APIViewModel.API.Information.Model;
    public int Version => APIViewModel.API.Information.Version;
    public string? SN => APIViewModel.API.Information.SN;
    public Modes Mode => APIViewModel.API.Configuration.Mode;
    public double MaxSettingFlowLimit
    {
        get => APIViewModel.API.Configuration.MaxSettingFlowLimit;
        set => SetProperty(APIViewModel.API.Configuration.MaxSettingFlowLimit, value, APIViewModel.API,
            (api, v) => api.Configuration.MaxSettingFlowLimit = v);
    }
    public double MaxSettingPressureLimit
    {
        get => APIViewModel.API.Configuration.MaxSettingPressureLimit;
        set => SetProperty(APIViewModel.API.Configuration.MaxSettingPressureLimit, value, APIViewModel.API,
            (api, v) => api.Configuration.MaxSettingPressureLimit = v);
    }
    public double HeadVolume => APIViewModel.API.Configuration.HeadVolume;
    public double FlowCalibrationOffset
    {
        get => APIViewModel.API.Configuration.FlowCalibrationOffset;
        set => SetProperty(APIViewModel.API.Configuration.FlowCalibrationOffset, value, APIViewModel.API,
            (api, v) => api.Configuration.FlowCalibrationOffset = v);
    }
    public double PressureCalibrationFactor
    {
        get => APIViewModel.API.Configuration.PressureCalibrationFactor;
        set => SetProperty(APIViewModel.API.Configuration.PressureCalibrationFactor, value, APIViewModel.API,
            (api, v) => api.Configuration.PressureCalibrationFactor = v);

    }

    public bool IsRinsePumpEnabled
    {
        get => APIViewModel.API.Configuration.IsRinsePumpEnabled;
        set => SetProperty(APIViewModel.API.Configuration.IsRinsePumpEnabled, value, APIViewModel.API,
            (api, v) => api.Configuration.IsRinsePumpEnabled = v);
    }

    public bool IsBuzzerEnabled
    {
        get => APIViewModel.API.Configuration.IsBuzzerEnabled;
        set => SetProperty(APIViewModel.API.Configuration.IsBuzzerEnabled, value, APIViewModel.API,
            (api, v) => api.Configuration.IsBuzzerEnabled = v);
    }
    public bool IsDegassorEnabled
    {
        get => APIViewModel.API.Configuration.IsDegassorEnabled;
        set => SetProperty(APIViewModel.API.Configuration.IsDegassorEnabled, value, APIViewModel.API,
            (api, v) => api.Configuration.IsDegassorEnabled = v);
    }
}
