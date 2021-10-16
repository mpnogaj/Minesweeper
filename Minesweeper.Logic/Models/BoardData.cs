using Minesweeper.Logic.ExtensionMethods;

namespace Minesweeper.Logic.Models
{
    public struct BoardData
    {
        public uint Height { get; private set; }
        public uint Width { get; private set; }
        public uint Mines { get; private set; }

        public BoardData(SizeU size, uint mines)
        {
            try
            {
                if (!(size.Height * size.Width).InRange(1, uint.MaxValue))
                {
                    throw new ArgumentOutOfRangeException(nameof(size));
                }
                if (!mines.InRange(1, size.Height * size.Width - 1))
                {
                    throw new ArgumentOutOfRangeException(nameof(mines));
                }
            }
            catch(OverflowException)
            {
                throw new ArgumentOutOfRangeException(nameof(size));
            }
            Height = size.Height;
            Width = size.Width;
            Mines = mines;
        }
    }
}
