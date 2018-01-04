using System;
using System.Collections.Generic;
using System.Text;

namespace ReportFormDesign.Models.GridViewModels
{

    public enum AutoSizeModel
    {
        SELF, FILL, NONE
    }

    public class Column
    {
        /// <summary>
        /// cell的开始位置
        /// </summary>
        public int ColStartX { get; set; }

        /// <summary>
        /// cell的实际宽度
        /// </summary>
        public int ColWidth { get; set; }

        /// <summary>
        /// cell内容展示
        /// </summary>
        public object Content { get; set; }

        /// <summary>
        /// 定义每一个Cell的宽度方式(默认AutoSizeModel.SELF)
        /// </summary>
        public AutoSizeModel SizeModel { get; set; }

        private ReportFormDesign.DrawUtils.LocationModel textLocation = ReportFormDesign.DrawUtils.LocationModel.Location_Center;
        /// <summary>
        /// 内容展示的位置(默认居中)
        /// </summary>
        public ReportFormDesign.DrawUtils.LocationModel TextLocation
        {
            get
            {
                return textLocation;
            }
            set
            {
                this.textLocation = value;
            }
        }
    }
}
