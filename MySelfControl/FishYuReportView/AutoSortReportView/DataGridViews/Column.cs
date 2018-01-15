﻿/* ======================================================================== 
* 本类功能概述:列
* 
* 作者：zjm 
* 时间：2018/1/13 15:54:19 
* 文件名：Column 
* 版本：V1.0.1 
* 
* 修改者： 时间： 
* 修改说明： 
* ======================================================================== 
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FishyuSelfControl.FishYuReportView.AutoSortReportView.DataGridViews
{
    /// <summary>
    /// 列的size设置(最终影响单元格)
    /// </summary>
    public enum AutoSizeMode
    {
        Default, Fill, None
    }

    /// <summary>
    /// 每列数据的排序(默认自动从大到小)
    /// </summary>
    public enum SortMode
    {
        AutoSmall, AutoBig, None
    }

    public class Column
    {
        /// <summary>
        /// 列的size设置(最终影响单元格)
        /// </summary>
        public AutoSizeMode AutoSizeMode { get; set; }

        /// <summary>
        /// 每列数据的排序(默认自动从大到小)
        /// </summary>
        public SortMode SortMode { get; set; }

        protected CellType _cellType;
        /// <summary>
        /// 设置单元格格式,如果Column有ChildColumn 由ChildColumn决定
        /// </summary>
        public CellType CellType
        {
            get { return _cellType; }
            set
            {
                _cellType = value;
                DrawCell.Type = _cellType;
                if (Cells != null)
                {
                    if (IsBottomColumn())
                    {
                        foreach (var item in Cells)
                        {
                            item.Type = _cellType;
                        }
                    }
                }
            }
        }

        public Cell DrawCell = new Cell();

        protected FishYuCellStyle _cellStyle;
        /// <summary>
        /// 设置单元格样式,如果Column有ChildColumn 由ChildColumn决定
        /// 底层Column不可为空
        /// </summary>
        public FishYuCellStyle CellStyle
        {
            get { return _cellStyle; }
            set
            {
                _cellStyle = value;
                DrawCell.CellStyle = _cellStyle;
                if (Cells != null && IsCellStyleEnableColumnCellStyle)
                {
                    if (IsBottomColumn())
                    {
                        foreach (var item in Cells)
                        {
                            item.CellStyle = _cellStyle;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 是否是最底层Column
        /// </summary>
        /// <returns></returns>
        private bool IsBottomColumn()
        {
            return ChildColumns == null;
        }

        protected int _width = 100;
        public int Width
        {
            get { return _width; }
            set
            {
                _width = value;
                DrawCell.Width = _width;
                DrawCell.Rectangle.Width = _width;
                if (Cells != null)
                {
                    if (IsBottomColumn())
                    {
                        foreach (var item in Cells)
                        {
                            item.Width = _width;
                        }
                    }
                }
            }
        }

        protected int _height = 35;
        public int Height
        {
            get { return _height; }
            set
            {
                _height = value;
                DrawCell.Height = _height;
                DrawCell.Rectangle.Height = _height;
                if (Cells != null)
                {
                    if (IsBottomColumn())
                    {
                        foreach (var item in Cells)
                        {
                            item.Height = _height;
                        }
                    }
                }
            }
        }

        protected int _x;
        protected int _y;

        public int X
        {
            get { return _x; }
            set { _x = value; DrawCell.Location.X = _x; }
        }

        public int Y
        {
            get { return _y; }
            set { _y = value; DrawCell.Location.Y = _y; }
        }



        public string Name { get; set; }
        // 父ColumnIndex
        public int PColumnIndex;
        // Column排列第几个
        public int Index;
        public int RowIndex;
        public int PRowIndex = 0;

        protected List<Cell> _cells;
        /// <summary>
        /// 第几列单元格List(当不是底层的Column可以为空)
        /// </summary>
        public List<Cell> Cells
        {
            get { return _cells; }
            set
            {
                _cells = value;
                if (_cells != null)
                {
                    foreach (var item in _cells)
                    {
                        item.CellStyle = _cellStyle;
                        item.Type = _cellType;
                        item.Width = _width;
                        item.Height = _height;
                    }
                }
            }
        }

        private bool isVisible = true;
        /// <summary>
        /// 设置列是否可见(影响该列下的所有列和单元格)
        /// </summary>
        [Description("设置列是否可见(影响该列下的所有列和单元格)"), Browsable(true), Category("样式")]
        public bool IsVisible
        {
            get { return isVisible; }
            set
            {
                isVisible = value;
                SetColumnIsVisible(this, value);
            }
        }

        // 设置列和单元格的可见性
        private static void SetColumnIsVisible(Column column, bool value)
        {
            if (column != null)
            {
                if (column.ChildColumns != null)
                {
                    foreach (var item in column.ChildColumns)
                    {
                        item.IsVisible = value;
                   }
                }
                else
                {
                    if (column.Cells != null)
                    {
                        foreach (var item in column.Cells)
                        {
                            item.IsVisible = value;
                        }
                    }
                }
            }
        }

        protected bool _isChangeSize = false;
        /// <summary>
        /// 是否拖动改变大小
        /// </summary>
        public bool IsChangeSize
        {
            get { return _isChangeSize; }
            set
            {
                if (Cells != null)
                {
                    // 改变, 感觉需要静态委托对每个Column进行改变大小
                }
            }
        }


        /// <summary>
        /// 如果这个Column有ChildColumns 则之后样式有ChildColumns样式决定
        /// 底层Column为空
        /// </summary>
        public List<Column> ChildColumns { get; set; }

        /// <summary>
        /// 是否该列下的单元格默认启用该列的样式
        /// </summary>
        [Description("是否该列下的单元格默认启用该列的样式"), Browsable(true), Category("样式")]
        public bool IsCellStyleEnableColumnCellStyle { get; private set; }
    }
}
