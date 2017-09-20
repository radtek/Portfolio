using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace COL.Core
{
    public class COLDataSource
    {
        private string _root = "";
        private string _course = "";
        private string _lectureid = "";
        COLFilerHelper colhelperWbImage;
        COLFilerHelper colhelperWbSequence;
        List<Index> wbImageIndexs;
        List<Index> wbSequenceIndexs;

        COLFilerHelper colhelperScreenImage;
        IndexList screenIndexList;
        //COLFilerHelper colhelperScreenSequence;
        
        //List<Index> screenSequenceIndexs;
        

        public void Close()
        {
            if (colhelperWbImage != null)
                colhelperWbImage.Close();
            if (colhelperWbSequence != null)
                colhelperWbSequence.Close();
            if (colhelperScreenImage != null)
                colhelperScreenImage.Close();
        }

        public ScreenData GetScreenshotData(DataType dt, int second)
        {
            ScreenData sd = new ScreenData();
            sd.Images = GetImages(dt, second);
            return sd;
        }

        private IDictionary<int, byte[]> GetImages(DataType dt, int second)
        {
            try
            {
                IDictionary<int, byte[]> ret = new Dictionary<int, byte[]>();

                if (screenIndexList == null)
                {
                    if (colhelperScreenImage == null)
                        colhelperScreenImage = GetCOLFilerHelper(dt);
                    screenIndexList = colhelperScreenImage.GetIndexList();
                }

                List<Index> screenImageIndexs = screenIndexList.GetImageIndexBySecond(second);

                foreach (Index index in screenImageIndexs)
                {
                    byte[] buf = colhelperScreenImage.SeekData(index.DataOffset, (int)index.DataLength);
                    ret.Add(new KeyValuePair<int, byte[]>((index.Row * Constants.MAX_ROW_NO + index.Col), buf));
                }

                return ret;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {

            }
        }
        public WBData GetWhiteBoardData(DataType dt, int second)
        {
            WBData wb = new WBData();
            wb.WBLines = GetWhiteBoardImage(dt, second);
            wb.WBEvents = GetWhiteBoardSequence(dt, second);
            return wb;
        }
        private List<WBLine> GetWhiteBoardImage(DataType dt, int second)
        {
            try
            {
                if (wbImageIndexs == null)
                {
                    if (colhelperWbImage == null)
                        colhelperWbImage = GetCOLFilerHelper(dt, false);
                    IndexList list = colhelperWbImage.GetIndexList();
                    wbImageIndexs = list.GetIndexList();
                }
                IDictionary<int, int> mapIndex = GetIndexMapByMinute(wbImageIndexs);

                List<WBLine> lines = new List<WBLine>();

                TimeSpan tspan = TimeSpan.FromSeconds(second);

                lines = GetDataList<WBLine>(colhelperWbImage, wbImageIndexs, mapIndex, WBLine.StreamSize, tspan);

                return lines;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {

            }
        }

        private List<WBEvent> GetWhiteBoardSequence(DataType dt, int ts)
        {
            try
            {
                if (wbSequenceIndexs == null)
                {
                    if (colhelperWbSequence == null)
                        colhelperWbSequence = GetCOLFilerHelper(dt, true);
                    IndexList list = colhelperWbSequence.GetIndexList();
                    wbSequenceIndexs = list.GetIndexList();
                }

                IDictionary<int, int> mapIndex = GetIndexMapByMinute(wbSequenceIndexs);

                List<WBEvent> events = new List<WBEvent>();

                TimeSpan tspan = TimeSpan.FromSeconds(ts);
                events = GetDataList<WBEvent>(colhelperWbSequence, wbSequenceIndexs, mapIndex, WBEvent.StreamSize, tspan);

                return events;

            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {

            }
        }

        private IDictionary<int, int> GetIndexMapByMinute(List<Index> indexs)
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

        List<T> GetDataList<T>(COLFilerHelper colhelper, List<Index> indexlist, IDictionary<int, int> mapindex, int streamSize, TimeSpan tspan)
        {
            List<T> result = new List<T>();

            Index indeximage = null;
            if (mapindex.ContainsKey((int)tspan.TotalMinutes))
                indeximage = indexlist[mapindex[(int)tspan.TotalMinutes]];
            try
            {
                if (indeximage != null && indeximage.DataLength > 0)
                {
                    byte[] buf = colhelper.SeekData(indeximage.DataOffset, (int)indeximage.DataLength);
                    if (buf.Length == indeximage.DataLength)
                    {
                        result = Read<T>(streamSize, buf);
                    }
                }
            }
            catch (Exception e)
            {

            }

            return result;
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
            catch (Exception e)
            {

            }

            return list;
        }      


        public string Root
        {
            get { return _root; }
            set { _root = value; }
        }
        public string Course
        {
            get { return _course; }
            set { _course = value; }
        }
        public string LectureId
        {
            get { return _lectureid; }
            set { _lectureid = value; }
        }

        private COLFilerHelper GetCOLFilerHelper(DataType dt)
        {
            return GetCOLFilerHelper(dt, false);
        }

        private COLFilerHelper GetCOLFilerHelper(DataType dt, bool wbseq)
        {
            string indexFileName = "";
            string dataFileName = string.Empty;
            COLFilerHelper dataFile = null;
            Utility.Root = this.Root;
            Utility.LectureId = this.LectureId;
            Utility.Course = this.Course;

            switch (dt)
            {
                case DataType.ScreenShot:
                    indexFileName = Utility.GetFilePath(FileType.ScreenshotImageIndex);
                    dataFileName = Utility.GetFilePath(FileType.ScreenshotImageData); ;
                    break;
                case DataType.WB_1:
                    if (!wbseq)
                    {
                        indexFileName = Utility.GetFilePath(FileType.Whiteboard1ImageIndex);
                        dataFileName = Utility.GetFilePath(FileType.Whiteboard1ImageData); ;
                    }
                    else
                    {
                        indexFileName = Utility.GetFilePath(FileType.Whiteboard1SequenceIndex);
                        dataFileName = Utility.GetFilePath(FileType.Whiteboard1SequenceData);
                    }
                    break;
                case DataType.WB_2:
                    if (!wbseq)
                    {
                        indexFileName = Utility.GetFilePath(FileType.Whiteboard2ImageIndex);
                        dataFileName = Utility.GetFilePath(FileType.Whiteboard2ImageData); ;
                    }
                    else
                    {
                        indexFileName = Utility.GetFilePath(FileType.Whiteboard2SequenceIndex);
                        dataFileName = Utility.GetFilePath(FileType.Whiteboard2SequenceData);
                    }
                    break;
                default:
                    break;
            }

            if (!String.IsNullOrEmpty(indexFileName) && !String.IsNullOrEmpty(dataFileName))
                dataFile = new COLFilerHelper(indexFileName, dataFileName);

            return dataFile;
        }
    }
}
