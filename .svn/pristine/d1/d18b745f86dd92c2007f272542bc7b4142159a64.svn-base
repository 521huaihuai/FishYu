using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FinshYuUtils.ImageUtils;
using FishyuAnimation;
using FishyuAnimation.Interpolation;
using FishyuAnimation.Animations;
using FinshYuUtils.CommonUtils;

namespace FishyuSelfControl.CommonPictureBoxs.MinPictureBoxs
{
    public partial class AnimationMinPictureBox : UserControl, IAnimation
    {
        private int index = 0;
        private Point moveInPoint;
        private Point picPoint;
        private Brush backBrush = null;
        public Animation animation = new SimpleAnimation();
        private static Image closeBlackImage = StaticImageLoad.LoadImage(@"\Resources\Img_32\min_black_32.png");
        private static Image closeRedImage = StaticImageLoad.LoadImage(@"\Resources\Img_32\min_red_32.png");
        private static Image closeWhiteImage = StaticImageLoad.LoadImage(@"\Resources\Img_32\min_white_32.png");

        public enum DisplayStyle
        {
            Default = 0,
            Black2RedBackground = 1,
        }

        /// <summary>
        /// 展示的样式
        /// </summary>
        public DisplayStyle MyDisplayStyle { get; set; }


        private Color overCrossColor = Color.FromArgb(224, 224, 224);
        /// <summary>
        /// IsIntelligence专用, 划过启用特效
        /// </summary>
        public Color OverCrossColor { get { return overCrossColor; } set { overCrossColor = value; } }

        /// <summary>
        /// 是否启用检测填充展示
        /// </summary>
        public bool IsIntelligence { get; set; }

        public AnimationMinPictureBox()
        {
            DoubleBuffered = true;
            InitializeComponent();
            animation.iAnimalionInterface = this;
            animation.AnimalionTime = 1f * 1000;
            animation.OnAnimationStartEvent += Animation_OnAnimationStartEvent;
        }

        private void Animation_OnAnimationStartEvent()
        {
        }

        private void AnimationClosePictureBox_Load(object sender, EventArgs e)
        {
            if (DesignModeUtil.Instance.IsDesignMode)
            {
                pictureBox1.BackColor = Color.Red;
            }
            pictureBox1.BackgroundImage = closeBlackImage;
            backBrush = new SolidBrush(overCrossColor);
        }

        private void pictureBox1_MouseEnter_1(object sender, EventArgs e)
        {
            // 进行定位
            if (IsIntelligence)
            {
                moveInPoint = Cursor.Position;
                picPoint = this.PointToScreen(pictureBox1.Location);
                if (animation.AnimalionTime == 1000)
                {
                    animation.AnimalionTime = 4000;
                }
            }

            switch (MyDisplayStyle)
            {
                case DisplayStyle.Default:
                    pictureBox1.BackgroundImage = closeRedImage;
                    break;
                case DisplayStyle.Black2RedBackground:
                    pictureBox1.BackgroundImage = closeWhiteImage;
                    break;
                default:
                    pictureBox1.BackgroundImage = closeRedImage;
                    break;
            }
            animation.StartAnimalion();
        }

        private void pictureBox1_MouseLeave_1(object sender, EventArgs e)
        {
            animation.StopAnimation();
            pictureBox1.BackgroundImage = closeBlackImage;
            BackColor = Color.Transparent;
            index = 0;
            this.Invalidate();
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            if (OnPictrueBoxClickListenerEvent != null)
            {
                OnPictrueBoxClickListenerEvent();
            }
        }

        public void PrepareAnimalion()
        {
        }

        public void AnimalionRender(int animationIndex, InterpolationValue animationFrameInterpolation, InterpolationValue animationInterpolation)
        {
            index = animationIndex;
            if (!IsIntelligence)
            {
                switch (MyDisplayStyle)
                {
                    case DisplayStyle.Default:
                        BackColor = Color.FromArgb(index, 75, 75, 75);
                        break;
                    case DisplayStyle.Black2RedBackground:
                        BackColor = Color.FromArgb(index * 6 + 50, 254, 67, 101);
                        break;
                    default:
                        BackColor = Color.FromArgb(index, 75, 75, 75);
                        break;
                }
            }
            Invalidate();
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (IsIntelligence)
            {
                FillDrawView(index, e);
            }
        }

        private void FillDrawView(int animationIndex, PaintEventArgs e)
        {
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            e.Graphics.FillEllipse(backBrush, moveInPoint.X - picPoint.X - animationIndex * (Width / 6f), moveInPoint.Y - picPoint.Y - animationIndex * (Width / 6f),
                 animationIndex * (Width / 3f), animationIndex * (Width / 3f));
            //switch (MyDisplayStyle)
            //{
            //    case DisplayStyle.Default:
            //        e.Graphics.DrawImage(closeBlackImage, new Rectangle(0, 0, Width, Height), 0, 0, closeBlackImage.Width, closeBlackImage.Height, GraphicsUnit.Pixel);
            //        break;
            //    case DisplayStyle.Black2RedBackground:
            //        e.Graphics.DrawImage(closeWhiteImage, new Rectangle(0, 0, Width, Height), 0, 0, closeBlackImage.Width, closeBlackImage.Height, GraphicsUnit.Pixel);
            //        break;
            //    default:
            //        e.Graphics.DrawImage(closeRedImage, new Rectangle(0, 0, Width, Height), 0, 0, closeBlackImage.Width, closeBlackImage.Height, GraphicsUnit.Pixel);
            //        break;
            //}
        }

        public void FrameAnimationFinished()
        {
        }

        /// <summary>
        /// 点击
        /// </summary>
        public event OnPictrueBoxClickListener OnPictrueBoxClickListenerEvent;
        public delegate void OnPictrueBoxClickListener();
    }
}
