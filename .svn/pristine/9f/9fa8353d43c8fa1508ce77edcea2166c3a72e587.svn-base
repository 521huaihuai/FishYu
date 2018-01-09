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
using ReportFormDesign.ReportViewPanel;

namespace ReportFormDesign
{
    public partial class MainForm : Form
    {
        private int index = 60;
        public MainForm()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //initPanelBackColor();
            initTableBackColor();
            initArcSplite();
            initRadiusRectAngle();
            initMultiRectAngle();
            initSpeedOfProgress();
            initCircleSplite();
            initCircleSplite2();
            initMultiArcSplte();
            initCoordinateView();
            initRankingView();
        }

        private void initTableBackColor()
        {
            tableLayoutPanel1.BackColor = ReportViewUtils.perferShallowGray;
            tableLayoutPanel2.BackColor = ReportViewUtils.perferShallowGray;
            tableLayoutPanel4.BackColor = ReportViewUtils.perferShallowGray;
            tableLayoutPanel5.BackColor = ReportViewUtils.perferShallowGray;
            tableLayoutPanel6.BackColor = ReportViewUtils.perferShallowGray;
            tableLayoutPanel7.BackColor = ReportViewUtils.perferShallowGray;
            tableLayoutPanel8.BackColor = ReportViewUtils.perferShallowGray;
            tableLayoutPanel9.BackColor = ReportViewUtils.perferShallowGray;
            tableLayoutPanel10.BackColor = ReportViewUtils.perferShallowGray;
            tableLayoutPanel11.BackColor = ReportViewUtils.perferShallowGray;
            tableLayoutPanel21.BackColor = ReportViewUtils.perferShallowGray;
        }

        private void initRankingView()
        {
            List<DataModel> list = new List<DataModel>();
            list.Add(new DataModel("杭州", 94));
            list.Add(new DataModel("上海", 90));
            list.Add(new DataModel("绍兴", 91));
            list.Add(new DataModel("金华", 98));
            list.Add(new DataModel("临安", 87));
            radius_Rectangle_ReportView3.initData(list);



            radius_Rectangle_ReportView4.initData(null);
        }

        private void initCoordinateView()
        {
            coordinatePointReportView1.Title = "";
            coordinatePointReportView1.initData(null);


            coordinatePointReportView2.Title = "";
            coordinatePointReportView2.LableX = "当前实时数据";
            coordinatePointReportView2.LableY = "";
            List<DataModel>  CoordinateModels = new List<DataModel>();
            CoordinateModels.Add(new DataModel("", 50));
            CoordinateModels.Add(new DataModel("", 100));
            CoordinateModels.Add(new DataModel("", 150));
            CoordinateModels.Add(new DataModel("", 200));
            CoordinateModels.Add(new DataModel("", 200));
            CoordinateModels.Add(new DataModel("", 200));
            CoordinateModels.Add(new DataModel("时间07", 230));
            CoordinateModels.Add(new DataModel("时间07", 20));
            CoordinateModels.Add(new DataModel("时间07", 100));
            CoordinateModels.Add(new DataModel("时间07", 240));
            CoordinateModels.Add(new DataModel("时间07", 10));
            coordinatePointReportView2.initData(CoordinateModels);


            coordinateMultiFillPathReportView1.Title = "";
            coordinateMultiFillPathReportView1.LableX = "";
            coordinateMultiFillPathReportView1.LableY = "";
            coordinateMultiFillPathReportView1.initData();


            List<DataModel> Models = new List<DataModel>();
            Models.Add(new DataModel("异常检测", 150, ReportViewUtils.PerferColors[0]));
            Models.Add(new DataModel("异常待定", 100, ReportViewUtils.PerferColors[0]));
            Models.Add(new DataModel("异常", 50, ReportViewUtils.PerferColors[0]));
            Models.Add(new DataModel("质量检测", 80, ReportViewUtils.PerferColors[1]));
            Models.Add(new DataModel("质量合格", 50, ReportViewUtils.PerferColors[1]));
            Models.Add(new DataModel("质量出错", 30, ReportViewUtils.PerferColors[1]));
            Models.Add(new DataModel("推送", 100, ReportViewUtils.PerferColors[2]));
            Models.Add(new DataModel("推送成功", 70, ReportViewUtils.PerferColors[2]));
            Models.Add(new DataModel("推送失败", 30, ReportViewUtils.PerferColors[2]));
            coordinateMultiRadiusRectAngleReportView1.initData(Models);
        }

        private void initMultiRectAngle()
        {
            List<DataModel> list = new List<DataModel>();
            for (int i = 100; i > 0; i--)
            {
                list.Add(new AutoSortDataModel("fga阿斯蒂芬东方闪电风动旛动啊fsdfd空打飞000" + i, (int)(0 + i * 0.5), 100, ReportViewUtils.PerferColors[i % 3]));
            }
            radius_Rectangle_ReportView2.TextLocation = TextLocations.up;
            radius_Rectangle_ReportView2.initData(list);
        }

        private void initRadiusRectAngle()
        {
            List<DataModel> list = new List<DataModel>();
            for (int i = 100; i > 0; i--)
            {
                list.Add(new AutoSortDataModel("fga阿斯蒂芬东方闪电风动旛动啊fsdfd空打飞000" + i, (int)(0 + i * 0.5), 100, ReportViewUtils.PerferColors[i % 5]));
            }
            //radius_Rectangle_ReportView1.Height = 2000;
            radius_Rectangle_ReportView1.padding = 20;
            radius_Rectangle_ReportView1.TextShowCount = 13;
            radius_Rectangle_ReportView1.TextLeaveOutIsEnd = true;
            radius_Rectangle_ReportView1.LeftPadding = 20;
            radius_Rectangle_ReportView1.TextAndTextPadding = 3;
            radius_Rectangle_ReportView1.StrokenWidth = 5;
            radius_Rectangle_ReportView1.TextSize = 8;
            radius_Rectangle_ReportView1.DataSize = 10;
            radius_Rectangle_ReportView1.MyToolTip.TextSize = 7;
            radius_Rectangle_ReportView1.MyToolTip.TextColor = Color.Black;
            radius_Rectangle_ReportView1.TextLocation = TextLocations.up;
            radius_Rectangle_ReportView1.TextShowCount = 6;
            radius_Rectangle_ReportView1.IsDrawRatio = true;
            radius_Rectangle_ReportView1.LineColor = ReportViewUtils.perferBlue;
            radius_Rectangle_ReportView1.TextColor = ReportViewUtils.perferWhite;
            radius_Rectangle_ReportView1.DataColor = ReportViewUtils.perferWhite;
            ReportViewAdapter adapter = new SimpleListViewAdapter();
            //设置数据源
            adapter.setData(list);
            //用于设置起始位置
            adapter.setBasePostitionRect(new AreaPositionRect(0, 20, 300, 35));
            radius_Rectangle_ReportView1.setAdapter(adapter);//设置panel的高度

        }

        private void initMultiArcSplte()
        {
            multiArc_Splite_SingleReportView1.Title = "报备";
            multiArc_Splite_SingleReportView1.TextAndData = new string[] { "今日报备", "600", "今日报备申请", "300", "申请未通过", "60" };
            multiArc_Splite_SingleReportView1.StartAngle = 90;
            multiArc_Splite_SingleReportView1._MyAnimalion.AnimalionTime = 15;
            multiArc_Splite_SingleReportView1.MaxNum = 1000;
            multiArc_Splite_SingleReportView1.IsCalculateDependByMaxNum = true;
            multiArc_Splite_SingleReportView1.InitAnimalThask();
            multiArc_Splite_SingleReportView1.OnNotifyAnimalDataChageEvent += multiArc_Splite_SingleReportView1_OnNotifyAnimalDataChageEvent;
            multiArc_Splite_SingleReportView1.initData();
        }

        string[] multiArc_Splite_SingleReportView1_OnNotifyAnimalDataChageEvent()
        {
            return new string[] { "今日报备", "1000", "今日报备申请", "700", "申请未通过", "100" };
        }

        private void initCircleSplite2()
        {
            circle_Splite_SingleReportView2.Title = "数据不规范量";
            circle_Splite_SingleReportView2.TextAndData = new string[] { "数据一", "20", "数据二", "30", "数据三", "50", "数据四", "80" };
            circle_Splite_SingleReportView1.IsShowViewByAnimalioned = true;
            circle_Splite_SingleReportView2.initData();
        }

        private void initCircleSplite()
        {
            circle_Splite_SingleReportView1.Title = "属性不完整量";
            circle_Splite_SingleReportView1.TextAndData = new string[] { "无坐标", "6580", "无属性", "890", "无子属性", "680"};
            //circle_Splite_SingleReportView1._MyAnimalion.AnimalionTime = 20;
            circle_Splite_SingleReportView1.IsShowViewByAnimalioned = true;
            circle_Splite_SingleReportView1.initData();
        }

        private void initArcSplite()
        {
            arcSplite_SingleReportView1.Title = "90";
            arcSplite_SingleReportView1.TextAndData = new string[] { "平台接入", "90", "同比上升", "4" };
            arcSplite_SingleReportView1.MaxNum = 100;
            arcSplite_SingleReportView1.TextSize = 10;
            arcSplite_SingleReportView1.initData();

            arcSplite_SingleReportView2.Title = "60";
            arcSplite_SingleReportView2.TextAndData = new string[] { "完善率", "60", "同比上升", "4" };
            arcSplite_SingleReportView2.MaxNum = 100;
            arcSplite_SingleReportView2.TextSize = 10;
            arcSplite_SingleReportView2.initData();

            arcSplite_SingleReportView3.Title = "30";
            arcSplite_SingleReportView3.TextAndData = new string[] { "平台接入", "30", "同比上升", "4" };
            arcSplite_SingleReportView3.MaxNum = 100;
            arcSplite_SingleReportView3.TextSize = 10;
            arcSplite_SingleReportView3.initData();
        }


        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (this.Focused)
            {
                base.OnMouseWheel(e);
            }
        }

        private void initSpeedOfProgress()
        {
            //speedOfProgress_SingleReportView1.Title = "计划表";
            speedOfProgress_SingleReportView1.TextAndData = new string[] { "当前数据", "70" };
            //speedOfProgress_SingleReportView1._Interpolation.Value = 1f;
            //speedOfProgress_SingleReportView1.AnimalionTextSize = 14;
            //speedOfProgress_SingleReportView1.DataColor = ReportViewUtils.perferBlue;
            //speedOfProgress_SingleReportView1.MyAnimalColor = ReportViewUtils.perferBlue;
            //speedOfProgress_SingleReportView1.DataSize = 14;
            ////动画持续时间
            //speedOfProgress_SingleReportView1._MyAnimalion.AnimalionTime = 4;
            speedOfProgress_SingleReportView1.IsNotAllowShowAnimalion = false;
            speedOfProgress_SingleReportView1.initData();
            speedOfProgress_SingleReportView1.InitAnimalThask();
            //自动检测时间间隔
            //speedOfProgress_SingleReportView1.OnNotifyAnimalDataChageEvent += speedOfProgress_SingleReportView1_OnNotifyDataChageEvent;
        }

        string[] speedOfProgress_SingleReportView1_OnNotifyDataChageEvent()
        {
            index++;
            return new string[] { "当前数据", "" + index };
        }

    }
}
