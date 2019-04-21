using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// TO DO
// * Написать функцию сравнения
namespace image_triangulation
{
    public class Pixel
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
    }
}
