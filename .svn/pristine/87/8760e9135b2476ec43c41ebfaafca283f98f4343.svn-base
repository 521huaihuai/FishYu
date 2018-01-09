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
    public class FollowPopView : ReportViewToolTip
    {

        public override void detailDraw(Graphics g, DataModel model, Brush TextBrush, Brush DataBrush, Brush BackGroundBrush, Font TextFont, Font DataFont)
        {
            if (model != null)
            {
                Rectangle rect = new Rectangle(LocalPosition.X + Padding, LocalPosition.Y, Width, Height);
                GraphicsPath path = ReportViewUtils.CreateRoundedRectanglePath(rect, Height / 3);
                //绘制底色
                g.FillPath(BackGroundBrush, path);
                int centerTextX = (this.Width - model.mainText.Length * TextSize) / 2;
                int centerTextY = (this.Height / 2 - TextSize) / 2;
                int centerDataX = (this.Width - model.mainData.ToString().Length * TextSize) / 2;
                int centerDataY = (this.Height / 2 - DataSize) / 2 + this.Height / 2;
                //绘制字体
                g.DrawString(model.mainText, TextFont, TextBrush, LocalPosition.X + Padding + centerTextX, LocalPosition.Y + centerTextY);
                g.DrawString(model.mainData + "", DataFont, DataBrush, LocalPosition.X + Padding + centerDataX, LocalPosition.Y + centerDataY);
                path.Dispose();
            }
        }
    }
}
