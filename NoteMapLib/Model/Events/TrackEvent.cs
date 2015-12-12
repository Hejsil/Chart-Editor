using NoteMapLib.Model.Enums;
using System;
using System.Collections.Generic;

namespace NoteMapLib.Model.Events
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