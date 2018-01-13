using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace FinshYuUtils.DrawUtils
{
    /// <summary>
    /// 绘制字体的位置信息
    /// </summary>
    public enum LocationModel
    {
        Location_Center = 0,
        Location_Up = -1,
        Location_Up_Up = -2,
        Location_Down = 1,
        Location_Down_Down = 2,
        Location_Left = -3,
        Location_Right = 3,
        Location_Left_Left = -4,
        Location_Right_Right = 4,
        Location_Up_Right = 5,
        Location_Down_Right,
        Location_Up_Right_Right,
        Location_Down_Right_Right,
        Location_Down_Down_Right_Right,
    }

    public class DrawUtil
    {

        /// <summary>
        /// 创建圆角矩形绘制路径
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="cornerRadius">角度</param>
        /// <returns></returns>
        public static GraphicsPath CreateRoundedRectanglePath(Rectangle rect, int cornerRadius)
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
            roundedRect.AddArc(rect.X, rect.Y, cornerRadius * 2, cornerRadius * 2, 180, 90);
            roundedRect.AddLine(rect.X + cornerRadius, rect.Y, rect.Right - cornerRadius * 2, rect.Y);
            roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y, cornerRadius * 2, cornerRadius * 2, 270, 90);
            roundedRect.AddLine(rect.Right, rect.Y + cornerRadius * 2, rect.Right, rect.Y + rect.Height - cornerRadius * 2);
            roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y + rect.Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0, 90);
            roundedRect.AddLine(rect.Right - cornerRadius * 2, rect.Bottom, rect.X + cornerRadius * 2, rect.Bottom);
            roundedRect.AddArc(rect.X, rect.Bottom - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90, 90);
            roundedRect.AddLine(rect.X, rect.Bottom - cornerRadius * 2, rect.X, rect.Y + cornerRadius * 2);
            roundedRect.CloseFigure();
            return roundedRect;
        }


        /// <summary>
        /// 居中绘制字体
        /// </summary>
        public static void DrawString(Graphics g, string mainText, Font TextFont, Brush TextBrush, float startX, float startY, float width, float height)
        {
            DrawString(g, LocationModel.Location_Center, mainText, TextFont, TextBrush, startX, startY, width, height);
        }

        public static void DrawString(Graphics g, LocationModel locationModel,  string mainText, Font TextFont, Brush TextBrush, Rectangle rectangle)
        {
            DrawString(g, locationModel, mainText, TextFont, TextBrush, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
        }

        /// <summary>
        /// 绘制字体
        /// </summary>
        /// <param name="g"></param>
        /// <param name="model">绘制字体位置选择</param>
        public static void DrawString(Graphics g, LocationModel model, string mainText, Font TextFont, Brush TextBrush, float startX, float startY, float width, float height)
        {
            SizeF size = g.MeasureString(mainText, TextFont);
            float centerTextX = 0.0f;
            float centerTextY = 0.0f;
            switch (model)
            {
                case LocationModel.Location_Center:
                    centerTextX = (width - size.Width) / 2;
                    centerTextY = (height - size.Height) / 2;
                    break;
                case LocationModel.Location_Up:
                    centerTextX = (width - size.Width) / 2;
                    centerTextY = (height / 2 - size.Height) / 2;
                    break;
                case LocationModel.Location_Up_Up:
                    centerTextX = (width - size.Width) / 2;
                    centerTextY = 0;
                    break;
                case LocationModel.Location_Down:
                    centerTextX = (width - size.Width) / 2;
                    centerTextY = (height / 2 - size.Height) / 2 + height / 2;
                    break;
                case LocationModel.Location_Down_Down:
                    centerTextX = (width - size.Width) / 2;
                    centerTextY = height - size.Height;
                    break;
                case LocationModel.Location_Left:
                    centerTextX = (width / 2 - size.Width) / 2;
                    centerTextY = (height - size.Height) / 2;
                    break;
                case LocationModel.Location_Right:
                    centerTextX = (width / 2 - size.Width) / 2 + width / 2;
                    centerTextY = (height - size.Height) / 2;
                    break;
                case LocationModel.Location_Left_Left:
                    centerTextX = 0;
                    centerTextY = (height - size.Height) / 2;
                    break;
                case LocationModel.Location_Right_Right:
                    centerTextX = width - size.Width;
                    centerTextY = (height - size.Height) / 2;
                    break;
                case LocationModel.Location_Up_Right:
                    centerTextX = (width / 2 - size.Width) / 2 + width / 2;
                    centerTextY = (height / 2 - size.Height) / 2;
                    break;
                case LocationModel.Location_Down_Right:
                    centerTextX = (width / 2 - size.Width) / 2 + width / 2;
                    centerTextY = (height / 2 - size.Height) / 2 + height / 2;
                    break;
                case LocationModel.Location_Up_Right_Right:
                    centerTextX = width - size.Width;
                    centerTextY = (height / 2 - size.Height) / 2;
                    break;
                case LocationModel.Location_Down_Right_Right:
                    centerTextX = width - size.Width;
                    centerTextY = (height / 2 - size.Height) / 2 + height / 2;
                    break;
                case LocationModel.Location_Down_Down_Right_Right:
                    centerTextX = width - size.Width;
                    centerTextY = height - size.Height;
                    break;
                default:
                    break;
            }
            //绘制字体
            g.DrawString(mainText, TextFont, TextBrush, startX + centerTextX, startY + centerTextY);
        }
    }
}
