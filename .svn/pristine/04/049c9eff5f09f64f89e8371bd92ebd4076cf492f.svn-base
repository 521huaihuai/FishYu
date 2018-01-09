using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ReportFormDesign.Adapter;
using ReportFormDesign.DataModels;
using ReportFormDesign.DrawUtils;
using ReportFormDesign.Model;
using ReportFormDesign.Models.GridViewModels;
using ReportFormDesign.ToolTips;

namespace ReportFormDesign.ReportViewPanel.SelfDefineReportView.DataGridView
{
    public class DataGridReportView : SelfDefineReportView
    {
        private AreaPositionRect AreaRect;
        private List<Row> RowList = new List<Row>();
        private Label label = new Label();
        private int rowHeight = 40;


        public DataGridReportView()
        {
            LeftPadding = 5;
            RightPadding = 5;
            TopPadding = 5;
            BottomPadding = 5;
            BackGroundAlpha = 100;
            isSelfDefineReportView = true;
            //MyToolTip = new FollowPopViewForTitle();
            //MyToolTip.BackGroundColor = ReportViewUtils.perferWhite_Shallow;
            //MyToolTip.Padding = 20;
            //MyToolTip.TextSize = 8;
            //MyToolTip.DataSize = 8;
            //MyToolTip.TextColor = Color.Black;
            //MyToolTip.DataColor = ReportViewUtils.perferRed;
            label.Text = "";
        }

        public void initData(List<Row> rowList)
        {
            RowList.Clear();
            this.RowList = rowList;
            AreaRect = new AreaPositionRect(EStartX, EStartY, EStartX + EViewWidth, EStartY + rowHeight);
            AreaRect.PaddingIn = 0;
            List<DataModel> list = new List<DataModel>();
            int index = 0;
            foreach (var item in RowList)
            {
                item.RowHeight = AreaRect.Height;
                item.RowWidth = AreaRect.Width;
                list.Add(new AutoSortDataModel(item.Columns[0].Content + "", 0, 100, ReportViewUtils.PerferColors[index % 5]));
                foreach (var column in item.Columns)
                {
                    item.AddColumn(column);
                }
                index++;
            }
            //是否展示滑动条
            if (IsShowScrollBar)
            {
                if (this.Parent is Panel)
                {
                    Panel panel = this.Parent as Panel;
                    if (this.Created && this.IsHandleCreated && !this.IsDisposed)
                    {
                        try
                        {
                            this.Invoke((EventHandler)delegate
                            {
                                panel.AutoScroll = true;
                                panel.Controls.Add(label);
                                label.Location = new Point(0, EStartY + rowHeight * rowList.Count + 2 * rowHeight);
                            });
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
            }
            ReportViewAdapter adapter = new SimpleListViewAdapter();
            //设置数据源
            adapter.setData(list);
            //用于设置起始位置
            adapter.setBasePostitionRect(AreaRect);
            setAdapter(adapter);//设置panel的高度
            this.Invalidate();
        }


        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
           // this.Focus();
           // return;
        }


        public override void ResizeReportViewChange()
        {
            //LeftPadding = 5;
            //RightPadding = 5;
            EViewWidth = Width - LeftPadding - RightPadding;
            EViewHeight = Height - TopPadding - BottomPadding;
            foreach (var item in RowList)
            {
                item.RowWidth = EViewWidth;
                item.RefreshCellSize();
            }
        }

        public override void ReLocationModelPos()
        {
            EStartX = LeftPadding;
            EStartY = TopPadding;
            if (MyToolTip is CoordinateHelpLineToolTip)
            {
                CoordinateHelpLineToolTip tool = MyToolTip as CoordinateHelpLineToolTip;
                tool.CoordinateWidth = EViewWidth;
                tool.CoordinateHeight = EViewHeight;
                tool.CoordinateStartX = EStartX;
                tool.CoordinateStartY = EStartY + EViewHeight - BottomPadding;
            }
            AreaRect = new AreaPositionRect(EStartX, EStartY, EStartX + EViewWidth, EStartY + RowHeight);
            AreaRect.PaddingIn = 0;
            if (adapter != null)
            {
                adapter.setIndex(-1);
                adapter.setBasePostitionRect(AreaRect);
            }
        }

        public override void DrawSelfDefine(System.Drawing.Graphics g, System.Drawing.Pen linePen, System.Drawing.Brush lineBrush, System.Drawing.Brush TextBrush, System.Drawing.Brush DataBrush, System.Drawing.Font FontText, System.Drawing.Font FontData)
        {
            int y = EStartY;
            for (int i = 0; i < RowList.Count; i++)
            {
                int height = RowList[i].RowHeight;
                foreach (Column item in RowList[i].Columns)
                {
                    int x = (int)(EStartX + item.ColStartX);
                    linePen.Color = Color.FromArgb(100, LineColor.R, LineColor.G, LineColor.B);
                    g.DrawLine(linePen, x, y, x, y + height);
                    ReportViewUtils.drawString(g, item.TextLocation, item.Content + "", FontText, TextBrush, x, y, item.ColWidth, height);
                }
                //绘制最后一列
                Column last = RowList[i].Columns[RowList[i].Columns.Count - 1];
                int lastX = last.ColStartX + last.ColWidth + EStartX;
                g.DrawLine(linePen, lastX, y, lastX, y + height);
                //绘制一行
                g.DrawLine(linePen, EStartX, y, EStartX + EViewWidth, y);
                //计算下一行的起始位置
                y += RowList[i].RowHeight;
            }
            if (RowList.Count == 0)
            {
                return;
            }
            //绘制最后一行
            g.DrawLine(linePen, EStartX, y, EStartX + RowList[RowList.Count - 1].RowWidth, y);
        }

        public override void OnRenderNormalView(System.Drawing.Graphics g, Model.DataModel data, System.Drawing.Pen linePen, System.Drawing.Brush lineBrush, System.Drawing.Brush TextBrush, System.Drawing.Brush DataBrush, System.Drawing.Font TextFont, System.Drawing.Font DataFont)
        {
            ///建议把绘制的直接写在这里
            if (data.Area.IsMouseIn)
            {
                Color color = data.ModelColor;
                Brush myBrush = new SolidBrush(Color.FromArgb(BackGroundAlpha, color.R, color.G, color.B));
                g.FillRectangle(myBrush, new Rectangle(data.Area.left, data.Area.top,
                     EViewWidth, data.Area.bottom - data.Area.top));
                myBrush.Dispose();
            }
        }

        public override void RefreshDataChange(List<Model.DataModel> list)
        {
        }

        #region 动画绘制
        public override void OnRenderAnimalionedView(System.Drawing.Graphics g, Model.DataModel data, System.Drawing.Pen linePen, System.Drawing.Brush lineBrush, System.Drawing.Brush TextBrush, System.Drawing.Brush DataBrush, System.Drawing.Font FontText, System.Drawing.Font FontData)
        {
        }

        public override object[] AnimalionPrepare()
        {
            return null;
        }

        public override void AnimalionDraw(System.Drawing.Graphics graphics, object[] AnimalionPrePareArgs)
        {
        }

        public override void AnimalionEnd(System.Drawing.Graphics graphics)
        {
        }
        #endregion


        /// <summary>
        /// 它的父层必须是Panel才生效
        /// </summary>
        public bool IsShowScrollBar { get; set; }
        public int BackGroundAlpha { get; set; }
        public int RowHeight
        {
            get
            {
                return rowHeight;
            }
            set
            {
                this.rowHeight = value;
            }
        }
    }
}
