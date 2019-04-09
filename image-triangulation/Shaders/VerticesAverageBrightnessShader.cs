using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

// TO DO
// * Переделать под работу без исходного изображения (сохранять яркости в узлах триангуляции).
// * Если возможно, не создавать кисть с новым цветом, а менять его в одной.
namespace image_triangulation
{
    static class VerticesAverageBrightnessShader
    {
        public static void Run(Bitmap originalImageBitmap, Bitmap rebuiltImageBitmap, HashSet<Triangle> triangles)
        {
            SolidBrush brush;
            Pen pen;
            Color color;
            float averageBrightness;
            Graphics rebuiltImageCanvas = Graphics.FromImage(rebuiltImageBitmap);

            foreach (Triangle triangle in triangles)
            {
                averageBrightness = (originalImageBitmap.GetPixel(triangle.points[0].X, triangle.points[0].Y).GetBrightness()
                                    + originalImageBitmap.GetPixel(triangle.points[1].X, triangle.points[1].Y).GetBrightness()
                                    + originalImageBitmap.GetPixel(triangle.points[2].X, triangle.points[2].Y).GetBrightness()) / 3;
                color = ColorConversion.ColorFromHSL(0, 0, averageBrightness);
                brush = new SolidBrush(color);
                pen = new Pen(color);                
                rebuiltImageCanvas.DrawPolygon(pen, triangle.Points());
                rebuiltImageCanvas.FillPolygon(brush, triangle.Points());                
            }
        }
    }
}
