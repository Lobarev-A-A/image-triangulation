using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

// TO DO
// * Распространить на изображения произвольного разрешения
namespace image_triangulation
{
    static class StripIterativeTriangulation
    {
        static List<List<Pixel>> Stripes = new List<List<Pixel>>();
        static List<Pixel> pivotPointsList = new List<Pixel>();

        public static void Run(Dictionary<float, Pixel> pivotPoints, List<Section> outputTriangulation,
                                             HashSet<Triangle> outputTriangles, Bitmap originalImageBitmap, float stripingFactor)
        {
            Striping(pivotPoints, stripingFactor);

            // Удаляем из словаря точки, которыми инициировалась триангуляция, если они там есть
            Pixel[] initPixels = { new Pixel(0, 0), new Pixel(0, 511), new Pixel(511, 511), new Pixel(0, 511) };
            for (int i = 0; i < 4; ++i)
            {
                if (pivotPoints.ContainsKey(initPixels[i].GetHashCode())) pivotPoints.Remove(initPixels[i].GetHashCode());
            }

            SimpleIterativeTriangulation.Run(pivotPointsList, outputTriangulation, outputTriangles, originalImageBitmap);
        }

        private static void Striping(Dictionary<float, Pixel> pivotPoints, float stripingFactor)
        {
            Stripes.Clear();
            int numberOfStripes = (int)Math.Sqrt((stripingFactor * 512 * pivotPoints.Count) / 512);

            // Разбиваем исходный список на полосы
            for (int i = 0; i < numberOfStripes; ++i) {
                Stripes.Add(new List<Pixel>());
            }
            foreach (Pixel p in pivotPoints.Values)
            {
                Stripes[(int)(p.X / (512.0 / numberOfStripes))].Add(p);
            }

            // Сортируем пиксели в полосках
            for (int i = 0; i < numberOfStripes; ++i)
            {
                Stripes[i].Sort(new YComparer());
                if (i % 2 != 0) Stripes[i].Reverse();
            }

            // Склеиваем полоски в один массив
            pivotPointsList.Clear();
            for (int i = 0; i < numberOfStripes; ++i)
            {
                pivotPointsList.AddRange(Stripes[i]);
            }
        }
    }
}
