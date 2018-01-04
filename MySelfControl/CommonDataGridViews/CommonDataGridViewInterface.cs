using System;
using System.Collections.Generic;
using System.Text;

namespace FishyuSelfControl.CommonDataGridViews
{
    public interface ICommonDataGridViewInterface
    {
        /// <summary>
        /// 创建每个ItemView
        /// </summary>
        void CreateItemView();

        /// <summary>
        /// 导出
        /// </summary>
        void ExportInExcelDetailWay();

        /// <summary>
        /// 导入
        /// </summary>
        void ExportOutExcelDetailWay();


    }
}
