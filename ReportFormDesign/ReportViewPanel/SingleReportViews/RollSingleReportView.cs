using ReportFormDesign.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace ReportFormDesign.ReportViewPanel.SingleReportViews
{
    public class RollSingleReportView : SingleReportView
    {
        private float RollStartX;
        private SizeF textSizef;
        private bool IsMouseIn;

        public RollSingleReportView()
        {
            _MyToolTip = null;
            //InitAnimalThask();
            RollText = "这是公告展示!";
            animalion.IsPrepareAnimaled = true;
            animalion.AnimalionTime = 180;
            StrokenWidth = 5;
            TextSize = 7;
            IsDrawFrame = false;
            IsHorizontally = false;
            IsAutoReSizeView = false;
            MaxIndex = 100000;
        }

        public override void LegendNoteSizeChanged()
        {
        }

        public override void OnRenderNormalView(System.Drawing.Graphics g, Model.DataModel data, System.Drawing.Pen linePen, System.Drawing.Brush lineBrush, System.Drawing.Brush TextBrush, System.Drawing.Brush DataBrush, System.Drawing.Font TextFont, System.Drawing.Font DataFont)
        {
        }

        public override object[] AnimalionPrepare(DetailDataModel drawModel)
        {
            try
            {
                if (!string.IsNullOrEmpty(RollText))
                {
                    textSizef = graphics.MeasureString(rollText, myAnimalFont);
                }
            }
            catch (Exception)
            {
            }

            RollStartX = EViewWidth;
            return null;
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            animalion.IsPrepareAnimaled = true;
            IsMouseIn = true;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            IsMouseIn = false;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            //base.OnSizeChanged(e);
            EViewWidth = Width;
            EViewHeight = Height;
        }

        public override void AnimalionDraw(System.Drawing.Graphics g, DetailDataModel data, System.Drawing.Pen pen, System.Drawing.Brush brush, System.Drawing.Font font, object[] args)
        {
            try
            {
                if (!IsMouseIn)
                {
                    RollStartX = RollStartX - _Interpolation.Value;
                }
                if (textSizef != null && RollStartX + textSizef.Width < 0)
                {
                    RollStartX = EViewWidth;
                }
                if (textSizef != null)
                {
                    g.DrawString(rollText, font, brush, RollStartX, (EViewHeight - textSizef.Height) / 2);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Second exception.", e.Message);
            }
            //ReportViewUtils.drawString(graphics, LocationModel.Location_Right_Right, "你好, 大幅度发发酵哦大家爱哦大范甘迪 大哥家的感觉爱逛街肉购入价格个人机构", font, brush, -Index, EStartY, EViewWidth, EViewHeight);
        }

        public override void AnimalionEnd(System.Drawing.Graphics g, DetailDataModel data, System.Drawing.Pen pen, System.Drawing.Brush brush, System.Drawing.Font font)
        {
            animalion.IsPrepareAnimaled = true;
        }

        public override DataModels.LegendNoteModel[] GetLegendNotes()
        {
            return null;
        }

        public override void ResizeReportViewChange()
        {
        }
        private string rollText;
        public string RollText { 
            get
            {
                return rollText;
            }

            set
            {
                StringBuilder sb = new StringBuilder();
                string[] strs = value.Split('\n');
                foreach (var item in strs)
                {
                    sb.Append(item);
                }
                rollText = sb.ToString();
            }
        }

    }
}
