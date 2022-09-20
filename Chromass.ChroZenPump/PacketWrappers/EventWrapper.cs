using Chromass.ChroZenPump.Packets;
using ChromassProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chromass.ChroZenPump.PacketWrappers
{
    public class EventWrapper : PacketWrapper<Event>
    {
        public const uint PacketCode = 0x90103;
        public override uint Code => PacketCode;
    }
}
