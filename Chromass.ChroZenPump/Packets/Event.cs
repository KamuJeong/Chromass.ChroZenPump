using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Chromass.ChroZenPump.Packets
{
    public enum EventCurves : byte
    {
        Level = 0,
        Lean = 5,
        None = 0xff
    }



    [StructLayout(LayoutKind.Sequential)]
    public struct Event
    {
        public float fTime;            // 실행 시간 [min] 
        public float fFlowSpeed;       // 설정 유속
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] fRatio;
        public EventCurves btCurve;      // 이벤트 타입

        // 외부 출력
        public byte btSwitch1;    // 0: OFF, 1: ON, 2:Pulse 
        public byte btSwitch2;    // 
        public byte btMarkOut;	// 0: close, 1: Open, 2:Pulse
    }
}
