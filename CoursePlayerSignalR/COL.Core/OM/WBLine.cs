using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace COL.Core
{
    public class WBLine
    { 
        private ushort _usX0;
        private ushort _usY0;
        private ushort _usX1;
        private ushort usY1;
        private short _sColor;
        private ushort _usReserved;

        public WBLine()
        {
        }
        public WBLine(ushort wx0, ushort wy0, ushort wx1, ushort wy1, short wcolor, ushort wreserver)
        {
            SetValue(wx0, wy0, wx1, wy1, wcolor, wreserver);
        }
        public WBLine(BinaryReader breader)
        {
            SetValue((ushort)breader.ReadInt16(), (ushort)breader.ReadInt16(), (ushort)breader.ReadInt16(),
                     (ushort)breader.ReadInt16(), (short)breader.ReadInt16(), (ushort)breader.ReadInt16());
        }

        private void SetValue(ushort x0, ushort y0, ushort x1, ushort y1, short color, ushort reserver)
        {
            _usX0 = x0;
            _usY0 = y0;
            _usX1 = x1;
            usY1 = y1;
            _sColor = color;
            _usReserved = reserver;
        }

        public ushort X0
        {
            get { return _usX0; }
        }
        public ushort Y0
        {
            get { return _usY0; }
        }
        public ushort X1
        {
            get { return _usX1; }
        }
        public ushort Y1
        {
            get { return usY1; }
        }
        public short UColor
        {
            get { return _sColor; }
        }
        public ushort Reserve
        {
            get { return _usReserved; }
        }
        public static int StreamSize
        {
            get { return 2 * 6; }
        }

        public WBPenColor GetColor()
        {
            switch (_sColor)
            {
                case (short)WBPenEvent.RedPenDown:
                    return WBPenColor.Red;
                case (short)WBPenEvent.GreenPenDown:
                    return WBPenColor.Green;
                case (short)WBPenEvent.BluePenDown:
                    return WBPenColor.Blue;
                default:
                    return WBPenColor.Black;
            }
        }
    }
}
