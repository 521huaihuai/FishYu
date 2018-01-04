using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using ReportFormDesign.DrawUtils;
using ReportFormDesign.Model;

namespace ReportFormDesign.ReportViewPanel.SingleReportViews
{
    public class RadiusRectangle_SingleReportView : SingleReportView
    {
        public RadiusRectangle_SingleReportView()
        {
            _MyToolTip = new FollowPopViewForTitle();
            _MyToolTip.TextColor = ReportViewUtils.perferRed;
            _MyToolTip.DataColor = ReportViewUtils.perferRed;
            StrokenWidth = 5;
            TextSize = 7;
            //animalion.IsAllowDrawAnimal = false;
            //radiusRectangle_SingleReportView1.MaxIndex = 60;
            //radiusRectangle_SingleReportView1._Interpolation.Value = 2;
            //IsAutoReSize = false;
        }

        public override void LegendNoteSizeChanged()
        {
            EStartX = this.Width / 6;
            EViewWidth = 4 * this.Width / 6;
            EViewHeight = EViewWidth / 15;
        }

        public override void OnRenderNormalView(System.Drawing.Graphics g, Model.DataModel data, System.Drawing.Pen linePen, System.Drawing.Brush lineBrush, System.Drawing.Brush TextBrush, System.Drawing.Brush DataBrush, System.Drawing.Font TextFont, System.Drawing.Font DataFont)
        {
            //utils.drawReportView(g, RePortViewStyle.Arc_angle_rectangle, StartX, StartY, ViewWidth, ViewHeight, linePen.Color, TextBrush, DataBrush, "录像", 130, 200);

            //圆角的弧度
            int radius = EViewHeight / 2;
            float per = EViewWidth * 1.0f / MaxNum;
            float realShowData = per * int.Parse(TextAndData[1]);
            Rectangle rect = new Rectangle(EStartX, EStartY, EViewWidth, 2 * radius);
            Rectangle rectReal = new Rectangle(EStartX, EStartY, (int)realShowData, 2 * radius);

            //底色
            Brush selfBrush = new SolidBrush(Color.FromArgb(200, 88, 94, 92));
            GraphicsPath path = ReportViewUtils.CreateRoundedRectanglePath(rect, radius);
            //绘制底色
            g.FillPath(selfBrush, path);

            //绘制展示色
            path = ReportViewUtils.CreateRoundedRectanglePath(rectReal, radius);
            g.FillPath(lineBrush, path);

            //绘制字体
            ReportViewUtils.drawStringWithLimiteText(g, LocationModel.Location_Left_Left, TextAndData[0], TextFont, TextBrush, EStartX, EStartY - EViewHeight, EViewWidth / 2, EViewHeight, 8);

            //绘制右侧百分比
            float perNum = int.Parse(TextAndData[1]) * 1.0f / MaxNum * 100;
            string str = ((int)perNum).ToString();
            ReportViewUtils.drawStringWithLimiteText(g, LocationModel.Location_Right_Right, str + "%", TextFont, TextBrush, EStartX + EViewWidth / 2, EStartY - EViewHeight, EViewWidth / 2, EViewHeight, 8);


            selfBrush.Dispose();
        }

        public override object[] AnimalionPrepare(DetailDataModel drawModel)
        {
            return null;
        }

        public override void AnimalionDraw(System.Drawing.Graphics g, DetailDataModel data, System.Drawing.Pen pen, System.Drawing.Brush brush, System.Drawing.Font font, object[] args)
        {
        }

        public override void AnimalionEnd(System.Drawing.Graphics g, DetailDataModel data, System.Drawing.Pen pen, System.Drawing.Brush brush, System.Drawing.Font font)
        {
        }

        public override DataModels.LegendNoteModel[] GetLegendNotes()
        {
            return null;
        }

        public override void ResizeReportViewChange()
        {
        }
    }
}
