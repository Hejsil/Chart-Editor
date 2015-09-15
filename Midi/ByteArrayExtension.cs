using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MidiLib
{
    public static class Extension
    {
        public static int ToInt32(this byte[] byt)
        {
            int count = byt.Count();
            int res = 0;

            for (int i = 0, j = count - 1; i < count; i++, j--)
            {
                res += byt[i] << (j * 8);
            }

            return res;
        }

        public static uint ToUInt32(this byte[] byt)
        {
            return (uint)byt.ToInt32();
        }
    }
}
