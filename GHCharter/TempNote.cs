using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartLib
{
    public class TempNote : IComparable<TempNote>
    {
        public int Position { get; set; }
        public int Length { get; set; }
        public byte Note { get; set; }
        public List<int> MappingList { get; set; }

        public TempNote(int pos, int length, byte note, List<int> mapping)
        {
            Position = pos;
            Length = length;
            Note = note;
            MappingList = mapping;
        }

        public Note ToNote()
        {
            return new Note(Position, (NoteType)MappingList.IndexOf(Note), Length);
        }

        public int CompareTo(TempNote other)
        {
            return Position - other.Position;
        }
    }
}
