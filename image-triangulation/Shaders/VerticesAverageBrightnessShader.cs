using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

// TO DO
// * Если возможно, не создавать кисть с новым цветом, а менять его в одной.
namespace image_triangulation
{
    static class VerticesAverageBrightnessShader
    {
        public static void Run(Bitmap rebuiltImageBitmap, HashSet<Triangle> triangles)
        {
            SolidBrush brush;
            Pen pen;
            Color color;
            int averageBrightness;
            Graphics rebuiltImageCanvas = Graphics.FromImage(rebuiltImageBitmap);

            foreach (Triangle triangle in triangles)
            {
                averageBrightness = (triangle.points[0].brightness + triangle.points[1].brightness + triangle.points[2].brightness) / 3;
                color = Color.FromArgb(averageBrightness, averageBrightness, averageBrightness);
                brush = new SolidBrush(color);
                pen = new Pen(color);                
                rebuiltImageCanvas.DrawPolygon(pen, triangle.Points());
                rebuiltImageCanvas.FillPolygon(brush, triangle.Points());                
            }
        }
    }
}
