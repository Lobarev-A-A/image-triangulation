﻿using System.Collections.Generic;
using System.Drawing;

// TO DO
// * Распространить на изображения произвольного разрешения
// * Прикрутить многопоточность
namespace image_triangulation
{
    /// <summary>
    /// Класс реализует алгоритм выбора опорных точек триангуляции на изображении.
    /// Изображение разбивается на секторы 4 x 4 пикселя. Для каждого сектора считается средняя яркость, затем проверяются самая яркая и самая
    /// тёмная точки сектора. Если абсолютная разница между яркостью проверяемой точки и средней яркостью сектора превышает заданный порог,
    /// точка выбирается в качетсве опорной.
    /// </summary>
    /// <remarks>
    /// В данной реализации работает только с изображениями, у которых обе размерности кратны 4.
    /// </remarks>
    static class SectorPPMaker1
    {
        /// <summary>
        /// Производит выбор опорных точек на изображении.
        /// </summary>
        /// <param name="sourceImage">Изображение, на котором требуется выбрать опорные точки.</param>
        /// <param name="threshold">Порог чувствительности.</param>
        /// <param name="pivotPoints">Ссылка, по которой будет сформирован список опорных точек.</param>
        public static void Run(Bitmap sourceImage, byte threshold, HashSet<Pixel> pivotPoints)
        {
            int outerLoopEndPixel = sourceImage.Height - 4;
            int innerLoopEndPixel = sourceImage.Width - 4;
            float averageBright;
            byte minBright;
            byte maxBright;
            Pixel bufPixel;

            // Записываем стартовые точки триангуляции
            Pixel[] initPixels = { new Pixel(0, 0, sourceImage.GetPixel(0, 0).R), new Pixel(511, 0, sourceImage.GetPixel(511, 0).R),
                                   new Pixel(511, 511, sourceImage.GetPixel(511, 511).R), new Pixel(0, 511, sourceImage.GetPixel(0, 511).R) };
            for (byte i = 0; i < 4; ++i)
            {
                pivotPoints.Add(initPixels[i]);
            }

            for (int i = 0; i <= outerLoopEndPixel; i += 4)
            {
                for (int j = 0; j <= innerLoopEndPixel; j += 4)
                {
                    averageBright = AverageBright(sourceImage, j, i);
                    // Проверка пикселя минимальной яркости
                    bufPixel = MinBrightPixel(sourceImage, j, i);
                    minBright = sourceImage.GetPixel(bufPixel.X, bufPixel.Y).R;
                    if ((averageBright - minBright) >= threshold) pivotPoints.Add(bufPixel);
                    // Проверка пикселя максимальной яркости
                    bufPixel = MaxBrightPixel(sourceImage, j, i);
                    maxBright = sourceImage.GetPixel(bufPixel.X, bufPixel.Y).R;
                    if ((maxBright - averageBright) >= threshold) pivotPoints.Add(bufPixel);
                }
            }
        }

        /// <summary>
        /// Возвращает среднюю яркость по сектору.
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
                    buf += sourceImage.GetPixel(x + i, y + j).R;
                }
            }

            return buf / 16;
        }

        /// <summary>
        /// Возвращает Pixel с координатами самого яркого пикселя в секторе.
        /// </summary>
        /// <param name="sourceImage">Изображение.</param>
        /// <param name="x">Координата сектора по x.</param>
        /// <param name="y">Координата сектора по y.</param>
        /// <returns>Pixel с координатами самого яркого пикселя в секторе.</returns>
        private static Pixel MaxBrightPixel(Bitmap sourceImage, int x, int y)
        {
            Pixel outPixel = new Pixel(x, y, sourceImage.GetPixel(x, y).R);
            for (int i = 0; i < 4; ++i)
            {
                for (int j = 0; j < 4; ++j)
                {
                    if (sourceImage.GetPixel(x + i, y + j).R > outPixel.brightness)
                    {
                        outPixel.X = x + i;
                        outPixel.Y = y + j;
                        outPixel.brightness = sourceImage.GetPixel(x + i, y + j).R;
                    }
                }
            }
            return outPixel;
        }

        /// <summary>
        /// Возвращает Pixel с координатами самого тёмного пикселя в секторе.
        /// </summary>
        /// <param name="sourceImage">Изображение.</param>
        /// <param name="x">Координата сектора по x.</param>
        /// <param name="y">Координата сектора по y.</param>
        /// <returns>Pixel с координатами самого тёмного пикселя в секторе.</returns>
        private static Pixel MinBrightPixel(Bitmap sourceImage, int x, int y)
        {
            Pixel outPixel = new Pixel(x, y, sourceImage.GetPixel(x, y).R);
            for (int i = 0; i < 4; ++i)
            {
                for (int j = 0; j < 4; ++j)
                {
                    if (sourceImage.GetPixel(x + i, y + j).R < outPixel.brightness)
                    {
                        outPixel.X = x + i;
                        outPixel.Y = y + j;
                        outPixel.brightness = sourceImage.GetPixel(x + i, y + j).R;
                    }
                }
            }
            return outPixel;
        }
    }
}
