using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidiLib
{
    public class TPB : Division
    {
        public ushort Ticks { get; set; }

        public TPB(ushort ticks)
        {
            Ticks = ticks;
        }

        public override string ToString()
        {
            return string.Format("{0}", Ticks);
        }
    }
}
