using Chromass.ChroZenPump.PacketWrappers;
using ChromassProtocol;
using ChromassProtocols.APIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chromass.ChroZenPump.APIs
{
    public class Event : Base<Packets.Event>
    {
        public Event(EventWrapper wrapper, Action? action) : base(wrapper, action)
        {
            wrapper.Packet.btCurve = EventCurves.None;
        }

        public Event(Event src) : base(new EventWrapper { Packet = src.Wrapper.Packet }, null) { }

        public float Time
        {
            get => Wrapper.Packet.fTime;
            set
            {
                if (Wrapper.Packet.fTime != value)
                {
                    Wrapper.Packet.fTime = value;
                    CallAction();
                }
            }
        }
        public SwitchOutputs Switch1
        {
            get => Wrapper.Packet.btSwitch1;
            set
            {
                if (Wrapper.Packet.btSwitch1 != value)
                {
                    Wrapper.Packet.btSwitch1 = value;
                    CallAction();
                }
            }
        }
        public SwitchOutputs Swicth2
        {
            get => Wrapper.Packet.btSwitch2;
            set
            {
                if (Wrapper.Packet.btSwitch2 != value)
                {
                    Wrapper.Packet.btSwitch2 = value;
                    CallAction();
                }
            }
        }
        public MarkOutputs MarkOut
        {
            get => Wrapper.Packet.btMarkOut;
            set
            {
                if (Wrapper.Packet.btMarkOut != value)
                {
                    Wrapper.Packet.btMarkOut = value;
                    CallAction();
                }
            }
        }
    }

    public class Gradient : Base<Packets.Event>
    {
        public Gradient(PacketWrapper<Packets.Event> wrapper, Action? action) : base(wrapper, action)
        {
            wrapper.Packet.btCurve = EventCurves.Lean;
        }

        public Gradient(Gradient src) : base(new EventWrapper { Packet = src.Wrapper.Packet }, null) { }

        public float Time
        {
            get => Wrapper.Packet.fTime;
            set
            {
                if (Wrapper.Packet.fTime != value)
                {
                    Wrapper.Packet.fTime = value;
                    CallAction();
                }
            }
        }

        public float Flow
        {
            get => Wrapper.Packet.fFlowSpeed;
            set
            {
                if (Wrapper.Packet.fFlowSpeed != value)
                {
                    Wrapper.Packet.fFlowSpeed = value;
                    CallAction();
                }
            }
        }

        public float A
        {
            get => Wrapper.Packet.fRatio[0];
            set
            {
                if (Wrapper.Packet.fRatio[0] != value)
                {
                    Wrapper.Packet.fRatio[0] = value;
                    CallAction();
                }
            }
        }

        public float B
        {
            get => Wrapper.Packet.fRatio[1];
            set
            {
                if (Wrapper.Packet.fRatio[1] != value)
                {
                    Wrapper.Packet.fRatio[1] = value;
                    CallAction();
                }
            }
        }

        public float C
        {
            get => Wrapper.Packet.fRatio[2];
            set
            {
                if (Wrapper.Packet.fRatio[2] != value)
                {
                    Wrapper.Packet.fRatio[2] = value;
                    CallAction();
                }
            }
        }

        public float D
        {
            get => Wrapper.Packet.fRatio[3];
            set
            {
                if (Wrapper.Packet.fRatio[3] != value)
                {
                    Wrapper.Packet.fRatio[3] = value;
                    CallAction();
                }
            }
        }
    }
}
