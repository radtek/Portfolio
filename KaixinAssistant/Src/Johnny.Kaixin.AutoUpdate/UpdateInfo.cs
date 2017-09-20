using System;
using System.Collections.Generic;
using System.Text;

namespace Johnny.Kaixin.AutoUpdate
{
    [Serializable]
    public class UpdateInfo
    {
        private string _urlAddress;
        private string _updateTime;
        private string _version;
        private string[] _updateFileList;
        private string[] _deletionDirectoryList;
        private string[] _deletionFileList;
        private bool _reStart;
        private string _appName;

        public UpdateInfo()
        { }

        public string UrlAddress
        {
            get { return _urlAddress; }
            set { _urlAddress = value; }
        }

        public string UpdateTime
        {
            get { return _updateTime; }
            set { _updateTime = value; }
        }

        public string Version
        {
            get { return _version; }
            set { _version = value; }
        }

        public string[] UpdateFileList
        {
            get { return _updateFileList; }
            set { _updateFileList = value; }
        }

        public string[] DeletionDirectoryList
        {
            get { return _deletionDirectoryList; }
            set { _deletionDirectoryList = value; }
        }

        public string[] DeletionFileList
        {
            get { return _deletionFileList; }
            set { _deletionFileList = value; }
        }

        public bool ReStart
        {
            get { return _reStart; }
            set { _reStart = value; }
        }
        public string AppName
        {
            get { return _appName; }
            set { _appName = value; }
        }
    }
}
