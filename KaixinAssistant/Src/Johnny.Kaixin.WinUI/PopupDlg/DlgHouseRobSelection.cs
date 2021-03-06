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
    public partial class DlgHouseRobSelection : DlgSelectionBase
    {
        private GameHouse _gameHouse;

        public DlgHouseRobSelection()
        {
            InitializeComponent();
            _gameHouse = new GameHouse();
            base.RadioAllCaption = "所有买房子的好友";
            base.RadioGameCaption = "露宿街头的好友";
            base.ColumnCategory = "性别";            
        }

        protected override void FormLoad()
        {
            base.KaixinBase = _gameHouse;
            _gameHouse.ValidateCodeNeeded += new KaixinBase.ValidateCodeNeededEventHandler(_gameHouse_ValidateCodeNeeded);
            _gameHouse.AllHouseFriendsFetched += new GameHouse.AllHouseFriendsFetchedEventHandler(_gameHouse_AllHouseFriendsFetched);
            _gameHouse.FreeFriendsFetched += new GameHouse.FreeFriendsFetchedEventHandler(_gameHouse_FreeFriendsFetched);
        }

        void _gameHouse_ValidateCodeNeeded(byte[] image, string taskid, string taskname)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new KaixinBase.ValidateCodeNeededEventHandler(_gameHouse_ValidateCodeNeeded), new object[] { image, taskid, taskname });
                }
                else
                {
                    DlgPicCode picCode = new DlgPicCode();
                    picCode.ValidationImage = image;
                    picCode.WindowsCaption = base.VALIDATION_CAPTION;
                    if (picCode.ShowDialog() == DialogResult.OK)
                        _gameHouse.ValidationCode = picCode.ValidationCode;
                    else
                        _gameHouse.ValidationCode = null;
                }
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("DlgHouseRobSelection._gameHouse_ValidateCodeNeeded", ex);
            }
        }
        
        void _gameHouse_AllHouseFriendsFetched(Collection<FriendInfo> allhousefriends)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new GameHouse.AllHouseFriendsFetchedEventHandler(_gameHouse_AllHouseFriendsFetched), new object[] { allhousefriends });
                }
                else
                {
                    if (allhousefriends != null && allhousefriends.Count > 0)
                    {
                        base.AllMyFriend = _gameHouse.AllHouseFriendsList;
                        base.BuildListView(base.AllMyFriend);
                    }
                    base.EnableControls(true);
                }
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("DlgHouseRobSelection", ex);
            }
        }

        void _gameHouse_FreeFriendsFetched(Collection<FriendInfo> friends)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new GameHouse.FreeFriendsFetchedEventHandler(_gameHouse_FreeFriendsFetched), new object[] { friends });
                }
                else
                {
                    if (friends != null && friends.Count > 0)
                    {
                        base.GameUser = _gameHouse.FreeFriendsList;
                        base.BuildListView(base.GameUser);
                    }
                    base.EnableControls(true);
                }
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("DlgHouseRobSelection", ex);
            }
        }        

        protected override void RefreshAllFriends()
        {
            _gameHouse.GetAllHouseFriendsByThread();
        }

        protected override void RefreshGameFriends()
        {
            _gameHouse.GetFreeFriendsByThread();
        }

        protected override string[] BuildListView(FriendInfo user)
        {
            string[] subItem = new string[3];
            subItem[0] = user.Name;
            subItem[1] = user.Id.ToString();
            subItem[2] = user.Gender ? "男" : "女";
            return subItem;
        }

        public GameHouse GameHouse
        {
            get { return _gameHouse; }
            set { _gameHouse = value; }
        }
    }
}