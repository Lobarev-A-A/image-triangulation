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

        public static void PointsToBitmap(List<Point> pointsList, Bitmap bitmap)
        {
            foreach (Point p in pointsList)
            {
                bitmap.SetPixel(p.X, p.Y, Color.Red);
            }
        }

        public static void LinesToGraphics(List<Section> sectionsList, Graphics canvas)
        {
            foreach (Section s in sectionsList)
            {
                canvas.DrawLine(pen, s.a, s.b);
            }
        }
    }
}
