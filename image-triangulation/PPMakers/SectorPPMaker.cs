using System.Collections.Generic;
using System.Linq;
using System.Drawing;

// TODO
// * 
namespace image_triangulation
{
    static class SectorPPMaker
    {
        public static void Run(Bitmap sourceImage, HashSet<Pixel> pivotPoints, byte threshold, int sectorSize)
        {
            // Записываем стартовые точки триангуляции
            Pixel[] initPixels = { new Pixel(0, 0, sourceImage.GetPixel(0, 0).R),
                                   new Pixel(sourceImage.Width - 1, 0, sourceImage.GetPixel(sourceImage.Width - 1, 0).R),
                                   new Pixel(sourceImage.Width - 1, sourceImage.Height - 1, sourceImage.GetPixel(sourceImage.Width - 1, sourceImage.Height - 1).R),
                                   new Pixel(0, sourceImage.Height - 1, sourceImage.GetPixel(0, sourceImage.Height - 1).R) };
            foreach (Pixel p in initPixels) pivotPoints.Add(p);

            List<Pixel> sector = new List<Pixel>();
            float averageBrightness;
            Pixel lightPixel, darkPixel;

            for (int i = 0; i < sourceImage.Height; i += sectorSize)
            {
                for (int j = 0; j < sourceImage.Width; j += sectorSize)
                {
                    // Формируем список пикселей текущего сектора
                    sector.Clear();
                    for (int ii = 0, io = i; ii < sectorSize && io < sourceImage.Height; ++ii, ++io)
                    {
                        for (int ji = 0, jo = j; ji < sectorSize && jo < sourceImage.Width; ++ji, ++jo)
                        {
                            sector.Add(new Pixel(jo, io, sourceImage.GetPixel(jo, io).R));
                        }
                    }

                    // Подсчитываем среднюю яркость по сектору, находим самый яркий и самый тусклый пиксели
                    averageBrightness = sector[0].brightness;
                    lightPixel = sector[0];
                    darkPixel = sector[0];
                    foreach (Pixel p in sector.Skip(1))
                    {
                        averageBrightness += p.brightness;
                        if (p.brightness > lightPixel.brightness) lightPixel = p;
                        if (p.brightness < darkPixel.brightness) darkPixel = p;
                    }
                    averageBrightness /= sector.Count;

                    // Проверяем самый яркий и самый тусклый пиксели и заносим в HashSet
                    if (averageBrightness - darkPixel.brightness >= threshold) pivotPoints.Add(darkPixel);
                    if (lightPixel.brightness - averageBrightness >= threshold) pivotPoints.Add(lightPixel);
                }
            }
        }
    }
}
