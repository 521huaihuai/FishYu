/*
* ==============================================================================
*
* File name: ImageUtil
* Description: 图像类操作
*
* Version: 1.0
* Created: 2018/3/23 11:35:37
*
* Author: zjm
*
* ==============================================================================
*/
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace FinshYuUtils.ImageUtils
{
    public class ImageUtil
    {
        /// <summary>
        /// 从大图中截取一部分图片
        /// </summary>
        /// <param name="fromImagePath">原图</param>        
        /// <param name="offsetX">从偏移X坐标位置开始截取</param>
        /// <param name="offsetY">从偏移Y坐标位置开始截取</param>
        /// <param name="width">保存图片的宽度</param>
        /// <param name="height">保存图片的高度</param>
        /// <param name="isDispose">是否释放原图像</param>
        /// <returns></returns>
        public static Image CaptureImage(Image fromImage, int offsetX, int offsetY, int width, int height, bool isDispose = false)
        {
            //创建新图位图
            Bitmap bitmap = new Bitmap(width, height);
            //创建作图区域
            Graphics graphic = Graphics.FromImage(bitmap);
            //截取原图相应区域写入作图区
            graphic.DrawImage(fromImage, 0, 0, new Rectangle(offsetX, offsetY, width, height), GraphicsUnit.Pixel);
            //从作图区生成新图
            Image saveImage = Image.FromHbitmap(bitmap.GetHbitmap());

            graphic.Dispose();
            bitmap.Dispose();
            if (isDispose)
            {
                fromImage.Dispose();
            }

            return saveImage;
        }


        /// <summary>
        /// 放大图像
        /// </summary>
        /// <param name="b">原始图像</param>
        /// <param name="destHeight">目标高度</param>
        /// <param name="destWidth">目标宽度</param>
        /// <returns></returns>
        public static Bitmap EnlargeImage(Image b, float destHeight, float destWidth, bool isDispose = false)
        {
            System.Drawing.Image imgSource = b;
            System.Drawing.Imaging.ImageFormat thisFormat = imgSource.RawFormat;
            float sW = 0, sH = 0;
            // 按比例缩放           
            int sWidth = imgSource.Width;
            int sHeight = imgSource.Height;
            if ((sWidth * destHeight) > (sHeight * destWidth))
            {
                sW = destWidth;
                sH = (destWidth * sHeight) / sWidth;
            }
            else
            {
                sH = destHeight;
                sW = (sWidth * destHeight) / sHeight;
            }
            Bitmap outBmp = new Bitmap((int)destWidth, (int)destHeight);
            Graphics g = Graphics.FromImage(outBmp);
            g.Clear(Color.Transparent);
            // 设置画布的描绘质量         
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(imgSource, new Rectangle((int)((destWidth - sW) / 2), (int)((destHeight - sH) / 2), (int)sW, (int)sH), 0, 0, imgSource.Width, imgSource.Height, GraphicsUnit.Pixel);
            g.Dispose();
            //// 以下代码为保存图片时，设置压缩质量     
            //EncoderParameters encoderParams = new EncoderParameters();
            //long[] quality = new long[1];
            //quality[0] = 100;
            //EncoderParameter encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            //encoderParams.Param[0] = encoderParam;
            if (isDispose)
            {
                imgSource.Dispose();
            }
            return outBmp;
        }


        ///// <summary>
        ///// 等比例放大
        ///// </summary>
        //public static PointF RateEnlargeView(int sWidth, int sHeight, int destWidth, int destHeight)
        //{
        //    float sW = 0;
        //    float sH = 0;

        //    if ((sWidth * destHeight) > (sHeight * destWidth))
        //    {
        //        sW = destWidth / sWidth;
        //        sH = (destWidth) / sWidth;
        //    }
        //    else
        //    {
        //        sH = destHeight;
        //        sW = (sWidth * destHeight) / sHeight;
        //    }
        //    return new PointF(sW, sH);
        //}
    }
}
