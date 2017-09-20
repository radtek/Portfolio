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
    public partial class DlgGardenSelection : DlgSelectionBase
    {
        private GameGarden _gameGarden;

        public DlgGardenSelection()
        {
            InitializeComponent();
            _gameGarden = new GameGarden();
            base.RadioAllCaption = "所有花园的好友";
            base.RadioGameCaption = "花园中有成熟果实的好友";
            base.ColumnCategory = "";            
        }

        protected override void FormLoad()
        {
            base.KaixinBase = _gameGarden;
            _gameGarden.ValidateCodeNeeded += new KaixinBase.ValidateCodeNeededEventHandler(_gameGarden_ValidateCodeNeeded);
            _gameGarden.AllGardenFriendsFetched += new GameGarden.AllGardenFriendsFetchedEventHandler(_gameGarden_AllGardenFriendsFetched);
            _gameGarden.MatureFriendsFetched += new GameGarden.MatureFriendsFetchedEventHandler(_gameGarden_MatureFriendsFetched);
        }

        void _gameGarden_ValidateCodeNeeded(byte[] image, string taskid, string taskname)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new KaixinBase.ValidateCodeNeededEventHandler(_gameGarden_ValidateCodeNeeded), new object[] { image, taskid, taskname });
                }
                else
                {
                    DlgPicCode picCode = new DlgPicCode();
                    picCode.ValidationImage = image;
                    picCode.WindowsCaption = base.VALIDATION_CAPTION;
                    if (picCode.ShowDialog() == DialogResult.OK)
                        _gameGarden.ValidationCode = picCode.ValidationCode;
                    else
                        _gameGarden.ValidationCode = null;
                }
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("DlgGardenSelection._gameGarden_ValidateCodeNeeded", ex);
            }
        }

        void _gameGarden_AllGardenFriendsFetched(Collection<FriendInfo> allgardenfriends)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new GameGarden.AllGardenFriendsFetchedEventHandler(_gameGarden_AllGardenFriendsFetched), new object[] { allgardenfriends });
                }
                else
                {
                    if (allgardenfriends != null && allgardenfriends.Count > 0)
                    {
                        base.AllMyFriend = _gameGarden.AllGardenFriendsList;
                        base.BuildListView(base.AllMyFriend);
                    }
                    base.EnableControls(true);
                }
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("DlgGardenSelection", ex);
            }
        }

        void _gameGarden_MatureFriendsFetched(Collection<FriendInfo> friends)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new GameGarden.MatureFriendsFetchedEventHandler(_gameGarden_MatureFriendsFetched), new object[] { friends });
                }
                else
                {
                    if (friends != null && friends.Count > 0)
                    {
                        base.GameUser = _gameGarden.MatureFriendsList;
                        base.BuildListView(base.GameUser);
                    }
                    base.EnableControls(true);
                }
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("DlgGardenSelection", ex);
            }
        }        

        protected override void RefreshAllFriends()
        {
            _gameGarden.GetAllGardenFriendsByThread();
        }

        protected override void RefreshGameFriends()
        {
            _gameGarden.GetMatureFriendsByThread();
        }

        protected override string[] BuildListView(FriendInfo user)
        {
            string[] subItem = new string[3];
            subItem[0] = user.Name;
            subItem[1] = user.Id.ToString();
            //subItem[2] = user.Gender ? "男" : "女";
            return subItem;
        }

        public GameGarden GameGarden
        {
            get { return _gameGarden; }
            set { _gameGarden = value; }
        }
    }
}