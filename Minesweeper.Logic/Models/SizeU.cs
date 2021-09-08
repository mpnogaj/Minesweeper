using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Logic.Models
{
    public struct SizeU
    {
        public uint Width { get; private set; }
        public uint Height { get; private set; }

        public SizeU(uint height, uint width)
        {
            Width = width;
            Height = height;
        }
    }
}
