using System;
using System.Collections.Generic;
using System.Text;

namespace ReportFormDesign.Model
{

    //每一条数据源
    public class DataModel
    {

        public DataModel(string mainText, int mainData)
        {
            this.mainText = mainText;
            this.mainData = mainData;
        }


        public string mainText { get; set; }
        public int mainData { get; set; }

        /// <summary>
        /// 直接存储在这里更加有利于你的快速计算
        /// </summary>
        public ReportFormDesign.DrawUtils.AreaPositionRect Area = new ReportFormDesign.DrawUtils.AreaPositionRect();
    }
}
