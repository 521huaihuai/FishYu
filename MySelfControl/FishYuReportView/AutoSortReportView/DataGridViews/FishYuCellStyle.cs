/* ======================================================================== 
* 本类功能概述:
* 
* 作者：zjm 
* 时间：2018/1/13 14:21:28 
* 文件名：FishYuCellStyle 
* 版本：V1.0.1 
* 
* 修改者： 时间： 
* 修改说明： 
* ======================================================================== 
*/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace FishyuSelfControl.FishYuReportView.AutoSortReportView.DataGridViews
{
    /// <summary>
    /// 位置显示
    /// </summary>
    public enum Alignment
    {
        MiddleCenter, MiddleLeft, MiddleRight
    }

    public class FishYuCellStyle
    {

        public Alignment Alignment { get; set; }

        private Color _backColor = Color.White;
        /// <summary>
        /// 背景色
        /// </summary>
        public Color BackColor { get { return _backColor; } set { _backColor = value; } }

        private Font _font = new Font("Consolas", 8);
        /// <summary>
        /// 字体
        /// </summary>
        public Font Font { get { return _font; } set { _font = value; } }


        private Color _foreColor = Color.Black;
        /// <summary>
        /// 字体前景色
        /// </summary>
        public Color ForeColor { get { return _foreColor; } set { _foreColor = value; } }


        private Color _selectBackColor = AbstractReportView.perferBlue;
        /// <summary>
        /// 选中后的颜色
        /// </summary>
        public Color SelectBackColor { get { return _selectBackColor; } set { _foreColor = value; } }


        private Color _selectForeColor = AbstractReportView.perferWhite;
        /// <summary>
        /// 选中后的字体颜色
        /// </summary>
        public Color SelectForeColor { get { return _selectForeColor; } set { _selectForeColor = value; } }
    }
}
