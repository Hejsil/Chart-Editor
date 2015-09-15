using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidiLib
{
    public enum MetaType 
    {
        SequenceNumber = 0x00,
        Text = 0x01,
        Copyright = 0x02,
        TrackName = 0x03,
        InstrumentName = 0x04,
        Lyric = 0x05,
        Marker = 0x06,
        CuePoint = 0x07,
        ProgramName = 0x08,
        DeviceName = 0x09,
        MidiChannelPrefix = 0x20,
        MidiPort = 0x21,
        EndOfTrack = 0x2F,
        Tempo = 0x51,
        SMPTEOffset = 0x54,
        TimeSignature = 0x58,
        KeySignature = 0x59,
        SequencerSpecificEvent = 0x7F
    }

    public class MetaEvent : Event
    {
        public int Length { get; set; }
        public MetaType MetaType { get; set; }
        public byte[] Values { get; set; }

        public MetaEvent(int deltaTime, EventType type, MetaType metaType, int length, byte[] values)
            : base(deltaTime, type)
        {
            MetaType = metaType;
            Length = length;
            Values = values;
        }

        public bool TryToBpm(out decimal res)
        {
            if (MetaType == MetaType.Tempo)
            {
                res = 60000000M / (decimal)Values.ToInt32();
                return true;
            }

            res = 0;
            return false;
        }

        public override string ToString()
        {
            var res = string.Format("{0} {1} {2}", base.ToString(), MetaType, Length);

            foreach (var value in Values)
            {
                res += " " + value;
            }

            return res;
        }
    }
}
