using Minesweeper.Logic.Models;
using Minesweeper.Logic.Properties;
using System.Drawing;

namespace Minesweeper.Logic
{
    public class Game
    {
        const double BombChance = 30.0;

        public event EventHandler? GameOver;
        public event EventHandler? GameWon;

        private readonly Field[,] _board;
        private readonly BoardData _boardData;

        private uint _uncoveredLeft;

        public Game(BoardData boardData) : 
            this(boardData, new PointU(uint.MaxValue, uint.MaxValue), new Random().Next()) { }

        public Game(BoardData boardData, PointU player) : 
            this(boardData, player, new Random().Next()) { }

        public Game(BoardData boardData, PointU player, int seed)
        {
            _boardData = boardData;
            Random random = new(seed);
            _board = new Field[boardData.Height, boardData.Width];
            int currMines = 0;
            while (true)
            {
                for (int i = 0; i < boardData.Height; i++)
                {
                    for (int j = 0; j < boardData.Width; j++)
                    {
                        if(currMines == boardData.Mines)
                        {
                            break;
                        }
                        if (player.Y != i && player.X != j &&
                            _board[i, j] == null &&
                            random.NextDouble() <= BombChance / 100)
                        {
                            _board[i, j] = new Field();
                            currMines++;
                        }
                    }
                }
            }
            for (int i = 0; i < boardData.Height; i++)
            {
                for (int j = 0; j < boardData.Width; j++)
                {
                    if (_board[i, j] != null)
                    {
                        continue;
                    }
                    uint bombCount = 0;
                    for (int x = -1; x <= 1; x++)
                    {
                        for (int y = -1; y <= 1; y++)
                        {
                            try
                            {
                                if (_board[i + y, j + x] != null && _board[i + y, j + x].IsBomb)
                                {
                                    bombCount++;
                                }
                            }
                            catch (IndexOutOfRangeException) { /*Ignore*/ }
                        }
                    }
                    _board[i, j] = new Field(bombCount);
                }
            }
            _uncoveredLeft = boardData.Height * boardData.Width - boardData.Mines;
        }

        public Bitmap HandleClick(uint id, bool isLong = false)
        {
            uint x = id % _boardData.Width, y = id / _boardData.Height;
            Field field = _board[y, x];
            if (isLong)
            {
                field.ToggleFlagged();
                return field.IsFlagged ? Resources.Flag : Resources.Default;
            }
            else
            {
                if(field.UncoverField())
                { 
                    GameOver?.Invoke(this, EventArgs.Empty);
                    return Resources.Bomb;
                }
                else
                {
                    _uncoveredLeft--;
                    if(_uncoveredLeft == 0)
                    {
                        GameWon?.Invoke(this, EventArgs.Empty);
                    }
                    return Resources.Empty;
                }
            }
        }
    }
}
