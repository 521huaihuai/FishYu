using FishyuAnimation.Interpolation;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FishyuAnimation.Animations
{
    /// <summary>
    /// 平移动画效果
    /// </summary>
    public class TranslationAnimation : Animation, IAnimation
    {
        private Control control;
        private Point _point;
        public TranslationAnimation()
        {
            iAnimalionInterface = this;
            DelayTime = 20;//每秒50帧
            AnimalionTime = 2000;
        }

        public void TranslateStart(Control control, int x, int y)
        {
            TranslateStart(control, new Point(x, y), 2000);
        }

        public void TranslateStart(Control control, Point toPoint)
        {
            TranslateStart(control, toPoint, 2000);
        }

        public void TranslateStart(Control control, Point toPoint, float animationTime)
        {
            StartAnimalion();
            this.control = control;
            _point = toPoint;
            AnimalionTime = animationTime;
        }

        public void AnimalionRender(int animationIndex, InterpolationValue animationFrameInterpolation, InterpolationValue animationInterpolation)
        {
            control.Invoke((EventHandler)delegate
            {
                control.Location = new Point((int)(control.Location.X + animationFrameInterpolation.V1), (int)(control.Location.Y + animationFrameInterpolation.V2));
            });
        }

        public void FrameAnimationFinished()
        {
        }

        public void PrepareAnimalion()
        {
        }
    }
}
