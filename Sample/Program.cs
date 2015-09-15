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
            var midi = new Sequence(@"C:\Users\Hejsil\Downloads\midi\sia-chandelier.mid");

            using (var str = new StreamWriter(@"C:\Users\Hejsil\Downloads\text.txt"))
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

                        foreach (var item in evt.MidiMessage.GetBytes())
                        {
                            stri += " " + item;
                        }

                        str.WriteLine(stri);
                    }

                }
            }

            Console.ReadKey();
        }
    }
}
