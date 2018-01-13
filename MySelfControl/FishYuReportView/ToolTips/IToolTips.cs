using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace FishyuSelfControl.FishYuReportView.ToolTips
{
    public interface IToolTips
    {
        /// <summary>
        /// 绘制ToolTip
        /// </summary>
        /// <param name="graphics">绘图</param>
        void RenderTips(Graphics graphics);
    }
}
