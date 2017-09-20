using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace COL.Core
{
    public class Utility
    {
        public static string Root = "";
        public static string Course = "";
        public static string LectureId = "";
        

        public static string GetFilePath(FileType ft)
        {
            return GetFilePath(Root, Course, LectureId, ft);
        }
        public static string GetFilePath(string root, string course, string lectureid, FileType ft)
        {
            string file = "";
            switch (ft)
            {
                case FileType.ScreenshotImageIndex:
                    file = Path.Combine("ScreenShot", "High", "package.pak");
                    break;
                case FileType.ScreenshotImageData:
                    file = Path.Combine("ScreenShot", "High", "1.pak");
                    break;
                case FileType.ScreenshotSequenceIndex:
                    file = Path.Combine("ScreenShot", "MMSeq", "package.pak");
                    break;
                case FileType.ScreenshotSequenceData:
                    file = Path.Combine("ScreenShot", "MMSeq", "1.pak");
                    break;
                case FileType.Whiteboard1ImageIndex:
                    file = Path.Combine("WB", "1", "VectorImage", "package.pak");
                    break;
                case FileType.Whiteboard1ImageData:
                    file = Path.Combine("WB", "1", "VectorImage", "1.pak");
                    break;
                case FileType.Whiteboard1SequenceIndex:
                    file = Path.Combine("WB", "1", "VectorSequence", "package.pak");
                    break;
                case FileType.Whiteboard1SequenceData:
                    file = Path.Combine("WB", "1", "VectorSequence", "1.pak");
                    break;
                case FileType.Whiteboard2ImageIndex:
                    file = Path.Combine("WB", "2", "VectorImage", "package.pak");
                    break;
                case FileType.Whiteboard2ImageData:
                    file = Path.Combine("WB", "2", "VectorImage", "1.pak");
                    break;
                case FileType.Whiteboard2SequenceIndex:
                    file = Path.Combine("WB", "2", "VectorSequence", "package.pak");
                    break;
                case FileType.Whiteboard2SequenceData:
                    file = Path.Combine("WB", "2", "VectorSequence", "1.pak");
                    break;
                default:
                    break;
            }

            if (String.IsNullOrEmpty(file))
                return "";
            else
                //return Path.Combine(root, course, lectureid, file);
                return Path.Combine(root, lectureid, file);
        }

    }
}
