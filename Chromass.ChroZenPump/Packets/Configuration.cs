using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Chromass.ChroZenPump.Packets;

[StructLayout(LayoutKind.Sequential)]
public struct Configuration
{
    public Modes btMode;       // 0: Quaternary, 1: Binary, 2:Isocratic 
                                   // read only

    public float fMaxFlow;
    public float fMaxPressure;
    public float fHeadVolume;      //  read only
    public float fEffectiveHeadVolume;     // not used
    public byte btRinsePumpOnOff;
    public byte btBuzzerEnable;
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

    public byte btDegassorOnOff;
}
