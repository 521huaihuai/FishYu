using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FinshYuUtils.ImageUtils;
using FinshYuUtils.CommonUtils;

namespace FishyuSelfControl.CommonPictureBoxs.SimplePictureBoxs
{
    public partial class SimplePictureBox : UserControl
    {
        //private static Image _image1 = StaticImageLoad.LoadImage(@"\Resources\Img_32\min_black_32.png");
        //private static Image _image2 = StaticImageLoad.LoadImage(@"\Resources\Img_32\min_red_32.png");

        public SimplePictureBox()
        {
            InitializeComponent();
        }

        private void SimpleClosePictureBox_Load(object sender, EventArgs e)
        {
            if (DesignModeUtil.Instance.IsDesignMode)
            {
                pictureBox1.BackColor = Color.Red;
            }
            //pictureBox1.BackgroundImage = _image1;
            pictureBox1.BackColor = BackColor;
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            //pictureBox1.BackgroundImage = _image2;
            pictureBox1.BackColor = _selectColor;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            //pictureBox1.BackgroundImage = _image1;
            pictureBox1.BackColor = BackColor;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox1.BackColor = ClickColor;
            if (OnPictrueBoxClickListenerEvent != null)
            {
                OnPictrueBoxClickListenerEvent();
            }
        }

        private Color _selectColor = Color.Silver;
        /// <summary>
        /// 选中的背景颜色
        /// </summary>
        [Description("选中的背景颜色"), Browsable(true), Category("背景颜色")]
        public Color SelectColor { get { return _selectColor; } set { _selectColor = value; this.Invalidate(); } }

        private Color _clickColor = Color.Silver;
        /// <summary>
        /// 点击的背景颜色
        /// </summary>
        [Description("点击的背景颜色"), Browsable(true), Category("背景颜色")]
        public Color ClickColor { get { return _clickColor; } set { _clickColor = value; this.Invalidate(); } }

        /// <summary>
        /// 点击
        /// </summary>
        public event OnPictrueBoxClickListener OnPictrueBoxClickListenerEvent;
        public delegate void OnPictrueBoxClickListener();
    }
}
