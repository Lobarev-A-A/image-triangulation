using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ImageTriangulation.Applicaton
{
    class GreedyTriangulation
    {
        public static void MakeTriangulation(List<Pixel> inputPivotPointsList, List<Section> outputTriangulation)
        {
            List<Section> bufSectionList = new List<Section>();
            
            // создаём все возможные отрезки на входном наборе опорных точек
            MakeAllSections(inputPivotPointsList, bufSectionList);
            // сортируем отрезки по неубыванию длины
            bufSectionList.Sort();
            // вставляем отрезки в триангуляцию
            Insert(bufSectionList, outputTriangulation);
        }

        static void MakeAllSections(List<Pixel> points, List<Section> sections)
        {
            int pointsListLength = points.Count;

            for (int i = 0; i < pointsListLength - 1; ++i)
            {
                for (int j = i + 1; j < pointsListLength; ++j)
                {
                    sections.Add(new Section(points[i], points[j]));
                }
            }
        }

        static void Insert(List<Section> inputSectionList, List<Section> outputSectionList)
        {
            bool f;

            for (int i = 0; i < inputSectionList.Count; ++i)
            {
                f = true;

                for (int j = 0; j < outputSectionList.Count; ++j)
                {
                    if (Geometry.Crosses(inputSectionList[i], outputSectionList[j]))
                    {
                        f = false;
                        break;
                    }
                }

                if (f)
                {
                    outputSectionList.Add(inputSectionList[i]);
                }
            }
        }
    }
}
