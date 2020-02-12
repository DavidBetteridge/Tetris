using System;
using System.Windows.Forms;

namespace Tetris
{
    public class Game
    {
        public enum Cell
        {
            Empty = 0,
            Purple,
            Red,
            Yellow,
            Green,
            Cyan,
            Blue,
            Orange
        }

        public const int NumberOfCellsWide = 10;
        public const int NumberOfCellsHigh = 20;

        private readonly Cell[,] _board = new Cell[NumberOfCellsWide, NumberOfCellsHigh];

        private Random _rnd = new Random();

        private readonly System.Threading.Timer _gameLoop;

        private Piece _currentPiece;

        public Piece NextPiece { get; private set; }

        public int Score { get; private set; }

        private bool _enableTimer = true;

        public Game()
        {
            AddNextPiece();
            AddNextPiece();  //Called twice to generate both the current and the next piece

            _gameLoop = new System.Threading.Timer(Tick, null, 0, 500);
        }

        private void Tick(object state)
        {
            if (_enableTimer && _currentPiece is object)
            {
                if (!_currentPiece.SoftDrop())
                {
                    CheckForCompletedLines();
                    AddNextPiece();
                }
            }
        }

        private void CheckForCompletedLines()
        {
            void ShuffleDownRows(int startRow)
            {
                for (int row = startRow; row < NumberOfCellsHigh - 1; row++)
                {
                    for (int column = 0; column < NumberOfCellsWide; column++)
                    {
                        _board[column, row] = _board[column, row + 1];
                    }
                }

                for (int column = 0; column < NumberOfCellsWide; column++)
                {
                    _board[column, NumberOfCellsWide - 1] = Cell.Empty;
                }
            }

            UpdateScore();

            for (int row = 0; row < NumberOfCellsHigh; row++)
            {
                if (IsRowCompleted(row))
                {
                    ShuffleDownRows(row);
                    row--;
                }
            }
        }

        private bool IsRowCompleted(int row)
        {
            for (int column = 0; column < NumberOfCellsWide; column++)
                if (_board[column, row] == Cell.Empty) return false;
            return true;
        }
        private bool IsRowEmpty(int row)
        {
            for (int column = 0; column < NumberOfCellsWide; column++)
                if (_board[column, row] != Cell.Empty) return false;
            return true;
        }

        private void UpdateScore()
        {
            var firstLevel = int.MaxValue;
            var numberOfLines = 0;
            var otherLinesWithPieces = 0;
            for (int level = 0; level < NumberOfCellsHigh; level++)
            {
                if (IsRowCompleted(level))
                {
                    numberOfLines++;
                    firstLevel = Math.Min(firstLevel, level);
                }
                else if (!IsRowEmpty(level))
                {
                    otherLinesWithPieces++;
                }
            }

            if (numberOfLines > 0)
            {
                if (otherLinesWithPieces == 0)
                {
                    Score += 2000 * (firstLevel + 1);
                }
                else
                {
                    Score += numberOfLines switch
                    {
                        1 => 50 * (firstLevel + 1),
                        2 => 150 * (firstLevel + 1),
                        3 => 350 * (firstLevel + 1),
                        4 => 1000 * (firstLevel + 1),
                        _ => throw new Exception("How did you complete more than 4 lines with a single piece??"),
                    };
                }
            }
        }

        internal void HandleKey(Keys keyCode)
        {
            _enableTimer = false;

            switch (keyCode)
            {
                case Keys.Right:
                    _currentPiece.MoveRight();
                    break;

                case Keys.Left:
                    _currentPiece.MoveLeft();
                    break;

                case Keys.Space:
                    _currentPiece.HardDrop();
                    CheckForCompletedLines();
                    AddNextPiece();
                    break;

                case Keys.Down:
                    if (!_currentPiece.SoftDrop())
                    {
                        CheckForCompletedLines();
                        AddNextPiece();
                    }
                    break;

                case Keys.Z:
                    _currentPiece.RotateAntiClockwise();
                    break;

                case Keys.X:
                    _currentPiece.RotateClockwise();
                    break;

                default:
                    break;
            }

            _enableTimer = true;

        }

        public Cell CellColour(int column, int row) => _board[column, row];

        private void AddNextPiece()
        {
            var pieceType = _rnd.Next(0, 7);
            var definition = pieceType switch
            {
                0 => TetriminoDefinition.L(),
                1 => TetriminoDefinition.I(),
                2 => TetriminoDefinition.J(),
                3 => TetriminoDefinition.O(),
                4 => TetriminoDefinition.S(),
                5 => TetriminoDefinition.T(),
                6 => TetriminoDefinition.Z(),
                _ => throw new Exception($"{pieceType} was not expected."),
            };

            _currentPiece = NextPiece;

            NextPiece = new Piece(this._board, definition);
        }

    }
}
