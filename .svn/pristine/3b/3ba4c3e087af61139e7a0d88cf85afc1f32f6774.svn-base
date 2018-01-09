using System;
using System.Collections.Generic;
using System.Text;
using ReportFormDesign.Model;
using ReportFormDesign.ReportViewPanel;

namespace ReportFormDesign.DrawUtils
{
    /// <summary>
    /// 位置信息
    /// </summary>
    public class AreaPositionRect
    {

        public bool IsCoordinateModel = false;

        public AreaPositionRect()
        {
            this.left = -20;
            this.top = -10;
            this.right = -10;
            this.bottom = -10;
            PaddingIn = 20;
        }

        //public AreaPositionRect(int left, int top, float width, float height)///, DataModel data)
        //{
        //    this.left = left;
        //    this.top = top;
        //    this.right = (int)(left + width);
        //    this.bottom = (int)(top + height);
        //    Width = (int)width;
        //    Height = (int)height;
        //    //this.data = data;
        //}

        public AreaPositionRect(int left, int top, int right, int bottom)///, DataModel data)
        {
            this.left = left;
            this.top = top;
            this.right = right;
            this.bottom = bottom;
            Width = right - left;
            Height = bottom - top;
            PaddingIn = 10;
            //this.data = data;
        }

        //public DataModel data { get; set; }
        public int left { get; set; }
        public int top { get; set; }
        public int right { get; set; }
        public int bottom { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        /// <summary>
        /// 是否是在该范围内
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool isInRect(int x, int y, bool b)
        {
            if (b && x < right && x > left && y < bottom + PaddingIn / 2)
            {
                return true;
            }
            if (x < right && x > left && y > top - PaddingIn / 2 && y < bottom + PaddingIn / 2)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 是否鼠标停留在上方
        /// </summary>
        public bool IsMouseIn { get; set; }


        /// <summary>
        /// 间距(默认20)
        /// </summary>
        public int PaddingIn { get; set; }
    }
}
