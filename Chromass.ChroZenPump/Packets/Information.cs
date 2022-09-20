using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Chromass.ChroZenPump.Packets
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct Information
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string Model;
        public int nVersion;
        public Diagnosis Result;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string Serial;

        public bool dhcp;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] ip;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] netmask;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] gateway;
    }
}
