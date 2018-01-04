using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using ReportFormDesign.Model;

namespace ReportFormDesign.DataModels
{
    /// <summary>
    /// 图形注解模型
    /// </summary>
    public class LegendNoteModel : DataModel
    {

        /// <summary>
        /// 图形注解的颜色
        /// </summary>
        public Color Color { get; set; }
        public LegendNoteModel()
        {
            IsSpecialAreaDataModel = true;
        }

        public LegendNoteModel(string mainText, int mainData, Color color):base(mainText, mainData)
        {
            this.Color = color;
            IsSpecialAreaDataModel = true;
        }

    }
}
