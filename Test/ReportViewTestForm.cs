/* ======================================================================== 
* 本类功能概述:
* 
* 作者：zjm 
* 时间：2018/1/15 14:51:14 
* 文件名：ReportViewTestForm 
* 版本：V1.0.1 
* 
* 修改者： 时间： 
* 修改说明： 
* ======================================================================== 
*/
using FishyuSelfControl.FishYuReportView.AutoSortReportView.DataGridViews;
using FishyuSelfControl.FishYuReportView.AutoSortReportView.DataGridViews.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test
{
    public partial class ReportViewTestForm : Form
    {

        public ReportViewTestForm()
        {
            InitializeComponent();
            
        }

        private void ReportViewTestForm_Load(object sender, EventArgs e)
        {
            List<Column> columns = new List<Column>();
            columns.Add(fishYuDataGridView1.ColumnFactoryInstance.CreateTitleColumn("名称", 0, 0, 0));
            columns.Add(fishYuDataGridView1.ColumnFactoryInstance.CreateTitleColumn("名称1", 1, 0, 0));
            columns.Add(fishYuDataGridView1.ColumnFactoryInstance.CreateTitleColumn("名称2", 2, 0, 0));
            columns.Add(fishYuDataGridView1.ColumnFactoryInstance.CreateTitleColumn("名称3", 3, 0, 0));
            fishYuDataGridView1.TitleColumns = fishYuDataGridView1.ColumnFactoryInstance.SortColumns(columns);
            fishYuDataGridView1.InitDataGridView();
        }
    }
}
