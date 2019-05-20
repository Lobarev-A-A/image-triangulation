using System;
using System.Collections.Generic;
using System.Drawing;

namespace image_triangulation
{
    static class TriangleGradientShader
    {
        private static void Run(Bitmap rebuiltImageBitmap, HashSet<Triangle> triangles)
        {
            //SolidBrush brush;
            //Pen pen;
            //Color color;
            //Graphics rebuiltImageCanvas = Graphics.FromImage(rebuiltImageBitmap);
            double[] barycentricVec = new double[3];

            foreach (Triangle triangle in triangles)
            {
                // Находим описывающий прямоугольник
                int minX = Math.Min(Math.Min(triangle.points[0].X, triangle.points[1].X), triangle.points[2].X);
                int minY = Math.Min(Math.Min(triangle.points[0].Y, triangle.points[1].Y), triangle.points[2].Y);
                int maxX = Math.Max(Math.Max(triangle.points[0].X, triangle.points[1].X), triangle.points[2].X);
                int maxY = Math.Max(Math.Max(triangle.points[0].Y, triangle.points[1].Y), triangle.points[2].Y);

                // Проходим по описывающему прямоугольнику
                for (int i = minY; i <= maxY; ++i)
                {
                    for (int j = minX; j <= maxX; ++j)
                    {
                        BarycentricCoords(j, i, triangle.points[0], triangle.points[1], triangle.points[2], barycentricVec);

                    }
                }

                //averageBrightness = (triangle.points[0].brightness + triangle.points[1].brightness + triangle.points[2].brightness) / 3;
                //color = Color.FromArgb(averageBrightness, averageBrightness, averageBrightness);
                //brush = new SolidBrush(color);
                //pen = new Pen(color);                
            }
        }

        private static void BarycentricCoords(int x, int y, Pixel p1, Pixel p2, Pixel p3, double[] barycentricVec)
        {
            barycentricVec[0] = ((p2.Y - p3.Y) * (x - p3.X) + (p3.X - p2.X) * (y - p3.Y)) / ((p2.Y - p3.Y) * (p1.X - p3.X) + (p3.X - p2.X) * (p1.Y - p3.Y));
            barycentricVec[1] = ((p3.Y - p1.Y) * (x - p3.X) + (p1.X - p3.X) * (y - p3.Y)) / ((p2.Y - p3.Y) * (p1.X - p3.X) + (p3.X - p2.X) * (p1.Y - p3.Y));
            barycentricVec[2] = 1 - barycentricVec[0] - barycentricVec[1];
        }
    }
}
