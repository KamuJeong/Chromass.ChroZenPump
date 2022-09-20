﻿using Chromass.ChroZenPump.Packets;
using ChromassProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chromass.ChroZenPump.PacketWrappers
{
    public class SetupWarpper : PacketWrapper<Setup>
    {
        public const uint PacketCode = 0x90102;
        public override uint Code => PacketCode;
    }
}
