using NoteMapLib.Models.Enums;
using NoteMapLib.Models.Events;
using System;
using System.Collections.Generic;

namespace NoteMapLib.Models
{
	public class Track : List<TrackEvent>
	{
        #region Properties
        public TrackTypes Type { get; set; }
		public TrackDifficulties Difficulty { get; set; }
        #endregion

        public Track(TrackTypes type = TrackTypes.Unknown, TrackDifficulties diff = TrackDifficulties.ExpertPlus)
        {
            Type = type;
            Difficulty = diff;
        }
    }
}