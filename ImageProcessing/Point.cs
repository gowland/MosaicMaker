using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing
{
    [Serializable]
    public class Point
    {
        public static Point Origin = new Point(0, 0);

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
        public int X { get; set; }
        public int Y { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as Point;

            if (other == null)
            {
                return false;
            }

            return (X == other.X) && (Y == other.Y);
        }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = hash << 7 + X.GetHashCode();
            hash = hash << 7 + Y.GetHashCode();
            return hash;
        }
    }
}
