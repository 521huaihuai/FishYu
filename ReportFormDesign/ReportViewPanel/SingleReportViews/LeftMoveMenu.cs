using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using ReportFormDesign.DataModels;
using ReportFormDesign.Model;
using System.Windows.Forms;

namespace ReportFormDesign.ReportViewPanel.SingleReportViews
{
    public class LeftMoveMenu : SingleReportView
    {
        public ImageList ImageList { get; set; }
        private List<PictureBox> PList = new List<PictureBox>();
        private bool isRight;
        private Point OldPosition;

        public LeftMoveMenu()
        {
            animalion.AnimalionTime = 5;
        }

        public void InitDataAndView()
        {
            
            if (ImageList != null && ImageList.Images.Count > 0)
            {
                FlowLayoutPanel flowPanel = new FlowLayoutPanel();
                flowPanel.Height = this.Height;
                flowPanel.Width = ImageList.Images[0].Width;
                flowPanel.Location = new Point(Width - flowPanel.Width, 0);
                //flowPanel.Dock = DockStyle.Right;
                this.Controls.Add(flowPanel);
                flowPanel.BringToFront();
                int index = 0;
                foreach (Image item in ImageList.Images)
                {
                    PictureBox box = new PictureBox();
                    box.MouseClick += Box_MouseClick;
                    box.Image = item;
                    box.Width = item.Width;
                    box.Height = item.Height;
                    box.Tag = index;
                    flowPanel.Controls.Add(box);
                    PList.Add(box);
                    index++;
                }
                OldPosition = this.Location;
                //确定宽高,以及位置
                //int perHeight = (this.Height - TopPadding - BottomPadding - MenuPadding * (PList.Count - 1)) / PList.Count;
                foreach (PictureBox item in PList)
                {
                    
                }
            }
        }

        private void Box_MouseClick(object sender, MouseEventArgs e)
        {
            if (OnMenuClickEvent != null)
            {
                OnMenuClickEvent(sender, e, int.Parse((sender as PictureBox).Tag + ""));
            }
            if (!isRight)
            {
                if (!isNotAllowShowAnimalion)
                {
                    this.animalion.IsPrepareAnimaled = true;
                    isRight = true;
                }
                else
                {
                    this.Location = new Point(2, Location.Y);
                    isRight = true;
                }
            }
            else
            {
                this.Location = OldPosition;
                isRight = false;
            }
        }

        public override void DrawSelfDefineView(Graphics g, Pen linePen, Brush lineBrush, Brush TextBrush, Brush DataBrush, Font FontText, Font FontData)
        {
            //base.DrawSelfDefineView(g, linePen, lineBrush, TextBrush, DataBrush, FontText, FontData);

        }

        public override void AnimalionDraw(Graphics g, DetailDataModel data, Pen pen, Brush brush, Font font, object[] args)
        {
            this.Location = new Point((int)(Location.X + _Interpolation.Value), Location.Y);
        }

        public override void AnimalionEnd(Graphics g, DetailDataModel data, Pen pen, Brush brush, Font font)
        {
        }

        public override object[] AnimalionPrepare(DetailDataModel drawModel)
        {
            return null;
        }

        public override LegendNoteModel[] GetLegendNotes()
        {
            return null;
        }

        public override void LegendNoteSizeChanged()
        {
        }

        public override void OnRenderNormalView(Graphics g, DataModel data, Pen linePen, Brush lineBrush, Brush TextBrush, Brush DataBrush, Font TextFont, Font DataFont)
        {
        }

        public override void ResizeReportViewChange()
        {
        }
        public event OnMenuClick OnMenuClickEvent;
    }
    public delegate void OnMenuClick(object sender, MouseEventArgs e, int Index);
}
