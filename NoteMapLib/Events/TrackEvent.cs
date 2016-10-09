namespace NoteMapLib.Events
{
    public class TrackEvent : Event
    {
        public enum Types
        {
            Note,
            Force,
            Tap,
            Overdrive,

            Unknown
        }

        #region Fields

        public Types Type { get; set; }
        public int Length { get; set; }
        public int Value { get; set; }

        #endregion
    }
}