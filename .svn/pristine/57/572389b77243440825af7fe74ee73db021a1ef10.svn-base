using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Threading;
using ReportFormDesign.Animals;
using ReportFormDesign.DrawUtils;
using ReportFormDesign.Legendnotes;
using ReportFormDesign.Model;

namespace ReportFormDesign.ReportViewPanel.SelfDefineReportView
{
    public abstract class SelfDefineReportView : ViewPanel, IAnimal
    {
        //注解相关
        protected bool IsShowLegendnote;
        protected int LegendNoteWidth;
        protected int LegendNoteHeight;
        //动画准备的数据
        private Object[] AnimalionPrePareArgs;
        //插值器
        protected InterfaceLegendnote interfaceLegendnote;
        //计数器(通过不同的插值器的Values影响index的计数,从而实现动画效果的加速减速等等)
        protected float Index;
        //最大计数值(默认1000)
        protected float MaxIndex;
        //自动或手动检测次数
        protected int CheckTimes;

        public SelfDefineReportView()
        {
            //动画的配置
            //animalion = new SimpleAnimalion();
            //this.animalion.AnimalInterface = this;
            //_Interpolation = new LinearInterpolation();
            isSelfDefineReportView = true;
            MaxIndex = 1000;
        }
        

        /// <summary>
        /// 常规的绘制(比如坐标线)
        /// </summary>
        public override void DrawSelfDefineView(Graphics g, Pen linePen, Brush lineBrush, Brush TextBrush, Brush DataBrush, System.Drawing.Font FontText, System.Drawing.Font FontData)
        {
            DrawSelfDefine( g,  linePen,  lineBrush,  TextBrush,  DataBrush, FontText, FontData);
            if (interfaceLegendnote != null)
            {
                interfaceLegendnote.drawLegendnote(g, linePen, lineBrush, TextBrush, DataBrush, FontText, FontData);
            }
        }

        /// <summary>
        /// 每个模型的绘制
        /// </summary>
        public override void childPaint(System.Drawing.Graphics g, Model.DataModel data, System.Drawing.Pen linePen, System.Drawing.Brush lineBrush, System.Drawing.Brush TextBrush, System.Drawing.Brush DataBrush, System.Drawing.Font FontText, System.Drawing.Font FontData)
        {
            if (animalion != null)
            {
                OnRenderNormalView(g, data, linePen, lineBrush, TextBrush, DataBrush, FontText, FontData);
            }
            else
            {
                if (IsShowViewByAnimalioned)
                {
                    Index = MaxIndex;
                    OnRenderAnimalionedView(g, data, linePen, lineBrush, TextBrush, DataBrush, FontText, FontData);
                }
                else
                {
                    OnRenderNormalView(g, data, linePen, lineBrush, TextBrush, DataBrush, FontText, FontData);
                }
            }
            if (IsShowLegendnote)
            {
                //ReportViewUtils.drawString(g, LocationModel.Location_Left_Left, Titles[j] + AllNum + LableAllNum, LableFont, TextBrush, CoordinateStartX + CoordinateWidth + ((Width - EViewWidth) / 4 + LegendNoteWidth + 10), CoordinateStartY - CoordinateHeight + (LegendNoteHeight + padding) * j, (Width - EViewWidth) / 2, LegendNoteHeight + padding);
                //Rectangle rect = new Rectangle(EStartX + EViewWidth + (RightPadding - LegendNoteWidth) / 2, TopPadding + (LegendNoteHeight + padding) * (j) + padding / 2, LegendNoteWidth, LegendNoteHeight);
                //GraphicsPath path = ReportViewUtils.CreateRoundedRectanglePath(rect, LegendNoteHeight / 2);
                //Brush bbsh = new SolidBrush(linePen.Color);
                //g.FillPath(bbsh, path);
                //bbsh.Dispose();
                //path.Dispose();
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (IsAutoReSizeView)
            {
                ResizeReportViewChange();
            }
            if (IsHorizontally)
            {
                ReLocationModelPos();
            }
            this.Invalidate();
        }

        #region 动画相关
        public void PrepareAnimalion()
        {
            //刷新数据
            if (OnNotifyAnimalDataChangeEvent != null)
            {
                RefreshDataChange(OnNotifyAnimalDataChangeEvent());
                CheckTimes++;
            }
            //新的一天
            if (DateTime.Now.Hour > 22 && DateTime.Now.Minute >= 59)
            {
                CheckTimes = 1;
            }
            AnimalionPrePareArgs = AnimalionPrepare();
            Index = 0;
        }

        public void StartAnimalion()
        {
            //动画绘制
            AnimalionDraw(graphics, AnimalionPrePareArgs);
            this.Invalidate();
            if (Index < MaxIndex)
            {
                Index += _Interpolation.GetInterpolationValue();
            }
        }

        public void EndAnimation()
        {
            AnimalionEnd(graphics);
        }
        #endregion

        #region 子类自定义缩放
        /// <summary>
        /// 自定义缩放大小
        /// </summary>
        public abstract void ResizeReportViewChange();

        /// <summary>
        /// 重定位View的CurrentDataModel.Area.left, right, top, bottom;
        /// </summary>
        public abstract void ReLocationModelPos();
        #endregion

        #region 子类自定义绘制
        /// <summary>
        /// 自定义绘制(比如坐标线)
        /// </summary>
        public abstract void DrawSelfDefine(Graphics g, Pen linePen, Brush lineBrush, Brush TextBrush, Brush DataBrush, System.Drawing.Font FontText, System.Drawing.Font FontData);

        /// <summary>
        /// 子类绘制, 普通的展示(可以通过animal.IsAnimaing来控制这个是否执行部分动画, 或者只是执行完全的动画)
        /// </summary>
        public abstract void OnRenderNormalView(Graphics g, DataModel data, Pen linePen, Brush lineBrush, Brush TextBrush, Brush DataBrush, Font TextFont, Font DataFont);

        #endregion

        #region 子类自定义刷新
        /// <summary>
        /// 数据刷新过来的数据源, 具体如何更新, 自己操作
        /// </summary>
        /// <param name="list"></param>
        public abstract void RefreshDataChange(List<DataModel> list);
        #endregion

        #region 子类自定义动画
        /// <summary>
        /// 以动画结束时的样子来绘制(如果IsShowViewByAnimalioned为true)
        /// </summary>
        public abstract void OnRenderAnimalionedView(Graphics g, DataModel data, Pen linePen, Brush lineBrush, Brush TextBrush, Brush DataBrush, System.Drawing.Font FontText, System.Drawing.Font FontData);

        public abstract object[] AnimalionPrepare();

        public abstract void AnimalionDraw(Graphics graphics, object[] AnimalionPrePareArgs);

        public abstract void AnimalionEnd(Graphics graphics);
        #endregion


        /// <summary>
        /// 是否以动画的终态来展示
        /// </summary>
        public bool IsShowViewByAnimalioned { get; set; }

        /// <summary>
        /// 插值器
        /// </summary>
        public AnimalInterpolation _Interpolation { get; set; }

        /// <summary>
        /// 刷新数据时的委托事件
        /// </summary>
        public event OnNotifyAnimalDataChange OnNotifyAnimalDataChangeEvent;

        #region 图像边距
        /// <summary>
        /// 如果设定IsAutoResize, 则边距设定无效
        /// </summary>
        public int LeftPadding { get; set; }
        public int TopPadding { get; set; }
        public int RightPadding { get; set; }
        public int BottomPadding { get; set; }
        #endregion

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

        public ReportViewToolTip MyToolTip
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
    }

    /// <summary>
    /// 数据刷新操作
    /// </summary>
    public delegate List<DataModel> OnNotifyAnimalDataChange();
}
