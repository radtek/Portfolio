using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace Johnny.Kaixin.Core
{
    public class AddFriendsInfo
    {
        private bool _addMode;
        private bool _deleteAllMessages;
        private bool _executeSendRequest;
        private bool _executeConfirmRequest;
        private Collection<AccountInfo> _newIdList = new Collection<AccountInfo>();
        private Collection<AccountInfo> _oldIdList = new Collection<AccountInfo>();
        private Collection<AccountInfo> _accounts;

        public AddFriendsInfo()
        {
            _addMode = true;
            _deleteAllMessages = false;
            _executeSendRequest = true;
            _executeConfirmRequest = true;
            _accounts = new Collection<AccountInfo>();
        }

        public bool AddMode
        {
            get { return _addMode; }
            set { _addMode = value; }
        }

        public bool DeleteAllMessages
        {
            get { return _deleteAllMessages; }
            set { _deleteAllMessages = value; }
        }

        public bool ExecuteSendRequest
        {
            get { return _executeSendRequest; }
            set { _executeSendRequest = value; }
        }

        public bool ExecuteConfirmRequest
        {
            get { return _executeConfirmRequest; }
            set { _executeConfirmRequest = value; }
        }

        public Collection<AccountInfo> NewAccountsList
        {
            get { return _newIdList; }
            set { _newIdList = value; }
        }

        public Collection<AccountInfo> OldAccountsList
        {
            get { return _oldIdList; }
            set { _oldIdList = value; }
        }

        public Collection<AccountInfo> Accounts
        {
            get { return _accounts; }
            set { _accounts = value; }
        }
    }
}
