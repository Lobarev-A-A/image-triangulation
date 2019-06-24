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
        private Bitmap sourceImage;
        private HashSet<Pixel> pivotPoints;
        private byte threshold;
        private int sectorSize;
        private int width;
        private int height;

        public ParallelSectorPPMaker(Bitmap sourceImage, HashSet<Pixel> pivotPoints, byte threshold, int sectorSize)
        {
            this.sourceImage = sourceImage;
            this.pivotPoints = pivotPoints;
            this.threshold = threshold;
            this.sectorSize = sectorSize;
            this.width = sourceImage.Width;
            this.height = sourceImage.Height;
        }

        public void Run()
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

        int GetIndex(int x, int y)
        {
            return y * width + x;
        }
    }
}
