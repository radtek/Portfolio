using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Community.CsharpSqlite;
using SQLiteClient;
using System.IO.IsolatedStorage;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Johnny.WP7.Dictionary
{
    public class SQLiteHelper
    {
        private String _dbName;
        private SQLiteConnection db = null;

        public SQLiteHelper(String dbName)
        {
            IsolatedStorageFile store =
              IsolatedStorageFile.GetUserStoreForApplication();
            if (!store.FileExists(dbName))
            {
                CopyFromContentToStorage(dbName);
            }
            _dbName = dbName;
        }

        private void Open()
        {
            if (db == null)
            {
                db = new SQLiteConnection(_dbName);
                db.Open();
            }
        }

        private void Close()
        {
            if (db != null)
            {
                db.Dispose();
                db = null;
            }
        }

        public List<T> SelectList<T>(String statement) where T : new()
        {
            Open();
            SQLiteCommand cmd = db.CreateCommand(statement);
            var lst = cmd.ExecuteQuery<T>();
            return lst.ToList();
        }

        public ObservableCollection<T> SelectObservableCollection<T>(String statement)
            where T : new()
        {
            List<T> lst = SelectList<T>(statement);
            ObservableCollection<T> oc = new ObservableCollection<T>();
            foreach (T item in lst)
            {
                oc.Add(item);
            }
            return oc;
        }

        private void CopyFromContentToStorage(String dbName)
        {
            IsolatedStorageFile store =
                IsolatedStorageFile.GetUserStoreForApplication();
            System.IO.Stream src =
                Application.GetResourceStream(
                  new Uri(@"/Johnny.WP7.Dictionary;component/Db/" + dbName,
                          UriKind.Relative)).Stream;
            IsolatedStorageFileStream dest =
                new IsolatedStorageFileStream(dbName,
                  System.IO.FileMode.OpenOrCreate,
                  System.IO.FileAccess.Write, store);
            src.Position = 0;
            CopyStream(src, dest);
            dest.Flush();
            dest.Close();
            src.Close();
            dest.Dispose();
        }

        private static void CopyStream(System.IO.Stream input,
                                       IsolatedStorageFileStream output)
        {
            byte[] buffer = new byte[32768];
            long TempPos = input.Position;
            int readCount;
            do
            {
                readCount = input.Read(buffer, 0, buffer.Length);
                if (readCount > 0)
                {
                    output.Write(buffer, 0, readCount);
                }
            } while (readCount > 0);
            input.Position = TempPos;
        }
    }
}
