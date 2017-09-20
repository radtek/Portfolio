using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COL.Core
{    
    public enum WBPenEvent
    {
        RedPenDown = -1,
        BluePenDown = -2,
        GreenPenDown = -3,
        BlackPenDown = -8,
        WideEraserDown = -9,
        NarrowEraserDown = -10,
        PenUp = -100,
        FunctionClear = -200
    };

    public enum WBPenColor
    {
        Red = -1,
        Blue = -2,
        Green = -3,
        Black = -8,
    };

    public enum WBEraserPenWidth
    {
        NarrowEraserWidth = 8 * 10 / 12,
        WideEraserWidth = 39 * 10 / 12
    };

    public enum DataType
    {
        ScreenShot = 0,
        WB_1 = 1,
        WB_2 = 2
    }
    public enum FileType
    {
        ScreenshotImageIndex = 0,
        ScreenshotImageData = 1,
        ScreenshotSequenceIndex = 2,
        ScreenshotSequenceData = 3,
        Whiteboard1ImageIndex = 4,
        Whiteboard1ImageData = 5,
        Whiteboard1SequenceIndex = 6,
        Whiteboard1SequenceData = 7,
        Whiteboard2ImageIndex = 8,
        Whiteboard2ImageData = 9,
        Whiteboard2SequenceIndex = 10,
        Whiteboard2SequenceData = 11
    }
}
