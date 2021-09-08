using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Logic.Models
{
    public struct PointU
    {
        public uint X { get; private set; }
        public uint Y { get; private set; }

        public PointU(uint x, uint y)
        {
            X = x;
            Y = y;
        }
    }
}
