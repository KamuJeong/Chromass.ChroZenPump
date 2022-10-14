using Chromass.ChroZenPump.Packets;
using ChromassProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chromass.ChroZenPump.APIs;

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
                return Error.Detail();
        }

        return "Unknown";
    }
}
