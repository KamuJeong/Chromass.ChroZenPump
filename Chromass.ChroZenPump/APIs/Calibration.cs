using Chromass.ChroZenPump.PacketWrappers;
using ChromassProtocol;
using ChromassProtocols.APIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chromass.ChroZenPump.APIs;

public class CalibrationPoint
{
    public float SettingFlow { get; set; }
    public float MeasuredFlow { get; set; }
    public float DisplayedPressure { get; set; }
}

public class Calibration : Base<Packets.Calibration>
{
    public Calibration(PacketWrapper<Packets.Calibration> wrapper, Action? action) : base(wrapper, action)
    {
    }

    public Calibration(Calibration src) : base(new CalibrationWrapper { Packet = src.Wrapper.Packet }, null)
    {
    }

    public IEnumerable<CalibrationPoint> Points
    {
        get => Enumerable.Range(0, Wrapper.Packet.Count)
                                    .Select(i => new CalibrationPoint
                                    {
                                        SettingFlow = Wrapper.Packet.SetFlow[i],
                                        MeasuredFlow = Wrapper.Packet.ActFlow[i],
                                        DisplayedPressure = Wrapper.Packet.Pressure[i]
                                    });

        set
        {
            Wrapper.Packet.Count = Points.Count();
            for (int i = 0; i < Wrapper.Packet.Count; i++)
            {
                Wrapper.Packet.SetFlow[i] = Points.ElementAt(i).SettingFlow;
                Wrapper.Packet.ActFlow[i] = Points.ElementAt(i).MeasuredFlow;
                Wrapper.Packet.Pressure[i] = Points.ElementAt(i).DisplayedPressure;
            }

            CallAction();
        }
    }
}

public class CalibrationUpdatedEventArgs : EventArgs
{
    public Calibration Calibration
    {
        get; init;
    }
    public CalibrationUpdatedEventArgs(Calibration calibration)
    {
        Calibration = calibration;
    }
}
