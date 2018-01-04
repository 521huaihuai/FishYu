using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ReportFormDesign.Models
{
    public class MultiArcSpliteInfoData :IComparable
    {
        public float sortY = 0;
        public string text;
        public PointF fitstPoint = new PointF();
        public PointF secondPoint = new PointF();
        public Color lineColor;

        public int CompareTo(object obj)
        {
            return sortY.CompareTo(((MultiArcSpliteInfoData)obj).sortY);
        }
    }
}
