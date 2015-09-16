using System;
using System.Collections.Generic;

namespace NoteMapLib
{
	public enum TrackEventType
	{
		NormalNote,
		ForcedNote,
		Chord,
		ForceChord,
		TapNote,
		Overdrive,
	}

	public class TrackEvent : IComparable<TrackEvent>, IEvent
	{
		#region Fields
		public int Posision { get; set; }
		public int Length { get; set; }
		public TrackEventType Type { get; private set; }

		public object Data
		{
			get
			{
				return data;
			}
			set
			{
				switch (Type)
				{
					case TrackEventType.NormalNote:
					case TrackEventType.ForcedNote:
					case TrackEventType.TapNote:
						if (!(value is int))
							throw new ArgumentException("Expected int, but was " + value.GetType());
						break;
					case TrackEventType.Chord:
					case TrackEventType.ForceChord:
						if (!(value is List<int>))
							throw new ArgumentException("Expected List<int>, but was " + value.GetType());
						break;
					default:
						break;
				}

				data = value;
			}
		}
		object data;
		#endregion

		#region Constructors
		public TrackEvent()
		{

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