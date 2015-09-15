using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartLib
{
    public enum NoteType
    {
        Green,
        Red,
        Yellow,
        Blue,
        Orange
    }

    public class Note : ChartEvent
    {
        public override string EventSymbol { get { return "N"; } }
        public NoteType Value { get; set; }
        public int Length { get; set; }

        public Note(int pos, NoteType value, int length)
            : base(pos)
        {
            Value = value;
            Length = length;
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}", base.ToString(), (int)Value, Length);
        }
    }
}
