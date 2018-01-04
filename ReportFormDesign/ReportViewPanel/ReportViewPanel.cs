using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ReportFormDesign.CurrentPosition;
using ReportFormDesign.Model;
using ReportFormDesign.DrawUtils;
using System.Drawing.Drawing2D;

namespace ReportFormDesign.ReportViewPanel
{


    /// <summary>
    /// 自定义控件 报表
    /// Design By @author zjm
    /// </summary>
    public abstract class ViewPanel : Panel
    {
        //绘制报表的帮助类
        public ReportViewUtils utils = new ReportViewUtils();

        //数据的适配器
        private ReportViewAdapter adapter;

        //鼠标移动的接口回调
        //private ReportViewMouseMoveCallBack callBack;
        public event OnMouseMove_ReportViewPanel OnMouseMove_ReportViewPanelEvent;

        /// <summary>
        /// 鼠标移动选中的标记
        /// </summary>
        protected ReportViewToolTip toolTip;

        /// <summary>
        /// 所有的坐标点的记录
        /// *****************************
        /// 这个列表不合理啊，会严重影响效率
        /// 因为你在Scroll事件中进行清理，但是在OnPaint里加入
        /// OnPaint是实时绘制的，每秒达到50次都可以，也就是说这个列表会被无限膨胀
        /// 效率随着后期对实时绘制的增加逐步降低
        /// *****************************
        /// 放弃这个列表，直接把这个值存储在变量里
        /// *****************************
        /// </summary>
        //public List<PositionRectData> list;
        //是否是垂直展示数据
        public static bool isVerticalShowData = true;
        //字体颜色
        public Color TextColor { get; set; }
        //数据颜色
        public Color DataColor { get; set; }
        //背景色
        public Color BackGroundColor{ get; set; }
        //绘制的颜色
        public Color LineColor { get; set; }

        /// <summary>
        /// 字体大小
        /// </summary>
        public int TextSize { get; set; }

        /// <summary>
        /// 数据字体大小
        /// </summary>
        public int DataSize { get; set; }

        /// <summary>
        /// 绘制圆弧线条的粗细
        /// </summary>
        public float StrokenWidth { get; set; }

        //设置边距
        public static int padding = 20;

        ///每条数据的最大数据值
        protected int maxNum { get; set; }

        //最大高度
        private int MaxHeight = 32767;

        //用来标定当前鼠标滚动的位置
        private int currentScrollY = 0;


        public ViewPanel()
        {
            //list = new List<PositionRectData>();
            DoubleBuffered = true;
            //初始化数据
            LineColor = ReportViewUtils.perferBlue;
            TextColor = ReportViewUtils.perferWhite;
            DataColor = ReportViewUtils.perferWhite;
            BackGroundColor = ReportViewUtils.perferBlue_Deep;
            TextSize = 8;
            DataSize = 8;
            StrokenWidth = 5;
            maxNum = 100;
            //this.Height = 500;
            //this.Width = 500;
            this.AutoScroll = true;
            //this.Dock = DockStyle.Left;
            //this.AutoScroll = true;
        }


        /// <summary>
        /// 设置适配器
        /// </summary>
        /// <param name="adapter"></param>
        public void setAdapter(ReportViewAdapter adapter)
        {
            this.adapter = adapter;
            int count = adapter.getCount();
            adapter.next();
            if (isVerticalShowData)
            {
                //设置高度
                int height = adapter.getPositionRect().bottom - adapter.getPositionRect().top + padding;
                //this.Height = height * count + padding;

                Label lable = new Label();
                lable.Location = new Point(0, height * count + padding);
                lable.Text = "";
                lable.Width = 0;
                this.Controls.Add(lable);
            }
            else
            {
                //设置宽度
                int width = adapter.getPositionRect().right - adapter.getPositionRect().left + padding;
                this.Width = width * count + padding;
            }
            
            adapter.setIndex(-1);
        }


        /// <summary>
        /// 点击事件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClick(EventArgs e)
        {
            //base.OnClick(e);
            this.Focus();
        }


        /// <summary>
        /// 鼠标移动事件, 委托给用户鼠标经过的图形化位置数据
        /// </summary>
        /// <param name="e"></param>
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
                    if (data.Area.isInRect(x, y))
                    {

                        //判定鼠标是否在这个区域上面
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
                
                //如果超出边界
                if (x > this.Width - toolTip.Width - padding)
                {
                    if (x > this.Width - padding)
                    {
                        toolTip.isVisible = false;
                    }
                    else
                    {
                        x = this.Width - toolTip.Width - 2 * padding;
                        toolTip.isVisible = true;
                    }
                }
                if (x < 0)
                {
                    toolTip.isVisible = false;
                }
                if (y > this.Height - toolTip.Height - padding)
                {
                    y = this.Height - toolTip.Height - padding;
                }
                toolTip.LocalPosition = new Point(x, y);
            }
            this.Invalidate();
        }



        /// <summary>
        /// 鼠标移动判定是哪个数据被选中
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>DataModel 基础数据类型</returns>
        private DataModel mouseMove(int x, int y)
        {
            return null;
            //Console.WriteLine("x = {0}, y ={1}", x, y);
            //全部遍历太耗时;从y获取比较接近的index-----主要是list引起的，查看上文list的备注
            //if (y < 0)
            //{
            //    y = -y;
            //    if (MaxHeight < y)
            //    {
            //        MaxHeight = y;
            //    }
            //    y = MaxHeight + (MaxHeight - y);
            //}
            //Console.WriteLine("x = {0}, y ={1}", x, y);
            //int index = getIndexFromList(y);
            //int size = list.Count;
            //Console.WriteLine("index = {0}, count = {1}", index, list.Count);
            //PositionRectData item = null;
            //int times = 0;
            //for (int i = index; i < list.Count; i++)///这里使用list.count更加安全，没法保证size和list.count完全一致，如果不一致会超出索引，导致程序整体崩溃
            //{
            //    item = list[i];
            //    times++;
            //    if (item.isInRect(x, y))
            //    {
            //        ///设置这个变量就好了,其他的通过事件更合理
            //        //item.IsMouseIn = true;

            //        if (currentIndex == i)
            //        {
            //            return item.data;
            //        }
            //        currentIndex = i;
            //        this.Invalidate();
            //        currentData = new CurrentPositionRectData(x, y, item.data);
            //        popPanel.Location = new Point(x, y);
            //        popPanel.Width = currentData.Width;
            //        popPanel.Height = currentData.Height;
            //        popPanel.BackColor = Color.White;
            //        ///Rectangle rect = new Rectangle(0, 0, 80, 40);这个New的没啥意义啊
            //        //graphics.FillRectangle(new SolidBrush(Color.Black), rect);
            //        return item.data;
            //    }
            //}
            //Console.WriteLine("times = {0}", times);
            //return null;
        }

        //private int getIndexFromList(int y)
        //{
        //    //throw new NotImplementedException();
        //    if (list.Count == 0)
        //    {
        //        return 0;
        //    }
        //    return (int)(y * 1.0f * list.Count / this.Height);
        //}


        /// <summary>
        /// 绘制操作
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            //自己声明的Graphics
            Graphics g = e.Graphics;
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
            Font font_Text = new Font("Consolas", TextSize);
            Font font_Data = new Font("Consolas", DataSize);

            //绘制背景
            if (BackGroundColor != null)
            {
                Brush background = new SolidBrush(BackGroundColor);
                Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
                g.FillRectangle(background, rect);
                background.Dispose();
            }


            DataModel mouseMoveModel = null;
            if (adapter != null)
            {
                adapter.setIndex(-1);
                DataModel data = null;
                AreaPositionRect rectData = null;
                while (adapter.next())
                {
                    data = adapter.getItem();
                    rectData = adapter.getPositionRect();
                    data.Area.left = rectData.left;
                    data.Area.right = rectData.right;
                    data.Area.top = rectData.top - currentScrollY;
                    data.Area.bottom = rectData.bottom - currentScrollY;
                    ///这个写的挺合理的
                    ///子类自己的绘制操作
                    childPaint(g, data, linePen, lineBrush, TextBrush, DataBrush, font_Text, font_Data);
                    if (isVerticalShowData)
                    {
                        data.Area.right = this.Width;
                    }
                    //data.Area.right = this.Width;
                    if (data.Area.IsMouseIn)
                    {
                        mouseMoveModel = data;
                    }
                    //rectData.right = this.Width;
                    //list.Add(rectData);
                }
                //数据辅助类的绘制
                introducePaint(g, mouseMoveModel, LineColor, TextColor, TextSize);
            }

            //释放资源
            disposeResource(linePen, lineBrush, TextBrush, DataBrush, font_Text, font_Data);
            
            ///绘制ToolTip
            if (toolTip != null)
            {
                toolTip.OnRender(g, mouseMoveModel);
            }
            //utils.disposeGraphics(g);
            //g.Dispose();
        }

        /// <summary>
        /// 释放资源
        /// </summary>
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



        /// <summary>
        /// 鼠标滚动时的数据刷新
        /// </summary>
        protected override void OnMouseWheel(MouseEventArgs e)
        {

            ////获取光标位置
            //Point mousePoint = new Point(e.X, e.Y);
            ////换算成相对本窗体的位置
            //mousePoint.Offset(this.Location.X, this.Location.Y);
            ////判断是否在panel内
            //if (this.RectangleToScreen(this.DisplayRectangle).Contains(mousePoint))
            //{
            //    //滚动
            //    this.AutoScrollPosition = new Point(0, this.VerticalScroll.Value - e.Delta);
            //    //this.VerticalScroll.Value += 10;
            //}
            //判断是否在panel内
            if (e.X < Location.X + this.Width && e.X > Location.X && e.Y > Location.Y && e.Y < Location.Y + this.Height)
            {
                //滚动
                this.AutoScrollPosition = new Point(0, this.VerticalScroll.Value - e.Delta);
            }

            //用于标定数据的位置
            currentScrollY = this.VerticalScroll.Value;
            //Console.WriteLine("VerticalScroll = {0}", this.VerticalScroll.Value);
            //this.Refresh();
            this.Invalidate();
            //this.Update();
        }

        protected override void OnScroll(ScrollEventArgs se)
        {
            this.Focus();
            //用于标定数据的位置
            currentScrollY = this.VerticalScroll.Value;
        }

        /// <summary>
        /// 子类的绘制
        /// </summary>
        public abstract void childPaint(Graphics g, DataModel data, Pen linePen, Brush lineBrush, Brush TextBrush, Brush DataBrush, System.Drawing.Font font_Text, System.Drawing.Font font_Data);

        /// <summary>
        /// 介绍之类的绘制
        /// </summary>
        /// <param name="e"></param>
        /// <param name="rectPosData"></param>
        /// <param name="GraphicalColor"></param>
        /// <param name="TextColor"></param>
        public abstract void introducePaint(Graphics g, DataModel rectPosData, Color lineColor, Color TextColor, float TextSize);

    }

    /// <summary>
    /// 鼠标移动事件
    /// </summary>
    /// <param name="data"></param>
    public delegate void OnMouseMove_ReportViewPanel(DataModel data);
}
