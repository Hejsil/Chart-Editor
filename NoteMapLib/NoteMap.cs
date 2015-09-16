using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteMapLib
{
    public class NoteMapLib : SortedSet<Track>
	{
		#region Fields
		public SortedSet<MetaEvent> MetaEvents { get; set; }
		#endregion

		#region Constructors
		public NoteMapLib()
		{
			MetaEvents = new SortedSet<MetaEvent>();
        }
		#endregion

		#region Public Methods

		#endregion

		#region Private Methods

		#endregion
	}
}
