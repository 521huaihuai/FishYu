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
using System.Windows.Forms;

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

        protected Color _columnGridColor = perferBlue;
        /// <summary>
        /// 列边框颜色
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Description("列边框颜色"), Browsable(true), Category("样式")]
        public Color ColumnGridColor
        {
            get { return _columnGridColor; }
            set
            {
                _columnGridColor = value;
                if (TitleColumns != null)
                {
                    foreach (var item in TitleColumns)
                    {
                        SetColumnColor(item);
                    }
                }
                this.Invalidate();
            }
        }


        protected Color _cellGridColor = perferBlue;
        /// <summary>
        /// 列边框颜色
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Description("列边框颜色"), Browsable(true), Category("样式")]
        public Color CellDefaultGridColor
        {
            get { return _cellGridColor; }
            set
            {
                _cellGridColor = value;
                this.Invalidate();
            }
        }


        private void SetColumnColor(Column item)
        {
            item.GridColor = _columnGridColor;
            if (item.ChildColumns != null)
            {
                foreach (var column in item.ChildColumns)
                {
                    SetColumnColor(column);
                }
            }
        }

        /// <summary>
        /// 列
        /// </summary>
        public List<Column> TitleColumns { get; set; }

        public Dictionary<int, Dictionary<int, Cell>> Cells = new Dictionary<int, Dictionary<int, Cell>>();
        /// <summary>
        /// 列工厂
        /// </summary>
        public ColumnFactory ColumnFactoryInstance = ColumnFactory.Instance;

        // 当前点击点
        private Point CurrentPoint;
        // 是否准备展示拖拽辅助线
        private bool isReadyShowVHelpLine;
        private bool isReadyShowHHelpLine;
        private bool isDrawVHelpLine;
        private bool isDraging;
        private bool isDrawHHelpLine;
        private Column CurrentColumn;

        public FishYuDataGridView()
        {
            InitializeComponent();
            _iView = this;
        }


        private void FishYuDataGridView_Load(object sender, EventArgs e)
        {
            InitDefaultDataGridView();
        }

        public void InitDefaultDataGridView()
        {
            if (TitleColumns != null)
            {
                foreach (var item in TitleColumns)
                {
                    SetChildStyle(item);
                }
                this.Invalidate();
            }
        }

        private void SetChildStyle(Column item)
        {
            item.CellStyle = _columnCellStyle;
            item.GridColor = _columnGridColor;
            if (item.ChildColumns != null)
            {
                foreach (var column in item.ChildColumns)
                {
                    SetChildStyle(column);
                }
            }
        }


        // 不允许初始化对象以及耗时操作
        public void ChildPaint(Graphics g, Pen pen, Brush brush)
        {
            // 先绘制Columns
            if (TitleColumns != null)
            {
                foreach (var item in TitleColumns)
                {
                    DrasColumns(g, item);
                }
            }

            if (isDrawVHelpLine)
            {
                g.DrawLine(pen, CurrentPoint.X, 0, CurrentPoint.X, Height);
            }
            if (isDrawHHelpLine)
            {
                g.DrawLine(pen, 0, CurrentPoint.Y, Width, CurrentPoint.Y);
            }
        }

        private void DrasColumns(Graphics g, Column item)
        {
            if (item.IsVisible)
            {
                if (item.CellType == CellType.Text)
                {
                    Cell.DrawCell(g, item.DrawCell);
                }
                else if (item.CellType == CellType.CheckBox)
                {
                    // 如果是CheckBox 则绘制CheckBox,可以进行勾选
                }
                if (null != item.ChildColumns)
                {
                    foreach (var column in item.ChildColumns)
                    {
                        DrasColumns(g, column);
                    }
                }
            }
        }




        #region MouseEvent
        protected override void OnMouseClick(MouseEventArgs e)
        {
            CurrentPoint = new Point(e.X, e.Y);
            if (TitleColumns != null)
            {
                foreach (var item in TitleColumns)
                {
                    SetIsSelected(item);
                }
            }
            if (TitleColumns != null)
            {
                foreach (var item in TitleColumns)
                {
                    if (IsInRect(item, CurrentPoint))
                    {
                        this.Invalidate();
                        break;
                    }
                }
            }

        }

        // 设置未被选中
        private void SetIsSelected(Column item)
        {
            if (item.IsVisible)
            {
                item.DrawCell.IsSelected = false;
                if (null != item.ChildColumns)
                {
                    foreach (var column in item.ChildColumns)
                    {
                        SetIsSelected(column);
                    }
                }
            }
        }

        // 是否被选中
        private bool IsInRect(Column column, Point point)
        {
            if (column.IsVisible)
            {
                if (Cell.IsInRectangle(point, column.DrawCell))
                {
                    column.DrawCell.IsSelected = true;
                    return true;
                }
                if (column.ChildColumns != null)
                {
                    foreach (var item in column.ChildColumns)
                    {
                        if (IsInRect(item, point))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            CurrentPoint = new Point(e.X, e.Y);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            CurrentPoint = new Point(e.X, e.Y);
            if (isReadyShowVHelpLine)
            {
                isDrawVHelpLine = true;
                isDraging = true;
            }
            if (isReadyShowHHelpLine)
            {
                isDrawHHelpLine = true;
                isDraging = true;
            }
            this.Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            CurrentPoint = new Point(e.X, e.Y);
            if (isDraging)
            {
                // 改变Columns的宽度或高度
                ChangeColumnsWH();

                // 改变Cells的宽度或高度
                ChangeCellsWH();
            }
  
            
            isDrawVHelpLine = false;
            isDrawHHelpLine = false;
            isDraging = false;
        }

        private void ChangeCellsWH()
        {

        }

        private void ChangeColumnsWH()
        {
            if (isDrawVHelpLine)
            {
                CurrentColumn.Width = CurrentPoint.X - CurrentColumn.X;
            }
            else
            {
                CurrentColumn.Height = CurrentPoint.Y - CurrentColumn.Y;
            }


            // 如果这个Column是有父亲的, 并且这个父节点还有多个子节点, 那么其他的不是节点关系的Columns将不会发生改变, 否则父节点需要随着子节点变化而且其他Columns也将发生改变大小
            ResizeColumns();
        }

        private void ResizeColumns()
        {
            if (TitleColumns != null)
            {
                if (CurrentColumn.pColumn != null && CurrentColumn.pColumn.ChildColumns != null && CurrentColumn.pColumn.ChildColumns.Count > 1)
                {
                    foreach (var item in CurrentColumn.pColumn.ChildColumns)
                    {
                        // 如果是固定宽高, 则不变
                        if (item.AutoSizeMode == DataGridViews.AutoSizeMode.Default)
                        {
                            //子节点也不需要改变
                            continue;
                        }
                        // 如果是填充, 则先确定改变的以及固定的在确定填充的
                        if (item.AutoSizeMode == DataGridViews.AutoSizeMode.Fill)
                        {

                        }
                    }
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            CurrentPoint = new Point(e.X, e.Y);
            isReadyShowVHelpLine = false;
            isReadyShowHHelpLine = false;
            if (isDraging)
            {
                //if (isDrawYHelpLine)
                //{
                //    this.Invalidate(new Rectangle(0, CurrentPoint.Y - 20, Width, 40));
                //}
                //else
                //{
                //    this.Invalidate(new Rectangle(CurrentPoint.X - 20, 0, 40, Height));
                //}
                if (isDrawVHelpLine)
                {
                    if (e.X <= CurrentColumn.X)
                    {
                        isDrawVHelpLine = false;
                    }
                    else
                    {
                        isDrawVHelpLine = true;
                    }
                }
                else
                {
                    if (e.Y <= CurrentColumn.Y)
                    {
                        isDrawHHelpLine = false;
                    }
                    else
                    {
                        isDrawHHelpLine = true;
                    }
                }
                this.Invalidate();
                return;
            }
            if (null != TitleColumns)
            {
                int ii = 0;
                foreach (var item in TitleColumns)
                {
                    ii = IsInBorderRound(item);
                    if (ii > 0)
                    {
                        switch (ii)
                        {
                            case 1:
                                isReadyShowVHelpLine = true;
                                Cursor = Cursors.VSplit;
                                break;
                            case 2:
                                isReadyShowHHelpLine = true;
                                Cursor = Cursors.HSplit;
                                break;
                            default:
                                break;
                        }
                        break;
                    }
                }
                if (ii == 0)
                {
                    Cursor = Cursors.Default;
                }
                else
                {
                    this.Invalidate();
                }
            }
        }


        // 鼠标是否在边界
        private int IsInBorderRound(Column item)
        {
            if (item.IsVisible)
            {
                int ii = Cell.IsInBorderRound(item.DrawCell, CurrentPoint);
                if (ii > 0)
                {
                    CurrentColumn = item;
                    return ii;
                }
                if (null != item.ChildColumns)
                {
                    foreach (var column in item.ChildColumns)
                    {
                        ii = IsInBorderRound(column);
                        if (ii > 0)
                        {
                            return ii;
                        }
                    }
                }
            }
            return 0;
        }
        #endregion
    }
}
