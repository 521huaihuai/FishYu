using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FishyuAnimation;
using FishyuAnimation.Animations;
using FishyuAnimation.Interpolation;

namespace FishYuAnimationWinFormDemo
{
    public partial class SimpleAnimationControl : UserControl, IAnimation
    {

        public Animation animation = new SimpleAnimation();
        private int index;

        public SimpleAnimationControl()
        {
            InitializeComponent();
            animation.iAnimalionInterface = this;
            animation.AnimalionTime = 100 * 1000;
            animation.OnAnimationStartEvent += Animation_OnAnimationStartEvent;
        }

        private void Animation_OnAnimationStartEvent()
        {
        }

        private void SimpleAnimationControl_Load(object sender, EventArgs e)
        {

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            this.BackColor = Color.FromArgb(255, index > 255 ? 0 : index, 255, 255);
            e.Graphics.DrawString(index + "", Font, Brushes.Black, Width / 2, Height / 2);
        }

        public void AnimalionRender(int animationIndex, InterpolationValue animationFrameInterpolation, InterpolationValue animationInterpolation)
        {
            index = animationIndex;
            Invalidate();
        }

        public void FrameAnimationFinished()
        {
        }

        public void PrepareAnimalion()
        {
        }
    }
}
