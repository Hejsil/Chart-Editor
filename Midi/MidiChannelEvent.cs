using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidiLib
{
    public class MidiChannelEvent : Event
    {
        public byte MidiChannel { get; set; }
        public byte Value1 { get; set; }
        public byte Value2 { get; set; }

        public MidiChannelEvent(int deltaTime, EventType type, byte channel, byte value1, byte value2 = 0)
            : base(deltaTime, type)
        {
            MidiChannel = channel;
            Value1 = value1;
            Value2 = value2;
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3}", base.ToString(), MidiChannel, Value1, Value2);
        }
    }
}
