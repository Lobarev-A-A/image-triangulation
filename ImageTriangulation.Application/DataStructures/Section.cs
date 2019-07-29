using System;
using System.Drawing;

namespace ImageTriangulation.Applicaton
{
    public class Section : IComparable<Section>
    {
        public Pixel a;
        public Pixel b;
        public double length;

        public Section(Pixel a, Pixel b)
        {
            this.a = a;
            this.b = b;
            length = Math.Sqrt(Math.Pow(b.X - a.X, 2) + Math.Pow(b.Y - a.Y, 2));
        }

        // Сравнение по длине отрезка
        public int CompareTo(Section s)
        {
            return this.length.CompareTo(s.length);
        }        
    }
}
