using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

// TO DO
// * передавать цвет в методы

namespace image_triangulation
{
    static class DrawOperations
    {
        static Pen pen = new Pen(Color.Red);

        public static void PointsToBitmap(List<Pixel> pointsList, Bitmap bitmap)
        {
            foreach (Pixel p in pointsList)
            {
                bitmap.SetPixel(p.X, p.Y, Color.Red);
            }
        }

        public static void LinesToGraphics(List<Section> sectionsList, Graphics canvas)
        {
            Point a = new Point(0, 0);
            Point b = new Point(0, 0);

            foreach (Section s in sectionsList)
            {
                a.X = s.a.X;
                a.Y = s.a.Y;
                b.X = s.b.X;
                b.Y = s.b.Y;
                canvas.DrawLine(pen, a, b);
            }
        }
    }
}
