using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COL.Core
{
    public class IndexList
    {
        protected TimeSpan _duration;
        protected List<Index> _listIndex = new List<Index>();
        IDictionary<int, int> _mapIndex = new Dictionary<int, int>();

        public IndexList()
        {
        }

        TimeSpan Duration
        {
            get { return _duration; }
        }

        public List<int> TimePoints
        {
            get
            {
                List<int> ls = new List<int>();
                foreach (int k in this._mapIndex.Keys)
                {
                    ls.Add(k);
                }

                return ls;
            }
        }

        public IndexList(byte[] indexbuf)
        {
            SetIndexList(indexbuf);

            if (_listIndex.Count > 0)
            {
                _duration = TimeSpan.FromSeconds(_listIndex[_listIndex.Count - 1].TimeStamp);
            }
            for (int i = 0; i < _listIndex.Count; i++)
            {
                if (!_mapIndex.ContainsKey(_listIndex[i].TimeStamp))
                {
                    _mapIndex.Add(new KeyValuePair<int, int>(_listIndex[i].TimeStamp, i));
                }
            }
        }

        protected void SetIndexList(byte[] indexbuf)
        {
            _listIndex.Clear();
            
            //unzip
            ICSharpCode.SharpZipLib.Wrapper zipwrapper = new ICSharpCode.SharpZipLib.Wrapper();
            indexbuf = zipwrapper.Decompress(indexbuf);

            //read data to index list
            MemoryStream stream = new MemoryStream(indexbuf);
            BinaryReader breader = new BinaryReader(stream);
            for (int i = 0; i < indexbuf.Length / Index.StreamSize; i++)
            {
                Index item = new Index(breader);

                _listIndex.Add(item);
            }

            //dataffset ==-1 is the point to same block as previous one
            for (int i = 0; i < _listIndex.Count; i++)
            {
                if (_listIndex[i].DataOffset == -1 && i > 0)
                {
                    _listIndex[i].ChangeDataOffsetLength(_listIndex[i - 1].DataOffset, _listIndex[i - 1].DataLength);
                }
            }

            _listIndex.Sort();
        }

        public List<Index> GetIndexList()
        {
            return _listIndex;
        }


        //used by the Screenshot Data index
        public List<Index> GetImageIndexBySecond(int second)
        {
            bool[] foundset = new bool[Constants.MAX_ROW_NO * Constants.MAX_COL_NO];

            List<Index> indexs = new List<Index>();

            int firstItem = 0;
            int firstSecond = second;
            for (; firstSecond >= 0; firstSecond--)
            {
                if (_mapIndex.ContainsKey(firstSecond))
                {
                    firstItem = _mapIndex[firstSecond];
                    break;
                }
            }

            while (firstItem < _listIndex.Count && _listIndex[firstItem].TimeStamp == firstSecond)
            {
                firstItem++;
            }

            if (firstItem > 0)
            {
                for (int i = firstItem - 1; i >= 0; i--)
                {
                    int value = _listIndex[i].Row * Constants.MAX_ROW_NO + _listIndex[i].Col;
                    if (!foundset[value])
                    {
                        foundset[value] = true;
                        indexs.Add(_listIndex[i]);
                    }
                    if (indexs.Count == Constants.MAX_ROW_NO * Constants.MAX_COL_NO)
                    {
                        break;
                    }
                }
            }
            return indexs;
        }

        //used by the Screenshot Data index
        public List<Index> GetIndexChange(int second)
        {
            List<Index> indexs = new List<Index>();
            if (_mapIndex.ContainsKey(second))
            {
                for (int i = _mapIndex[second]; i < _listIndex.Count; i++)
                {
                    if (_listIndex[i].TimeStamp != second)
                    {
                        break;
                    }
                    indexs.Add(_listIndex[i]);
                }
            }
            return indexs;
        }
    }
}
