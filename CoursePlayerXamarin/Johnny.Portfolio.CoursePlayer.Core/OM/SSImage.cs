using System;

namespace Johnny.Portfolio.CoursePlayer.Core.OM
{
    public class SSImage
    {
        public SSImage(int row, int col, byte[] image)
        {
            Row = row;
            Col = col;
            Image = image;
        }

        public int Row { get; set; }
        public int Col { get; set; }
        public byte[] Image { get; set; }
    }
}
