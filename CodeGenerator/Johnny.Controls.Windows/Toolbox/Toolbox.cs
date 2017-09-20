using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Johnny.Controls.Windows.Toolbox
{
    public delegate void ItemChangedEventHandler(Object sender, EventArgs e);
    public class Toolbox : Control
    {
        #region fields
        private VScrollBar vScrollBar1;
        private ToolboxCategoryCollection _categories = new ToolboxCategoryCollection();
        private Int32 _itemHeight = 18;
        private Int32 _categoryHeight = 16;
        private Int32 _itemSpace = 2;
        private ToolboxItem _mouseHoverItem = null;
        private ToolboxItem _selectedItem = null;
        private Color _borderColor = Color.Black;
        private Pen _borderPen = null;
        private Color _categoryBackColor = Color.WhiteSmoke;
        //private ImageList _innerImageList; = new ImageList();
        private ImageList _imageList = new ImageList();
        private ImageList imageList;
        private IContainer components;
        private string SYSTEMERROR = "ϵͳ����";
        #endregion

        #region Properties
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ToolboxCategoryCollection Categories
        {
            get
            {
                return _categories;
            }
            set
            {
                _categories = value;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ToolboxItem SelectedItem
        {
            get
            {
                return _selectedItem;
            }
        }

        public ImageList ImageList
        {
            get
            {
                return _imageList;
            }
            set
            {
                _imageList = value;
            }
        }

        [Browsable(true)]
        public Color BorderColor
        {
            get
            {
                return _borderColor;
            }
            set
            {
                if (_borderColor == value) return;
                _borderColor = value;
                _borderPen = new Pen(_borderColor);
                Invalidate();
            }
        }

        [Browsable(true)]
        public Color CategoryBackColor
        {
            get
            {
                return _categoryBackColor;
            }
            set
            {
                _categoryBackColor = value;
                Invalidate();
            }
        }        
        #endregion

        #region Events
        public event ItemChangedEventHandler ItemChanged;
        #endregion

        #region WINAPI functions/structures
        [StructLayout(LayoutKind.Sequential)]
        public struct WinAPI_RECT
        {
            public Int32 Left;
            public Int32 Top;
            public Int32 Right;
            public Int32 Bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct WinAPI_NCCALCSIZE_PARAMS
        {
            public WinAPI_RECT rgrc0, rgrc1, rgrc2;
            public IntPtr lppos;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct WINDOWINFO
        {
            public uint cbSize;
            public WinAPI_RECT rcWindow;
            public WinAPI_RECT rcClient;
            public uint dwStyle;
            public uint dwExStyle;
            public uint dwWindowStatus;
            public uint cxWindowBorders;
            public uint cyWindowBorders;
            public Int32 atomWindowType;
            public Int32 wCreatorVersion;

        }

        public struct WinAPI_HT
        {
            public const uint HTERROR = unchecked((uint)-2);
            public const uint HTTRANSPARENT = unchecked((uint)-1);
            public const int HTNOWHERE = 0;
            public const int HTCLIENT = 1;
            public const int HTCAPTION = 2;
            public const int HTSYSMENU = 3;
            public const int HTGROWBOX = 4;
            public const int HTSIZE = HTGROWBOX;
            public const int HTMENU = 5;
            public const int HTHSCROLL = 6;
            public const int HTVSCROLL = 7;
            public const int HTMINBUTTON = 8;
            public const int HTMAXBUTTON = 9;
            public const int HTLEFT = 10;
            public const int HTRIGHT = 11;
            public const int HTTOP = 12;
            public const int HTTOPLEFT = 13;
            public const int HTTOPRIGHT = 14;
            public const int HTBOTTOM = 15;
            public const int HTBOTTOMLEFT = 16;
            public const int HTBOTTOMRIGHT = 17;
            public const int HTBORDER = 18;
            public const int HTREDUCE = HTMINBUTTON;
            public const int HTZOOM = HTMAXBUTTON;
            public const int HTSIZEFIRST = HTLEFT;
            public const int HTSIZELAST = HTBOTTOMRIGHT;
            public const int HTOBJECT = 19;
        }

        public struct WinAPI_SWP
        {
            public const int SWP_NOSIZE = 0x0001;
            public const int SWP_NOMOVE = 0x0002;
            public const int SWP_NOZORDER = 0x0004;
            public const int SWP_NOREDRAW = 0x0008;
            public const int SWP_NOACTIVATE = 0x0010;
            public const int SWP_FRAMECHANGED = 0x0020;  // The frame changed: send WM_NCCALCSIZE 
            public const int SWP_DRAWFRAME = SWP_FRAMECHANGED;
            public const int SWP_SHOWWINDOW = 0x0040;
            public const int SWP_HIDEWINDOW = 0x0080;
            public const int SWP_NOCOPYBITS = 0x0100;
            public const int SWP_NOOWNERZORDER = 0x0200;  // Don't do owner Z ordering 
            public const int SWP_NOREPOSITION = SWP_NOOWNERZORDER;
            public const int SWP_NOSENDCHANGING = 0x0400;  // Don't send WM_WINDOWPOSCHANGING 
        }

        public enum WinAPI_WM
        {
            WM_NCCALCSIZE = 0x0083,
            WM_NCHITTEST = 0x0084,
            WM_NCLBUTTONDOWN = 0x00A1,
            WM_NCLBUTTONUP = 0x00A2,
            WM_NCMOUSEMOVE = 0x00A0,
            WM_NCPAINT = 0x0085,


            WM_LBUTTONDOWN = 0x0201,
            WM_MOUSEMOVE = 0x0200
        }

        [DllImport("User32.dll")]
        public extern static IntPtr GetWindowDC(IntPtr hWnd);


        [DllImport("User32.dll")]
        public extern static int ReleaseDC(IntPtr hWnd, IntPtr hDC);


        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        [DllImport("user32.dll")]
        public static extern Boolean GetWindowInfo(IntPtr hwnd, ref WINDOWINFO pwi);
        #endregion

        #region �ǿͻ����ļ���ͻ���
        /// <summary>
        /// ��ӷǿͻ����ļ���ͻ���
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case (int)WinAPI_WM.WM_NCCALCSIZE:
                    if (m.WParam.ToInt32() == 0)
                    {
                        WinAPI_RECT rc = (WinAPI_RECT)m.GetLParam(typeof(WinAPI_RECT));
                        rc.Left += 1;
                        rc.Top += 1;
                        rc.Right -= 1;
                        rc.Bottom -= 1;
                        Marshal.StructureToPtr(rc, m.LParam, true);
                        m.Result = IntPtr.Zero;
                    }
                    else
                    {
                        WinAPI_NCCALCSIZE_PARAMS csp;
                        csp = (WinAPI_NCCALCSIZE_PARAMS)m.GetLParam(typeof(WinAPI_NCCALCSIZE_PARAMS));
                        csp.rgrc0.Top += 1;
                        csp.rgrc0.Bottom -= 1;
                        csp.rgrc0.Left += 1;
                        csp.rgrc0.Right -= 1;

                        Marshal.StructureToPtr(csp, m.LParam, true);
                        //Return zero to preserve client rectangle
                        m.Result = IntPtr.Zero;
                    }
                    break;
                case (int)WinAPI_WM.WM_NCPAINT:
                    {
                        m.WParam = NCPaint(m.WParam);
                        break;
                    }
            }

            base.WndProc(ref m);
        }

        public IntPtr NCPaint(IntPtr region)
        {
            IntPtr hDC = GetWindowDC(this.Handle);
            if (hDC != IntPtr.Zero)
            {
                Graphics grTemp = Graphics.FromHdc(hDC);

                int ScrollBarWidth = SystemInformation.VerticalScrollBarWidth;
                int ScrollBarHeight = SystemInformation.HorizontalScrollBarHeight;

                WINDOWINFO wi = new WINDOWINFO();
                wi.cbSize = (uint)Marshal.SizeOf(wi);

                //�õ���ǰ�ؼ��Ĵ�����Ϣ
                GetWindowInfo(Handle, ref wi);

                wi.rcClient.Right--;
                wi.rcClient.Bottom--;


                //��õ�ǰ�ؼ�������
                Region UpdateRegion = new Region(new Rectangle(wi.rcWindow.Top, wi.rcWindow.Left, wi.rcWindow.Right - wi.rcWindow.Left, wi.rcWindow.Bottom - wi.rcWindow.Top));

                //��ÿͻ������������
                UpdateRegion.Exclude(new Rectangle(wi.rcClient.Top, wi.rcClient.Left, wi.rcClient.Right - wi.rcClient.Left, wi.rcClient.Bottom - wi.rcClient.Top));

                //if (IsHScrollVisible && IsVScrollVisible)
                //{
                //    UpdateRegion.Exclude(Rectangle.FromLTRB
                //            (wi.rcClient.Right + 2, wi.rcClient.Bottom + 2,
                //            wi.rcWindow.Right, wi.rcWindow.Bottom));
                //}

                //�õ���ǰ����ľ��
                IntPtr hRgn = UpdateRegion.GetHrgn(grTemp);

                //For Painting we need to zero offset the Rectangles.
                Rectangle WindowRect = new Rectangle(wi.rcWindow.Top, wi.rcWindow.Left, wi.rcWindow.Right - wi.rcWindow.Left, wi.rcWindow.Bottom - wi.rcWindow.Top);

                Point offset = Point.Empty - (Size)WindowRect.Location;

                WindowRect.Offset(offset);

                Rectangle ClientRect = WindowRect;

                ClientRect.Inflate(-1, -1);

                //Fill the BorderArea
                Region PaintRegion = new Region(WindowRect);
                PaintRegion.Exclude(ClientRect);
                grTemp.FillRegion(SystemBrushes.Control, PaintRegion);

                //Adjust ClientRect for Drawing Border.
                ClientRect.Inflate(1, 1);
                ClientRect.Width--;
                ClientRect.Height--;

                //Draw Outer Raised Border
                //ControlPaint.DrawBorder3D(grTemp, WindowRect, Border3DStyle.Raised,
                //Border3DSide.Bottom | Border3DSide.Left | Border3DSide.Right | Border3DSide.Top);
                WindowRect.Width--;
                WindowRect.Height--;
                grTemp.DrawRectangle(_borderPen, WindowRect);

                //Draw Inner Sunken Border
                //ControlPaint.DrawBorder3D(grTemp, ClientRect, Border3DStyle.Sunken,
                //Border3DSide.Bottom | Border3DSide.Left | Border3DSide.Right | Border3DSide.Top);

                ReleaseDC(Handle, hDC);

                grTemp.Dispose();

                return hRgn;

            }
            return region;

        }

        #endregion

        #region Override
        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                base.OnPaint(e);
                Graphics g = e.Graphics;

                Rectangle visibleRectangle = GetVisibleRect();

                //draw border
                //g.DrawRectangle(Pens.Olive,new Rectangle(0,0,this.Width-1,this.Height-1));
                Brush categoryBrush = new SolidBrush(_categoryBackColor);
                Brush categoryTextBrush = new SolidBrush(Color.Black);
                Brush itemHoverBrush = new SolidBrush(SystemColors.Info);
                Brush selectedBrush = new SolidBrush(Color.Orange);
                Font categoryFont = new Font(Font, Font.Style | FontStyle.Bold);
                //Image img = new Image();
                Int32 top = _itemSpace;

                if (vScrollBar1.Visible)
                {
                    top -= vScrollBar1.Value;
                }

                Int32 left = 3;
                foreach (ToolboxCategory tbc in _categories)
                {
                    if (tbc.IsOpen)
                    {
                        g.FillRectangle(categoryBrush, left, top, visibleRectangle.Width - 2 - 4, _categoryHeight);
                        Image img = imageList.Images[1];
                        if (img != null)
                        {
                            g.DrawImage(img, left, top, img.Size.Width, img.Size.Height);
                            g.DrawString(tbc.Name, categoryFont, categoryTextBrush, new Rectangle(4 + img.Size.Width, top + 1, visibleRectangle.Width - 2 - 4, _categoryHeight));
                        }
                        else
                        {
                            g.DrawString(tbc.Name, categoryFont, categoryTextBrush, new Rectangle(4, top + 1, visibleRectangle.Width - 2 - 4, _categoryHeight));
                        }

                        top += _categoryHeight + _itemSpace;
                        foreach (ToolboxItem tbi in tbc.Items)
                        {
                            if (tbi == _selectedItem)
                            {
                                g.DrawRectangle(Pens.Black, new Rectangle(left, top, visibleRectangle.Width - 2 * left, _itemHeight));
                                g.FillRectangle(selectedBrush, new Rectangle(left + 1, top + 1, visibleRectangle.Width - 2 * left - 2, _itemHeight - 1));
                            }
                            else if (tbi == _mouseHoverItem)
                            {
                                g.DrawRectangle(Pens.Black, new Rectangle(left, top, visibleRectangle.Width - 2 * left, _itemHeight));
                                g.FillRectangle(itemHoverBrush, new Rectangle(left + 1, top + 1, visibleRectangle.Width - 2 * left - 2, _itemHeight - 1));
                            }
                            if (tbi.ImageIndex > 0 && tbi.ImageIndex < _imageList.Images.Count)
                            {
                                Image subimg = _imageList.Images[tbi.ImageIndex];
                                if (subimg != null)
                                {
                                    g.DrawImage(subimg, 4, top + 1, subimg.Size.Width, subimg.Size.Height);
                                }
                            }
                            g.DrawString(tbi.Name, Font, categoryTextBrush, new Rectangle(23, top + 1, visibleRectangle.Width - 2 - 4, _itemHeight));
                            top += _itemHeight + _itemSpace;
                        }
                    }
                    else
                    {
                        g.FillRectangle(categoryBrush, left, top, visibleRectangle.Width - 2 - 4, _categoryHeight);
                        Image img = imageList.Images[0];
                        if (img != null)
                        {
                            g.DrawImage(img, left, top, img.Size.Width, img.Size.Height);
                            g.DrawString(tbc.Name, categoryFont, categoryTextBrush, new Rectangle(4 + img.Size.Width, top + 1, visibleRectangle.Width - 2 - 4, _categoryHeight));
                        }
                        else
                        {
                            g.DrawString(tbc.Name, categoryFont, categoryTextBrush, new Rectangle(4, top + 1, visibleRectangle.Width - 2 - 4, _categoryHeight));
                        }
                        top += _categoryHeight + _itemSpace;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, SYSTEMERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            try
            {
                base.OnMouseDown(e);
                ToolboxItem item = GetToolboxItemByPoint(e.Location);
                if (item != null)
                {
                    if (item is ToolboxCategory)
                    {
                        ToolboxCategory tbc = item as ToolboxCategory;
                        tbc.IsOpen = !tbc.IsOpen;
                        RefreshScrollBar();
                        Invalidate();
                    }
                    else
                    {
                        _selectedItem = item;
                        Invalidate();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, SYSTEMERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            try
            {
                base.OnMouseMove(e);
                if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right) return;

                ToolboxItem item = GetToolboxItemByPoint(e.Location);
                if (item != null)
                {
                    if (!(item is ToolboxCategory))
                    {
                        _mouseHoverItem = item;
                        Invalidate();
                    }
                    else
                    {
                        _mouseHoverItem = null;
                        Invalidate();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, SYSTEMERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            try
            {
                base.OnMouseWheel(e);
                if (vScrollBar1.Visible)
                {
                    int newVal = vScrollBar1.Value - (e.Delta / 120) * vScrollBar1.LargeChange;
                    if (newVal < 0)
                        vScrollBar1.Value = 0;
                    else if (newVal > (vScrollBar1.Maximum - vScrollBar1.LargeChange))
                        vScrollBar1.Value = vScrollBar1.Maximum;
                    else
                        vScrollBar1.Value = newVal;
                    Invalidate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, SYSTEMERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            try
            {
                base.OnResize(e);
                RefreshScrollBar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, SYSTEMERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            try
            {
                base.OnMouseLeave(e);
                _mouseHoverItem = null;
                Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, SYSTEMERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Mothed

        private void RefreshScrollBar()
        {
            Int32 totalHeight = GetTotalHeight();
            Rectangle paintRect = GetVisibleRect();
            if (totalHeight > paintRect.Height)
            {
                vScrollBar1.Visible = true;
                vScrollBar1.Maximum = totalHeight - paintRect.Height;

                vScrollBar1.LargeChange = vScrollBar1.Maximum / 5;
                vScrollBar1.SmallChange = vScrollBar1.Maximum / 10;
                vScrollBar1.Maximum += vScrollBar1.LargeChange;
            }
            else
            {
                vScrollBar1.Visible = false;
            }
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Toolbox));
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Dock = System.Windows.Forms.DockStyle.Right;
            this.vScrollBar1.LargeChange = 1;
            this.vScrollBar1.Location = new System.Drawing.Point(-17, 0);
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(17, 0);
            this.vScrollBar1.TabIndex = 0;
            this.vScrollBar1.Visible = false;
            this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar1_Scroll);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "Plus.gif");
            this.imageList.Images.SetKeyName(1, "Minus.gif");
            // 
            // Toolbox
            // 
            this.Controls.Add(this.vScrollBar1);
            this.ResumeLayout(false);

        }

        public Toolbox()
        {
            _borderPen = new Pen(_borderColor);

            InitializeComponent();

            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            RefreshScrollBar();
            this._categories.ItemChanged += new CollectionChangeEventHandler(OnCategoryCollectionChanged);
            this.ItemChanged += new ItemChangedEventHandler(OnItemChanged);
        }

        private ToolboxItem GetToolboxItemByPoint(Point pi)
        {
            ToolboxItem result = null;
            Int32 top = _itemSpace;
            if (vScrollBar1.Visible)
            {
                top -= vScrollBar1.Value;
            }

            foreach (ToolboxCategory tbc in _categories)
            {
                Rectangle rectCategory = new Rectangle(0, top, this.Width, _categoryHeight);
                if (rectCategory.Contains(pi))
                {
                    result = tbc;
                    break;
                }
                else
                {
                    if (tbc.IsOpen)
                    {
                        top += _categoryHeight + _itemSpace;
                        Boolean find = false;
                        foreach (ToolboxItem tbi in tbc.Items)
                        {
                            Rectangle rectItem = new Rectangle(0, top, this.Width, _itemHeight);
                            if (rectItem.Contains(pi))
                            {
                                tbi.Parent = tbc;
                                result = tbi;
                                find = true;
                                break;
                            }
                            else
                            {
                                top += _itemHeight + _itemSpace;
                            }
                        }

                        if (find)
                        {
                            break;
                        }
                    }
                    else
                    {
                        top += _categoryHeight + _itemSpace;
                    }
                }
            }
            return result;
        }

        private Int32 GetTotalHeight()
        {
            Int32 result = _itemSpace;
            foreach (ToolboxCategory tbc in _categories)
            {
                if (tbc.IsOpen)
                {
                    result += _categoryHeight + _itemSpace;
                    result += tbc.Items.Count * (_itemHeight + _itemSpace);
                }
                else
                {
                    result += _categoryHeight + _itemSpace;
                }
            }

            return result;
        }

        private Rectangle GetVisibleRect()
        {
            Rectangle rect = ClientRectangle;
            if (vScrollBar1.Visible)
            {
                rect.Width -= vScrollBar1.Width;
            }
            return rect;
        }

        private void OnCategoryCollectionChanged(Object sender, CollectionChangeEventArgs e)
        {
            ToolboxCategory tbc = e.Element as ToolboxCategory;
            if (e.Action == CollectionChangeAction.Add)
            {
                tbc.Items.ItemChanged += new CollectionChangeEventHandler(OnCategoryItemChanged);
            }
            else if (e.Action == CollectionChangeAction.Remove)
            {
                tbc.Items.ItemChanged -= new CollectionChangeEventHandler(OnCategoryItemChanged);
            }
            Invalidate();
        }

        private void OnCategoryItemChanged(Object sender, CollectionChangeEventArgs e)
        {
            Invalidate();
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            Invalidate();
        }

        protected virtual void OnItemChanged(Object sender, EventArgs e)
        {

        }

        public void ResetBorderColor()
        {
            _borderColor = Color.Black;
        }

        public bool ShouldSerializeBorderColor()
        {
            return (_borderColor == Color.Black) ? false : true;
        }
        #endregion

        #region Properties
        //public ToolboxItem SelectedItem
        //{
        //    get { return _selectedItem; }
        //}
        #endregion
    }
}
