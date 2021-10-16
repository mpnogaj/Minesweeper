using Minesweeper.Logic.Models;
using Minesweeper.Logic.Properties;
using System.Drawing;

namespace Minesweeper.Logic
{
    public class Game
    {
        private const double BombChance = 10.0;

        public event EventHandler? GameOver;
        public event EventHandler? GameWon;

        private readonly Field[,] _board;
        private readonly BoardData _boardData;

        private uint _uncoveredLeft;

        public static Game? Instance
        {
            get;
            private set;
        }

        public static void PrepareGame(BoardData boardData) =>
            Instance = new Game(boardData);

        public static void PrepareGame(BoardData boardData, PointU player) => 
            Instance = new Game(boardData, player);

        public static void PrepareGame(BoardData boardData, PointU player, int seed) =>
            Instance = new Game(boardData, player, seed);

        private Game(BoardData boardData) :
            this(boardData, new PointU(uint.MaxValue, uint.MaxValue), new Random().Next())
        { }

        private Game(BoardData boardData, PointU player) :
            this(boardData, player, new Random().Next())
        { }

        private Game(BoardData boardData, PointU player, int seed)
        {
            _boardData = boardData;
            Random random = new(seed);
            _board = new Field[boardData.Height, boardData.Width];
            PlaceMinses(boardData, player, random);
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

        private void PlaceMinses(BoardData boardData, PointU player, Random random)
        {
            int currMines = 0;
            while (true)
            {
                for (int i = 0; i < boardData.Height; i++)
                {
                    for (int j = 0; j < boardData.Width; j++)
                    {
                        if (currMines == boardData.Mines)
                        {
                            return;
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
        }

        private static Bitmap GetCloseBombsBmp(uint closeBombs)
        {
            return closeBombs switch
            {
                1 => Resources.Field1,
                2 => Resources.Field2,
                3 => Resources.Field3,
                4 => Resources.Field4,
                5 => Resources.Field5,
                6 => Resources.Field6,
                7 => Resources.Field7,
                8 => Resources.Field8,
                _ => Resources.Empty,
            };
        }

        public Bitmap HandleClick(uint id, bool isLong = false, Bitmap? targetBmp = null)
        {
            uint x = id % _boardData.Width, y = id / _boardData.Height;
            return HandleClick(new PointU(x, y), isLong, targetBmp);
        }

        public Bitmap HandleClick(PointU position, bool isLong = false, Bitmap? targetBmp = null)
        {
            uint x = position.X, y = position.Y;
            Field field = _board[y, x];
            if (isLong)
            {
                if (field.IsUncovered)
                {
                    return targetBmp ?? (field.IsBomb ? Resources.Bomb : GetCloseBombsBmp(field.CloseBombs));
                }
                field.ToggleFlagged();
                return field.IsFlagged ? Resources.Flag : Resources.Default;
            }
            else
            {
                if (!field.IsUncovered)
                {
                    if (field.IsFlagged)
                    {
                        return Resources.Flag;
                    }
                    else if (field.UncoverField())
                    {
                        GameOver?.Invoke(this, EventArgs.Empty);
                        return Resources.Bomb;
                    }
                    else
                    {
                        _uncoveredLeft--;
                        if (_uncoveredLeft == 0)
                        {
                            GameWon?.Invoke(this, EventArgs.Empty);
                        }
                        return GetCloseBombsBmp(field.CloseBombs);
                    }
                }
                return targetBmp ?? (field.IsBomb ? Resources.Bomb : GetCloseBombsBmp(field.CloseBombs));
            }
        }
    }
}
