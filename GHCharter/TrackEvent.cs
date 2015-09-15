using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartLib
{
    public class TrackEvent : ChartEvent
    {
        public override string EventSymbol { get { return "E"; } }
        public string Value { get; set; }

        public TrackEvent(int pos, string value)
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
