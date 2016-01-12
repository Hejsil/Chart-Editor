using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoteMapLib.Models;
using Sanford.Multimedia.Midi;
using NoteMapLib.Models.Enums;
using MoreLinq;
using NoteMapLib.Models.Events;
using System.Text.RegularExpressions;
using Fractions;

namespace NoteMapLib.Generators
{
    public class FromMidiNoteMapGenerator
    {
        /// <summary>
        /// Generates a Notemap from the midifile found in Path
        /// </summary>
        public NoteMap Generate(string path)
        {
            // Loads the midi
            Sequence midi = new Sequence(path);
            NoteMap result = new NoteMap(midi.Division);

            // Creating a new NoteMap track for each track in the midi.
            foreach (var miditrack in midi)
            {
                Models.Track track = new Models.Track();
                
                List<Tuple<int, IMidiMessage>> onNotes;
                List<Tuple<int, IMidiMessage>> offNotes;
                List<Tuple<int, MetaMessage>> metaEvents;

                // Get all relevant events
                GetMidiEvents(miditrack, out onNotes, out offNotes, out metaEvents);

                // Generate a TrackEvent for each note event
                track.AddRange(GenerateTrackEvents(onNotes, offNotes));

                // Generate a MetaEvent for each meta event and get the track type
                TrackTypes type;
                result.MetaEvents.AddRange(GenerateMetaEvents(metaEvents, out type));
                track.Type = type;
                    
                // Add track to NoteMap if track contains any TrackEvents
                if (track.Count != 0)
                {
                    result.Add(track);
                }
            }

            return result;
        }

        private void GetMidiEvents(Sanford.Multimedia.Midi.Track track, out List<Tuple<int, IMidiMessage>> onNotes, 
                                                                        out List<Tuple<int, IMidiMessage>> offNotes, 
                                                                        out List<Tuple<int, MetaMessage>> metaEvents)
        {
            // Keeps a reference to all relevant events.
            // Also stores their postion relative to the start of the midi.
            onNotes = new List<Tuple<int, IMidiMessage>>();
            offNotes = new List<Tuple<int, IMidiMessage>>();
            metaEvents = new List<Tuple<int, MetaMessage>>();

            // The distance between an event, and the start of the midi.
            int position = 0;

            // Iterate through all events in the track.
            foreach (var @event in track.Iterator())
            {
                position += @event.DeltaTicks;
                IMidiMessage message = @event.MidiMessage;

                switch (message.MessageType)
                {
                    case MessageType.Channel:
                        byte[] bytes = message.GetBytes();

                        // Get the midi channel event type
                        switch (bytes[0] & 0xF0)
                        {
                            // Off note
                            case 0x80:
                                offNotes.Add(new Tuple<int, IMidiMessage>(position, message));
                                break;

                            // On note
                            case 0x90:
                                // If velocity of a Note On event is 0, then its a note off.
                                if (bytes[2] == 0)
                                {
                                    offNotes.Add(new Tuple<int, IMidiMessage>(position, message));
                                }
                                else
                                {
                                    onNotes.Add(new Tuple<int, IMidiMessage>(position, message));
                                }
                                break;
                        }
                        break;

                    case MessageType.Meta:
                        metaEvents.Add(new Tuple<int, MetaMessage>(position, message as MetaMessage));
                        break;
                }
            }
        }


        private List<TrackEvent> GenerateTrackEvents(List<Tuple<int, IMidiMessage>> onNotes, List<Tuple<int, IMidiMessage>> offNotes)
        {
            var result = new List<TrackEvent>();

            // Save all not mapped notes
            var buffer = new List<Tuple<int, IMidiMessage>>();

            // The list used to map midi-notes to NoteMap-notes.
            // It stores two ints. One for the note that should be mapped,
            // And one reprecenting when this note was added to the list.
            var mapping = new List<Tuple<int, int>>(5);
            var age = 0;

            // Iterate all on note events in the midi
            foreach (var on in onNotes)
            {
                // Get the note number in the event.
                int note = on.Item2.GetBytes()[1];

                var map = mapping.FirstOrDefault(x => x.Item2 == note);
                if (map == null)
                {
                    // When the mapping list is full, the note mapping starts.
                    if (mapping.Count == mapping.Capacity)
                    {
                        result.AddRange(MapBuffer(buffer, offNotes, mapping));
                        mapping.Remove(mapping.MinBy(x => x.Item1));
                        buffer.Clear();
                    }

                    mapping.Add(new Tuple<int, int>(age++, note));
                }
                else
                {
                    mapping.Remove(map);
                    mapping.Add(new Tuple<int, int>(age++, map.Item2));
                }

                buffer.Add(on);
            }

            result.AddRange(MapBuffer(buffer, offNotes, mapping));

            return result;
        }

        private IEnumerable<TrackEvent> MapBuffer(List<Tuple<int, IMidiMessage>> buffer, 
                                                  List<Tuple<int, IMidiMessage>> offNotes, 
                                                  List<Tuple<int, int>> mapping)
        {
            // Sort mapping list. This is done because the index of each element is used later.
            mapping.Sort((x1, x2) => x1.Item2.CompareTo(x2.Item2));
            
            foreach (var message in buffer)
            {
                // Length of the note.
                var length = 0;
                var note = message.Item2.GetBytes()[1];

                // Get the closest offnote to the current note, where the distance between them must not be under 0.
                var aboveNotes = offNotes.FindAll(x => x.Item2.GetBytes()[1] == note && x.Item1 - message.Item1 > 0);
                Tuple<int, IMidiMessage> closest = null;

                if (aboveNotes.Count > 0)
                {
                    closest = aboveNotes.MinBy(x => x.Item1);
                }

                // If a offnote was found, set length to the distance between the current note and closest note
                if (closest != null)
                {
                    length = closest.Item1 - message.Item1;
                    offNotes.Remove(closest);
                }
                
                yield return new TrackEvent()
                    {
                        Length = length,
                        Position = message.Item1,
                        Type = TrackEventTypes.Note,
                        Value = mapping.IndexOf(mapping.FirstOrDefault(x => x.Item2 == message.Item2.GetBytes()[1]))
                    };
            }
        }

        private List<MetaEvent> GenerateMetaEvents(List<Tuple<int, MetaMessage>> metaEvents, out TrackTypes type)
        {
            List<MetaEvent> result = new List<MetaEvent>();
            type = TrackTypes.Unknown;

            foreach (var @event in metaEvents)
            {
                byte[] bytes = @event.Item2.GetBytes();

                switch (@event.Item2.MetaType)
                {
                    case MetaType.Tempo:
                        result.Add(ConvertToTempo(bytes, @event.Item1));
                        break;

                    case MetaType.TimeSignature:
                        result.Add(new MetaEvent() { Position = @event.Item1, Type = MetaEventTypes.TimeSignature, Data = new Fraction(bytes[0], (int)Math.Pow(2d, bytes[1])) });
                        break;

                    // TrackName and InstrumentName are used to determin the tracktype.
                    case MetaType.TrackName:
                    case MetaType.InstrumentName:
                        if (type == TrackTypes.Unknown)
                            type = ConvertToTrackType(bytes);
                        break;
                }
            }

            return result;
        }

        private MetaEvent ConvertToTempo(byte[] bytes, int position)
        {
            int value = 0;
            int offset = bytes.Length - 1;

            foreach (var item in bytes)
            {
                value += item << offset * 8;
                offset--;
            }

            return new MetaEvent() { Position = position, Type = MetaEventTypes.Tempo, Data = (int)(60000000000 / value) };
        }

        private TrackTypes ConvertToTrackType(byte[] bytes)
        {
            var chararray = new char[bytes.Length];
            bytes.CopyTo(chararray, 0);
            var name = new string(chararray);
            var match = Regex.Match(name, "guitar|bass|drum|key", RegexOptions.IgnoreCase);

            if (match != null)
            {
                switch (match.Value.ToLower())
                {
                    case "guitar":
                        return TrackTypes.Guitar;
                    case "bass":
                        return TrackTypes.Bass;
                    case "drum":
                        return TrackTypes.Drums;
                    case "key":
                        return TrackTypes.Keys;
                }
            }

            return TrackTypes.Unknown;
        }
    }
}
