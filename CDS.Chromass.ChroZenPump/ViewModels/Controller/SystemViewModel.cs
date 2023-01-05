using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Chromass.ChroZenPump;
using CDS.Core;
using ChromassProtocol;
using Chromass.ChroZenPump.Packets;
using Chromass.ChroZenPump.APIs;
using System.Diagnostics.CodeAnalysis;

namespace CDS.Chromass.ChroZenPump.ViewModels;

public class SystemViewModel : ObservableObject
{
    [NotNull]
    public ControllerViewModel Controller
    {
        get; init;
    }

    public SystemViewModel([NotNull]  ControllerViewModel controller)
    {
        Controller = controller;

        new WeakEventSubscriber<API, SystemViewModel, InformationUpdatedEventArgs>(
            Controller.Device.API,
            nameof(Controller.Device.API.InformationUpdated),
            this, 
            (sub, s, e) => sub.OnPropertyChanged(string.Empty));

        new WeakEventSubscriber<API, SystemViewModel, ConfigurationUpdatedEventArgs>(
            Controller.Device.API,
            nameof(Controller.Device.API.ConfigurationUpdated),
            this, 
            (sub, s, e) => sub.OnPropertyChanged(string.Empty));
    }

    public string? Model => Controller.Device!.API!.Information.Model;
    public int Version => Controller.Device!.API!.Information.Version;
    public string? SN => Controller.Device!.API!.Information.SN;
    public Modes Mode => Controller.Device!.API!.Configuration.Mode;
    public double MaxSettingFlowLimit
    {
        get => Controller.Device!.API!.Configuration.MaxSettingFlowLimit;
        set => SetProperty(Controller.Device!.API!.Configuration.MaxSettingFlowLimit, value, Controller.Device.API,
            (api, v) => api.Configuration.MaxSettingFlowLimit = v);
    }
    public double MaxSettingPressureLimit
    {
        get => Controller.Device!.API!.Configuration.MaxSettingPressureLimit;
        set => SetProperty(Controller.Device!.API!.Configuration.MaxSettingPressureLimit, value, Controller.Device.API,
            (api, v) => api.Configuration.MaxSettingPressureLimit = v);
    }
    public double HeadVolume => Controller.Device!.API!.Configuration.HeadVolume;
    public double FlowCalibrationOffset
    {
        get => Controller.Device!.API!.Configuration.FlowCalibrationOffset;
        set => SetProperty(Controller.Device!.API!.Configuration.FlowCalibrationOffset, value, Controller.Device.API,
            (api, v) => api.Configuration.FlowCalibrationOffset = v);
    }

    public double PressureCalibrationFactor
    {
        get => Controller.Device!.API!.Configuration.PressureCalibrationFactor;
        set => SetProperty(Controller.Device!.API!.Configuration.PressureCalibrationFactor, value, Controller.Device.API,
            (api, v) => api.Configuration.PressureCalibrationFactor = v);

    }

    public bool IsRinsePumpEnabled
    {
        get => Controller.Device!.API!.Configuration.IsRinsePumpEnabled;
        set => SetProperty(Controller.Device!.API!.Configuration.IsRinsePumpEnabled, value, Controller.Device.API,
            (api, v) => api.Configuration.IsRinsePumpEnabled = v);
    }

    public bool IsBuzzerEnabled
    {
        get => Controller.Device.API.Configuration.IsBuzzerEnabled;
        set => SetProperty(Controller.Device.API.Configuration.IsBuzzerEnabled, value, Controller.Device.API,
            (api, v) => api.Configuration.IsBuzzerEnabled = v);
    }
    public bool IsDegassorEnabled
    {
        get => Controller.Device.API.Configuration.IsDegassorEnabled;
        set => SetProperty(Controller.Device.API.Configuration.IsDegassorEnabled, value, Controller.Device.API,
            (api, v) => api.Configuration.IsDegassorEnabled = v);
    }
}
