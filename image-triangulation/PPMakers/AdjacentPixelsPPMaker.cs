using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace image_triangulation
{
    class AdjacentPixelsPPMaker
    {
        public static void RunPPMaker(Bitmap sourceImage, byte threshold, List<Point> outputPointsList)
        {
            float br1, br2;
            // флаг для определения, осуществлялась ли вставка точек на предыдущей итерации
            bool f = false;

            for (int i = 0; i < sourceImage.Height; ++i)
            {
                for (int j = 0; j < sourceImage.Width - 1; ++j)
                {
                    br1 = sourceImage.GetPixel(j, i).R;
                    br2 = sourceImage.GetPixel(j + 1, i).R;

                    if (Math.Abs(br1 - br2) >= threshold)
                    {
                        if (!f)
                        {
                            outputPointsList.Add(new Point(j, i));
                            f = true;
                        }
                        outputPointsList.Add(new Point(j + 1, i));
                    }
                    else
                    {
                        f = false;
                    }
                }

                f = false;
            }
        }
    }
}
