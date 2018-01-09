using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using ReportFormDesign.DrawUtils;

namespace ReportFormDesign.Model
{
    /// <summary>
    /// 标记信息
    /// </summary>
    public abstract class ReportViewToolTip
    {
        public string Text;

        public ReportViewToolTip()
        {
            BackGroundColor = ReportViewUtils.perferShallowGray3;
            TextColor = ReportViewUtils.perferRed;
            DataColor = ReportViewUtils.perferRed;
            Width = 100;
            TextSize = 10;
            DataSize = 10;
            Padding = 20;
            PaddingIn = 10;
            Height = 40;
            IsVisible = true;
        }

        /// <summary>
        /// 绘制基类
        /// </summary>
        /// <param name="g">绘制的图形化要素</param>
        public abstract void detailDraw(Graphics g, DataModel model, Brush TextBrush, Brush DataBrush, Brush BackGroundBrush, Font TextFont, Font DataFont);

        public virtual void OnRender(Graphics g, DataModel model)
        {
            if (IsVisible)
            {
                Brush BackGroundBrush = new SolidBrush(BackGroundColor);
                Brush TextBrush = new SolidBrush(TextColor);
                Brush DataBrush = new SolidBrush(DataColor);
                Font Text_Font = new Font("Consolas", TextSize, FontStyle.Bold);
                Font Data_Font = new Font("Consolas", DataSize, FontStyle.Bold);
                if (model != null)
                {
                    ResizeWithAndHeight(g, model.mainText, model.mainData + "", "", Text_Font, Data_Font);
                    detailDraw(g, model, TextBrush, DataBrush, BackGroundBrush, Text_Font, Data_Font);
                    Text = model.mainText;
                }
                BackGroundBrush.Dispose();
                TextBrush.Dispose();
                DataBrush.Dispose();
                Text_Font.Dispose();
                Data_Font.Dispose();
                //g.Dispose();
            }
           
        }

        /// <summary>
        /// 重缩放tooltip的宽高
        /// </summary>
        protected void ResizeWithAndHeight(Graphics g, string mainText, string mainData, string tag, Font Text_Font, Font Data_Font)
        {
            this.Width = Math.Max(ResizeWidth(g, mainText, Text_Font), ResizeWidth(g, mainData, Data_Font));
            this.Width = Math.Max(ResizeWidth(g, tag, Text_Font), this.Width);
            this.Height = Math.Max(ResizeHeight(g, mainText, Text_Font), ResizeHeight(g, mainData, Data_Font));
        }

        protected int ResizeWidth(Graphics g, string Text, Font TextFont)
        {
            SizeF sizef = g.MeasureString(Text, TextFont);
            return (int)sizef.Width + 2 * PaddingIn;
        }

        protected int ResizeHeight(Graphics g, string Text, Font TextFont)
        {
            SizeF sizef = g.MeasureString(Text, TextFont);
            return (int)sizef.Height + 2 * PaddingIn;
        }

        /// <summary>
        /// 基础位置
        /// </summary>
        public Point LocalPosition{get; set;}

        /// <summary>
        /// 背景色
        /// </summary>
        public Color BackGroundColor { get; set; }
        /// <summary>
        /// 字体颜色
        /// </summary>
        public Color TextColor { get; set; }
        /// <summary>
        /// 数据颜色
        /// </summary>
        public Color DataColor { get; set; }
        /// <summary>
        /// 宽度
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// 高度
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// 左边距
        /// </summary>
        public int Padding { get; set; }
        /// <summary>
        /// 字体型号
        /// </summary>
        public float TextSize { get; set; }
        /// <summary>
        /// 数据字体型号
        /// </summary>
        public int DataSize { get; set; }

        /// <summary>
        /// 是否展示toolTip
        /// </summary>
        public bool IsVisible { get; set; }

        /// <summary>
        /// 内边距
        /// </summary>
        public int PaddingIn { get; set; }

        /// <summary>
        /// 是否绘制底色
        /// </summary>
        public bool IsDrawBackGround { get; set; }

    }
}
