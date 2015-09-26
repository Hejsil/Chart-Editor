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
		RealDrums,
		Unknown
	}

	public enum TrackDifficulty
	{
		Easy,
		Medium,
		Hard,
		Expert,
		ExpertPlus
	}

	public class Track : List<TrackEvent>, IComparable<Track>
	{
		#region Fields
		public TrackType Type { get; set; }
		public TrackDifficulty Difficulty { get; set; }
		#endregion

		#region Constructors
		public Track(TrackType type = TrackType.Unknown, TrackDifficulty difficulty = TrackDifficulty.Expert)
		{
			Type = type;
			Difficulty = difficulty;
        }
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