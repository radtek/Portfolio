using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

using Johnny.Kaixin.Core;

namespace Johnny.Kaixin.Controls.AccountTree
{
    public class GroupEventArgs : EventArgs
    {
        public string Group;
        public Collection<AccountInfo> Accounts;
    }

    public class AccountEventArgs : EventArgs
    {
        public string Group;
        public AccountInfo Account;
    }

    public class RootNodeEventArgs : EventArgs
    {
        public string[] Groups;
    }
}
