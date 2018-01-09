using FinshYuUtils.DrawUtils;
using FishyuAnimation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using FishyuAnimation.Interpolation;
using FishyuAnimation.Animations;

namespace FishyuSelfControl.SimpleMessageBoxs
{
    public partial class SimpleMessageBox : Form, IAnimation
    {
        private static int screenWidth = Screen.PrimaryScreen.Bounds.Width;
        private static int screenHeight = Screen.PrimaryScreen.Bounds.Height;
        private Animation animation = new SimpleAnimation();

        public SimpleMessageBox()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.Manual;
            Location = new Point((screenWidth - Width) / 2, 11 * screenHeight / 15 - Height / 2);
            BackColor = Color.White;
            ShowInTaskbar = false;
            TransparencyKey = Color.White;
            animation.iAnimalionInterface = this;
        }

        public string Content { get; private set; }

        private float showTime = 3.5f;
        /// <summary>
        /// 展示的时间
        /// </summary>
        public float ShowTime { get { return showTime; }  set { showTime = value; } }

        public string TitleName { get; private set; }

        public static void ShowMessageBox(String content, float showTime = 3.5f, String title = "")
        {
            SimpleMessageBox messageBox = new SimpleMessageBox();
            messageBox.Content = content;
            messageBox.ShowTime = showTime;
            messageBox.TitleName = title;
            messageBox.Show();
        }

        private void SimpleMessageBox_Load(object sender, EventArgs e)
        {
            animation.AnimalionTime = showTime * 1000;
            animation.OnAnimationFinishedEvent += Animation_OnAnimationFinishedEvent;
            animation.StartAnimalion();
        }

        public void AnimalionRender(int animationIndex, InterpolationValue animationFrameInterpolation, InterpolationValue animationInterpolation)
        {

        }

        public void FrameAnimationFinished()
        {
        }

        public void PrepareAnimalion()
        {
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            GraphicsPath path = DrawUtil.CreateRoundedRectanglePath(new Rectangle(0, 0, Width, Height), Height / 4);
            Brush brush = new SolidBrush(Color.FromArgb(210, 36,33,28));
            g.FillPath(brush, path);
            SizeF sizef = g.MeasureString(Content, Font);
            g.DrawString(Content, Font, Brushes.White, new RectangleF((Width - sizef.Width) / 2, (Height - sizef.Height) / 2, sizef.Width, sizef.Height));

            path.Dispose();
            brush.Dispose();
        }

        private void Animation_OnAnimationFinishedEvent()
        {
            try
            {
                this.Invoke((EventHandler)delegate
                {
                    this.Close();
                });
            }
            catch (Exception)
            {
            }
        }
    }
}
