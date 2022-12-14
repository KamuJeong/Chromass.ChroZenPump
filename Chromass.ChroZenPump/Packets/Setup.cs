using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Chromass.ChroZenPump.Packets
{

    [StructLayout(LayoutKind.Sequential)]
	public struct Setup
    {
		public Commands Command;   // 0:None, 1:Set/Init Flow, 2:Set Pressure, 
								// 3:Gradient Start, 4:Stop, 5:Halt Pumping, 
								// 6:Diagnosis Start, 7:Set Pressure 0, 
								// 8:Reset Error, 9:Purge, 10: Service
								// 11: Finish

		public ushort nGradientCount;  // 0 ~ 100
		public ushort nEventCount;     // 0 ~ 100
		public Event InitEvent;
		// 유속 허용 범위 
		public float fMaxFlowSpeed;        // not used
		public float fMinFlowSpeed;        // not used
									// 압력 제어 셋업 
		public float fMaxPressure;         // 압력 상한 
		public float fMinPressure;         // 압력 하한 
		public float fPressure;        // not used 
								// 외부 입력 
		public StartInputs btStartExtIn; // 0: None, 1: Pulse(0>1), 2: Pulse(1>0)
		public ReadyInputs btReadyExtIn; // 0: Level0, 1: Level1
		public ExternalOutputs btStartExtOut;    // 0: close, 1: open
		public ExternalOutputs btReadyExtOut;    // 0: close, 1: open
										// EVENT의 MarkOut Default 값
		public SwitchOutputs btDefaultSwitch1; // 0: OFF, 1: ON
		public SwitchOutputs btDefaultSwitch2; // 0: Off, 1: On
		public MarkOutputs btDefaultMarkOut; // 0: close, 1: open
		public uint nExtoutTime;   // mSec -> Pulse일경우만 해당(100--100000)

		public Diagnosis Diagnosis;
	}
}
