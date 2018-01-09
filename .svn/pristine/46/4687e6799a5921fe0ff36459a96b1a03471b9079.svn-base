using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace MySelfControl.MyControls
{
    public class MultiTextBox: TextBox
    {
        //设置Rect消息
        private const int EM_SETRECT = 179;
        //获取Rect消息
        private const int EM_GETRECT = 178;
        //private const int WM_GETTEXT = 0x000d;
        //private const int WM_COPY = 0x0301;
        //粘贴消息
        private const int WM_PASTE = 0x0302;
        //绘制消息
        private const int WM_PAINT = 0xF;
        //控件颜色编辑消息
        private const int WM_CTLCOLOREDIT = 0x0133;
        //private const int WM_CONTEXTMENU = 0x007B;
        //private const int WM_RBUTTONDOWN = 0x0204;

        public MultiTextBox()
        {
            //InitializeComponent();
            //this.SetStyle(ControlStyles.UserPaint
            //    | ControlStyles.DoubleBuffer
            //    | ControlStyles.ResizeRedraw  //调整大小时重绘
            //    | ControlStyles.AllPaintingInWmPaint // 禁止擦除背景.
            //    | ControlStyles.OptimizedDoubleBuffer // 双缓冲
            //    | ControlStyles.SupportsTransparentBackColor //透明效果
            //    , true);
            //多行显示 只有多行显示才能设置Rect有效
            this.Multiline = true;
            //允许回国
            AllowReturn = false;
            //关闭默认的边框
            BorderStyle = System.Windows.Forms.BorderStyle.None;
            //禁止折行
            WordWrap = false;

            _textMargin = new Padding(1);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            //NotifyTextChange();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            //NotifyTextChange();
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);

            NotifyTextChange();
        }

        protected override void OnTextAlignChanged(EventArgs e)
        {
            base.OnTextAlignChanged(e);
            NotifyTextChange();
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            //如果不允许回车 屏蔽回车 换行键值
            if (!AllowReturn
                && ((int)e.KeyChar == (int)Keys.Return || (int)e.KeyChar == (int)Keys.LineFeed))
            {
                e.Handled = true;
            }
            base.OnKeyPress(e);
        }

        /// <summary>
        /// 绘制消息进行绘制边框
        /// </summary>
        /// <param name="m"></param>
        private void WmPaint(ref System.Windows.Forms.Message m)
        {

            if (ClientRectangle.Width > 0 && ClientRectangle.Height > 0)
            {
                Rectangle clientRect = this.ClientRectangle;

                Color lineColor = borderRenderStyle.LineColor;
                if (Focused)
                    lineColor = borderRenderStyle.ActiveLineColor;
                using (Graphics g = this.CreateGraphics())
                {
                    if (clientRect.Width > 0 && clientRect.Height > 0)
                    {
                        using (Pen pen = new Pen(lineColor, borderRenderStyle.LineWidth))
                        {
                            pen.DashStyle = borderRenderStyle.LineDashStyle;
                            if (borderRenderStyle.ShowLeftLine)
                            {
                                g.DrawLine(pen, clientRect.Location, new Point(clientRect.Left, clientRect.Bottom));
                            }

                            if (borderRenderStyle.ShowTopLine)
                            {
                                g.DrawLine(pen, clientRect.Location, new Point(clientRect.Right, clientRect.Top));
                            }

                            if (borderRenderStyle.ShowRightLine)
                            {
                                g.DrawLine(pen, new Point(clientRect.Right - 1, clientRect.Top), new Point(clientRect.Right - 1, clientRect.Bottom));
                            }

                            if (borderRenderStyle.ShowBottomLine)
                            {
                                g.DrawLine(pen, new Point(clientRect.Left, clientRect.Bottom - 1), new Point(clientRect.Right, clientRect.Bottom - 1));

                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 窗体处理消息主函数 处理粘贴及绘制消息
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            string str = "";
            bool flag = false;
            int i = 0;
            if (m.Msg == 0x0204)
                i++;
            if (!AllowReturn
                && m.Msg == WM_PASTE
                && System.Windows.Forms.Clipboard.ContainsText())
            {
                str = System.Windows.Forms.Clipboard.GetText();
                System.Windows.Forms.Clipboard.Clear();
                string nstr = str.Replace(char.ConvertFromUtf32((int)Keys.Return), "").Replace(char.ConvertFromUtf32((int)Keys.LineFeed), "");
                System.Windows.Forms.Clipboard.SetText(nstr);
                if (str.Length > 0) flag = true;
            }


            base.WndProc(ref m);
            if (flag)
            {
                flag = false;
                System.Windows.Forms.Clipboard.SetText(str);
                str = "";
            }

            if (m.Msg == WM_PAINT || m.Msg == WM_CTLCOLOREDIT)
            {
                WmPaint(ref m);
            }
        }


        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, string lParam);

        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, ref Rectangle lParam);
        

        /// <summary>
        /// 设置文本显示布局位置
        /// </summary>
        public void NotifyTextChange()
        {
            if (Text == "")
                return;
            Rectangle rect = new Rectangle();
            SendMessage(this.Handle, EM_GETRECT, (IntPtr)0, ref rect);
            SizeF size = CreateGraphics().MeasureString(Text, Font);
            rect.Y = (int)(Height - size.Height) / 2 + TextMargin.Top;
            rect.X = 1 + TextMargin.Left;
            rect.Height = Height - 2;
            rect.Width = Width - TextMargin.Right - TextMargin.Left - 2;
            SendMessage(this.Handle, EM_SETRECT, IntPtr.Zero, ref rect);
        }

        /// <summary>
        /// 边框样式
        /// </summary>
        /// <remarks>获取或设置边框样式.</remarks>
        [Category("Appearance"),
         Description("边框样式"),
         DefaultValue(null)]
        public virtual TTextBoxBorderRenderStyle BorderRenderStyle
        {
            get { return borderRenderStyle; }
            set { borderRenderStyle = value; }
        }
        private TTextBoxBorderRenderStyle borderRenderStyle = new TTextBoxBorderRenderStyle();

        /// <summary>
        /// 是否允许有回车
        /// </summary>
        public bool AllowReturn { get; set; }

        private Padding _textMargin;
        /// <summary>
        /// Text Padding值
        /// </summary>
        public Padding TextMargin { get { return _textMargin; } set { _textMargin = value; NotifyTextChange(); } }
    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class TTextBoxBorderRenderStyle
    {
        /// <summary>
        /// 边线颜色
        /// </summary>
        /// <remarks>获取或设置边线颜色</remarks>
        [Category("Appearance"),
         Description("获取或设置边线颜色"),
         DefaultValue(typeof(Color), "Gray")]
        public virtual Color LineColor
        {
            get { return gridLineColor; }
            set { gridLineColor = value; }
        }
        private Color gridLineColor = Color.LightGray;

        /// <summary>
        /// 激活状态时的边线颜色
        /// </summary>
        /// <remarks>获取或设置激活状态时的边线颜色.</remarks>
        [Category("Appearance"),
         Description("激活状态时的边线颜色"),
         DefaultValue(typeof(Color), "RoyalBlue")]
        public virtual Color ActiveLineColor
        {
            get { return activeGridLineColor; }
            set { activeGridLineColor = value; }
        }
        private Color activeGridLineColor = Color.RoyalBlue;

        [Category("Appearance"),
         Description("线宽度"),
         DefaultValue(1)]
        public virtual float LineWidth
        {
            get { return lineWidth; }
            set { lineWidth = value; }
        }
        private float lineWidth = 1;

        /// <summary>
        ///线样式
        /// </summary>
        /// <remarks>获取或设置线样式.</remarks>
        [Category("Appearance"),
         Description("获取或设置线样式"),
         DefaultValue(typeof(DashStyle), "Solid")]
        public virtual DashStyle LineDashStyle
        {
            get { return lineDashStyle; }
            set { lineDashStyle = value; }
        }
        private DashStyle lineDashStyle = DashStyle.Solid;

        /// <summary>
        /// 左边线是否显示
        /// </summary>
        /// <remarks>获取或设置左线是否显示.</remarks>
        [Category("Appearance"),
         Description("左边网格线是否显示"),
        DefaultValue(true)
        ]
        public virtual bool ShowLeftLine
        {
            get { return showLeftLine; }
            set { showLeftLine = value; }
        }
        private bool showLeftLine = true;

        /// <summary>
        /// 上边线是否显示
        /// </summary>
        /// <remarks>获取或设置上边线是否显示.</remarks>
        [Category("Appearance"),
         Description("上边线是否显示"),
        DefaultValue(true)
        ]
        public virtual bool ShowTopLine
        {
            get { return showTopLine; }
            set { showTopLine = value; }
        }
        private bool showTopLine = true;

        /// <summary>
        /// 右边线是否显示
        /// </summary>
        /// <remarks>获取或设置右边线是否显示.</remarks>
        [Category("Appearance"),
         Description("右边线是否显示"),
        DefaultValue(true)
        ]
        public virtual bool ShowRightLine
        {
            get { return showRightLine; }
            set { showRightLine = value; }
        }
        private bool showRightLine = true;

        /// <summary>
        /// 底边线是否显
        /// </summary>
        /// <remarks>获取或设置底边线是否显示.</remarks>
        [Category("Appearance"),
         Description("底边线是否显示"),
        DefaultValue(true)
        ]
        public virtual bool ShowBottomLine
        {
            get { return showBottomLine; }
            set { showBottomLine = value; }
        }
        private bool showBottomLine = true;

    }
}
