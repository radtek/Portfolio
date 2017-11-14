using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.IO;
using Johnny.Portfolio.CoursePlayer.Core;

[assembly: Dependency(typeof(Johnny.Portfolio.CoursePlayer.iOS.FileHelper))]

namespace Johnny.Portfolio.CoursePlayer.iOS
{
    public class FileHelper : IFileHelper
    {
        FileStream datastream;

        public bool Exists(string filename)
        {
            string filepath = GetFilePath(filename);
            return File.Exists(filepath);
        }

        public void WriteText(string filename, string text)
        {
            string filepath = GetFilePath(filename);
            File.WriteAllText(filepath, text);
        }

        public string ReadText(string filename)
        {
            string filepath = GetFilePath(filename);
            return File.ReadAllText(filepath);
        }

        public IEnumerable<string> GetFiles()
        {
            return Directory.GetFiles(GetDocsPath());
        }

        public void Delete(string filename)
        {
            File.Delete(GetFilePath(filename));
        }

        public byte[] ReadBytes(string filename)
        {
            try
            {
                FileStream indexstream = new System.IO.FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
                BinaryReader breader = new BinaryReader(indexstream);

                return breader.ReadBytes((int)indexstream.Length);

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public byte[] Seek(string filename, int offset, int length)
        {
            try
            {
                byte[] buf = new byte[length];
                if (datastream == null || datastream.CanRead == false) //make sure DependencyFetchTarget.NewInstance is set
                    datastream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);                
                datastream.Seek(offset, SeekOrigin.Begin);
                datastream.Read(buf, 0, length);
                return buf;
            }
            catch (Exception ex)
            {
                return null;
            }            
        }

        public void Close()
        {
            if (datastream != null)
            {
                datastream.Close();
            }
        }

        // Private methods.
        private string GetFilePath(string filename)
        {
            return Path.Combine(GetDocsPath(), filename);
        }

        private string GetDocsPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }
    }
}