using NoteMapLib.Models.Enums;
using System;
using System.Collections.Generic;

namespace NoteMapLib.Models.Events
{
	public class TrackEvent : Event
	{
        #region Fields
        public int Length { get; set; }
        public TrackEventTypes Type { get; set; }
        public int Value { get; set; }
        #endregion
	}
}