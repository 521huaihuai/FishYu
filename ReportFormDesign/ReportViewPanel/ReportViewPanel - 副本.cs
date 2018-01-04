using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ReportFormDesign.CurrentPosition;
using ReportFormDesign.Data;
using ReportFormDesign.DrawUtils;

namespace ReportFormDesign.ReportViewPanel
{


    /// <summary>
    /// 自定义控件 报表
    /// Design By @author zjm
    /// </summary>
    abstract class ReportViewPanel : Panel
    {

        protected Panel popPanel;

        //绘制报表的帮助类
        protected ReportViewUtils utils = new ReportViewUtils();
        //数据的适配器
        private ReportViewAdapter adapter;
        //鼠标移动的接口回调
        private ReportViewMouseMoveCallBack callBack;
        //所有的坐标点的记录
        protected List<PositionRectData> list;
        //当前鼠标的指定的数据
        private CurrentPositionRectData currentData = null;

        p

        private int currentIndex = 0;


        private Color TextColor;
        //线条颜色
        private Color myColor;
        //背景色
        private Color backGroundColor;
        //最大高度
        private int MaxHeight = 32767;

        public ReportViewPanel()
        {
            list = new List<PositionRectData>();
            popPanel = new Panel();
            this.Controls.Add(popPanel);
            DoubleBuffered = true;
            //this.AutoScroll = true;
        }


        /// <summary>
        /// 设置适配器
        /// </summary>
        /// <param name="adapter"></param>
        internal void setAdapter(ReportViewAdapter adapter)
        {
            this.adapter = adapter;
            //设置高度
            int count = adapter.getCount();
            adapter.next();
            int height = adapter.getPositionRect().bottom - adapter.getPositionRect().top + adapter.getPadding();
            this.Height = height * count + adapter.getPadding();
            adapter.setIndex(-1);
        }

        /// <summary>
        /// 为鼠标添加监听回调函数
        /// </summary>
        /// <param name="callBack"></param>
        public void setOnMouseMoveCallBack(ReportViewMouseMoveCallBack callBack)
        {
            if (callBack != null)
            {
                this.callBack = callBack;
            }
        }


        /// <summary>
        /// 双击响应事件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDoubleClick(EventArgs e)
        {
            //base.OnDoubleClick(e);
        }


        /// <summary>
        /// 点击事件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClick(EventArgs e)
        {
            //base.OnClick(e);
        }

        protected override void OnScroll(ScrollEventArgs se)
        {
            base.OnScroll(se);
        }

        /// <summary>
        /// 鼠标移动事件, 回调给用户鼠标经过的图形化位置数据
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            int x = e.X;
            int y = e.Y;
            //DataModel data = utils.mouseMove(x, y);
            DataModel data = mouseMove(x, y);
            if (data != null)
            {
                if (callBack != null)
                {
                    //回调鼠标移过的数据
                    callBack.OnMouseMoveCallBack(data);
                    //this.Invalidate();
                }
               
            }
        }



        /// <summary>
        /// 鼠标移动判定是哪个数据被选中
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>DataModel 基础数据类型</returns>
        private DataModel mouseMove(int x, int y)
        {

            //Console.WriteLine("x = {0}, y ={1}", x, y);
            //全部遍历太耗时;从y获取比较接近的index
            //如果panel的数据太多超过最大高度 Y会变为负数;
            if (y < 0)
            {
                y = -y;
                if (MaxHeight < y)
                {
                    MaxHeight = y;
                }
                y = MaxHeight + (MaxHeight - y);
            }
            //Console.WriteLine("x = {0}, y ={1}", x, y);
            int index = getIndexFromList(y);
            int size = list.Count;
            Console.WriteLine("index = {0}, count = {1}", index, list.Count);
            PositionRectData item = null;
            int times = 0;
            for (int i = index ; i < size; i++)
            {
                item = list[i];
                times++;
                if (item.isInRect(x, y))
                {
                    if (currentIndex == i)
                    {
                        return item.data;
                    }
                    currentIndex = i;
                    this.Invalidate();
                    currentData = new CurrentPositionRectData(x, y, item.data);
                    popPanel.Location = new Point(x, y);
                    popPanel.Width = currentData.Width;
                    popPanel.Height = currentData.Height;
                    popPanel.BackColor = Color.White;
                    //graphics.FillRectangle(new SolidBrush(Color.Black), rect);
                    return item.data;
                }
            }
            Console.WriteLine("times = {0}", times);
            return null;
        }

        private int getIndexFromList(int y)
        {
            //throw new NotImplementedException();
            if (list.Count == 0)
            {
                return 0;
            }
            return (int)(y * 1.0f * list.Count/this.Height);
        }


        /// <summary>
        /// 绘制操作
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            //自己声明的Graphics
            Graphics g = e.Graphics;
            if (backGroundColor != null)
            {
                Brush background = new SolidBrush(backGroundColor);
                Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
                g.FillRectangle(background, rect);
                background.Dispose();
            }
            if (adapter != null)
            {
                adapter.setIndex(-1);
                DataModel data = null;
                PositionRectData rectData = null;
                while (adapter.next())
                {
                    data = adapter.getItem();
                    rectData = adapter.getPositionRect();
                    childPaint(e, rectData, myColor, TextColor);
                    rectData.right = this.Width;
                    list.Add(rectData);
                }

                introducePaint(g, currentData, myColor, TextColor);
            }
            //释放资源
            //utils.disposeGraphics(g);
            //g.Dispose();
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            //base.OnMouseWheel(e);
            list = new List<PositionRectData>();
            this.Invalidate();
        }

        /// <summary>
        /// 设置颜色
        /// </summary>
        /// <param name="color1">线条颜色</param>
        /// <param name="color2">字体数据颜色</param>
        internal void setColor(System.Drawing.Color color1, System.Drawing.Color color2)
        {
            myColor = color1;
            TextColor = color2;
        }



        /// <summary>
        /// 设置背景颜色
        /// </summary>
        /// <param name="color"></param>
        internal void setBackGroundColor(Color color)
        {

            backGroundColor = color;
            //throw new NotImplementedException();
        }




        /// <summary>
        /// 子类的绘制
        /// </summary>
        /// <param name="e"></param>
        public abstract void childPaint(PaintEventArgs e, PositionRectData rectPosData, Color GraphicalColor, Color TextColor);


        /// <summary>
        /// 介绍之类的绘制
        /// </summary>
        /// <param name="e"></param>
        /// <param name="rectPosData"></param>
        /// <param name="GraphicalColor"></param>
        /// <param name="TextColor"></param>
        public abstract void introducePaint(Graphics g, CurrentPositionRectData rectPosData, Color GraphicalColor, Color TextColor);
    }
}
