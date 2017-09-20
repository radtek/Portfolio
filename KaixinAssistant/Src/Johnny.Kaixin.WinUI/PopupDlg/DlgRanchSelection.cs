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
    public partial class DlgRanchSelection : DlgSelectionBase
    {
        private GameRanch _gameRanch;

        public DlgRanchSelection()
        {
            InitializeComponent();
            _gameRanch = new GameRanch();
            base.RadioAllCaption = "所有牧场的好友";
            base.RadioGameCaption = "牧场中有成熟农副产品的好友";
            base.ColumnCategory = "";            
        }

        protected override void FormLoad()
        {
            base.KaixinBase = _gameRanch;
            _gameRanch.ValidateCodeNeeded += new KaixinBase.ValidateCodeNeededEventHandler(_gameRanch_ValidateCodeNeeded);
            _gameRanch.AllRanchFriendsFetched += new GameRanch.AllRanchFriendsFetchedEventHandler(_gameRanch_AllRanchFriendsFetched);
            _gameRanch.AgriculturalProductFriendsFetched += new GameRanch.AgriculturalProductFriendsFetchedEventHandler(_gameRanch_AgriculturalProductFriendsFetched);
        }

        void _gameRanch_ValidateCodeNeeded(byte[] image, string taskid, string taskname)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new KaixinBase.ValidateCodeNeededEventHandler(_gameRanch_ValidateCodeNeeded), new object[] { image, taskid, taskname });
                }
                else
                {
                    DlgPicCode picCode = new DlgPicCode();
                    picCode.ValidationImage = image;
                    picCode.WindowsCaption = base.VALIDATION_CAPTION;
                    if (picCode.ShowDialog() == DialogResult.OK)
                        _gameRanch.ValidationCode = picCode.ValidationCode;
                    else
                        _gameRanch.ValidationCode = null;
                }
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("DlgRanchSelection._gameRanch_ValidateCodeNeeded", ex);
            }
        }

        void _gameRanch_AllRanchFriendsFetched(Collection<FriendInfo> allranchfriends)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new GameRanch.AllRanchFriendsFetchedEventHandler(_gameRanch_AllRanchFriendsFetched), new object[] { allranchfriends });
                }
                else
                {
                    if (allranchfriends != null && allranchfriends.Count > 0)
                    {
                        base.AllMyFriend = _gameRanch.AllRanchFriendsList;
                        base.BuildListView(base.AllMyFriend);
                    }
                    base.EnableControls(true);
                }
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("DlgRanchSelection._gameRanch_AllRanchFriendsFetched", ex);
            }
        }

        void _gameRanch_AgriculturalProductFriendsFetched(Collection<FriendInfo> friends)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new GameRanch.AgriculturalProductFriendsFetchedEventHandler(_gameRanch_AgriculturalProductFriendsFetched), new object[] { friends });
                }
                else
                {
                    if (friends != null && friends.Count > 0)
                    {
                        base.GameUser = _gameRanch.AgriculturalProductFriendsList;
                        base.BuildListView(base.GameUser);
                    }
                    base.EnableControls(true);
                }
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("DlgGardenSelection._gameRanch_AgriculturalProductFriendsFetched", ex);
            }
        }

        protected override void RefreshAllFriends()
        {
            _gameRanch.GetAllRanchFriendsByThread();
        }

        protected override void RefreshGameFriends()
        {
            _gameRanch.GetAgriculturalProductFriendsByThread();
        }

        protected override string[] BuildListView(FriendInfo user)
        {
            string[] subItem = new string[3];
            subItem[0] = user.Name;
            subItem[1] = user.Id.ToString();
            //subItem[2] = user.Gender ? "男" : "女";
            return subItem;
        }

        public GameRanch GameRanch
        {
            get { return _gameRanch; }
            set { _gameRanch = value; }
        }
    }
}