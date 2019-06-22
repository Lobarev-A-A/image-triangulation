using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace image_triangulation
{
    class Triangle// : IEquatable<object>
    {
        // Вершины должны быть записаны в порядке по часовой стрелке
        public Pixel[] points = new Pixel[3];
        // Индекс ребра должен соответствовать индексу противолежащей вершины
        public Edge[] edges = new Edge[3];
        // Индекс соседнего треугольника должен соответствовать индексу общего ребра
        public Triangle[] triangles = new Triangle[3];

        public Triangle(Pixel p1, Pixel p2, Pixel p3)
        {
            points[0] = p1;
            points[1] = p2;
            points[2] = p3;
        }

        public Triangle(Pixel[] points, Edge[] edges, Triangle[] triangles)
        {
            this.points = points;
            this.edges = edges;
            this.triangles = triangles;
        }

        /// <summary>
        /// Возвращает массив вершин как массив Point.
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

        /// <summary>
        /// Осуществляет неглубокое копирование объекта.
        /// </summary>
        /// <param name="triangle">Triangle, в который осуществляется копирование.</param>
        public void CopyTo(Triangle triangle)
        {
            if (triangle == null)
            {
                triangle = new Triangle(points, edges, triangles);
            }
            else
            {
                triangle.points = points;
                triangle.edges = edges;
                triangle.triangles = triangles;
            }
        }

        //public override bool Equals(object obj)
        //{
        //    if (obj == null || GetType() != obj.GetType()) return false;

        //    Triangle triangle = obj as Triangle;
        //    if (triangle.points.Contains(points[0]) && triangle.points.Contains(points[1]) && triangle.points.Contains(points[2])) return true;
        //    else return false;
        //}

        //public override int GetHashCode()
        //{
        //    return points[0].GetHashCode() ^ points[1].GetHashCode() ^ points[2].GetHashCode();
        //}
    }
}
