namespace Minesweeper.Logic.Models
{
    public class Field
    {
        public bool IsFlagged { get; private set; } = false;
        public bool IsUncovered { get; private set; } = false;
        public bool IsBomb { get; private set; }
        public uint CloseBombs { get; private set; }
        
        public Field(uint bombCount)
        {
            IsBomb = false;
            CloseBombs = bombCount;
        }

        public Field()
        {
            IsBomb = true;
            CloseBombs = uint.MaxValue;
        }

        public void ToggleFlagged()
        {
            IsFlagged = !IsFlagged;
        }

        public bool UncoverField()
        {
            IsUncovered = true;
            return IsBomb;
        }
    }
}
