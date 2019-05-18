using System;
using System.Drawing;

namespace image_triangulation
{
    /// <summary>
    /// Содержит функцию для расчёта среднеквадратического отклонения между Bitmap-изображениями.
    /// </summary>
    static class StandartDeviation
    {
        /// <summary>
        /// Расчитывает среднеквадратическое отклонение между двумя Bitmap-изображениями одинакового разрешения.
        /// </summary>
        /// <param name="firstImage">Первое изображение.</param>
        /// <param name="secondImage">Второе изображение.</param>
        /// <returns>Значение среднеквадратического отклонения.</returns>
        public static double Run(Bitmap firstImage, Bitmap secondImage)
        {
            if (firstImage.Width != secondImage.Width || firstImage.Height != secondImage.Height)
            {
                // Исключение, изображения д.б. одного разрешения
                return 0;
            }

            double bufDiff = 0;
            for (int i = 0; i < firstImage.Height; ++i)
            {
                for (int j = 0; j < firstImage.Width; ++j)
                {
                    bufDiff += (firstImage.GetPixel(j, i).R - secondImage.GetPixel(j, i).R) * (firstImage.GetPixel(j, i).R - secondImage.GetPixel(j, i).R);
                }
            }

            return Math.Sqrt(bufDiff / (firstImage.Width * firstImage.Height));
        }
    }
}
