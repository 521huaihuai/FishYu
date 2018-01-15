/* ======================================================================== 
* 本类功能概述:
* 
* 作者：zjm 
* 时间：2018/1/13 14:20:25 
* 文件名：FishYuDataGridView 
* 版本：V1.0.1 
* 
* 修改者： 时间： 
* 修改说明： 
* ======================================================================== 
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;

namespace FishyuSelfControl.FishYuReportView.AutoSortReportView.DataGridViews
{
    public partial class FishYuDataGridView : AbstractReportView, IView
    {

        protected FishYuCellStyle _columnCellStyle = new FishYuCellStyle();
        /// <summary>
        /// 列样式(如果有多行列则无效)
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Description("列样式(如果有多行列则无效)"), Browsable(true), Category("样式")]
        public FishYuCellStyle ColumnCellStyle { get { return _columnCellStyle; } set { _columnCellStyle = value; } }


        /// <summary>
        /// 列
        /// </summary>
        public List<Column> TitleColumns { get; set; }

        /// <summary>
        /// 列工厂
        /// </summary>
        public ColumnFactory ColumnFactoryInstance = ColumnFactory.Instance;

        public FishYuDataGridView()
        {
            InitializeComponent();
            _iView = this;
        }


        private void FishYuDataGridView_Load(object sender, EventArgs e)
        {
            InitDataGridView();
        }

        public void InitDataGridView()
        {
            if (TitleColumns != null)
            {
                foreach (var item in TitleColumns)
                {
                    if (item.ChildColumns == null)
                    {
                        item.CellStyle = _columnCellStyle;
                    }
                }
                this.Invalidate();
            }
        }


        public void ChildPaint(Graphics g, Pen pen, Brush brush)
        {
            // 先绘制Columns
            if (TitleColumns != null)
            {
                foreach (var item in TitleColumns)
                {
                    if (item.CellType == CellType.Text)
                    {
                        Cell.DrawCell(g, item.DrawCell);
                    }
                    else if (item.CellType == CellType.CheckBox)
                    {
                        // 如果是CheckBox 则绘制CheckBox,可以进行勾选
                    }
                }
            }
        }
    }
}
