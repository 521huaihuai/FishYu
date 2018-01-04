using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using ReportFormDesign.CurrentPosition;
using ReportFormDesign.Model;
using ReportFormDesign.DrawUtils;

namespace ReportFormDesign.ReportViewPanel
{
    /// <summary>
    /// 圆形分割报表
    /// </summary>
    class Circle_Splite_ReportView : ViewPanel
    {

        public Circle_Splite_ReportView()
        {

        }

        public override void childPaint(Graphics g, DataModel Data, Pen linePen, Brush lineBrush, Brush TextBrush, Brush DataBrush, System.Drawing.Font font_Text, System.Drawing.Font font_Data)
        {
            
        }

        public override void introducePaint(Graphics g, DataModel rectPosData, System.Drawing.Color GraphicalColor, System.Drawing.Color TextColor, float TextSize)
        {
        }
    }
}
