using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using ReportFormDesign.DrawUtils;
using ReportFormDesign.Model;

namespace ReportFormDesign.ToolTips
{
    public class CoordinateHelpLineToolTip : ReportViewToolTip
    {
        public CoordinateHelpLineToolTip()
        {
            HelpLineColorAlpha = 150;
            HelpLineColor = ReportViewUtils.perferBlue;
            PeerHelpLineWidth = 5;
            CoordinatePeerDistance = 5;
            Padding = 10;
            PaddingIn = 20;
            TextColor = ReportViewUtils.perferBlue;
        }

        public override void OnRender(Graphics g, DataModel model)
        {
            if (IsVisible)
            {
                Brush BackGroundBrush = new SolidBrush(BackGroundColor);
                Brush TextBrush = new SolidBrush(TextColor);
                Brush DataBrush = new SolidBrush(DataColor);
                Font Text_Font = new Font("Consolas", TextSize, FontStyle.Bold);
                Font Data_Font = new Font("Consolas", DataSize, FontStyle.Bold);
                HelpLineColor = Color.FromArgb(HelpLineColorAlpha, HelpLineColor.R, HelpLineColor.G, HelpLineColor.B);
                if (model != null)
                {
                    ResizeWithAndHeight(g, model.mainText, model.mainData + "", "", Text_Font, Data_Font);
                    if (!string.IsNullOrEmpty(model.mainText))
                    {
                        Text = model.mainText;
                    }
                    else
                    {
                        if (model.Tag != null)
                        {
                            Text = model.Tag + "";
                        }
                    }
                    
                }
                detailDraw(g, model, TextBrush, DataBrush, BackGroundBrush, Text_Font, Data_Font);
                BackGroundBrush.Dispose();
                TextBrush.Dispose();
                DataBrush.Dispose();
                Text_Font.Dispose();
                Data_Font.Dispose();
                //g.Dispose();
            }

        }

        public override void detailDraw(System.Drawing.Graphics g, DataModel model, System.Drawing.Brush TextBrush, System.Drawing.Brush DataBrush, System.Drawing.Brush BackGroundBrush, System.Drawing.Font TextFont, System.Drawing.Font DataFont)
        {

            int startX = LocalPosition.X;
            int startY = LocalPosition.Y;
            //绘制提示
            if (model != null)
            {
                //GraphicsPath path = null;
                //if (IsOverWidth(model))
                //{
                //    string tip = "字数过长, 请双击大屏展示";
                //    TextFont.Dispose();
                //    TextFont = new Font("Consolas", 9, FontStyle.Bold);
                //    ResizeWithAndHeight(g, tip, "", TextFont, DataFont);
                //    Rectangle rect = new Rectangle(startX + Padding, startY, Width, Height);
                //    path = ReportViewUtils.CreateRoundedRectanglePath(rect, Height / 3);
                //    //绘制底色
                //    g.FillPath(BackGroundBrush, path);
                //    TextBrush.Dispose();
                //    TextBrush = new SolidBrush(ReportViewUtils.perferRed);
                //    ReportViewUtils.drawString(g, LocationModel.Location_Center, tip, TextFont, TextBrush, startX + Padding, startY, this.Width, this.Height);
                //}
                //else
                //{
                    //Rectangle rect = new Rectangle(startX + Padding, startY, Width, Height);
                    //path = ReportViewUtils.CreateRoundedRectanglePath(rect, Height / 3);
                    //绘制底色
                    //g.FillPath(BackGroundBrush, path);
                if (IsDrawBackGround)
                {
                    Rectangle rect = new Rectangle(startX + Padding, startY + PaddingIn / 2, Width - PaddingIn, Height);
                    GraphicsPath path = ReportViewUtils.CreateRoundedRectanglePath(rect, Height / 6);
                    //绘制底色
                    g.FillPath(BackGroundBrush, path);
                    path.Dispose();
                }


                if (IsDrawXYPoint)
                {
                    if (!string.IsNullOrEmpty(model.mainText))
                    {
                        ReportViewUtils.drawString(g, LocationModel.Location_Left_Left, model.mainText, TextFont, TextBrush, startX + Padding, startY, this.Width + 2 * PaddingIn, this.Height);
                    }
                    else
                    {
                        ReportViewUtils.drawString(g, LocationModel.Location_Left_Left, model.Tag + "", TextFont, TextBrush, startX + Padding, startY, this.Width + 2 * PaddingIn, this.Height);
                    }
                    ReportViewUtils.drawString(g, LocationModel.Location_Left_Left, model.mainData + "", TextFont, TextBrush, startX + Padding, startY + 3 * PaddingIn, this.Width, this.Height);
                }
                else
                {
                    if (!string.IsNullOrEmpty(model.mainText))
                    {
                        ReportViewUtils.drawString(g, LocationModel.Location_Left_Left, model.mainText, TextFont, TextBrush, startX + Padding, startY, this.Width, this.Height);
                    }
                    else
                    {
                        ReportViewUtils.drawString(g, LocationModel.Location_Left_Left, model.Tag + "", TextFont, TextBrush, startX + Padding, startY, this.Width, this.Height);
                    }
                    
                    ReportViewUtils.drawString(g, LocationModel.Location_Left_Left, model.mainData + "", TextFont, TextBrush, startX + Padding, startY + PaddingIn, this.Width, this.Height);
                }
                
                    //ReportViewUtils.drawString(g, LocationModel.Location_Down, model.mainData + "", TextFont, TextBrush, startX + Padding, startY, this.Width, this.Height);
                    //ReportViewUtils.drawString(g, LocationModel.Location_Left_Left, "y : " + startY, TextFont, TextBrush, startX + Padding, startY + Padding, this.Width, this.Height);
                //}
                //path.Dispose(); 
            }
            if (!IsOutView(startX, startY))
            {
                Pen helpLinePen = new Pen(HelpLineColor, 1.0f);
                int count1 = (int)(CoordinateWidth / (PeerHelpLineWidth + CoordinatePeerDistance));
                int count2 = (int)(CoordinateHeight / (PeerHelpLineWidth + CoordinatePeerDistance));
                Point point1;
                Point point2;
                Point point3;
                Point point4;
                for (int i = 0; i < count1; i++)
                {
                    float potX = CoordinateStartX + (i + 1) * PeerHelpLineWidth + (i) * CoordinatePeerDistance;
                    point1 = new Point((int)(potX), startY);
                    point2 = new Point((int)(potX + CoordinatePeerDistance), startY);
                    g.DrawLine(helpLinePen, point1, point2);
                }
                for (int i = 0; i < count2; i++)
                {
                    float potY = CoordinateStartY - (i + 1) * PeerHelpLineWidth - (i) * CoordinatePeerDistance;
                    point3 = new Point(startX, (int)(potY));
                    point4 = new Point(startX, (int)(potY - CoordinatePeerDistance));
                    g.DrawLine(helpLinePen, point3, point4);
                }

                if (IsDrawXYPoint)
                {
                    ReportViewUtils.drawString(g, LocationModel.Location_Left_Left, "x : " + (startX - CoordinateStartX), TextFont, TextBrush, startX + Padding, startY, this.Width, this.Height);
                    ReportViewUtils.drawString(g, LocationModel.Location_Left_Left, "y : " + (CoordinateStartY - startY), TextFont, TextBrush, startX + Padding, startY + PaddingIn, this.Width, this.Height);
                }
                helpLinePen.Dispose();
            }
            //g.DrawLine(helpLinePen, new Point(startX, startY), new Point(startX - 100, startY));
        }

        private bool IsOutView(int startX, int startY)
        {
            if (startX < CoordinateStartX || startX > CoordinateStartX + CoordinateWidth)
            {
                return true;
            }

            if (startY > CoordinateStartY || startY < CoordinateStartY - CoordinateHeight)
            {
                return true;
            }

            return false;
        }





        private bool IsOverWidth(DataModel model)
        {
            int width = model.Area.right - model.Area.left;
            return this.Width > width;
        }

        /// <summary>
        /// 帮助线的颜色(默认蓝色)
        /// </summary>
        public Color HelpLineColor { get; set; }

        /// <summary>
        /// 坐标的宽度
        /// </summary>
        public int CoordinateWidth { get; set; }

        /// <summary>
        /// 坐标的高度
        /// </summary>
        public int CoordinateHeight { get; set; }

        /// <summary>
        /// 绘制虚线的每一条宽度(默认5)
        /// </summary>
        public float PeerHelpLineWidth { get; set; }

        /// <summary>
        /// 坐标点起始位置
        /// </summary>
        public int CoordinateStartX { get; set; }

        /// <summary>
        /// 坐标的起始位置
        /// </summary>
        public int CoordinateStartY { get; set; }

        /// <summary>
        /// 每条辅助线的间隔(默认5)
        /// </summary>
        public float CoordinatePeerDistance { get; set; }

        /// <summary>
        /// 辅助线的透明度
        /// </summary>
        public int HelpLineColorAlpha { get; set; }

        public bool IsDrawXYPoint { get; set; }
    }
}
