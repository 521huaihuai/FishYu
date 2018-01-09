using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using ReportFormDesign.Adapter;
using ReportFormDesign.DrawUtils;
using ReportFormDesign.Model;

namespace ReportFormDesign.ReportViewPanel.AutoSortReportView
{
    public partial class PieChart_ReportView : ViewPanel
    {
        #region 字段
        //自定义Pie标题
        public string pieTitle;

        //报表的数据源(奇数位为数据值, 偶数位为数据名称)
        public string[] TextAndData;
        //存储数据源最大值
        public int maxNum;
        //绘制pen的宽度
        private int piePenWidth;

        //是否第一次绘制标签
        private bool IsFirst;
        //前几次扫过的角度
        private float sweepAngle;
        #endregion
        public PieChart_ReportView()
        {
            IsDrawTitle = false;
            IsDrawBoard = false;
            IsDrawFrame = false;
            BoardColor = Color.LawnGreen;
            IsDrawPieTiTle = true;
            pieTitleFont = new Font("微软雅黑", 10);
            pieTitleColor = Color.White;
            piePenWidth = 60;
            IsDrawLineNote = true;
            NoteColor = Color.White;
        }

        public void initData()
        {
            List<DataModel> list = new List<DataModel>();
            if (pieTitle == null)
            {
                pieTitle = "October 1024件";
            }
            if (TextAndData == null)
            {
                TextAndData = new string[] { "移动", "230", "联通", "140", "电信", "410", "公网", "200", "华数", "44" };
            }
            //计算数据之和以及设定最大数据
            allNum = 0;
            maxNum = 0;
            CalculateAllNum();

            DataModel model = new DetailDataModel(pieTitle, 0, TextAndData);
            if (IsHorizontally)
            {
                EStartX = this.Width / 2 - EViewWidth / 2;
                EStartY = this.Height / 2 - EViewHeight / 2;
            }
            model.Area = new AreaPositionRect(EStartX, EStartY, EViewWidth + EStartX, EViewHeight + EStartY);
            list.Add(model);

            ReportViewAdapter adapter = new SingleReportViewAdapter();
            //设置数据源
            adapter.setData(list);
            adapter.IsVerticalShowData = false;
            setAdapter(adapter);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (IsAutoReSizeView)
            {
                int resize = Math.Min(Width, Height);
                EViewWidth = 2 * resize / 3;
                EViewHeight = EViewWidth;
                //ResizeReportViewChange();
            }
            if (IsHorizontally)
            {
                EStartX = this.Width / 2 - EViewWidth / 2;
                EStartY = this.Height / 2 - EViewHeight / 2;
            }
            //避免异常
            if (piePenWidth <= 1)
            {
                piePenWidth = 1;
            }
            if (TextSize <= 1)
            {
                TextSize = 1.0f;
            }
            if (DataSize <= 1)
            {
                DataSize = 1.0f;
            }
            if (Height <= 40)
            {
                Height = 40;
            }
        }

        private void CalculateAllNum()
        {
            for(int i = 0; i < TextAndData.Length / 2; i++)
            {
                string dd = TextAndData[2 * i + 1];
                if(maxNum < int.Parse(dd))
                {
                    maxNum = int.Parse(dd);
                }

                allNum += int.Parse(dd);
            }
        }

        public override void DrawSelfDefineView(System.Drawing.Graphics g, System.Drawing.Pen linePen, System.Drawing.Brush lineBrush, System.Drawing.Brush TextBrush, System.Drawing.Brush DataBrush, System.Drawing.Font FontText, System.Drawing.Font FontData)
        {
            throw new NotImplementedException();
        }

        public override void childPaint(System.Drawing.Graphics g, Model.DataModel Data, System.Drawing.Pen linePen, System.Drawing.Brush lineBrush, System.Drawing.Brush TextBrush, System.Drawing.Brush DataBrush, System.Drawing.Font FontText, System.Drawing.Font FontData)
        {
            //绘制Pie自定义标题
            if (IsDrawPieTiTle)
            {
                Brush pieTitleBrush = new SolidBrush(pieTitleColor);
                ReportViewUtils.drawString(g, LocationModel.Location_Right_Right, pieTitle, pieTitleFont, pieTitleBrush, 0, 3, this.Width, padding);

                pieTitleBrush.Dispose();
            }

            int length = this.Height > this.Width ? (this.Width / 2) : (this.Height / 2);
            int startX = (this.Width - length) / 2;
            int startY = (this.Height - length) / 2;

            if (Data is DetailDataModel)
            {
                DetailDataModel data = (DetailDataModel)Data;
                string[] datas = data.TextAndData;
                linePen = new Pen(Color.FromArgb(180, 71, 85, 182), 60);
                //7/12的效果最好看
                piePenWidth = 7 * length / 12;
                Rectangle rectangle = new Rectangle(startX, startY, length, length);

                IsFirst = true;
                sweepAngle = 0;
                float startAngle = 360;
                for (int i = 0; i < datas.Length / 2; i++)
                {
                    string str = datas[2 * i + 1];
                    float realAngle = 360 * int.Parse(str) * 1.0f / allNum;

                    linePen.Width = piePenWidth * int.Parse(str) / maxNum;
                    int realX = startX + (int)(piePenWidth - linePen.Width + 0.5f) / 2;
                    int realY = startY + (int)(piePenWidth - linePen.Width + 0.5f) / 2;
                    int realLength = length - (int)(piePenWidth - linePen.Width);

                    rectangle = new Rectangle(realX, realY, realLength, realLength);
                    linePen.Color = ReportViewUtils.PerferColors[i % ReportViewUtils.PerferColors.Length];
                    startAngle -= (realAngle - 0.5f);
                    g.DrawArc(linePen, rectangle, startAngle, realAngle + 0.7f);

                    if(IsDrawLineNote)
                    {
                        DrawLineNote(g, linePen.Color, data, 2 * i + 1, startAngle, realAngle, realLength / 2);
                    }
                }

            }

            linePen.Dispose();
            lineBrush.Dispose();
            TextBrush.Dispose();
            DataBrush.Dispose();
            FontText.Dispose();
            FontData.Dispose();
        }

        private void DrawLineNote(Graphics g, Color dataColor, DetailDataModel data, int index, float startAngle, float realAngle, int radius)
        {
            Pen bl = new Pen(dataColor);
            Brush brush = new SolidBrush(NoteColor);
            Font LineNoteFont = new Font("微软雅黑", 9);
            string[] str = data.TextAndData;

            ///绘制线
            float midAngle;
            if (IsFirst)
            {
                midAngle = realAngle / 2;
                sweepAngle = realAngle;
                IsFirst = false;
            }
            else
            {
                midAngle = sweepAngle + realAngle / 2;
                sweepAngle += realAngle;
            }
            float rmidAngle = (float)(2 * Math.PI / (360 / (midAngle)));

            float startx = radius * (float)Math.Cos(rmidAngle) + this.Width / 2;
            float starty = this.Height / 2 - radius * (float)Math.Sin(rmidAngle);
            float endx = (radius + 3 * piePenWidth / 4) * (float)Math.Cos(rmidAngle) + this.Width / 2;
            float endy = this.Height / 2 - (radius + 3 * piePenWidth / 4) * (float)Math.Sin(rmidAngle);
            g.DrawLine(bl, startx, starty, endx, endy);

            float end = endx;
            float fonty = endy - padding/ 2;
            float fontx;
            if (end >= this.Width / 2)
            {
                end += (piePenWidth / 3);
                fontx = end;
                ReportViewUtils.drawString(g, LocationModel.Location_Left_Left, str[index - 1], LineNoteFont, brush, fontx, fonty, padding, padding);
            }
            else
            {
                end -= (piePenWidth / 3);
                fontx = end - padding;
                ReportViewUtils.drawString(g, LocationModel.Location_Right_Right, str[index - 1], LineNoteFont, brush, fontx, fonty, padding, padding);
            }
            g.DrawLine(bl, endx, endy, end, endy);
            ///绘制标签
            //ReportViewUtils.drawString(g, LocationModel.Location_Right_Right, str[index - 1], LineNoteFont, brush, fontx, fonty, padding, padding);

            bl.Dispose();
            brush.Dispose();
            LineNoteFont.Dispose();
        }


        #region 属性
        /// <summary>
        /// 是否显示Pie自定义标题 默认true
        /// </summary>
        public bool IsDrawPieTiTle { get; set; }

        /// <summary>
        /// 自定义Pie标题 默认White
        /// </summary>
        public Color pieTitleColor { get; set; }

        /// <summary>
        /// Pie自定义标题文本 默认"微软雅黑",10
        /// </summary>
        public System.Drawing.Font pieTitleFont { get; set; }

        /// <summary>
        /// 是否绘制饼图标识 默认true
        /// </summary>
        public bool IsDrawLineNote { get; set; }

        /// <summary>
        /// 饼图标识字体颜色 默认white
        /// </summary>
        public Color NoteColor { get; set; }
        #endregion

    }
}
