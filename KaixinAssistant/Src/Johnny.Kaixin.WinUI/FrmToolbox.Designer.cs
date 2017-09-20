namespace Johnny.Kaixin.WinUI
{
    partial class FrmToolbox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmToolbox));
            Johnny.Controls.Windows.Toolbox.ToolboxCategory toolboxCategory1 = new Johnny.Controls.Windows.Toolbox.ToolboxCategory();
            Johnny.Controls.Windows.Toolbox.ToolboxItem toolboxItem1 = new Johnny.Controls.Windows.Toolbox.ToolboxItem();
            Johnny.Controls.Windows.Toolbox.ToolboxItem toolboxItem2 = new Johnny.Controls.Windows.Toolbox.ToolboxItem();
            Johnny.Controls.Windows.Toolbox.ToolboxItem toolboxItem3 = new Johnny.Controls.Windows.Toolbox.ToolboxItem();
            Johnny.Controls.Windows.Toolbox.ToolboxItem toolboxItem4 = new Johnny.Controls.Windows.Toolbox.ToolboxItem();
            Johnny.Controls.Windows.Toolbox.ToolboxItem toolboxItem5 = new Johnny.Controls.Windows.Toolbox.ToolboxItem();
            Johnny.Controls.Windows.Toolbox.ToolboxItem toolboxItem6 = new Johnny.Controls.Windows.Toolbox.ToolboxItem();
            Johnny.Controls.Windows.Toolbox.ToolboxItem toolboxItem7 = new Johnny.Controls.Windows.Toolbox.ToolboxItem();
            Johnny.Controls.Windows.Toolbox.ToolboxCategory toolboxCategory2 = new Johnny.Controls.Windows.Toolbox.ToolboxCategory();
            Johnny.Controls.Windows.Toolbox.ToolboxItem toolboxItem8 = new Johnny.Controls.Windows.Toolbox.ToolboxItem();
            Johnny.Controls.Windows.Toolbox.ToolboxItem toolboxItem9 = new Johnny.Controls.Windows.Toolbox.ToolboxItem();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.myToolbox = new Johnny.Controls.Windows.Toolbox.Toolbox();
            this.SuspendLayout();
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "Mouse.bmp");
            this.imageList.Images.SetKeyName(1, "Minus.gif");
            this.imageList.Images.SetKeyName(2, "Plus.gif");
            this.imageList.Images.SetKeyName(3, "team.ico");
            this.imageList.Images.SetKeyName(4, "delete_16x.ico");
            this.imageList.Images.SetKeyName(5, "Messages.ico");
            this.imageList.Images.SetKeyName(6, "fonfile.ico");
            this.imageList.Images.SetKeyName(7, "Rally.ico");
            this.imageList.Images.SetKeyName(8, "daoju.ico");
            this.imageList.Images.SetKeyName(9, "ico_park.ico");
            this.imageList.Images.SetKeyName(10, "contact.ico");
            this.imageList.Images.SetKeyName(11, "Tables.ico");
            // 
            // myToolbox
            // 
            toolboxCategory1.ImageIndex = 0;
            toolboxCategory1.IsOpen = true;
            toolboxItem1.ImageIndex = 3;
            toolboxItem1.Name = "互加好友";
            toolboxItem1.Parent = null;
            toolboxItem2.ImageIndex = 5;
            toolboxItem2.Name = "群发消息";
            toolboxItem2.Parent = null;
            toolboxItem3.ImageIndex = 7;
            toolboxItem3.Name = "组建车队";
            toolboxItem3.Parent = null;
            toolboxItem4.ImageIndex = 8;
            toolboxItem4.Name = "购买道具";
            toolboxItem4.Parent = null;
            toolboxItem5.ImageIndex = 9;
            toolboxItem5.Name = "争车位工具";
            toolboxItem5.Parent = null;
            toolboxItem6.ImageIndex = 10;
            toolboxItem6.Name = "维护联系人";
            toolboxItem6.Parent = null;
            toolboxItem7.ImageIndex = 11;
            toolboxItem7.Name = "更新数据";
            toolboxItem7.Parent = null;
            toolboxCategory1.Items.Add(toolboxItem1);
            toolboxCategory1.Items.Add(toolboxItem2);
            toolboxCategory1.Items.Add(toolboxItem3);
            toolboxCategory1.Items.Add(toolboxItem4);
            toolboxCategory1.Items.Add(toolboxItem5);
            toolboxCategory1.Items.Add(toolboxItem6);
            toolboxCategory1.Items.Add(toolboxItem7);
            toolboxCategory1.Name = "常用工具";
            toolboxCategory1.Parent = null;
            toolboxCategory2.ImageIndex = 0;
            toolboxCategory2.IsOpen = true;
            toolboxItem8.ImageIndex = 6;
            toolboxItem8.Name = "汉字<->拼音";
            toolboxItem8.Parent = null;
            toolboxItem9.ImageIndex = 7;
            toolboxItem9.Name = "TestFormManager";
            toolboxItem9.Parent = null;
            toolboxCategory2.Items.Add(toolboxItem8);
            toolboxCategory2.Items.Add(toolboxItem9);
            toolboxCategory2.Name = "其他";
            toolboxCategory2.Parent = null;
            this.myToolbox.Categories.Add(toolboxCategory1);
            this.myToolbox.Categories.Add(toolboxCategory2);
            this.myToolbox.CategoryBackColor = System.Drawing.Color.WhiteSmoke;
            this.myToolbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myToolbox.ImageList = this.imageList;
            this.myToolbox.Location = new System.Drawing.Point(0, 2);
            this.myToolbox.Name = "myToolbox";
            this.myToolbox.Size = new System.Drawing.Size(221, 391);
            this.myToolbox.TabIndex = 0;
            this.myToolbox.Text = "myToolbox";
            this.myToolbox.Click += new System.EventHandler(this.myToolbox_Click);
            // 
            // FrmToolbox
            // 
            this.ClientSize = new System.Drawing.Size(221, 395);
            this.Controls.Add(this.myToolbox);
            this.HideOnClose = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmToolbox";
            this.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.DockLeftAutoHide;
            this.TabText = "工具箱";
            this.Text = "工具箱";
            this.Load += new System.EventHandler(this.FrmToolbox_Load);
            this.ResumeLayout(false);

		}
		#endregion

        private System.Windows.Forms.ImageList imageList;
        private Johnny.Controls.Windows.Toolbox.Toolbox myToolbox;   
    }
}