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
        IDictionary<string, FileStream> dictionary = new Dictionary<string, FileStream>();

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
                FileStream indexstream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
                BinaryReader breader = new BinaryReader(indexstream);

                return breader.ReadBytes((int)indexstream.Length);

            }
            catch (Exception)
            {
                return null;
            }
        }

        public byte[] Seek(string filename, int offset, int length)
        {
            try
            {
                byte[] buf = new byte[length];
                FileStream fs = null;
                if (dictionary.ContainsKey(filename))
                {
                    fs = dictionary[filename];
                }
                else
                {
                    //make sure DependencyFetchTarget.NewInstance is set
                    fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
                    dictionary.Add(filename, fs);
                }
                fs.Seek(offset, SeekOrigin.Begin);
                fs.Read(buf, 0, length);
                return buf;
            }
            catch (Exception)
            {
                return null;
            }            
        }

        public void Close()
        {
            foreach (KeyValuePair<string, FileStream> entry in dictionary)
            {
                if (entry.Value != null)
                {
                    entry.Value.Close();
                }
            }
            dictionary.Clear();
        }

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