using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Chromass.ChroZenPump;
using CDS.Core;

namespace CDS.Chromass.ChroZenPump.ViewModels;

public class SystemViewModel : ObservableObject, IDisposable
{
    private class SystemViewModelSubscriber : WeakEventSubscriber<SystemViewModel, ChroZenPumpDevice>
    {
        public SystemViewModelSubscriber(SystemViewModel subscriber, ChroZenPumpDevice publisher) : base(subscriber, publisher)
        {
        }

        public override void SubScribe()
        {
            Publisher.API.Controller.Information.Updated += Information_Updated;
            Publisher.API.Controller.Configuration.Updated += Configuration_Updated;
        }

        private void Configuration_Updated(object? sender, ChromassProtocol.PacketUpdatedEventArgs<global::Chromass.ChroZenPump.Packets.Configuration> e)
        {
            if (GetSubscriber() is SystemViewModel subscriber)
            {
                subscriber.OnPropertyChanged(string.Empty);
            }
        }

        private void Information_Updated(object? sender, ChromassProtocol.PacketUpdatedEventArgs<global::Chromass.ChroZenPump.Packets.Information> e)
        {
            if (GetSubscriber() is SystemViewModel subscriber)
            {
                subscriber.OnPropertyChanged(string.Empty);
            }
        }

        public override void Unsubscribe()
        {
            Publisher.API.Controller.Information.Updated -= Information_Updated;
            Publisher.API.Controller.Configuration.Updated -= Configuration_Updated;
        }
    }


    public ControllerViewModel Controller
    {
        get; init;
    }

    private readonly SystemViewModelSubscriber systemViewModelSubscriber;

    public SystemViewModel(ControllerViewModel controller)
    {
        Controller = controller;

        systemViewModelSubscriber = new SystemViewModelSubscriber(this, Controller.Device);
        systemViewModelSubscriber.SubScribe();
    }

    public string? Model => Controller.Device.API.Information.Model;
    public int Version => Controller.Device.API.Information.Version;
    public string? SN => Controller.Device.API.Information.SN;
    public Modes Mode => Controller.Device.API.Configuration.Mode;
    public double MaxSettingFlowLimit
    {
        get => Controller.Device.API.Configuration.MaxSettingFlowLimit;
        set => SetProperty(Controller.Device.API.Configuration.MaxSettingFlowLimit, value, Controller.Device.API,
            (api, v) => api.Configuration.MaxSettingFlowLimit = v);
    }
    public double MaxSettingPressureLimit
    {
        get => Controller.Device.API.Configuration.MaxSettingPressureLimit;
        set => SetProperty(Controller.Device.API.Configuration.MaxSettingPressureLimit, value, Controller.Device.API,
            (api, v) => api.Configuration.MaxSettingPressureLimit = v);
    }
    public double HeadVolume => Controller.Device.API.Configuration.HeadVolume;
    public double FlowCalibrationOffset
    {
        get => Controller.Device.API.Configuration.FlowCalibrationOffset;
        set => SetProperty(Controller.Device.API.Configuration.FlowCalibrationOffset, value, Controller.Device.API,
            (api, v) => api.Configuration.FlowCalibrationOffset = v);
    }

    public double PressureCalibrationFactor
    {
        get => Controller.Device.API.Configuration.PressureCalibrationFactor;
        set => SetProperty(Controller.Device.API.Configuration.PressureCalibrationFactor, value, Controller.Device.API,
            (api, v) => api.Configuration.PressureCalibrationFactor = v);

    }

    public bool IsRinsePumpEnabled
    {
        get => Controller.Device.API.Configuration.IsRinsePumpEnabled;
        set => SetProperty(Controller.Device.API.Configuration.IsRinsePumpEnabled, value, Controller.Device.API,
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

    public void Dispose() => systemViewModelSubscriber.Unsubscribe();
}
