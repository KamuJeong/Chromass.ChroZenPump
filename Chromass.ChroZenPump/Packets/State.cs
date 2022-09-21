using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Chromass.ChroZenPump.Packets
{

    [StructLayout(LayoutKind.Sequential)]
    public struct State
    {
        public PumpStatus btStatus;  // 0: Initializing, 1: Ready, 2: Gradient, 
                                    // 3: Pressure, 4: Diagnosis, 5: Halt, 
                                    // 6: Error, 7: Purge, 8: Service, 9: Finish
        public PumpErrors uErrorCode;
        public int nEventStep;    // 현재 실행 중 인 Step 번호 
        public float fElapseTime;      // Running Time[min] 
        public float fFlowSpeed;       // 현재유속 
        public float fPressure;        // 현재압력

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] fRatio;            // 용리비

        public byte btReadyExtIn; // 0: not ready, 1: ready

        public byte left_stroke;  // 각 헤드별 stroke 강도 (0 ~ 192)
        public byte right_stroke;
    }
}
