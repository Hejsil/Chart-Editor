using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartLib
{
    public abstract class ChartEvent : IComparable<ChartEvent>
    {
        public abstract string EventSymbol { get; }
        public int Position { get; set; }

        public ChartEvent(int pos)
        {
            Position = pos;
        }

        public override string ToString()
        {
            return string.Format("\t{0} = {1}", Position, EventSymbol);
        }

        public int CompareTo(ChartEvent other)
        {
            return Position - other.Position;
        }
    }
}
