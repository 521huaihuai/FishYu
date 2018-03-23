/*
* ==============================================================================
*
* File name: EnlargeImageImp
* Description: 简单的调用EnlargeImagePopuForm进行展示, 具体实现
*
* Version: 1.0
* Created: 2018/3/23 10:42:42
*
* Author: zjm
*
* ==============================================================================
*/
using FinshYuUtils.ImageUtils;
using System;
using System.Drawing;

namespace FishyuSelfControl.CommonPictureBoxs.EnlargePopuPicture
{
    public class EnlargeImageImp : IEnlargPicture
    {

        // 放大的窗体
        private EnlargeImagePopuForm enlargeImagePopuForm;
        // 放大倍数
        private float enlargeRate = 5;
        private int width;
        private int height;

        // 放大图片
        private Image enLargeImage;
        // 原图片
        private Image souceImage;
        // 图片真实的宽高(比如在pictureBox显示与实际不同)
        private int realImageWidth;
        private int realImageHeight;

        // 起始坐标
        private Point startPoint;

        public EnlargeImageImp(Image image, Point point, int realImageWidth, int realImageHeight)
        {
            startPoint = point;
            souceImage = image;
            this.realImageWidth = realImageWidth;
            this.realImageHeight = realImageHeight;
            if (image != null)
            {

                //ImageUtil.CaptureImage(image, );
                // 放大图片
                enLargeImage = ImageUtil.EnlargeImage(image, (image.Height * enlargeRate), (image.Width * enlargeRate));

                enlargeImagePopuForm = new EnlargeImagePopuForm(enLargeImage);
                enlargeImagePopuForm.Show();
                width = enlargeImagePopuForm.Width;
                height = enlargeImagePopuForm.Height;
            }
        }

        public void MoveEnlargePicture(Point movePoint, string info = "")
        {

            // 放大窗体的位置定位(注意在边缘的展示的问题)
            Point point = ReCalculatePosition(width, height, movePoint);

            // 如果窗体已被释放
            if (enlargeImagePopuForm.IsDisposed || !enlargeImagePopuForm.IsHandleCreated || !enlargeImagePopuForm.Created)
            {
                enlargeImagePopuForm = new EnlargeImagePopuForm(enLargeImage);
                enlargeImagePopuForm.Show();
            }

            // 进行定位
            enlargeImagePopuForm.Location = point;

            // 进行放大图像定位
            //Point enLargePoint = new Point((int)(movePoint.X * enlargeRate), (int)(movePoint.Y * enlargeRate));
            Point enLargePoint = new Point((movePoint.X * enLargeImage.Width / realImageWidth), (movePoint.Y * enLargeImage.Height / realImageHeight));
            enlargeImagePopuForm.Move2Target(enLargePoint);
        }

        /// <summary>
        /// 隐藏放大ImageView
        /// </summary>
        public void HidePopuView()
        {
            if (null != enlargeImagePopuForm && !enlargeImagePopuForm.IsDisposed && enlargeImagePopuForm.IsHandleCreated)
            {
                try
                {
                    if (enLargeImage != null)
                    {
                        enLargeImage.Dispose();
                    }
                    enlargeImagePopuForm.Close();
                }
                catch (Exception)
                {
                }
            }
        }


        public void OnDispose(bool isDisposeSource = false)
        {
            if (enLargeImage != null)
            {
                enLargeImage.Dispose();
            }
            if (isDisposeSource)
            {
                if (souceImage != null)
                {
                    souceImage.Dispose();
                }
            }
        }


        public void ChangeStartPosition(Point point)
        {
            startPoint = point;
        }


        // 重定位位置
        private Point ReCalculatePosition(int width, int height, Point movePoint)
        {
            Point point = new Point(movePoint.X + width / 4 + startPoint.X, movePoint.Y + startPoint.Y);
            return point;
        }
    }
}
