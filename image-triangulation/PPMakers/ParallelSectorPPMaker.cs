using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace image_triangulation
{
    class ParallelSectorPPMaker
    {
        public static void Run(Bitmap sourceImage, HashSet<Pixel> pivotPoints, byte threshold, int sectorSize)
        {
            // Блокируем в памяти Bitmap исходного изображения
            Rectangle rect = new Rectangle(0, 0, sourceImage.Width, sourceImage.Height);
            System.Drawing.Imaging.BitmapData bmpData = sourceImage.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly,
                                                                             sourceImage.PixelFormat);
            // Получаем адрес первой строки
            IntPtr ptr = bmpData.Scan0;
            // Сохраняем байты в массив
            int numberOfBytes = Math.Abs(bmpData.Stride) * bmpData.Height;
            byte[] rgbValues = new byte[numberOfBytes];
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, numberOfBytes);
        }
    }
}
