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
    public abstract class CoordinateMultiOverlapModelsReportView : SelfDefineReportView
    {

        public CoordinateMultiOverlapModelsReportView()
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
            IsShowLegendnote = true;
            LegendNoteWidth = 30;
            LegendNoteHeight = 20;
            Titles = new List<string>();
        }

        public void initData()
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
            if (CoordinateModels != null)
            {
                CoordinateDataModelBean.CoordinateModelList = CoordinateModels;
            }
            else
            {
                List<DataModel> Models = new List<DataModel>();
                Models.Add(new DataModel("时间01", 50));
                Models.Add(new DataModel("时间02", 100));
                Models.Add(new DataModel("时间03", 150));
                Models.Add(new DataModel("时间04", 200));
                Models.Add(new DataModel("时间05", 200));
                Models.Add(new DataModel("时间06", 200));
                Models.Add(new DataModel("时间07", 200));
                addDifferentModels(Models);
                Models = new List<DataModel>();
                Models.Add(new DataModel("时间01", 80));
                Models.Add(new DataModel("时间02", 10));
                Models.Add(new DataModel("时间03", 50));
                Models.Add(new DataModel("时间04", 20));
                Models.Add(new DataModel("时间05", 220));
                Models.Add(new DataModel("时间06", 10));
                Models.Add(new DataModel("时间07", 230));
                addDifferentModels(Models);
                Models = new List<DataModel>();
                Models.Add(new DataModel("时间01", 180));
                Models.Add(new DataModel("时间02", 110));
                Models.Add(new DataModel("时间03", 150));
                Models.Add(new DataModel("时间04", 120));
                Models.Add(new DataModel("时间05", 80));
                Models.Add(new DataModel("时间06", 50));
                Models.Add(new DataModel("时间07", 30));
                addDifferentModels(Models);
                CoordinateDataModelBean.CoordinateModelList = CoordinateModels;
            }

            CoordinateDataModelBean.initXYData();
            //计算数据之和以及设定最大数据
            //AllNum = 0;
            MaxNum = ReportViewUtils.getMaxNumFromData(CoordinateDataModelBean.Y_Data);
            //CalculateAllNum();
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

            if (IsHorizontally)
            {
                if (EViewWidth != 0)
                {
                    EStartX = this.Width / 2 - EViewWidth / 2;
                    EStartY = this.Height / 2 - EViewHeight / 2;
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


        public override void ResizeReportViewChange()
        {

            EViewWidth = Width / 2;
            EViewHeight = 2 * this.Height / 3;
            TextSize = Width * 1.0f / 40;
            padding = EViewWidth / 20;
            LegendNoteWidth = (Width - EViewWidth) / 2 / 6;
            LegendNoteHeight = LegendNoteWidth / 2;
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
            Font LableFont = new Font("Arial", LableSize);
            Font AllSumFont = new Font("Arial", AllSumSize);
            

            //绘制横坐标和标题
            ReportViewUtils.drawString(g, Title, FontText, TextBrush, EStartX, EStartY, EViewWidth, TopPadding);
            g.DrawLine(linePen, CoordinateStartX, CoordinateStartY, CoordinateStartX + CoordinateWidth, CoordinateStartY);
            if (!IsEnableGridLine)
            {
                g.DrawLine(linePen, CoordinateStartX + CoordinateWidth, CoordinateStartY, CoordinateStartX + CoordinateWidth - 6, CoordinateStartY + 3);
                g.DrawLine(linePen, CoordinateStartX + CoordinateWidth, CoordinateStartY, CoordinateStartX + CoordinateWidth - 6, CoordinateStartY - 3);
            }

            // 绘制围绕点旋转的文本  
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;
            //绘制标签
            DrawRoateText.DrawString(g, LableX, LableFont, TextBrush, new PointF(CoordinateStartX + CoordinateWidth + padding, CoordinateStartY + padding), format, TextRotate);


            //数据源长度
            int dataLength = 0;
            if (CoordinateModels != null)
            {
                dataLength = CoordinateModels.Count / (ModelCount);
            }

            Point[] listPoint = new Point[dataLength];
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
                            DrawRoateText.DrawString(g, CoordinateDataModelBean.X_Data[i], LableFont, TextBrush, new PointF(CoordinateStartX + i * per_X, CoordinateStartY + padding), format, TextRotate);
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
                g.DrawLine(linePen, CoordinateStartX + 3, CoordinateStartY - CoordinateHeight + 6, CoordinateStartX, CoordinateStartY - CoordinateHeight);
                g.DrawLine(linePen, CoordinateStartX - 3, CoordinateStartY - CoordinateHeight + 6, CoordinateStartX, CoordinateStartY - CoordinateHeight);
            }
            //绘制标签
            int lableLength = CoordinateDataModelBean.Lable_Y.Length;
            ReportViewUtils.drawString(g, LocationModel.Location_Right_Right, LableY, LableFont, TextBrush, EStartX, CoordinateStartY - CoordinateHeight - padding, LeftPadding - 10, padding);
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
                    ReportViewUtils.drawString(g, LocationModel.Location_Right_Right, perNum * i + "", LableFont, TextBrush, EStartX, CoordinateStartY - i * per_Y - padding / 2, LeftPadding - 10, padding);
                    //g.DrawString(perNum * i + "", LableFont, TextBrush, (padding - lableSize * 3) / 2 + startX, height - padding - i * per_Y - lableSize - bottomPadding);
                }
                int y = (int)(CoordinateStartY - i * per_Y);
                g.DrawLine(linePen, CoordinateStartX, y, CoordinateStartX + CoordinateWidth, y);
            }
            linePen.Color = LineColor;



            for (int j = 0; j < ModelCount; j++)
            {
                linePen.Color = CoordinateDataModelBean.CoordinateModelList[dataLength * j].ModelColor;
                //计算数据中的点对应到坐标的点
                bool isMouseIn = false;
                for (int i = 0; i < dataLength; i++)
                {
                    float dataY = CoordinateDataModelBean.Y_Data[i + dataLength * j];
                    //把数据对应到实际的坐标
                    int posY = (int)(CoordinateStartY - dataY * per_Y / perNum);
                    listPoint[i].Y = posY;
                    //重新定位数据模型的坐标
                    isMouseIn = adapter.DataList[i + dataLength * j].Area.IsMouseIn;
                    adapter.DataList[i + dataLength * j].Area = ReLoadAreaPosition(listPoint[i].X, listPoint[i].Y, (int)per_X, (int)per_Y);
                    //adapter.DataList[i].Area = new AreaPositionRect(listPoint[i].X, listPoint[i].Y, listPoint[i].X, listPoint[i].Y);
                    adapter.DataList[i + dataLength * j].Area.IsMouseIn = isMouseIn;
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
                        Brush fillBrush = new SolidBrush(Color.FromArgb(FillBrushAlpha, linePen.Color.R, linePen.Color.G, linePen.Color.B));
                        g.FillPath(fillBrush, path);
                        fillBrush.Dispose();
                        path.Dispose();
                    }
                }
                if (IsShowLegendnote)
                {
                    CalculateAllNum(dataLength, j);
                    ReportViewUtils.drawString(g, LocationModel.Location_Left_Left, Titles[j] + allNum + LableAllNum, LableFont, TextBrush, CoordinateStartX + CoordinateWidth + ((Width - EViewWidth) / 4 + LegendNoteWidth + 10), CoordinateStartY - CoordinateHeight + (LegendNoteHeight + padding) * j, (Width - EViewWidth) / 2, LegendNoteHeight + padding);
                    Rectangle rect = new Rectangle(CoordinateStartX + CoordinateWidth + ((Width - EViewWidth) / 2 - LegendNoteWidth) / 2, CoordinateStartY - CoordinateHeight + (LegendNoteHeight + padding) * (j) + padding / 2, LegendNoteWidth, LegendNoteHeight);
                    GraphicsPath path = ReportViewUtils.CreateRoundedRectanglePath(rect, LegendNoteHeight / 2);
                    Brush bbsh = new SolidBrush(linePen.Color);
                    g.FillPath(bbsh, path);
                    bbsh.Dispose();
                    path.Dispose();
                }
               //drawAllsumText(g, AllSumFont, TextBrush, CoordinateStartX, CoordinateStartY, CoordinateWidth, CoordinateHeight);
            }
            AllSumFont.Dispose();
            LableFont.Dispose();

        }

        private void CalculateAllNum(int length, int startX)
        {
            allNum = 0;
            float[] data = CoordinateDataModelBean.Y_Data;
            for (int i = startX * length; i < data.Length; i++)
            {
                allNum += data[i];
            }
        }

        /// <summary>
        /// 设定多组数据模型的概念, 颜色由PerferColors决定
        /// </summary>
        /// <param name="ModelList"></param>
        public void addDifferentModels(List<DataModel> ModelList)
        {
            addDifferentModels(ModelList, "");
        }

        /// <summary>
        /// 设定多组数据模型的概念, 颜色由PerferColors决定
        /// </summary>
        /// <param name="ModelList"></param>
        public void addDifferentModels(List<DataModel> ModelList, string title)
        {
            Titles.Add(title);
            if (CoordinateModels == null)
            {
                CoordinateModels = new List<DataModel>();
            }
            foreach (DataModel item in ModelList)
            {
                item.ModelColor = ReportViewUtils.PerferColors2[ModelCount % 5];
                CoordinateModels.Add(item);
            }
            ModelCount++;

        }

        public override void RefreshDataChange(List<DataModel> list)
        {
            CoordinateModels = list;
            initData();
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
        /// 数量统计的单位(默认 个)
        /// </summary>
        public string LableAllNum { get; set; }

        /// <summary>
        /// 是否绘制注解图例
        /// </summary>
        public bool IsShowLegendnote { get; set; }

        /// <summary>
        /// 注解图例宽高(默认30 * 20)
        /// </summary>
        public int LegendNoteWidth { get; set; }


        /// <summary>
        /// 注解图例宽高(默认30 * 20)
        /// </summary>
        public int LegendNoteHeight { get; set; }

        /// <summary>
        /// 多模型的标题
        /// </summary>
        public List<string> Titles { get; set; }

        /// <summary>
        /// 坐标的宽度
        /// </summary>
        public int CoordinateWidth { get; set; }

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
        /// 模型数量
        /// </summary>
        protected int ModelCount { get; set; }

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
    }
}
