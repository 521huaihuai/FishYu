using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ReportFormDesign.Adapter;
using ReportFormDesign.Model;
using ReportFormDesign.DrawUtils;
using ReportFormDesign.ReportViewPanel;

namespace ReportFormDesign
{
    public partial class Radius_Rectangle_ReportView_ : Form, ReportViewMouseMoveCallBack
    {
        public Radius_Rectangle_ReportView_()
        {
            InitializeComponent();
        }

        private void Test_Load(object sender, EventArgs e)
        {
            List<DataModel> list = new List<DataModel>();
            for (int i = 200; i > 0; i--)
            {
                list.Add(new DataModel("0000" + i, (int)(0 + i * 0.5)));
            }
            //radius_Rectangle_ReportView1.Height = 2000;
            ViewPanel.padding = 20;
            ViewPanel.isVerticalShowData = true;
            radius_Rectangle_ReportView1.StrokenWidth = 5;
            radius_Rectangle_ReportView1.TextSize = 10;
            radius_Rectangle_ReportView1.DataSize = 10;
            radius_Rectangle_ReportView1.LineColor = ReportViewUtils.perferBlue;
            radius_Rectangle_ReportView1.TextColor = ReportViewUtils.perferWhite;
            radius_Rectangle_ReportView1.DataColor = ReportViewUtils.perferWhite;
            radius_Rectangle_ReportView1.BackGroundColor = ReportViewUtils.perferBlue_Deep;
            ReportViewAdapter adapter = new SimapleListViewAdapter();
            //设置数据源
            adapter.setData(list);
            //用于设置起始位置
            adapter.setBasePostitionRect(new AreaPositionRect(0, 20, 300, 38));
            radius_Rectangle_ReportView1.setAdapter(adapter);//设置panel的高度
            radius_Rectangle_ReportView1.OnMouseMove_ReportViewPanelEvent += OnMouseMoveCallBack;
            ///radius_Rectangle_ReportView1.setOnMouseMoveCallBack(this);
        }

        public void OnMouseMoveCallBack(DataModel data)
        {

            Console.WriteLine("string = {0}, data ={1}", data.mainText, data.mainData);
        }
    }
}
