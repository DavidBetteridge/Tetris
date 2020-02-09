﻿using System;
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

        public Game()
        {
            _gameLoop = new System.Threading.Timer(Tick, null, 0, 500);
        }

        private void Tick(object state)
        {
            if (_currentPiece is object)
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
            bool IsRowCompleted(int row)
            {
                for (int column = 0; column < NumberOfCellsWide; column++)
                    if (_board[column, row] == Cell.Empty) return false;
                return true;
            }

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

            for (int row = 0; row < NumberOfCellsHigh; row++)
            {
                if (IsRowCompleted(row))
                {
                    ShuffleDownRows(row);
                }
            }
        }

        internal void HandleKey(Keys keyCode)
        {
            switch (keyCode)
            {
                case Keys.Right:
                    _currentPiece.MoveRight();
                    break;

                case Keys.Left:
                    _currentPiece.MoveLeft();
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
                    return;
            }
        }

        public Cell CellColour(int column, int row) => _board[column, row];

        public void AddNextPiece()
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

            _currentPiece = new Piece(this._board, definition);
        }

    }
}
