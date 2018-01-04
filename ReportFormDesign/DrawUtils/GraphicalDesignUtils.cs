using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace ReportFormDesign.DrawUtils
{
    public class GraphicalDesignUtils
    {

        /// <summary>
        /// 绘制指针
        /// </summary>
        public static GraphicsPath CreatePath(Point point0, Point point1, Point point2, Point point3)
        {
            GraphicsPath roundedRect = new GraphicsPath();
            roundedRect.AddLine(point0.X, point0.Y, point1.X, point1.Y);
            roundedRect.AddLine(point1.X, point1.Y, point3.X, point3.Y);
            roundedRect.AddLine(point3.X, point3.Y, point2.X, point2.Y);
            roundedRect.AddLine(point2.X, point2.Y, point0.X, point0.Y);
            roundedRect.CloseFigure();
            return roundedRect;
        }

        /// <summary>
        /// 绘制箭头
        /// </summary>
        public static GraphicsPath CreateArrowViewPath(Rectangle rect, int ArrowHeadHeight, float ArrowBodyWidth)
        {
            if (rect.Height <= 1)
            {
                rect.Height = 1;
            }
            if (rect.Width <= 1)
            {
                rect.Width = 1;
            }
            GraphicsPath roundedRect = new GraphicsPath();
            roundedRect.AddLine(rect.X, rect.Y + ArrowHeadHeight, rect.X + rect.Width / 2, rect.Y);
            roundedRect.AddLine(rect.X + rect.Width / 2, rect.Y, rect.X + rect.Width, rect.Y + ArrowHeadHeight);
            roundedRect.AddLine(rect.X + rect.Width, rect.Y + ArrowHeadHeight, rect.X + ArrowBodyWidth + (rect.Width - ArrowBodyWidth) / 2, rect.Y + ArrowHeadHeight);
            roundedRect.AddLine(rect.X + ArrowBodyWidth + (rect.Width - ArrowBodyWidth) / 2, rect.Y + ArrowHeadHeight, rect.X + ArrowBodyWidth + (rect.Width - ArrowBodyWidth) / 2, rect.Y + rect.Height);
            roundedRect.AddLine(rect.X + ArrowBodyWidth + (rect.Width - ArrowBodyWidth) / 2, rect.Y + rect.Height, rect.X + (rect.Width - ArrowBodyWidth) / 2, rect.Y + rect.Height);
            roundedRect.AddLine(rect.X + (rect.Width - ArrowBodyWidth) / 2, rect.Y + rect.Height, rect.X + (rect.Width - ArrowBodyWidth) / 2, rect.Y + ArrowHeadHeight);
            roundedRect.AddLine(rect.X + (rect.Width - ArrowBodyWidth) / 2, rect.Y + ArrowHeadHeight, rect.X, rect.Y + ArrowHeadHeight);
            roundedRect.CloseFigure();
            return roundedRect;
        }
    }
}
