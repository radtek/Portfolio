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
    public partial class DlgSlaveSelection : DlgSelectionBase
    {
        private GameSlave _gameSlave;

        public DlgSlaveSelection()
        {
            InitializeComponent();
            _gameSlave = new GameSlave();
            base.RadioGameCaption = "当前可买的奴隶";
            base.ColumnCategory = "价格";            
        }

        protected override void FormLoad()
        {
            base.KaixinBase = _gameSlave;
            _gameSlave.ValidateCodeNeeded += new KaixinBase.ValidateCodeNeededEventHandler(_gameSlave_ValidateCodeNeeded);
            _gameSlave.AllMyFriendsFetched += new KaixinBase.AllMyFriendsFetchedEventHandler(_gameSlave_AllMyFriendsFetched);
            _gameSlave.BuyableSlavesFetched += new GameSlave.BuyableSlavesFetchedEventHandler(_gameSlave_BuyableSlavesFetched);
        }

        void _gameSlave_ValidateCodeNeeded(byte[] image, string taskid, string taskname)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new KaixinBase.ValidateCodeNeededEventHandler(_gameSlave_ValidateCodeNeeded), new object[] { image, taskid, taskname });
                }
                else
                {
                    DlgPicCode picCode = new DlgPicCode();
                    picCode.ValidationImage = image;
                    picCode.WindowsCaption = base.VALIDATION_CAPTION;
                    if (picCode.ShowDialog() == DialogResult.OK)
                        _gameSlave.ValidationCode = picCode.ValidationCode;
                    else
                        _gameSlave.ValidationCode = null;
                }
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("DlgSlaveSelection._gameSlave_ValidateCodeNeeded", ex);
            }
        }

        void _gameSlave_AllMyFriendsFetched(Collection<FriendInfo> allmyfriends)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new KaixinBase.AllMyFriendsFetchedEventHandler(_gameSlave_AllMyFriendsFetched), new object[] { allmyfriends });
                }
                else
                {
                    if (allmyfriends != null && allmyfriends.Count > 0)
                    {
                        base.AllMyFriend = _gameSlave.AllMyFriendsList;
                        base.BuildListView(base.AllMyFriend);
                    }
                    base.EnableControls(true);
                }
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("DlgSlaveSelection", ex);
            }
        }

        void _gameSlave_BuyableSlavesFetched(Collection<FriendInfo> friends)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new GameSlave.BuyableSlavesFetchedEventHandler(_gameSlave_BuyableSlavesFetched), new object[] { friends });
                }
                else
                {
                    if (friends != null && friends.Count > 0)
                    {
                        base.GameUser = _gameSlave.BuyableSlaveList;
                        base.BuildListView(base.GameUser);
                    }
                    base.EnableControls(true);
                }
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("DlgSlaveSelection", ex);
            }
        }

        protected override void RefreshAllFriends()
        {
            _gameSlave.GetAllMyFriendsByThread();
        }


        protected override void RefreshGameFriends()
        {
            _gameSlave.GetBuyableSlavesByThread();
        }

        protected override string[] BuildListView(FriendInfo user)
        {
            string[] subItem = new string[3];
            subItem[0] = user.Name;
            subItem[1] = user.Id.ToString();
            if (user.Price > 0)
                subItem[2] = user.Price.ToString();
            else
                subItem[2] = "";           
            return subItem;
        }

        public GameSlave GameSlave
        {
            get { return _gameSlave; }
            set { _gameSlave = value; }
        }
    }
}