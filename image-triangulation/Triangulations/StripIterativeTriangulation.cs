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

        public static void Run(List<Pixel> pivotPoints, List<Section> outputTriangulation,
                                             HashSet<Triangle> outputTriangles, Bitmap originalImageBitmap, float stripingFactor)
        {
            Striping(pivotPoints, stripingFactor);
            SimpleIterativeTriangulation.Run(pivotPoints, outputTriangulation, outputTriangles, originalImageBitmap);
        }

        private static void Striping(List<Pixel> pivotPoints, float stripingFactor)
        {
            Stripes.Clear();
            int numberOfStripes = (int)Math.Sqrt((stripingFactor * 512 * pivotPoints.Count) / 512);

            // Разбиваем исходный список на полосы
            for (int i = 0; i < numberOfStripes; ++i) {
                Stripes.Add(new List<Pixel>());
            }
            foreach (Pixel p in pivotPoints)
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
            pivotPoints.Clear();
            for (int i = 0; i < numberOfStripes; ++i)
            {
                pivotPoints.AddRange(Stripes[i]);
            }
        }
    }
}
