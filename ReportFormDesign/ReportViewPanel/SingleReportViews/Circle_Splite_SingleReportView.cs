using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using ReportFormDesign.CurrentPosition;
using ReportFormDesign.Model;
using ReportFormDesign.DrawUtils;
using ReportFormDesign.Adapter;
using ReportFormDesign.Animals;
using ReportFormDesign.DataModels;
using System.Drawing.Drawing2D;

namespace ReportFormDesign.ReportViewPanel
{
    /// <summary>
    /// 圆形分割报表
    /// </summary>
    public class Circle_Splite_SingleReportView : SingleReportView
    {

        private bool isFirst = true;
        /// <summary>
        /// 是否以动画结束时的效果来绘制(默认false)
        /// 需要重写OnRenderAnimalionedView方法
        /// </summary>
        public bool IsShowViewByAnimalioned { get; set; }
        /// <summary>
        /// 数据之和
        /// </summary>
        private int AllSum { get; set; }

        /// <summary>
        /// 绘制横线的长度
        /// </summary>
        private float LineWidth { get; set; }
        /// <summary>
        /// 绘制横线的高度
        /// </summary>
        public float LineHeight { get; set; }

        /// <summary>
        /// 绘制横线的最大长度
        /// </summary>
        public float MaxLineWidth { get; set; }


        public Circle_Splite_SingleReportView()
        {
            _MyToolTip = new FollowPopViewForTitle();
            _MyToolTip.TextColor = ReportViewUtils.perferRed;
            _MyToolTip.DataColor = ReportViewUtils.perferRed;
            _Interpolation.Value = 4;
            //AllSum = 0;
            MaxIndex = 70;
            MaxLineWidth = 40;
            LineHeight = 30;
            LegendNoteWidth = 30;
            LegendNoteHeight = 20;
            isAutoResizeLegendNote = false;
        }


        public override Object[] AnimalionPrepare(DetailDataModel drawModel)
        {
            //传递给动画绘制的数据
            Object[] args = null;
            if (drawModel is DetailDataModel)
            {
                DetailDataModel data = drawModel as DetailDataModel;
                string[] strs = data.TextAndData;
                float startAngle1 = 360;
                float centerAngle = 0;
                args = new Object[strs.Length];

                //绘制的中心点
                int centerX = EStartX + EViewWidth / 2;
                int centerY = EStartY + EViewHeight / 2;
                //半径
                float radius = EViewWidth / 2;
                float x, y;
                for (int i = 0; i < strs.Length / 2; i++)
                {
                    string dd = strs[2 * i + 1];
                        float realAngle = 360 * int.Parse(dd) * 1.0f / allNum;
                        //顺时针绘制
                        startAngle1 = startAngle1 - realAngle;
                        centerAngle = startAngle1 + realAngle / 2;

                        x = (float)(centerX + radius * Math.Cos(2 * Math.PI * centerAngle / 360));
                        y = (float)(centerY + radius * Math.Sin(2 * Math.PI * centerAngle / 360));
                        args[2 * i] = x;
                        args[2 * i + 1] = y;
                }
            }
            LineWidth = 0;
            TextLimiteCount = 6;
            return args;
        }

        public override void AnimalionDraw(Graphics graphics, DetailDataModel data, Pen pen, Brush brush, Font font, Object[] args)
        {
            DetailDataModel Data = data as DetailDataModel;
            drawLine(graphics, pen, brush, font, args, Data);
        }

        private void drawLine(Graphics graphics, Pen pen, Brush brush, Font font, Object[] args, DetailDataModel data)
        {
            if (data != null)
            {
                string[] strs = data.TextAndData;
                if (args != null)
                {
                    //绘制的中心点
                    int centerX = EStartX + EViewWidth / 2;
                    int centerY = EStartY + EViewHeight / 2;
                    for (int i = 0; i < args.Length / 2; i++)
                    {
                        if (strs[2 * i + 1].Equals("0"))
                        {
                            continue;
                        }
                        pen.Color = ReportViewUtils.PerferColors[i % ReportViewUtils.PerferColors.Length];
                        //Console.WriteLine("object = {0}", (double)item);
                        float x = (float)args[2 * i];
                        float y = (float)args[2 * i + 1];
                        float sx = x;
                        float sy = y;
                        graphics.FillEllipse(brush, new RectangleF(x - 2f, y -2f, 2f, 2f));
                        if (x < centerX)
                        {
                            sx = x - Index;
                            if (y < centerY)
                            {
                                sy = y - Index;
                                if (Index >= MaxIndex)
                                {
                                    graphics.DrawLine(pen, sx, sy, sx - LineWidth, sy);
                                    if (LineWidth >= MaxLineWidth)
                                    {
                                        ReportViewUtils.drawStringWithLimiteText(graphics, LocationModel.Location_Center, strs[2 * i + 1], font, brush, (sx - LineWidth), sy, LineWidth, LineHeight, TextLimiteCount);
                                        ReportViewUtils.drawStringWithLimiteText(graphics, LocationModel.Location_Center, strs[2 * i], font, brush, (sx - LineWidth), sy - LineHeight, LineWidth, LineHeight, TextLimiteCount);
                                    }
                                }
                                graphics.DrawLine(pen, x, y, sx, sy);
                            }
                            else
                            {
                                sy = y + Index;
                                if (Index >= MaxIndex)
                                {
                                    graphics.DrawLine(pen, sx, sy, sx - LineWidth, sy);
                                    if (LineWidth >= MaxLineWidth)
                                    {
                                        ReportViewUtils.drawStringWithLimiteText(graphics, LocationModel.Location_Center, strs[2 * i + 1], font, brush, (sx - LineWidth), sy, LineWidth, LineHeight, TextLimiteCount);
                                        ReportViewUtils.drawStringWithLimiteText(graphics, LocationModel.Location_Center, strs[2 * i], font, brush, (sx - LineWidth), sy - LineHeight, LineWidth, LineHeight, TextLimiteCount);
                                    }
                                }
                                graphics.DrawLine(pen, x, y, sx, sy);
                            }
                        }
                        else
                        {
                            sx = x + Index;
                            if (y < centerY)
                            {
                                sy = y - Index;
                                if (Index >= MaxIndex)
                                {
                                    graphics.DrawLine(pen, sx, sy, sx + LineWidth, sy);
                                    if (LineWidth >= MaxLineWidth)
                                    {
                                        ReportViewUtils.drawStringWithLimiteText(graphics, LocationModel.Location_Center, strs[2 * i + 1], font, brush, (sx), sy, LineWidth, LineHeight, TextLimiteCount);
                                        ReportViewUtils.drawStringWithLimiteText(graphics, LocationModel.Location_Center, strs[2 * i], font, brush, (sx), sy - LineHeight, LineWidth, LineHeight, TextLimiteCount);
                                    }
                                }
                                graphics.DrawLine(pen, x, y, sx, sy);
                            }
                            else
                            {
                                sy = y + Index;
                                if (Index >= MaxIndex)
                                {
                                    graphics.DrawLine(pen, sx, sy, sx + LineWidth, sy);
                                    if (LineWidth >= MaxLineWidth)
                                    {
                                        ReportViewUtils.drawStringWithLimiteText(graphics, LocationModel.Location_Center, strs[2 * i + 1], font, brush, (sx), sy, LineWidth, LineHeight, TextLimiteCount);
                                        ReportViewUtils.drawStringWithLimiteText(graphics, LocationModel.Location_Center, strs[2 * i], font, brush, (sx), sy - LineHeight, LineWidth, LineHeight, TextLimiteCount);
                                    }
                                }
                                graphics.DrawLine(pen, x, y, sx, sy);
                            }
                        }

                    }
                }

                if (Index >= MaxIndex)
                {
                    if (LineWidth <= MaxLineWidth)
                    {
                        LineWidth += 4;
                    }
                }
                //Console.WriteLine("AnimalionDraw");
                //graphics.DrawLine(pen, 0, 0, index, index);
            }
        }


        public override void AnimalionEnd(Graphics graphics, DetailDataModel data, Pen pen, Brush brush, Font font)
        {

        }

        public override LegendNoteModel[] GetLegendNotes()
        {
            string[] datas = TextAndData;
            int count = datas.Length / 2;
            LegendNoteModel[] models = new LegendNoteModel[count];
            LegendNoteModel legendNoteModel = null;
            int sX = (int)(EStartX + EViewWidth + (Width - EStartX - EViewWidth) / 2 - LegendNoteWidth);
            int sY = EStartY + EViewHeight / 2 - (LegendNoteHeight + LegendNoteUpDownPadding) * count / 2;
            for (int i = 0; i < count; i++)
            {
                legendNoteModel = new LegendNoteModel();
                legendNoteModel.Color = ReportViewUtils.PerferColors[i % ReportViewUtils.PerferColors.Length];
                legendNoteModel.mainText = datas[2 * i];
                legendNoteModel.mainData = int.Parse(datas[2 * i + 1]);
                legendNoteModel.Area = new AreaPositionRect(sX, sY + (LegendNoteUpDownPadding + LegendNoteHeight) * i, sX + LegendNoteWidth, sY + (LegendNoteUpDownPadding + LegendNoteHeight) * i + LegendNoteHeight);
                models[i] = legendNoteModel;
            }
            return models;
        }

        public override void OnRenderNormalView(Graphics g, DataModel Data, Pen linePen, Brush lineBrush, Brush TextBrush, Brush DataBrush, Font FontText, Font FontData)
        {
            int StartX = Data.Area.left;
            int startY = Data.Area.top;
            int height = Data.Area.bottom - Data.Area.top;
            int width = Data.Area.right - StartX;
            //常规绘制(自定义模型的绘制)
            if (Data is DetailDataModel)
            {
                DetailDataModel data = (DetailDataModel)Data;
                string[] datas = data.TextAndData;
                SolidBrush sbrush1 = new SolidBrush(Color.FromArgb(150, 1, 77, 103));
                Rectangle rect = new Rectangle(StartX, startY, width, height);
                //if (isFirst)
                //{
                //    for (int i = 0; i < datas.Length / 2; i++)
                //    {
                //        string dd = datas[2 * i + 1];
                //        AllNum += int.Parse(dd);
                //    }
                //    isFirst = false;
                //}
                float startAngle1 = 360;
                for (int i = 0; i < datas.Length / 2; i++)
                {
                    string dd = datas[2 * i + 1];
                    float realAngle = 360 * int.Parse(dd) * 1.0f / allNum;
                    //顺时针绘制
                    startAngle1 = startAngle1 - realAngle;
                    linePen.Color = ReportViewUtils.PerferColors[i % ReportViewUtils.PerferColors.Length];
                    g.DrawArc(linePen, rect, startAngle1, realAngle + 1);
                }

                sbrush1.Dispose();

                //LinearGradientBrush brush = new LinearGradientBrush(rect, Color.Black, Color.Gray, LinearGradientMode.Horizontal);
                //g.FillEllipse(brush, rect);
            }
            if (IsShowViewByAnimalioned)
            {
                Index = MaxIndex;
                DetailDataModel data = Data as DetailDataModel;
                linePen.Width = 1.0f;
                object[] args = AnimalionPrepare(data);
                LineWidth = MaxLineWidth;
                drawLine(g, linePen, lineBrush, FontText, args, data);
            }
        }

        public override void LegendNoteSizeChanged()
        {
            if (IsAutoReSizeView)
            {
                if (IsDrawLegendNote)
                {
                    LegendNoteHeight = EViewWidth / 9;
                    LegendNoteWidth = LegendNoteHeight * 2;
                    LegendNoteUpDownPadding = LegendNoteHeight;
                    //对注解重新缩放
                    LegendNoteModel legendNoteModel = null;
                    int count = LegendNoteModels.Length;
                    int sX = (int)(EStartX + EViewWidth + (Width - EStartX - EViewWidth) / 2 - LegendNoteWidth);
                    int sY = EStartY + EViewHeight / 2 - (LegendNoteHeight + LegendNoteUpDownPadding) * count / 2;
                    for (int i = 0; i < count; i++)
                    {
                        legendNoteModel = LegendNoteModels[i];
                        legendNoteModel.Area = new AreaPositionRect(sX, sY + (LegendNoteHeight + LegendNoteUpDownPadding) * i, sX + LegendNoteWidth, sY + (LegendNoteHeight + LegendNoteUpDownPadding) * i + LegendNoteHeight);
                    }
                    //LegendNoteFontTextSize = ViewWidth / 12;
                }
            }
            
        }


        public override void ResizeReportViewChange()
        {
            int resize = Math.Min(Width, Height);
            EViewWidth = 3 * resize / 7;
            EViewHeight = EViewWidth;
            TextSize = EViewWidth * 1.0f / 12;
            DataSize = EViewWidth * 1.0f / 14;
            if (IsHorizontally)
            {
                EStartX = this.Width / 2 - EViewWidth / 2;
                EStartY = this.Height / 2 - EViewHeight / 2;
            }
            StrokenWidth = EViewWidth * 1.0f / 4;
            MaxIndex = 2 * EViewWidth / 5;
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
    }
}
