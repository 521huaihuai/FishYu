using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ReportFormDesign.CurrentPosition;
using ReportFormDesign.Model;
using ReportFormDesign.DrawUtils;
using System.Drawing.Drawing2D;
using ReportFormDesign.Animals;
using System.Threading;

namespace ReportFormDesign.ReportViewPanel
{

    /// <summary>
    /// 自定义控件 报表
    /// Design By @author zjm
    /// </summary>
    public abstract class ViewPanel : Panel
    {

        #region 子类字段
        //动画
        protected Animalion animalion;
        //用于动画的绘制
        protected Graphics graphics;
        //数据的适配器
        protected ReportViewAdapter adapter;
        //所有数据之和(默认0)
        protected float allNum;
        //用于记录双击的次数
        protected int doubleClickTimes = 0;
        //自动刷新时间(默认5分钟刷新一次)
        protected int autoUpdateTime = 300;
        //如果为false, 允许展示动画效果
        protected bool isNotAllowShowAnimalion = false;
        //tooltip超出边界是否要弹回(默认是)
        protected bool isNeedReLocationToopTip = true;
        //如果是更加复杂,坐标信息不能自动排列生成的报表, 数据的响应的位置需要自己重新书写
        protected bool isSelfDefineReportView;
        //是否自动刷新数据(默认false)(调用InitAutoUpdateDataThread()后, 为true)
        //第一次子类设置的更新时间(或者5 * 60)秒后更新, 接下来就是自己设点的AutoUpdateTime时间更新
        //需要实现OnNotifyDataModelListChageEvent委托事件
        protected bool isAutoUpdateData = false;
        //当前要绘制的数据模型(鼠标移过该图像才拥有实例)
        protected DataModel CurrentDataModel;
        //鼠标移动选中时PopView
        protected ReportViewToolTip toolTip;
        //绘制报表的帮助类
        protected ReportViewUtils utils = new ReportViewUtils();
        protected bool IsCoordinateReportView = false;
        #endregion

        #region 私有字段
        //自适应排列的报表最大高度
        private int MaxHeight;
        private int sleepTime;
        private int perScrollerDistance = 20;
        //private bool isFirstDraw = true;
        //最大高度
        //private int MaxHeight = 32767;
        //用来标定当前鼠标滚动的位置
        private int currentScrollY = 0;
        private bool IsShowVerticalBar;
        private bool isUpWheelScrolling;
        private bool isDownWheelScrolling;
        private DateTime InvalidateTime = DateTime.Now;
        //是否正在更新， 在查看数据的时候不允许数据的刷新
        private bool IsUpdateing;

        private Thread animalThread;
        #endregion


        //上下边距
        public int padding = 20;

        public ViewPanel()
        {
            DoubleBuffered = true;
            //Dock = DockStyle.Fill;
            //初始化数据
            LineColor = ReportViewUtils.perferBlue;
            TextColor = ReportViewUtils.perferWhite;
            DataColor = ReportViewUtils.perferWhite;
            BoardColor = ReportViewUtils.perferWhite;
            FrameColor = ReportViewUtils.perferWhite;
            TextSize = 8;
            DataSize = 8;
            StrokenWidth = 5;
            IsHorizontally = true;
            IsAutoReSizeView = true;
            this.AutoScroll = true;
            IsDrawFrame = true;
            IsDrawBoard = false;
            IsDrawTitle = true;
        }

        #region 事件点击相关事件
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Focus();
            }
        }

        protected override void OnDoubleClick(EventArgs e)
        {
            base.OnDoubleClick(e);
            this.Focus();
            if (OnDoubleClickListenerEvent != null)
            {
                OnDoubleClickListenerEvent(CurrentDataModel);
            }
            if (OnDoubleClickSelfViewEvent != null)
            {
                if (doubleClickTimes == 0)
                {
                    OnDoubleClickSelfViewEvent();
                    doubleClickTimes++;
                }
            }
            if (OnDoubleClickReBackViewEvent != null)
            {
                if (doubleClickTimes == 0)
                {
                    IsShowVerticalBar = true;
                    Form form = new Form();
                    form.StartPosition = FormStartPosition.CenterScreen;
                    form.WindowState = FormWindowState.Maximized;
                    form.AutoScroll = true;
                    form.VerticalScroll.Enabled = true;
                    form.VerticalScroll.Visible = true;
                    Label lable = new Label();
                    lable.Location = new Point(0, MaxHeight + padding);
                    lable.Text = "";
                    lable.Width = 0;
                    form.Controls.Add(lable);
                    //form.Name = "大图展示";
                    if (!NewFormBackColor.IsEmpty)
                    {
                        form.BackColor = NewFormBackColor;
                    }
                    else
                    {
                        form.BackColor = Color.Gray;
                    }
                    if (NewFormBackgroundImage != null)
                    {
                        form.BackgroundImage = NewFormBackgroundImage;
                    }
                    form.Text = this.Name + "大图展示";
                    form.Controls.Add(this);
                    form.Show();
                    this.Focus();
                    form.FormClosing += form_FormClosing;
                    doubleClickTimes++;
                }
            }

        }

        void form_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form form = OnDoubleClickReBackViewEvent();
            form.ActiveControl.Controls.Add(this);
            if (animalion != null)
            {
                animalion.IsPrepareAnimaled = false;
            }
            doubleClickTimes--;
            adapter.setIndex(-1);
            currentScrollY = 0;
            IsShowVerticalBar = false;
        }
        #endregion

        #region 鼠标移动操作相关事件
        /// <summary>
        /// 鼠标移动事件, 委托给用户鼠标经过的图形化位置数据
        /// </summary>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            int x = e.X;
            int y = e.Y;

            if (adapter != null)
            {
                //设置起始位置
                adapter.setIndex(-1);
                //遍历所有数据源
                while (adapter.next())
                {
                    DataModel data = adapter.getItem();
                    if (data == null)
                    {
                        return;
                    }
                    //判定鼠标是否在这个区域上面
                    bool isIn;
                    if (IsCoordinateReportView)
                    {
                        isIn = data.Area.isInRect(x, y, true);
                    }
                    else
                    {
                        isIn = data.Area.isInRect(x, y, false);
                    }
                    if (isIn)
                    {
                        if (animalion != null && !isNotAllowShowAnimalion)
                        {
                            //触发动画效果
                            animalion.IsPrepareAnimaled = true;
                        }
                        data.Area.IsMouseIn = true;
                        //委托事件传递
                        if (OnMouseMove_ReportViewPanelEvent != null)
                        {
                            OnMouseMove_ReportViewPanelEvent(data);
                        }
                    }
                    else
                    {
                        data.Area.IsMouseIn = false;
                    }
                }
            }
            //PopView显示具体某条数据
            if (toolTip != null)
            {
                if (x > this.Width - toolTip.Width - padding)
                {
                    //如果没有超出边界
                    if (x < (this.Width - padding))
                    {
                        if (isNeedReLocationToopTip)
                        {
                            x = this.Width - toolTip.Width - 2 * padding;
                        }
                    }
                }
                if (y > this.Height - toolTip.Height)
                {
                    y = this.Height - toolTip.Height;
                }
                toolTip.LocalPosition = new Point(x, y);
            }

            if (animalion == null || !animalion.IsPrepareAnimaled)
            {
                InvalidateView();
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            //base.OnMouseLeave(e);
            if (toolTip != null)
            {
                toolTip.IsVisible = false;
            }
            if (animalion == null || !animalion.IsPrepareAnimaled)
            {
                InvalidateView();
            }
            isAutoUpdateData = true;
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            if (toolTip != null)
            {
                toolTip.IsVisible = true;
            }
            InvalidateView();
            //如果正在常看数据不进行数据的自动刷新
            isAutoUpdateData = false;
        }

        /// <summary>
        /// 鼠标滚动时的数据刷新
        /// </summary>
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            int dd = e.Delta;
            if (dd < 0)
            {
                if (IsShowVerticalBar)
                {
                    currentScrollY = perScrollerDistance;
                }
                else
                {
                    currentScrollY = perScrollerDistance;
                }

            }
            else
            {
                currentScrollY = -perScrollerDistance;
            }

            isUpWheelScrolling = true;
            isDownWheelScrolling = true;
            if (adapter == null)
            {
                return;
            }
            if (adapter.DataList[adapter.DataList.Count - 1].Area.bottom <= Height)
            {
                isDownWheelScrolling = false;
            }
            //if (adapter.DataList[0].Area.top > adapter.DataList[0].Area.PaddingIn + adapter.PosRectData.top / 2)
            if (adapter.PosRectData != null)
            {
                if (adapter.DataList[0].Area.top >= adapter.PosRectData.top)
                {
                    isUpWheelScrolling = false;
                }
            }
            InvalidateView();
        }

        protected override void OnScroll(ScrollEventArgs se)
        {
            //this.Focus();
            //用于标定数据的位置
            currentScrollY = this.VerticalScroll.Value;
        }
        #endregion

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (Height <= 20)
            {
                Height = 20;
            }
            InvalidateView();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            DateTime currentTime = DateTime.Now;

            //自己声明的Graphics
            Graphics g = e.Graphics;

            //绘制四边的边框(最基础的绘制)
            if(IsDrawBoard)
            {
                float boardWidth = 1.5f;
                Pen BoardPen = new Pen(BoardColor, boardWidth);
                g.DrawLine(BoardPen, 0 + boardWidth / 2, 0 + boardWidth / 2, Width - boardWidth, 0 + boardWidth / 2);
                g.DrawLine(BoardPen, 0 + boardWidth / 2, Height - boardWidth, Width - boardWidth, Height - boardWidth);
                g.DrawLine(BoardPen, 0 + boardWidth / 2, 0 + boardWidth / 2, 0 + boardWidth / 2, Height - boardWidth);
                g.DrawLine(BoardPen, Width - boardWidth, 0 + boardWidth / 2, Width - boardWidth, Height - boardWidth);
            }
            if (IsDrawFrame)
            {
                float penWidth = 2.0f;
                Pen FramePen = new Pen(FrameColor, penWidth);
                int lineWidth = 7;
                g.DrawLine(FramePen, 0 + penWidth / 2, 0 + penWidth / 2, lineWidth + penWidth / 2, 0 + penWidth / 2);
                g.DrawLine(FramePen, 0 + penWidth / 2, 0 + penWidth / 2, 0 + penWidth / 2, lineWidth + penWidth / 2);

                g.DrawLine(FramePen, Width - penWidth, 0 + penWidth / 2, Width - lineWidth - penWidth, 0 + penWidth / 2);
                g.DrawLine(FramePen, Width - penWidth, 0 + penWidth / 2, Width - penWidth, lineWidth + penWidth / 2);

                g.DrawLine(FramePen, Width - penWidth, Height - penWidth, Width - penWidth, Height - lineWidth - penWidth);
                g.DrawLine(FramePen, Width - penWidth, Height - penWidth, Width - lineWidth - penWidth, Height - penWidth);

                g.DrawLine(FramePen, 0 + penWidth / 2, Height - penWidth, lineWidth + penWidth / 2, Height - penWidth);
                g.DrawLine(FramePen, 0 + penWidth / 2, Height - penWidth, 0 + penWidth / 2, Height - lineWidth - penWidth);
            }


            //绘制的质量设置
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            g.SmoothingMode = SmoothingMode.HighQuality;
            //绘制的画笔和笔刷
            Pen linePen = new Pen(LineColor, StrokenWidth);
            Brush lineBrush = new SolidBrush(LineColor);
            Brush TextBrush = new SolidBrush(TextColor);
            Brush DataBrush = new SolidBrush(DataColor);
            Font font_Text = new Font("幼圆", TextSize);
            Font font_Data = new Font("Consolas", DataSize);
            //绘制特殊的固定图形（自定义绘制2）
            if (isSelfDefineReportView)
            {
                DrawSelfDefineView(g, linePen, lineBrush, TextBrush, DataBrush, font_Text, font_Data);
            }



            DataModel mouseMoveModel = null;
            if (adapter != null && !(animalion != null && animalion.IsPrepareAnimaled && !isNotAllowShowAnimalion))
            {
                adapter.setIndex(-1);
                DataModel data = null;
                //AreaPositionRect rectData = null;
                while (adapter.next())
                {
                    data = adapter.getItem();
                    if (!isSelfDefineReportView)
                    {
                        //适配器自动生成的位置
                        //rectData = adapter.getPositionRect();
                        //data.Area.left = rectData.left;
                        //data.Area.right = rectData.right;
                        //data.Area.top = rectData.top;
                        //data.Area.bottom = rectData.bottom;
                        if (isUpWheelScrolling && currentScrollY < 0)
                        {
                            data.Area.top = data.Area.top - currentScrollY;
                            data.Area.bottom = data.Area.bottom - currentScrollY;
                        }
                        if (isDownWheelScrolling && currentScrollY > 0)
                        {
                            data.Area.top = data.Area.top - currentScrollY;
                            data.Area.bottom = data.Area.bottom - currentScrollY;
                        }
                    }
                    //子类自己的绘制操作（最重要的绘制基于模型数据的绘制）
                    childPaint(g, data, linePen, lineBrush, TextBrush, DataBrush, font_Text, font_Data);
                    if (adapter.IsVerticalShowData)
                    {
                        data.Area.right = this.Width;
                    }
                    if (data.Area.IsMouseIn)
                    {
                        mouseMoveModel = data;
                        CurrentDataModel = data;
                    }
                }
                isUpWheelScrolling = false;
                isDownWheelScrolling = false;
            }

            //释放资源
            disposeResource(linePen, lineBrush, TextBrush, DataBrush, font_Text, font_Data);

            if (graphics != null)
            {
                graphics.Dispose();
            }
            graphics = g;
            //动画的绘制
            if (!isNotAllowShowAnimalion && animalion != null)
            {
                if (animalion.IsPrepareAnimaled)
                {
                    animalion.IsAnimailing = true;
                    //if (graphics != null)
                    //{
                        
                    //}
                    animalion.AnimalionRender();
                    sleepTime = (int)(animalion.DelayTime - (DateTime.Now - currentTime).TotalMilliseconds);
                    animalion.IsAnimailing = false;
                }
            }
            ///绘制ToolTip
            if (toolTip != null)
            {
                toolTip.OnRender(g, mouseMoveModel);
            }
        }

        #region 基础方法
        /// <summary>
        /// 设置适配器
        /// </summary>
        public void setAdapter(ReportViewAdapter adapter)
        {
            this.adapter = adapter;
            int count = adapter.getCount();
            adapter.next();
            if (adapter.IsVerticalShowData)
            {
                if (adapter.PosRectData != null)
                {
                    //设置高度
                    int height = adapter.PosRectData.bottom - adapter.PosRectData.top + adapter.PosRectData.PaddingIn;
                    MaxHeight = height * count;
                }
            }
            adapter.setIndex(-1);
        }

        private void disposeResource(Pen myPen, Brush myBrush, Brush TextBrush, Brush DataBrush, System.Drawing.Font font_Text, System.Drawing.Font font_Data)
        {
            //释放资源
            myBrush.Dispose();
            DataBrush.Dispose();
            TextBrush.Dispose();
            myPen.Dispose();
            font_Text.Dispose();
            font_Data.Dispose();
        }

        #endregion

        #region 开启动画任务
        public void InitAnimalThask()
        {
            if (animalThread == null || !animalThread.IsAlive)
            {
                animalThread = new Thread(AnimalThask);
                animalThread.IsBackground = true;
                animalThread.Priority = ThreadPriority.Highest;
                animalThread.Start();
            }
        }
        private void AnimalThask(object obj)
        {
            while (true)
            {
                if (isNotAllowShowAnimalion == false && animalion != null && animalion.IsPrepareAnimaled)
                {
                    if (!animalion.IsAnimailing)
                    {
                        //每50ms执行一次
                        try
                        {
                            if (sleepTime > 0)
                            {
                                Thread.Sleep(sleepTime);
                            }
                        }
                        catch (Exception)
                        {
                        }
                        this.Invalidate();
                    }
                    else
                    {
                        Thread.Sleep(5);
                    }
                }
                else
                {
                    Thread.Sleep(500);
                }
            }
        }


        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            //if (animalThread != null)
            //{
            //    animalThread.Abort();
            //}
        }
        #endregion

        #region 自动刷新任务
        /// <summary>
        /// 初始化自动刷新线程(默认刷新时间间隔5 * 60)
        /// </summary>
        public void InitAutoUpdateDataThread()
        {
            //自动刷新数据的定时器
            isAutoUpdateData = true;
            Thread thread = new Thread(ThreadStart);
            thread.IsBackground = true;
            thread.Start();
        }

        /// <summary>
        /// 线程任务, 自动刷新数据(如果有动画则优先进行动画刷新数据)
        /// </summary>
        public void ThreadStart()
        {
            while (IsReadyAutoUpdate)
            {
                try
                {
                    if (autoUpdateTime > 0)
                    {
                        Thread.Sleep(1000 * autoUpdateTime);
                        if (isAutoUpdateData && !IsUpdateing)
                        {
                            IsUpdateing = true;
                            AutoUpdateData();
                        }
                        IsUpdateing = false;
                    }
                    else
                    {
                        Thread.Sleep(1000 * 5 * 60);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        /// <summary>
        /// 自动刷新数据检测(自动开启动画效果)
        /// </summary>
        public void AutoUpdateData()
        {
            //基于动画刷新数据(空实现也是可以刷新数据的, 记得在prepareAnimal中关闭接下来的动画效果就可以, 就不会产生帧动画引起的卡顿))
            if (!isNotAllowShowAnimalion && animalion != null)
            {
                animalion.IsPrepareAnimaled = true;
            }
            //数据的自动刷新通知
            if (OnNotifyDataModelListChageEvent != null)
            {
                adapter.setIndex(-1);
                adapter.setData(OnNotifyDataModelListChageEvent());
                //重定位位置
                adapter.setBasePostitionRect(adapter.PosRectData);
                InvalidateView();
            }
        }
        #endregion

        private void InvalidateView()
        {
            if ((DateTime.Now - InvalidateTime).TotalMilliseconds > 40)
            {
                this.Invalidate();
                InvalidateTime = DateTime.Now;
            }
        }

        //位置待定, 形式多样化报表绘制坐标
        public abstract void DrawSelfDefineView(Graphics g, Pen linePen, Brush lineBrush, Brush TextBrush, Brush DataBrush, System.Drawing.Font FontText, System.Drawing.Font FontData);

        //子类的绘制
        public abstract void childPaint(Graphics g, DataModel data, Pen linePen, Brush lineBrush, Brush TextBrush, Brush DataBrush, System.Drawing.Font FontText, System.Drawing.Font FontData);

        #region event事件
        /// <summary>
        /// 鼠标移动的接口回调
        /// </summary>
        public event OnMouseMove_ReportViewPanel OnMouseMove_ReportViewPanelEvent;

        /// <summary>
        /// panel 双击响应事件
        /// </summary>
        public event OnDoubleClickListener OnDoubleClickListenerEvent;

        /// <summary>
        /// panel 回退图形
        /// </summary>
        public event OnDoubleClickShowMaxReportView OnDoubleClickReBackViewEvent;

        /// <summary>
        /// panel 双击大图展示
        /// </summary>
        public event OnDoubleClickShowReportView OnDoubleClickSelfViewEvent;

        /// <summary>
        /// 刷新数据时的委托事件
        /// </summary>
        public event OnNotifyDataChange OnNotifyDataModelListChageEvent;
        public bool IsReadyAutoUpdate = true;
        #endregion

        #region 存根属性
        /// <summary>
        /// 是否绘制边框(默认false)
        /// </summary>
        public bool IsDrawBoard { get; set; }

        /// <summary>
        /// Panel边界的颜色(默认白色)
        /// </summary>
        public Color BoardColor { get; set; }

        /// <summary>
        /// Panel角框的颜色(默认白色)
        /// </summary>
        public Color FrameColor { get; set; }

        /// <summary>
        /// 是否绘制标题
        /// </summary>
        public bool IsDrawTitle { get; set; }

        /// <summary>
        /// 字体颜色(默认白色)
        /// </summary>
        public Color TextColor { get; set; }

        /// <summary>
        /// 数据颜色(默认白色)
        /// </summary>
        public Color DataColor { get; set; }

        /// <summary>
        /// 绘制的颜色(默认蓝色)
        /// </summary>
        public Color LineColor { get; set; }

        /// <summary>
        /// 字体大小(默认8)
        /// </summary>
        public float TextSize { get; set; }

        /// <summary>
        /// 数据字体大小(默认8)
        /// </summary>
        public float DataSize { get; set; }

        /// <summary>
        /// 绘制圆弧线条的粗细
        /// </summary>
        public float StrokenWidth { get; set; }

        /// <summary>
        /// 绘制图形的起始X
        /// </summary>
        public int EStartX { get; set; }

        /// <summary>
        /// 绘制图形的起始Y
        /// </summary>
        public int EStartY { get; set; }

        /// <summary>
        /// 图形的宽度
        /// </summary>
        public int EViewWidth { get; set; }

        /// <summary>
        /// 图形的高度
        /// </summary>
        public int EViewHeight { get; set; }

        /// <summary>
        /// 是否自动调整图形大小(默认true)
        /// 如果是, 自己设定的注解宽高, 图形宽高, 字体大小都将失效, 变为自动调整
        /// 如果设置IsAutoSize = false,必须先自定义确定ViewWith和ViewHeight
        /// 必须重写ReportViewSizeChanged()方法来自己调整大小
        /// </summary>
        public bool IsAutoReSizeView { get; set; }

        /// <summary>
        /// 是否水平居中显示(默认true)
        /// 如果设置IsAutoSize = false 而且要水平居中显示则必须设置图形宽高
        /// 设计者必须先确定EViewWith和EViewHeight
        /// </summary>
        public bool IsHorizontally { get; set; }

        /// <summary>
        /// 新窗体的背景图片
        /// </summary>
        public Image NewFormBackgroundImage { get; set; }

        /// <summary>
        /// 新窗体的背景色
        /// </summary>
        public Color NewFormBackColor { get; set; }

        /// <summary>
        /// 是否绘制边框
        /// </summary>
        public bool IsDrawFrame { get; set; }
        #endregion

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);

        }

    }

    #region 委托事件
    /// <summary>
    /// 双击大图展示(默认的)
    /// </summary>
    public delegate Form OnDoubleClickShowMaxReportView();

    /// <summary>
    /// 双击大图展示（需要自定义）
    /// </summary>
    public delegate void OnDoubleClickShowReportView();

    /// <summary>
    /// 双击响应事件
    /// </summary>
    public delegate void OnDoubleClickListener(DataModel data);

    /// <summary>
    /// 数据刷新操作
    /// </summary>
    public delegate List<DataModel> OnNotifyDataChange();

    /// <summary>
    /// 鼠标移动事件
    /// </summary>
    public delegate void OnMouseMove_ReportViewPanel(DataModel data);
    #endregion
}
