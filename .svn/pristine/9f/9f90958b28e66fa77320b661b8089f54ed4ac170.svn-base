using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Threading;
using ReportFormDesign.DataModels;
using ReportFormDesign.DrawUtils;
using ReportFormDesign.Model;
using ReportFormDesign.Models;

namespace ReportFormDesign.ReportViewPanel.SingleReportViews
{
    public class MultiArc_Splite_SingleReportView : SingleReportView
    {

        /// <summary>
        /// 起始角度设置(逆时针)
        /// </summary>
        public int StartAngle { get; set; }
        private List<MultiArcSpliteInfoData> DataInfoList = new List<MultiArcSpliteInfoData>();

        public MultiArc_Splite_SingleReportView()
        {
            _MyToolTip = new FollowPopViewForTitle();
            _MyToolTip.TextColor = ReportViewUtils.perferRed;
            _MyToolTip.DataColor = ReportViewUtils.perferRed;
            StartAngle = 0;
            IsDescendingOrder = true;
            isAutoUpdateData = false;
            _Interpolation.Value = 3;
            autoUpdateTime = 6 * 60;
            isAutoResizeLegendNote = false;
            LegendNoteLeftPadding = 20;
        }


        public override void LegendNoteSizeChanged()
        {
            if (isDrawLegendNote && TextAndData != null)
            {
                LegendNoteUpDownPadding = EViewWidth / 20;
                LegendNoteWidth = EViewWidth / 8;
                LegendNoteHeight = EViewHeight / 2 / (TextAndData.Length / 2) - LegendNoteUpDownPadding;
            }
            //对注解重新缩放
            int centerY = EViewHeight / 4 + EStartY;
            int LegendNotestartY = centerY - (LegendNoteHeight + LegendNoteUpDownPadding) * LegendNoteModels.Length / 2;
            LegendNoteModel item = null;
            for (int i = 0; i < LegendNoteModels.Length; i++)
            {
                item = LegendNoteModels[i];
                int left = LegendNoteLeftPadding + EStartX + EViewWidth;
                int top = LegendNotestartY + i * (LegendNoteHeight + LegendNoteUpDownPadding);
                item.Area = new AreaPositionRect(left, top, left + LegendNoteWidth, top + LegendNoteHeight);
            }

            LegendNoteFontTextSize = EViewWidth * 1.0f / 24;
        }

        public override void ResizeReportViewChange()
        {
            StrokenWidth = EViewWidth * 1.0f / 15;
            LastLandingX = (int)(EStartX / 1.5);
            AnimalionTextSize = EViewWidth * 1.0f / 20;
            //MaxIndex = EViewWidth / 2;
        }

        public override void OnRenderNormalView(System.Drawing.Graphics g, Model.DataModel data, System.Drawing.Pen linePen, System.Drawing.Brush lineBrush, System.Drawing.Brush TextBrush, System.Drawing.Brush DataBrush, System.Drawing.Font FontText, System.Drawing.Font FontData)
        {
            if (_MyAnimalion.AnimalionUsedTime > _MyAnimalion.AnimalionTime / 3)
            {
                ReportViewUtils.drawStringWithLimiteText(g, LocationModel.Location_Center, data.mainText, FontText, TextBrush, EStartX, EStartY, EViewWidth, EViewHeight, 20);
            }

            if (!data.IsSpecialAreaDataModel && !_MyAnimalion.IsPrepareAnimaled)
            {
                DetailDataModel Data = data as DetailDataModel;
                string[] datas = Data.TextAndData;

                int count = datas.Length / 2;
                float x, y;
                //绘制的中心点
                int centerX = EStartX + EViewWidth / 2;
                int centerY = EStartY + EViewHeight / 2;
                Pen lines = new Pen(lineBrush);
                lines.Width = 1.0f;
                float perHeightPadding = (Height - count * (g.MeasureString("2", FontData).Height)) / (count + 1);
                float newY = 0;
                DataInfoList.Clear();
                for (int i = 0; i < count; i++)
                {
                    //半径
                    float radius = (EViewWidth - 2 * i * StrokenWidth) / 2;
                    RectangleF rectf = new RectangleF((EStartX + i * StrokenWidth), (EStartY + i * StrokenWidth), (radius * 2), (radius * 2));
                    float realAngle = 0;
                    if (IsCalculateDependByMaxNum)
                    {
                        realAngle = 240 * int.Parse(datas[i * 2 + 1]) * 1.0f / MaxNum;
                    }
                    else
                    {
                        realAngle = 360 * int.Parse(datas[i * 2 + 1]) * 1.0f / allNum;
                    }
                    ////顺时针绘制
                    linePen.Color = ReportViewUtils.PerferColors[i % ReportViewUtils.PerferColors.Length];
                    lines.Color = linePen.Color;
                    float angle = 360 - StartAngle - realAngle;
                    try
                    {
                        g.DrawArc(linePen, rectf, angle, realAngle);
                    }
                    catch (Exception)
                    {
                    }
                    MultiArcSpliteInfoData dataInfo = new MultiArcSpliteInfoData();
                    ///绘制直线备注
                    x = (float)(centerX + radius * Math.Cos(2 * Math.PI * angle / 360));
                    y = (float)(centerY + radius * Math.Sin(2 * Math.PI * angle / 360));
                    
                    dataInfo.fitstPoint = new PointF(x, y);
                    dataInfo.secondPoint = new PointF(EStartX * 3 / 4, y);
                    dataInfo.text = TextAndData[2 * i + 1];
                    dataInfo.sortY = y;
                    dataInfo.lineColor = lines.Color;
                    DataInfoList.Add(dataInfo);
                }
                //进行排序
                DataInfoList.Sort();
                int ii = 1;
                foreach (var item in DataInfoList)
                {
                    SizeF sf = g.MeasureString(item.text, FontData);
                    newY = ii * (perHeightPadding) + (ii - 1) * sf.Height + sf.Height / 2;
                    ii++;
                    lines.Color = item.lineColor;
                    g.DrawLine(lines, item.fitstPoint, item.secondPoint);
                    g.DrawLine(lines, item.secondPoint, new PointF(LastLandingX, newY));
                    Rectangle rect = new Rectangle((int)(LastLandingX - 20), (int)newY, (int)sf.Width + padding, (int)sf.Height + padding);
                    GraphicsPath path = ReportViewUtils.CreateRoundedRectanglePath(rect, 13);
                    //绘制底色
                    g.FillPath(new SolidBrush(item.lineColor), path);
                    path.Dispose();
                    ReportViewUtils.drawString(g, LocationModel.Location_Center, item.text, FontData, TextBrush, rect.Left, rect.Top, rect.Width, rect.Height);
                }
                lines.Dispose();
                ReportViewUtils.drawStringWithLimiteText(g, LocationModel.Location_Center, data.mainText, FontText, TextBrush, EStartX, EStartY, EViewWidth, EViewHeight, 10);
            }
            else
            {

            }

        }

        public override object[] AnimalionPrepare(DetailDataModel drawModel)
        {
            return null;
        }

        public override void AnimalionDraw(System.Drawing.Graphics g, DetailDataModel data, System.Drawing.Pen pen, System.Drawing.Brush brush, System.Drawing.Font font, object[] args)
        {
            //Console.WriteLine("index = {0}", Index);
            if (_MyAnimalion.AnimalionUsedTime < _MyAnimalion.AnimalionTime / 3)
            {
                ReportViewUtils.drawStringWithLimiteText(graphics, LocationModel.Location_Center, "正在重新获取数据ing...", font, brush, EStartX, EStartY, EViewWidth, EViewHeight, 20);
            }
            //else
            //{
            //    ReportViewUtils.drawStringWithLimiteText(graphics, LocationModel.Location_Center, data.mainText, font, brush, StartX, StartY, ViewWidth, ViewHeight, 20);
            //}
            int count = TextAndData.Length / 2;

            float realStartAngle = 0;
            float x, y;
            //绘制的中心点
            int centerX = EStartX + EViewWidth / 2;
            int centerY = EStartY + EViewHeight / 2;
            ///绘制弧形
            for (int i = 0; i < count; i++)
            {
                //半径
                float radius = (EViewWidth - 2 * i * StrokenWidth) / 2;
                RectangleF rectf = new RectangleF((EStartX + i * StrokenWidth), (EStartY + i * StrokenWidth), (radius * 2), (radius * 2));
                float realAngle = 0;
                if (IsCalculateDependByMaxNum)
                {
                    realAngle = 240 * int.Parse(TextAndData[i * 2 + 1]) * 1.0f / MaxNum;
                }
                else
                {
                    realAngle = 360 * int.Parse(TextAndData[i * 2 + 1]) * 1.0f / allNum;
                }

                ////顺时针绘制
                pen.Color = ReportViewUtils.PerferColors[i % ReportViewUtils.PerferColors.Length];
                pen.Width = StrokenWidth;
                try
                {
                    float currentIndex = Index;
                    if (Index > realAngle)
                    {
                        currentIndex = realAngle;
                    }
                    realStartAngle = 360 - StartAngle - currentIndex;
                    g.DrawArc(pen, rectf, realStartAngle, currentIndex);
                }
                catch (Exception)
                {
                }
                ///绘制直线备注
                x = (float)(centerX + radius * Math.Cos(2 * Math.PI * realStartAngle / 360));
                y = (float)(centerY + radius * Math.Sin(2 * Math.PI * realStartAngle / 360));
                pen.Width = 1.0f;
                if (x - Index < LastLandingX)
                {
                    g.DrawLine(pen, new PointF(x, y), new PointF(LastLandingX, y));
                    SizeF sf = g.MeasureString(TextAndData[2 * i + 1], font);
                    Rectangle rect = new Rectangle((int)(LastLandingX - 20), (int)y, (int)sf.Width + padding, (int)sf.Height + padding);
                    GraphicsPath path = ReportViewUtils.CreateRoundedRectanglePath(rect, 13);
                    //绘制底色
                    g.FillPath(brush, path);
                    Brush TextBrush = new SolidBrush(pen.Color);
                    ReportViewUtils.drawString(g, LocationModel.Location_Center, TextAndData[2 * i + 1], font, TextBrush, rect.Left, rect.Top, rect.Width, rect.Height);
                    TextBrush.Dispose();
                    path.Dispose();
                }
                else
                {
                    g.DrawLine(pen, new PointF(x, y), new PointF(x - Index, y));
                }
            }
        }

        public override void AnimalionEnd(System.Drawing.Graphics graphics, DetailDataModel data, System.Drawing.Pen pen, System.Drawing.Brush brush, System.Drawing.Font font)
        {
        }

        public override LegendNoteModel[] GetLegendNotes()
        {
            string[] datas = TextAndData;
            int count = datas.Length / 2;
            LegendNoteModel[] models = new LegendNoteModel[count];
            LegendNoteModel legendNoteModel = null;
            int centerY = EViewHeight / 4 + EStartY;
            int LegendNotestartY = centerY - (LegendNoteHeight + LegendNoteUpDownPadding) * count;
            for (int i = 0; i < count; i++)
            {
                legendNoteModel = new LegendNoteModel();
                legendNoteModel.Color = ReportViewUtils.PerferColors[i % ReportViewUtils.PerferColors.Length];
                legendNoteModel.mainText = datas[2 * i];
                legendNoteModel.mainData = int.Parse(datas[2 * i + 1]);
                int left = (EViewWidth / 2 - LegendNoteWidth) / 2 + EStartX + EViewWidth / 2;
                int top = LegendNotestartY + i * (LegendNoteHeight + LegendNoteUpDownPadding);
                legendNoteModel.Area = new AreaPositionRect(left, top, left + LegendNoteWidth, top + LegendNoteHeight);
                models[i] = legendNoteModel;
            }
            return models;
        }

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

        /// <summary>
        /// 与图形右侧的间距(默认20)
        /// </summary>
        public int LegendNoteLeftPadding { get; set; }

        /// <summary>
        /// 动画的最终落点(默认EStartX / 2;)
        /// </summary>
        public int LastLandingX { get; set; }

        /// <summary>
        /// 计算是否依赖于最大值(默认true)
        /// </summary>
        public bool IsCalculateDependByMaxNum { get; set; }
    }
}
