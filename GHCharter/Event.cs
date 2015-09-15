using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartLib
{
    public class Event : ChartEvent
    {
        public override string EventSymbol { get { return "E"; } }
        public string Value { get; set; }

        public Event(int pos, string value) 
            : base(pos)
        {
            Value = value;
        }

        public override string ToString()
        {
            return string.Format("{0} \"{1}\"", base.ToString(), Value);
        }
    }
}
