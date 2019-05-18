using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// TO DO
// * 
namespace image_triangulation
{
    public struct Pixel : IEquatable<object>
    {
        public int X;
        public int Y;
        public byte brightness;

        public Pixel(int X, int Y, byte brightness = 0)
        {
            this.X = X;
            this.Y = Y;
            this.brightness = brightness;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;

            Pixel pixel = (Pixel)obj;
            if (pixel.X == X && pixel.Y == Y) return true;
            else return false;
        }

        public override int GetHashCode()
        {
            return 347 * X.GetHashCode() + Y.GetHashCode();
        }

        //public new float GetHashCode()
        //{
        //    float hash = Y * 10 + 1;
        //    while (hash >= 1) hash /= 10;
        //    return hash + X;
        //}

        public static bool operator == (Pixel p1, Pixel p2)
        {
            return p1.Equals(p2);
        }

        public static bool operator != (Pixel p1, Pixel p2)
        {
            return !p1.Equals(p2);
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
