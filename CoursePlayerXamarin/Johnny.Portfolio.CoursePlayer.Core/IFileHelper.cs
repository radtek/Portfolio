using System.Collections.Generic;

namespace Johnny.Portfolio.CoursePlayer.Core
{
    public interface IFileHelper
    {
        bool Exists(string filename);

        void WriteText(string filename, string text);

        string ReadText(string filename);

        IEnumerable<string> GetFiles();

        void Delete(string filename);

        byte[] ReadBytes(string filename);

        byte[] Seek(string filename, int offset, int length);

        void Close();
    }
}
