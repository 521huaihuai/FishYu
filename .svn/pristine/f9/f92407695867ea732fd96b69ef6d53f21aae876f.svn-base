using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using ReportFormDesign.Adapter;
using ReportFormDesign.DrawUtils;
using ReportFormDesign.Legendnotes;
using ReportFormDesign.Model;
using ReportFormDesign.Models.DataModels;

namespace ReportFormDesign.ReportViewPanel.AutoSortReportView
{
    public partial class DataAnalysis_ReportView : ViewPanel, InterfaceLegendnote
    {
        #region 字段
        public string group;
        //数据形式
        public string[] TextDataData;
        private int imgLabelWidth;
        private int imgLabelHeight;
        public string Legendnote1;
        public string Legendnote2;

        //插值器
        protected InterfaceLegendnote interfaceLegendnote;

        private static Image _rentalImg1 = null;
        private static Image _rentalImg2 = null;
        private ImageAttributes _attr;
        #endregion
        public DataAnalysis_ReportView()
        {
            IsDrawFrame = false;
            TopPadding = 5;
            LeftPadding = 15;
            RightPadding = 5;
            imgLabelWidth = 40;
            imgLabelHeight = 35;
            DataTitleHeight = 27;
            DataTitleColor = Color.White;
            DataTitleSize = 12;
            DataValueColor = Color.White;

            this.interfaceLegendnote = this;
            LegendnoteColor1 = Color.Blue;
            LegendnoteColor2 = Color.Yellow;

            this.Height = 84;
        }

        public void InitData()
        {
            List<DataModel> list = new List<DataModel>();
            if (group == null)
            {
                group = "测试";
            }
            if (TextDataData == null)
            {
                TextDataData = new string[] { "出租房管理情况", "230", "140", "流动人口管理情况", "410", "200" };
            }

            DataModel model = new DetailDataModel(group, 0, TextDataData);
            list.Add(model);

            ReportViewAdapter adapter = new SingleReportViewAdapter();
            adapter.setData(list);
            adapter.IsVerticalShowData = false;
            setAdapter(adapter);

            _attr = GetAlphaImgAttr(98);
            _rentalImg1 = Image.FromFile(ImgPath1);
            _rentalImg2 = Image.FromFile(ImgPath2);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            if (IsAutoReSizeView)
            {
                TopPadding = 5;
                LeftPadding = 15;
                RightPadding = 5;
            }
        }

        public override void DrawSelfDefineView(Graphics g, Pen linePen, Brush lineBrush, Brush TextBrush, Brush DataBrush, Font FontText, Font FontData)
        {
            
        }

        public override void childPaint(Graphics g, DataModel data, Pen linePen, Brush lineBrush, Brush TextBrush, Brush DataBrush, Font FontText, Font FontData)
        {
            if (data is DetailDataModel)
            {
                DetailDataModel model = (DetailDataModel)data;
                string[] datas = model.TextAndData;

                imgLabelWidth = _rentalImg1.Width;
                int startX = LeftPadding + imgLabelWidth + 3;
                int startY = TopPadding + DataTitleHeight + 8;
                int width = (this.Width - 2 * LeftPadding) / 2 - startX;
                int height = (imgLabelHeight - 8) / 2;
                Brush brush = new SolidBrush(Color.White);
                Rectangle rectangle;
                float realWidth;

                for (int i = 0; i < 4; i = i + 3 )
                {
                    DrawmainText(g, datas[i], FontText, startX - imgLabelWidth);
                    if(i == 0)
                    {
                        g.DrawImage(_rentalImg1, new Rectangle(startX - imgLabelWidth - 3, startY, _rentalImg1.Width, _rentalImg1.Height), 0, 0, _rentalImg1.Width, _rentalImg1.Height, GraphicsUnit.Pixel, _attr);
                    }
                    else
                    {
                        g.DrawImage(_rentalImg2, new Rectangle(startX - imgLabelWidth - 3, startY, _rentalImg2.Width, _rentalImg2.Height), 0, 0, _rentalImg2.Width, _rentalImg2.Height, GraphicsUnit.Pixel, _attr);
                    }

                    realWidth = width * (int.Parse(datas[i + 1]) * 1.0f / (int.Parse(datas[i + 1]) + int.Parse(datas[i + 2])));
                    brush = new SolidBrush(LegendnoteColor1);
                    rectangle = new Rectangle(startX, startY, (int)realWidth, height);
                    g.FillRectangle(brush, rectangle);
                    SizeF size = g.MeasureString(datas[i + 1], FontData);
                    ReportViewUtils.drawString(g, LocationModel.Location_Left_Left, datas[i + 1], FontData, DataBrush, startX + realWidth, startY, size.Width, height);

                    realWidth = width * (int.Parse(datas[i + 2]) * 1.0f / (int.Parse(datas[i + 1]) + int.Parse(datas[i + 2])));
                    brush = new SolidBrush(LegendnoteColor2);
                    rectangle = new Rectangle(startX, startY + height + 4, (int)realWidth, height);
                    g.FillRectangle(brush, rectangle);
                    size = g.MeasureString(datas[i + 2], FontData);
                    ReportViewUtils.drawString(g, LocationModel.Location_Left_Left, datas[i + 2], FontData, DataBrush, startX + realWidth, startY + height + 4, size.Width, height);

                    startX += (width + imgLabelWidth + 3);
                }

                brush.Dispose();
            }

            if (interfaceLegendnote != null)
            {
                interfaceLegendnote.drawLegendnote(g, linePen, lineBrush, TextBrush, DataBrush, FontText, FontData);
            }
        }

        public void drawLegendnote(Graphics g, Pen linePen, Brush lineBrush, Brush TextBrush, Brush DataBrush, Font FontText, Font FontData)
        {
            if (Legendnote1 == null)
            {
                return;
            }
            TextBrush = new SolidBrush(ReportViewUtils.perferOrange);
            int width = (this.Width - LeftPadding - RightPadding) / 2;
            Brush brush = new SolidBrush(LegendnoteColor1);
            FontText = new Font("微软雅黑", 9);
            SizeF size = g.MeasureString(Legendnote1, FontText);

            g.FillRectangle(brush, this.Width - 2 * size.Width - RightPadding, TopPadding + 3, size.Width, 2 * size.Height / 3);
            ReportViewUtils.drawString(g, LocationModel.Location_Left_Left, Legendnote1, FontText, TextBrush, this.Width - size.Width - RightPadding, TopPadding, size.Width, size.Height);
            brush = new SolidBrush(LegendnoteColor2);
            g.FillRectangle(brush, this.Width - 2 * size.Width - RightPadding, TopPadding + size.Height + 3, size.Width, 2 * size.Height / 3);
            ReportViewUtils.drawString(g, LocationModel.Location_Left_Left, Legendnote2, FontText, TextBrush, this.Width - size.Width - RightPadding, TopPadding + size.Height, size.Width, size.Height);

            brush.Dispose();
        }

        private void DrawmainText(Graphics g, string p, System.Drawing.Font FontText, int startX)
        {
            if (p == null)
            {
                return;
            }

            Brush brush = new SolidBrush(DataTitleColor);
            FontText = new Font("微软雅黑", DataTitleSize);
            SizeF size = g.MeasureString(p, FontText);
            ReportViewUtils.drawString(g, LocationModel.Location_Left_Left, p, FontText, brush, startX, TopPadding, size.Width, DataTitleHeight);

            brush.Dispose();
        }

        public ImageAttributes GetAlphaImgAttr(int opcity)
        {
            if (opcity < 0 || opcity > 100)
            {
                throw new ArgumentOutOfRangeException("opcity 值为 0~100");
            }
            //颜色矩阵
            float[][] matrixItems =
            {
                new float[]{1,0,0,0,0},
                new float[]{0,1,0,0,0},
                new float[]{0,0,1,0,0},
                new float[]{0,0,0,(float)opcity / 100,0},
                new float[]{0,0,0,0,1}
            };
            ColorMatrix colorMatrix = new ColorMatrix(matrixItems);
            ImageAttributes imageAtt = new ImageAttributes();
            imageAtt.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            return imageAtt;
        }

        #region 属性
        /// <summary>
        /// 左边距
        /// </summary>
        public int LeftPadding { get; set; }

        /// <summary>
        /// 右边距
        /// </summary>
        public int RightPadding { get; set; }

        /// <summary>
        /// 顶边距
        /// </summary>
        public int TopPadding { get; set; }

        /// <summary>
        /// 数据标题颜色 默认white
        /// </summary>
        public Color DataTitleColor { get; set; }

        /// <summary>
        /// 数据值颜色 默认white
        /// </summary>
        public Color DataValueColor { get; set; }

        /// <summary>
        /// 数据标题高度 默认27
        /// </summary>
        public int DataTitleHeight { get; set; }

        /// <summary>
        /// 数据标题大小 默认12
        /// </summary>
        public float DataTitleSize { get; set; }

        /// <summary>
        /// 标签颜色1 默认blue
        /// </summary>
        public Color LegendnoteColor1 { get; set; }

        /// <summary>
        /// 标签颜色2 默认yellow
        /// </summary>
        public Color LegendnoteColor2 { get; set; }
        #endregion


        public string ImgPath1 { get; set; }

        public string ImgPath2 { get; set; }
    }
}
