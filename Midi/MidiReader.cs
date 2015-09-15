using System;
using System.Collections.Generic;
using System.IO;

namespace MidiLib
{
    public enum FormatType
    {
        SingleTrack,
        MultiTrack,
        IndepentedMultiTrack
    }

    /// <summary>
    /// A class for reading Midifiles
    /// </summary>
    public class MidiReader
    {
        #region Public Properties
        /// <summary>
        /// Name of the file (without extention)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The datachunks the midifile contains
        /// </summary>
        public List<TrackChunk> Chunks { get; set; }

        /// <summary>
        /// The midifiles format. Can be either SingleTrack, MultiTrack or IndepentedMultiTrack.
        /// </summary>
        public FormatType Format { get; set; }

        /// <summary>
        /// Number of tracks the midifiles contains. This should be Chunks.Count - 1. 
        /// </summary>
        public ushort Tracks { get; set; }

        /// <summary>
        /// The midifiles division. The way in which time is described in the midifile.
        /// </summary>
        public Division Division { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Create a empty MidiReader.
        /// </summary>
        public MidiReader()
        {
            Chunks = new List<TrackChunk>();
        }

        /// <summary>
        /// Create a MidiReader, and load information from a file.
        /// </summary>
        /// <param name="path">Path to a midifile.</param>
        public MidiReader(string path)
        {
            ReadMidi(path);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Clears all data in the MidiReader, and loads information from a file.
        /// </summary>
        /// <param name="path">Path to a midifile.</param>
        public void ReadMidi(string path)
        {
            //Clearing current data in MidiReader
            Name = Path.GetFileNameWithoutExtension(path);
            Chunks.Clear();
            Format = default(FormatType);
            Tracks = default(ushort);
            Division = default(Division);

            //Opens file on path
            using (var file = File.Open(path, FileMode.Open))
            using (var reader = new BinaryReader(file))
            {
                bool headChunkFound = false;
                int chunks = 1;
                int chunkSize;
                string chunkID;

                //Runs until there is no more data chunks in the midi files
                while (chunks != 0)
                {
                    //chunkID and and chunkSize is allways contained in the first 8 bytes of a data chunk
                    chunkID = new string(reader.ReadChars(4));
                    chunkSize = reader.ReadBytes(4).ToInt32();

                    switch (chunkID)
                    {
                        /* This is the header chunk id.
                         * This chunk should only appear once and contains relevant global data.
                         * The first 2 bytes contain the midi format.
                         * The next 2 bytes contain the number of tracks the midifile contains.
                         * The final 2 bytes contain the midifiles division.
                         */
                        case "MThd":
                            if (headChunkFound)
                                throw new FileLoadException(string.Format("More than one header chunk was found in the midifile"));

                            Format = (FormatType)reader.ReadBytes(2).ToInt32();
                            Tracks = (ushort)reader.ReadBytes(2).ToInt32();
                            Division = GetDivision(reader.ReadBytes(2));
                            chunks = Tracks;
                            headChunkFound = true;
                            break;
                        //This is the track chunk id. A track chunk contains midi events.
                        case "MTrk":
                            chunks--;
                            Chunks.Add(new TrackChunk(chunkSize, GetEvents(reader, chunkSize)));
                            break;
                        //Standard midi files only have 2 kinds of chunks. If any other chunk id is found, an exception will the thrown.
                        default:
                            throw new FileLoadException(string.Format("Chunck Id: \"{1}\" is not valid for midi files."));
                    }
                }
            }
        }

        /// <summary>
        /// This method is used for debugging the MidiReader.
        /// I write all the data from the MidiReader into a textfile, in a "readable" format.
        /// </summary>
        /// <param name="text">Is used to write important information at the end of the file</param>
        public void WriteToFile(string text = "")
        {
            using (var str = new StreamWriter(@"C:\Users\Hejsil\Downloads\text.txt"))
            {
                str.WriteLine(string.Format("Format: {0}", Format));
                str.WriteLine(string.Format("Tracks: {0}", Tracks));
                str.WriteLine(string.Format("Division: {0}", Division));
                str.WriteLine();

                foreach (var chunk in Chunks)
                    chunk.WriteToFile(str);

                str.WriteLine(text);
            }
        }

        public override string ToString()
        {
            var res = string.Format("Format: {0}\nTracks: {1}\nDivision: {2}\n\n", Format, Tracks, Division);

            foreach (var chunck in Chunks)
                res += chunck.ToString() + "\n";

            return res;
        }
        #endregion

        #region Private Methods
        private Division GetDivision(byte[] bytes)
        {
            //If the first bit is 1, then the division describes Frames Per Second. Otherwise it is Ticks Per Beat.
            if (bytes[0] >= 0x80)
            {
                bytes[0] -= 0x80;
                return new FPS(bytes[0], bytes[1]);
            }

            return new TPB((ushort)bytes.ToInt32());
        }

        private List<Event> GetEvents(BinaryReader reader, int chunkSize)
        {
            var res = new List<Event>();
            var stop = reader.BaseStream.Position + chunkSize;
            Event prev = null;

            while (!(((prev is MetaEvent) && (prev as MetaEvent).MetaType == MetaType.EndOfTrack) || (stop <= reader.BaseStream.Position)))
            {
                var deltaTime = GetVariableLength(reader);
                var byt = reader.ReadByte();

                //Meta Event
                if (byt == 0xFF)
                    res.Add(prev = GetMetaEvent(deltaTime, reader, byt));
                //System Exclusive Event
                else if (byt >= 0xF0)
                    res.Add(prev = GetSystemExclusiveEvent(deltaTime, reader, byt));
                //Midi Channel Event
                else if (byt >= 0x80)
                    res.Add(prev = GetMidiChannelEvent(deltaTime, reader, byt));
                else
                    res.Add(prev = GetMidiChannelEvent(deltaTime, reader, byt, false, prev as MidiChannelEvent));
            }

            return res;
        }

        private int GetVariableLength(BinaryReader reader)
        {
            var reslist = new List<byte>();
            var res = 0;

            do
                reslist.Add(reader.ReadByte());
            while (reslist[reslist.Count - 1] >= 0x80);

            for (int i = 0, j = reslist.Count - 1; i < reslist.Count - 1; i++, j--)
                res += (reslist[i] - 0x80) << j * 7;

            return res + reslist[reslist.Count - 1];
        }

        private Event GetSystemExclusiveEvent(int deltaTime, BinaryReader reader, byte byt)
        {
            var type = (EventType)byt;

            switch (type)
            {
                case EventType.SystemExclusive:
                    int length = GetVariableLength(reader);
                    return new SystemExclusive(deltaTime, type, length, reader.ReadBytes(length));
                case EventType.MIDITimeCodeQuarterFrame:
                case EventType.SongPositionPointer:
                case EventType.SongSelect:
                case EventType.TuneRequest:
                case EventType.EndofExclusive:
                case EventType.TimingClock:
                case EventType.Start:
                case EventType.Continue:
                case EventType.Stop:
                case EventType.ActiveSensing:
                    return new SystemExclusive(deltaTime, type, 0, reader.ReadBytes(0));
                default:
                    throw new NotValidEventException(type.ToString());
            }
        }

        private Event GetMidiChannelEvent(int deltaTime, BinaryReader reader, byte byt, bool typeKnown = true, MidiChannelEvent prev = null)
        {
            EventType type;
            byte channel;
            byte value1;

            if (typeKnown)
            {
                type = (EventType)(byt & 0xF0);
                channel = (byte)(byt & 0x0F);
                value1 = reader.ReadByte();
            }
            else
            {
                type = prev.Type;
                channel = prev.MidiChannel;
                value1 = byt;
            }

            switch (type)
            {
                case EventType.NoteOff:
                case EventType.NoteOn:
                case EventType.ControlChange:
                case EventType.PitchBend:
                case EventType.Aftertouch:
                    return new MidiChannelEvent(deltaTime, type, channel, value1, reader.ReadByte());
                case EventType.ProgramChange:
                case EventType.ChannelPressure:
                    return new MidiChannelEvent(deltaTime, type, channel, value1, 0);
                default:
                    throw new NotValidEventException(string.Format("DeltaTime: {4} Type: {0} Channel: {1} Value1: {2} Value2: {3}", type, channel, value1, reader.ReadByte(), deltaTime));
            }
        }

        private Event GetMetaEvent(int deltaTime, BinaryReader reader, byte byt)
        {
            var type = (EventType)byt;
            var metaType = (MetaType)reader.ReadByte();
            var length = GetVariableLength(reader);

            switch (metaType)
            {
                case MetaType.SequenceNumber:
                case MetaType.Text:
                case MetaType.Copyright:
                case MetaType.TrackName:
                case MetaType.InstrumentName:
                case MetaType.Lyric:
                case MetaType.Marker:
                case MetaType.CuePoint:
                case MetaType.ProgramName:
                case MetaType.DeviceName:
                case MetaType.MidiChannelPrefix:
                case MetaType.MidiPort:
                case MetaType.EndOfTrack:
                case MetaType.Tempo:
                case MetaType.SMPTEOffset:
                case MetaType.TimeSignature:
                case MetaType.KeySignature:
                case MetaType.SequencerSpecificEvent:
                    return new MetaEvent(deltaTime, type, metaType, length, reader.ReadBytes(length));
                default:
                    throw new NotValidEventException(metaType.ToString());
            }
        }
        #endregion
    }
}
