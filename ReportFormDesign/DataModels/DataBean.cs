using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ReportFormDesign
{
    public class DataBean
    {
        //标题
        public string Title;
        //x轴数据
        public string[] X_Data;
        //y轴数据
        public int[] Y_Data;

        //Y轴标签
        public string Lable_Y;
        //X轴标签
        public string Lable_X;
        //其他组件数据源
        public string[] CompontData;
    }
}
