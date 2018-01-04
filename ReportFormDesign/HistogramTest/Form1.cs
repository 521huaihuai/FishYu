using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ReportFormDesign.Adapter;
using ReportFormDesign.DataModels;
using ReportFormDesign.DrawUtils;
using ReportFormDesign.Model;
using ReportFormDesign.Models.DataModels;
using ReportFormDesign.ReportViewPanel;
using ReportFormDesign.ToolTips;

namespace HistogramTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitCoordinateMultiRadiusRectAngleReportView();
            InitCircle();
            InitPirchart();
        }

        public void InitPirchart()
        {
            pie_Chart_SingleReportView1.Title = "属性不完整量";
            pie_Chart_SingleReportView1.TextAndData = new string[] { "无坐标", "6580", "无属性", "680", "无子属性", "890" };
            pie_Chart_SingleReportView1.IsShowViewByAnimalioned = true;
            pie_Chart_SingleReportView1.initData();
        }

        public void InitCoordinateMultiRadiusRectAngleReportView()
        {
            List<DataModel> Models = new List<DataModel>();
            Models.Add(new DataModel("1月", 1500, ReportViewUtils.PerferColors[0]));
            Models.Add(new DataModel("1月", 1000, ReportViewUtils.PerferColors[1]));
            Models.Add(new DataModel("2月", 500, ReportViewUtils.PerferColors[0]));
            Models.Add(new DataModel("2月", 800, ReportViewUtils.PerferColors[1]));
            Models.Add(new DataModel("3月", 500, ReportViewUtils.PerferColors[0]));
            Models.Add(new DataModel("3月", 300, ReportViewUtils.PerferColors[1]));
            Models.Add(new DataModel("4月", 1000, ReportViewUtils.PerferColors[0]));
            Models.Add(new DataModel("4月", 700, ReportViewUtils.PerferColors[1]));
            Models.Add(new DataModel("", 0));
            coordinateMultiGroupRadiusRectAngleReportView1.MyToolTip =new CoordinateMultiToolTip();
            coordinateMultiGroupRadiusRectAngleReportView1.TitlesName.Add("2016 year");
            coordinateMultiGroupRadiusRectAngleReportView1.TitlesName.Add("2017 year");
            coordinateMultiGroupRadiusRectAngleReportView1.initData(Models);
        }

        public void InitCircle()
        {
            List<DataModel> list = new List<DataModel>();
            list.Add(new AutoSortDataModel("百丈", 330, 1000, ReportViewUtils.PerferColors[0]));
            list.Add(new AutoSortDataModel("黄湖", 704, 1000, ReportViewUtils.PerferColors[1]));
            list.Add(new AutoSortDataModel("瓶窑", 091, 1000, ReportViewUtils.PerferColors[3]));
            list.Add(new AutoSortDataModel("仓前", 120, 1000, ReportViewUtils.PerferColors[2]));
            list.Add(new AutoSortDataModel("南苑", 156, 1000, ReportViewUtils.PerferColors[2]));
            list.Add(new AutoSortDataModel("百丈", 330, 1000, ReportViewUtils.PerferColors[0]));
            list.Add(new AutoSortDataModel("黄湖", 704, 1000, ReportViewUtils.PerferColors[1]));
            list.Add(new AutoSortDataModel("瓶窑", 091, 1000, ReportViewUtils.PerferColors[3]));
            list.Add(new AutoSortDataModel("仓前", 120, 1000, ReportViewUtils.PerferColors[2]));
            list.Add(new AutoSortDataModel("南苑", 156, 1000, ReportViewUtils.PerferColors[2]));
            list.Add(new AutoSortDataModel("余杭", 791, 1000, ReportViewUtils.PerferColors[3]));
            list.Add(new AutoSortDataModel("百丈", 330, 1000, ReportViewUtils.PerferColors[0]));
            list.Add(new AutoSortDataModel("黄湖", 704, 1000, ReportViewUtils.PerferColors[1]));
            list.Add(new AutoSortDataModel("临平", 814, 1000, ReportViewUtils.PerferColors[2]));
            list.Add(new AutoSortDataModel("余杭", 791, 1000, ReportViewUtils.PerferColors[3]));
            list.Add(new AutoSortDataModel("临平", 814, 1000, ReportViewUtils.PerferColors[2]));
            list.Add(new AutoSortDataModel("乔司", 156, 1000, ReportViewUtils.PerferColors[2]));
            histogram_Rectangle_ReportView1.PropertyName.Add("部门");
            histogram_Rectangle_ReportView1.PropertyName.Add("6月办理数");
            histogram_Rectangle_ReportView1.initData(list);
        }
    }
}
