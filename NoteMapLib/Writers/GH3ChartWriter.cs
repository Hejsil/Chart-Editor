using Fractions;
using NoteMapLib.Model;
using NoteMapLib.Model.Enums;
using NoteMapLib.Writers.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NoteMapLib.Writers
{
    public class GH3ChartWriter
    {
        public void Write(string path, NoteMap notemap)
        {
            using (var str = new StreamWriter(path))
            {
                List<ChartTracks> tracksCharted = new List<ChartTracks>();

                str.WriteLine("[Song]");
                str.WriteLine("{");
                str.WriteLine("\tOffset = " + notemap.Offset);
                str.WriteLine("\tResolution = " + notemap.Division);
                str.WriteLine("}");

                str.WriteLine("[SyncTrack]");
                str.WriteLine("{");
                WriteSyncTrack(str, notemap);
                str.WriteLine("}");

                str.WriteLine("[Events]");
                str.WriteLine("{");
                WriteEvents(str, notemap);
                str.WriteLine("}");

                foreach (var track in notemap)
                {
                    ChartTracks trackType = GetTrackType(track);
                    ChartTracks original = trackType;

                    for (int i = 0; ; i++)
                    {
                        trackType = (ChartTracks)((int)original + (0x10 * i));

                        if (trackType > ChartTracks.DontChart)
                        {
                            trackType = ChartTracks.DontChart;
                        }

                        if (trackType >= ChartTracks.DontChart || !tracksCharted.Contains(trackType))
                        {
                            break;
                        }
                    }

                    str.WriteLine("[" + trackType + "]");
                    str.WriteLine("{");
                    WriteTrack(track, str);
                    str.WriteLine("}");

                    tracksCharted.Add(trackType);
                }
            }
        }

        private void WriteTrack(Track track, StreamWriter str)
        {
            foreach (var evnt in track)
            {
                switch (evnt.Type)
                {
                    case TrackEventTypes.Note:
                        str.WriteLine(string.Format("	{0} = N {1} {2}", evnt.Position, evnt.Value, evnt.Length));
                        break;
                    case TrackEventTypes.Force:
                        throw new NotImplementedException();
                    case TrackEventTypes.Tap:
                        throw new NotImplementedException();
                    case TrackEventTypes.Overdrive:
                        str.WriteLine(string.Format("	{0} = S 2 {1}", evnt.Position, evnt.Length));
                        break;
                    default:
                        break;
                }
            }
        }

        private void WriteSyncTrack(StreamWriter str, NoteMap notemap)
        {
            foreach (var evnt in notemap.MetaEvents)
            {
                switch (evnt.Type)
                {
                    case MetaEventTypes.Tempo:
                        str.WriteLine(string.Format("	{0} = B {1}", evnt.Position, (int)evnt.Data));
                        break;
                    case MetaEventTypes.TimeSignature:
                        str.WriteLine(string.Format("	{0} = TS {1}", evnt.Position, ((Fraction)evnt.Data).Numerator));
                        break;
                    default:
                        break;
                }
            }
        }

        private void WriteEvents(StreamWriter str, NoteMap notemap)
        {
            foreach (var evnt in notemap.MetaEvents)
            {
                switch (evnt.Type)
                {
                    case MetaEventTypes.SectionName:
                        str.WriteLine(string.Format("	{0} = E \"section {1}\"", evnt.Position, (string)evnt.Data));
                        break;
                    default:
                        break;
                }
            }
        }

        private ChartTracks GetTrackType(Track track)
        {
            ChartTracks res;

            switch (track.Type)
            {
                case TrackTypes.Guitar:
                    res = ChartTracks.ExpertSingle;
                    break;

                case TrackTypes.Bass:
                    res = ChartTracks.ExpertDoubleBass;
                    break;

                case TrackTypes.GuitarCoop:
                    res = ChartTracks.ExpertDoubleGuitar;
                    break;

                case TrackTypes.Rhythm:
                    res = ChartTracks.ExpertDoubleGuitar;
                    break;

                default:
                    res = ChartTracks.DontChart;
                    break;
            }

            if (res != ChartTracks.DontChart)
                res += 0x10 * (int)track.Difficulty;

            return res;
        }
    }
}
