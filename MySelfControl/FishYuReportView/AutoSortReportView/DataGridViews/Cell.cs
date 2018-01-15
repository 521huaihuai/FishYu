/* ======================================================================== 
* 本类功能概述:单元格
* 
* 作者：zjm 
* 时间：2018/1/13 14:47:47 
* 文件名：Cell 
* 版本：V1.0.1 
* 
* 修改者： 时间： 
* 修改说明： 
* ======================================================================== 
*/
using FinshYuUtils.DrawUtils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace FishyuSelfControl.FishYuReportView.AutoSortReportView.DataGridViews
{
    public enum CellType
    {
        Text, Image, CheckBox, SelfDefine
    }

    public class Cell
    {

        public Cell()
        {
            Rectangle.Location = Location;
        }

        public Cell(Rectangle rectangle, object tag = null)
        {
            Location = rectangle.Location;
            Width = rectangle.Width;
            Height = rectangle.Height;
            Rectangle = rectangle;
            Tag = tag;
        }

        public Cell(int x, int y, int width, int height, object tag = null)
        {
            Location = new Point(x, y);
            Width = width;
            Height = height;
            Rectangle = new Rectangle(Location, new Size(width, height));
            Tag = tag;
        }

        // 第j列
        public int ColumnIndex;
        // 第i行
        public int RowIndex;
        // 第几个
        public int Index;


        /// <summary>
        /// 存储对象
        /// </summary>
        public object Tag { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public object Value { get; set; }



        public int Width { get; set; }
        public int Height { get; set; }
        public Point Location = new Point();
        public Rectangle Rectangle = new Rectangle();

        /// <summary>
        /// 是否可见(合并单元格时被设置不可见)
        /// </summary>
        public bool IsVisible = true;

        /// <summary>
        /// 是否拖动改变大小
        /// </summary>
        public bool IsChangeSize = false;

        /// <summary>
        /// 是否被选中
        /// </summary>
        public bool IsSelected { get; set; }

        /// <summary>
        /// 单元格展示的View
        /// </summary>
        public CellType Type { get; set; }

        /// <summary>
        /// 单元格样式
        /// </summary>
        public FishYuCellStyle CellStyle { get; set; }

        protected List<Cell> _roundCellList = new List<Cell>();
        /// <summary>
        /// 四周的单元格
        /// </summary>
        public List<Cell> AllRoundCells { get { return _roundCellList; } set { _roundCellList = value; } }


        /// <summary>
        /// 自定义绘制Cell的接口
        /// </summary>
        public ICellSelfDefineView iCellSelfDefineView = null;


        /// <summary>
        /// 是否在单元格内
        /// </summary>
        /// <param name="point">鼠标的位置</param>
        /// <param name="cell"></param>
        /// <returns></returns>
        public static bool IsInRectangle(Point point, Cell cell)
        {
            if (cell != null &&  cell.Rectangle.Contains(point))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 绘制单元格
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="cell"></param>
        public static void DrawCell(Graphics graphics, Cell cell)
        {
            if (cell == null)
            {
                return;
            }
            if (cell.CellStyle == null)
            {
                cell.CellStyle = FishYuCellStyle.DefaultCellStyle;
            }
            // 如果是可见的或者未被合并单元格的
            if (cell.IsVisible)
            {
                switch (cell.Type)
                {
                    case CellType.Text:
                        switch (cell.CellStyle.Alignment)
                        {
                            case Alignment.MiddleCenter:
                                DrawText(graphics, cell, LocationModel.Location_Center);
                                break;
                            case Alignment.MiddleLeft:
                                DrawText(graphics, cell, LocationModel.Location_Left);
                                break;
                            case Alignment.MiddleRight:
                                DrawText(graphics, cell,
                                    LocationModel.Location_Right);
                                break;
                            default:
                                DrawText(graphics, cell, LocationModel.Location_Center);
                                break;
                        }
                        break;
                    case CellType.Image:
                        switch (cell.CellStyle.Alignment)
                        {
                            case Alignment.MiddleCenter:
                                break;
                            case Alignment.MiddleLeft:
                                break;
                            case Alignment.MiddleRight:
                                break;
                            default:
                                break;
                        }
                        break;
                    case CellType.CheckBox:
                        switch (cell.CellStyle.Alignment)
                        {
                            case Alignment.MiddleCenter:
                                break;
                            case Alignment.MiddleLeft:
                                break;
                            case Alignment.MiddleRight:
                                break;
                            default:
                                break;
                        }
                        break;
                    case CellType.SelfDefine:
                        if (cell.iCellSelfDefineView != null)
                        {
                            cell.iCellSelfDefineView.DrawSelfDefineCell(graphics, cell);
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private static void DrawText(Graphics graphics, Cell cell, LocationModel locationModel)
        {
            if (cell.IsSelected)
            {
                // 绘制背景
                using (Brush backBrush = new SolidBrush(cell.CellStyle.SelectBackColor))
                {
                    graphics.FillRectangle(backBrush, cell.Rectangle);
                }
                // 绘制
                using (Brush brush = new SolidBrush(cell.CellStyle.SelectForeColor))
                {
                    DrawUtil.DrawString(graphics, locationModel, cell.Value + "", cell.CellStyle.Font, brush, cell.Rectangle);
                }
            }
            else
            {
                // 绘制背景
                using (Brush backBrush = new SolidBrush(cell.CellStyle.BackColor))
                {
                    graphics.FillRectangle(backBrush, cell.Rectangle);
                }
                using (Brush brush = new SolidBrush(cell.CellStyle.ForeColor))
                {
                    DrawUtil.DrawString(graphics, locationModel, cell.Value + "", cell.CellStyle.Font, brush, cell.Rectangle);
                }
            }
        }
    }
}
