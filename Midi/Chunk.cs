using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidiLib
{
    public abstract class Chunk
    {
        public abstract string ChunkID { get; }
        public int Size { get; set; }

        public Chunk(int size)
        {
            Size = size;
        }

        public override string ToString()
        {
            return string.Format("ID: {0}\nSize: {1}\n", ChunkID, Size);
        }
    }
}
