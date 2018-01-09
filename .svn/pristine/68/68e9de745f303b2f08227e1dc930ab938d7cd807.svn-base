using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using ReportFormDesign.DrawUtils;
using ReportFormDesign.Model;

namespace ReportFormDesign.ReportViewPanel
{
    class Arc_Splite_ReportView : ViewPanel
    {

        private Color oldColor;
        private bool isFirst = true;

        public Arc_Splite_ReportView()
        {
            toolTip = new FollowPopView();
            toolTip.BackGroundColor = ReportViewUtils.perferWhite_Shallow;
            toolTip.Padding = 20;
            toolTip.Width = 100;
            toolTip.TextSize = 10;
            toolTip.TextColor = ReportViewUtils.perferRed;
            toolTip.DataColor = ReportViewUtils.perferRed;
        }
        public override void childPaint(System.Drawing.Graphics g, Model.DataModel Data, System.Drawing.Pen linePen, System.Drawing.Brush lineBrush, System.Drawing.Brush TextBrush, System.Drawing.Brush DataBrush, System.Drawing.Font font_Text, System.Drawing.Font font_Data)
        {
            if (isFirst)
            {
                oldColor = linePen.Color;
                isFirst = false;
            }
            if (Data != null)
            {
                ///建议把绘制的直接写在这里
                if (Data.Area.IsMouseIn)
                {
                    linePen.Color = ReportViewUtils.perferRed;
                }
                else
                {
                    linePen.Color = oldColor;
                }
                ChildDataModel data = (ChildDataModel)Data;
                utils.drawReportView(g, RePortViewStyle.Arc_rectangle, Data.Area.left, Data.Area.top, Data.Area.right - Data.Area.left, Data.Area.bottom - Data.Area.top, linePen.Color, Data.mainText, Data.mainData, 200, data.childText, data.childData, 10, 16);
                int startX = Data.Area.left;

                //圆点的大小
                //int CircleRaius = 10;
                //SolidBrush sbrush1 = new SolidBrush(Color.FromArgb(150, 1, 77, 103));
                //Rectangle rect = new Rectangle(startX, startY, width, height);
                //float realAngle = 360 * Data_InArc * 1.0f / Data_InArc_Max;
                ////顺时针绘制
                //float startAngle = 360 - realAngle;
                //g.DrawArc(myPen, rect, startAngle, realAngle);
                ////绘制圆弧内的字体
                //g.DrawString(Text_InArc, font_Text, myBrush, (width - TextSize * (Text_InArc.Length + 2)) / 2 + startX, height / 2 - 2 * padding + startY);
                ////绘制圆弧内的数据
                //g.DrawString(Data_InArc + "", font_Data, DataBrush, (width - DataSize * (Data_InArc.ToString().Length)) / 2 + startX, height / 2 + padding + startY);

                ////绘制圆弧外的字体
                //Rectangle Arc_Out = new Rectangle(startX, startY + height + 4 * padding / 3, width, 30);
                //g.FillEllipse(sbrush1, Arc_Out);
                //g.DrawString(Text_OutArc + Data_OutArc + "%", font_Text, TextBrush, (width - TextSize * (Text_InArc.Length + Data_OutArc.ToString().Length + 3)) / 2 + startX, height + 2 * padding + startY);


                ////定位圆点
                //int y = (int)(Math.Sin(startAngle / 180 * Math.PI) * width / 2);
                //int x = (int)(Math.Cos(startAngle / 180 * Math.PI) * width / 2);
                //Rectangle rect_Circle = new Rectangle(startX + width / 2 - CircleRaius / 2 + x, startY + height / 2 - CircleRaius / 2 + y, CircleRaius, CircleRaius);
                ////绘制圆点
                //g.FillEllipse(myBrush, rect_Circle);

                //sbrush1.Dispose();
            
            }
            
        }

        public override void introducePaint(System.Drawing.Graphics g, Model.DataModel rectPosData, System.Drawing.Color lineColor, System.Drawing.Color TextColor, float TextSize)
        {
        }
    }
}
