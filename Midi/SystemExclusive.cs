using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidiLib
{
    public class SystemExclusive : Event
    {
        public int Length { get; set; }
        public byte[] DataBytes { get; set; }

        public SystemExclusive(int deltaTime, EventType type, int length, byte[] data)
            : base(deltaTime, type)
        {
            Length = length;
            DataBytes = data;
        }

        public override string ToString()
        {
            var res = string.Format("{0} {1}", base.ToString(), Length);

            foreach (var byt in DataBytes)
            {
                res += " " + byt;
            }

            return res;
        }
    }
}
