/* ======================================================================== 
* 本类功能概述: 抽象View基础绘制(不允许任何非扩展性字段会方法)
* 
* 作者：zjm 
* 时间：2018/1/13 10:17:16 
* 文件名：AbstractReportView 
* 版本：V1.0.1 
* 
* 修改者： 时间： 
* 修改说明： 
* ======================================================================== 
*/
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using FishyuSelfControl.FishYuReportView.ToolTips;
using FishyuAnimation.Animations;
using System.Drawing.Drawing2D;

namespace FishyuSelfControl.FishYuReportView
{
    public  partial class AbstractReportView : UserControl
    {

        // 画笔
        private Pen _linePen;
        // 画刷
        private Brush _lineBrush;

        private static bool isDesignMode = FinshYuUtils.CommonUtils.DesignModeUtil.Instance.IsDesignMode;

        #region protected

        #region StaticColors
        // 浅蓝色
        public static Color perferShallowBlue = Color.FromArgb(40, 36, 169, 255);
        // 蓝色
        public static Color perferBlue = Color.FromArgb(255, 36, 169, 255);
        // 粉色
        public static Color perferPink = Color.FromArgb(255, 244, 13, 100);
        // 浅粉色
        public static Color perferShallowPink = Color.FromArgb(255, 100, 30, 101);
        // 红色
        public static Color perferRed = Color.FromArgb(255, 244, 0, 0);
        // 白色
        public static Color perferWhite = Color.White;
        // 深绿色
        public static Color perferGreen = Color.FromArgb(255, 64, 116, 52);
        // 黄色
        public static Color perferYellow = Color.FromArgb(255, 244, 208, 0);
        // 棕色
        public static Color perferBrown = Color.FromArgb(255, 34, 8, 7);
        // 紫色
        public static Color perferPurple = Color.FromArgb(255, 175, 18, 88);
        // 浅紫色
        public static Color perferWhite_Shallow = Color.FromArgb(255, 201, 207, 202);
        // 深蓝色
        public static Color perferBlue_Deep = Color.FromArgb(255, 3, 22, 52);
        // 浅灰色
        public static Color perferShallowGray = Color.FromArgb(20, 210, 210, 210);
        // 浅灰色2
        public static Color perferShallowGray2 = Color.FromArgb(60, 210, 210, 210);
        public static Color perferShallowGray3 = Color.FromArgb(180, 210, 210, 210);

        public static Color[] PerferColors = { perferBlue, perferYellow, perferRed, perferPurple, perferPink, perferBrown };

        public static Color[] PerferColors2 = { Color.FromArgb(255, 132, 202, 206), Color.FromArgb(255, 197, 186, 222), perferBlue, perferRed, perferPurple };
        #endregion

        // 动画配置
        protected Animation _animation;
        // 鼠标移动时触发提示tips
        protected IToolTips _toolTips;
        // 子类绘制
        protected IView _iView;
        #endregion

        #region 属性
        // 画笔的粗细
        protected float _strokenWidth = 1.0f;
        /// <summary>
        /// 画笔的粗细
        /// </summary>
        [Description("画笔的粗细"), Browsable(true), Category("绘制工具")]
        public float StrokenWidth { set { this._strokenWidth = value; } }

        // 画笔的颜色
        protected Color _lineColor = perferBlue_Deep;
        /// <summary>
        /// 画笔的颜色
        /// </summary>
        [Description("画笔的颜色"), Browsable(true), Category("绘制工具")]
        public Color LineColor { get { return _lineColor; } set { this._lineColor = value; this.Invalidate(); } }


        // 画笔的颜色
        protected Color _brushColor = perferBlue;
        /// <summary>
        /// 画刷的颜色
        /// </summary>
        [Description("画刷的颜色"), Browsable(true), Category("绘制工具")]
        public Color BrushColor { get { return _brushColor; } set { _brushColor = value; this.Invalidate(); } }


        /// <summary>
        /// 鼠标移动时触发提示tips
        /// </summary>
        [Description("鼠标移动时触发提示tips"), Browsable(true), Category("提示")]
        public IToolTips ToolTips { set { this._toolTips = value; } }

        /// <summary>
        /// 是否启用动画, 注意子类是否实现动画
        /// </summary>
        [Description("是否启用动画"), Browsable(true), Category("动画")]
        public bool IsEnableAnimation { get; set; }
        #endregion


        public AbstractReportView()
        {
            InitializeComponent();
            DoubleBuffered = true;
            // 会被子类覆盖,所以可以默认赋值
            IsEnableAnimation = true;
        }

        private void AbstractReportView_Load(object sender, EventArgs e)
        {
            _linePen = new Pen(_lineColor, _strokenWidth);
            _lineBrush = new SolidBrush(_brushColor);
        }

        // 不允许初始化对象以及耗时操作
        protected override void OnPaint(PaintEventArgs e)
        {
            DateTime currentTime = DateTime.Now;

            // 自己声明的Graphics
            Graphics g = e.Graphics;
            // 绘制的质量设置
            g.CompositingQuality = CompositingQuality.Default;
            //g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            g.SmoothingMode = SmoothingMode.HighQuality;


            // 如果是设计器模式进行普通绘制以便设计展示
            if (isDesignMode)
            {
                g.FillRectangle(Brushes.Silver, new Rectangle(0, 0, Width, Height));
                if (_iView != null)
                {
                    // 进行正常绘制(// 不允许初始化对象以及耗时操作)
                    _iView.ChildPaint(g, _linePen, _lineBrush);
                }
            }


            // 如果有动画且启用则开始绘制动画, 否则正常绘制
            if (IsEnableAnimation && _animation != null && _animation.iAnimalionInterface != null)
            {
                if (_animation.AnimationState == Animation.AnimationStates.AnimationStop)
                {
                    // 这是异步动画
                    _animation.StartAnimalion();
                }
            }
            else
            {
                if (_iView != null)
                {
                    // 进行正常绘制
                    _iView.ChildPaint(g, _linePen, _lineBrush);
                }
            }

            //绘制toolTips, 最低绘制避免覆盖
            if (_toolTips != null)
            {
                _toolTips.RenderTips(g);
            }
        }
    }
}
