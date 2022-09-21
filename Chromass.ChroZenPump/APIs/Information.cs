using Chromass.ChroZenPump.PacketWrappers;
using ChromassProtocols.APIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chromass.ChroZenPump.APIs
{
    public class Information : Base<Packets.Information>
    {
        public Information(InformationWrapper wrapper) : base(wrapper, null) { }
        public Information(Information src) : base(new InformationWrapper { Packet = src.Wrapper.Packet }, null) { }

        public String? Model => Wrapper.Packet.Model;
        public int Version => Wrapper.Packet.nVersion;
        public string? SN => Wrapper.Packet.Serial;
    }
}
