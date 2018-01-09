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
    public class FollowPopViewForTitle : ReportViewToolTip
    {

        public override void detailDraw(Graphics g, DataModel model, Brush TextBrush, Brush DataBrush, Brush BackGroundBrush, Font TextFont, Font DataFont)
        {
            if (model != null)
            {

                Rectangle rect = new Rectangle(LocalPosition.X + Padding, LocalPosition.Y, Width, Height);
                GraphicsPath path = ReportViewUtils.CreateRoundedRectanglePath(rect, Height / 3);

                //if (model is DetailDataModel)
                //{
                //    //绘制底色
                //    g.FillPath(BackGroundBrush, path);
                //    DetailDataModel data = (DetailDataModel)model;
                //    ReportViewUtils.drawString(g,data.mainText, TextFont, TextBrush, LocalPosition.X + Padding, LocalPosition.Y, this.Width, this.Height);
                //}
                //绘制底色
                g.FillPath(BackGroundBrush, path);
                ReportViewUtils.drawString(g, model.mainText, TextFont, TextBrush, LocalPosition.X + Padding, LocalPosition.Y, this.Width, this.Height);
                path.Dispose();
            }
        }
    }
}
