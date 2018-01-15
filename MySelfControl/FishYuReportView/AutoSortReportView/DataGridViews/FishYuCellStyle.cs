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
using FishyuSelfControl.FishYuReportView.AutoSortReportView.DataGridViews.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

    [TypeConverter(typeof(CellStyleConverter))] //自定义类型转换器
    public class FishYuCellStyle
    {

        public FishYuCellStyle()
        {
        }

        public FishYuCellStyle(Alignment alignment, Color backColor, Font font, Color foreColor, Color selectBackColor, Color selectForeColor)
        {
            this.Alignment = alignment;
            this.BackColor = backColor;
            this.Font = font;
            this.ForeColor = foreColor;
            this.SelectBackColor = selectBackColor;
            this.SelectForeColor = selectForeColor;
        }

        /// <summary>
        /// 默认样式
        /// </summary>
        private static FishYuCellStyle _fishYuCellStyle = new FishYuCellStyle();
        public static FishYuCellStyle DefaultCellStyle { get { return _fishYuCellStyle; } }


        /// <summary>
        /// 展示边距
        /// </summary>
        [Description("展示边距"), Browsable(true), Category("样式")]
        public Alignment Alignment { get; set; }

        private Color _backColor = Color.White;
        /// <summary>
        /// 背景色
        /// </summary>
        [Description("背景色"), Browsable(true), Category("样式")]
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
        [Description("字体前景色"), Browsable(true), Category("样式")]
        public Color ForeColor { get { return _foreColor; } set { _foreColor = value; } }


        private Color _selectBackColor = AbstractReportView.perferBlue;
        /// <summary>
        /// 选中后的颜色
        /// </summary>
        [Description("选中后的颜色"), Browsable(true), Category("样式")]
        public Color SelectBackColor { get { return _selectBackColor; } set { _foreColor = value; } }


        private Color _selectForeColor = AbstractReportView.perferWhite;
        /// <summary>
        /// 选中后的字体颜色
        /// </summary>
        [Description("选中后的字体颜色"), Browsable(true), Category("样式")]
        public Color SelectForeColor { get { return _selectForeColor; } set { _selectForeColor = value; } }
    }
}
