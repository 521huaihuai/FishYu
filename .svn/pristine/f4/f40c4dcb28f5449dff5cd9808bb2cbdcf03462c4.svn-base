using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using ReportFormDesign.DrawUtils;
using ReportFormDesign.Model;

namespace ReportFormDesign.ReportViewPanel
{
    public class Arc_Splite_ReportView : ViewPanel
    {

        private Color oldColor;
        private bool isFirst = true;

        public Arc_Splite_ReportView()
        {
            MyToolTip = new FollowPopViewForTitle();
            MyToolTip.BackGroundColor = ReportViewUtils.perferWhite_Shallow;
            MyToolTip.Padding = 20;
            MyToolTip.Width = 100;
            MyToolTip.TextSize = 10;
            MyToolTip.TextColor = ReportViewUtils.perferRed;
            MyToolTip.DataColor = ReportViewUtils.perferRed;
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
                ChildDataModel data = Data as ChildDataModel;
                //圆点的大小
                int startX = data.Area.left;
                int startY = data.Area.top;
                int width = data.Area.right - startX;
                int height = data.Area.bottom - startY;
                int CircleRaius = 10;
                SolidBrush sbrush1 = new SolidBrush(Color.FromArgb(150, 1, 77, 103));
                SolidBrush sf = new SolidBrush(linePen.Color);
                Rectangle rect = new Rectangle(startX, startY, width, height);
                float realAngle = 360 * data.mainData * 1.0f / MaxNum;
                //顺时针绘制
                float startAngle = 360 - realAngle;
                g.DrawArc(linePen, rect, startAngle, realAngle);
                //绘制圆弧内的字体
                ReportViewUtils.drawStringWithLimiteText(g, LocationModel.Location_Down, data.mainText, font_Text, TextBrush, startX, startY, width, height / 2, 5);
                //绘制圆弧内的数据
                ReportViewUtils.drawStringWithLimiteText(g, LocationModel.Location_Up, data.mainData + "", font_Data, DataBrush, startX, startY + height / 2, width, height / 2, 5);

                //绘制圆弧外的字体
                Rectangle Arc_Out = new Rectangle(startX, startY + height + height / 2, width, 30);
                GraphicsPath path = ReportViewUtils.CreateRoundedRectanglePath(Arc_Out, 15);
                g.FillPath(sbrush1, path);
                path.Dispose();
                ReportViewUtils.drawStringWithLimiteText(g, LocationModel.Location_Center, data.childText + "" + data.childData, font_Data, DataBrush, startX, startY + height + height / 2, width, 30, 8);


                //定位圆点
                int y = (int)(Math.Sin(startAngle / 180 * Math.PI) * width/ 2);
                int x = (int)(Math.Cos(startAngle / 180 * Math.PI) * height / 2);
                Rectangle rect_Circle = new Rectangle(startX + width / 2 - CircleRaius / 2 + x, startY + height / 2 - CircleRaius / 2 + y, CircleRaius, CircleRaius);
                //绘制圆点
                g.FillEllipse(sf, rect_Circle);

                sf.Dispose();
                sbrush1.Dispose();
            
            }
            
        }


        public override void DrawSelfDefineView(Graphics g, Pen linePen, Brush lineBrush, Brush TextBrush, Brush DataBrush, Font FontText, Font FontData)
        {
        }

        public ReportViewToolTip MyToolTip
        {
            get
            {
                return toolTip;
            }
            set
            {
                this.toolTip = value;
            }
        }

        /// <summary>
        /// 所有数据的最大数据值(默认100)
        /// </summary>
        public float MaxNum { get; set; }
    }
}
