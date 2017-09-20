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
    public partial class DlgFishSelection : DlgSelectionBase
    {
        private GameFish _gameFish;

        public DlgFishSelection()
        {
            InitializeComponent();
            _gameFish = new GameFish();
            base.RadioGameCaption = "所有钓鱼的好友";
            base.ColumnCategory = "";            
        }

        protected override void FormLoad()
        {
            base.KaixinBase = _gameFish;
            _gameFish.ValidateCodeNeeded += new KaixinBase.ValidateCodeNeededEventHandler(_gameFish_ValidateCodeNeeded);
            _gameFish.AllMyFriendsFetched += new KaixinBase.AllMyFriendsFetchedEventHandler(_gameFish_AllMyFriendsFetched);
            _gameFish.AllFishFriendsFetched += new GameFish.AllFishFriendsFetchedEventHandler(_gameFish_AllFishFriendsFetched);
        }

        void _gameFish_ValidateCodeNeeded(byte[] image, string taskid, string taskname)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new KaixinBase.ValidateCodeNeededEventHandler(_gameFish_ValidateCodeNeeded), new object[] { image, taskid, taskname });
                }
                else
                {
                    DlgPicCode picCode = new DlgPicCode();
                    picCode.ValidationImage = image;
                    picCode.WindowsCaption = base.VALIDATION_CAPTION;
                    if (picCode.ShowDialog() == DialogResult.OK)
                        _gameFish.ValidationCode = picCode.ValidationCode;
                    else
                        _gameFish.ValidationCode = null;
                }
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("DlgFishSelection._gameRanch_ValidateCodeNeeded", ex);
            }
        }

        void _gameFish_AllMyFriendsFetched(Collection<FriendInfo> allmyfriends)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new KaixinBase.AllMyFriendsFetchedEventHandler(_gameFish_AllMyFriendsFetched), new object[] { allmyfriends });
                }
                else
                {
                    if (allmyfriends != null && allmyfriends.Count > 0)
                    {
                        base.AllMyFriend = _gameFish.AllMyFriendsList;
                        base.BuildListView(base.AllMyFriend);
                    }
                    base.EnableControls(true);
                }
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("DlgFishSelection._gameFish_AllMyFriendsFetched", ex);
            }
        }

        void _gameFish_AllFishFriendsFetched(Collection<FriendInfo> allfishfriends)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new GameRanch.AllRanchFriendsFetchedEventHandler(_gameFish_AllFishFriendsFetched), new object[] { allfishfriends });
                }
                else
                {
                    if (allfishfriends != null && allfishfriends.Count > 0)
                    {
                        base.GameUser = _gameFish.AllFishFriendsList;
                        base.BuildListView(base.GameUser);
                    }
                    base.EnableControls(true);
                }
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("DlgFishSelection._gameFish_AllFishFriendsFetched", ex);
            }
        }        

        protected override void RefreshAllFriends()
        {
            _gameFish.GetAllMyFriendsByThread();
        }

        protected override void RefreshGameFriends()
        {
            _gameFish.GetAllFishFriendsByThread();
        }

        protected override string[] BuildListView(FriendInfo user)
        {
            string[] subItem = new string[3];
            subItem[0] = user.Name;
            subItem[1] = user.Id.ToString();
            //subItem[2] = user.Gender ? "男" : "女";
            return subItem;
        }

        public GameFish GameFish
        {
            get { return _gameFish; }
            set { _gameFish = value; }
        }
    }
}