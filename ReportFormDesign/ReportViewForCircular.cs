using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using ReportFormDesign.Model;
using ReportFormDesign.DrawUtils;

namespace ReportFormDesign
{
    public partial class ReportViewForCircular : Form
    {

        ReportViewUtils utils = new ReportViewUtils();



        public ReportViewForCircular()
        {
            InitializeComponent();
            panel1.MouseMove += panel1_MouseMove;
        }

        void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            //throw new NotImplementedException();
            int x = e.X;
            int y = e.Y;
            //DataModel data = utils.mouseMove(x, y);
            //if (data != null)
            //{
            //    Console.WriteLine("string = {0}, data ={1}", data.mainText, data.mainData);
            //}
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

            Panel panel = (Panel)sender;
            Graphics g = e.Graphics;
            Rectangle rect = new Rectangle(0, 0, panel.Width, panel.Height);
            g.FillRectangle(new SolidBrush(ReportViewUtils.perferBlue_Deep), rect);

            //起始坐标
            int startX = 40;
            int startY = 40;
            //绘制宽高
            int ArcWidth = 200;
            int ArcHeight = 5;
            //绘制的颜色
            Color ArcColor = Color.FromArgb(255, 36, 169, 255);
            //字体颜色
            Brush TextBrush = new SolidBrush(DrawUtils.ReportViewUtils.perferWhite_Shallow);
            //数据颜色
            Brush DataBrush = new SolidBrush(Color.FromArgb(255, 253, 218, 4));


            //绘制弧形矩阵报表
            //utils.drawReportView(g, RePortViewStyle.Arc_angle_rectangle, startX, startY, ArcWidth, ArcHeight, ArcColor, TextBrush, DataBrush, "视频质量", 270, 300, "同步上升", 4, 8, 8);

            //ArcColor = ReportViewUtils.perferRed;
            //TextBrush = new SolidBrush(DrawUtils.ReportViewUtils.perferPurple);
            //DataBrush = new SolidBrush(Color.FromArgb(255, 253, 218, 4));
            //utils.drawReportView(g, RePortViewStyle.Arc_angle_rectangle, startX, startY + 50, ArcWidth, ArcHeight, ArcColor, TextBrush, DataBrush, "录像", 130, 200);
            //utils.drawReportView(g, RePortViewStyle.Arc_angle_rectangle, 40, 150, 200, 5, ReportViewUtils.perferYellow, "完好率", 100, 400, 8, 8);

            //绘制弧形有事件触发矩阵报表
            utils.drawReportView(g, RePortViewStyle.Arc_Angle_rectangle_EventHandle, startX, startY, ArcWidth, ArcHeight, ArcColor, TextBrush, DataBrush, "视频质量", 270, 300, "同步上升", 4, 8, 8);

            ArcColor = ReportViewUtils.perferRed;
            TextBrush = new SolidBrush(DrawUtils.ReportViewUtils.perferPurple);
            DataBrush = new SolidBrush(Color.FromArgb(255, 253, 218, 4));
            utils.drawReportView(g, RePortViewStyle.Arc_Angle_rectangle_EventHandle, startX, startY + 40, ArcWidth, ArcHeight, ArcColor, TextBrush, DataBrush, "录像", 130, 200);
            utils.drawReportView(g, RePortViewStyle.Arc_Angle_rectangle_EventHandle, 40, startY + 80, 200, 5, ReportViewUtils.perferYellow, "完好率0", 100, 400, 8, 8);
            utils.drawReportView(g, RePortViewStyle.Arc_Angle_rectangle_EventHandle, 40, startY + 120, 200, 5, ReportViewUtils.perferBlue, "完好率1", 100, 400, 8, 8);
            utils.drawReportView(g, RePortViewStyle.Arc_Angle_rectangle_EventHandle, 40, startY + 160, 200, 5, ReportViewUtils.perferGreen, "完好率2", 100, 400, 8, 8);
            utils.drawReportView(g, RePortViewStyle.Arc_Angle_rectangle_EventHandle, 40, startY + 200, 200, 5, ReportViewUtils.perferWhite, "完好率3", 100, 400, 8, 8);
            utils.drawReportView(g, RePortViewStyle.Arc_Angle_rectangle_EventHandle, 40, startY + 240, 200, 5, ReportViewUtils.perferPurple, "完好率4", 100, 400, 8, 8);


            //绘制多矩阵形式的报表
            //ReportViewUtils.padding = 25;
            //utils.drawReportView(g, RePortViewStyle.MultiRectangle, 10, 20, 15, 30, ArcColor, "一致", 50, 100, 9, 15);
            //utils.drawReportView(g, RePortViewStyle.MultiRectangle, 10, 70, 15, 30, ReportViewUtils.perferRed, "错误", 60, 100, 9, 15);
            //utils.drawReportView(g, RePortViewStyle.MultiRectangle, 10, 120, 15, 30, ReportViewUtils.perferYellow, "未知", 764, 1000, 9, 15);


            //绘制弧形矩形报表
            //ReportViewUtils.padding = 10;
            //utils.drawReportView(g, RePortViewStyle.Arc_rectangle, 10, 20, 100, 100, ArcColor, "平台接入", 150, 200, "同比下降", 4, 10, 16);
            //utils.drawReportView(g, RePortViewStyle.Arc_rectangle, 130, 20, 100, 100, ReportViewUtils.perferRed, "在线率", 100, 200, "同比下降", 4, 10, 16);
            //utils.drawReportView(g, RePortViewStyle.Arc_rectangle, 250, 20, 100, 100, ReportViewUtils.perferYellow, "故障点位", 180, 200, "同比上降", 4, 10, 16);


            ////绘制报表
            //DataBean data = DataBeanFactory.CreateData();
            //utils.DrawReportViewForCoordinate(g, ReportViewUtils.perferBlue, ReportViewUtils.perferWhite_Shallow, ReportViewUtils.perferPink, startX, startY, 500, 400, data);

            //DataBean data = DataBeanFactory.CreateTestData_02();
            //utils.DrawReportViewForCoordinate(g, ReportViewUtils.perferBlue, ReportViewUtils.perferWhite_Shallow, ReportViewUtils.perferPink, startX, startY, 500, 400, data, 12, 8, 0, 0, true, false,false, false);
            
            
            
            //释放资源
            utils.disposeGraphics(g);

        }

        private void ReportViewForCircular_Load(object sender, EventArgs e)
        {

          
        }
    }
}
