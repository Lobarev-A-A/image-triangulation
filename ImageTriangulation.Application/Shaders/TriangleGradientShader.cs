﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace ImageTriangulation.Applicaton
{
    class TriangleGradientShader
    {
        #region Fields
        private Bitmap rebuiltImage;
        private HashSet<Triangle> triangles;
        #endregion

        public TriangleGradientShader(Bitmap rebuiltImage, HashSet<Triangle> triangles)
        {
            this.rebuiltImage = rebuiltImage;
            this.triangles = triangles;
        }

        public void Run()
        {
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
                        double[] barycentricVec = BarycentricCoords(j, i, triangle.points[0], triangle.points[1], triangle.points[2]);

                        if ((barycentricVec[0] >= 0) && (barycentricVec[0] <= 1) &&
                            (barycentricVec[1] >= 0) && (barycentricVec[1] <= 1) &&
                            (barycentricVec[2] >= 0) && (barycentricVec[2] <= 1))
                        {
                            // Вычисление цвета пикселя
                            int brightness = (int)(barycentricVec[0] * triangle.points[0].brightness +
                                                   barycentricVec[1] * triangle.points[1].brightness +
                                                   barycentricVec[2] * triangle.points[2].brightness);
                            Color color = Color.FromArgb(brightness, brightness, brightness);
                            rebuiltImage.SetPixel(j, i, color);
                        }
                    }
                }  
            }
        }
        
        public void ParallelRun()
        {
            object locker = new object();
            Parallel.ForEach(
                triangles,
                () => new ParallelLocalData(),
                (triangle, state, local) =>
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
                            double[] barycentricVec = BarycentricCoords(j, i, triangle.points[0], triangle.points[1], triangle.points[2]);

                            if ((barycentricVec[0] >= 0) && (barycentricVec[0] <= 1) &&
                                (barycentricVec[1] >= 0) && (barycentricVec[1] <= 1) &&
                                (barycentricVec[2] >= 0) && (barycentricVec[2] <= 1))
                            {
                                // Вычисление цвета пикселя
                                byte brightness = (byte)(barycentricVec[0] * triangle.points[0].brightness +
                                                         barycentricVec[1] * triangle.points[1].brightness +
                                                         barycentricVec[2] * triangle.points[2].brightness);

                                local.shadedTriangle.Add(new Pixel(j, i, brightness));
                            }
                        }
                    }

                    return local;
                },
                (local) => {
                    lock (locker)
                    {
                        foreach(Pixel p in local.shadedTriangle) rebuiltImage.SetPixel(p.X, p.Y, Color.FromArgb(p.brightness, p.brightness, p.brightness));
                    }
                }
            );
        }

        class ParallelLocalData
        {
            public List<Pixel> shadedTriangle; 

            public ParallelLocalData()
            {
                shadedTriangle = new List<Pixel>();
            }
        }

        private double[] BarycentricCoords(int x, int y, Pixel p1, Pixel p2, Pixel p3)
        {
            double[] barycentricVec = new double[3];
            barycentricVec[0] = ((p2.Y - p3.Y) * (x - p3.X) + (p3.X - p2.X) * (y - p3.Y)) / (double)((p2.Y - p3.Y) * (p1.X - p3.X) + (p3.X - p2.X) * (p1.Y - p3.Y));
            barycentricVec[1] = ((p3.Y - p1.Y) * (x - p3.X) + (p1.X - p3.X) * (y - p3.Y)) / (double)((p2.Y - p3.Y) * (p1.X - p3.X) + (p3.X - p2.X) * (p1.Y - p3.Y));
            barycentricVec[2] = 1 - barycentricVec[0] - barycentricVec[1];
            return barycentricVec;
        }
    }
}
