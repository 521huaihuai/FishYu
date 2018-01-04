using FishyuAnimation.Interpolation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;

namespace FishyuAnimation.Animations
{
    public abstract class Animation
    {
        public IAnimation iAnimalionInterface;
        public IInterpolation iInterpolation;

        #region 动画状态标记
        /// <summary>
        /// 通过控制动画状态来控制动画
        /// </summary>
        public AnimationStates AnimationState { get; set; }

        /// <summary>
        /// 动画状态
        /// </summary>
        public enum AnimationStates
        {
            AnimationStart,
            AnimationRuning,
            AnimationStop,
            AnimationPause
        }
        #endregion

        /// <summary>
        /// 动画的执行一次需要的时间
        /// </summary>
        [DefaultValue(40)]
        public int DelayTime { get; set; }

        /// <summary>
        /// 动画持续时间(默认8000(8秒))
        /// 动画时间为负数,不停止动画
        /// </summary>
        [DefaultValue(8000)]
        public float AnimalionTime { get; set; }

        // 动画次数(标记)
        private int AnimationIndex = 0;
        // 每帧动画的插值数
        private InterpolationValue AnimationFrameInterpolation = new InterpolationValue();
        // 动画的插值数累计
        private InterpolationValue AnimationInterpolation = new InterpolationValue();
        //动画已消耗的时间
        private float AnimalionUsedTimeMilliseconds { get; set; }

        public Animation()
        {
            DelayTime = 40;
            AnimalionTime = 8000;
        }

        /// <summary>
        /// 开始动画
        /// </summary>
        public void StartAnimalion()
        {
            AnimationState = AnimationStates.AnimationStart;
            if (OnAnimationStartEvent != null)
            {
                OnAnimationStartEvent();
            }
            //准备动画
            if (iAnimalionInterface != null)
            {
                iAnimalionInterface.PrepareAnimalion();
            }
            Thread animalThread = new Thread(() =>
            {
                if (iInterpolation != null)
                {
                    AnimationFrameInterpolation = iInterpolation.GetInterpolationValue();
                }
                DateTime startAnimationTime;
                int sleepTime;
                AnimationState = AnimationStates.AnimationRuning;
                while (AnimationState != AnimationStates.AnimationStop)
                {
                    startAnimationTime = DateTime.Now;
                    //动画绘制
                    if (iAnimalionInterface != null)
                    {
                        iAnimalionInterface.AnimalionRender(AnimationIndex, AnimationFrameInterpolation, AnimationInterpolation);
                    }

                    AnimationIndex++;
                    if (iInterpolation != null)
                    {
                        AnimationFrameInterpolation = iInterpolation.GetInterpolationValue();
                        AnimationInterpolation.V1 += AnimationFrameInterpolation.V1;
                        AnimationInterpolation.V2 += AnimationFrameInterpolation.V2;
                        AnimationInterpolation.Tag = AnimationFrameInterpolation.Tag;
                    }
                    AnimalionUsedTimeMilliseconds = AnimationIndex * DelayTime;

                    //如果超过设定的动画时间, 结束动画(如果动画时间为负数,不停止动画)
                    if (AnimalionTime > 0 && AnimalionUsedTimeMilliseconds >= AnimalionTime)
                    {
                        StopAnimation();
                    }
                    //每帧动画结束
                    if (iAnimalionInterface != null)
                    {
                        iAnimalionInterface.FrameAnimationFinished();
                    }
                    if (OnFrameAnimationEndEvent != null)
                    {
                        OnFrameAnimationEndEvent();
                    }
                    //暂停动画
                    if (AnimationState == AnimationStates.AnimationPause)
                    {
                        if (OnAnimationPauseWvent != null)
                        {
                            OnAnimationPauseWvent();
                        }
                        while (AnimationState == AnimationStates.AnimationPause)
                        {
                            Thread.Sleep(200);
                        }
                    }
                    sleepTime = (int)(DelayTime - (DateTime.Now - startAnimationTime).TotalMilliseconds);
                    try
                    {
                        if (sleepTime > 0)
                        {
                            Thread.Sleep(sleepTime);
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                if (OnAnimationFinishedEvent != null)
                {
                    OnAnimationFinishedEvent();
                }
            });
            animalThread.IsBackground = true;
            animalThread.Priority = ThreadPriority.Highest;
            animalThread.Start();
        }

        /// <summary>
        /// 结束动画
        /// </summary>
        public void StopAnimation()
        {
            AnimationIndex = 0;
            AnimationState = AnimationStates.AnimationStop;
        }


        /// <summary>
        /// 暂停动画
        /// </summary>
        public void PauseAnimation()
        {
            AnimationState = AnimationStates.AnimationPause;
        }

        /// <summary>
        /// 恢复动画
        /// </summary>
        public void RecoveryAnimation()
        {
            AnimationState = AnimationStates.AnimationRuning;
        }

        /// <summary>
        /// 动画开始
        /// </summary>
        public event OnAnimationStart OnAnimationStartEvent;
        /// <summary>
        /// 每帧动画结束
        /// </summary>
        public event OnFrameAnimationEnd OnFrameAnimationEndEvent;
        /// <summary>
        /// 动画结束
        /// </summary>
        public event OnAnimationFinished OnAnimationFinishedEvent;
        /// <summary>
        /// 暂停动画
        /// </summary>
        public event OnAnimationPause OnAnimationPauseWvent;
    }
    public delegate void OnAnimationStart();
    public delegate void OnAnimationPause();
    public delegate void OnFrameAnimationEnd();
    public delegate void OnAnimationFinished();
}