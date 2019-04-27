﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

// TO DO
// * Хранить точки внутри Edge, чтобы не костылить дополнительный Dictionary
// * При проверке прямой не обязательно предварительно считать всю прямую
namespace image_triangulation
{
    static class SearchInTriangle
    {
        static HashSet<Triangle> trianglesForSearchPoints = new HashSet<Triangle>();
        static HashSet<Triangle> trianglesForDelaunayCheck = new HashSet<Triangle>();
        static HashSet<Pixel> pivotPoints = new HashSet<Pixel>(); 
        static HashSet<Pixel> newPoints = new HashSet<Pixel>(); // Добавлять без проверок, потом взять разницу между множествами
        static Dictionary<Edge, Section> edges = new Dictionary<Edge, Section>();
        static Triangle curTriangle;

        public static void Run(Bitmap sourceImageBitmap, float threshold, HashSet<Pixel> outerPivotPoints,
                               List<Section> triangulationSectionsList, HashSet<Triangle> trianglesHashSet)
        {
            Initialization(trianglesHashSet, sourceImageBitmap);
            SearchAndAdd(sourceImageBitmap, threshold, triangulationSectionsList, trianglesHashSet);
            GetOut(triangulationSectionsList, outerPivotPoints);
        }

        private static void Initialization(HashSet<Triangle> outputTriangles, Bitmap sourceImageBitmap)
        {
            edges.Clear();
            pivotPoints.Clear();
            newPoints.Clear();

            // Добавляем в список стартовые точки
            newPoints.Add(new Pixel(0, 0, sourceImageBitmap.GetPixel(0, 0).GetBrightness()));
            newPoints.Add(new Pixel(sourceImageBitmap.Width - 1, 0, sourceImageBitmap.GetPixel(sourceImageBitmap.Width - 1, 0).GetBrightness()));
            newPoints.Add(new Pixel(sourceImageBitmap.Width - 1, sourceImageBitmap.Height - 1, sourceImageBitmap.GetPixel(sourceImageBitmap.Width - 1, sourceImageBitmap.Height - 1).GetBrightness()));
            newPoints.Add(new Pixel(0, sourceImageBitmap.Height - 1, sourceImageBitmap.GetPixel(0, sourceImageBitmap.Height - 1).GetBrightness()));

            // Создаём стартовые треугольники
            Triangle newTriangle0 = new Triangle(newPoints[0], newPoints[1], newPoints[2]);
            Triangle newTriangle1 = new Triangle(newPoints[0], newPoints[2], newPoints[3]);
            outputTriangles.Add(newTriangle0);
            outputTriangles.Add(newTriangle1);
            trianglesForSearchPoints.Add(newTriangle0);
            trianglesForSearchPoints.Add(newTriangle1);

            // Переписываем стартовые точки в общую таблицу
            foreach (Pixel p in newPoints) pivotPoints.Add(p);
            newPoints.Clear();

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
        }

        private static void SearchAndAdd(Bitmap sourceImageBitmap, float threshold, List<Section> triangulationSectionsList, HashSet<Triangle> trianglesHashSet)
        {
            while (trianglesForSearchPoints.Count != 0)
            {
                SearchPoints(sourceImageBitmap, threshold);
                AddPoints(pivotPoints, trianglesHashSet);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceImageBitmap"></param>
        /// <param name="threshold"></param>
        /// <returns>Количество найденных точек.</returns>
        private static void SearchPoints(Bitmap sourceImageBitmap, float threshold)
        {            
            foreach (Triangle triangle in trianglesForSearchPoints)
            {
                for (int i = 0; i < 3; ++i)
                {
                    Pixel middlePixel = Geometry.MiddlePixel(triangle.points[(i + 1) % 3], triangle.points[(i + 2) % 3]);
                    
                    List<Pixel> straightForCheking = Geometry.Bresenham(triangle.points[i], middlePixel, sourceImageBitmap);
                    if (straightForCheking != null)
                    {
                        foreach (Pixel pixel in straightForCheking)
                        {
                            if (Math.Abs(triangle.points[i].brightness - pixel.brightness) > threshold && !pivotPoints.Contains(pixel))
                            {
                                pivotPoints.Add(pixel);
                                break;
                            }
                        }
                    }
                }
            }
            curTriangle = trianglesForSearchPoints.First();
            trianglesForSearchPoints.Clear();
        }

        /// <summary>
        /// Добавляет узлы в триангуляцию
        /// </summary>
        /// <param name="pivotPoints">Список опорных точек</param>
        private static void AddPoints(List<Pixel> pivotPoints, HashSet<Triangle> outputTriangles)
        {
            foreach (Pixel curPoint in pivotPoints.Skip(addedPoints))
            {
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

                // Если точка внутри треугольника
                if (prtCheck == 1)
                {
                    Triangle newTriangle0 = new Triangle(curTriangle.points[1], curTriangle.points[2], curPoint);
                    Triangle newTriangle1 = new Triangle(curTriangle.points[2], curTriangle.points[0], curPoint);
                    Triangle newTriangle2 = new Triangle(curTriangle.points[0], curTriangle.points[1], curPoint);
                    outputTriangles.Add(newTriangle0);
                    outputTriangles.Add(newTriangle1);
                    outputTriangles.Add(newTriangle2);
                    outputTriangles.Remove(curTriangle);

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
                        newTriangle0.triangles[2].triangles[i] = newTriangle0;
                    }

                    // Заполнение первого треугольника
                    newTriangle1.edges[0] = new Edge();
                    edges.Add(newTriangle1.edges[0], new Section(newTriangle1.points[1], newTriangle1.points[2]));
                    newTriangle1.edges[1] = newTriangle0.edges[0];
                    newTriangle1.edges[2] = curTriangle.edges[1];

                    newTriangle1.triangles[0] = newTriangle2;
                    newTriangle1.triangles[1] = newTriangle0;
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

                    newTriangle2.triangles[0] = newTriangle0;
                    newTriangle2.triangles[1] = newTriangle1;
                    newTriangle2.triangles[2] = curTriangle.triangles[2];

                    i = 0;
                    if (newTriangle2.triangles[2] != null)
                    {
                        while (newTriangle2.triangles[2].edges[i] != newTriangle2.edges[2]) ++i;
                        newTriangle2.triangles[2].triangles[i] = newTriangle2;
                    }

                    // Заносим три новых треугольника в множество для проверки условия Делоне
                    trianglesForDelaunayCheck.Add(newTriangle0);
                    trianglesForDelaunayCheck.Add(newTriangle1);
                    trianglesForDelaunayCheck.Add(newTriangle2);

                    curTriangle = newTriangle2;
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
                        outputTriangles.Add(newTriangle0);
                        outputTriangles.Add(newTriangle1);
                        outputTriangles.Remove(curTriangle);

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
                            newTriangle0.triangles[2].triangles[i] = newTriangle0;
                        }

                        // Заполнение первого треугольника
                        newTriangle1.edges[0] = new Edge();
                        edges.Add(newTriangle1.edges[0], new Section(newTriangle1.points[1], newTriangle1.points[2]));
                        newTriangle1.edges[1] = newTriangle0.edges[0];
                        newTriangle1.edges[2] = curTriangle.edges[(e + 2) % 3];

                        newTriangle1.triangles[0] = null;
                        newTriangle1.triangles[1] = newTriangle0;
                        newTriangle1.triangles[2] = curTriangle.triangles[(e + 2) % 3];

                        i = 0;
                        if (newTriangle1.triangles[2] != null)
                        {
                            while (newTriangle1.edges[2] != newTriangle1.triangles[2].edges[i]) ++i;
                            newTriangle1.triangles[2].triangles[i] = newTriangle1;
                        }

                        // Заносим новые треугольники в множество для проверки условия Делоне
                        trianglesForDelaunayCheck.Add(newTriangle0);
                        trianglesForDelaunayCheck.Add(newTriangle1);

                        edges.Remove(curTriangle.edges[e]);
                        curTriangle = newTriangle1;
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
                        outputTriangles.Add(newTriangle0);
                        outputTriangles.Add(newTriangle1);
                        outputTriangles.Add(newTriangle2);
                        outputTriangles.Add(newTriangle3);
                        outputTriangles.Remove(curTriangle);
                        outputTriangles.Remove(curTriangle2);

                        // Заполнение нулевого треугольника
                        newTriangle0.edges[0] = new Edge();
                        edges.Add(newTriangle0.edges[0], new Section(newTriangle0.points[1], newTriangle0.points[2]));
                        newTriangle0.edges[1] = new Edge();
                        edges.Add(newTriangle0.edges[1], new Section(newTriangle0.points[0], newTriangle0.points[2]));
                        newTriangle0.edges[2] = curTriangle.edges[(e + 1) % 3];

                        newTriangle0.triangles[0] = newTriangle1;
                        newTriangle0.triangles[1] = newTriangle3;
                        newTriangle0.triangles[2] = curTriangle.triangles[(e + 1) % 3];

                        i = 0;
                        if (newTriangle0.triangles[2] != null)
                        {
                            while (newTriangle0.edges[2] != newTriangle0.triangles[2].edges[i]) ++i;
                            newTriangle0.triangles[2].triangles[i] = newTriangle0;
                        }

                        // Заполнение первого треугольника
                        newTriangle1.edges[0] = new Edge();
                        edges.Add(newTriangle1.edges[0], new Section(newTriangle1.points[2], newTriangle1.points[1]));
                        newTriangle1.edges[1] = newTriangle0.edges[0];
                        newTriangle1.edges[2] = curTriangle.edges[(e + 2) % 3];

                        newTriangle1.triangles[0] = newTriangle2;
                        newTriangle1.triangles[1] = newTriangle0;
                        newTriangle1.triangles[2] = curTriangle.triangles[(e + 2) % 3];

                        i = 0;
                        if (newTriangle1.triangles[2] != null)
                        {
                            while (newTriangle1.edges[2] != newTriangle1.triangles[2].edges[i]) ++i;
                            newTriangle1.triangles[2].triangles[i] = newTriangle1;
                        }

                        // Заполнение второго треугольника
                        newTriangle2.edges[0] = new Edge();
                        edges.Add(newTriangle2.edges[0], new Section(newTriangle2.points[1], newTriangle2.points[2]));
                        newTriangle2.edges[1] = newTriangle1.edges[0];
                        newTriangle2.edges[2] = curTriangle2.edges[(nEI + 1) % 3];

                        newTriangle2.triangles[0] = newTriangle3;
                        newTriangle2.triangles[1] = newTriangle1;
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

                        newTriangle3.triangles[0] = newTriangle0;
                        newTriangle3.triangles[1] = newTriangle2;
                        newTriangle3.triangles[2] = curTriangle2.triangles[(nEI + 2) % 3];

                        i = 0;
                        if (newTriangle3.triangles[2] != null)
                        {
                            while (newTriangle3.edges[2] != newTriangle3.triangles[2].edges[i]) ++i;
                            newTriangle3.triangles[2].triangles[i] = newTriangle3;
                        }

                        // Заносим новые треугольники в множество для проверки условия Делоне
                        trianglesForDelaunayCheck.Add(newTriangle0);
                        trianglesForDelaunayCheck.Add(newTriangle1);
                        trianglesForDelaunayCheck.Add(newTriangle2);
                        trianglesForDelaunayCheck.Add(newTriangle3);

                        edges.Remove(curTriangle.edges[e]);
                        curTriangle = newTriangle3;
                    }
                }

                // Проверки условия Делоне и перестроение
                DelaunayBuilder(outputTriangles);
            }
        }

        /// <summary>
        /// Проверяет все треугольники из множества <c>trianglesForDelaunayCheck</c> на соответствию условию Делоне и, в случае необходимости,
        /// производит перестроение треугольников
        /// </summary>
        private static void DelaunayBuilder(HashSet<Triangle> outputTriangles)
        {
            foreach (Triangle triangle in trianglesForDelaunayCheck) trianglesForSearchPoints.Add(triangle);

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
                            Triangle newTriangle0 = new Triangle(checkedTriangle.points[i], checkedTriangle2.points[j], checkedTriangle.points[(i + 2) % 3]);
                            Triangle newTriangle1 = new Triangle(checkedTriangle.points[i], checkedTriangle.points[(i + 1) % 3], checkedTriangle2.points[j]);
                            outputTriangles.Add(newTriangle0);
                            outputTriangles.Add(newTriangle1);
                            outputTriangles.Remove(checkedTriangle);
                            outputTriangles.Remove(checkedTriangle2);
                            trianglesForSearchPoints.Remove(checkedTriangle);
                            trianglesForSearchPoints.Add(newTriangle0);
                            trianglesForSearchPoints.Add(newTriangle1);

                            int k;

                            // Записываем нулевой треугольник
                            newTriangle0.edges[0] = checkedTriangle2.edges[(j + 2) % 3];
                            newTriangle0.edges[1] = checkedTriangle.edges[(i + 1) % 3];
                            newTriangle0.edges[2] = new Edge();
                            edges.Add(newTriangle0.edges[2], new Section(newTriangle0.points[0], newTriangle0.points[1]));

                            newTriangle0.triangles[0] = checkedTriangle2.triangles[(j + 2) % 3];
                            newTriangle0.triangles[1] = checkedTriangle.triangles[(i + 1) % 3];
                            newTriangle0.triangles[2] = newTriangle1;

                            k = 0;
                            if (newTriangle0.triangles[0] != null)
                            {
                                while (newTriangle0.edges[0] != newTriangle0.triangles[0].edges[k]) ++k;
                                newTriangle0.triangles[0].triangles[k] = newTriangle0;
                            }
                            k = 0;
                            if (newTriangle0.triangles[1] != null)
                            {
                                while (newTriangle0.edges[1] != newTriangle0.triangles[1].edges[k]) ++k;
                                newTriangle0.triangles[1].triangles[k] = newTriangle0;
                            }

                            // Записываем первый треугольник
                            newTriangle1.edges[0] = checkedTriangle2.edges[(j + 1) % 3];
                            newTriangle1.edges[1] = newTriangle0.edges[2];
                            newTriangle1.edges[2] = checkedTriangle.edges[(i + 2) % 3];

                            newTriangle1.triangles[0] = checkedTriangle2.triangles[(j + 1) % 3];
                            newTriangle1.triangles[1] = newTriangle0;
                            newTriangle1.triangles[2] = checkedTriangle.triangles[(i + 2) % 3];

                            k = 0;
                            if (newTriangle1.triangles[0] != null)
                            {
                                while (newTriangle1.edges[0] != newTriangle1.triangles[0].edges[k]) ++k;
                                newTriangle1.triangles[0].triangles[k] = newTriangle1;
                            }
                            k = 0;
                            if (newTriangle1.triangles[2] != null)
                            {
                                while (newTriangle1.edges[2] != newTriangle1.triangles[2].edges[k]) ++k;
                                newTriangle1.triangles[2].triangles[k] = newTriangle1;
                            }

                            edges.Remove(checkedTriangle.edges[i]);
                            curTriangle = newTriangle1;

                            // Записываем треугольники в множество
                            trianglesForDelaunayCheck.Add(newTriangle0);
                            trianglesForDelaunayCheck.Add(newTriangle1);

                            break;
                        }
                    }
                }
            }
        }

        private static void GetOut(List<Section> triangulationSectionsList, HashSet<Pixel> outerPivotPoints)
        {
            triangulationSectionsList = edges.Values.ToList();
            foreach (Pixel pixel in pivotPoints) outerPivotPoints.Add(pixel);
        }
    }
}
