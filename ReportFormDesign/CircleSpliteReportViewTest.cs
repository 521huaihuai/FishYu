using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ReportFormDesign.Adapter;
using ReportFormDesign.DrawUtils;
using ReportFormDesign.Model;
using ReportFormDesign.ReportViewPanel;

namespace ReportFormDesign
{
    public partial class CircleSpliteReportViewTest : Form
    {
        public CircleSpliteReportViewTest()
        {
            InitializeComponent();
        }

        private void CircleSpliteReportViewTest_Load(object sender, EventArgs e)
        {
            List<DataModel> list = new List<DataModel>();
            for (int i = 2; i > 0; i--)
            {
                list.Add(new ThreeTextAndDataModel("编号 1", 20, "编号 2", 30, "编号 3", 40));
            }
            ViewPanel.padding = 60;
            ViewPanel.isVerticalShowData = false;
            ReportViewAdapter adapter = new SimapleListViewAdapter();
            //设置数据源
            adapter.setData(list);
            //用于设置起始位置和宽高
            adapter.setBasePostitionRect(new AreaPositionRect(10, 20, 110, 120));
            circle_Splite_ReportView1.setAdapter(adapter);
            circle_Splite_ReportView1.OnMouseMove_ReportViewPanelEvent += OnMouseMoveCallBack;
        }

        private void OnMouseMoveCallBack(DataModel data)
        {
            Console.WriteLine("string = {0}, data ={1}", data.mainText, data.mainData);
        }
    }
}
