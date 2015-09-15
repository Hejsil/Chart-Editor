using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidiLib
{
    public class FPS : Division
    {
        public byte Frames { get; set; }
        public byte Ticks { get; set; }

        public FPS(byte frames, byte ticks)
        {
            Frames = frames;
            Ticks = ticks;
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", Frames, Ticks);
        }
    }
}
