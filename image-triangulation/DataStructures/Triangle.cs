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
        public Point[] points = new Point[3];
        // Индекс ребра должен соответствовать индексу противолежащей вершины
        public Edge[] edges = new Edge[3];
        // Индекс соседнего треугольника должен соответствовать индексу общего ребра
        public Triangle[] triangles = new Triangle[3];

        // Вершины p1, p2, p3 должны быть записаны в порядке по часовой стрелке
        public Triangle(Point p1, Point p2, Point p3)
        {
            points[0] = p1;
            points[1] = p2;
            points[2] = p3;
        }
    }
}
