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
        private readonly TetriminoDefinition _tetriminoDefinition;

        public Piece(Cell[,] board, TetriminoDefinition definition)
        {
            _board = board;
            _tetriminoDefinition = definition;
        }

        public bool SoftDrop()
        {
            DrawOnBoard(Cell.Empty);
            var newLocation = CalculateRotationAndTransformation(_centerX, _centerY - 1);
            var canBeDropped = AllLocationsValid(newLocation);
            if (canBeDropped)
            {
                _centerY--;
            }
            DrawOnBoard(_tetriminoDefinition.Colour);

            return canBeDropped;
        }

        public void MoveLeft()
        {
            DrawOnBoard(Cell.Empty);
            var newLocation = CalculateRotationAndTransformation(_centerX - 1, _centerY);
            if (AllLocationsValid(newLocation))
            {
                _centerX--;
            }
            DrawOnBoard(_tetriminoDefinition.Colour);
        }

        public void MoveRight()
        {
            DrawOnBoard(Cell.Empty);
            var newLocation = CalculateRotationAndTransformation(_centerX + 1, _centerY);
            if (AllLocationsValid(newLocation))
            {
                _centerX++;
            }
            DrawOnBoard(_tetriminoDefinition.Colour);
        }

        public void RotateAntiClockwise()
        {
            DrawOnBoard(Cell.Empty);

            // Rotate

            // If not valid,  apply offset(s)
            // If still not valid then not allowed.

            _rotation = (_rotation + 3) % 4;
            DrawOnBoard(_tetriminoDefinition.Colour);
        }

        public void RotateClockwise()
        {
            DrawOnBoard(Cell.Empty);
            _rotation = (_rotation + 1) % 4;
            DrawOnBoard(_tetriminoDefinition.Colour);
        }

        internal void DrawOnBoard(Cell setTo)
        {
            var rotated = CalculateRotationAndTransformation(_centerX, _centerY);
            foreach (var location in rotated)
            {
                _board[location.X, location.Y] = setTo;
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

        internal CellLocation[] CalculateRotationAndTransformation(int xOffset, int yOffset)
        {
            return _tetriminoDefinition.Locations.Select(location =>
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
