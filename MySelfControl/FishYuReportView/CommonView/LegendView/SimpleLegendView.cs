/* ======================================================================== 
* 本类功能概述:
* 
* 作者：zjm 
* 时间：2018/1/13 11:24:03 
* 文件名：SimpleLegendView 
* 版本：V1.0.1 
* 
* 修改者： 时间： 
* 修改说明： 
* ======================================================================== 
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FishyuAnimation;
using FishyuAnimation.Interpolation;
using FishyuAnimation.Animations;

namespace FishyuSelfControl.FishYuReportView.CommonView.LegendView
{
    public partial class SimpleLegendView : AbstractReportView, IView, IAnimation
    {
        public SimpleLegendView()
        {
            InitializeComponent();
            _iView = this;
            _animation = new SimpleAnimation();
            _animation.AnimalionTime = -1;
            _animation.iAnimalionInterface = this;
        }

        public void AnimalionRender(int animationIndex, InterpolationValue animationFrameInterpolation, InterpolationValue animationInterpolation)
        {
            this.Invalidate();
        }

        public void ChildPaint(Graphics g, Pen pen, Brush brush)
        {
            Rectangle rect = new Rectangle((int)(pen.Width / 2), (int)(pen.Width / 2), (int)(Width - pen.Width / 2), (int)(Height - pen.Width / 2));
            g.DrawRectangle(pen, rect);
            g.FillRectangle(brush, rect);
        }

        public void FrameAnimationFinished()
        {
        }

        public void PrepareAnimalion()
        {
        }

        private void SimpleLegendView_Load(object sender, EventArgs e)
        {

        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            //_animation.PauseAnimation();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            //this.Invalidate();
        }
    }
}
