using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace COL.Core
{
    public class WBEvent
    {
        //public const int WB_XY_INDRIVER_SCALERATE = 12;
        //public const int WB_DEVICE_WIDTH = 800;
        //public const int WB_DEVICE_HEIGHT = 400;

        private uint _uiTimeStamp; // From 0 to 60000, millseconds(0~59)
        private ushort _usReserved;
        private int _iX; //if its value is in WBPenEvent it is special event based on its value
        private int _iY;

        public uint TimeStamp
        {
            get { return _uiTimeStamp; }
        }
        public ushort Reserve
        {
            get { return _usReserved; }
        }
        public int X
        {
            get { return _iX; }
        }
        public int Y
        {
            get { return _iY; }
        }

        public WBEvent(uint wTime, int wX, int wY)
        {
            SetValue(wTime, 0, wX, wY);
        }

        public WBEvent(uint wTime, ushort wReserved, int wX, int wY)
        {
            SetValue(wTime, wReserved, wX, wY);
        }
        public WBEvent(BinaryReader breader)
        {
            SetValue((uint)(breader.ReadUInt16()), (ushort)breader.ReadInt16(), (int)breader.ReadInt16(), (int)breader.ReadInt16());
        }

        private void SetValue(uint wTime, ushort wReserved, int wX, int wY)
        {
            _uiTimeStamp = wTime;
            _usReserved = 0;
            _iX = wX;
            _iY = wY;
        }

        public static int StreamSize
        {
            get { return 2 * 4; }
        }
        
        /*
        public bool IsFunctionClear()
        {
            return _iX == (int)WBPenEvent.FunctionClear;
        }*/
    }
}
