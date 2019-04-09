using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace image_triangulation
{
    class Triangle
    {
        // Вершины должны быть записаны в порядке по часовой стрелке
        public Pixel[] points = new Pixel[3];
        // Индекс ребра должен соответствовать индексу противолежащей вершины
        public Edge[] edges = new Edge[3];
        // Индекс соседнего треугольника должен соответствовать индексу общего ребра
        public Triangle[] triangles = new Triangle[3];

        // Вершины p1, p2, p3 должны быть записаны в порядке по часовой стрелке
        public Triangle(Pixel p1, Pixel p2, Pixel p3)
        {
            points[0] = p1;
            points[1] = p2;
            points[2] = p3;
        }

        /// <summary>
        /// Возвращает массив вершин как Point[].
        /// </summary>
        /// <returns>Массив вершин.</returns>
        public Point[] Points()
        {
            Point[] points = new Point[3];
            for (int i = 0; i < 3; ++i)
            {
                points[i] = new Point(this.points[i].X, this.points[i].Y);
            }
            return points;
        }
    }
}
