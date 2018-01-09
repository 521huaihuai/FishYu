using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace ReportFormDesign.DrawUtils
{
    class DrawRoateText
    {

        /// <summary>  
        /// 绘制根据点旋转文本，一般旋转点给定位文本包围盒中心点  
        /// </summary>  
        /// <param name="s">文本</param>  
        /// <param name="font">字体</param>  
        /// <param name="brush">填充</param>  
        /// <param name="point">旋转点</param>  
        /// <param name="format">布局方式</param>  
        /// <param name="angle">角度</param>  
        public static void DrawString(Graphics _graphics, string s, Font font, Brush brush, PointF point, StringFormat format, float angle)
        {
            // Save the matrix  
            Matrix mtxSave = _graphics.Transform;

            Matrix mtxRotate = _graphics.Transform;
            mtxRotate.RotateAt(angle, point);
            _graphics.Transform = mtxRotate;

            _graphics.DrawString(s, font, brush, point, format);

            // Reset the matrix  
            _graphics.Transform = mtxSave;
        }  
    }
}
