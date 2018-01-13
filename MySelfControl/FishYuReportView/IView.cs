/* ======================================================================== 
* 本类功能概述:
* 
* 作者：zjm 
* 时间：2018/1/13 11:53:54 
* 文件名：IView 
* 版本：V1.0.1 
* 
* 修改者： 时间： 
* 修改说明： 
* ======================================================================== 
*/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace FishyuSelfControl.FishYuReportView
{
    public interface IView
    {
        /// <summary>
        /// 子类的绘制
        /// </summary>
        /// <param name="g">绘制</param>
        /// <param name="linePen"></param>
        /// <param name="lineBrush"></param>
        void ChildPaint(Graphics g, Pen pen, Brush brush);
    }
}
