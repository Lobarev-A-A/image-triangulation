using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

// TO DO
// * Распространить на изображения произвольного разрешения
// * В DelanayBuilder можно не создавать каждый раз новые bufTriangle
// * Каша с использованием буферных треугольников при создании новых (не использовать буферные для cur и cur2)
namespace image_triangulation
{
    static class DCIterativeTriangulation
    {
        static Triangle curTriangle;
        // Словарь <ссылка на ребро триангуляции, ссылка на соответствующий отрезок во внешнем представлении>
        static Dictionary<Edge, Section> edges = new Dictionary<Edge, Section>();
        static HashSet<Triangle> trianglesForDelaunayCheck = new HashSet<Triangle>();
        static List<List<Triangle>> dynamicCache = new List<List<Triangle>>();

        public static void Run(Dictionary<float, Pixel> pivotPoints, List<Section> outputTriangulation,
                                             HashSet<Triangle> outputTriangles, Bitmap originalImageBitmap, float coefOfCacheExpand)
        {
            Initialization(outputTriangles, originalImageBitmap);
            AddPoints(pivotPoints, outputTriangles, coefOfCacheExpand);
            GetOut(outputTriangulation);
        }

        private static void Initialization(HashSet<Triangle> outputTriangles, Bitmap originalImageBitmap)
        {
            edges.Clear();

            Triangle newTriangle0 = new Triangle(new Pixel(0, 0, originalImageBitmap.GetPixel(0, 0).GetBrightness()),
                                                 new Pixel(511, 0, originalImageBitmap.GetPixel(511, 0).GetBrightness()),
                                                 new Pixel(511, 511, originalImageBitmap.GetPixel(511, 511).GetBrightness()));
            Triangle newTriangle1 = new Triangle(new Pixel(0, 0, originalImageBitmap.GetPixel(0, 0).GetBrightness()),
                                                 new Pixel(511, 511, originalImageBitmap.GetPixel(511, 511).GetBrightness()),
                                                 new Pixel(0, 511, originalImageBitmap.GetPixel(0, 511).GetBrightness()));
            outputTriangles.Add(newTriangle0);
            outputTriangles.Add(newTriangle1);

            // Заполнение нулевого треугольника
            newTriangle0.edges[0] = new Edge();
            edges.Add(newTriangle0.edges[0], new Section(newTriangle0.points[1], newTriangle0.points[2]));
            newTriangle0.edges[1] = new Edge();
            edges.Add(newTriangle0.edges[1], new Section(newTriangle0.points[2], newTriangle0.points[0]));
            newTriangle0.edges[2] = new Edge();
            edges.Add(newTriangle0.edges[2], new Section(newTriangle0.points[0], newTriangle0.points[1]));

            newTriangle0.triangles[0] = null;
            newTriangle0.triangles[1] = newTriangle1;
            newTriangle0.triangles[2] = null;

            // Заполнение первого треугольника
            newTriangle1.edges[0] = new Edge();
            edges.Add(newTriangle1.edges[0], new Section(newTriangle1.points[1], newTriangle1.points[2]));
            newTriangle1.edges[1] = new Edge();
            edges.Add(newTriangle1.edges[1], new Section(newTriangle1.points[2], newTriangle1.points[0]));
            newTriangle1.edges[2] = newTriangle0.edges[1];

            newTriangle1.triangles[0] = null;
            newTriangle1.triangles[1] = null;
            newTriangle1.triangles[2] = newTriangle0;

            // Инициализация кэша
            dynamicCache.Clear();
            dynamicCache.Add(new List<Triangle>());
            dynamicCache.Add(new List<Triangle>());
            dynamicCache[0].Add(newTriangle0);
            dynamicCache[0].Add(newTriangle0);
            dynamicCache[1].Add(newTriangle1);
            dynamicCache[1].Add(newTriangle1);
            curTriangle = newTriangle0;
        }

        /// <summary>
        /// Добавляет узлы в триангуляцию
        /// </summary>
        /// <param name="pivotPoints">Список опорных точек</param>
        private static void AddPoints(Dictionary<float, Pixel> pivotPoints, HashSet<Triangle> outputTriangles, float coefOfCacheExpand)
        {
            // Удаляем из словаря точки, которыми инициировалась триангуляция, если они там есть
            Pixel[] initPixels = { new Pixel(0, 0), new Pixel(0, 511), new Pixel(511, 511), new Pixel(0, 511) };
            for (int i = 0; i < 4; ++i)
            {
                if (pivotPoints.ContainsKey(initPixels[i].GetHashCode())) pivotPoints.Remove(initPixels[i].GetHashCode());
            }

            long numberOfAddedPoints = 4;
            float delta;
            Pixel previousPoint = new Pixel(0, 0);

            foreach (Pixel curPoint in pivotPoints.Values)
            {
                delta = 512 / dynamicCache[0].Count;
                if (Geometry.Distance(previousPoint, curPoint) > delta)
                {
                    // Выбор треугольника из кэша
                    curTriangle = dynamicCache[curPoint.X / (512 / dynamicCache[0].Count)][curPoint.Y / (512 / dynamicCache.Count)];
                }
                previousPoint = curPoint;

                int i;
                // Цикл локализации точки (пока точка не попадёт в текущий треугольник либо на его границу)
                int prtCheck = Geometry.PointRelativelyTriangle(curPoint, curTriangle.points[0], curTriangle.points[1], curTriangle.points[2]);
                while (prtCheck == -1)
                {
                    i = 0;
                    while (Geometry.PointsRelativelyStraight(curTriangle.points[i], curPoint, edges[curTriangle.edges[i]]) != -1) ++i;
                    curTriangle = curTriangle.triangles[i];

                    prtCheck = Geometry.PointRelativelyTriangle(curPoint, curTriangle.points[0], curTriangle.points[1], curTriangle.points[2]);
                }

                // Обновление кэша
                dynamicCache[curPoint.X / (512 / dynamicCache[0].Count)][curPoint.Y / (512 / dynamicCache.Count)] = curTriangle;

                // Если точка внутри треугольника
                if (prtCheck == 1)
                {
                    Triangle newTriangle0 = new Triangle(curTriangle.points[1], curTriangle.points[2], curPoint);
                    Triangle newTriangle1 = new Triangle(curTriangle.points[2], curTriangle.points[0], curPoint);
                    Triangle newTriangle2 = new Triangle(curTriangle.points[0], curTriangle.points[1], curPoint);

                    // Заполнение нулевого треугольника
                    newTriangle0.edges[0] = new Edge();
                    edges.Add(newTriangle0.edges[0], new Section(newTriangle0.points[1], newTriangle0.points[2]));
                    newTriangle0.edges[1] = new Edge();
                    edges.Add(newTriangle0.edges[1], new Section(newTriangle0.points[2], newTriangle0.points[0]));
                    newTriangle0.edges[2] = curTriangle.edges[0];

                    newTriangle0.triangles[0] = newTriangle1;
                    newTriangle0.triangles[1] = newTriangle2;
                    newTriangle0.triangles[2] = curTriangle.triangles[0];

                    i = 0;
                    if (newTriangle0.triangles[2] != null)
                    {
                        while (newTriangle0.triangles[2].edges[i] != newTriangle0.edges[2]) ++i;
                        newTriangle0.triangles[2].triangles[i] = curTriangle;
                    }

                    // Заполнение первого треугольника
                    newTriangle1.edges[0] = new Edge();
                    edges.Add(newTriangle1.edges[0], new Section(newTriangle1.points[1], newTriangle1.points[2]));
                    newTriangle1.edges[1] = newTriangle0.edges[0];
                    newTriangle1.edges[2] = curTriangle.edges[1];

                    newTriangle1.triangles[0] = newTriangle2;
                    newTriangle1.triangles[1] = curTriangle;
                    newTriangle1.triangles[2] = curTriangle.triangles[1];

                    i = 0;
                    if (newTriangle1.triangles[2] != null)
                    {
                        while (newTriangle1.triangles[2].edges[i] != newTriangle1.edges[2]) ++i;
                        newTriangle1.triangles[2].triangles[i] = newTriangle1;
                    }

                    // Заполнение второго треугольника
                    newTriangle2.edges[0] = newTriangle0.edges[1];
                    newTriangle2.edges[1] = newTriangle1.edges[0];
                    newTriangle2.edges[2] = curTriangle.edges[2];

                    newTriangle2.triangles[0] = curTriangle;
                    newTriangle2.triangles[1] = newTriangle1;
                    newTriangle2.triangles[2] = curTriangle.triangles[2];

                    i = 0;
                    if (newTriangle2.triangles[2] != null)
                    {
                        while (newTriangle2.triangles[2].edges[i] != newTriangle2.edges[2]) ++i;
                        newTriangle2.triangles[2].triangles[i] = newTriangle2;
                    }

                    newTriangle0.CopyTo(curTriangle);
                    outputTriangles.Add(newTriangle1);
                    outputTriangles.Add(newTriangle2);
                    trianglesForDelaunayCheck.Add(curTriangle);
                    trianglesForDelaunayCheck.Add(newTriangle1);
                    trianglesForDelaunayCheck.Add(newTriangle2);
                }

                // Если точка на ребре
                else
                {
                    int e = prtCheck / 10;  // Индекс ребра, на котором оказалась точка
                    switch (prtCheck)
                    {
                        case 0:
                            e = 0;
                            break;
                        case 10:
                            e = 1;
                            break;
                        case 20:
                            e = 2;
                            break;
                        default:
                            e = 3;  // Дальше выпадет ошибка
                            break;
                    }

                    // Если точка на границе изображения
                    if (curTriangle.triangles[e] == null)
                    {
                        Triangle newTriangle0 = new Triangle(curTriangle.points[(e + 2) % 3], curTriangle.points[e], curPoint);
                        Triangle newTriangle1 = new Triangle(curTriangle.points[e], curTriangle.points[(e + 1) % 3], curPoint);

                        // Заполнение нулевого треугольника
                        newTriangle0.edges[0] = new Edge();
                        edges.Add(newTriangle0.edges[0], new Section(newTriangle0.points[1], newTriangle0.points[2]));
                        newTriangle0.edges[1] = new Edge();
                        edges.Add(newTriangle0.edges[1], new Section(newTriangle0.points[0], newTriangle0.points[2]));
                        newTriangle0.edges[2] = curTriangle.edges[(e + 1) % 3];

                        newTriangle0.triangles[0] = newTriangle1;
                        newTriangle0.triangles[1] = null;
                        newTriangle0.triangles[2] = curTriangle.triangles[(e + 1) % 3];

                        i = 0;
                        if (newTriangle0.triangles[2] != null)
                        {
                            while (newTriangle0.edges[2] != newTriangle0.triangles[2].edges[i]) ++i;
                            newTriangle0.triangles[2].triangles[i] = curTriangle;
                        }

                        // Заполнение первого треугольника
                        newTriangle1.edges[0] = new Edge();
                        edges.Add(newTriangle1.edges[0], new Section(newTriangle1.points[1], newTriangle1.points[2]));
                        newTriangle1.edges[1] = newTriangle0.edges[0];
                        newTriangle1.edges[2] = curTriangle.edges[(e + 2) % 3];

                        newTriangle1.triangles[0] = null;
                        newTriangle1.triangles[1] = curTriangle;
                        newTriangle1.triangles[2] = curTriangle.triangles[(e + 2) % 3];

                        i = 0;
                        if (newTriangle1.triangles[2] != null)
                        {
                            while (newTriangle1.edges[2] != newTriangle1.triangles[2].edges[i]) ++i;
                            newTriangle1.triangles[2].triangles[i] = newTriangle1;
                        }

                        edges.Remove(curTriangle.edges[e]);

                        newTriangle0.CopyTo(curTriangle);
                        outputTriangles.Add(newTriangle1);
                        trianglesForDelaunayCheck.Add(curTriangle);
                        trianglesForDelaunayCheck.Add(newTriangle1);
                    }

                    // Точка не на границе изображения
                    else
                    {
                        Triangle curTriangle2 = curTriangle.triangles[e];
                        int nEI = 0; // Индекс ребра, которому принадлежит точка, для смежного треугольника
                        while (curTriangle.edges[e] != curTriangle2.edges[nEI]) ++nEI;

                        Triangle newTriangle0 = new Triangle(curTriangle.points[(e + 2) % 3], curTriangle.points[e], curPoint);
                        Triangle newTriangle1 = new Triangle(curTriangle.points[e], curTriangle.points[(e + 1) % 3], curPoint);
                        Triangle newTriangle2 = new Triangle(curTriangle2.points[(nEI + 2) % 3], curTriangle2.points[nEI], curPoint);
                        Triangle newTriangle3 = new Triangle(curTriangle2.points[nEI], curTriangle2.points[(nEI + 1) % 3], curPoint);

                        // Заполнение нулевого треугольника
                        newTriangle0.edges[0] = new Edge();
                        edges.Add(newTriangle0.edges[0], new Section(newTriangle0.points[1], newTriangle0.points[2]));
                        newTriangle0.edges[1] = new Edge();
                        edges.Add(newTriangle0.edges[1], new Section(newTriangle0.points[0], newTriangle0.points[2]));
                        newTriangle0.edges[2] = curTriangle.edges[(e + 1) % 3];

                        newTriangle0.triangles[0] = curTriangle2;
                        newTriangle0.triangles[1] = newTriangle3;
                        newTriangle0.triangles[2] = curTriangle.triangles[(e + 1) % 3];

                        i = 0;
                        if (newTriangle0.triangles[2] != null)
                        {
                            while (newTriangle0.edges[2] != newTriangle0.triangles[2].edges[i]) ++i;
                            newTriangle0.triangles[2].triangles[i] = curTriangle;
                        }

                        // Заполнение первого треугольника
                        newTriangle1.edges[0] = new Edge();
                        edges.Add(newTriangle1.edges[0], new Section(newTriangle1.points[2], newTriangle1.points[1]));
                        newTriangle1.edges[1] = newTriangle0.edges[0];
                        newTriangle1.edges[2] = curTriangle.edges[(e + 2) % 3];

                        newTriangle1.triangles[0] = newTriangle2;
                        newTriangle1.triangles[1] = curTriangle;
                        newTriangle1.triangles[2] = curTriangle.triangles[(e + 2) % 3];

                        i = 0;
                        if (newTriangle1.triangles[2] != null)
                        {
                            while (newTriangle1.edges[2] != newTriangle1.triangles[2].edges[i]) ++i;
                            newTriangle1.triangles[2].triangles[i] = curTriangle2;
                        }

                        // Заполнение второго треугольника
                        newTriangle2.edges[0] = new Edge();
                        edges.Add(newTriangle2.edges[0], new Section(newTriangle2.points[1], newTriangle2.points[2]));
                        newTriangle2.edges[1] = newTriangle1.edges[0];
                        newTriangle2.edges[2] = curTriangle2.edges[(nEI + 1) % 3];

                        newTriangle2.triangles[0] = newTriangle3;
                        newTriangle2.triangles[1] = curTriangle2;
                        newTriangle2.triangles[2] = curTriangle2.triangles[(nEI + 1) % 3];

                        i = 0;
                        if (newTriangle2.triangles[2] != null)
                        {
                            while (newTriangle2.edges[2] != newTriangle2.triangles[2].edges[i]) ++i;
                            newTriangle2.triangles[2].triangles[i] = newTriangle2;
                        }

                        // Заполнение третьего треугольника
                        newTriangle3.edges[0] = newTriangle0.edges[1];
                        newTriangle3.edges[1] = newTriangle2.edges[0];
                        newTriangle3.edges[2] = curTriangle2.edges[(nEI + 2) % 3];

                        newTriangle3.triangles[0] = curTriangle;
                        newTriangle3.triangles[1] = newTriangle2;
                        newTriangle3.triangles[2] = curTriangle2.triangles[(nEI + 2) % 3];

                        i = 0;
                        if (newTriangle3.triangles[2] != null)
                        {
                            while (newTriangle3.edges[2] != newTriangle3.triangles[2].edges[i]) ++i;
                            newTriangle3.triangles[2].triangles[i] = newTriangle3;
                        }

                        edges.Remove(curTriangle.edges[e]);
                        edges.Remove(curTriangle2.edges[nEI]);

                        newTriangle0.CopyTo(curTriangle);
                        newTriangle1.CopyTo(curTriangle2);
                        outputTriangles.Add(newTriangle2);
                        outputTriangles.Add(newTriangle3);
                        trianglesForDelaunayCheck.Add(curTriangle);
                        trianglesForDelaunayCheck.Add(curTriangle2);
                        trianglesForDelaunayCheck.Add(newTriangle2);
                        trianglesForDelaunayCheck.Add(newTriangle3);
                    }
                }
                numberOfAddedPoints += 1;

                CacheExpander(numberOfAddedPoints, coefOfCacheExpand);
                DelaunayBuilder(outputTriangles);
            }
        }

        /// <summary>
        /// Проверяет все треугольники из множества <c>trianglesForDelaunayCheck</c> на соответствию условию Делоне и, в случае необходимости,
        /// производит перестроение треугольников
        /// </summary>
        private static void DelaunayBuilder(HashSet<Triangle> outputTriangles)
        {
            while (trianglesForDelaunayCheck.Count() > 0)
            {
                Triangle checkedTriangle = trianglesForDelaunayCheck.Last();
                trianglesForDelaunayCheck.Remove(checkedTriangle);
                Triangle checkedTriangle2;
                int j;  // Хранит индекс ребра, общего для проверяемых треугольников

                for (int i = 0; i < 3; ++i)
                {
                    if (checkedTriangle.triangles[i] != null)
                    {
                        j = 0;
                        while (checkedTriangle.edges[i] != checkedTriangle.triangles[i].edges[j]) ++j;

                        // Если треугольники не соответствуют условию
                        if (!Geometry.DelaunayCheck(checkedTriangle.triangles[i].points[j], checkedTriangle.points[(i + 2) % 3], checkedTriangle.points[i], checkedTriangle.points[(i + 1) % 3]))
                        {
                            checkedTriangle2 = checkedTriangle.triangles[i];
                            if (trianglesForDelaunayCheck.Contains(checkedTriangle2)) trianglesForDelaunayCheck.Remove(checkedTriangle2);
                            Triangle bufTriangle0 = new Triangle(checkedTriangle.points[i], checkedTriangle2.points[j], checkedTriangle.points[(i + 2) % 3]);
                            Triangle bufTriangle1 = new Triangle(checkedTriangle.points[i], checkedTriangle.points[(i + 1) % 3], checkedTriangle2.points[j]);
                            int k;

                            // Записываем нулевой треугольник
                            bufTriangle0.edges[0] = checkedTriangle2.edges[(j + 2) % 3];
                            bufTriangle0.edges[1] = checkedTriangle.edges[(i + 1) % 3];
                            bufTriangle0.edges[2] = new Edge();
                            edges.Add(bufTriangle0.edges[2], new Section(bufTriangle0.points[0], bufTriangle0.points[1]));

                            bufTriangle0.triangles[0] = checkedTriangle2.triangles[(j + 2) % 3];
                            bufTriangle0.triangles[1] = checkedTriangle.triangles[(i + 1) % 3];
                            bufTriangle0.triangles[2] = checkedTriangle2;

                            k = 0;
                            if (bufTriangle0.triangles[0] != null)
                            {
                                while (bufTriangle0.edges[0] != bufTriangle0.triangles[0].edges[k]) ++k;
                                bufTriangle0.triangles[0].triangles[k] = checkedTriangle;
                            }
                            k = 0;
                            if (bufTriangle0.triangles[1] != null)
                            {
                                while (bufTriangle0.edges[1] != bufTriangle0.triangles[1].edges[k]) ++k;
                                bufTriangle0.triangles[1].triangles[k] = checkedTriangle;
                            }

                            // Записываем первый треугольник
                            bufTriangle1.edges[0] = checkedTriangle2.edges[(j + 1) % 3];
                            bufTriangle1.edges[1] = bufTriangle0.edges[2];
                            bufTriangle1.edges[2] = checkedTriangle.edges[(i + 2) % 3];

                            bufTriangle1.triangles[0] = checkedTriangle2.triangles[(j + 1) % 3];
                            bufTriangle1.triangles[1] = checkedTriangle;
                            bufTriangle1.triangles[2] = checkedTriangle.triangles[(i + 2) % 3];

                            k = 0;
                            if (bufTriangle1.triangles[0] != null)
                            {
                                while (bufTriangle1.edges[0] != bufTriangle1.triangles[0].edges[k]) ++k;
                                bufTriangle1.triangles[0].triangles[k] = checkedTriangle2;
                            }
                            k = 0;
                            if (bufTriangle1.triangles[2] != null)
                            {
                                while (bufTriangle1.edges[2] != bufTriangle1.triangles[2].edges[k]) ++k;
                                bufTriangle1.triangles[2].triangles[k] = checkedTriangle2;
                            }

                            edges.Remove(checkedTriangle.edges[i]);

                            bufTriangle0.CopyTo(checkedTriangle);
                            bufTriangle1.CopyTo(checkedTriangle2);
                            trianglesForDelaunayCheck.Add(checkedTriangle);
                            trianglesForDelaunayCheck.Add(checkedTriangle2);

                            break;
                        }
                    }
                }
            }
        }

        private static void CacheExpander(long numberOfAddedPoints, float coefOfCacheExpand)
        {
            if (numberOfAddedPoints > coefOfCacheExpand * Math.Pow(dynamicCache.Count * dynamicCache[0].Count, 2))
            {
                // Создаём пустой новый кеш
                List<List<Triangle>> newCache = new List<List<Triangle>>();
                for (int i = 0; i < dynamicCache.Count * 2; ++i)
                {
                    newCache.Add(new List<Triangle>());
                    for (int j = 0; j < dynamicCache[0].Count * 2; ++j)
                    {
                        newCache[i].Add(null);
                    }
                }

                // Заполняем новый кеш
                for (int i = 0; i < dynamicCache.Count; ++i)
                {
                    for (int j = 0; j < dynamicCache[0].Count; ++j)
                    {
                        newCache[i * 2][j * 2] = dynamicCache[i][j];
                        newCache[i * 2 + 1][j * 2] = dynamicCache[i][j];
                        newCache[i * 2][j * 2 + 1] = dynamicCache[i][j];
                        newCache[i * 2 + 1][j * 2 + 1] = dynamicCache[i][j];
                    }
                }

                dynamicCache = newCache;
            }
        }

        /// <summary>
        /// Записывает все значения словаря <c>edges</c> в <c>outputTriangulation</c>
        /// </summary>
        /// <param name="outputTriangulation">Список, в который производится копирование значений словаря <c>edges</c></param>
        private static void GetOut(List<Section> outputTriangulation)
        {
            foreach (Section s in edges.Values)
            {
                outputTriangulation.Add(s);
            }
        }
    }
}
