using System;
using System.Collections.Generic;
using System.Text;
using ReportFormDesign.Model;

namespace ReportFormDesign.CurrentPosition
{
    /// <summary>
    /// 当前鼠标位置信息和数据
    /// </summary>
    public class CurrentPositionRectData
    {

        public CurrentPositionRectData(int x, int y, DataModel data)
        {
            this.X = x;
            this.Y = y;
            this.Width = 80;
            this.Height = 40;
            this.data = data;
        }

        public CurrentPositionRectData(int x, int y, int width, int height, DataModel data)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
            this.data = data;
        }

        public DataModel data { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

    }
}
