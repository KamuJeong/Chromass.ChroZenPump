﻿using Chromass.ChroZenPump.PacketWrappers;
using ChromassProtocol;
using ChromassProtocols.APIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chromass.ChroZenPump.APIs;

public class State : Base<Packets.State>
{
    public State(PacketWrapper<Packets.State> wrapper) : base(wrapper, null) { }

    public State(State src) : base(new StateWrapper { Packet = src.Wrapper.Packet }, null) { }

    public Statuses Status => Wrapper.Packet.btStatus;

    public Errors Error => Wrapper.Packet.uErrorCode;

    public double ElapsedTime => Math.Round(Wrapper.Packet.fElapseTime, 1);
    
    public double Flow => Math.Round(Wrapper.Packet.fFlowSpeed, 3);
    public double A => Math.Round(Wrapper.Packet.fRatio[0], 1);
    public double B => Math.Round(Wrapper.Packet.fRatio[1], 1);
    public double C => Math.Round(Wrapper.Packet.fRatio[2], 1);
    public double D => Math.Round(Wrapper.Packet.fRatio[3], 1);

    public double Pressure => Math.Round(Wrapper.Packet.fPressure, 1);
}
