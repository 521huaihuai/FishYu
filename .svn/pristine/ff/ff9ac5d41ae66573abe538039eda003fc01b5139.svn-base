using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using ReportFormDesign.DataModels;
using ReportFormDesign.DrawUtils;
using ReportFormDesign.Model;

namespace ReportFormDesign.ReportViewPanel.SingleReportViews
{
    public class ArcSplite_SingleReportView : SingleReportView
    {
        private bool isFirst = true;
        private System.Drawing.Color oldColor;
        private int bottomLegendHeight;

        //圆点的大小
        private float CircleRaius = 10;

        public ArcSplite_SingleReportView()
        {
            _MyToolTip = new FollowPopViewForTitle();
            _MyToolTip.TextColor = ReportViewUtils.perferRed;
            _MyToolTip.DataColor = ReportViewUtils.perferRed;
            IsAutoReSizeView = true;
            IsHorizontally = true;
            MouseMoveInColor = ReportViewUtils.perferRed;
            //DataColor = LineColor
            //Dock = System.Windows.Forms.DockStyle.Fill;
            bottomLegendHeight = 20;
            IsDrawTextInLegendNote = true;
            isAutoResizeLegendNote = false;
        }

        public override void LegendNoteSizeChanged()
        {
            //ViewWidth = Width / 3 * 2;
            //ViewHeight = ViewWidth;
            //if (IsInCenter)
            //{
            //    StartX = this.Width / 2 - ViewWidth / 2;
            //    StartY = this.Height / 2 - ViewHeight / 2;
            //}
            TextSize = EViewWidth * 1.0f / 8;
            DataSize = EViewWidth * 1.0f / 12;
            bottomLegendHeight = EViewHeight / 5;
            CircleRaius = EViewHeight / 10;
            if (CircleRaius <= 1)
            {
                CircleRaius = 1;
            }
            if (bottomLegendHeight <= 1)
            {
                bottomLegendHeight = 1;
            }
            StrokenWidth = EViewHeight / 25;
            LegendNoteFontTextSize = DataSize;
            int left = EStartX;
            int top = EStartY + EViewHeight + (Height - EStartY - EViewHeight - bottomLegendHeight) / 2;
            LegendNoteModels[0].Area = new AreaPositionRect(left, top, left + EViewWidth, top + bottomLegendHeight);
        }

        /// <summary>
        /// 如果想要改变大小, 先设置IsEnable...为false, 在自己设置View的宽高
        /// </summary>
        public override void ResizeReportViewChange()
        {
        }

        public override void OnRenderNormalView(System.Drawing.Graphics g, Model.DataModel Data, System.Drawing.Pen linePen, System.Drawing.Brush lineBrush, System.Drawing.Brush TextBrush, System.Drawing.Brush DataBrush, System.Drawing.Font TextFont, System.Drawing.Font DataFont)
        {
            if (isFirst)
            {
                oldColor = linePen.Color;
                isFirst = false;
            }
            if (Data != null)
            {
                ///建议把绘制的直接写在这里
                if (Data.Area.IsMouseIn)
                {
                    linePen.Color = MouseMoveInColor;
                }
                else
                {
                    linePen.Color = oldColor;
                }
                DetailDataModel data = Data as DetailDataModel;
                
                System.Drawing.SolidBrush sbrush1 = new System.Drawing.SolidBrush(Color.FromArgb(150, 1, 77, 103));
                System.Drawing.SolidBrush sf = new System.Drawing.SolidBrush(linePen.Color);
                System.Drawing.Rectangle rect = new System.Drawing.Rectangle(EStartX, EStartY, EViewWidth, EViewHeight);
                float realAngle = 360 * int.Parse(data.TextAndData[1]) * 1.0f / MaxNum;
                //顺时针绘制
                float startAngle = 360 - realAngle;
                g.DrawArc(linePen, rect, startAngle, realAngle);
                //绘制圆弧内的字体
                ReportViewUtils.drawStringWithLimiteText(g, LocationModel.Location_Down, data.TextAndData[0], TextFont, TextBrush, EStartX, EStartY, EViewWidth, EViewHeight / 2, 5);
                //绘制圆弧内的数据
                ReportViewUtils.drawStringWithLimiteText(g, LocationModel.Location_Up, data.TextAndData[1] + "", DataFont, DataBrush, EStartX, EStartY + EViewHeight / 2, EViewWidth, EViewHeight / 2, 5);

                //绘制圆弧外的字体
                //Rectangle Arc_Out = new Rectangle(StartX, StartY + ViewHeight + bottomLegendHeight, ViewWidth, bottomLegendHeight);
                //GraphicsPath path = ReportViewUtils.CreateRoundedRectanglePath(Arc_Out, bottomLegendHeight / 2);
               // g.FillPath(sbrush1, path);
                //path.Dispose();
                //ReportViewUtils.drawStringWithLimiteText(g, LocationModel.Location_Center, data.TextAndData[2] + "" + data.TextAndData[3], DataFont, DataBrush, StartX, StartY + ViewHeight + bottomLegendHeight, ViewWidth, bottomLegendHeight, 8);


                //定位圆点
                float y = (float)(Math.Sin(startAngle / 180 * Math.PI) * EViewWidth / 2);
                float x = (float)(Math.Cos(startAngle / 180 * Math.PI) * EViewHeight / 2);
                RectangleF rect_Circle = new RectangleF((EStartX + EViewWidth / 2 - CircleRaius / 2 + x), (EStartY + EViewHeight / 2 - CircleRaius / 2) + y, CircleRaius, CircleRaius);
                //绘制圆点
                g.FillEllipse(sf, rect_Circle);

                sf.Dispose();
                sbrush1.Dispose();
            
            }
            
        }

        public override object[] AnimalionPrepare(Model.DetailDataModel drawModel)
        {
            return null;
        }

        public override void AnimalionDraw(System.Drawing.Graphics g, Model.DetailDataModel data, System.Drawing.Pen pen, System.Drawing.Brush brush, System.Drawing.Font font, object[] args)
        {
        }

        public override void AnimalionEnd(System.Drawing.Graphics g, Model.DetailDataModel data, System.Drawing.Pen pen, System.Drawing.Brush brush, System.Drawing.Font font)
        {
        }

        public override DataModels.LegendNoteModel[] GetLegendNotes()
        {
            LegendNoteModel[]  legendNoteModels = new LegendNoteModel[1];
            LegendNoteModel legendNoteModel = new LegendNoteModel();
            legendNoteModel.mainText = TextAndData[2] + " " + TextAndData[3];
            legendNoteModel.mainData = float.Parse(TextAndData[3]);
            legendNoteModel.Color = Color.FromArgb(150, 1, 77, 103);
            int left = EStartX;
            int top = EStartY + EViewHeight + (Height - EStartY - EViewHeight - bottomLegendHeight) / 2;
            legendNoteModel.Area = new AreaPositionRect(left, top, left + EViewWidth, top + bottomLegendHeight);
            legendNoteModels[0] = legendNoteModel;
            return legendNoteModels;
        }

        /// <summary>
        /// 鼠标移入的颜色
        /// </summary>
        public Color MouseMoveInColor { get; set; }

        /// <summary>
        /// 是否绘制注解图例(默认绘制)
        /// </summary>
        public bool IsDrawLegendNote
        {
            get
            {
                return isDrawLegendNote;
            }
            set
            {
                this.isDrawLegendNote = value;
            }
        }

        public bool IsDrawTextInLegendNote
        {
            get
            {
                return isDrawTextInLegendNote;
            }
            set
            {
                this.isDrawTextInLegendNote = value;
            }
        }
    }
}
