using System.Collections.Generic;
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
    static class PPMaker1
    {
        /// <summary>
        /// Производит выбор опорных точек на изображении.
        /// </summary>
        /// <param name="sourceImage">Изображение, на котором требуется выбрать опорные точки.</param>
        /// <param name="threshold">Порог чувствительности.</param>
        /// <param name="pivotPoints">Ссылка, по которой будет сформирован список опорных точек.</param>
        public static void Run(Bitmap sourceImage, float threshold, List<Pixel> pivotPoints)
        {
            int outerLoopEndPoint = sourceImage.Height - 4;
            int innerLoopEndPoint = sourceImage.Width - 4;
            float averageBright;
            float minBright;
            float maxBright;
            Pixel bufPoint;

            for (int i = 0; i <= outerLoopEndPoint; i += 4)
            {
                for (int j = 0; j <= innerLoopEndPoint; j += 4)
                {
                    averageBright = AverageBright(sourceImage, j, i);
                    // Проверка пикселя минимальной яркости
                    bufPoint = MinBrightPoint(sourceImage, j, i);
                    minBright = sourceImage.GetPixel(bufPoint.X, bufPoint.Y).GetBrightness();
                    if ((averageBright - minBright) >= threshold) pivotPoints.Add(bufPoint);
                    // Проверка пикселя максимальной яркости
                    bufPoint = MaxBrightPoint(sourceImage, j, i);
                    maxBright = sourceImage.GetPixel(bufPoint.X, bufPoint.Y).GetBrightness();
                    if ((maxBright - averageBright) >= threshold) pivotPoints.Add(bufPoint);
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
                    buf += sourceImage.GetPixel(x + i, y + j).GetBrightness();
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
        private static Pixel MaxBrightPoint(Bitmap sourceImage, int x, int y)
        {
            Pixel outPoint = new Pixel(x, y);
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

        /// <summary>
        /// Возвращает Pixel с координатами самого тёмного пикселя в секторе.
        /// </summary>
        /// <param name="sourceImage">Изображение.</param>
        /// <param name="x">Координата сектора по x.</param>
        /// <param name="y">Координата сектора по y.</param>
        /// <returns>Pixel с координатами самого тёмного пикселя в секторе.</returns>
        private static Pixel MinBrightPoint(Bitmap sourceImage, int x, int y)
        {
            Pixel outPoint = new Pixel(x, y);
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
