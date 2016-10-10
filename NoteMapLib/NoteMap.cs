using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Fractions;
using HejsilsUtilities.Collections;
using NoteMapLib.Events;
using NoteMapLib.Events.MetaEvents;
using Sanford.Multimedia.Midi;

namespace NoteMapLib
{
    public class NoteMap
    {
        #region Constructors
        public NoteMap(int division = 16, int offset = 0)
        {
            Division = division;
            Offset = offset;
        }

        #endregion

        #region Generate from Midi

        /// <summary>
        /// Generates a Notemap from the midifile found in Path
        /// </summary>
        public static NoteMap GenerateFromMidi(string path)
        {
            var midi = new Sequence(path);
            var result = new NoteMap(midi.Division);

            foreach (var miditrack in midi)
            {
                var track = new Track(Track.Types.Guitar);
                var position = 0;

                // teporarily stores NoteOn channel messages that havn't been paired up with an NoteOff message.
                // also stores the position of that message relative to the start of the midi file
                var noteOns = new List<Tuple<int, ChannelMessage>>();

                // used to map midi tunes to guitar hero notes.
                var mapping = new AlwaysSortedList<int>(5);

                // used to keep track of the usage of the notes in the mapping list.
                // this is needed when mapping is full, and one of the tunes should be replaced
                var usageQueue = new List<int>(5);

                // keep track of notes that have yet to be fully processed
                var unmappedNotes = new List<TrackEvent>();

                // HACK: this is prettier than making a private none local function.
                // this lambda is needed two different places in this function.
                // it process' an NoteOn into a track event.
                // it needs to be here, so it can capture the variables above
                Action<Tuple<int, ChannelMessage>, int> processNoteOn = (noteOnTuple, pos) =>
                {
                    var noteOn = noteOnTuple.Item2;
                    var noteOnPosition = noteOnTuple.Item1;

                    // we set the value of this track event to be the tune of the midi note.
                    // this will later in the process be converted to the actual value later
                    var trackNote = new TrackEvent
                    {
                        Position = noteOnPosition,
                        Length = pos - noteOnPosition,
                        Type = TrackEvent.Types.Note,
                        Value = noteOn.Data1
                    };

                    var index = mapping.IndexOf(noteOn.Data1);

                    // in mapping
                    if (index != -1)
                    {
                        // move this tune to the end of the usageQueue
                        usageQueue.Remove(noteOn.Data1);
                        usageQueue.Add(noteOn.Data1);
                    }
                    // not in mapping
                    else
                    {
                        if (mapping.Capacity == mapping.Count)
                        {
                            // map all unmapped notes and add them to the track
                            foreach (var note in unmappedNotes)
                            {
                                note.Value = mapping.IndexOf(note.Value);
                                track.Events.Add(note);
                            }

                            unmappedNotes.Clear();

                            // remove the least used tune in mapping
                            mapping.Remove(usageQueue.First());
                            usageQueue.RemoveAt(0);
                        }

                        // add tune to mapping and usage queue
                        mapping.Add(noteOn.Data1);
                        usageQueue.Add(noteOn.Data1);
                    }

                    unmappedNotes.Add(trackNote);
                };

                foreach (var @event in miditrack.Iterator())
                {
                    var message = @event.MidiMessage;
                    position += @event.DeltaTicks;

                    if (message.MessageType == MessageType.Channel)
                    {
                        var cMessage = message as ChannelMessage;

                        // note events data1 is the tune and data2 is velocity
                        // if an NoteOn has velocity is 0, then it's the same as it being a NoteOff
                        if (cMessage.Command == ChannelCommand.NoteOff ||
                           (cMessage.Command == ChannelCommand.NoteOn && cMessage.Data2 == 0))
                        {
                            // find the first NoteOn that has the same tune as the current NoteOff.
                            // these two notes will be a pair, and will be used together to create a Note TrackEvent
                            var noteOnTuple = noteOns.Find(x => x.Item2.Data1 == cMessage.Data1);

                            processNoteOn(noteOnTuple, position);

                            noteOns.Remove(noteOnTuple);
                        }
                        else if (cMessage.Command == ChannelCommand.NoteOn)
                        {
                            noteOns.Add(new Tuple<int, ChannelMessage>(position, cMessage));
                        }
                    }
                    else if (message.MessageType == MessageType.Meta)
                    {
                        var mMessage = message as MetaMessage;
                        var bytes = mMessage.GetBytes();

                        if (mMessage.MetaType == MetaType.Tempo)
                        {
                            var value = 0;
                            var offset = bytes.Length - 1;

                            foreach (var item in bytes)
                            {
                                value += item << (offset*8);
                                offset--;
                            }

                            result.MetaEvents.Add(new TempoEvent
                            {
                                Position = position,
                                Tempo = (int) (60000000000/value)
                            });
                        }
                        else if (mMessage.MetaType == MetaType.TimeSignature)
                        {
                            result.MetaEvents.Add(new TimeSignature
                            {
                                Position = position,
                                Signature = new Fraction(bytes[0], (int) Math.Pow(2d, bytes[1]))
                            });
                        }
                        else if (mMessage.MetaType == MetaType.TimeSignature ||
                                 mMessage.MetaType == MetaType.InstrumentName)
                        {
                            if (track.Type != Track.Types.Unknown)
                                break;

                            var chararray = new char[bytes.Length];
                            bytes.CopyTo(chararray, 0);
                            var name = new string(chararray);
                            var match = Regex.Match(name, "guitar|bass|drum|key", RegexOptions.IgnoreCase);

                            if (!match.Success)
                                break;

                            switch (match.Value.ToLower())
                            {
                                case "guitar":
                                    track.Type = Track.Types.Guitar;
                                    break;
                                case "bass":
                                    track.Type = Track.Types.Bass;
                                    break;
                                case "drum":
                                    track.Type = Track.Types.Drums;
                                    break;
                                case "key":
                                    track.Type = Track.Types.Keys;
                                    break;
                            }
                        }
                    }
                }

                foreach (var noteOn in noteOns)
                {
                    processNoteOn(noteOn, noteOn.Item1);
                }

                foreach (var note in unmappedNotes)
                {
                    note.Value = mapping.IndexOf(note.Value);
                    track.Events.Add(note);
                }

                result.Tracks.Add(track);
            }

            return result;
        }

        #endregion

        #region Properties

        public int Offset { get; set; }
        public int Division { get; }

        public List<Track> Tracks { get; private set; } = new List<Track>();
        public List<MetaEvent> MetaEvents { get; } = new List<MetaEvent>();

        #endregion

        #region Write guitar hero char
        private enum ChartTracks
        {
            SyncTrack = 0x00,
            Events = 0x01,

            ExpertSingle = 0x10,
            ExpertDoubleGuitar = 0x11,
            ExpertDoubleBass = 0x12,
            ExpertEnhancedGuitar = 0x13,
            ExpertCoopLead = 0x14,
            ExpertCoopBass = 0x15,
            Expert10KeyGuitar = 0x16,
            ExpertDrums = 0x17,
            ExpertDoubleDrums = 0x18,
            ExpertVocals = 0x19,
            ExpertKeyboard = 0x1A,

            HardSingle = 0x20,
            HardDoubleGuitar = 0x21,
            HardDoubleBass = 0x22,
            HardEnhancedGuitar = 0x23,
            HardCoopLead = 0x24,
            HardCoopBass = 0x25,
            Hard10KeyGuitar = 0x26,
            HardDrums = 0x27,
            HardDoubleDrums = 0x28,
            HardVocals = 0x29,
            HardKeyboard = 0x2A,

            MediumSingle = 0x30,
            MediumDoubleGuitar = 0x31,
            MediumDoubleBass = 0x32,
            MediumEnhancedGuitar = 0x33,
            MediumCoopLead = 0x34,
            MediumCoopBass = 0x35,
            Medium10KeyGuitar = 0x36,
            MediumDrums = 0x37,
            MediumDoubleDrums = 0x38,
            MediumVocals = 0x39,
            MediumKeyboard = 0x3A,


            EasySingle = 0x40,
            EasyDoubleGuitar = 0x41,
            EasyDoubleBass = 0x42,
            EasyEnhancedGuitar = 0x43,
            EasyCoopLead = 0x44,
            EasyCoopBass = 0x45,
            Easy10KeyGuitar = 0x46,
            EasyDrums = 0x47,
            EasyDoubleDrums = 0x48,
            EasyVocals = 0x49,
            EasyKeyboard = 0x4A,

            DontChart = EasyKeyboard + 1
        }

        public void WriteGuitarHeroChart(string path)
        {
            using (var stream = new StreamWriter(path))
            {
                var tracksCharted = new HashSet<ChartTracks>();

                stream.WriteLine("[Song]");
                stream.WriteLine("{");
                stream.WriteLine("\tOffset = " + Offset);
                stream.WriteLine("\tResolution = " + Division);
                stream.WriteLine("}");

                stream.WriteLine("[SyncTrack]");
                stream.WriteLine("{");

                foreach (var evnt in MetaEvents)
                {
                    if (evnt is TempoEvent)
                    {
                        stream.WriteLine("	{0} = B {1}", evnt.Position, (evnt as TempoEvent).Tempo);
                    }
                    else if (evnt is TimeSignature)
                    {
                        stream.WriteLine("	{0} = TS {1}", evnt.Position, (evnt as TimeSignature).Signature.Numerator);
                    }
                }

                stream.WriteLine("}");

                stream.WriteLine("[Events]");
                stream.WriteLine("{");

                foreach (var evnt in MetaEvents)
                {
                    if (evnt is SectionName)
                    {
                        stream.WriteLine("	{0} = E \"section {1}\"", evnt.Position, (evnt as SectionName).Name);
                    }
                }

                stream.WriteLine("}");

                foreach (var track in Tracks)
                {
                    ChartTracks trackType;

                    switch (track.Type)
                    {
                        case Track.Types.Guitar:
                            trackType = ChartTracks.ExpertSingle;
                            break;

                        case Track.Types.Bass:
                            trackType = ChartTracks.ExpertDoubleBass;
                            break;

                        case Track.Types.GuitarCoop:
                            trackType = ChartTracks.ExpertDoubleGuitar;
                            break;

                        case Track.Types.Rhythm:
                            trackType = ChartTracks.ExpertDoubleGuitar;
                            break;

                        default:
                            trackType = ChartTracks.DontChart;
                            break;
                    }

                    if (trackType != ChartTracks.DontChart)
                        trackType += 0x10 * (int)track.Difficulty;
                    
                    var original = trackType;

                    for (var i = 0; (trackType < ChartTracks.DontChart) && tracksCharted.Contains(trackType) ; i++)
                    {
                        trackType = (ChartTracks) ((int) original + 0x10*i);

                        if (trackType > ChartTracks.DontChart)
                            trackType = ChartTracks.DontChart;
                    }

                    stream.WriteLine("[" + trackType + "]");
                    stream.WriteLine("{");

                    foreach (var evnt in track.Events)
                    {
                        switch (evnt.Type)
                        {
                            case TrackEvent.Types.Note:
                                stream.WriteLine("	{0} = N {1} {2}", evnt.Position, evnt.Value, evnt.Length);
                                break;
                            case TrackEvent.Types.Force:
                            case TrackEvent.Types.Tap:
                                throw new NotImplementedException();
                            case TrackEvent.Types.Overdrive:
                                stream.WriteLine("	{0} = S 2 {1}", evnt.Position, evnt.Length);
                                break;
                        }
                    }

                    stream.WriteLine("}");

                    tracksCharted.Add(trackType);
                }
            }
        }
        #endregion
    }
}