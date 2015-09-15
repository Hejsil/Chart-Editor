using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sanford.Multimedia.Midi;
using System.IO;

namespace Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var midi = new Sequence(@"D:\Hejsil\Downloads\midi.mid");

            using (var str = new StreamWriter(@"D:\Hejsil\Downloads\text.txt"))
            {
                str.WriteLine(string.Format("Format: {0}", midi.Format));
                str.WriteLine(string.Format("Tracks: {0}", midi.Count));
                str.WriteLine(string.Format("Division: {0}", midi.Division));
                str.WriteLine();

                foreach (var track in midi)
                {
                    str.WriteLine(string.Format("Size: {0}", track.Length));

                    foreach (var evt in track.Iterator())
                    {
                        var stri = evt.DeltaTicks.ToString();

                        stri += string.Format(" {0} {1}", evt.MidiMessage.MessageType.ToString(), evt.MidiMessage.GetType());

                        foreach (var item in evt.MidiMessage.GetBytes())
                        {
                            stri += string.Format(" {0:X}", item);
                        }

                        str.WriteLine(stri);
                    }

                }
            }
        }
    }
}
