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
    public class State : Base<Packets.State>
    {
        public State(PacketWrapper<Packets.State> wrapper) : base(wrapper, null) { }

        public State(State src) : base(new StateWrapper { Packet = src.Wrapper.Packet }, null) { }

        public Statuses Status => Wrapper.Packet.btStatus;

        public Errors Error => Wrapper.Packet.uErrorCode;

        public float ElapsedTime => Wrapper.Packet.fElapseTime;
        
        public float Flow => Wrapper.Packet.fFlowSpeed;
        public float A => Wrapper.Packet.fRatio[0];
        public float B => Wrapper.Packet.fRatio[1];
        public float C => Wrapper.Packet.fRatio[2];
        public float D => Wrapper.Packet.fRatio[3];

        public float Pressure => Wrapper.Packet.fPressure;
    }
}
