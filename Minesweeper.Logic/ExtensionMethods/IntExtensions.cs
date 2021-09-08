using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Logic.ExtensionMethods
{
    public static class IntExtensions
    {
        public static bool InRange(this int x, int min, int max)
        {
            return x >= min && x <= max;
        }

        public static bool InRange(this uint x, uint min, uint max)
        {
            return x >= min && x <= max;
        }
    }
}
