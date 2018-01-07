using System.Collections.Generic;


namespace Johnny.Portfolio.CoursePlayer.Core
{
    public class ScreenshotData
    {
        public ScreenshotData()
        {
            Images = new Dictionary<int, byte[]>();
        }

        //<position of the cell from 0 to 63, image data of the cell>
        public IDictionary<int, byte[]> Images { get; set; }

    }
}
