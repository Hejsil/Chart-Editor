using System.Collections.Generic;
using NoteMapLib.Events;

namespace NoteMapLib
{
    public class Track
    {
        public enum Difficulties
        {
            ExpertPlus,
            Expert,
            Hard,
            Medium,
            Easy
        }

        public enum Types
        {
            Unknown,
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

        public Track(Types type = Types.Unknown, Difficulties diff = Difficulties.ExpertPlus)
        {
            Type = type;
            Difficulty = diff;
        }

        #region Properties

        public Types Type { get; set; }
        public Difficulties Difficulty { get; set; }
        public List<TrackEvent> Events { get; private set; } = new List<TrackEvent>();

        #endregion
    }
}