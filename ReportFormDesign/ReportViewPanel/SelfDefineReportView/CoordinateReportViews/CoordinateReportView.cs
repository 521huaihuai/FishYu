using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using ReportFormDesign.Adapter;
using ReportFormDesign.Animals;
using ReportFormDesign.DataModels;
using ReportFormDesign.DrawUtils;
using ReportFormDesign.Model;
using ReportFormDesign.ToolTips;

namespace ReportFormDesign.ReportViewPanel.SelfDefineReportView.CoordinateReportViews
{


    public abstract class CoordinateReportView : SelfDefineReportView
    {

        /// <summary>
        /// 绘制统计数的位置
        /// </summary>
        public enum AllSumLocation
        {
            none, left_up, left_down, left_center, center_up, center_down, center_center, right_up, right_down, right_center,
        }
        public CoordinateReportView()
        {
            MyToolTip = new CoordinateHelpLineToolTip();
            MyToolTip.DataColor = ReportViewUtils.perferRed;
            MaxNum = 0;
            LableSize = 8;
            AllSumSize = 10;
            TextRotate = 60;
            CoordinateDataModelBean = new CoordinateDataBean();
            TopPadding = 30;
            LeftPadding = 0;
            RightPadding = 30;
            BottomPadding = 0;
            FillBrushAlpha = 30;
            Count_X = 10;
            Count_Y = 5;
            Tension = 0.5f;
            IsShowX_Scale = true;
            IsShowY_Scale = true;
            IsEnableGridLine = true;
            IsNeedReLocationToopTip = false;
            IsDrawDetailData = true;
            IsDrawCurve = true;
            LableAllNum = " / 个";
            PerMax = 50;
            IsEnableArrow = false;
        }

        public void initData(List<DataModel> CoordinateModel)
        {
            //List<DataModel> list = new List<DataModel>();
            //DataModel model = new DataModel();
            if (Title != null)
            {
                CoordinateDataModelBean.Title = Title;
            }
            else
            {
                Title = CoordinateDataModelBean.Title;
            }
            if (LableX != null)
            {
                CoordinateDataModelBean.Lable_X = LableX;
            }
            else
            {
                LableX = CoordinateDataModelBean.Lable_X;
            }
            if (LableY != null)
            {
                CoordinateDataModelBean.Lable_Y = LableY;
            }
            else
            {
                LableY = CoordinateDataModelBean.Lable_Y;
            }
            if (CoordinateModel != null)
            {
                CoordinateModels = CoordinateModel;
                CoordinateDataModelBean.CoordinateModelList = CoordinateModels;
            }
            else
            {
                CoordinateModels = new List<DataModel>();
                CoordinateModels.Add(new DataModel("时间01", 50));
                CoordinateModels.Add(new DataModel("时间02", 100));
                CoordinateModels.Add(new DataModel("时间03", 150));
                CoordinateModels.Add(new DataModel("时间04", 200));
                CoordinateModels.Add(new DataModel("时间05", 200));
                CoordinateModels.Add(new DataModel("时间06", 200));
                CoordinateModels.Add(new DataModel("时间07", 200));
                CoordinateDataModelBean.CoordinateModelList = CoordinateModels;
            }

            CoordinateDataModelBean.initXYData();
            //计算数据之和以及设定最大数据
            allNum = 0;
            MaxNum = ReportViewUtils.getMaxNumFromData(CoordinateDataModelBean.Y_Data, PerMax);
            CalculateAllNum();
            if (IsNeedDataPartitioning)
            {
                //整理数据
            }
            //自动缩放
            if (IsAutoReSizeView)
            {
                ResizeReportViewChange();
            }
            else
            {
                if (EViewWidth == 0)
                {
                    throw new ArgumentException("请设定绘制图像的宽高, 或者设置IaAutoReSize = true");
                }
            }

            //自动调整位置
            if (IsHorizontally)
            {
                ReLocationModelPos();
            }


            //model.Area = new AreaPositionRect(StartX, StartY, ViewWidth + StartX, ViewHeight + StartY);
            //list.Add(model);
            ReportViewAdapter adapter = new SingleReportViewAdapter();
            //设置数据源
            adapter.setData(CoordinateModels);
            adapter.IsVerticalShowData = false;
            setAdapter(adapter);
        }

        private void CalculateAllNum()
        {
            float[] data = CoordinateDataModelBean.Y_Data;
            foreach (float item in data)
            {
                allNum += item;
            }
        }

        public override void ResizeReportViewChange()
        {
            EViewWidth = 6 * Width / 7;
            EViewHeight = 5 * this.Height / 6;
            TextSize = Width * 1.0f / 40;
            padding = EViewWidth / 20;
            ResizePadding();
        }

        public override void ReLocationModelPos()
        {
            EStartX = (Width - EViewWidth) / 2;
            EStartY = (Height - EViewHeight) / 4;
            if (MyToolTip is CoordinateHelpLineToolTip)
            {
                CoordinateHelpLineToolTip tool = MyToolTip as CoordinateHelpLineToolTip;
                tool.CoordinateWidth = EViewWidth - RightPadding - LeftPadding;
                tool.CoordinateHeight = EViewHeight - TopPadding - BottomPadding;
                tool.CoordinateStartX = EStartX + LeftPadding;
                tool.CoordinateStartY = EStartY + EViewHeight - BottomPadding;
            }
            CoordinateWidth = EViewWidth - RightPadding - LeftPadding;
            CoordinateHeight = EViewHeight - TopPadding - BottomPadding;
            CoordinateStartX = EStartX + LeftPadding;
            CoordinateStartY = EStartY + EViewHeight - BottomPadding;
        }

        public override void DrawSelfDefine(System.Drawing.Graphics g, System.Drawing.Pen linePen, System.Drawing.Brush lineBrush, System.Drawing.Brush TextBrush, System.Drawing.Brush DataBrush, System.Drawing.Font FontText, System.Drawing.Font FontData)
        {
            linePen.Width = 1.0f;
            Font LableFont = null;
            if (IsLableFontBold)
            {
                LableFont = new Font("幼圆", LableSize, FontStyle.Bold);
            }
            else
            {
                LableFont = new Font("幼圆", LableSize);
            }

            Font AllSumFont = new Font("Arial", AllSumSize);

            //数据源长度
            int dataLength = 0;
            if (CoordinateModels != null)
            {
                dataLength = CoordinateModels.Count;
            }

            Point[] listPoint = new Point[dataLength];

            if(IsDrawTitle)
            {
                ReportViewUtils.drawString(g, Title, FontText, TextBrush, EStartX, EStartY, EViewWidth, TopPadding);
            }
            g.DrawLine(linePen, CoordinateStartX, CoordinateStartY, CoordinateStartX + CoordinateWidth, CoordinateStartY);
            if (!IsEnableGridLine)
            {
                if (IsEnableArrow)
                {
                    g.DrawLine(linePen, CoordinateStartX + CoordinateWidth, CoordinateStartY, CoordinateStartX + CoordinateWidth - 6, CoordinateStartY + 3);
                    g.DrawLine(linePen, CoordinateStartX + CoordinateWidth, CoordinateStartY, CoordinateStartX + CoordinateWidth - 6, CoordinateStartY - 3);
                }
            }


            // 绘制围绕点旋转的文本  
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;
            //绘制标签
            DrawRoateText.DrawString(g, LableX, LableFont, TextBrush, new PointF(CoordinateStartX + CoordinateWidth + padding, CoordinateStartY + padding), format, TextRotate);

            //绘制刻度
            float per_X = 0;
            if (IsEnableGridLine)
            {
                per_X = (CoordinateWidth * 1.0f) / Count_X;
            }
            else
            {
                per_X = (CoordinateWidth - padding) * 1.0f / Count_X;
            }
            for (int i = 0; i < Count_X + 1; i++)
            {
                //绘制刻度
                int x = (int)(CoordinateStartX + i * per_X);
                if (IsEnableGridLine)
                {

                    linePen.Color = Color.FromArgb(30, LineColor.R, LineColor.G, LineColor.B);
                    g.DrawLine(linePen, x, CoordinateStartY, x, CoordinateStartY - CoordinateHeight);
                }
                else
                {
                    g.DrawLine(linePen, x, CoordinateStartY, x, CoordinateStartY - padding / 10);
                }

                if (dataLength > i)
                {
                    listPoint[i] = new Point(x, 0);
                }


                //绘制刻度值
                if (IsShowX_Scale)
                {
                    if (dataLength > i)
                    {
                        if (CoordinateDataModelBean != null)
                        {
                            if (IsRectAngle)
                            {
                                if (ConformCount == 0)
                                {
                                    DrawRoateText.DrawString(g, CoordinateDataModelBean.X_Data[i], LableFont, TextBrush, new PointF(CoordinateStartX + i * per_X + per_X / 4, CoordinateStartY + padding), format, TextRotate);
                                }
                                else
                                {
                                    DrawRoateText.DrawString(g, CoordinateDataModelBean.X_Data[i], LableFont, TextBrush, new PointF(CoordinateStartX + i * per_X + (ConformCount - 1) * 1.0f / 2 * per_X + per_X / 4, CoordinateStartY + padding), format, TextRotate);
                                }

                            }
                            else
                            {
                                DrawRoateText.DrawString(g, CoordinateDataModelBean.X_Data[i], LableFont, TextBrush, new PointF(CoordinateStartX + i * per_X, CoordinateStartY + padding), format, TextRotate);
                            }

                        }

                    }
                }
            }

            linePen.Color = LineColor;



            //如果自动刷新时间, 则会刷新最大值, 从而刷新Y轴
            if (isAutoUpdateData)
            {
                if (CoordinateDataModelBean != null)
                {
                    if (CoordinateDataModelBean.Y_Data != null)
                    {
                        MaxNum = ReportViewUtils.getMaxNumFromData(CoordinateDataModelBean.Y_Data);
                    }
                }
            }

            //绘制Y轴
            g.DrawLine(linePen, CoordinateStartX, CoordinateStartY, CoordinateStartX, CoordinateStartY - CoordinateHeight);
            if (!IsEnableGridLine)
            {
                if (IsEnableArrow)
                {
                    g.DrawLine(linePen, CoordinateStartX + 3, CoordinateStartY - CoordinateHeight + 6, CoordinateStartX, CoordinateStartY - CoordinateHeight);
                    g.DrawLine(linePen, CoordinateStartX - 3, CoordinateStartY - CoordinateHeight + 6, CoordinateStartX, CoordinateStartY - CoordinateHeight);
                }
            }
            //绘制标签
            int lableLength = CoordinateDataModelBean.Lable_Y.Length;
            ReportViewUtils.drawString(g, LocationModel.Location_Right_Right + padding, LableY, LableFont, TextBrush, EStartX, CoordinateStartY - CoordinateHeight - padding, LeftPadding - 10, padding);
            //g.DrawString(LableY, LableFont, TextBrush, (padding - lableLength * lableSize) / 2 + startX, startY + topPadding);
            //对应到坐标上
            //规定Y轴刻度值
            float perNum = MaxNum / Count_Y;
            //规定Y轴刻度
            float per_Y = 0;
            if (IsEnableGridLine)
            {
                per_Y = (CoordinateHeight) * 1.0f / Count_Y;
            }
            else
            {
                per_Y = (CoordinateHeight - padding) * 1.0f / Count_Y;
            }
            //绘制刻度值和刻度
            linePen.Color = Color.FromArgb(30, LineColor.R, LineColor.G, LineColor.B);
            for (int i = 0; i < Count_Y + 1; i++)
            {
                if (IsShowY_Scale)
                {
                    ReportViewUtils.drawString(g, LocationModel.Location_Right_Right, (int)perNum * i + "", LableFont, TextBrush, EStartX, CoordinateStartY - i * per_Y - padding / 2, LeftPadding - 10, padding);
                    //g.DrawString(perNum * i + "", LableFont, TextBrush, (padding - lableSize * 3) / 2 + startX, height - padding - i * per_Y - lableSize - bottomPadding);
                }
                int y = (int)(CoordinateStartY - i * per_Y);
                g.DrawLine(linePen, CoordinateStartX, y, CoordinateStartX + CoordinateWidth, y);
            }
            linePen.Color = LineColor;




            //计算数据中的点对应到坐标的点
            bool isMouseIn = false;
            for (int i = 0; i < dataLength; i++)
            {
                float dataY = CoordinateDataModelBean.Y_Data[i];
                //把数据对应到实际的坐标
                int posY = (int)(CoordinateStartY - dataY * per_Y / perNum);
                listPoint[i].Y = posY;
                //重新定位数据模型的坐标
                isMouseIn = adapter.DataList[i].Area.IsMouseIn;
                adapter.DataList[i].Area = ReLoadAreaPosition(listPoint[i].X, listPoint[i].Y, (int)per_X, (int)per_Y);
                //adapter.DataList[i].Area = new AreaPositionRect(listPoint[i].X, listPoint[i].Y, listPoint[i].X, listPoint[i].Y);
                adapter.DataList[i].Area.IsMouseIn = isMouseIn;
            }

            //绘制曲线
            if (IsDrawCurve)
            {
                if (listPoint.Length > 0)
                {
                    g.DrawCurve(linePen, listPoint, Tension);
                }
            }

            if (IsFillPathCurve)
            {
                Point[] point2 = new Point[4];

                if (listPoint.Length > 0)
                {
                    GraphicsPath path = null;
                    point2[0].X = listPoint[dataLength - 1].X;
                    point2[0].Y = listPoint[dataLength - 1].Y;
                    point2[1].X = listPoint[dataLength - 1].X;
                    point2[1].Y = CoordinateStartY;
                    point2[2].X = CoordinateStartX;
                    point2[2].Y = CoordinateStartY;

                    point2[3].X = CoordinateStartX;
                    point2[3].Y = listPoint[0].Y;
                    path = CreatePath(listPoint, point2);
                    Brush fillBrush = new SolidBrush(Color.FromArgb(FillBrushAlpha, LineColor.R, LineColor.G, LineColor.B));
                    g.FillPath(fillBrush, path);
                    fillBrush.Dispose();
                    path.Dispose();
                }
            }
            drawAllsumText(g, AllSumFont, TextBrush, CoordinateStartX, CoordinateStartY, CoordinateWidth, CoordinateHeight);
            ////绘制点
            //for (int i = 0; i < dataLength; i++)
            //{
            //    Point item = listPoint[i];
            //    if (IsDrawDetailData)
            //    {
            //        //ReportViewUtils.drawString(g, LocationModel.Location_Right_Right, CoordinateDataModelBean.Y_Data[i] + "", FontData, DataBrush, StartX, StartY, LeftPadding, padding);
            //        g.DrawString(CoordinateDataModelBean.Y_Data[i] + "", FontData, DataBrush, (item.X), item.Y - 2 * DataSize);
            //    }
            //    g.FillEllipse(TextBrush, item.X - 2, item.Y - 2, 4, 4);
            //}
            AllSumFont.Dispose();
            LableFont.Dispose();

        }


        public override void RefreshDataChange(List<DataModel> list)
        {
            CoordinateModels = list;
            initData(CoordinateModels);
        }

        private void drawAllsumText(Graphics g, System.Drawing.Font LableFont, Brush TextBrush, int CoordinateStartX, int CoordinateStartY, int CoordinateWidth, int CoordinateHeight)
        {
            string str = allNum + LableAllNum;
            switch (DrawAllSumLocation)
            {
                case AllSumLocation.none:
                    break;
                case AllSumLocation.left_up:
                    ReportViewUtils.drawString(g, LocationModel.Location_Left_Left, str, LableFont, TextBrush, CoordinateStartX, CoordinateStartY - CoordinateHeight, CoordinateWidth, padding);
                    break;
                case AllSumLocation.left_down:
                    ReportViewUtils.drawString(g, LocationModel.Location_Left_Left, str, LableFont, TextBrush, CoordinateStartX, CoordinateStartY, CoordinateWidth, padding);
                    break;
                case AllSumLocation.left_center:
                    ReportViewUtils.drawString(g, LocationModel.Location_Left_Left, str, LableFont, TextBrush, CoordinateStartX, CoordinateStartY - CoordinateHeight / 2, CoordinateWidth, padding);
                    break;
                case AllSumLocation.center_up:
                    ReportViewUtils.drawString(g, LocationModel.Location_Center, str, LableFont, TextBrush, CoordinateStartX, CoordinateStartY - CoordinateHeight, CoordinateWidth, padding);
                    break;
                case AllSumLocation.center_down:
                    ReportViewUtils.drawString(g, LocationModel.Location_Center, str, LableFont, TextBrush, CoordinateStartX, CoordinateStartY, CoordinateWidth, padding);
                    break;
                case AllSumLocation.center_center:
                    ReportViewUtils.drawString(g, LocationModel.Location_Center, str, LableFont, TextBrush, CoordinateStartX, CoordinateStartY - CoordinateHeight / 2, CoordinateWidth, padding);
                    break;
                case AllSumLocation.right_up:
                    ReportViewUtils.drawString(g, LocationModel.Location_Right_Right, str, LableFont, TextBrush, CoordinateStartX, CoordinateStartY - CoordinateHeight, CoordinateWidth, padding);
                    break;
                case AllSumLocation.right_down:
                    ReportViewUtils.drawString(g, LocationModel.Location_Right_Right, str, LableFont, TextBrush, CoordinateStartX, CoordinateStartY, CoordinateWidth, padding);
                    break;
                case AllSumLocation.right_center:
                    ReportViewUtils.drawString(g, LocationModel.Location_Right_Right, str, LableFont, TextBrush, CoordinateStartX, CoordinateStartY - CoordinateHeight / 2, CoordinateWidth, padding);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 重缩放
        /// </summary>
        public abstract void ResizePadding();

        /// <summary>
        /// 重新定位数据模型的坐标
        /// 设定areaPositionRect
        /// </summary>
        /// <param name="CenterX">中心点坐标</param>
        /// <param name="CenterY"></param>
        public abstract AreaPositionRect ReLoadAreaPosition(int CenterX, int CenterY, int PaddingX, int PaddingY);


        /// <summary>
        /// 绘制指针
        /// </summary>
        /// <returns></returns>
        private GraphicsPath CreatePath(Point[] points, Point[] point2)
        {
            GraphicsPath roundedRect = new GraphicsPath();
            roundedRect.AddCurve(points, Tension);
            roundedRect.AddLines(point2);
            //roundedRect.AddLine(point0.X, point0.Y, point1.X, point1.Y);
            //roundedRect.AddLine(point1.X, point1.Y, point3.X, point3.Y);
            //roundedRect.AddLine(point3.X, point3.Y, point2.X, point2.Y);
            //roundedRect.AddLine(point2.X, point2.Y, point0.X, point0.Y);
            roundedRect.CloseFigure();
            return roundedRect;
        }

        #region 属性
        /// <summary>
        /// 坐标类型报表实例Bean
        /// </summary>
        public CoordinateDataBean CoordinateDataModelBean { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// X轴标签
        /// </summary>
        public string LableX { get; set; }

        /// <summary>
        /// Y轴标签
        /// </summary>
        public string LableY { get; set; }

        /// <summary>
        /// 数据源
        /// </summary>
        public List<DataModel> CoordinateModels { get; set; }

        /// <summary>
        /// 是否需要对数据进行划分整理
        /// </summary>
        public bool IsNeedDataPartitioning { get; set; }


        /// <summary>
        /// 是否启用辅助线(网格线)(默认启用)
        /// </summary>
        public bool IsEnableGridLine { get; set; }

        /// <summary>
        /// 右边距(距离图形的距离)(默认30)
        /// (IsAutoSize = true 边距将失效, 自动伸缩)
        /// </summary>
        public int RightPadding { get; set; }

        /// <summary>
        /// 左边距(距离图形的距离)(默认0)
        /// (IsAutoSize = true 边距将失效, 自动伸缩)
        /// </summary>
        public int LeftPadding { get; set; }

        /// <summary>
        /// 底边距(距离图形的距离)(默认0)
        /// (IsAutoSize = true 边距将失效, 自动伸缩)
        /// </summary>
        public int BottomPadding { get; set; }

        /// <summary>
        /// 顶边距(距离图形的距离)(默认30)
        /// (IsAutoSize = true 边距将失效, 自动伸缩)
        /// </summary>
        public int TopPadding { get; set; }

        /// <summary>
        /// 字体旋转角度(默认30度)
        /// </summary>
        public float TextRotate { get; set; }

        /// <summary>
        /// 标签字体大小(默认8)
        /// </summary>
        public float LableSize { get; set; }

        /// <summary>
        /// 绘制总数的字体大小(默认10)
        /// </summary>
        public float AllSumSize { get; set; }

        /// <summary>
        /// X轴刻度展示数量(默认10)
        /// </summary>
        public int Count_X { get; set; }

        /// <summary>
        /// Y轴刻度展示数量(默认5)
        /// </summary>
        public int Count_Y { get; set; }

        /// <summary>
        /// 是否展示X刻度(默认展示)
        /// </summary>
        public bool IsShowX_Scale { get; set; }

        /// <summary>
        /// 是否展示Y刻度(默认展示)
        /// </summary>
        public bool IsShowY_Scale { get; set; }

        /// <summary>
        /// 是否在坐标点上绘制详细的信息(默认true)
        /// </summary>
        public bool IsDrawDetailData { get; set; }

        /// <summary>
        /// 曲线扭曲程度(默认0.5)
        /// </summary>
        public float Tension { get; set; }

        /// <summary>
        /// 是否绘制曲线(默认是)
        /// </summary>
        public bool IsDrawCurve { get; set; }

        /// <summary>
        /// 是否向下填充颜色(默认false)
        /// </summary>
        public bool IsFillPathCurve { get; set; }

        /// <summary>
        /// 向下填充的颜色的透明度(默认30)
        /// </summary>
        public int FillBrushAlpha { get; set; }


        /// <summary>
        /// 默认绘制总数, 只是绘制点位置none不显示, 自己选择位置显示
        /// </summary>
        public AllSumLocation DrawAllSumLocation { get; set; }

        /// <summary>
        /// 数量统计的单位(默认 个)
        /// </summary>
        public string LableAllNum { get; set; }

        /// <summary>
        /// 坐标的宽度
        /// </summary>
        protected int CoordinateWidth;

        /// <summary>
        /// 坐标的高度
        /// </summary>
        public int CoordinateHeight { get; set; }

        /// <summary>
        /// 坐标的起始位置
        /// </summary>
        public int CoordinateStartX { get; set; }

        /// <summary>
        /// 坐标的起始Y
        /// </summary>
        public int CoordinateStartY { get; set; }

        /// <summary>
        /// 是否是矩形图形(默认false)
        /// 如果是, X的标签会自动偏移
        /// </summary>
        protected bool IsRectAngle { get; set; }

        /// <summary>
        /// 是否对标签字体加粗(默认false)
        /// </summary>
        public bool IsLableFontBold { get; set; }

        /// <summary>
        /// 对于横坐标进行整合,把几个X的模型看做为1个
        /// </summary>
        public int ConformCount { get; set; }

        /// <summary>
        /// 对X轴的名称进行偏移
        /// </summary>
        public int OffsetX { get; set; }

        /// <summary>
        /// 所有数据的最大数据值(默认100)
        /// </summary>
        public float MaxNum { get; set; }

        public Animalion MyAnimalion
        {
            get
            {
                return animalion;
            }
            set
            {
                this.animalion = value;
            }
        }

        /// <summary>
        /// 用于标定Y刻度的间隔
        /// </summary>
        public float PerMax { get; set; }

        /// <summary>
        /// 是否绘制箭头 默认false
        /// </summary>
        public bool IsEnableArrow { get; set; }

        #endregion

    }
}
