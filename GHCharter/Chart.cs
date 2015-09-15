using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MidiLib;

namespace ChartLib
{
    public enum ChartTrack
    {
        SyncTrack,
        Events,

        ExpertSingle,
        ExpertDoubleGuitar,
        ExpertDoubleBass,
        ExpertEnhancedGuitar,
        ExpertCoopLead,
        ExpertCoopBass,
        Expert10KeyGuitar,
        ExpertDrums,
        ExpertDoubleDrums,
        ExpertVocals,
        ExpertKeyboard,

        HardSingle,
        HardDoubleGuitar,
        HardDoubleBass,
        HardEnhancedGuitar,
        HardCoopLead,
        HardCoopBass,
        Hard10KeyGuitar,
        HardDrums,
        HardDoubleDrums,
        HardVocals,
        HardKeyboard,

        MediumSingle,
        MediumDoubleGuitar,
        MediumDoubleBass,
        MediumEnhancedGuitar,
        MediumCoopLead,
        MediumCoopBass,
        Medium10KeyGuitar,
        MediumDrums,
        MediumDoubleDrums,
        MediumVocals,
        MediumKeyboard,

        EasySingle,
        EasyDoubleGuitar,
        EasyDoubleBass,
        EasyEnhancedGuitar,
        EasyCoopLead,
        EasyCoopBass,
        Easy10KeyGuitar,
        EasyDrums,
        EasyDoubleDrums,
        EasyVocals,
        EasyKeyboard
    }

    public class Chart
    {
        public string Name { get; set; }
        public List<List<ChartEvent>> Tracks { get; set; }
        public Int64 Resolution { get; set; }
        public Int64 Offset { get; set; }

        public Chart(string name = "Untilted", Int64 resolution = 0, Int64 offset = 0)
        {
            Name = name;
            Resolution = resolution;
            Offset = offset;
            Tracks = new List<List<ChartEvent>>();
        }

        public Chart(string path) : this(new MidiReader(path)) { }
        public Chart(MidiReader midi) : this(midi, midi.Name) { }
        public Chart(MidiReader midi, string name)
        {
            Name = name;
            Tracks = new List<List<ChartEvent>>();
            Generate(midi);
        }

        private void Generate(MidiReader midi)
        {
            Tracks.Clear();

            if (midi.Division is TPB)
                Resolution = (midi.Division as TPB).Ticks;
            else
                throw new NotImplementedException();

            GetEvents(midi);
        }

        private void GetEvents(MidiReader midi)
        {
            //Adding SyncTrack.
            Tracks.Add(new List<ChartEvent>());
            //Adding Events.
            Tracks.Add(new List<ChartEvent>());

            switch (midi.Format)
            {
                case FormatType.SingleTrack:
                    Tracks.Add(ConvertTrack(midi.Chunks[0]));
                    break;
                case FormatType.MultiTrack:
                    ConvertTrack(midi.Chunks[0]);

                    for (int i = 1; i < midi.Chunks.Count; i++)
                        Tracks.Add(ConvertTrack(midi.Chunks[i]));
                    break;
                case FormatType.IndepentedMultiTrack:
                default:
                    throw new NotImplementedException();
            }
        }

        private List<ChartEvent> ConvertTrack(TrackChunk trackChunk)
        {
            var syncTrack = Tracks[(int)ChartTrack.SyncTrack];
            int pos = 0;
            var ons = new List<TempNote>();
            var notes = new List<TempNote>();
            var events = new List<ChartEvent>();
            var mapping = new List<int>();

            foreach (var evnt in trackChunk.Events)
            {
                pos += (int)evnt.DeltaTime;

                switch (evnt.Type)
                {
                    case EventType.MetaEvent:
                        var meta = (MetaEvent)evnt;
                        switch (meta.MetaType)
                        {
                            case MetaType.Tempo:
                                decimal tempo;

                                if (meta.TryToBpm(out tempo))
                                {
                                    syncTrack.Add(new Tempo(pos, (int)(tempo * 1000)));
                                }
                                break;
                            case MetaType.SMPTEOffset:
                                break;
                            case MetaType.TimeSignature:
                                syncTrack.Add(new TimeSignature(pos, meta.Values[0]));
                                break;
                            #region Not Used
                            case MetaType.SequenceNumber:
                                break;
                            case MetaType.Text:
                                break;
                            case MetaType.Copyright:
                                break;
                            case MetaType.TrackName:
                                break;
                            case MetaType.InstrumentName:
                                break;
                            case MetaType.Lyric:
                                break;
                            case MetaType.Marker:
                                break;
                            case MetaType.CuePoint:
                                break;
                            case MetaType.ProgramName:
                                break;
                            case MetaType.DeviceName:
                                break;
                            case MetaType.MidiChannelPrefix:
                                break;
                            case MetaType.MidiPort:
                                break;
                            case MetaType.EndOfTrack:
                                break;
                            case MetaType.KeySignature:
                                break;
                            case MetaType.SequencerSpecificEvent:
                                break;
                            #endregion
                            default:
                                break;
	                    }
                        break;
                    case EventType.NoteOff:
                        var off = (MidiChannelEvent)evnt;

                        for (int i = 0; i < ons.Count; i++)
                        {
                            if (ons[i].Note == off.Value1)
                            {
                                var length = pos - ons[i].Position;
                                if (length >= Resolution)
                                    ons[i].Length = length;

                                notes.Add(ons[i]);
                                ons.RemoveAt(i);
                                break;
                            }
                        }
                        break;
                    case EventType.NoteOn:
                        var on = (MidiChannelEvent)evnt;

                        if (!mapping.Contains(on.Value1))
                        {
                            if (mapping.Count == 5)
                                mapping = new List<int>();
                            else if (mapping.Count > 5)
                                throw new Exception("Mapping list became to big for unknown reasons.");

                            mapping.Add(on.Value1);
                            mapping.Sort();
                        }

                        ons.Add(new TempNote(pos, 0, on.Value1, mapping));
                        break;
                    #region Not Used
                    case EventType.Aftertouch:
                        break;
                    case EventType.ControlChange:
                        break;
                    case EventType.ProgramChange:
                        break;
                    case EventType.ChannelPressure:
                        break;
                    case EventType.PitchBend:
                        break;
                    case EventType.SystemExclusive:
                        break;
                    case EventType.MIDITimeCodeQuarterFrame:
                        break;
                    case EventType.SongPositionPointer:
                        break;
                    case EventType.SongSelect:
                        break;
                    case EventType.TuneRequest:
                        break;
                    case EventType.EndofExclusive:
                        break;
                    case EventType.TimingClock:
                        break;
                    case EventType.Start:
                        break;
                    case EventType.Continue:
                        break;
                    case EventType.Stop:
                        break;
                    case EventType.ActiveSensing:
                        break;
                    #endregion
                    default:
                        break;
                }
            }

            foreach (var note in ons)
                notes.Add(note);

            foreach (var note in notes)
                events.Add(note.ToNote());

            events.Sort();

            return events;
        }

        public void WriteChart(string path) { WriteChart(path, Name); }
        public void WriteChart(string path, string name)
        {
            using (var str = new StreamWriter(string.Format("{0}\\{1}.chart", path, name)))
            {
                str.WriteLine("[Song]");
                str.WriteLine("{");
                str.WriteLine("\tOffset = " + Offset);
                str.WriteLine("\tResolution = " + Resolution);
                str.WriteLine("}");

                for (ChartTrack i = 0; (int)i < Tracks.Count; i++)
                {
                    str.WriteLine("[" + i + "]");
                    str.WriteLine("{");

                    foreach (var evnt in Tracks[(int)i])
                    {
                        str.WriteLine(evnt.ToString());
                    }

                    str.WriteLine("}");
                }
            }
        }
    }
}
