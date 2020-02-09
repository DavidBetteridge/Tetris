using System.Drawing;
using static Tetris.Game;

namespace Tetris
{
    public class BoardRenderer
    {
        private readonly Image _image;

        public BoardRenderer()
        {
            _image = Image.FromFile("background.jpg");
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
                        var colour = _game.CellColour(column, row) switch
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

                        g.FillRectangle(colour, (column * cellSize) + 1, ((NumberOfCellsHigh - row - 1) * cellSize) + 1, cellSize - 2, cellSize - 2);
                    }
                }
            }

            g.ResetTransform();

        }
    }
}
