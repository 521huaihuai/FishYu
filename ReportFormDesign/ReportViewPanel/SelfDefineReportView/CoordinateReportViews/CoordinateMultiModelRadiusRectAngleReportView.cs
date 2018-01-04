using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using ReportFormDesign.DrawUtils;
using ReportFormDesign.Model;
using ReportFormDesign.Models.DataModels;
using ReportFormDesign.ReportViewPanel.SelfDefineReportView.CoordinateReportViews;

namespace ReportFormDesign.ReportViewPanel.SelfDefineReportView.CoordinateMultiModelsReportViews
{
    public class CoordinateMultiModelRadiusRectAngleReportView : CoordinateReportView
    {
        private int indexer;

        public CoordinateMultiModelRadiusRectAngleReportView()
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

        public override void OnRenderNormalView(System.Drawing.Graphics g, Model.DataModel dataModel, System.Drawing.Pen linePen, System.Drawing.Brush lineBrush, System.Drawing.Brush TextBrush, System.Drawing.Brush DataBrush, System.Drawing.Font TextFont, System.Drawing.Font DataFont)
        {
            if (dataModel is CoordinateMultiDataModel)
            {
                CoordinateMultiDataModel datas = dataModel as CoordinateMultiDataModel;
                List<DataModel> dataList = datas.dataList;
                int i = 0;
                foreach (DataModel data in dataList)
                {
                    data.Area.left = dataModel.Area.left + i * dataModel.Area.Width / 2;
                    data.Area.right = data.Area.left + dataModel.Area.Width / 2 - MultiPadding;
                    data.Area.bottom = dataModel.Area.bottom;
                    data.Area.Width = data.Area.right - data.Area.left;

                    data.Area.Height = (int)(data.mainData / MaxNum * EViewHeight);
                    data.Area.top = dataModel.Area.top - data.Area.Height;
                    i++;
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
                            g.FillRectangle(bb, dataModel.Area.left, CoordinateStartY - CoordinateHeight, dataModel.Area.Width, CoordinateHeight);
                        }
                        else
                        {
                            g.FillRectangle(bs, item);
                        }
                    }

                    bb.Dispose();
                    bs.Dispose();
                }
            }
            
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            //base.OnSizeChanged(e);
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

        public int MultiPadding { get; set; }
    }
}
