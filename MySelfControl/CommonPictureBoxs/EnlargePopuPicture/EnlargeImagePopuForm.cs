/*************************************************************************************
    * 类 名 称： EnlargeImagePopuForm
    * 命名空间： FishyuSelfControl.CommonPictureBoxs.EnlargePopuPicture
    * 创建时间： 2018/3/23 10:31:55
    * 作    者： zjm
    * 说    明： 只是进行展示
    * 修改时间：
    * 修 改 人：
**************************************************************************************/
using System;
using System.Drawing;
using System.Windows.Forms;

namespace FishyuSelfControl.CommonPictureBoxs.EnlargePopuPicture
{
    public partial class EnlargeImagePopuForm : Form
    {
        // 放大的图片源
        private Image enLargeImage;

        // 鼠标移动点
        private Point movePoint;


        public EnlargeImagePopuForm(Image image)
        {
            InitializeComponent();
            enLargeImage = image;
            DoubleBuffered = true;
        }


        private void EnlargeImagePopuForm_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 移动到中心目标
        /// </summary>
        /// <param name="x">x</param>
        /// <param name="y">y</param>
        public void Move2Target(Point point)
        {
            movePoint = point;
            this.Invalidate();
        }


        // 进行重绘
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawImage(enLargeImage, new Point(-movePoint.X + Width / 2, -movePoint.Y + Height / 2));
        }
    }
}
