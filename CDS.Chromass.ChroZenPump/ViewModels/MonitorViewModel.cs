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

        Device.API.StateUpdated += API_StateUpdated;
    }

    private void API_StateUpdated(object? sender, State e)
    {
        OnPropertyChanged(String.Empty);
    }

    public Statuses Status => Device.API.State.Status;

    public Errors Error => Device.API.State.Error;

    public double ElapsedTime => Device.API.State.ElapsedTime;

    public double Flow => Device.API.State.Flow;
    public double A => Device.API.State.A;
    public double B => Device.API.State.B;
    public double C => Device.API.State.C;
    public double D => Device.API.State.D;

    public double Pressure => Device.API.State.Pressure;
}
