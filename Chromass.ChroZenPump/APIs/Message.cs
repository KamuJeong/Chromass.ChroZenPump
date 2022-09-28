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

        public MessageTypes Type => packet.btMessage;
        public short OldValue => packet.sOldValue;
        public short NewValue => packet.sNewValue;
        public Errors Error => packet.uErrorCode;

        public override string ToString()
        {
            switch(Type)
            {
                case MessageTypes.State:
                    return $"Change to {(Statuses)NewValue}";

                case MessageTypes.ExtIn:
                    return $"{(ExtInMessageValues)NewValue} event in"; 

                case MessageTypes.Error:
                    switch(Error)
                    {
                        case Errors.Config:         return "Configuration fail";
                        case Errors.Setup:          return "Setup fail";
                        case Errors.Service:        return "Calibration fail";
                        case Errors.HighPressure:   return "High pressure limit error";
                        case Errors.LowPressure:    return "Low pressure limit error";
                        case Errors.HighFlow:       return "High flow limit error";
                        case Errors.LowFlow:        return "Low flow limit error";
                        case Errors.Leakage:        return "Leakage error";
                        case Errors.Degassor:       return "Degassor error";
                        case Errors.Motor:          return "Motor error";
                        case Errors.Bubble:         return "Bubble warning";
                    }
                    return "Unknown error";
            }

            return "Unknown";
        }
    }
}
