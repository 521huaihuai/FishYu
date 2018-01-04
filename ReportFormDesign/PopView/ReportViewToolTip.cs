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

        public ReportViewToolTip()
        {
            BackGroundColor = ReportViewUtils.perferWhite_Shallow;
            TextColor = ReportViewUtils.perferRed;
            DataColor = ReportViewUtils.perferRed;
            Width = 100;
            TextSize = 10;
            DataSize = 10;
            Padding = 20;
            Height = 40;
            isVisible = true;
        }

        /// <summary>
        /// 绘制基类
        /// </summary>
        /// <param name="g">绘制的图形化要素</param>
        public abstract void detailDraw(Graphics g, DataModel model, Brush TextBrush, Brush DataBrush, Brush BackGroundBrush, Font TextFont, Font DataFont);

        public void OnRender(Graphics g, DataModel model)
        {
            if (isVisible)
            {
                Brush BackGroundBrush = new SolidBrush(BackGroundColor);
                Brush TextBrush = new SolidBrush(TextColor);
                Brush DataBrush = new SolidBrush(DataColor);
                Font Text_Font = new Font("Consolas", TextSize);
                Font Data_Font = new Font("Consolas", DataSize);
                detailDraw(g, model, TextBrush, DataBrush, BackGroundBrush, Text_Font, Data_Font);
                BackGroundBrush.Dispose();
                TextBrush.Dispose();
                DataBrush.Dispose();
                Text_Font.Dispose();
                Data_Font.Dispose();
            }
           
        }

        /// <summary>
        /// 基础位置
        /// </summary>
        public Point LocalPosition{get; set;}

        public Color BackGroundColor { get; set; }
        public Color TextColor { get; set; }
        public Color DataColor { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Padding { get; set; }
        public int TextSize { get; set; }
        public int DataSize { get; set; }
        public bool isVisible { get; set; }
    }
}
