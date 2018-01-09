using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using ReportFormDesign.DrawUtils;

namespace ReportFormDesign.ReportViewPanel.SelfDefineReportView.CoordinateReportViews
{
    public class CoordinateFillPathReportView : CoordinateReportView
    {

        public CoordinateFillPathReportView()
        {
            IsFillPathCurve = true;
        }

        public override void ResizePadding()
        {
            TopPadding = (int)(1.0f * EViewHeight / 5);
        }

        public override DrawUtils.AreaPositionRect ReLoadAreaPosition(int CenterX, int CenterY, int PaddingX, int PaddingY)
        {
            return new DrawUtils.AreaPositionRect(CenterX - PaddingX / 2, CenterY - PaddingY / 2, CenterX + PaddingX / 2, CenterY + PaddingY / 2);
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
            if (data.Area.IsMouseIn)
            {
                g.FillEllipse(lineBrush, item.X - 4, item.Y - 4, 8, 8);
            }
            else
            {
                g.FillEllipse(lineBrush, item.X - 2, item.Y - 2, 4, 4);
            }
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
