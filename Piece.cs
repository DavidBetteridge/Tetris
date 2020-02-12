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
            var newLocation = CalculateRotationAndTransformation(_centerX, _centerY - 1, _rotation, CellOffset.Zero);
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
            var newLocation = CalculateRotationAndTransformation(_centerX - 1, _centerY, _rotation, CellOffset.Zero);
            if (AllLocationsValid(newLocation))
            {
                _centerX--;
            }
            DrawOnBoard();
        }

        public void MoveRight()
        {
            RemoveFromBoard();
            var newLocation = CalculateRotationAndTransformation(_centerX + 1, _centerY, _rotation, CellOffset.Zero);
            if (AllLocationsValid(newLocation))
            {
                _centerX++;
            }
            DrawOnBoard();
        }

        public void RotateAntiClockwise()
        {
            RemoveFromBoard();

            var newRotation = (_rotation + 3) % 4;

            foreach (var offsets in Definition.Offsets)
            {
                var rotationOffset = offsets.GetForRotation(_rotation, clockWise: false);
                var newLocation = CalculateRotationAndTransformation(_centerX, _centerY, newRotation, rotationOffset);
                if (AllLocationsValid(newLocation))
                {
                    _centerX -= rotationOffset.X;
                    _centerY -= rotationOffset.Y;
                    _rotation = newRotation;
                    break;
                }
            }

            DrawOnBoard();
        }

        public void RotateClockwise()
        {
            RemoveFromBoard();
            
            var newRotation = (_rotation + 1) % 4;

            foreach (var offsets in Definition.Offsets)
            {
                var rotationOffset = offsets.GetForRotation(_rotation, clockWise: true);
                var newLocation = CalculateRotationAndTransformation(_centerX, _centerY, newRotation, rotationOffset);
                if (AllLocationsValid(newLocation))
                {
                    _centerX -= rotationOffset.X;
                    _centerY -= rotationOffset.Y;
                    _rotation = newRotation;
                    break;
                }
            }

            DrawOnBoard();
        }

        private void DrawOnBoard()
        {
            var newLocation = CalculateRotationAndTransformation(_centerX, _centerY, _rotation, CellOffset.Zero); 
            foreach (var location in newLocation)
            {
                _board[location.X, location.Y] = Definition.Colour;
            }
        }

        private void RemoveFromBoard()
        {
            var currentLocation = CalculateRotationAndTransformation(_centerX, _centerY, _rotation, CellOffset.Zero);
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

        private CellLocation[] CalculateRotationAndTransformation(int xOffset, int yOffset, int rotation, CellOffset rotationOffset)
        {
            return Definition.Locations.Select(location =>
                rotation switch
                {
                    0 => new CellLocation(-rotationOffset.X + xOffset + location.X, -rotationOffset.Y + yOffset + location.Y),
                    1 => new CellLocation(-rotationOffset.X + xOffset + location.Y, -rotationOffset.Y + yOffset - location.X),
                    2 => new CellLocation(-rotationOffset.X + xOffset - location.X, -rotationOffset.Y + yOffset - location.Y),
                    3 => new CellLocation(-rotationOffset.X + xOffset - location.Y, -rotationOffset.Y + yOffset + location.X),
                    _ => throw new System.Exception($"Unexpected rotation - {_rotation}"),
                }
            ).ToArray();
        }
    }
}
