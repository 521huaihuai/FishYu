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
    public partial class ReportViewTestForm : Form, ICellSelfDefineView, FishyuSelfControl.FishYuReportView.AutoSortReportView.DataGridViews.IDataAdapter
    {

        public ReportViewTestForm()
        {
            InitializeComponent();

        }

        public void DrawSelfDefineCell(Graphics graphics, Cell cell)
        {
            Self self = new Self();
            self.Location = cell.Rectangle.Location;
            self.Width = cell.Width;
            self.Height = cell.Height - 1;
            self.Top = 1;
            fishYuDataGridView2.Controls.Add(self);
            using (Pen pen = new Pen(Color.Black))
            {
                graphics.DrawRectangle(pen, cell.Rectangle);
            }

        }

        public void SetData(object data, Dictionary<string, Cell> cells)
        {
            List<string>  mm = data as List<string>;
            cells["cell1"].Rectangle.X += 100;
            cells["cell1"].CellStyle = new FishYuCellStyle();
            cells["cell1"].Value = mm[0];
            cells["cell2"].Value = mm[1];
            cells["cell2"].Rectangle.X += 100;
            cells["cell3"].Value = mm[2];
            cells["cell3"].Rectangle.X += 100;
        }

        private void ReportViewTestForm_Load(object sender, EventArgs e)
        {
            List<Column> columns = new List<Column>();
            #region 一级目录
            FishYuCellStyle cellStyle = new FishYuCellStyle();
            cellStyle.BackColor = Color.Yellow;
            cellStyle.Font = new Font("宋体", 10);
            columns.Add(fishYuDataGridView1.ColumnFactoryInstance.CreateTitleColumn("tes", 0, 0, 0, 200, 35, cellStyle));
            columns.Add(fishYuDataGridView1.ColumnFactoryInstance.CreateTitleColumn("名称1", 1, 0, 0, 200));
            columns.Add(fishYuDataGridView1.ColumnFactoryInstance.CreateTitleColumn("名称2", 2, 0, 0));
            columns.Add(fishYuDataGridView1.ColumnFactoryInstance.CreateTitleColumn("名称3", 3, 0, 0));
            #endregion

            #region 二级
            columns.Add(fishYuDataGridView1.ColumnFactoryInstance.CreateTitleColumn("名称1-1", 0, 1, 0));
            columns.Add(fishYuDataGridView1.ColumnFactoryInstance.CreateTitleColumn("名称1-2", 1, 1, 0));

            columns.Add(fishYuDataGridView1.ColumnFactoryInstance.CreateTitleColumn("名称2-1", 2, 1, 1, 50));
            columns.Add(fishYuDataGridView1.ColumnFactoryInstance.CreateTitleColumn("名称2-2", 3, 1, 1, 50));
            columns.Add(fishYuDataGridView1.ColumnFactoryInstance.CreateTitleColumn("名称2-1", 4, 1, 1, 50));
            columns.Add(fishYuDataGridView1.ColumnFactoryInstance.CreateTitleColumn("名称2-2", 5, 1, 1, 50));

            columns.Add(fishYuDataGridView1.ColumnFactoryInstance.CreateTitleColumn("名称3-1", 6, 1, 2, 50));
            columns.Add(fishYuDataGridView1.ColumnFactoryInstance.CreateTitleColumn("名称3-2", 7, 1, 2, 50));

            columns.Add(fishYuDataGridView1.ColumnFactoryInstance.CreateTitleColumn("名称4-2", 8, 1, 3, 100));
            #endregion

            #region 三级
            columns.Add(fishYuDataGridView1.ColumnFactoryInstance.CreateTitleColumn("名称1-1", 0, 2, 0, 25));
            columns.Add(fishYuDataGridView1.ColumnFactoryInstance.CreateTitleColumn("名称1-2", 1, 2, 0, 25));
            columns.Add(fishYuDataGridView1.ColumnFactoryInstance.CreateTitleColumn("名称1-3", 2, 2, 0, 25));
            columns.Add(fishYuDataGridView1.ColumnFactoryInstance.CreateTitleColumn("名称1-4", 3, 2, 0, 25));


            columns.Add(fishYuDataGridView1.ColumnFactoryInstance.CreateTitleColumn("名称1-1", 4, 2, 1, 25));
            columns.Add(fishYuDataGridView1.ColumnFactoryInstance.CreateTitleColumn("名称1-2", 5, 2, 1, 25));
            columns.Add(fishYuDataGridView1.ColumnFactoryInstance.CreateTitleColumn("名称1-3", 6, 2, 1, 25));
            columns.Add(fishYuDataGridView1.ColumnFactoryInstance.CreateTitleColumn("名称1-4", 7, 2, 1, 25));

            columns.Add(fishYuDataGridView1.ColumnFactoryInstance.CreateTitleColumn("名称1-1", 8, 2, 2, 25));
            columns.Add(fishYuDataGridView1.ColumnFactoryInstance.CreateTitleColumn("名称1-2", 9, 2, 2, 25));
            columns.Add(fishYuDataGridView1.ColumnFactoryInstance.CreateTitleColumn("名称1-1", 10, 2, 3, 25));
            columns.Add(fishYuDataGridView1.ColumnFactoryInstance.CreateTitleColumn("名称1-2", 11, 2, 3, 25));

            columns.Add(fishYuDataGridView1.ColumnFactoryInstance.CreateTitleColumn("名称1-1", 12, 2, 4, 25));
            columns.Add(fishYuDataGridView1.ColumnFactoryInstance.CreateTitleColumn("名称1-2", 13, 2, 4, 25));

            columns.Add(fishYuDataGridView1.ColumnFactoryInstance.CreateTitleColumn("名称1-1", 14, 2, 5, 25));
            columns.Add(fishYuDataGridView1.ColumnFactoryInstance.CreateTitleColumn("名称1-2", 15, 2, 5, 25));

            columns.Add(fishYuDataGridView1.ColumnFactoryInstance.CreateTitleColumn("名称1-1", 16, 2, 6, 25));
            columns.Add(fishYuDataGridView1.ColumnFactoryInstance.CreateTitleColumn("名称1-2", 17, 2, 6, 25));

            columns.Add(fishYuDataGridView1.ColumnFactoryInstance.CreateTitleColumn("名称1-1", 18, 2, 7, 25));
            columns.Add(fishYuDataGridView1.ColumnFactoryInstance.CreateTitleColumn("名称1-2", 19, 2, 7, 25));

            columns.Add(fishYuDataGridView1.ColumnFactoryInstance.CreateTitleColumn("名称1-1", 20, 2, 8, 25));
            columns.Add(fishYuDataGridView1.ColumnFactoryInstance.CreateTitleColumn("名称1-2", 21, 2, 8, 25));
            columns.Add(fishYuDataGridView1.ColumnFactoryInstance.CreateTitleColumn("名称1-1", 22, 2, 8, 25));
            columns.Add(fishYuDataGridView1.ColumnFactoryInstance.CreateTitleColumn("名称1-2", 23, 2, 8, 25));
            #endregion

            fishYuDataGridView1.TitleColumns = fishYuDataGridView1.ColumnFactoryInstance.SortColumns(columns);
            //fishYuDataGridView1.InitDefaultDataGridView();




            columns = new List<Column>();
            columns.Add(fishYuDataGridView1.ColumnFactoryInstance.CreateTitleColumn("名称", 0, 0, 0, 100, 140, cellStyle));

            columns.Add(fishYuDataGridView1.ColumnFactoryInstance.CreateTitleColumn("名称", 1, 0, 0, 200, 35, cellStyle));


            columns.Add(fishYuDataGridView1.ColumnFactoryInstance.CreateTitleColumn("名称", 0, 1, 1, 100, 35, cellStyle));
            columns.Add(fishYuDataGridView1.ColumnFactoryInstance.CreateTitleColumn("名称2-1", 0, 2, 0, 100, 35, cellStyle));
            columns.Add(fishYuDataGridView1.ColumnFactoryInstance.CreateTitleColumn("名称3-1", 0, 3, 0, 100, 35, cellStyle));

            columns.Add(fishYuDataGridView1.ColumnFactoryInstance.CreateCellColumn("Cell2-1", "cell1", 1, 1, 1, 100, 17, cellStyle));
            columns.Add(fishYuDataGridView1.ColumnFactoryInstance.CreateCellColumn("Cell2-1", "cell2", 1, 2, 1, 100, 18, cellStyle));
            columns.Add(fishYuDataGridView1.ColumnFactoryInstance.CreateCellColumn("Cell3-1", "cell3", 1, 3, 1, 100, 17, cellStyle));
            columns.Add(fishYuDataGridView1.ColumnFactoryInstance.CreateCellColumn("Cell3-1", "cell4", 1, 4, 1, 100, 18, cellStyle));
            columns.Add(fishYuDataGridView1.ColumnFactoryInstance.CreateCellColumn("Cell3-1", "cell5", 1, 5, 1, 100, 17, cellStyle));
            columns.Add(fishYuDataGridView1.ColumnFactoryInstance.CreateCellColumn("Cell3-1", "cell6", 1, 6, 1, 100, 18, cellStyle));

            cellStyle = new FishYuCellStyle();
            cellStyle.BackColor = FishYuDataGridView.perferPink;
            columns.Add(fishYuDataGridView1.ColumnFactoryInstance.CreateTitleColumn("特殊", 2, 0, 0, 200, 35, cellStyle, CellType.SelfDefine, this));
            fishYuDataGridView2.TitleColumns = fishYuDataGridView1.ColumnFactoryInstance.SortColumns(columns);

            fishYuDataGridView2.SetAdapter(this);
            List<List<string>> list = new List<List<string>>();
            list.Add(new List<string>() { "1", "2", "3" });
            fishYuDataGridView2.SetList(list);
        }
    }
}
