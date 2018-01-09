using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using ReportFormDesign.DrawUtils;

namespace ReportFormDesign.Model
{
    /// <summary>
    /// 绘制弹出窗口
    /// </summary>
    public class FollowPopView: ReportViewToolTip
    {

        public override void detailDraw(Graphics g, DataModel model, Brush TextBrush, Brush DataBrush, Brush BackGroundBrush, Font TextFont, Font DataFont)
        {
            if (model != null)
            {
                GraphicsPath path = null;
                if (IsOverWidth(model))
                {
                    string tip = "字数过长, 请双击大屏展示";
                    TextFont.Dispose();
                    TextFont = new Font("Consolas", 9, FontStyle.Bold);
                    ResizeWithAndHeight(g, tip, "", "", TextFont, DataFont);
                    Rectangle rect = new Rectangle(LocalPosition.X + Padding, LocalPosition.Y, Width, Height);
                    path = ReportViewUtils.CreateRoundedRectanglePath(rect, Height / 3);
                    //绘制底色
                    g.FillPath(BackGroundBrush, path);
                    TextBrush.Dispose();
                    TextBrush = new SolidBrush(ReportViewUtils.perferRed);
                    ReportViewUtils.drawString(g, LocationModel.Location_Center, tip, TextFont, TextBrush, LocalPosition.X + Padding, LocalPosition.Y, this.Width, this.Height);
                }
                else
                {
                    Rectangle rect = new Rectangle(LocalPosition.X + Padding, LocalPosition.Y, Width, Height);
                    path = ReportViewUtils.CreateRoundedRectanglePath(rect, Height / 3);
                    //绘制底色
                    g.FillPath(BackGroundBrush, path);
                    ReportViewUtils.drawString(g, LocationModel.Location_Up, model.mainText, TextFont, TextBrush, LocalPosition.X + Padding, LocalPosition.Y, this.Width, this.Height);
                    ReportViewUtils.drawString(g, LocationModel.Location_Down, model.mainData + "", TextFont, TextBrush, LocalPosition.X + Padding, LocalPosition.Y, this.Width, this.Height);
                }
                //int centerTextX = (this.Width - model.mainText.Length * TextSize) / 2;
                //int centerTextY = (this.Height / 2 - TextSize) / 2;
                //int centerDataX = (this.Width - model.mainData.ToString().Length * TextSize) / 2;
                //int centerDataY = (this.Height / 2 - DataSize) / 2 + this.Height / 2;
                ////绘制字体
                //g.DrawString(model.mainText, TextFont, TextBrush, LocalPosition.X + Padding + centerTextX, LocalPosition.Y + centerTextY);
                //g.DrawString(model.mainData + "", DataFont, DataBrush, LocalPosition.X + Padding + centerDataX, LocalPosition.Y + centerDataY);
                path.Dispose();
            }
        }

        private bool IsOverWidth(DataModel model)
        {
            int width = model.Area.right - model.Area.left;
            return this.Width > width;
        }
    }
}
