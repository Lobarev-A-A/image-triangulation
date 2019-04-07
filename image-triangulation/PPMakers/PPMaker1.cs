using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace image_triangulation
{
    /// <summary>
    /// Поиск осуществляется по сектору 4 х 4. Порог – минимальное значение отклонения яркости пикселя от средней яркости по сектору в процентах для
    /// выбора в качестве опорной точки. В качестве опорных выбираются две точки, самая яркая и самая тёмная, если модуль отклонения по яркости
    /// превышает порог.
    /// </summary>
    static class PPMaker1
    {
        // TO DO
        // При применении к изображениям, размерности которых не кратны 4-м, необходимо добавить обработку выхода сектора
        // за границы изображения.
        public static void Run(Bitmap sourceImage, float threshold, List<Point> pivotPoints)
        {
            int outerLoopEndPoint = sourceImage.Height - 4;
            int innerLoopEndPoint = sourceImage.Width - 4;
            float averageBright;
            float minBright;
            float maxBright;
            Point bufPoint;

            for (int i = 0; i <= outerLoopEndPoint; i += 4)
            {
                for (int j = 0; j <= innerLoopEndPoint; j += 4)
                {
                    averageBright = AverageBright(sourceImage, j, i);
                    // Проверка пикселя минимальной яркости
                    bufPoint = MinBrightPoint(sourceImage, j, i);
                    minBright = sourceImage.GetPixel(bufPoint.X, bufPoint.Y).GetBrightness();
                    if ((Math.Abs(averageBright - minBright) / averageBright) >= threshold) pivotPoints.Add(bufPoint);
                    // Проверка пикселя максимальной яркости
                    bufPoint = MaxBrightPoint(sourceImage, j, i);
                    maxBright = sourceImage.GetPixel(bufPoint.X, bufPoint.Y).GetBrightness();
                    if (((Math.Abs(averageBright - maxBright) / averageBright) >= threshold )) pivotPoints.Add(bufPoint);
                }
            }
        }

        /// <summary>
        /// Возвращает среднюю яркость по сектору изображения.
        /// </summary>
        /// <param name="sourceImage">Изображение.</param>
        /// <param name="x">Координата сектора по x.</param>
        /// <param name="y">Координата сектора по y.</param>
        /// <returns>Средняя яркость по сектору.</returns>
        private static float AverageBright(Bitmap sourceImage, int x, int y)
        {
            float buf = 0;
            for (int i = 0; i < 4; ++i)
            {
                for (int j = 0; j < 4; ++j)
                {
                    buf += sourceImage.GetPixel(x + i, y + j).GetBrightness();
                }
            }

            return buf / 16;
        }

        private static Point MaxBrightPoint(Bitmap sourceImage, int x, int y)
        {
            Point outPoint = new Point(x, y);
            for (int i = 0; i < 4; ++i)
            {
                for (int j = 0; j < 4; ++j)
                {
                    if (sourceImage.GetPixel(x + i, y + j).GetBrightness() > sourceImage.GetPixel(outPoint.X, outPoint.Y).GetBrightness())
                    {
                        outPoint.X = x + i;
                        outPoint.Y = y + j;
                    }
                }
            }

            return outPoint;
        }

        private static Point MinBrightPoint(Bitmap sourceImage, int x, int y)
        {
            Point outPoint = new Point(x, y);
            for (int i = 0; i < 4; ++i)
            {
                for (int j = 0; j < 4; ++j)
                {
                    if (sourceImage.GetPixel(x + i, y + j).GetBrightness() < sourceImage.GetPixel(outPoint.X, outPoint.Y).GetBrightness())
                    {
                        outPoint.X = x + i;
                        outPoint.Y = y + j;
                    }
                }
            }

            return outPoint;
        }
    }
}
