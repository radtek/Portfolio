using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections.ObjectModel;

using Johnny.Kaixin.Core;

namespace Johnny.Kaixin.WinUI
{
    public partial class DlgParkerSelection : DlgSelectionBase
    {
        private GamePark _gamePark;

        public DlgParkerSelection()
        {
            InitializeComponent();
            _gamePark = new GamePark();
            base.RadioAllCaption = "所有争车位的好友";
            base.RadioGameCaption = "目前有空车位的好友";
            base.ColumnCategory = "在线";            
        }

        protected override void FormLoad()
        {
            base.KaixinBase = _gamePark;
            _gamePark.ValidateCodeNeeded += new KaixinBase.ValidateCodeNeededEventHandler(_gamePark_ValidateCodeNeeded);
            _gamePark.ParkFriendsFetched += new GamePark.ParkFriendsFetchedEventHandler(_gamePark_ParkFriendsFetched);
            _gamePark.ParkEmptyGarageFriendsFetched += new GamePark.ParkEmptyGarageFriendsFetchedEventHandler(_gamePark_ParkEmptyGarageFriendsFetched);
        }

        void _gamePark_ValidateCodeNeeded(byte[] image, string taskid, string taskname)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new KaixinBase.ValidateCodeNeededEventHandler(_gamePark_ValidateCodeNeeded), new object[] { image, taskid, taskname });
                }
                else
                {
                    DlgPicCode picCode = new DlgPicCode();
                    picCode.ValidationImage = image;
                    picCode.WindowsCaption = base.VALIDATION_CAPTION;
                    if (picCode.ShowDialog() == DialogResult.OK)
                        _gamePark.ValidationCode = picCode.ValidationCode;
                    else
                        _gamePark.ValidationCode = null;
                }
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("DlgParkerSelection._gamePark_ValidateCodeNeeded", ex);
            }
        }

        void _gamePark_ParkFriendsFetched(Collection<FriendInfo> friends)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new GamePark.ParkFriendsFetchedEventHandler(_gamePark_ParkFriendsFetched), new object[] { friends });
                }
                else
                {
                    if (friends != null && friends.Count > 0)
                    {
                        base.AllMyFriend = _gamePark.ParkFriendsList;
                        base.BuildListView(base.AllMyFriend);
                    }
                    base.EnableControls(true);
                }
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("DlgParkerSelection", ex);
            }
        }

        void _gamePark_ParkEmptyGarageFriendsFetched(Collection<FriendInfo> friends)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new GamePark.ParkEmptyGarageFriendsFetchedEventHandler(_gamePark_ParkEmptyGarageFriendsFetched), new object[] { friends });
                }
                else
                {
                    if (friends != null && friends.Count > 0)
                    {
                        base.GameUser = _gamePark.ParkEmptyGarageFriendsList;
                        base.BuildListView(base.GameUser);
                    }
                    base.EnableControls(true);
                }
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("DlgParkerSelection", ex);
            }
        }

        protected override void RefreshAllFriends()
        {
            _gamePark.GetParkFriendsByThread();
        }

        protected override void RefreshGameFriends()
        {
            _gamePark.GetParkEmptyGarageFriendsByThread();
        }

        protected override string[] BuildListView(FriendInfo user)
        {
            string[] subItem = new string[3];
            subItem[0] = user.Name;
            subItem[1] = user.Id.ToString();
            subItem[2] = user.Online ? "是" : "否";
            return subItem;
        }

        public GamePark GamePark
        {
            get { return _gamePark; }
            set { _gamePark = value; }
        }
    }
}