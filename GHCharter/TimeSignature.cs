using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartLib
{
    public class TimeSignature : ChartEvent
    {
        public override string EventSymbol { get { return "TS"; } }
        public int Value { get; set; }

        public TimeSignature(int pos, int value)
            : base(pos)
        {
            Value = value;
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", base.ToString(), Value);
        }
    }
}
