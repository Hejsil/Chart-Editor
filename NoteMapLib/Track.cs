using System;
using System.Collections.Generic;

namespace NoteMapLib
{
	public enum TrackType
	{
		Guitar,
		Bass,
		GuitarCoop,
		Rhythm,
		Drums,
		Vocals,
		Keys,
		RealBass,
		RealGuitar,
		Dance,
		RealBass2,
		RealGuitar2,
		RealDrums
	}

	public class Track : SortedSet<TrackEvent>, IComparable<Track>
	{
		#region Fields
		public TrackType Type { get; private set; }
		#endregion

		#region Constructors

		#endregion

		#region Public Methods

		public int CompareTo(Track other)
		{
			return Type - other.Type;
		}
		#endregion

		#region Private Methods

		#endregion
	}
}