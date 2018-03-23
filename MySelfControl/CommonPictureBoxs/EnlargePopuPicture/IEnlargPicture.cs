/*************************************************************************************
    * 类 名 称： IEnlargPicture
    * 命名空间： FishyuSelfControl.CommonPictureBoxs.EnlargePopuPicture
    * 创建时间： 2018/3/23 10:30:05
    * 作    者： zjm
    * 说    明： 定义图片放大的接口
    * 修改时间：
    * 修 改 人：
**************************************************************************************/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace FishyuSelfControl.CommonPictureBoxs.EnlargePopuPicture
{
    public interface IEnlargPicture
    {
        /// <summary>
        /// 接口回调实现鼠标移动时图片放大并提醒
        /// </summary>
        /// <param name="movePoint">鼠标坐标</param>
        /// <param name="info">需要提醒的内容</param>
        void MoveEnlargePicture(Point movePoint, string info = "");

        /// <summary>
        /// 隐藏放大ImageView
        /// </summary>
        void HidePopuView();

        /// <summary>
        /// 释放图像资源
        /// </summary>
        /// <param name="isDisposeSource">是否释放原图片</param>
        void OnDispose(bool isDisposeSource = false);

        /// <summary>
        /// 挡窗体拖拽时需要进行调用更改起始位置
        /// </summary>
        /// <param name="point">窗体的location</param>
        void ChangeStartPosition(Point point);
    }
}
