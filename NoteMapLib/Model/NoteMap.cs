using NoteMapLib.Model.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteMapLib.Model
{
    public class NoteMap : List<Track>
	{
        #region Properties
        public int Offset { get; set; }
        public int Division { get; private set; }

        public List<MetaEvent> MetaEvents { get; private set; }
        #endregion

        #region Constructors
        public NoteMap(int division = 64, int offset = 0)
		{
			Division = division;
			Offset = offset;
            MetaEvents = new List<MetaEvent>();
        }
		#endregion
	}
}
