using System;
using System.Drawing;
using static Tetris.Game;

namespace Tetris
{
    public class BoardRenderer
    {
        private readonly Image _image;
        private readonly Font _previewFont = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
        private readonly Font _scoreFont = new Font("Segoe UI", 20F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));

        public BoardRenderer()
        {
            _image = Image.FromFile("background.jpg");
        }

        public void RenderScore(Graphics g, int totalClientWidth, int totalClientHeight, Game _game)
        {
            var cellSize = (totalClientHeight - 100) / NumberOfCellsHigh;
            var totalWidth = cellSize * NumberOfCellsWide;
            var x1 = (totalClientWidth - totalWidth) / 2;

            g.DrawString($"Score : {_game.Score}", _scoreFont, Brushes.Black, x1 + totalWidth + 100, 240);
        }

        public void RenderPreview(Graphics g, int totalClientWidth, int totalClientHeight, Game _game)
        {
            var cellSize = (totalClientHeight - 100) / NumberOfCellsHigh;
            var totalWidth = cellSize * NumberOfCellsWide;
            var x1 = (totalClientWidth - totalWidth) / 2;

            g.TranslateTransform(x1 + totalWidth + 100 - 1, 299);

            g.FillRectangle(Brushes.Black, 0, 0, 152, 152);
            g.FillRectangle(Brushes.LightGray, 1, 1, 150, 150);

            var colour = CellColourToBrush(_game.NextPiece.Definition.Colour);
            foreach (var location in _game.NextPiece.Definition.Locations)
            {
                var x = (location.X + 2) * 25;
                var y = (6 - location.Y - 4) * 25;
                g.FillRectangle(colour, x, y, 24, 24);
            }

            g.DrawString("Next Piece", _previewFont, Brushes.Black, 20, 120);

            g.ResetTransform();

        }

        public void RenderBackground(Graphics g, int totalClientWidth, int totalClientHeight)
        {
            g.DrawImage(_image, 0, 0, totalClientWidth, totalClientHeight);

            var cellSize = (totalClientHeight - 100) / NumberOfCellsHigh;
            var totalWidth = cellSize * NumberOfCellsWide;
            var totalHeight = cellSize * NumberOfCellsHigh;

            var x1 = (totalClientWidth - totalWidth) / 2;
            var y1 = totalClientHeight - 100;

            g.FillRectangle(Brushes.Black, x1 - 10, y1, totalWidth + 20, 10);
            g.FillRectangle(Brushes.Black, x1 - 10, y1 - totalHeight, 10, totalHeight);
            g.FillRectangle(Brushes.Black, x1 + totalWidth, y1 - totalHeight, 10, totalHeight);

            g.FillRectangle(Brushes.LightGray, x1, y1 - totalHeight, totalWidth, totalHeight);

        }

        public void RenderBlocks(Graphics g, int totalClientWidth, int totalClientHeight, Game _game)
        {
            var cellSize = (totalClientHeight - 100) / NumberOfCellsHigh;
            var totalWidth = cellSize * NumberOfCellsWide;
            var totalHeight = cellSize * NumberOfCellsHigh;

            var x1 = (totalClientWidth - totalWidth) / 2;
            var y1 = (totalClientHeight - 100) - totalHeight;

            g.TranslateTransform(x1, y1);

            for (int row = 0; row < NumberOfCellsHigh; row++)
            {
                for (int column = 0; column < NumberOfCellsWide; column++)
                {
                    if (_game.CellColour(column, row) != Cell.Empty)
                    {
                        var colour = CellColourToBrush(_game.CellColour(column, row));

                        g.FillRectangle(colour, (column * cellSize) + 1, ((NumberOfCellsHigh - row - 1) * cellSize) + 1, cellSize - 2, cellSize - 2);
                    }
                }
            }

            g.ResetTransform();

        }

        private static Brush CellColourToBrush(Cell cell)
        {
            return cell switch
            {
                Cell.Blue => Brushes.Blue,
                Cell.Cyan => Brushes.Cyan,
                Cell.Green => Brushes.Green,
                Cell.Purple => Brushes.Purple,
                Cell.Red => Brushes.Red,
                Cell.Yellow => Brushes.Yellow,
                Cell.Orange => Brushes.Orange,
                _ => Brushes.Black,
            };
        }
    }
}
