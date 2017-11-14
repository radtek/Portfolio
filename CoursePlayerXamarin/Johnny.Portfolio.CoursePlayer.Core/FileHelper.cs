using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;

namespace Johnny.Portfolio.CoursePlayer.Core
{
    class FileHelper : IFileHelper
    {
        IFileHelper fileHelper = DependencyService.Get<IFileHelper>(DependencyFetchTarget.NewInstance);  //create new instance each time

        public bool Exists(string filename)
        {
            return fileHelper.Exists(filename);
        }

        public void WriteText(string filename, string text)
        {
            fileHelper.WriteText(filename, text);
        }

        public string ReadText(string filename)
        {
            return fileHelper.ReadText(filename);
        }

        public IEnumerable<string> GetFiles()
        {
            IEnumerable<string> filepaths = fileHelper.GetFiles();
            List<string> filenames = new List<string>();

            foreach (string filepath in filepaths)
            {
                filenames.Add(Path.GetFileName(filepath));
            }
            return filenames;
        }

        public void Delete(string filename)
        {
            fileHelper.Delete(filename);
        }

        public byte[] ReadBytes(string filename)
        {
            return fileHelper.ReadBytes(filename);
        }

        public byte[] Seek(string filename, int offset, int length)
        {
            return fileHelper.Seek(filename, offset, length);
        }

        public void Close()
        {
            fileHelper.Close();
        }
    }
}
