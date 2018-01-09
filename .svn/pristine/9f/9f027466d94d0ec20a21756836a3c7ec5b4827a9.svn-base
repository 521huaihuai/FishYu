using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Threading;
using ReportFormDesign.DataModels;
using ReportFormDesign.DrawUtils;
using ReportFormDesign.Model;

namespace ReportFormDesign.ReportViewPanel
{
    public class SpeedOfProgress_SingleReportView : SingleReportView
    {
        private float linepenWidth;

        public SpeedOfProgress_SingleReportView()
        {
            //_MyToolTip = new FollowPopViewForTitle();
            //_MyToolTip.TextColor = ReportViewUtils.perferRed;
            //_MyToolTip.DataColor = ReportViewUtils.perferRed;
            _Interpolation.Value = 2;
            ScaleLength = 15;
            OutsidePointerDistanceArc = 15;
            PointWidth = 4;
            //默认
            autoUpdateTime = 60;
            isDrawLegendNote = false;
            MaxIndex = 100;
        }

        public override void OnRenderNormalView(Graphics g, DataModel data, Pen linePen, Brush lineBrush, Brush TextBrush, Brush DataBrush, Font FontText, Font FontData)
        {
            if (!data.IsSpecialAreaDataModel)
            {
                float dd = drawFrame(g, linePen, TextBrush, FontText);
                if (_MyAnimalion != null && !_MyAnimalion.IsPrepareAnimaled)
                {
                    string str = (int)(dd * 100) + "%";
                    if (int.Parse(TextAndData[1]) >= MaxNum)
                    {
                        str = "任务完成!";
                    }
                    ReportViewUtils.drawString(g, LocationModel.Location_Up_Up, str, FontData, DataBrush, EStartX, EStartY + EViewHeight - 3 * TextSize, EViewWidth, Height - EStartY - EViewHeight);
                }
                //绘制指针//坐标定位
                Point p0;
                Point p1;
                Point p2;
                Point p3;
                LocationPointer(dd, out p0, out p1, out p2, out p3);
                GraphicsPath path = GraphicalDesignUtils.CreatePath(p0, p1, p2, p3);
                g.FillPath(lineBrush, path);
                path.Dispose();
            }
        }

        private float drawFrame(Graphics g, Pen linePen, Brush TextBrush, Font FontText)
        {
            Rectangle rect = new Rectangle(EStartX, EStartY, EViewWidth, EViewHeight);
            linePen.Width = linepenWidth;
            //顺时针绘制, 绘制弧形
            linePen.Color = Color.FromArgb(255, 197, 129, 123);
            g.DrawArc(linePen, rect, 140, 40 + 1);
            linePen.Color = ReportViewUtils.perferBlue;
            g.DrawArc(linePen, rect, 180, 180);
            linePen.Color = Color.FromArgb(255, 66, 190, 189);
            g.DrawArc(linePen, rect, 0 - 1, 41);
            //绘制刻度(定制绘制)
            linePen.Width = 0.1f;
            for (int i = 0; i < 14; i++)
            {
                switch (i)
                {
                    case 0:
                        linePen.Color = Color.FromArgb(255, 197, 129, 123);
                        break;
                    case 3:
                        linePen.Color = ReportViewUtils.perferBlue;
                        break;
                    case 8:
                        linePen.Color = Color.FromArgb(255, 66, 190, 189);
                        break;
                    default:
                        break;
                }
                Point point1 = LocationPointFromAngle(140 + 20 * i, EViewWidth / 2, EViewWidth, EStartX, EStartY);
                Point point2 = LocationPointFromAngle(140 + 20 * i, (EViewWidth - ScaleLength) / 2, EViewWidth, EStartX, EStartY);
                g.DrawLine(linePen, point1, point2);
            }
            //绘制名称
            float dd = int.Parse(TextAndData[1]) * 1.0f / MaxNum;
            //ReportViewUtils.drawString(g, data.mainText, font_Text, TextBrush, StartX, StartY + ViewHeight, ViewWidth, ViewHeight / 2);
            ReportViewUtils.drawString(g, TextAndData[0], FontText, TextBrush, EStartX, EStartY + EViewHeight, EViewWidth, Height - EStartY - EViewHeight);
            return dd;
        }

        #region 动画绘制
        public override object[] AnimalionPrepare(DetailDataModel drawModel)
        {
            return null;
        }

        public override void AnimalionDraw(System.Drawing.Graphics graphics, DetailDataModel data, System.Drawing.Pen pen, System.Drawing.Brush brush, System.Drawing.Font font, object[] args)
        {
            drawFrame(graphics, pen, brush, font);
            if (TextAndData != null)
            {
                Point p0;
                Point p1;
                Point p2;
                Point p3;
                string str = Index / MaxIndex * 100 + "%";
                if (Index / MaxIndex >= ((int.Parse(TextAndData[1]) * 1.0f) / MaxNum))
                {
                    //Index = int.Parse(TextAndData[1]);
                    str = (int)((int.Parse(TextAndData[1]) * 1.0f) / MaxNum * 100) + "%";
                    //完成任务
                    if (int.Parse(TextAndData[1]) >= MaxNum)
                    {
                        str = "任务完成!";
                        isAutoUpdateData = false;
                    }
                    LocationPointer((int.Parse(TextAndData[1]) * 1.0f) / MaxNum, out p0, out p1, out p2, out p3);
                }
                else
                {
                    LocationPointer(Index / MaxIndex, out p0, out p1, out p2, out p3);
                }
                ReportViewUtils.drawString(graphics, LocationModel.Location_Up_Up, str, font, brush, EStartX, EStartY + EViewHeight - 2 * TextSize, EViewWidth, EViewHeight / 2);

                GraphicsPath path = GraphicalDesignUtils.CreatePath(p0, p1, p2, p3);
                graphics.FillPath(brush, path);
                path.Dispose();
            }

        }

        public override void AnimalionEnd(System.Drawing.Graphics graphics, DetailDataModel data, System.Drawing.Pen pen, System.Drawing.Brush brush, System.Drawing.Font font)
        {
        }
        #endregion

        #region SizeChange
        public override void LegendNoteSizeChanged()
        {

        }

        public override void ResizeReportViewChange()
        {
            OutsidePointerDistanceArc = EViewWidth / 8;
            PointWidth = EViewWidth * 1.0f / 40;
            linepenWidth = PointWidth;
            ScaleLength = 4 * linepenWidth;
            DataSize = EViewWidth * 1.0f / 8;
        }
        #endregion

        public override LegendNoteModel[] GetLegendNotes()
        {
            return null;
        }

        private Point LocationPointFromAngle(float startAngle, float radius, int width, int startX, int startY)
        {
            int y = (int)(Math.Sin(startAngle / 180 * Math.PI) * radius);
            int x = (int)(Math.Cos(startAngle / 180 * Math.PI) * radius);
            return new Point(x + startX + width / 2, y + startY + width / 2);
        }

        private void LocationPointer(double dd, out Point p0, out Point p1, out Point p2, out Point p3)
        {
            double allPi = 260 * 2 * Math.PI / 360;
            double realAngle = 2 * Math.PI - dd * allPi - 140 * Math.PI / 180;
            int centerX = EStartX + EViewWidth / 2;
            int centerY = EStartY + EViewHeight / 2;
            p0 = new Point(centerX, centerY);
            double dy = (Math.Sin(realAngle));
            double dx = (Math.Cos(realAngle));

            //定位指针左右两端
            double centerX2 = centerX + PointWidth * dx;
            double centerY2 = centerY - PointWidth * dy;
            double angle = Math.Atan(dx / dy);
            p1 = new Point((int)(centerX2 - PointWidth * Math.Cos(angle)), (int)(-PointWidth * Math.Sin(angle) + centerY2));
            p2 = new Point((int)(centerX2 + PointWidth * Math.Cos(angle)), (int)(PointWidth * Math.Sin(angle) + centerY2));
            //定位指针末端
            p3 = new Point((int)((EViewWidth / 2 - OutsidePointerDistanceArc) * dx) + centerX, -(int)(dy * (EViewWidth / 2 - OutsidePointerDistanceArc)) + centerY);
        }

        /// <summary>
        /// 绘制刻度的长度
        /// </summary>
        public float ScaleLength { get; set; }

        /// <summary>
        /// 指针顶端距离圆盘的距离
        /// </summary>
        public int OutsidePointerDistanceArc { get; set; }

        /// <summary>
        /// 指针宽度
        /// </summary>
        public float PointWidth { get; set; }
    }

}
