using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ReportFormDesign.Adapter;
using ReportFormDesign.Animals;
using ReportFormDesign.DataModels;
using ReportFormDesign.DrawUtils;
using ReportFormDesign.Model;

namespace ReportFormDesign.ReportViewPanel
{

    /// <summary>
    /// 适合一个panel对应一个数据模型的报表
    /// 数据模型选用DetailDataModel
    /// </summary>
    public abstract class SingleReportView : ViewPanel, IAnimal
    {

        //当前动画绘制的模型
        private DetailDataModel CurrentDrawAnimalModel;
        //动画准备的数据
        private Object[] PrePareArgs;


        //动画绘制的专业pen, myBrush, myFont
        protected Pen myAnimalPen;
        protected Brush myAnimalBrush;
        protected Font myAnimalFont;
        //自动检测次数
        protected int CheckTimes = 1;
        //计数器(通过不同的插值器的Values影响index的计数,从而实现动画效果的加速减速等等)
        protected float Index;
        //是否绘制注解图例(默认绘制)
        protected bool isDrawLegendNote;
        //是否把注解绘制在图例之内
        protected bool isDrawTextInLegendNote;
        //是否启用默认的图例缩放(位置没有改变, 如果位置需要改变请设置false)(注意一定要有注解图例的数据(LegendNoteModels != null), 否则无效)
        //默认是
        //如果设置false,但是还是想使用默认的外加自己的, 可以直接调用Default...方法加上自己的
        //一般没有注解图例要绘制的, 请在DefaultResizeReportView方法中自己缩放
        protected bool isAutoResizeLegendNote;
        //注解
        protected LegendNoteModel[] LegendNoteModels;


        //报表的数据源(奇数位为数据值, 偶数位为数据名称)
        public string[] TextAndData;

        public SingleReportView()
        {
            TextLimiteCount = 10;
            _MyAnimalion = new SimpleAnimalion();
            this._MyAnimalion.AnimalInterface = this;
            MyAnimalColor = Color.White;
            _Interpolation = new LinearInterpolation();
            LegendNoteUpDownPadding = 5;
            LegendNoteFontTextSize = 8;
            MaxIndex = 1000;
            IsHorizontally = true;
            isDrawLegendNote = true;
            //IsAutoUpdateData = false;
            MaxNum = 100;
            AnimalionTextSize = 9;
            isAutoResizeLegendNote = true;
            isSelfDefineReportView = true;
            LegengNoteTextLimiteCount = 10;
        }

        /// 初始化数据
        public void initData()
        {
            List<DataModel> list = new List<DataModel>();
            if (Title == null)
            {
                Title = "报表";
            }
            if (TextAndData == null)
            {
                TextAndData = new string[] { "数据一", "20", "数据二", "30", "数据三", "40" };
            }
            //计算数据之和以及设定最大数据
            allNum = 0;
            CalculateAllNum();
            //CalculateAllNum();
            if (IsDescendingOrder)
            {
                TextAndData = ReportViewUtils.SortTextAndData(TextAndData);
            }
            DataModel model = new DetailDataModel(Title, 0, TextAndData);
            if (IsHorizontally)
            {
                EStartX = this.Width / 2 - EViewWidth / 2;
                EStartY = this.Height / 2 - EViewHeight / 2;
            }
            model.Area = new AreaPositionRect(EStartX, EStartY, EViewWidth + EStartX, EViewHeight + EStartY);
            list.Add(model);

            //添加图形注解,用于解释该图形代表什么
            LegendNoteModels = GetLegendNotes();
            //添加特殊的鼠标响应区域, 注意数据模型必须是坐标已赋值的
            DataModel[] datamodels = LegendNoteModels;
            if (datamodels != null)
            {
                foreach (DataModel item in datamodels)
                {
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            ReportViewAdapter adapter = new SingleReportViewAdapter();
            //设置数据源
            adapter.setData(list);
            adapter.IsVerticalShowData = false;
            setAdapter(adapter);
            AutoResizeWithHeightAndPos();
        }

        #region SizeChange
        /// <summary>
        /// 当Panel缩放时 如果设置自动缩放, 则图形会自适应
        /// </summary>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            AutoResizeWithHeightAndPos();
            //避免异常
            if (StrokenWidth <= 1)
            {
                StrokenWidth = 1;
            }
            if (LegendNoteFontTextSize <= 1)
            {
                LegendNoteFontTextSize = 1;
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

        private void AutoResizeWithHeightAndPos()
        {
            //重缩放图形位置以及大小
            DefaultResizeReportView();
            //重缩放注解位置以及大小
            DefaultLegendNoteResizeChanged();
            //重定位提示信息位置
            ReLocationtoolTipPos();
        }

        /// <summary>
        /// 重定位toolTip位置
        /// </summary>
        protected void ReLocationtoolTipPos()
        {
            //不设定, 它的toolTip将失效
            if (CurrentDataModel == null)
            {
                if (adapter != null)
                {
                    CurrentDataModel = adapter.DataList[0];
                }
            }
            else
            {
                if (!(CurrentDataModel is DetailDataModel))
                {
                    if (adapter != null)
                    {
                        CurrentDataModel = adapter.DataList[0];
                    }
                }
                CurrentDataModel.Area.left = EStartX;
                CurrentDataModel.Area.right = EStartX + EViewWidth;
                CurrentDataModel.Area.top = EStartY;
                CurrentDataModel.Area.bottom = EStartY + EViewHeight;
            }
        }

        /// <summary>
        /// 默认的报表缩放
        /// </summary>
        protected void DefaultResizeReportView()
        {
            if (IsAutoReSizeView)
            {
                int resize = Math.Min(Width, Height);
                EViewWidth = 2 * resize / 3;
                EViewHeight = EViewWidth;
                TextSize = EViewWidth * 1.0f / 12;
                DataSize = EViewWidth * 1.0f / 14;
                ResizeReportViewChange();
            }
            if (IsHorizontally)
            {
                EStartX = this.Width / 2 - EViewWidth / 2;
                EStartY = this.Height / 2 - EViewHeight / 2;
            }
            if (OnChangeCenterLocationEvent != null)
            {
                Point pos = OnChangeCenterLocationEvent();
                if (pos != null)
                {
                    EStartX = pos.X - EViewWidth / 2;
                    EStartY = pos.Y - EViewHeight / 2;
                }
            }
        }

        /// <summary>
        /// 图例的默认缩放
        /// </summary>
        private void DefaultLegendNoteResizeChanged()
        {
            if (isAutoResizeLegendNote)
            {
                if (isDrawLegendNote && TextAndData != null)
                {
                    LegendNoteUpDownPadding = EViewWidth / 20;
                    LegendNoteWidth = EViewWidth / 6;
                    LegendNoteHeight = EViewHeight / 2 / (TextAndData.Length / 2) - LegendNoteUpDownPadding;
                }
            }
            else
            {
                if (LegendNoteModels != null)
                {
                    LegendNoteSizeChanged();
                }
            }
        }
        #endregion

        public override void DrawSelfDefineView(Graphics g, Pen linePen, Brush lineBrush, Brush TextBrush, Brush DataBrush, System.Drawing.Font FontText, System.Drawing.Font FontData)
        {
        }

        public override void childPaint(Graphics g, DataModel data, Pen linePen, Brush lineBrush, Brush TextBrush, Brush DataBrush, Font FontText, Font FontData)
        {
            //图形的绘制
            if (TextAndData != null && data is DetailDataModel)
            {
                OnRenderNormalView(g, data, linePen, lineBrush, TextBrush, DataBrush, FontText, FontData);
            }

            //注解的绘制
            if (isDrawLegendNote && LegendNoteModels != null)
            {
                foreach (LegendNoteModel item in LegendNoteModels)
                {
                    Brush selfBrush = new SolidBrush(item.Color);
                    Font LegendNoteFont = new Font("幼圆", LegendNoteFontTextSize);
                    if (item.Area.Height / 4 <= 1)
                    {
                        item.Area.Height = 4;
                    }
                    GraphicsPath path = ReportViewUtils.CreateRoundedRectanglePath(new Rectangle(item.Area.left, item.Area.top, item.Area.Width, item.Area.Height), item.Area.Height / 4);
                    g.FillPath(selfBrush, path);
                    if (isDrawTextInLegendNote)
                    {
                        ReportViewUtils.drawStringWithLimiteText(g, LocationModel.Location_Center, item.mainText, LegendNoteFont, TextBrush, item.Area.left, item.Area.top, item.Area.Width, item.Area.Height, LegengNoteTextLimiteCount);
                    }
                    else
                    {
                        ReportViewUtils.drawStringWithLimiteText(g, LocationModel.Location_Left_Left, item.mainText, LegendNoteFont, TextBrush, item.Area.right + LegendNoteUpDownPadding, item.Area.top, LegendNoteWidth, LegendNoteHeight, LegengNoteTextLimiteCount);
                    }
                    selfBrush.Dispose();
                    LegendNoteFont.Dispose();
                    path.Dispose();
                }
            }
        }

        #region 动画的绘制
        public void PrepareAnimalion()
        {
            if (MyAnimalColor == null)
            {
                MyAnimalColor = Color.White;
            }
            myAnimalBrush = new SolidBrush(MyAnimalColor);
            myAnimalPen = new Pen(MyAnimalColor, 1.0f);
            myAnimalFont = new Font("Consolas", AnimalionTextSize);
            if (_MyAnimalion.IsPrepareAnimaled)
            {
                //刷新数据
                if (OnNotifyAnimalDataChageEvent != null)
                {
                    TextAndData = OnNotifyAnimalDataChageEvent();
                    initData();
                    CheckTimes++;
                }
                //新的一天
                if (DateTime.Now.Hour >= 23 && DateTime.Now.Minute >= 59)
                {
                    CheckTimes = 1;
                }
            }
            if (adapter != null)
            {
                if (!(adapter.DataList[0] is DetailDataModel))
                {
                    throw new ArgumentException("数据源模型不是DetailModel类型");
                }
                CurrentDrawAnimalModel = adapter.DataList[0] as DetailDataModel;
            }
            PrePareArgs = AnimalionPrepare(CurrentDrawAnimalModel);
            allNum = 0;
            if (TextAndData != null)
            {
                CalculateAllNum();
            }
            Index = 0;
        }

        public void StartAnimalion()
        {
            if (TextAndData != null)
            {
                AnimalionDraw(graphics, CurrentDrawAnimalModel, myAnimalPen, myAnimalBrush, myAnimalFont, PrePareArgs);
            }
            if (Index < MaxIndex)
            {
                Index += _Interpolation.GetInterpolationValue();
            }
        }

        public void EndAnimation()
        {
            AnimalionEnd(graphics, CurrentDrawAnimalModel, myAnimalPen, myAnimalBrush, myAnimalFont);
            myAnimalFont.Dispose();
            myAnimalBrush.Dispose();
            myAnimalPen.Dispose();
        }
        #endregion

        /// 计算最大数值
        private void CalculateAllNum()
        {
            for (int i = 0; i < TextAndData.Length / 2; i++)
            {
                string dd = TextAndData[2 * i + 1];
                int cd = 0;
                if (dd == "" || dd == " ")
                {
                    dd = "0";
                }
                cd = int.Parse(dd);
                allNum += cd;
                if (MaxNum == 100)
                {
                    if (MaxNum < cd)
                    {
                        MaxNum = cd;
                    }
                }
            }
        }

        //报表大小变化时报表的变化(在设定IsEnableDefaultResizeReportView为false时触发)
        public abstract void ResizeReportViewChange();

        //报表大小变化时图例注解的变化(在设定IsEnableDefaultResizeLegendNote为false时触发)
        public abstract void LegendNoteSizeChanged();

        //子类绘制
        public abstract void OnRenderNormalView(Graphics g, DataModel data, Pen linePen, Brush lineBrush, Brush TextBrush, Brush DataBrush, Font TextFont, Font DataFont);

        //准备动画(数据的初始化)
        public abstract Object[] AnimalionPrepare(DetailDataModel drawModel);

        ///动画的绘制
        //<param name="args">动画准备的数据</param>
        public abstract void AnimalionDraw(Graphics g, DetailDataModel data, Pen pen, Brush brush, Font font, Object[] args);

        /// 动画绘制结束
        public abstract void AnimalionEnd(Graphics g, DetailDataModel data, Pen pen, Brush brush, Font font);

        /// 图形注解说明
        public abstract LegendNoteModel[] GetLegendNotes();

        /// 中心位置信息改变事件
        public event OnChangeCenterLocation OnChangeCenterLocationEvent;

        /// 刷新数据时的委托事件
        public event OnNotifyAnimalDataChage OnNotifyAnimalDataChageEvent;

        #region 存根属性
        public ReportViewToolTip _MyToolTip
        {
            get
            {
                return toolTip;
            }
            set
            {
                this.toolTip = value;
            }
        }
        public int AutoUpdateTime
        {
            get
            {
                return autoUpdateTime;
            }
            set
            {
                this.autoUpdateTime = value;
            }
        }
        public bool IsNotAllowShowAnimalion
        {
            get
            {
                return isNotAllowShowAnimalion;
            }
            set
            {
                this.isNotAllowShowAnimalion = value;
            }
        }
        public bool IsNeedReLocationToopTip
        {
            get
            {
                return isNeedReLocationToopTip;
            }
            set
            {
                this.isNeedReLocationToopTip = value;
            }
        }
        public Animalion _MyAnimalion
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
        /// 注解字体限制(默认10)
        /// </summary>
        public int LegengNoteTextLimiteCount { get; set; }

        /// <summary>
        /// 所有数据的最大数据值(默认100)
        /// </summary>
        public float MaxNum { get; set; }

        /// <summary>
        /// 绘制动画时的颜色
        /// </summary>
        public Color MyAnimalColor { get; set; }

        /// <summary>
        /// 报表标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 横线上绘制字体数量的限制(默认10)
        /// </summary>
        public int TextLimiteCount { get; set; }

        /// <summary>
        /// 最大计数值(默认1000)
        /// </summary>
        public float MaxIndex { get; set; }

        /// <summary>
        /// 插值器
        /// </summary>
        public AnimalInterpolation _Interpolation { get; set; }

        /// <summary>
        /// 注解的宽度
        /// </summary>
        public int LegendNoteWidth { get; set; }

        /// <summary>
        /// 注解的高度
        /// </summary>
        public int LegendNoteHeight { get; set; }

        /// <summary>
        /// 注解之间的间距(默认5)
        /// </summary>
        public int LegendNoteUpDownPadding { get; set; }

        /// <summary>
        /// 注解描述的字体大小(默认8)
        /// </summary>
        public float LegendNoteFontTextSize { get; set; }

        /// <summary>
        /// 是否对数据进行降序排序
        /// </summary>
        public bool IsDescendingOrder { get; set; }

        /// <summary>
        /// 绘制动画专用的字体大小(默认9)
        /// </summary>
        public float AnimalionTextSize { get; set; }
        #endregion
    }

    /// <summary>
    /// 中心位置改变或窗体缩放重新点位中心点
    /// </summary>
    public delegate Point OnChangeCenterLocation();

    /// <summary>
    /// 数据刷新操作
    /// </summary>
    public delegate string[] OnNotifyAnimalDataChage();
}
