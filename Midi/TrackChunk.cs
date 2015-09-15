using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MidiLib
{
    public class TrackChunk : Chunk
    {
        public override string ChunkID { get { return "MTrk"; } }
        public List<Event> Events { get; set; }

        public TrackChunk(int size, List<Event> events)
            : base(size)
        {
            Events = events;
        }

        public override string ToString()
        {
            var res = base.ToString();

            foreach (var evt in Events)
                res += evt + "\n";

            return res;
        }

        public void WriteToFile(StreamWriter str)
        {
            str.WriteLine(string.Format("ID: {0}", ChunkID));
            str.WriteLine(string.Format("Size: {0}", Size));

            foreach (var evt in Events)
                str.WriteLine(evt.ToString());

            str.WriteLine();
        }
    }
}
