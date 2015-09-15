using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartLib
{
    public class SectionName : ChartEvent
    {
        public override string EventSymbol { get { return "E"; } }
        public string Value { get; set; }

        public SectionName(int pos, string value)
            : base(pos)
        {
            Value = value;
        }

        public override string ToString()
        {
            return string.Format("{0} \"section {1}\"", base.ToString(), Value);
        }
    }
}
