using Chromass.ChroZenPump.PacketWrappers;
using ChromassProtocols.APIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chromass.ChroZenPump.APIs;

public class Configuration : Base<Packets.Configuration>
{
    public Configuration(ConfigurationWrapper packet, Action? action) : base(packet, action) { }
    public Configuration(Configuration src) : base(new ConfigurationWrapper { Packet = src.Wrapper.Packet }, null) { }

    public Modes Mode => Wrapper.Packet.btMode;

    public double MaxSettingFlowLimit
    {
        get => Math.Round(Wrapper.Packet.fMaxFlow, 2);
        set
        {
            if (Math.Round(Wrapper.Packet.fMaxFlow, 2) != value)
            {
                Wrapper.Packet.fMaxFlow = (float)value;
                CallAction();
            }
        }
    }

    public double MaxSettingPressureLimit
    {
        get => Math.Round(Wrapper.Packet.fMaxPressure);
        set
        {
            if (Math.Round(Wrapper.Packet.fMaxPressure) != value)
            {
                Wrapper.Packet.fMaxPressure = (float)value;
                CallAction();
            }
        }
    }

    public double HeadVolume => Math.Round(Wrapper.Packet.fHeadVolume, 3);

    public double FlowCalibrationOffset
    {
        get => Math.Round(Wrapper.Packet.fFlowCalOffset1, 3);
        set
        {
            if (Math.Round(Wrapper.Packet.fFlowCalOffset1, 3) != value)
            {
                Wrapper.Packet.fFlowCalOffset1 = (float)value;
                CallAction();
            }
        }
    }

    public double PressureCalibrationFactor
    {
        get => Math.Round(Wrapper.Packet.fZeroBalance1, 3);
        set
        {
            if (Math.Round(Wrapper.Packet.fZeroBalance1, 3) != value)
            {
                Wrapper.Packet.fZeroBalance1 = (float)value;
                CallAction();
            }
        }
    }

    public bool IsRinsePumpEnabled
    {
        get => Wrapper.Packet.btRinsePumpOnOff != 0;
        set
        {
            if ((Wrapper.Packet.btRinsePumpOnOff != 0) != value)
            {
                Wrapper.Packet.btRinsePumpOnOff = (byte)(value ? 1 : 0);
            }
        }
    }

    public bool IsBuzzerEnabled
    {
        get => Wrapper.Packet.btBuzzerEnable != 0;
        set
        {
            if ((Wrapper.Packet.btBuzzerEnable != 0) != value)
            {
                Wrapper.Packet.btBuzzerEnable = (byte)(value ? 1 : 0);
            }
        }
    }
    public bool IsDegassorEnabled
    {
        get => Wrapper.Packet.btDegassorOnOff != 0;
        set
        {
            if ((Wrapper.Packet.btDegassorOnOff != 0) != value)
            {
                Wrapper.Packet.btDegassorOnOff = (byte)(value ? 1 : 0);
            }
        }
    }
}
