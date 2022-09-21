using Chromass.ChroZenPump.PacketWrappers;
using ChromassProtocol;
using ChromassProtocols.APIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chromass.ChroZenPump.APIs
{
    public class Setup : Base<Packets.Setup>
    {
        public IList<Event> Events { get; init; }
        public IList<Gradient> Gradients { get; init; }

        public Setup(SetupWrapper wrapper, IEnumerable<EventWrapper> events, Action? action) : base(wrapper, action)
        {
            Events = new List<Event>(events.Skip(100).Select(e => new Event(e, action)));
            Gradients = new List<Gradient>(events.Take(100).Select(e => new Gradient(e, action)));
        }

        public Setup(Setup src) : base(new SetupWrapper { Packet = src.Wrapper.Packet }, null) 
        {
            Events = new List<Event>(src.Events.Select(e => new Event(e)));
            Gradients = new List<Gradient>(src.Gradients.Take(100).Select(e => new Gradient(e)));
        }

        public override void Assign(Base<Packets.Setup> src)
        {
            base.Assign(src);

            if(src is Setup setup)
            {
                for (int i = 0; i < setup.GradientCount; i++)
                    Gradients[i].Assign(setup.Gradients[i]);

                for (int i = 0; i < setup.EventCount; i++)
                    Events[i].Assign(setup.Events[i]);
            }
        }

        public int GradientCount
        {
            get => Wrapper.Packet.nGradientCount;
            set
            {
                if (Wrapper.Packet.nGradientCount != value)
                {
                    Wrapper.Packet.nGradientCount = (ushort)value;
                    CallAction();
                }
            }
        }

        public int EventCount
        {
            get => Wrapper.Packet.nEventCount;
            set
            {
                if (Wrapper.Packet.nEventCount != value)
                {
                    Wrapper.Packet.nEventCount = (ushort)value;
                    CallAction();
                }
            }
        }

        public float MaxPressureLimit
        {
            get => Wrapper.Packet.fMaxPressure;
            set
            {
                if (Wrapper.Packet.fMaxPressure != value)
                {
                    Wrapper.Packet.fMaxPressure = value;
                    CallAction();
                }
            }
        }

        public float MinPressureLimit
        {
            get => Wrapper.Packet.fMinPressure;
            set
            {
                if (Wrapper.Packet.fMinPressure != value)
                {
                    Wrapper.Packet.fMinPressure = value;
                    CallAction();
                }
            }
        }

        public float Flow
        {
            get => Wrapper.Packet.InitEvent.fFlowSpeed;
            set
            {
                if (Wrapper.Packet.InitEvent.fFlowSpeed != value)
                {
                    Wrapper.Packet.InitEvent.fFlowSpeed = value;
                    CallAction();
                }
            }
        }

        public float A
        {
            get => Wrapper.Packet.InitEvent.fRatio[0];
            set
            {
                if (Wrapper.Packet.InitEvent.fRatio[0] != value)
                {
                    Wrapper.Packet.InitEvent.fRatio[0] = value;
                    CallAction();
                }
            }
        }

        public float B
        {
            get => Wrapper.Packet.InitEvent.fRatio[1];
            set
            {
                if (Wrapper.Packet.InitEvent.fRatio[1] != value)
                {
                    Wrapper.Packet.InitEvent.fRatio[1] = value;
                    CallAction();
                }
            }
        }

        public float C
        {
            get => Wrapper.Packet.InitEvent.fRatio[2];
            set
            {
                if (Wrapper.Packet.InitEvent.fRatio[2] != value)
                {
                    Wrapper.Packet.InitEvent.fRatio[2] = value;
                    CallAction();
                }
            }
        }

        public float D
        {
            get => Wrapper.Packet.InitEvent.fRatio[3];
            set
            {
                if (Wrapper.Packet.InitEvent.fRatio[3] != value)
                {
                    Wrapper.Packet.InitEvent.fRatio[3] = value;
                    CallAction();
                }
            }
        }

        public SwitchOutputs Switch1
        {
            get => Wrapper.Packet.btDefaultSwitch1;
            set
            {
                Wrapper.Packet.InitEvent.btSwitch1 = value;
                if (value != SwitchOutputs.Pulse)
                    Wrapper.Packet.btDefaultSwitch1 = value;

                CallAction();
            }
        }

        public SwitchOutputs Swicth2
        {
            get => Wrapper.Packet.btDefaultSwitch2;
            set
            {
                Wrapper.Packet.InitEvent.btSwitch2 = value;
                if (value != SwitchOutputs.Pulse)
                    Wrapper.Packet.btDefaultSwitch2 = value;

                CallAction();
            }
        }

        public MarkOutputs MarkOut
        {
            get => Wrapper.Packet.btDefaultMarkOut;
            set
            {
                Wrapper.Packet.InitEvent.btMarkOut = value;
                if (value != MarkOutputs.Pulse)
                    Wrapper.Packet.btDefaultMarkOut = value;

                CallAction();
            }
        }

        public StartInputs StartInput
        {
            get => Wrapper.Packet.btStartExtIn;
            set
            {
                if (Wrapper.Packet.btStartExtIn != value)
                {
                    Wrapper.Packet.btStartExtIn = value;
                    CallAction();
                }
            }
        }

        public ReadyInputs ReadyInput
        {
            get => Wrapper.Packet.btReadyExtIn;
            set
            {
                if (Wrapper.Packet.btReadyExtIn != value)
                {
                    Wrapper.Packet.btReadyExtIn = value;
                    CallAction();
                }
            }
        }

        public ExternalOutputs StartOutput
        {
            get => Wrapper.Packet.btStartExtOut;
            set
            {
                if (Wrapper.Packet.btStartExtOut != value)
                {
                    Wrapper.Packet.btStartExtOut = value;
                    CallAction();
                }
            }
        }

        public ExternalOutputs ReadyOutput
        {
            get => Wrapper.Packet.btReadyExtOut;
            set
            {
                if (Wrapper.Packet.btReadyExtOut != value)
                {
                    Wrapper.Packet.btReadyExtOut = value;
                    CallAction();
                }
            }
        }

        public uint PulseWidth
        {
            get => Wrapper.Packet.nExtoutTime;
            set
            {
                if(Wrapper.Packet.nExtoutTime != value)
                {
                    Wrapper.Packet.nExtoutTime = value;
                    CallAction();
                }
            }
        }

    }
}
