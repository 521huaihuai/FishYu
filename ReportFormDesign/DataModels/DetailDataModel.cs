using System;
using System.Collections.Generic;
using System.Text;

namespace ReportFormDesign.Model
{
    class DetailDataModel : DataModel
    {

        public DetailDataModel(string mainText, int mainData, string[] textAndData):base(mainText, mainData)
        {
            this.TextAndData = textAndData;
        }

        /// <summary>
        /// 奇数位为Text, 偶数为位Data
        /// </summary>
        public string[] TextAndData { get; set; }
    }
}
