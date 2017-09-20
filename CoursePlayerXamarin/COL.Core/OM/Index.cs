using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace COL.Core
{
    public class Index : IComparable<Index>
    {
        private ushort _usTimeStamp; //in minute for whiteboard, in second for screenshot
        private byte _bGrid;
        private int _iOffset;
        private uint _uiLength;

        public Index()
        {
            setvalue(0, 0, 0, 0);
        }
        public Index(ushort timestamp, int offset, uint length)
        {
            setvalue(timestamp, 0, offset, length);
        }
        public Index(ushort timestamp, byte grid, int offset, uint length)
        {
            setvalue(timestamp, grid, offset, length);
        }
        public Index(BinaryReader breader)
        {
            setvalue((ushort)breader.ReadInt16(), breader.ReadByte(), breader.ReadInt32(), (uint)breader.ReadInt32());
        }

        private void setvalue(ushort timestamp, byte grid, int offset, uint length)
        {
            _usTimeStamp = timestamp;
            _bGrid = grid;
            _iOffset = offset;
            _uiLength = length;
        }

        public ushort TimeStamp
        {
            get { return _usTimeStamp; }
        }
        public byte Grid
        {
            get { return _bGrid; }
        }
        public int DataOffset
        {
            get { return _iOffset; }
        }
        public uint DataLength
        {
            get { return _uiLength; }
        }
        public byte Row
        {
            get { return (byte)(_bGrid >> 4); }
        }
        public byte Col
        {
            get { return (byte)(_bGrid & 0xf); }
        }

        public static int StreamSize
        {
            get { return 2 + 1 + 4 + 4; }
        }

        public void ChangeDataOffsetLength(int dwOffset, uint dwLength)
        {
            _iOffset = dwOffset;
            _uiLength = dwLength;
        }

        public int CompareTo(Index obj)
        {
            int ret = TimeStamp.CompareTo(obj.TimeStamp);
            if (ret == 0)
            {
                ret = Grid.CompareTo(obj.Grid);
            }
            return ret;
        }
    }
}
