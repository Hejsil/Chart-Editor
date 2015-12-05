using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteMapModel
{
    public class NoteMap : List<Track>
	{
		#region Fields
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

		#region Public Methods

		#endregion

		#region Private Methods

		#endregion
	}
}
