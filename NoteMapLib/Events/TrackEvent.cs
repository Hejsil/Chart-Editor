using System;
using System.Collections.Generic;

namespace NoteMapLib
{
	public enum TrackEventType
	{
		Green,
		Red,
		Yellow,
		Blue,
		Orange,

        Force,
		Tap,
		Overdrive,
	}

	public class TrackEvent : IComparable<TrackEvent>
	{
		#region Fields
		public int Posision { get; set; }
		public int Length { get; set; }
		public TrackEventType Type { get; private set; }
		#endregion

		#region Constructors
		public TrackEvent(int pos, TrackEventType type, int length = 0)
		{
			Posision = pos;
			Type = type;
			Length = length;
		}
		#endregion

		#region Public Methods

		public int CompareTo(TrackEvent other)
		{
			return Posision - other.Posision;
		}
		#endregion

		#region Private Methods

		#endregion
	}
}