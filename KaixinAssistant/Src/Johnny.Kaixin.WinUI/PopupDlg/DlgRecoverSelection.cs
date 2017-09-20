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
    public partial class DlgRecoverSelection : DlgSelectionBase
    {
        private GameBite _gameBite;

        public DlgRecoverSelection()
        {
            InitializeComponent();
            _gameBite = new GameBite();
            base.RadioGameCaption = "我能休息的房间";
            base.ColumnCategory = "状态";            
        }

        protected override void FormLoad()
        {
            base.KaixinBase = _gameBite;
            _gameBite.ValidateCodeNeeded += new KaixinBase.ValidateCodeNeededEventHandler(_gameBite_ValidateCodeNeeded);
            _gameBite.AllMyFriendsFetched += new KaixinBase.AllMyFriendsFetchedEventHandler(_gameBite_AllMyFriendsFetched);
            _gameBite.RestableFriendsFetched += new GameBite.RestableFriendsFetchedEventHandler(_gameBite_RestableFriendsFetched);
        }

        void _gameBite_ValidateCodeNeeded(byte[] image, string taskid, string taskname)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new KaixinBase.ValidateCodeNeededEventHandler(_gameBite_ValidateCodeNeeded), new object[] { image, taskid, taskname });
                }
                else
                {
                    DlgPicCode picCode = new DlgPicCode();
                    picCode.ValidationImage = image;
                    picCode.WindowsCaption = base.VALIDATION_CAPTION;
                    if (picCode.ShowDialog() == DialogResult.OK)
                        _gameBite.ValidationCode = picCode.ValidationCode;
                    else
                        _gameBite.ValidationCode = null;
                }
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("DlgRecoverSelection._gameBite_ValidateCodeNeeded", ex);
            }
        }

        void _gameBite_AllMyFriendsFetched(Collection<FriendInfo> allmyfriends)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new KaixinBase.AllMyFriendsFetchedEventHandler(_gameBite_AllMyFriendsFetched), new object[] { allmyfriends });
                }
                else
                {
                    if (allmyfriends != null && allmyfriends.Count > 0)
                    {
                        base.AllMyFriend = _gameBite.AllMyFriendsList;
                        base.BuildListView(base.AllMyFriend);
                    }
                    base.EnableControls(true);
                }
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("DlgRecoverSelection", ex);
            }
        }

        void _gameBite_RestableFriendsFetched(Collection<FriendInfo> friends)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new GameBite.RestableFriendsFetchedEventHandler(_gameBite_RestableFriendsFetched), new object[] { friends });
                }
                else
                {
                    if (friends != null && friends.Count > 0)
                    {
                        base.GameUser = _gameBite.RestableFriendsList;
                        base.BuildListView(base.GameUser);
                    }
                    base.EnableControls(true);
                }
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("DlgRecoverSelection", ex);
            }
        }

        protected override void RefreshAllFriends()
        {
            _gameBite.GetAllMyFriendsByThread();
        }

        protected override void RefreshGameFriends()
        {
            _gameBite.GetRestableFriendsByThread();
        }

        protected override string[] BuildListView(FriendInfo user)
        {
            string[] subItem = new string[3];
            subItem[0] = user.Name;
            subItem[1] = user.Id.ToString();
            subItem[2] = user.Status;
            return subItem;
        }

        public GameBite GameBite
        {
            get { return _gameBite; }
            set { _gameBite = value; }
        }
    }
}