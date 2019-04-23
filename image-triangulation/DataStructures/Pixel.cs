using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// TO DO
// * Написать функцию сравнения
namespace image_triangulation
{
    public class Pixel : IEquatable<Pixel>
    {
        public int X { get; set; }
        public int Y { get; set; }
        public float brightness { get; set; }

        public Pixel(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public Pixel(int X, int Y, float brightness)
        {
            this.X = X;
            this.Y = Y;
            this.brightness = brightness;
        }

        public bool Equals(Pixel pixel)
        {
            if (pixel == null) return false;

            if (pixel.X == X && pixel.Y == Y) return true;
            else return false;
        }

        public new float GetHashCode()
        {
            float hash = Y;
            while (hash >= 1) hash /= 10;
            return hash + X;
        }
    }

    public class XComparer : IComparer<Pixel>
    {
        public int Compare(Pixel p1, Pixel p2)
        {
            if (p1.X > p2.X) return 1;
            else if (p1.X < p2.X) return -1;
            else return 0;
        }
    }

    public class YComparer : IComparer<Pixel>
    {
        public int Compare(Pixel p1, Pixel p2)
        {
            if (p1.Y > p2.Y) return 1;
            else if (p1.Y < p2.Y) return -1;
            else return 0;
        }
    }
}
