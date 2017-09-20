using System;
using System.Collections.Generic;
using System.Text;
using Johnny.Kaixin.Core;

namespace Johnny.Kaixin.WinUI
{
    public interface IDlgSelection
    {
        string RadioCaption { get; set; }
        void FormLoad();
        void RefreshFriends();
        void BuildListView(FriendInfo user);
    }
}
