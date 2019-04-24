using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

namespace image_triangulation
{
    /// <summary>
    /// Содержит функции для работы с .t файлами.
    /// </summary>
    static class TExtension
    {
        public static void Save(Dictionary<float, Pixel> pivotPoints, string path, int shaderIndex)
        {
            using (BinaryWriter tmpFileWriter = new BinaryWriter(File.Create(path + "~")))
            {
                tmpFileWriter.Write((byte)shaderIndex);
                foreach (Pixel pixel in pivotPoints.Values)
                {
                    tmpFileWriter.Write((short)pixel.X);
                    tmpFileWriter.Write((short)pixel.Y);
                    tmpFileWriter.Write(pixel.brightness);
                }
            }

            using (FileStream tmpFileReader = new FileStream(path + "~", FileMode.Open))
            {
                using (FileStream outputFileWriter = new FileStream(path, FileMode.Create))
                {
                    using (GZipStream compressionStream = new GZipStream(outputFileWriter, CompressionMode.Compress))
                    {
                        tmpFileReader.CopyTo(compressionStream);
                    }
                }
            }
            File.Delete(path + "~");
        }

        public static void Open(Dictionary<float, Pixel> pivotPoints, string path)
        {

        }
    }
}
