using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using ReportFormDesign.DrawUtils;
using ReportFormDesign.ReportViewPanel.SelfDefineReportView.CoordinateReportViews;

namespace ReportFormDesign.ReportViewPanel.SelfDefineReportView.CoordinateMultiModelsReportViews
{
    public class CoordinateMultiRadiusRectAngleReportView : CoordinateReportView
    {
        private int indexer;

        public CoordinateMultiRadiusRectAngleReportView()
        {
            IsDrawCurve = false;
            TextRotate = 0;
            //IsShowX_Scale = false;
            LableX = "";
            LableY = "";
            IsRectAngle = true;
            LableSize = 10;
            IsLableFontBold = true;
            isSelfDefineReportView = true;
            IsCoordinateReportView = true;
        }

        public override void ResizePadding()
        {
            TopPadding = (int)(1.0f * EViewHeight / 5);
        }

        public override DrawUtils.AreaPositionRect ReLoadAreaPosition(int CenterX, int CenterY, int PaddingX, int PaddingY)
        {
            indexer++;
            return new DrawUtils.AreaPositionRect(CenterX, CenterY, CenterX + PaddingX / 2, CoordinateStartY);
        }

        public override void OnRenderNormalView(System.Drawing.Graphics g, Model.DataModel data, System.Drawing.Pen linePen, System.Drawing.Brush lineBrush, System.Drawing.Brush TextBrush, System.Drawing.Brush DataBrush, System.Drawing.Font TextFont, System.Drawing.Font DataFont)
        {
            //绘制点
            Rectangle item = new Rectangle(data.Area.left, data.Area.top, data.Area.Width, data.Area.Height);
            if (IsDrawDetailData)
            {
                //ReportViewUtils.drawString(g, LocationModel.Location_Right_Right, CoordinateDataModelBean.Y_Data[i] + "", FontData, DataBrush, StartX, StartY, LeftPadding, padding);
                g.DrawString(data.mainData + "", DataFont, DataBrush, (item.X), item.Y - 2 * DataSize);
            }
            Brush bs = new SolidBrush(data.ModelColor);
            Brush bb = new SolidBrush(Color.FromArgb(100, data.ModelColor.R, data.ModelColor.G, data.ModelColor.B));
            if (IsRadiusRectAngle)
            {
                GraphicsPath path = ReportViewUtils.CreateRoundedRectanglePath(item, data.Area.Width / 4);
                
                if (data.Area.IsMouseIn)
                {
                    g.FillPath(bs, path);
                    g.FillRectangle(bb, data.Area.left, CoordinateStartY - CoordinateHeight, data.Area.Width, CoordinateHeight);
                }
                else
                {

                    g.FillPath(bs, path);
                }
                
                path.Dispose();
            }
            else
            {
                if (data.Area.IsMouseIn)
                {
                    g.FillRectangle(bs, item);
                    g.FillRectangle(bb, data.Area.left, CoordinateStartY - CoordinateHeight, data.Area.Width, CoordinateHeight);
                }
                else
                {
                    g.FillRectangle(bs, item);
                }
            }

            bb.Dispose();
            bs.Dispose();
        }

        public override void OnRenderAnimalionedView(System.Drawing.Graphics g, Model.DataModel data, System.Drawing.Pen linePen, System.Drawing.Brush lineBrush, System.Drawing.Brush TextBrush, System.Drawing.Brush DataBrush, System.Drawing.Font FontText, System.Drawing.Font FontData)
        {
        }

        public override object[] AnimalionPrepare()
        {
            //throw new NotImplementedException();
            return null;
        }

        public override void AnimalionDraw(System.Drawing.Graphics graphics, object[] AnimalionPrePareArgs)
        {
        }

        public override void AnimalionEnd(System.Drawing.Graphics graphics)
        {
        }


        /// <summary>
        /// 展示的图形是否是圆角矩形(默认false)
        /// </summary>
        public bool IsRadiusRectAngle { get; set; }
    }
}
