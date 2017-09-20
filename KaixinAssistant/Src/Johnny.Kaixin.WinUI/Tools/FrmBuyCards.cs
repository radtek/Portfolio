using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Collections.ObjectModel;

using Johnny.Kaixin.Core;
using Johnny.Kaixin.Helper;

namespace Johnny.Kaixin.WinUI
{
    public partial class FrmBuyCards : FrmToolBase
    {
        private Collection<AccountInfo> _accounts;
        private ToolBuyCards _toolbuycards;

        //public delegate void MessageChangedEventHandler(string caption, string key, string message);
        //public event MessageChangedEventHandler messageChanged;

        public FrmBuyCards()
        {
            InitializeComponent();
            _toolbuycards = new ToolBuyCards();
            _toolbuycards.MessageChanged += new KaixinBase.MessageChangedEventHandler(_toolbuycards_MessageChanged);
            _toolbuycards.ValidateCodeNeeded += new KaixinBase.ValidateCodeNeededEventHandler(_toolbuycards_ValidateCodeNeeded);
            _toolbuycards.BuyCardsFinished += new GamePark.BuyCardsFinishedEventHandler(_toolbuycards_BuyCardsFinished);
        }

        void _toolbuycards_ValidateCodeNeeded(byte[] image, string taskid, string taskname)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new KaixinBase.ValidateCodeNeededEventHandler(_toolbuycards_ValidateCodeNeeded), new object[] { image, taskid, taskname });
            }
            else
            {
                DlgPicCode picCode = new DlgPicCode();
                picCode.ValidationImage = image;
                picCode.WindowsCaption = "�������";
                if (picCode.ShowDialog() == DialogResult.OK)
                    _toolbuycards.ValidationCode = picCode.ValidationCode;
                else
                    _toolbuycards.ValidationCode = null;
            }
        }

        #region FrmBuyCards_Load
        private void FrmBuyCards_Load(object sender, EventArgs e)
        {
            try
            {
                //build group combox
                string[] groups = ConfigCtrl.GetGroups();
                if (groups != null)
                {
                    foreach (string group in groups)
                    {
                        cmbGroup.Items.Add(group);
                    }
                }

                if (cmbGroup.Items.Count > 0)
                    cmbGroup.SelectedIndex = 0;

                cmbGroup_SelectedIndexChanged(null, null);

                //build card combox
                CardInfo card = new CardInfo(16, "�����ٱ���", 100000);
                cmbCards.Items.Add(card);
                card = new CardInfo(15, "��λ�����", 30000);
                cmbCards.Items.Add(card);
                card = new CardInfo(14, "��¸��", 90000);
                cmbCards.Items.Add(card);
                card = new CardInfo(2, "�ɳ����տ�", 10000);
                cmbCards.Items.Add(card);
                card = new CardInfo(13, "�乫��", 200000);
                cmbCards.Items.Add(card);
                cmbCards.SelectedIndex = 3;
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmBuyCards", ex);
            }
        }
        #endregion

        #region cmbGroup_SelectedIndexChanged
        private void cmbGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbGroup.Items.Count > 0 && cmbGroup.Text != string.Empty)
            {
                _accounts = ConfigCtrl.GetAccounts(cmbGroup.Text);
                listBoxSelectorAccounts.Clear();
                listBoxSelectorAccounts.AllItems = _accounts;
            }
        }
        #endregion

        #region btnRun_Click
        private void btnRun_Click(object sender, EventArgs e)
        {
            if (listBoxSelectorAccounts.SelectedItems.Count <= 0)
            {
                MessageBox.Show("��ѡ��Ҫִ�е��˺ţ�", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                listBoxSelectorAccounts.Select();
                return;
            }

            if (String.IsNullOrEmpty(txtCount.Text) || !DataValidation.IsNaturalNumber(txtCount.Text))
            {
                MessageBox.Show("��������Ϊ���ұ�����������", MainConstants.MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCount.Select();
                return;
            }

            SetControlStatus(false);

            CardInfo card = cmbCards.SelectedItem as CardInfo;
            if (card != null)
            {
                _toolbuycards._accounts = listBoxSelectorAccounts.SelectedItems;
                _toolbuycards._card = card;
                _toolbuycards._count = DataConvert.GetInt32(txtCount.Text);
                _toolbuycards.BuyCardsByThread();
            }            
        }        

        void _toolbuycards_MessageChanged(string caption, string key, string message)
        {
            SetMessageByParam(caption, key, message);
        }

        void _toolbuycards_BuyCardsFinished()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new GamePark.BuyCardsFinishedEventHandler(_toolbuycards_BuyCardsFinished), new object[] { });
            }
            else
            {
                SetControlStatus(true);
            }
        }
        #endregion

        #region SetControlStatus
        private void SetControlStatus(bool enabled)
        {
            cmbGroup.Enabled = enabled;
            btnRun.Enabled = enabled;
        }
        #endregion

        #region btnStop_Click
        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                if (_toolbuycards != null)
                    _toolbuycards.StopThread();
                SetControlStatus(true);
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmBuyCards", ex);
            }
        }
        #endregion

        #region FrmBuyCards_FormClosing
        private void FrmBuyCards_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (_toolbuycards != null)
                    _toolbuycards.StopThread();               
            }
            catch (Exception ex)
            {
                Program.ShowMessageBox("FrmBuyCards", ex);
            }
        }
        #endregion
    }
}