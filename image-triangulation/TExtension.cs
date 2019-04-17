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
        public static void Save(HashSet<Triangle> triangles, string path, int shaderIndex)
        {
            using (BinaryWriter tmpFileWriter = new BinaryWriter(File.Create(path + "~")))
            {
                tmpFileWriter.Write((byte)shaderIndex);
                foreach (Triangle triangle in triangles)
                {
                    for (int i = 0; i < 3; ++i)
                    {
                        tmpFileWriter.Write((short)triangle.points[i].X);
                        tmpFileWriter.Write((short)triangle.points[i].Y);
                        tmpFileWriter.Write(triangle.points[i].brightness);
                    }
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
    }
}
