using Chromass.ChroZenPump.PacketWrappers;
using ChromassProtocols.APIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chromass.ChroZenPump.APIs
{
    public class Configuration : Base<Packets.Configuration>
    {
        public Configuration(ConfigurationWrapper packet, Action? action) : base(packet, action) { }
        public Configuration(Configuration src) : base(new ConfigurationWrapper { Packet = src.Wrapper.Packet }, null) { }

        public PumpModes Mode => Wrapper.Packet.btMode;
        
        public float MaxFlowLimit
        {
            get => Wrapper.Packet.fMaxFlow;
            set
            {
                if (Wrapper.Packet.fMaxFlow != value)
                {
                    Wrapper.Packet.fMaxFlow = value;
                    CallAction();
                }
            }
        }
        
        public float MaxPressureLimit
        {
            get => Wrapper.Packet.fMaxPressure;
            set
            {
                if(Wrapper.Packet.fMaxPressure != value)
                {
                    Wrapper.Packet.fMaxPressure = value;
                    CallAction();
                }
            }
        }

        public float HeadVolume => Wrapper.Packet.fHeadVolume;

        public float FlowCalibrationOffset
        {
            get => Wrapper.Packet.fFlowCalOffset1;
            set
            {
                if (Wrapper.Packet.fFlowCalOffset1 != value)
                {
                    Wrapper.Packet.fFlowCalOffset1 = value;
                    CallAction();
                }
            }
        }

        public float PressureCalibrationFactor
        {
            get => Wrapper.Packet.fZeroBalance1;
            set
            {
                if(Wrapper.Packet.fZeroBalance1 != value)
                {
                    Wrapper.Packet.fZeroBalance1 = value;
                    CallAction();
                }
            }
        }
    }
}
