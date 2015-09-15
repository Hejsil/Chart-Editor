using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Sanford.Multimedia.Midi;


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
        public int Resolution { get; set; }
        public int Offset { get; set; }

        public Chart(string name = "Untilted", int resolution = 0, int offset = 0)
        {
            Name = name;
            Resolution = resolution;
            Offset = offset;
            Tracks = new List<List<ChartEvent>>();
        }

        public Chart(string path) : this(new Sequence(path), Path.GetFileNameWithoutExtension(path)) { }
        public Chart(Sequence midi) : this(midi, "Unknown Name") { }
        public Chart(Sequence midi, string name)
        {
            Name = name;
            Tracks = new List<List<ChartEvent>>();
            Generate(midi);
        }

        private void Generate(Sequence midi)
        {
            Tracks.Clear();

            if ((midi.Division & 0x80) != 1)
                Resolution = midi.Division;
            else
                throw new NotImplementedException();

            GetEvents(midi);
        }

        private void GetEvents(Sequence midi)
        {
            //Adding SyncTrack.
            Tracks.Add(new List<ChartEvent>());
            //Adding Events.
            Tracks.Add(new List<ChartEvent>());

            switch (midi.Format)
            {
                case 0:
                    Tracks.Add(ConvertTrack(midi[0]));
                    break;
                case 1:
                    for (int i = 0; i < midi.Count; i++)
                        Tracks.Add(ConvertTrack(midi[i]));
                    break;
                case 2:
                default:
                    throw new NotImplementedException();
            }
        }

        private List<ChartEvent> ConvertTrack(Track track)
        {
            var syncTrack = Tracks[(int)ChartTrack.SyncTrack];
            int pos = 0;
            var ons = new List<TempNote> ();
            var notes = new List<TempNote>();
            var events = new List<ChartEvent>();
            var mapping = new List<int>();

            foreach (var evnt in track.Iterator())
            {
                var message = evnt.MidiMessage;
                var bytes = message.GetBytes();
                pos += evnt.DeltaTicks;

                switch (message.MessageType)
                {
                    case MessageType.Channel:
                        switch (bytes[0] & 0xF0)
                        {
                            case 0x80:
                                for (int i = 0; i < ons.Count; i++)
                                {
                                    if (ons[i].Note == bytes[1])
                                    {
                                        var length = RoundOffToNearest(pos, 64) - ons[i].Position;
                                        if (length >= Resolution)
                                            ons[i].Length = length;

                                        notes.Add(ons[i]);
                                        ons.RemoveAt(i);
                                        break;
                                    }
                                }
                                break;
                            case 0x90:
                                if (!mapping.Contains(bytes[1]))
                                {
                                    if (mapping.Count == 5)
                                    {
                                        mapping.Sort();
                                        mapping = new List<int>();
                                    }
                                    else if (mapping.Count > 5)
                                        throw new Exception("Mapping list became to big for unknown reasons.");

                                    mapping.Add(bytes[1]);
                                }

                                ons.Add(new TempNote(RoundOffToNearest(pos, 64), 0, bytes[1], mapping));
                                break;
                            default:
                                break;
                        }
                        break;
                    case MessageType.SystemExclusive:
                        break;
                    case MessageType.SystemCommon:
                        break;
                    case MessageType.SystemRealtime:
                        break;
                    case MessageType.Meta:
                        switch ((message as MetaMessage).MetaType)
                        {
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
                            case MetaType.EndOfTrack:
                                break;
                            case MetaType.Tempo:
                                int value = 0;
                                int offset = bytes.Length - 1;

                                foreach (var item in bytes)
                                {
                                    value += item << offset * 8;
                                    offset--;
                                }

                                syncTrack.Add(new Tempo(RoundOffToNearest(pos, 64), (int)(60000000000 / value)));
                                break;
                            case MetaType.SmpteOffset:
                                syncTrack.Add(new TimeSignature(RoundOffToNearest(pos, 64), bytes[0]));
                                break;
                            case MetaType.TimeSignature:
                                break;
                            case MetaType.KeySignature:
                                break;
                            case MetaType.ProprietaryEvent:
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }


            mapping.Sort();

            foreach (var note in ons)
                notes.Add(note);

            foreach (var note in notes)
                events.Add(note.ToNote());

            events.Sort();

            return events;
        }

        private int RoundOffToNearest(int value, int number)
        {
            int note = ((Resolution * 4) / number);
            int rest = value % note;
            int res = value - rest;

            if (rest >= note / 2)
            {
                res += note;
            }

            return res;
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
