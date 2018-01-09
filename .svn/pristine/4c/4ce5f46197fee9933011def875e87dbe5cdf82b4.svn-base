using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WindowsApplication15
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void rowMergeView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            List<DataGridViewRow> list = new List<DataGridViewRow>();
            DataGridViewRow dgv = new DataGridViewRow();
            list.Add(dgv);
            dgv.CreateCells(rowMergeView1);
            dgv.Cells[id.Index].Value = 1;
            dgv.Cells[mc.Index].Value = "huaihuai";
            dgv.Cells[nr.Index].Value = "nr";
            dgv.Cells[bh.Index].Value = "2014";

            dgv = new DataGridViewRow();
            list.Add(dgv);
            dgv.CreateCells(rowMergeView1);
            dgv.Cells[id.Index].Value = 1;
            dgv.Cells[mc.Index].Value = "huaihuai";
            dgv.Cells[nr.Index].Value = "nr";
            dgv.Cells[bh.Index].Value = "2014";

            rowMergeView1.Rows.AddRange(list.ToArray());
        }
    }
}
