using System;
using System.Collections.Generic;
using System.Text;

namespace Johnny.Kaixin.Controls.AccountTree
{
    [Flags]
    [Serializable]
    public enum NodeType
    {
        Base = 0,
        Group,
        Account        
    }

    public enum IconType
    {
        Users = 0,
        User,
        AddUser,
        Keys,
        Delete,
        WebBrowser,
        OperationConfig,
        Refresh,
        Root,
        Xml
    } 
}
