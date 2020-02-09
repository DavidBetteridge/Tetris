using System.Linq;
using static Tetris.Game;

namespace Tetris
{
    public class Piece
    {
        private int _centerY = 18;
        private int _centerX = 5;
        private int _rotation = 0;
        private Cell[,] _board;
        public TetriminoDefinition Definition { get; private set; }

        public Piece(Cell[,] board, TetriminoDefinition definition)
        {
            _board = board;
            Definition = definition;
        }

        public bool SoftDrop()
        {
            RemoveFromBoard();
            var newLocation = CalculateRotationAndTransformation(_centerX, _centerY - 1);
            var canBeDropped = AllLocationsValid(newLocation);
            if (canBeDropped)
            {
                _centerY--;
            }

            DrawOnBoard();

            return canBeDropped;
        }

        public void MoveLeft()
        {
            RemoveFromBoard();
            var newLocation = CalculateRotationAndTransformation(_centerX - 1, _centerY);
            if (AllLocationsValid(newLocation))
            {
                _centerX--;
            }
            DrawOnBoard();
        }

        public void MoveRight()
        {
            RemoveFromBoard();
            var newLocation = CalculateRotationAndTransformation(_centerX + 1, _centerY);
            if (AllLocationsValid(newLocation))
            {
                _centerX++;
            }
            DrawOnBoard();
        }

        public void RotateAntiClockwise()
        {
            RemoveFromBoard();

            // Rotate

            // If not valid,  apply offset(s)
            // If still not valid then not allowed.

            _rotation = (_rotation + 3) % 4;
            var rotated = CalculateRotationAndTransformation(_centerX, _centerY);
            DrawOnBoard();
        }

        public void RotateClockwise()
        {
            RemoveFromBoard();
            _rotation = (_rotation + 1) % 4;
            var rotated = CalculateRotationAndTransformation(_centerX, _centerY);
            DrawOnBoard();
        }

        private void DrawOnBoard()
        {
            var newLocation = CalculateRotationAndTransformation(_centerX, _centerY); 
            foreach (var location in newLocation)
            {
                _board[location.X, location.Y] = Definition.Colour;
            }
        }

        private void RemoveFromBoard()
        {
            var currentLocation = CalculateRotationAndTransformation(_centerX, _centerY);
            foreach (var location in currentLocation)
            {
                _board[location.X, location.Y] = Cell.Empty;
            }
        }

        private bool AllLocationsValid(CellLocation[] cellLocations)
        {
            bool IsLocationValid(CellLocation cellLocation)
            {
                if (cellLocation.X < 0) return false;
                if (cellLocation.Y < 0) return false;
                if (cellLocation.Y >= Game.NumberOfCellsHigh) return false;
                if (cellLocation.X >= Game.NumberOfCellsWide) return false;

                return _board[cellLocation.X, cellLocation.Y] == Cell.Empty;
            };

            return cellLocations.All(IsLocationValid);
        }

        private CellLocation[] CalculateRotationAndTransformation(int xOffset, int yOffset)
        {
            return Definition.Locations.Select(location =>
                _rotation switch
                {
                    0 => new CellLocation(xOffset + location.X, yOffset + location.Y),
                    1 => new CellLocation(xOffset + location.Y, yOffset - location.X),
                    2 => new CellLocation(xOffset - location.X, yOffset - location.Y),
                    3 => new CellLocation(xOffset - location.Y, yOffset + location.X),
                    _ => throw new System.Exception($"Unexpected rotation - {_rotation}"),
                }
            ).ToArray();
        }
    }
}
