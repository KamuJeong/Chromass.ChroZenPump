using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Chromass.ChroZenPump.Packets
{
    [StructLayout(LayoutKind.Sequential)]
	public struct SelfMessage
    {
		public PumpMessageTypes btMessage;    // 1: State, 2: ExtIn, 3:ExtOut, 4: Error, 

		public short sOldValue;
		public short sNewValue;

		public PumpErrors uErrorCode;
	}
}
