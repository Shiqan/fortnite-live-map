using System;
using System.Collections.Generic;
using System.Text;

namespace FortniteMapTracker.Core.Models
{
    public class Coord
    {
        public Coord(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }
    }
}
