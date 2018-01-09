using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ReportFormDesign.Model
{

    //每一条数据源
    public class DataModel
    {

        public DataModel()
        {
            IsSpecialAreaDataModel = false;
            this.ModelColor = ReportFormDesign.DrawUtils.ReportViewUtils.perferBlue;
        }

        public DataModel(string mainText, float mainData)
            : base()
        {
            this.mainText = mainText;
            this.mainData = mainData;
        }

        public DataModel(string mainText, int mainData, Color color)
            : this(mainText, mainData)
        {
            this.ModelColor = color;
        }

        public DataModel(string mainText, int mainData, Color color, object Tag)
            : this(mainText, mainData, color)
        {
            this.Tag = Tag;
        }


        /// <summary>
        /// 在DetailDataMedel中代表标题, 其它一律为一条基础数据的名字
        /// </summary>
        public string mainText { get; set; }
        public float mainData { get; set; }

        /// <summary>
        /// 模型颜色
        /// </summary>
        public Color ModelColor { get; set; }

        /// <summary>
        /// 是否是指定的特殊区域的图形模型
        /// </summary>
        public bool IsSpecialAreaDataModel { get; set; }

        public object Tag { get; set; }

        /// <summary>
        /// 直接存储在这里更加有利于你的快速计算
        /// </summary>
        public ReportFormDesign.DrawUtils.AreaPositionRect Area = new ReportFormDesign.DrawUtils.AreaPositionRect();
    }
}
