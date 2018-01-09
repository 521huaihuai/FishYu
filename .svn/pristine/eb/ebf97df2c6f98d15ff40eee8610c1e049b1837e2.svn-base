using System;
using System.Collections.Generic;
using System.Text;

namespace ReportFormDesign.Models.GridViewModels
{
    public class Row
    {

        public int RowHeight { get; set; }
        public int RowWidth { get; set; }

        private List<Column> columnList = new List<Column>();
        private int fillCount;
        private int allSelfWidth;

        /// <summary>
        /// 添加某一列,使用AddColumn()
        /// </summary>
        public List<Column> Columns { get; set; }



        public Row()
        {
            Columns = new List<Column>();
        }

        /// <summary>
        /// 添加一列
        /// </summary>
        /// <returns>添加这一列的位置</returns>
        public int AddColumn(Column colum)
        {
            columnList.Add(colum);
            if (colum.SizeModel == AutoSizeModel.FILL)
            {
                fillCount++;
            }
            else if (colum.SizeModel == AutoSizeModel.SELF)
            {
                allSelfWidth += colum.ColWidth;
            }
            RefreshCellSize();
            return columnList.Count - 1;
        }

        public void RefreshCellSize()
        {
            if (fillCount != 0)
            {
                //计算自适应填充的Col的with
                int fillWidth = RowWidth - allSelfWidth;
                if (fillWidth < 0)
                {
                    fillWidth = 0;
                }
                int perWidth = fillWidth / fillCount;
                foreach (var item in columnList)
                {
                    if (item.SizeModel == AutoSizeModel.FILL)
                    {
                        item.ColWidth = perWidth;
                    }
                }
            }
            //确定每个cell的起始位置
            int startX = 0;
            foreach (var item in columnList)
            {
                item.ColStartX = startX;
                startX += item.ColWidth;
            }
        }
    }
}
