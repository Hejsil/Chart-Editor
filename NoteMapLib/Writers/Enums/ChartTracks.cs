using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteMapLib.Writers.Enums
{
    public enum ChartTracks
    {
        SyncTrack               = 0x00,
        Events                  = 0x01,

        ExpertSingle            = 0x10,
        ExpertDoubleGuitar      = 0x11,
        ExpertDoubleBass        = 0x12,
        ExpertEnhancedGuitar    = 0x13,
        ExpertCoopLead          = 0x14,
        ExpertCoopBass          = 0x15,
        Expert10KeyGuitar       = 0x16,
        ExpertDrums             = 0x17,
        ExpertDoubleDrums       = 0x18,
        ExpertVocals            = 0x19,
        ExpertKeyboard          = 0x1A,
        
        HardSingle              = 0x20,
        HardDoubleGuitar        = 0x21,
        HardDoubleBass          = 0x22,
        HardEnhancedGuitar      = 0x23,
        HardCoopLead            = 0x24,
        HardCoopBass            = 0x25,
        Hard10KeyGuitar         = 0x26,
        HardDrums               = 0x27,
        HardDoubleDrums         = 0x28,
        HardVocals              = 0x29,
        HardKeyboard            = 0x2A,
        
        MediumSingle            = 0x30,
        MediumDoubleGuitar      = 0x31,
        MediumDoubleBass        = 0x32,
        MediumEnhancedGuitar    = 0x33,
        MediumCoopLead          = 0x34,
        MediumCoopBass          = 0x35,
        Medium10KeyGuitar       = 0x36,
        MediumDrums             = 0x37,
        MediumDoubleDrums       = 0x38,
        MediumVocals            = 0x39,
        MediumKeyboard          = 0x3A,


        EasySingle              = 0x40,
        EasyDoubleGuitar        = 0x41,
        EasyDoubleBass          = 0x42,
        EasyEnhancedGuitar      = 0x43,
        EasyCoopLead            = 0x44,
        EasyCoopBass            = 0x45,
        Easy10KeyGuitar         = 0x46,
        EasyDrums               = 0x47,
        EasyDoubleDrums         = 0x48,
        EasyVocals              = 0x49,
        EasyKeyboard            = 0x4A,

        DontChart               = EasyKeyboard + 1
    }
}
