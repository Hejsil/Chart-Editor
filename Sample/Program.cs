using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sanford.Multimedia.Midi;
using System.IO;
using NoteMapLib;
using NoteMapLib.Models;

namespace Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            NoteMap.GenerateFromMidi(@"D:\Hejsil\Downloads\FZC_Mute_City_Arr-KM.mid").WriteGuitarHeroChart(@"D:\Hejsil\Downloads\test.chart");

            /*
			ChannelMessageBuilder channelBuilder = new ChannelMessageBuilder();
			TempoChangeBuilder tempoBuilder = new TempoChangeBuilder();

			Sequence sequence = new Sequence();

			Track track0 = new Track();
			Track track1 = new Track();
			Track track2 = new Track();

			sequence.Add(track0);
			sequence.Add(track1);
			sequence.Add(track2);

			tempoBuilder.Tempo = (int)(1 / 150.0 * 60000000);
			tempoBuilder.Build();
			track0.Insert(0, tempoBuilder.Result);

			channelBuilder.MidiChannel = 1;

			channelBuilder.Command = ChannelCommand.ProgramChange;
			channelBuilder.Data1 = (int)GeneralMidiInstrument.AcousticGrandPiano;
			channelBuilder.Data2 = 0;
			channelBuilder.Build();
			track1.Insert(0, channelBuilder.Result);

			channelBuilder.Command = ChannelCommand.NoteOn;
			channelBuilder.Data1 = 60; // note C
			channelBuilder.Data2 = 127; // velocity 127
			channelBuilder.Build();
			track1.Insert(0, channelBuilder.Result);

			channelBuilder.Command = ChannelCommand.NoteOff;
			channelBuilder.Data1 = 60; // note C
			channelBuilder.Data2 = 0; // note off, so velocity 0
			channelBuilder.Build();
			track1.Insert(479, channelBuilder.Result);

			channelBuilder.MidiChannel = 2;

			channelBuilder.Command = ChannelCommand.ProgramChange;
			channelBuilder.Data1 = (int)GeneralMidiInstrument.AcousticBass;
			channelBuilder.Data2 = 0;
			channelBuilder.Build();
			track2.Insert(0, channelBuilder.Result);

			channelBuilder.Command = ChannelCommand.NoteOn;
			channelBuilder.Data1 = 67; // note G
			channelBuilder.Data2 = 60; // velocity 60
			channelBuilder.Build();
			track2.Insert(480, channelBuilder.Result);

			channelBuilder.Command = ChannelCommand.NoteOff;
			channelBuilder.Data1 = 67; // note G
			channelBuilder.Data2 = 0; // note off, so velocity 0
			channelBuilder.Build();
			track2.Insert(480 + 760, channelBuilder.Result);

			sequence.Save(@"D:\Hejsil\Downloads\text.mid");
            */
        }
    }
}
