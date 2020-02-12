using System.Collections.Generic;
using static Tetris.Game;

namespace Tetris
{
    public class TetriminoDefinition
    {
        public CellLocation[] Locations { get; set; }

        public Cell Colour { get; set; }
        public List<CellOffsets> Offsets { get; set; }

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

                Offsets = CommonOffsets(),
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

                Offsets = CommonOffsets(),
            };
        }

        public static TetriminoDefinition S()
        {
            return new TetriminoDefinition
            {
                Locations = new CellLocation[]
                {
                                new CellLocation(0,1),
                                new CellLocation(1,1),
                                new CellLocation(0,0),
                                new CellLocation(-1,0),
                },
                Colour = Cell.Green,

                Offsets = CommonOffsets(),
            };
        }

        public static TetriminoDefinition Z()
        {
            return new TetriminoDefinition
            {
                Locations = new CellLocation[]
                {
                                new CellLocation(0,1),
                                new CellLocation(-1,1),
                                new CellLocation(0,0),
                                new CellLocation(1,0),
                },
                Colour = Cell.Red,

                Offsets = CommonOffsets(),
            };
        }

        public static TetriminoDefinition J()
        {
            return new TetriminoDefinition
            {
                Locations = new CellLocation[]
                {
                                new CellLocation(-1,1),
                                new CellLocation(-1,0),
                                new CellLocation(0,0),
                                new CellLocation(1,0),
                },
                Colour = Cell.Blue,

                Offsets = CommonOffsets(),

            };
        }

        private static List<CellOffsets> CommonOffsets()
        {
            return new List<CellOffsets>
                {
                    new CellOffsets
                    {
                        Rotation0R = CellOffset.Zero,
                        RotationR0 = CellOffset.Zero,
                        RotationR2 = CellOffset.Zero,
                        Rotation2R = CellOffset.Zero,
                        Rotation2L = CellOffset.Zero,
                        RotationL2 = CellOffset.Zero,
                        RotationL0 = CellOffset.Zero,
                        Rotation0L = CellOffset.Zero,
                    },

                    //new CellOffsets
                    //{
                    //    Rotation0 = new CellOffset(0, 0),
                    //    Rotation1 = new CellOffset(1, 0),
                    //    Rotation2 = new CellOffset(0, 0),
                    //    Rotation3 = new CellOffset(-1, 0),
                    //},

                    //new CellOffsets
                    //{
                    //    Rotation0 = new CellOffset(0, 0),
                    //    Rotation1 = new CellOffset(1, -1),
                    //    Rotation2 = new CellOffset(0, 0),
                    //    Rotation3 = new CellOffset(-1, -1),
                    //},

                    //new CellOffsets
                    //{
                    //    Rotation0 = new CellOffset(0, 0),
                    //    Rotation1 = new CellOffset(0, 2),
                    //    Rotation2 = new CellOffset(0, 0),
                    //    Rotation3 = new CellOffset(0, 2),
                    //},

                    //new CellOffsets
                    //{
                    //    Rotation0 = new CellOffset(0, 0),
                    //    Rotation1 = new CellOffset(1, 2),
                    //    Rotation2 = new CellOffset(0, 0),
                    //    Rotation3 = new CellOffset(-1, 2),
                    //}

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

                Offsets = new List<CellOffsets>
                {
                    new CellOffsets
                    {
                        Rotation0R = new CellOffset(0, -1),
                        RotationR2 = new CellOffset(-1, 0),
                        Rotation2L = new CellOffset(0, 1),
                        RotationL0 = new CellOffset(1, 0),

                        Rotation0L = new CellOffset(0, -1),
                        RotationL2 = new CellOffset(0, -1),
                        Rotation2R = new CellOffset(0, -1),
                        RotationR0 = new CellOffset(0, -1),
                    }
                },
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

                Offsets = new List<CellOffsets>
                {
                    new CellOffsets
                    {
                        Rotation0R = CellOffset.Zero,
                        RotationR0 = CellOffset.Zero,
                        RotationR2 = CellOffset.Zero,
                        Rotation2R = CellOffset.Zero,
                        Rotation2L = CellOffset.Zero,
                        RotationL2 = CellOffset.Zero,
                        RotationL0 = CellOffset.Zero,
                        Rotation0L = CellOffset.Zero,
                    },

                    new CellOffsets
                    {
                        Rotation0R = new CellOffset(-2, 0),
                        RotationR0 = new CellOffset(2, 0),
                        RotationR2 = new CellOffset(-1, 0),
                        Rotation2R = new CellOffset(1, 0),
                        Rotation2L = new CellOffset(2, 0),
                        RotationL2 = new CellOffset(-1, 0),
                        RotationL0 = new CellOffset(1, 0),
                        Rotation0L = new CellOffset(-1, 0),
                    },

                    new CellOffsets
                    {
                        Rotation0R = new CellOffset(1, 0),
                        RotationR0 = new CellOffset(-1, 0),
                        RotationR2 = new CellOffset(2, 0),
                        Rotation2R = new CellOffset(-2, 0),
                        Rotation2L = new CellOffset(-1, 0),
                        RotationL2 = new CellOffset(1, 0),
                        RotationL0 = new CellOffset(-2, 0),
                        Rotation0L = new CellOffset(2, 0),
                    },

                    new CellOffsets
                    {
                        Rotation0R = new CellOffset(-2, -1),
                        RotationR0 = new CellOffset(2, 1),
                        RotationR2 = new CellOffset(-1, 2),
                        Rotation2R = new CellOffset(1, -2),
                        Rotation2L = new CellOffset(2, 1),
                        RotationL2 = new CellOffset(-2, -1),
                        RotationL0 = new CellOffset(1, -2),
                        Rotation0L = new CellOffset(-1, 2),
                    },

                    new CellOffsets
                    {
                        Rotation0R = new CellOffset(1,2),
                        RotationR0 = new CellOffset(-1,-2),
                        RotationR2 = new CellOffset(2, 1),
                        Rotation2R = new CellOffset(-2, 1),
                        Rotation2L = new CellOffset(-1,-2),
                        RotationL2 = new CellOffset(1,2),
                        RotationL0 = new CellOffset(-2,1),
                        Rotation0L = new CellOffset(2,-1),
                    }
                },
            };
        }
    }
}