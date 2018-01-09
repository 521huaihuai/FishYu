using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ReportFormDesign.CurrentPosition;
using ReportFormDesign.Model;
using ReportFormDesign.DrawUtils;
using System.Drawing.Drawing2D;

namespace ReportFormDesign.ReportViewPanel
{
    /// <summary>
    /// 主要负责报表的绘制
    /// 以及popView的基本设置(不负责具体的popView绘制)
    /// </summary>
    public class Radius_Rectangle_ReportView : ViewPanel
    {
        public Radius_Rectangle_ReportView()
        {
            toolTip = new FollowPopView();
            toolTip.BackGroundColor = ReportViewUtils.perferWhite_Shallow;
            toolTip.Padding = 20;
            toolTip.Width = 100;
            toolTip.TextSize = 10;
            toolTip.TextColor = ReportViewUtils.perferRed;
            toolTip.DataColor = ReportViewUtils.perferRed;
        }

        public override void childPaint(Graphics g, DataModel Data, Pen linePen, Brush lineBrush, Brush TextBrush, Brush DataBrush, System.Drawing.Font font_Text, System.Drawing.Font font_Data)
        {
            ///建议把绘制的直接写在这里
            int newStart_X = Data.Area.left + 5 * padding;
            //圆角的弧度
            int height = Data.Area.bottom - Data.Area.top;

            int width = this.Width - newStart_X - 2 * padding - height;
            //int width = this.Width - newStart_X - 2 * padding;
            int radius = height / 2;
            float per = width * 1.0f / maxNum;
            float realShowData = per * Data.mainData;

            Rectangle rect = new Rectangle(newStart_X, Data.Area.top, width + height, height);
            Rectangle rectReal = new Rectangle(newStart_X, Data.Area.top, (int)realShowData + height, height);
            
            //Rectangle rect = new Rectangle(newStart_X, Data.Area.top, width, height);
            //Rectangle rectReal = new Rectangle(newStart_X, Data.Area.top, (int)realShowData, height);
            //底色
            Brush selfBrush = new SolidBrush(Color.FromArgb(200, 88, 94, 92));
            GraphicsPath path = ReportViewUtils.CreateRoundedRectanglePath(rect, radius);
            //绘制底色
            g.FillPath(selfBrush, path);

            //绘制展示色
            path = ReportViewUtils.CreateRoundedRectanglePath(rectReal, radius);
            g.FillPath(lineBrush, path);

            //绘制字体
            g.DrawString(Data.mainText, font_Text, TextBrush, Data.Area.left + (newStart_X - Data.Area.left - Data.mainText.Length * TextSize) / 2, Data.Area.top);

            //绘制右侧百分比
            //float perNum = Data.mainData * 1.0f / maxNum * 100;
            //string str = ((int)perNum).ToString();
            //g.DrawString(str + "%", font_Data, DataBrush, width - (str.Length + 1) * TextSize + startX, startY);

            path.Dispose();
            selfBrush.Dispose();

            //utils.drawReportView(g, RePortViewStyle.Arc_Angle_rectangle_EventHandle, Data.Area.left, Data.Area.top, Data.Area.right, Data.Area.bottom - Data.Area.top, linePen.Color, Data.mainText, Data.mainData, 400, TextSize, DataSize);

            ///建议把绘制的直接写在这里
            if (Data.Area.IsMouseIn)
            {
                Color color = linePen.Color;
                Brush myBrush = new SolidBrush(Color.FromArgb(50, color.R, color.G, color.B));
                g.FillRectangle(myBrush, new Rectangle(Data.Area.left, Data.Area.top - padding / 2,
                     this.Width, Data.Area.bottom - Data.Area.top + padding));
                myBrush.Dispose();
            }
        }


        public override void introducePaint(Graphics e, DataModel rectPosData, Color GraphicalColor, Color TextColor, float TextSize)
        {
            //Console.WriteLine("redraw");
            //if (rectPosData != null)
            //{
            //    utils.drawReportView(e, RePortViewStyle.Simple_Rectangle, rectPosData.X, rectPosData.Y, rectPosData.Width, rectPosData.Height, GraphicalColor, new SolidBrush(TextColor), new SolidBrush(TextColor), rectPosData.data.mainText, rectPosData.data.mainData, maxNum, "", 0, 8, 8);
            //}
        }
    }
}
