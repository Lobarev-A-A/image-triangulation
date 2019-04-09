using System;
using System.Drawing;

namespace image_triangulation
{
    class Section : IComparable<Section>
    {
        public Pixel a { get; }
        public Pixel b { get; }
        public double length { get; }

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
