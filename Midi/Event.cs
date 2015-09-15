using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidiLib
{
    public enum EventType
    {
        MetaEvent = 0xFF,

        //Midi Channel Events
        NoteOff = 0x80,
        NoteOn = 0x90,
        Aftertouch = 0xA0,
        ControlChange = 0xB0,
        ProgramChange = 0xC0,
        ChannelPressure = 0xD0,
        PitchBend = 0xE0,

        //System Exclusive Events
        SystemExclusive = 0xF0,
        MIDITimeCodeQuarterFrame = 0xF1,
        SongPositionPointer = 0xF2,
        SongSelect = 0xF3,
        TuneRequest = 0xF6,
        EndofExclusive = 0xF7,
        TimingClock = 0xF8,
        Start = 0xFA,
        Continue = 0xFB,
        Stop = 0xFC,
        ActiveSensing = 0xFE
    }

    public abstract class Event
    {
        public int DeltaTime { get; set; }
        public EventType Type { set; get; }

        public Event(int deltaTime, EventType type)
        {
            DeltaTime = deltaTime;
            Type = type;
        }

        public override string ToString()
        {
            return string.Format("{0,6} {1}", DeltaTime, Type);
        }
    }
}
