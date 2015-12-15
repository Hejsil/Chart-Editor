using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteMapLib.Models.Events
{
    public abstract class Event : IComparable<Event>
    {
        #region Properties
        public int Position { get; set; }
        #endregion
        
        #region IComparable Implementation
        public int CompareTo(Event other)
        {
            return Position - other.Position;
        }
        #endregion
    }
}
