using Chromass.ChroZenPump.Packets;
using ChromassProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chromass.ChroZenPump.APIs
{
    public class Message
    {
        private Packets.SelfMessage packet;
        public Message(PacketWrapper<SelfMessage> wrapper) 
        {
            packet = wrapper.Packet;
        }

        public PumpMessageTypes Type => packet.btMessage;
        public short OldValue => packet.sOldValue;
        public short NewValue => packet.sNewValue;
        public PumpErrors Error => packet.uErrorCode;

        public override string ToString()
        {
            switch(Type)
            {
                case PumpMessageTypes.State:
                    return $"Change to {(PumpStatus)NewValue}";

                case PumpMessageTypes.ExtIn:
                    return $"{(PumpExtInMessageValues)NewValue} event in"; 

                case PumpMessageTypes.Error:
                    switch(Error)
                    {
                        case PumpErrors.Config:         return "Configuration fail";
                        case PumpErrors.Setup:          return "Setup fail";
                        case PumpErrors.Service:        return "Calibration fail";
                        case PumpErrors.HighPressure:   return "High pressure limit error";
                        case PumpErrors.LowPressure:    return "Low pressure limit error";
                        case PumpErrors.HighFlow:       return "High flow limit error";
                        case PumpErrors.LowFlow:        return "Low flow limit error";
                        case PumpErrors.Leakage:        return "Leakage error";
                        case PumpErrors.Degassor:       return "Degassor error";
                        case PumpErrors.Motor:          return "Motor error";
                        case PumpErrors.Bubble:         return "Bubble warning";
                    }
                    return "Unknown error";
            }

            return "Unknown";
        }
    }
}
