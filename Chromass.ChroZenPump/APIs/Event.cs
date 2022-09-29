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

    public float Time
    {
        get => Wrapper.Packet.fTime;
        set
        {
            if (Wrapper.Packet.fTime != value)
            {
                Wrapper.Packet.fTime = value;
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

    public float Time
    {
        get => Wrapper.Packet.fTime;
        set
        {
            if (Wrapper.Packet.fTime != value)
            {
                Wrapper.Packet.fTime = value;
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
            }
        }
    }
}
