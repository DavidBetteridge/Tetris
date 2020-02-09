using static Tetris.Game;

namespace Tetris
{
    public class TetriminoDefinition
    {
        public CellLocation[] Locations { get; set; }

        public Cell Colour { get; set; }

        public static TetriminoDefinition L()
        {
            return new TetriminoDefinition
            {
                Locations = new CellLocation[]
                {
                                new CellLocation(1,1),
                                new CellLocation(-1,0),
                                new CellLocation(0,0),
                                new CellLocation(+1,0),
                },
                Colour = Cell.Orange,
            };
        }
        public static TetriminoDefinition T()
        {
            return new TetriminoDefinition
            {
                Locations = new CellLocation[]
                {
                                new CellLocation(0,1),
                                new CellLocation(-1,0),
                                new CellLocation(0,0),
                                new CellLocation(+1,0),
                },
                Colour = Cell.Purple,
            };
        }

        public static TetriminoDefinition S()
        {
            return new TetriminoDefinition
            {
                Locations = new CellLocation[]
                {
                                new CellLocation(0,-1),
                                new CellLocation(1,-1),
                                new CellLocation(0,0),
                                new CellLocation(-1,0),
                },
                Colour = Cell.Green,
            };
        }

        public static TetriminoDefinition Z()
        {
            return new TetriminoDefinition
            {
                Locations = new CellLocation[]
                {
                                new CellLocation(0,-1),
                                new CellLocation(-1,-1),
                                new CellLocation(0,0),
                                new CellLocation(1,0),
                },
                Colour = Cell.Red,
            };
        }

        public static TetriminoDefinition J()
        {
            return new TetriminoDefinition
            {
                Locations = new CellLocation[]
                {
                                new CellLocation(-1,-1),
                                new CellLocation(-1,0),
                                new CellLocation(0,0),
                                new CellLocation(1,0),
                },
                Colour = Cell.Blue,
            };
        }

        public static TetriminoDefinition O()
        {
            return new TetriminoDefinition
            {
                Locations = new CellLocation[]
                {
                                new CellLocation(1,1),
                                new CellLocation(0,1),
                                new CellLocation(0,0),
                                new CellLocation(1,0),
                },
                Colour = Cell.Yellow,
            };
        }

        public static TetriminoDefinition I()
        {
            return new TetriminoDefinition
            {
                Locations = new CellLocation[]
                {
                                new CellLocation(-1,0),
                                new CellLocation(0,0),
                                new CellLocation(1,0),
                                new CellLocation(2,0),
                },
                Colour = Cell.Cyan,
            };
        }
    }
}