namespace Tetris
{
    public class CellOffset
    {
        public int X { get; private set; }
        public int Y { get; private  set; }
        public CellOffset(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static CellOffset Zero => new CellOffset(0, 0);

    }

    public class CellOffsets
    {
        public CellOffset Rotation0R { get; set; }
        public CellOffset RotationR0 { get; set; }
        public CellOffset RotationR2 { get; set; }
        public CellOffset Rotation2R { get; set; }

        public CellOffset Rotation2L { get; set; }
        public CellOffset RotationL2 { get; set; }
        public CellOffset RotationL0 { get; set; }
        public CellOffset Rotation0L { get; set; }


        public CellOffset GetForRotation(int currentRotation, bool clockWise)
        {
            return currentRotation switch
            {
                0 when clockWise => Rotation0R,
                0 => Rotation0L,

                1 when clockWise => RotationR2,
                1 => RotationR0,

                2 when clockWise => Rotation2R,
                2 => Rotation2L,

                3 when clockWise => RotationL0,
                3 => RotationL2,

                _ => throw new System.Exception($"Unexpected rotation {currentRotation}"),
            };
        }
    }
}
 
