using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using ReportFormDesign.DrawUtils;
using ReportFormDesign.Model;
using ReportFormDesign.ReportViewPanel.SelfDefineReportView.CoordinateMultiModelsReportViews;

namespace ReportFormDesign.ToolTips
{
    public class CoordinateMultiToolTip : ReportViewToolTip
    {
        public CoordinateMultiToolTip()
        {
            Padding = 10;
            PaddingIn = 20;
            TextColor = ReportViewUtils.perferBlue;
        }

        public override void OnRender(Graphics g, DataModel model)
        {
            if (IsVisible)
            {
                Brush BackGroundBrush = new SolidBrush(Color.FromArgb(100, 210, 210, 210));
                Brush TextBrush = new SolidBrush(TextColor);
                Brush DataBrush = new SolidBrush(DataColor);
                Font Text_Font = new Font("Consolas", TextSize, FontStyle.Bold);
                Font Data_Font = new Font("Consolas", DataSize, FontStyle.Bold);
                if (model != null)
                {
                    ResizeWithAndHeight(g, model.mainText, model.mainData + "", "", Text_Font, Data_Font);
                    if (!string.IsNullOrEmpty(model.mainText))
                    {
                        Text = model.mainText;
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
                IsDrawBackGround = true;
                if (IsDrawBackGround)
                {
                    Rectangle rect = new Rectangle(startX + Padding, startY + PaddingIn / 2, Width - PaddingIn, Height);
                    GraphicsPath path = ReportViewUtils.CreateRoundedRectanglePath(rect, Height / 6);
                    //绘制底色
                    g.FillPath(BackGroundBrush, path);
                    path.Dispose();
                }

                TextBrush = new SolidBrush(Color.White);
                DrawDataString(g, model, startX, startY, TextBrush, TextFont);
            }
        }

        private void DrawDataString(System.Drawing.Graphics g, DataModel model, int startX, int startY, Brush TextBrush, Font TextFont)
        {
            for (int i = 0; i < CoordinateMultiGroupRadiusRectAngleReportView.ALLGroup.Count; i++)
            {
                foreach (DataModel item in CoordinateMultiGroupRadiusRectAngleReportView.ALLGroup[i])
                {
                    if (item.Area.left == model.Area.left)
                    {
                        for (int j = 0; j < CoordinateMultiGroupRadiusRectAngleReportView.ALLGroup[i].Count; j++)
                        {
                            ReportViewUtils.drawString(g, CoordinateMultiGroupRadiusRectAngleReportView.ALLGroup[i][j].mainData + "", TextFont, TextBrush, startX, startY, this.Width, this.Height);
                            startY += PaddingIn;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }

        #region 属性
        /// <summary>
        /// 坐标的宽度
        /// </summary>
        public int CoordinateWidth { get; set; }

        /// <summary>
        /// 坐标的高度
        /// </summary>
        public int CoordinateHeight { get; set; }

        /// <summary>
        /// 坐标点起始位置
        /// </summary>
        public int CoordinateStartX { get; set; }

        /// <summary>
        /// 坐标的起始位置
        /// </summary>
        public int CoordinateStartY { get; set; }

        #endregion

    }
}
