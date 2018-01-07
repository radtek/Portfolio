using System;
using System.Collections.Generic;
using Johnny.Portfolio.CoursePlayer.Core.OM;

namespace Johnny.Portfolio.CoursePlayer.Core
{
    public class CourseApi
    {
        private FileApi fileApi = new FileApi();
        private FileApi file2Api = new FileApi();
        private FileApi file3Api = new FileApi();
        private List<Index> ssIndexList = new List<Index>();
        IDictionary<int, int> mapIndex = new Dictionary<int, int>();

        List<Index> wbImageIndexList;
        IDictionary<int, int> wbImageIndex;
        List<Index> wbSequenceIndexList;
        IDictionary<int, int> wbSequenceIndex;

        public CourseApi()
        {
        }

        public List<SSImage> GetScreenshotData(int second) {
            if (ssIndexList == null || ssIndexList.Count == 0)
            {
                var buffer = fileApi.GetIndexFile(GetFilePath(DataType.ScreenShot, false));
                ssIndexList = fileApi.GetIndexList(buffer);

                mapIndex.Clear();
                for (int i = 0; i < ssIndexList.Count; i++)
                {
                    if (!mapIndex.ContainsKey(ssIndexList[i].TimeStamp))
                    {
                        mapIndex.Add(new KeyValuePair<int, int>(ssIndexList[i].TimeStamp, i));
                    }
                }
            }

            var ssIndex = fileApi.GetSSIndex(ssIndexList, mapIndex, second);
            return fileApi.GetSSData(GetFilePath2(DataType.ScreenShot, false), ssIndex);
        }

        public WBData GetWhiteboardData(int second)
        {
            // get lines
            List<WBLine> lines = GetWBImageData(second);
            // get events
            List<WBEvent> events = GetWBSequenceData(second);
            WBData wb = new WBData(lines, events);
            return wb;
        }
        private List<WBLine> GetWBImageData(int second)
        {
            try
            {
                if (wbImageIndex == null)
                {
                    var buffer = file2Api.GetIndexFile(GetFilePath(DataType.WB_1, false));
                    wbImageIndexList = file2Api.GetIndexList(buffer);
                    wbImageIndex = file2Api.GetWBIndex(wbImageIndexList);
                }

                List<WBLine> lines = new List<WBLine>();

                TimeSpan tspan = TimeSpan.FromSeconds(second);

                lines = file2Api.GetWBImageData(GetFilePath2(DataType.WB_1, false), wbImageIndexList, wbImageIndex, WBLine.StreamSize, tspan);

                return lines;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {

            }
        }

        private List<WBEvent> GetWBSequenceData(int second)
        {
            try
            {
                if (wbSequenceIndex == null)
                {
                    var buffer = file3Api.GetIndexFile(GetFilePath(DataType.WB_1, true));
                    wbSequenceIndexList = file3Api.GetIndexList(buffer);
                    wbSequenceIndex = file3Api.GetWBIndex(wbSequenceIndexList);
                }

                List<WBEvent> events = new List<WBEvent>();

                TimeSpan tspan = TimeSpan.FromSeconds(second);
                events = file3Api.GetWBSequenceData(GetFilePath2(DataType.WB_1, true), wbSequenceIndexList, wbSequenceIndex, WBEvent.StreamSize, tspan);
                return events;

            }
            catch (Exception)
            {
                return null;
            }
            finally
            {

            }
        }


        public void Close()
        {
            if (fileApi != null)
                fileApi.Close();
            if (file2Api != null)
                file2Api.Close();
            if (file3Api != null)
                file3Api.Close();
        }

        private String GetFilePath(DataType dt, bool wbseq)
        {
            string indexFileName = "";
            string dataFileName = string.Empty;

            Utility.LectureId = this.LectureId;

            /*Utility.Root = this.Root;
            Utility.LectureId = this.LectureId;
            Utility.Course = this.Course;
    */
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

            return indexFileName;
        }

        private String GetFilePath2(DataType dt, bool wbseq)
        {
            string indexFileName = "";
            string dataFileName = string.Empty;

            Utility.LectureId = this.LectureId;

            /*Utility.Root = this.Root;
            Utility.LectureId = this.LectureId;
            Utility.Course = this.Course;
    */
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

            return dataFileName;
        }

        public string LectureId { get; set; }
    }


}
