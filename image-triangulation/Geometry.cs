using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace image_triangulation
{
    static class Geometry
    {
        // Косое произведение векторов
        // po начало координат, p1 – координата первого вектора, p2 – координата второго вектора
        private static int ObliqueProduct(Pixel p0, Pixel p1, Pixel p2)
        {
            return (p1.X - p0.X) * (p2.Y - p0.Y) - (p2.X - p0.X) * (p1.Y - p0.Y);
        }

        // Скалярное произведение векторов
        private static int ScalarProduct(int x1, int y1, int x2, int y2)
        {
            return x1 * x2 + y1 * y2;
        }

        // Проверяет взаимное расположение двух точек относительно прямой, заданной двумя точками
        // возвращает -1 – точки лежат по разные стороны, 1 – по одну сторону, 0 – одна или обе лежат на прямой
        public static int PointsRelativelyStraight(Pixel p1, Pixel p2, Section s)
        {
            return Math.Sign(ObliqueProduct(s.a, s.b, p1)) * Math.Sign(ObliqueProduct(s.a, s.b, p2));
        }

        /// <summary>
        /// Проверяет расположение точки по отношению к треугольнику
        /// </summary>
        /// <remarks>
        /// * Вершины не должны лежать на одной прямой
        /// * Для правильного определения индекса ребра, которому принадлежит точка, номера вершин должны соответствовать индексам протеволежащих рёбер
        /// </remarks>
        /// <param name="p">Проверяемая точка</param>
        /// <param name="t1">Первая вершина треугольника</param>
        /// <param name="t2">Вторая вершина треугольника</param>
        /// <param name="t3">Третяя вершина треугольника</param>
        /// <returns>
        /// * -1 – точка лежит вне треугольника
        /// * 1 – точка лежит внутри треугольника
        /// * 0 – точка принадлежит ребру с индексом 0
        /// * 10 – точка принадлежит ребру с индексом 1
        /// * 20 – точка принадлежит ребру с индексом 2
        /// </returns>
        public static int PointRelativelyTriangle(Pixel p, Pixel t1, Pixel t2, Pixel t3)
        {
            int sign;

            sign = Math.Sign(ObliqueProduct(t1, t2, p)) * Math.Sign(ObliqueProduct(t1, t2, t3));
            if (sign == -1)
            {
                return -1;
            }
            else if (sign == 1)
            {
                sign = Math.Sign(ObliqueProduct(t2, t3, p)) * Math.Sign(ObliqueProduct(t2, t3, t1));
                if (sign == -1)
                {
                    return -1;
                }
                else if (sign == 1)
                {
                    sign = Math.Sign(ObliqueProduct(t3, t1, p)) * Math.Sign(ObliqueProduct(t3, t1, t2));
                    if (sign == -1)
                    {
                        return -1;
                    }
                    else if (sign == 1)
                    {
                        return 1;
                    }
                    else
                    {
                        if (ScalarProduct(t3.X - p.X, t3.Y - p.Y, t1.X - p.X, t1.Y - p.Y) <= 0) return 10;
                        else return -1;
                    }
                }
                else
                {
                    if (ScalarProduct(t2.X - p.X, t2.Y - p.Y, t3.X - p.X, t3.Y - p.Y) <= 0) return 0;
                    else return -1;
                }
            }
            else
            {
                if (ScalarProduct(t1.X - p.X, t1.Y - p.Y, t2.X - p.X, t2.Y - p.Y) <= 0) return 20;
                else return -1;
            }
        }
                
        /// <summary>
        /// Проверяет выполнение условия Делоне
        /// </summary>
        /// <remarks>
        /// Узлы po-p3 должны быть расположены по часовой стрелке
        /// </remarks>
        /// <param name="p0">Узел, проверяемый на вхождение в описанную окружность</param>
        /// <param name="p1">Первая вершина треугольника</param>
        /// <param name="p2">Вторая вершина треугольника</param>
        /// <param name="p3">Третяя вершина треугольника</param>
        /// <returns>
        /// * true – условие Делоне выполняется
        /// * false – условие Делоне не выполняется
        /// </returns>
        public static bool DelaunayCheck(Pixel p0, Pixel p1, Pixel p2, Pixel p3)
        {
            long sa = checked((p0.X - p1.X) * (p0.X - p3.X) + (p0.Y - p1.Y) * (p0.Y - p3.Y));
            long sb = checked((p2.X - p1.X) * (p2.X - p3.X) + (p2.Y - p1.Y) * (p2.Y - p3.Y));

            if ((sa < 0) && (sb < 0))
            {
                return false;
            }
            else if ((sa >= 0) && (sb >= 0))
            {
                return true;
            }
            else
            {
                long ca = checked((p0.X - p1.X) * (p0.Y - p3.Y) - (p0.X - p3.X) * (p0.Y - p1.Y));
                long cb = checked((p2.X - p3.X) * (p2.Y - p1.Y) - (p2.X - p1.X) * (p2.Y - p3.Y));                
                long b = checked(ca * sb + sa * cb);

                if (b >= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        // Проверка, как расположены точки: против часовой стрелки, по часовой стрелке, на одной прямой
        // Возврат: < 0 – против часовой, > 0 – по часовой, 0 – лежат на одной прямой
        public static int ClockwiseCheck(Pixel p1, Pixel p2, Pixel p3)
        {
            return ScalarProduct(p1.Y - p2.Y, p2.X - p1.X, p3.X - p1.X, p3.Y - p1.Y);
        }

        /// <summary>
        /// Возвращает расстояние между точками.
        /// </summary>
        /// <param name="p1">Первая точка.</param>
        /// <param name="p2">Вторая точка.</param>
        /// <returns>Расстояние.</returns>
        public static double Distance(Pixel p1, Pixel p2)
        {
            return Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));
        }
        
        // Проверяет, пересекаются ли отрезки (касающиеся обрабатываются как непересекающиеся)
        public static bool Crosses(Section s1, Section s2)
        {
            return Intersect(s1.a.X, s1.b.X, s2.a.X, s2.b.X)
                    && Intersect(s1.a.Y, s2.b.Y, s2.a.Y, s2.b.Y)
                    && Math.Sign(ObliqueProduct(s1.a, s1.b, s2.a)) * Math.Sign(ObliqueProduct(s1.a, s1.b, s2.b)) == -1
                    && Math.Sign(ObliqueProduct(s2.a, s2.b, s1.a)) * Math.Sign(ObliqueProduct(s2.a, s2.b, s1.b)) == -1;
        }

        private static bool Intersect(int a, int b, int c, int d)
        {
            if (a > b)
            {
                a = a + b;
                b = a - b;
                a = a - b;
            }
            if (c > d)
            {
                c = c + d;
                d = c - d;
                c = c - d;
            }
            return Math.Max(a, c) <= Math.Min(b, d);
        }
    }
}
