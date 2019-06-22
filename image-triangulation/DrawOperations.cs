using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace image_triangulation
{
    static class DrawOperations
    {
        static Pen pen = new Pen(Color.Red);

        public static void PixelsToBitmap(HashSet<Pixel> pointsList, Bitmap bitmap)
        {
            foreach (Pixel p in pointsList)
            {
                bitmap.SetPixel(p.X, p.Y, Color.Red);
            }
        }

        public static void SectionsToBitmap(List<Section> sectionsList, Bitmap bitmap)
        {
            Point a = new Point(0, 0);
            Point b = new Point(0, 0);
            Graphics canvas = Graphics.FromImage(bitmap);

            foreach (Section s in sectionsList)
            {
                a.X = s.a.X;
                a.Y = s.a.Y;
                b.X = s.b.X;
                b.Y = s.b.Y;
                canvas.DrawLine(pen, a, b);
            }

            canvas.Dispose();
        }
    }
}
