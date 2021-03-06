﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

/// <summary>
/// design by Zjm
/// </summary>
namespace FishyuSelfControl.FishyuAnimateImage
{

    /// <summary>   
    /// 表示一类带动画功能的图像。   
    /// </summary>   
    public class AnimateWaitForm
    {
        Image image = null;
        FrameDimension frameDimension = null;
        bool mCanAnimate = false;
        int mFrameCount = 1, mCurrentFrame = 0;
        public Rectangle Rect;


        private static long CurrentTimeTick = 0;

        public enum GifType
        {
            Default,
            Reload,
            LongSpin,
            OriginRotation,
            OriginSizeRotation,
            StripLoading
        }


        /// <summary>   
        /// 实例化一个AnimateImage。   
        /// </summary>   
        /// <param name="img">动画图片。</param>   
        public AnimateWaitForm(Image img)
        {
            image = img;

            lock (image)
            {
                mCanAnimate = ImageAnimator.CanAnimate(image);
                if (mCanAnimate)
                {
                    Guid[] guid = image.FrameDimensionsList;
                    frameDimension = new FrameDimension(guid[0]);
                    mFrameCount = image.GetFrameCount(frameDimension);
                }
            }
        }

        #region PSR
        /// <summary>   
        /// 播放这个动画。   
        /// </summary>   
        public void Play()
        {
            if (mCanAnimate)
            {
                //lock (image)
                //{

                //}
                ImageAnimator.Animate(image, new EventHandler(FrameChanged));
            }
        }

        /// <summary>   
        /// 停止播放。   
        /// </summary>   
        public void Stop()
        {
            if (mCanAnimate)
            {
                //lock (image)
                //{
                //    ImageAnimator.StopAnimate(image, new EventHandler(FrameChanged));
                //}
                ImageAnimator.StopAnimate(image, new EventHandler(FrameChanged));
            }
        }

        /// <summary>   
        /// 重置动画，使之停止在第帧位置上。   
        /// </summary>   
        public void Reset()
        {
            if (mCanAnimate)
            {
                ImageAnimator.StopAnimate(image, new EventHandler(FrameChanged));
                //lock (image)
                //{
                //    image.SelectActiveFrame(frameDimension, 0);
                //    mCurrentFrame = 0;
                //}
                image.SelectActiveFrame(frameDimension, 0);
                mCurrentFrame = 0;
            }
        }
        #endregion

        public static void AnimatingWait(WaitAction waitAct, bool isSame = true)
        {
            AnimatingWait(waitAct, null, false, isSame);
        }

        public static void AnimatingWait(WaitAction waitAct, Control parent)
        {
            AnimatingWait(waitAct, parent, GifType.Default, false, true,  "");
        }

        public static void AnimatingWait(WaitAction waitAct, Control parent, bool isInMainThread = false, bool isSame = true)
        {
            AnimatingWait(waitAct, parent, GifType.Default, isInMainThread, isSame,  "");
        }

        /// <summary>
        /// 注意鼠标会穿透,需要父窗体拦截点击事件
        /// <summary>
        public static void AnimatingWait(WaitAction waitAct, Control parent, GifType type = GifType.Default, bool isInMainThread = false, bool isSame = true,  string content = "")
        {
            // 避免1秒内重复触发
            if (isSame && (DateTime.Now.Ticks - CurrentTimeTick) < 10000000)
            {
                return;
            }
            CurrentTimeTick = DateTime.Now.Ticks;
            WaitForm form = null;
            AnimateWaitForm animateImage = null;
            bool isFinish = false;

            //Gif动画展示
            Thread drawThread = new Thread(() =>
            {
                form = new WaitForm(parent, isInMainThread);
                Point drawPoint;
                Image _image;
                switch (type)
                {
                    case GifType.Default:
                        _image = FishyuSelfControl.Properties.Resources.Spinner;
                        break;
                    case GifType.Reload:
                        _image = FishyuSelfControl.Properties.Resources.Reload;
                        break;
                    case GifType.LongSpin:
                        _image = FishyuSelfControl.Properties.Resources.LongSpin;
                        break;
                    case GifType.OriginRotation:
                        _image = FishyuSelfControl.Properties.Resources.OriginRotation;
                        break;
                    case GifType.OriginSizeRotation:
                        _image = FishyuSelfControl.Properties.Resources.OriginSizeRotation;
                        break;
                    case GifType.StripLoading:
                        _image = FishyuSelfControl.Properties.Resources.StripLoading;
                        break;
                    default:
                        _image = FishyuSelfControl.Properties.Resources.Reload;
                        break;
                }
                animateImage = new AnimateWaitForm(_image);

                drawPoint = new Point((form.Width - _image.Width) / 2, (form.Height - _image.Height) / 2);
                form.Paint += ((obj, e) =>
                {
                    Graphics g = e.Graphics;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    g.PixelOffsetMode = PixelOffsetMode.Default; //高像素偏移质量
                    g.CompositingQuality = CompositingQuality.HighQuality;
                    //InterpolationMode不能使用High或者HighQualityBicubic,如果是灰色或者部分浅色的图像是会在边缘处出一白色透明的线
                    //用HighQualityBilinear却会使图片比其他两种模式模糊（需要肉眼仔细对比才可以看出）
                    g.InterpolationMode = InterpolationMode.Default;
                    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                    lock (animateImage.image)
                    {
                        try
                        {
                            g.DrawImage(animateImage.Image, drawPoint);
                        }
                        catch (Exception)
                        {
                        }
                        //g.DrawString()
                    }
                    //try
                    //{
                    //    g.DrawImage(animateImage.Image, drawPoint);
                    //}
                    //catch (Exception)
                    //{
                    //}

                });
                animateImage.Location = form.Location;
                animateImage.Rect = new Rectangle(animateImage.Location, new Size(_image.Width, _image.Height));
                animateImage.OnFrameChanged = ((image, evg) =>
                {
                    try
                    {
                        form.Invalidate(new Rectangle(drawPoint, new Size(_image.Width, _image.Height)));
                    }
                    catch (Exception)
                    {
                    }
                    //form.Invalidate();
                });

                //lock (_obj)
                //{

                //}
                if (form != null && !form.IsDisposed && !isFinish)
                {
                    animateImage.Play();
                    form.Focus();
                    form.ShowDialog();
                    //isOnShow = true;
                }
            });
            drawThread.IsBackground = true;
            drawThread.Start();


            //如果选择主线程阻塞
            if (isInMainThread)
            {
                waitAct();
                isFinish = true;
                CloseForm(animateImage, form);
            }
            else
            {
                Thread thread = new Thread(() =>
                {
                    waitAct();
                    isFinish = true;
                    CloseForm(animateImage, form);
                });
                thread.IsBackground = true;
                thread.Start();
            }
        }

        private static void CloseForm(AnimateWaitForm animateImage, Form form)
        {
            //停止动画
            //lock (animateImage.image)
            //{
            //    if (animateImage != null)
            //    {
            //        animateImage.Stop();
            //        animateImage.DisposeImage();
            //    }
            //}
            try
            {
                if (animateImage != null)
                {
                    animateImage.Stop();
                    animateImage.DisposeImage();
                }
            }
            catch (Exception)
            {
            }
            lock (new object())
            {
                //关闭窗体
                //if (form != null && form.Created && form.IsHandleCreated && !form.IsDisposed)
                if (form != null)
                {
                    try
                    {
                        form.Invoke((EventHandler)delegate
                        {
                            form.Close();
                            form.Dispose();
                        });
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        private void DisposeImage()
        {
            if (image != null)
            {
                image.Dispose();
            }
        }

        private void FrameChanged(object sender, EventArgs e)
        {
            mCurrentFrame = mCurrentFrame + 1 >= mFrameCount ? 0 : mCurrentFrame + 1;
            try
            {
                lock (image)
                {
                    image.SelectActiveFrame(frameDimension, mCurrentFrame);
                }
                //image.SelectActiveFrame(frameDimension, mCurrentFrame);
            }
            catch (Exception)
            {
            }
            if (OnFrameChanged != null)
            {
                OnFrameChanged(image, e);
            }
        }

        /// <summary>   
        /// 图片。   
        /// </summary>   
        public Image Image
        {
            get { return image; }
        }

        /// <summary>   
        /// 总帧数。   
        /// </summary>   
        public int FrameCount
        {
            get { return mFrameCount; }
        }

        /// <summary>   
        /// 播放的当前帧。   
        /// </summary>   
        public int CurrentFrame
        {
            get { return mCurrentFrame; }
        }


        public Point Location { get; set; }

        public delegate void WaitAction();

        /// <summary>   
        /// 动画当前帧发生改变时触发。   
        /// </summary>   
        public event EventHandler OnFrameChanged;
    }
}
