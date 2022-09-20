using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Chromass.ChroZenPump.Packets
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Diagnosis
    {
		public byte bt24VCheck;       // Motor Power
		public byte bt12VCheck;       // OP Amp Power
		public byte btN12VCheck;
		public byte bt3_3VCheck;      // CPU Power
		public byte btD5VCheck;       // VDD
		public byte btA5VCheck;       // Analog +5
		public byte bt2_5VCheck;      // ADC Referecne
		public byte bt1_5VCheck;      // FPGA Core
		public byte btV12VCheck;      // 4-Way Valve Power // not used
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct DiagnosisData
	{
		public uint uDiagnosis;
		public float fValue;
		public byte btPass;
	}
}
