using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidiLib
{
    public class Tempo : MetaEvent
    {
        public byte[] DataBytes { get; set; }

        public Tempo(int deltaTime, int length, byte[] data)
            : base(deltaTime, length)
        {
            DataBytes = data;
        }

        public decimal ToBPM()
        {
            return 60000000M / (decimal)DataBytes.ToInt32();
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", base.ToString(), ToBPM());
        }
    }
}
