using Chromass.ChroZenPump.PacketWrappers;
using ChromassProtocol;
using ChromassProtocols.APIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chromass.ChroZenPump.APIs;

public class Event : Base<Packets.Event>
{
    public Event() : this(new EventWrapper())
    {
        
    }

    public Event(EventWrapper wrapper) : base(wrapper, null)
    {
        wrapper.Packet.btCurve = EventCurves.None;
    }

    public Event(Event src) : base(new EventWrapper { Packet = src.Wrapper.Packet }, null) { }

    public double Time
    {
        get => Math.Round(Wrapper.Packet.fTime, 2);
        set
        {
            if (Math.Round(Wrapper.Packet.fTime, 2) != value)
            {
                Wrapper.Packet.fTime = (float)value;
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
            }
        }
    }
}

public class Gradient : Base<Packets.Event>
{
    public Gradient() : this(new EventWrapper())
    {
    
    }

    public Gradient(EventWrapper wrapper) : base(wrapper, null)
    {
        wrapper.Packet.btCurve = EventCurves.Lean;
    }

    public Gradient(Gradient src) : base(new EventWrapper { Packet = src.Wrapper.Packet }, null) { }

    public double Time
    {
        get => Math.Round(Wrapper.Packet.fTime, 2);
        set
        {
            if (Math.Round(Wrapper.Packet.fTime, 2) != value)
            {
                Wrapper.Packet.fTime = (float)value;
            }
        }
    }

    public double Flow
    {
        get => Math.Round(Wrapper.Packet.fFlowSpeed, 3);
        set
        {
            if (Math.Round(Wrapper.Packet.fFlowSpeed, 3) != value)
            {
                Wrapper.Packet.fFlowSpeed = (float)value;
            }
        }
    }

    public int A
    {
        get => (int)Math.Round(Wrapper.Packet.fRatio[0], 0);
        set
        {
            if ((int)Math.Round(Wrapper.Packet.fRatio[0], 0) != value)
            {
                Wrapper.Packet.fRatio[0] = value;
            }
        }
    }

    public int B
    {
        get => (int)Math.Round(Wrapper.Packet.fRatio[1], 0);
        set
        {
            if ((int)Math.Round(Wrapper.Packet.fRatio[1], 0) != value)
            {
                Wrapper.Packet.fRatio[1] = value;
            }
        }
    }

    public int C
    {
        get => (int)Math.Round(Wrapper.Packet.fRatio[2], 0);
        set
        {
            if ((int)Math.Round(Wrapper.Packet.fRatio[2], 0) != value)
            {
                Wrapper.Packet.fRatio[2] = value;
            }
        }
    }

    public float D
    {
        get => (int)Math.Round(Wrapper.Packet.fRatio[3], 0);
        set
        {
            if ((int)Math.Round(Wrapper.Packet.fRatio[3], 0) != value)
            {
                Wrapper.Packet.fRatio[3] = value;
            }
        }
    }
}
