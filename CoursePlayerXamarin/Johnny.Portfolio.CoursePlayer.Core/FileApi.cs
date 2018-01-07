using System;
using System.Collections.Generic;
using System.IO;
using Johnny.Portfolio.CoursePlayer.Core.OM;

namespace Johnny.Portfolio.CoursePlayer.Core
{
    public class FileApi
    {
        private readonly FileHelper _fileHelper;
        //private string _indexfilename;
        //private string _datafilename;
        //private byte[] _decompressedBytes;

        public FileApi()
        {
            _fileHelper = new FileHelper();
           // _indexfilename = indexfilename;
           // _datafilename = datafilename;
        }

        public byte[] GetIndexFile(string originalFile) {
            //if (_decompressedBytes == null || _decompressedBytes.Length == 0)
            //{
            byte[] decompressedBytes = UnzipIndexFile(originalFile);
            //}
            return decompressedBytes;
        }

        private byte[] UnzipIndexFile(string originalFile)
        {
            byte[] bytes = _fileHelper.ReadBytes(originalFile);
            //unzip
            Wrapper zipwrapper = new Wrapper();
            byte[] indexbuf = zipwrapper.Decompress(bytes);

            return indexbuf;
        }

        public List<Index> GetIndexList(byte[] indexbuf)
        {
            List<Index> listIndex = new List<Index>();
            //read data to index list
            MemoryStream stream = new MemoryStream(indexbuf);
            BinaryReader breader = new BinaryReader(stream);
            for (int i = 0; i < indexbuf.Length / Index.StreamSize; i++)
            {
                Index item = new Index((ushort)breader.ReadInt16(), breader.ReadByte(), breader.ReadInt32(), (uint)breader.ReadInt32());
                listIndex.Add(item);
            }

            //dataffset ==-1 is the point to same block as previous one
            for (int i = 0; i < listIndex.Count; i++)
            {
                if (listIndex[i].DataOffset == -1 && i > 0)
                {
                    listIndex[i].DataOffset = listIndex[i - 1].DataOffset;
                    listIndex[i].DataLength = listIndex[i - 1].DataLength;
                }
            }

            listIndex.Sort();

            //if (listIndex.Count > 0)
            //{
            //    _duration = TimeSpan.FromSeconds(_listIndex[_listIndex.Count - 1].TimeStamp);
            //}
           
            return listIndex;
        }

        //used by the Screenshot Data index
        public List<Index> GetSSIndex(List<Index> ssIndexList, IDictionary<int, int> mapIndex, int second)
        {
            bool[] foundset = new bool[Constants.MAX_ROW_NO * Constants.MAX_COL_NO];

            List<Index> res = new List<Index>();

            int firstItem = 0;
            int firstSecond = second;
            for (; firstSecond >= 0; firstSecond--)
            {
                if (mapIndex.ContainsKey(firstSecond))
                {
                    firstItem = mapIndex[firstSecond];
                    break;
                }
            }

            while (firstItem < ssIndexList.Count && ssIndexList[firstItem].TimeStamp == firstSecond)
            {
                firstItem++;
            }

            if (firstItem > 0)
            {
                for (int i = firstItem - 1; i >= 0; i--)
                {
                    int value = ssIndexList[i].Row * Constants.MAX_ROW_NO + ssIndexList[i].Col;
                    if (!foundset[value])
                    {
                        foundset[value] = true;
                        res.Add(ssIndexList[i]);
                    }
                    if (res.Count == Constants.MAX_ROW_NO * Constants.MAX_COL_NO)
                    {
                        break;
                    }
                }
            }
            return res;
        }

        public List<SSImage> GetSSData(string imagedatafile, List<Index> ssIndex)
        {
            List<SSImage> imageList = new List<SSImage>();
            foreach (Index index in ssIndex)
            {
                byte[] buf = _fileHelper.Seek(imagedatafile, index.DataOffset, (int)index.DataLength);
                imageList.Add(new SSImage(index.Row, index.Col, buf));
            }

            return imageList;
        }

        // whiteboard
        public IDictionary<int, int> GetWBIndex(List<Index> indexs)
        {
            if (indexs == null || indexs.Count == 0)
                return null;

            IDictionary<int, int> minute_index_map = new Dictionary<int, int>();

            for (int i = 0; i < indexs.Count; i++)
            {
                if (!minute_index_map.ContainsKey(indexs[i].TimeStamp))
                {
                    minute_index_map.Add(new KeyValuePair<int, int>(indexs[i].TimeStamp, i));
                }
            }

            return minute_index_map;
        }

        public List<WBLine> GetWBImageData(string wbImageDataFile, List<Index> wbIndexList, IDictionary<int, int> wbImageIndex, int streamSize, TimeSpan tspan)
        {
            List<WBLine> wblines = new List<WBLine>();

            Index indeximage = null;
            if (wbImageIndex.ContainsKey((int)tspan.TotalMinutes))
                indeximage = wbIndexList[wbImageIndex[(int)tspan.TotalMinutes]];
            try
            {
                if (indeximage != null && indeximage.DataLength > 0)
                {
                    byte[] buf = _fileHelper.Seek(wbImageDataFile, indeximage.DataOffset, (int)indeximage.DataLength);
                    if (buf.Length == indeximage.DataLength)
                    {
                        //read data to index list
                        MemoryStream stream = new MemoryStream(buf);
                        BinaryReader breader = new BinaryReader(stream);
                        for (int i = 0; i < buf.Length / streamSize; i++)
                        {
                            wblines.Add(new WBLine((ushort)breader.ReadInt16(), (ushort)breader.ReadInt16(), (ushort)breader.ReadInt16(),
                                (ushort)breader.ReadInt16(), (short)breader.ReadInt16(), (ushort)breader.ReadInt16()));
                        }
                    }
                }
            }
            catch (Exception)
            {


            }

            return wblines;
        }

        public List<WBEvent> GetWBSequenceData(string wbSequenceDataFile, List<Index> wbIndexList, IDictionary<int, int> wbSequenceIndex, int streamSize, TimeSpan tspan)
        {
            List<WBEvent> wbEvents = new List<WBEvent>();

            Index indeximage = null;
            if (wbSequenceIndex.ContainsKey((int)tspan.TotalMinutes))
                indeximage = wbIndexList[wbSequenceIndex[(int)tspan.TotalMinutes]];
            try
            {
                if (indeximage != null && indeximage.DataLength > 0)
                {
                    byte[] buf = _fileHelper.Seek(wbSequenceDataFile, indeximage.DataOffset, (int)indeximage.DataLength);
                    if (buf.Length == indeximage.DataLength)
                    {
                        //read data to index list
                        MemoryStream stream = new MemoryStream(buf);
                        BinaryReader breader = new BinaryReader(stream);
                        for (int i = 0; i < buf.Length / streamSize; i++)
                        {
                            wbEvents.Add(new WBEvent((uint)(breader.ReadUInt16()), (ushort)breader.ReadInt16(), (int)breader.ReadInt16(), (int)breader.ReadInt16()));
                        }
                    }
                }
            }
            catch (Exception)
            {


            }

            return wbEvents;
        }


        static public List<T> Read<T>(int TStreamSize, byte[] databuf)
        {
            List<T> list = new List<T>();

            MemoryStream stream = new MemoryStream(databuf);
            BinaryReader breader = new BinaryReader(stream);

            try
            {
                for (int i = 0; i < databuf.Length / TStreamSize; i++)
                {
                    object[] args = new Object[1];
                    args[0] = breader;
                    T t = (T)Activator.CreateInstance(typeof(T), args);

                    list.Add(t);
                }
            }
            catch (Exception)
            {

            }

            return list;
        }      

        public void Close()
        {
            if (_fileHelper != null)
                _fileHelper.Close();
        }
    }
}
