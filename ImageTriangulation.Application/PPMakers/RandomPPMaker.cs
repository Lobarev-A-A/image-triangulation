using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ImageTriangulation.Applicaton
{
    class RandomPPMaker
    {
        public static void Run(Bitmap sourceImage, HashSet<Pixel> pivotPoints, int numberOfPivotPoints)
        {
            // Записываем стартовые точки триангуляции
            Pixel[] initPixels = { new Pixel(0, 0, sourceImage.GetPixel(0, 0).R),
                                   new Pixel(sourceImage.Width - 1, 0, sourceImage.GetPixel(sourceImage.Width - 1, 0).R),
                                   new Pixel(sourceImage.Width - 1, sourceImage.Height - 1, sourceImage.GetPixel(sourceImage.Width - 1, sourceImage.Height - 1).R),
                                   new Pixel(0, sourceImage.Height - 1, sourceImage.GetPixel(0, sourceImage.Height - 1).R) };
            foreach (Pixel p in initPixels) pivotPoints.Add(p);

            Random rand = new Random();

            while (pivotPoints.Count < numberOfPivotPoints)
            {
                int X = rand.Next(sourceImage.Width);
                int Y = rand.Next(sourceImage.Height);
                pivotPoints.Add(new Pixel(X, Y, sourceImage.GetPixel(X, Y).R));
            }
        }
    }
}
