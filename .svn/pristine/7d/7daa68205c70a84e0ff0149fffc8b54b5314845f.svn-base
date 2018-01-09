using FishyuAnimation.Interpolation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FishyuAnimation
{
    public interface IAnimation
    {
        /// <summary>
        /// 准备动画
        /// </summary>
        void PrepareAnimalion();

        /// <summary>
        /// 动画绘制
        /// </summary>
        /// <param name="animationIndex">第几次动画</param>
        /// <param name="animationFrameInterpolation">每帧动画插值数</param>
        /// <param name="animationInterpolation">插值数总数</param>
        void AnimalionRender(int animationIndex, InterpolationValue animationFrameInterpolation, InterpolationValue animationInterpolation);

        /// <summary>
        /// 动画结束
        /// </summary>
        void FrameAnimationFinished();
    }
}
