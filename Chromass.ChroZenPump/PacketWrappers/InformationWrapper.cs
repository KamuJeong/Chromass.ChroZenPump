using Chromass.ChroZenPump.Packets;
using ChromassProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chromass.ChroZenPump.PacketWrappers
{
    public class InformationWrapper : PacketWrapper<Information>
    {
        public const uint PacketCode = 0x90100;
        public override uint Code => PacketCode;

        public InformationWrapper()
        {
            Packet.nVersion = 400;
        }
    }
}
