using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using ReportFormDesign.DrawUtils;

namespace ReportFormDesign.ReportViewPanel
{
    public class SimpleTestPanel : ViewPanel
    {
        public SimpleTestPanel()
        {
            isSelfDefineReportView = true;
        }

        public override void DrawSelfDefineView(System.Drawing.Graphics g, System.Drawing.Pen linePen, System.Drawing.Brush lineBrush, System.Drawing.Brush TextBrush, System.Drawing.Brush DataBrush, System.Drawing.Font FontText, System.Drawing.Font FontData)
        {
            System.Drawing.Drawing2D.GraphicsPath path = GraphicalDesignUtils.CreateArrowViewPath(new Rectangle(0, 0, Width, Height), Height / 4, Width / 4);
            g.FillPath(lineBrush, path);
        }

        public override void childPaint(System.Drawing.Graphics g, Model.DataModel data, System.Drawing.Pen linePen, System.Drawing.Brush lineBrush, System.Drawing.Brush TextBrush, System.Drawing.Brush DataBrush, System.Drawing.Font FontText, System.Drawing.Font FontData)
        {
        }
    }
}
