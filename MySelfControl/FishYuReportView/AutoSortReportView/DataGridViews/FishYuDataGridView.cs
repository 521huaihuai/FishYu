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
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FishyuSelfControl.FishYuReportView.AutoSortReportView.DataGridViews
{
    public partial class FishYuDataGridView : AbstractReportView, IView
    {
        public FishYuDataGridView()
        {
            InitializeComponent();
        }

        public void ChildPaint(Graphics g, Pen pen, Brush brush)
        {
        }
    }
}
