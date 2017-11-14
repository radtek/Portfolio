using System;

namespace Johnny.Portfolio.CoursePlayer.Core
{
    class COLFilerHelper
    {
        private FileHelper _fileHelper;
        private string _indexfilename;
        private string _datafilename;

        public COLFilerHelper(string indexfilename, string datafilename)
        {
            _fileHelper = new FileHelper();
            _indexfilename = indexfilename;
            _datafilename = datafilename;
        }

        public IndexList GetIndexList()
        {
            try
            {
                byte[] indexData = _fileHelper.ReadBytes(_indexfilename);
                return new IndexList(indexData);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public byte[] SeekData(int offset, int length)
        {
            try
            {
                return _fileHelper.Seek(_datafilename, offset, length);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public void Close()
        {
            try
            {
                _fileHelper.Close();
            }
            catch (Exception ex)
            {
            } 
        }
        
    }
}
