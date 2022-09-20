using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Chromass.ChroZenPump.Packets
{
    public struct Calibration
    {
        public int Count;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public float[] SetFlow;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public float[] ActFlow;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public float[] Pressure;
    }
}
