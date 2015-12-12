using NoteMapLib.Model.Enums;
using NoteMapLib.Model.Events;
using System;
using System.Collections.Generic;

namespace NoteMapLib.Model
{
	public class Track : List<TrackEvent>
	{
        #region Properties
        public TrackTypes Type { get; set; }
		public TrackDifficulties Difficulty { get; set; }
        #endregion
	}
}