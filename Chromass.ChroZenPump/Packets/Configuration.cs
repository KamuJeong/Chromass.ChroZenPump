﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Chromass.ChroZenPump.Packets
{
    public enum PumpModes : byte
    {
        Quarternary = 0,
        Binary,
        Isocratic,
        // for Multi-Link
        Elute0 = 0x00,     // %A
        Elute1 = 0x10,     // %B
        Elute2 = 0x20,     // %C
        Elute3 = 0x30,     // %D
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Configuration
    {
        public PumpModes btMode;       // 0: Quaternary, 1: Binary, 2:Isocratic 
                                    // read only

        public float fMaxFlow;
        public float fMaxPressure;
        public float fHeadVolume;      //  read only
        public float fEffectiveHeadVolume;     // not used
        public char btRinsePumpOnOff;
        public char btLeakCheck;
        public char btDegassorOnOff;
        public float fZeroBalanceFinal;

        public float fFlowCalOffset1;              // 유속 보정 옵셋
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public float[] fFlowCalibrationFactor1;    // read only
        public float fCorrelationFactor1;           // read only
        public float fZeroBalance1;

        // for binary pump
        public float fFlowCalOffset2;               // 유속 보정 옵셋
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public float[] fFlowCalibrationFactor2;    // read only
        public float fCorrelationFactor2;           // read only
        public float fZeroBalance2;
    }
}
