using System;
using System.Collections.Generic;
using Johnny.Portfolio.CoursePlayer.Core.OM;

namespace Johnny.Portfolio.CoursePlayer.Core
{
    public class CourseApi
    {
        const string ssIndexFile = "204304/ScreenShot/High/package.pak";
        const string ssDataFile = "204304/ScreenShot/High/1.pak";
        const string wbImageIndexFile = "204304/WB/1/VectorImage/package.pak";
        const string wbImageDataFile = "204304/WB/1/VectorImage/1.pak";
        const string wbSequenceIndexFile = "204304/WB/1/VectorSequence/package.pak";
        const string wbSequenceDataFile = "204304/WB/1/VectorSequence/1.pak";

        private FileApi fileApi = new FileApi();
        private FileApi file2Api = new FileApi();
        private FileApi file3Api = new FileApi();
        // Screenshot
        private List<Index> ssIndexList = new List<Index>();
        private IDictionary<int, int> ssIndexMap = new Dictionary<int, int>();
        // Whiteboard
        private List<Index> wbImageIndexList;
        private IDictionary<int, int> wbImageIndex;
        private List<Index> wbSequenceIndexList;
        private IDictionary<int, int> wbSequenceIndex;

        public CourseApi()
        {
        }

        public List<SSImage> GetScreenshotData(int second) {
            if (ssIndexList == null || ssIndexList.Count == 0)
            {
                var buffer = fileApi.GetIndexFile(ssIndexFile);
                ssIndexList = fileApi.GetIndexList(buffer);

                ssIndexMap.Clear();
                for (int i = 0; i < ssIndexList.Count; i++)
                {
                    if (!ssIndexMap.ContainsKey(ssIndexList[i].TimeStamp))
                    {
                        ssIndexMap.Add(new KeyValuePair<int, int>(ssIndexList[i].TimeStamp, i));
                    }
                }
            }

            var ssIndex = fileApi.GetSSIndex(ssIndexList, ssIndexMap, second);
            return fileApi.GetSSData(ssDataFile, ssIndex);
        }

        public WBData GetWhiteboardData(int second)
        {
            // get lines
            List<WBLine> lines = GetWBImageData(second);
            // get events
            List<WBEvent> events = GetWBSequenceData(second);
            // combine them to whiteboard data
            WBData wb = new WBData(lines, events);
            return wb;
        }

        private List<WBLine> GetWBImageData(int second)
        {
            try
            {
                if (wbImageIndex == null)
                {
                    var buffer = file2Api.GetIndexFile(wbImageIndexFile);
                    wbImageIndexList = file2Api.GetIndexList(buffer);
                    wbImageIndex = file2Api.GetWBIndex(wbImageIndexList);
                }

                TimeSpan tspan = TimeSpan.FromSeconds(second);
                List<WBLine> lines = file2Api.GetWBImageData(wbImageDataFile, wbImageIndexList, wbImageIndex, WBLine.StreamSize, tspan);
                return lines;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private List<WBEvent> GetWBSequenceData(int second)
        {
            try
            {
                if (wbSequenceIndex == null)
                {
                    var buffer = file3Api.GetIndexFile(wbSequenceIndexFile);
                    wbSequenceIndexList = file3Api.GetIndexList(buffer);
                    wbSequenceIndex = file3Api.GetWBIndex(wbSequenceIndexList);
                }

                TimeSpan tspan = TimeSpan.FromSeconds(second);
                List<WBEvent> events = file3Api.GetWBSequenceData(wbSequenceDataFile, wbSequenceIndexList, wbSequenceIndex, WBEvent.StreamSize, tspan);
                return events;

            }
            catch (Exception)
            {
                return null;
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
    }
}
