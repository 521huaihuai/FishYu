using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FinshYuUtils.ImageUtils;
using FinshYuUtils.CommonUtils;

namespace FishyuSelfControl.CommonPictureBoxs.ClosePictureBoxs
{
    public partial class SimpleClosePictureBox : UserControl
    {
        private bool isDesignMode = DesignModeUtil.Instance.IsDesignMode;
        private static Image closeBlackImage = StaticImageLoad.LoadImage(@"\Resources\Img_32\close_black.png");
        private static Image closeRedImage = StaticImageLoad.LoadImage(@"\Resources\Img_32\close_red.png");

        public SimpleClosePictureBox()
        {
            InitializeComponent();
        }

        private void SimpleClosePictureBox_Load(object sender, EventArgs e)
        {
            if (isDesignMode)
            {
                pictureBox1.BackColor = Color.Red;
            }
            pictureBox1.BackgroundImage = closeBlackImage;
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.BackgroundImage = closeRedImage;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.BackgroundImage = closeBlackImage;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (OnPictrueBoxClickListenerEvent != null)
            {
                OnPictrueBoxClickListenerEvent();
            }
        }

        /// <summary>
        /// 点击
        /// </summary>
        public event OnPictrueBoxClickListener OnPictrueBoxClickListenerEvent;
        public delegate void OnPictrueBoxClickListener();
    }
}
