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
            byte[] brightnessValues = new byte[numberOfBytes];
            System.Runtime.InteropServices.Marshal.Copy(ptr, brightnessValues, 0, numberOfBytes);

            // Записываем стартовые точки триангуляции
            Pixel[] initPixels = { new Pixel(0, 0, brightnessValues[GetIndex(0, 0)]),
                                   new Pixel(width - 1, 0, brightnessValues[GetIndex(width - 1, 0)]),
                                   new Pixel(width - 1, height - 1, brightnessValues[GetIndex(width - 1, height - 1)]),
                                   new Pixel(0, height - 1, brightnessValues[GetIndex(0, height - 1)]) };
            foreach (Pixel p in initPixels) pivotPoints.Add(p);

            // Параллельный поиск ОТ
            object locker = new object();
            Parallel.For<LocalData>(
                0,
                height,
                () => new LocalData(threshold, sectorSize, width, height),
                (i, state, local) => 
                {
                    List<Pixel> sector = new List<Pixel>();

                    for (int j = 0; j < local.width; j += local.sectorSize)
                    {
                        // Формируем список пикселей текущего сектора
                        sector.Clear();
                        for (int ii = 0, io = i; ii < local.sectorSize && io < local.height; ++ii, ++io)
                        {
                            for (int ji = 0, jo = j; ji < local.sectorSize && jo < local.width; ++ji, ++jo)
                            {
                                sector.Add(new Pixel(jo, io, brightnessValues[GetIndex(jo, io)]));
                            }
                        }

                        // Подсчитываем среднюю яркость по сектору, находим самый яркий и самый тусклый пиксели
                        float averageBrightness = sector[0].brightness;
                        Pixel lightPixel = sector[0];
                        Pixel darkPixel = sector[0];
                        foreach (Pixel p in sector.Skip(1))
                        {
                            averageBrightness += p.brightness;
                            if (p.brightness > lightPixel.brightness) lightPixel = p;
                            if (p.brightness < darkPixel.brightness) darkPixel = p;
                        }
                        averageBrightness /= sector.Count;

                        // Проверяем самый яркий и самый тусклый пиксели и заносим в HashSet
                        if (averageBrightness - darkPixel.brightness >= local.threshold) local.localPivotPoints.Add(darkPixel);
                        if (lightPixel.brightness - averageBrightness >= local.threshold) local.localPivotPoints.Add(lightPixel);
                    }

                    return local;
                },
                (local) => 
                {
                    lock (locker)
                    {
                        pivotPoints.Concat(local.localPivotPoints);
                    }
                }
            );
        }

        int GetIndex(int x, int y)
        {
            return y * width + x;
        }

        struct LocalData
        {
            public HashSet<Pixel> localPivotPoints;
            public byte threshold;
            public int sectorSize;
            public int width;
            public int height;

            public LocalData(byte threshold, int sectorSize, int width, int height)
            {
                localPivotPoints = new HashSet<Pixel>();
                this.threshold = threshold;
                this.sectorSize = sectorSize;
                this.width = width;
                this.height = height;
            }
        }
    }
}
