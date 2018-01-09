using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using ReportFormDesign.ReportViewPanel.SelfDefineReportView.CoordinateReportViews;

namespace ReportFormDesign.ReportViewPanel.SelfDefineReportView.CoordinateMultiModelsReportViews
{
    public class CoordinateMultiFillPathReportView : CoordinateMultiOverlapModelsReportView
    {

        public CoordinateMultiFillPathReportView()
        {
            IsFillPathCurve = true;
            //animalion.IsNeverShowAnimalion = true;
        }

        public override void ResizePadding()
        {
            TopPadding = (int)(1.0f * EViewHeight / 5);
            //RightPadding = (Width - ViewWidth) / 2;
        }

        public override DrawUtils.AreaPositionRect ReLoadAreaPosition(int CenterX, int CenterY, int PaddingX, int PaddingY)
        {
            return new DrawUtils.AreaPositionRect(CenterX - PaddingX / 4, CenterY, CenterX + PaddingX / 4, CenterY);
        }

        public override void OnRenderNormalView(System.Drawing.Graphics g, Model.DataModel data, System.Drawing.Pen linePen, System.Drawing.Brush lineBrush, System.Drawing.Brush TextBrush, System.Drawing.Brush DataBrush, System.Drawing.Font TextFont, System.Drawing.Font DataFont)
        {
            //绘制点
            Point item = new Point(data.Area.left + data.Area.Width / 2, data.Area.top + data.Area.Height / 2);
            if (IsDrawDetailData)
            {
                //ReportViewUtils.drawString(g, LocationModel.Location_Right_Right, CoordinateDataModelBean.Y_Data[i] + "", FontData, DataBrush, StartX, StartY, LeftPadding, padding);
                g.DrawString(data.mainData + "", DataFont, DataBrush, (item.X), item.Y - 2 * DataSize);
            }
            Brush bs = new SolidBrush(data.ModelColor);
            if (data.Area.IsMouseIn)
            {
                g.FillEllipse(bs, item.X - 4, item.Y - 4, 8, 8);
            }
            else
            {
                g.FillEllipse(bs, item.X - 2, item.Y - 2, 4, 4);
            }
            bs.Dispose();
        }

        public override void OnRenderAnimalionedView(System.Drawing.Graphics g, Model.DataModel data, System.Drawing.Pen linePen, System.Drawing.Brush lineBrush, System.Drawing.Brush TextBrush, System.Drawing.Brush DataBrush, System.Drawing.Font FontText, System.Drawing.Font FontData)
        {
        }

        public override object[] AnimalionPrepare()
        {
            return null;
        }

        public override void AnimalionDraw(System.Drawing.Graphics graphics, object[] AnimalionPrePareArgs)
        {
        }

        public override void AnimalionEnd(System.Drawing.Graphics graphics)
        {
        }
    }
}
