using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Johnny.Kaixin.WinUI
{
    public partial class FrmOutput : ToolWindow
    {        

        public FrmOutput()
        {
            InitializeComponent();
        }

        private void FrmOutput_Load(object sender, EventArgs e)
        {

        }

        public void SetSelection(string caption, string key)
        {
            outputTab.SetSelection(caption, key);
        }

        public void SetMessage(string caption, string key, string message, DockPanel dockPanel)
        {
            if (message == "Johnny.Command.ClearLog")
            {
                outputTab.ClearMessage(caption, key);
                return;
            }

            outputTab.SetMessage(caption, key, message);
            //如果输出窗口没有显示
            if (outputTab.IsNewTabPage)
            {
                this.DockState = DockState.DockBottom;
                this.DockAreas = DockAreas.DockBottom;
                this.Show(dockPanel);
            }
        }

        protected override void CopyToClipboard()
        {
            outputTab.CopyToClipboard();
        }

        protected override void ClearTaskLog()
        {
            outputTab.ClearMessage();
        }

        protected override void CloseTabPage()
        {
            outputTab.CloseTagPage();
        }
    }
}