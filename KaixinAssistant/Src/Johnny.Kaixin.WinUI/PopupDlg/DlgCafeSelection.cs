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
    public partial class DlgCafeSelection : DlgSelectionBase
    {
        private GameCafe _gameCafe;

        public DlgCafeSelection()
        {
            InitializeComponent();
            _gameCafe = new GameCafe();
            base.RadioAllCaption = "所有餐厅的好友";
            base.RadioGameCaption = "餐厅中可雇佣的好友";
            base.ColumnCategory = "";            
        }

        protected override void FormLoad()
        {
            base.KaixinBase = _gameCafe;
            _gameCafe.ValidateCodeNeeded += new KaixinBase.ValidateCodeNeededEventHandler(_gameGarden_ValidateCodeNeeded);
            _gameCafe.AllCafeFriendsFetched += new GameCafe.AllCafeFriendsFetchedEventHandler(_gameCafe_AllCafeFriendsFetched);
            _gameCafe.HirableFriendsFetched += new GameCafe.HirableFriendsFetchedEventHandler(_gameCafe_HirableFriendsFetched);
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
                        _gameCafe.ValidationCode = picCode.ValidationCode;
                    else
                        _gameCafe.ValidationCode = null;
                }
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("DlgCafeSelection._gameGarden_ValidateCodeNeeded", ex);
            }
        }

        void _gameCafe_AllCafeFriendsFetched(Collection<FriendInfo> allcafefriends)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new GameCafe.AllCafeFriendsFetchedEventHandler(_gameCafe_AllCafeFriendsFetched), new object[] { allcafefriends });
                }
                else
                {
                    if (allcafefriends != null && allcafefriends.Count > 0)
                    {
                        base.AllMyFriend = _gameCafe.AllCafeFriendsList;
                        base.BuildListView(base.AllMyFriend);
                    }
                    base.EnableControls(true);
                }
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("DlgCafeSelection._gameCafe_AllCafeFriendsFetched", ex);
            }
        }

        void _gameCafe_HirableFriendsFetched(Collection<FriendInfo> hirablefriends)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new GameCafe.HirableFriendsFetchedEventHandler(_gameCafe_HirableFriendsFetched), new object[] { hirablefriends });
                }
                else
                {
                    if (hirablefriends != null && hirablefriends.Count > 0)
                    {
                        base.GameUser = _gameCafe.HirableFriendsList;
                        base.BuildListView(base.GameUser);
                    }
                    base.EnableControls(true);
                }
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("DlgCafeSelection._gameCafe_HirableFriendsFetched", ex);
            }
        }        

        protected override void RefreshAllFriends()
        {
            _gameCafe.GetAllCafeFriendsByThread();
        }

        protected override void RefreshGameFriends()
        {
            _gameCafe.GetHirableFriendsByThread();
        }

        protected override string[] BuildListView(FriendInfo user)
        {
            string[] subItem = new string[3];
            subItem[0] = user.Name;
            subItem[1] = user.Id.ToString();
            //subItem[2] = user.Gender ? "男" : "女";
            return subItem;
        }

        public GameCafe GameCafe
        {
            get { return _gameCafe; }
            set { _gameCafe = value; }
        }
    }
}