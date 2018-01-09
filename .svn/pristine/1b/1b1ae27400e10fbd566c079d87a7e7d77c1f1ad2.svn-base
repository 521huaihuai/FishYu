using System;
using System.Collections.Generic;
using System.Text;
using ReportFormDesign.DrawUtils;

namespace ReportFormDesign.ReportViewPanel
{
    public class SimpleTitlePanel : ViewPanel
    {
        public SimpleTitlePanel()
        {
            isSelfDefineReportView = true;
            TextSize = 10;
        }
        public override void DrawSelfDefineView(System.Drawing.Graphics g, System.Drawing.Pen linePen, System.Drawing.Brush lineBrush, System.Drawing.Brush TextBrush, System.Drawing.Brush DataBrush, System.Drawing.Font FontText, System.Drawing.Font FontData)
        {
            System.Drawing.Font textFont = new System.Drawing.Font("幼圆", TextSize, System.Drawing.FontStyle.Bold);
            //ReportViewUtils.drawString(g, Title, textFont, TextBrush, 0, 0, Width, Height);
            ReportViewUtils.drawString(g, locationType, Title, textFont, TextBrush, 0, 0, Width, Height);
            textFont.Dispose();
        }

        public override void childPaint(System.Drawing.Graphics g, Model.DataModel data, System.Drawing.Pen linePen, System.Drawing.Brush lineBrush, System.Drawing.Brush TextBrush, System.Drawing.Brush DataBrush, System.Drawing.Font FontText, System.Drawing.Font FontData)
        {
        }

        public string Title { get; set; }

        public LocationModel locationType = LocationModel.Location_Center;

        public LocationModel LocationType
        {
            get
            {
                return locationType;
            }
            set
            {
                locationType = value;
            }
        }
    }
}
