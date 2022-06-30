using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalibrationParserSA
{
    public static class Extensions
    {
        public static byte[] ProgressiveReadAllBytes(this BinaryReader reader, int bufferSize = 16 * 1024)
        {
            byte[] buffer = new byte[bufferSize];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = reader.Read(buffer, 0, buffer.Length)) > 0)
                    ms.Write(buffer, 0, read);

                return ms.ToArray();
            }
        }
    }
}
