using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using ReportFormDesign.DrawUtils;
using ReportFormDesign.Legendnotes;
using ReportFormDesign.Model;
using ReportFormDesign.ReportViewPanel.SelfDefineReportView.CoordinateReportViews;

namespace ReportFormDesign.ReportViewPanel.SelfDefineReportView.CoordinateMultiModelsReportViews
{
    public class CoordinateMultiGroupRadiusRectAngleReportView : CoordinateReportView, InterfaceLegendnote
    {
        #region 字段
        private int indexer;
        public static List<List<DataModel>> ALLGroup = new List<List<DataModel>>();
        private List<DataModel> GroupData = new List<DataModel>();

        /// <summary>
        /// 一队中每组对应的标题
        /// </summary>
        public List<string> TitlesName = new List<string>();

        //用于标识数据是否为一队
        private Color ModelColor;
        private bool isFirst = true;

        #endregion
        public CoordinateMultiGroupRadiusRectAngleReportView()
        {
            IsDrawCurve = false;
            TextRotate = 0;
            //IsShowX_Scale = false;
            LableX = "";
            LableY = "";
            IsRectAngle = true;
            LableSize = 10;
            LableFont = new Font("Arial", LableSize);
            IsLableFontBold = true;
            isSelfDefineReportView = true;
            IsCoordinateReportView = true;
            this.interfaceLegendnote = this;
            LegendNoteWidth = 20;
            LegendNoteHeight = 10;
            IsShowX_Scale = false;
        }

        public override void ResizePadding()
        {
            TopPadding = (int)(1.0f * EViewHeight / 3);
        }

        public override DrawUtils.AreaPositionRect ReLoadAreaPosition(int CenterX, int CenterY, int PaddingX, int PaddingY)
        {
            indexer++;
            return new DrawUtils.AreaPositionRect(CenterX, CenterY, CenterX + PaddingX / 2, CoordinateStartY);
        }

        public override void OnRenderNormalView(System.Drawing.Graphics g, Model.DataModel data, System.Drawing.Pen linePen, System.Drawing.Brush lineBrush, System.Drawing.Brush TextBrush, System.Drawing.Brush DataBrush, System.Drawing.Font TextFont, System.Drawing.Font DataFont)
        {
            Model.DataModel dataPoint = new Model.DataModel();
            int x = CoordinateStartX + CoordinateWidth;
            int width = 0;

            if(IsGroup(data))
            {
                //装载rectangle数据结构的队列
                Queue rectangleQueue = new Queue();
                Rectangle item = new Rectangle();
                //装载color数据结构的队列
                Queue colorQueue = new Queue();

                foreach (Model.DataModel datas in GroupData)
                {
                    //绘制柱值
                    item = new Rectangle(datas.Area.left, datas.Area.top, datas.Area.Width, datas.Area.Height);
                    if (IsDrawDetailData)
                    {
                        //ReportViewUtils.drawString(g, LocationModel.Location_Right_Right, CoordinateDataModelBean.Y_Data[i] + "", FontData, DataBrush, StartX, StartY, LeftPadding, padding);
                        g.DrawString(datas.mainData + "", DataFont, DataBrush, (item.X), item.Y - 2 * DataSize);
                    }
                    rectangleQueue.Enqueue(item);
                    colorQueue.Enqueue(datas.ModelColor);

                    if(x > datas.Area.left)
                    {
                        x = datas.Area.left;
                    }
                    else
                    {
                        width = datas.Area.right - x;
                    }
                }
                foreach (Model.DataModel aimData in GroupData)
                {
                    OnRenderMultiView(g, aimData, rectangleQueue, colorQueue, width, x);
                }
                GroupData = new List<DataModel>();
                GroupData.Add(data);
            }
        }

        /// <summary>
        /// 多组数据的柱形绘制
        /// </summary>
        /// <param name="g"></param>
        /// <param name="data"></param>
        /// <param name="rectange"></param>
        /// <param name="linePen"></param>
        /// <param name="lineBrush"></param>
        /// <param name="TextBrush"></param>
        /// <param name="DataBrush"></param>
        /// <param name="TextFont"></param>
        /// <param name="DataFont"></param>
        private void OnRenderMultiView(System.Drawing.Graphics g, DataModel data, Queue rectange, Queue color, int width, int left)
        {
            Rectangle item = new Rectangle();
            Brush bs = new SolidBrush(data.ModelColor);
            Brush bb = new SolidBrush(Color.FromArgb(100, 210, 210, 210));
            if (IsRadiusRectAngle)
            {
                GraphicsPath path = ReportViewUtils.CreateRoundedRectanglePath(item, data.Area.Width / 4);

                if (data.Area.IsMouseIn)
                {
                    while (rectange.Count != 0)
                    {
                        bs = new SolidBrush((Color)color.Dequeue());
                        item = (Rectangle)rectange.Dequeue();
                        g.FillRectangle(bs, item);
                    }
                    
                    g.FillRectangle(bb, left, CoordinateStartY - CoordinateHeight, width, CoordinateHeight);
                }
                else
                {
                    while (rectange.Count != 0)
                    {
                        bs = new SolidBrush((Color)color.Dequeue());
                        item = (Rectangle)rectange.Dequeue();
                        g.FillRectangle(bs, item);
                    }
                }

                path.Dispose();
            }
            else
            {
                if (data.Area.IsMouseIn)
                {
                    while (rectange.Count != 0)
                    {
                        bs = new SolidBrush((Color)color.Dequeue());
                        item = (Rectangle)rectange.Dequeue();
                        g.FillRectangle(bs, item);
                    }
                    g.FillRectangle(bb, left, CoordinateStartY - CoordinateHeight, width, CoordinateHeight);
                }
                else
                {
                    while (rectange.Count != 0)
                    {
                        bs = new SolidBrush((Color)color.Dequeue());
                        item = (Rectangle)rectange.Dequeue();
                        g.FillRectangle(bs, item);
                    }
                }
            }
            bb.Dispose();
            bs.Dispose();
        }

        /// <summary>
        /// 接收的数据是否为一队的数据
        /// 是,则加入List;否,则返回true
        /// </summary>
        /// <param name="data"></param>
        /// <param name="GroupData"></param>
        /// <returns></returns>
        private bool IsGroup(DataModel data)
        {
            if (!isFirst && data.ModelColor == ModelColor)
            {
                isFirst = true;
                ALLGroup.Add(GroupData);
                return true;
            }

            if (data.ModelColor != ModelColor)
            {
                int distance = data.Area.PaddingIn - MultiPadding;
                data.Area.left -= distance;
                data.Area.right -= distance;
            }
            GroupData.Add(data);
            isFirst = false;
            return false;
        }

        public override void OnRenderAnimalionedView(System.Drawing.Graphics g, DataModel data, System.Drawing.Pen linePen, System.Drawing.Brush lineBrush, System.Drawing.Brush TextBrush, System.Drawing.Brush DataBrush, System.Drawing.Font FontText, System.Drawing.Font FontData)
        {
        }

        public override object[] AnimalionPrepare()
        {
            //throw new NotImplementedException();
            return null;
        }

        public override void AnimalionDraw(System.Drawing.Graphics graphics, object[] AnimalionPrePareArgs)
        {
        }

        public override void AnimalionEnd(System.Drawing.Graphics graphics)
        {
        }

        #region 存根属性
        /// <summary>
        /// 展示的图形是否是圆角矩形(默认false)
        /// </summary>
        public bool IsRadiusRectAngle { get; set; }

        /// <summary>
        /// 一队数据中每组直方柱的间距
        /// </summary>
        public int MultiPadding { get; set; }

        /// <summary>
        /// LabelFont 默认"Arial"
        /// </summary>
        public System.Drawing.Font LableFont { get; set; }

        #endregion

        public void drawLegendnote(Graphics g, Pen linePen, Brush lineBrush, Brush TextBrush, Brush DataBrush, Font FontText, Font FontData)
        {
            if (CoordinateModels == null)
            {
                return;
            }
            //设定默认的初始颜色
            if (CoordinateModels != null && CoordinateModels.Count > 0)
            {
                ModelColor = CoordinateModels[0].ModelColor;
            }
            foreach (DataModel item in CoordinateModels)
            {
                if (IsGroup(item))
                {
                    GroupData = new List<DataModel>();
                    GroupData.Add(item);
                }
            }
            List<Rectangle> position = GetAllRectangle();
            //圆角矩形标签绘制
            for (int i = 0; i < position.Count; i++)
            {
                ReportViewUtils.drawString(g, LocationModel.Location_Left_Left, TitlesName[i], FontData, TextBrush, position[i].X + LegendNoteWidth, position[i].Y - LegendNoteHeight, (Width - EViewWidth) / 2, LegendNoteHeight + padding);
                GraphicsPath path = ReportViewUtils.CreateRoundedRectanglePath(position[i], LegendNoteHeight / 2);
                Brush bbsh = new SolidBrush(ALLGroup[0][i].ModelColor);
                g.FillPath(bbsh, path);
                bbsh.Dispose();
                path.Dispose();
            }

            // 绘制围绕点旋转的文本  
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;
            for(int i=0; i < ALLGroup.Count;i++)
            {
                DrawRoateText.DrawString(g, ALLGroup[i][0].mainText, LableFont, TextBrush, new PointF(ALLGroup[i][0].Area.left + MultiPadding / 2, CoordinateStartY + padding), format, TextRotate);
            }

            ALLGroup = new List<List<DataModel>>();
            GroupData = new List<DataModel>();
        }

        private List<Rectangle> GetAllRectangle()
        {
            List<Rectangle> list = new List<Rectangle>();
            Rectangle rect;
            if (ALLGroup.Count > 0)
            {
                for (int i = 0; i < ALLGroup[0].Count; i++)
                {
                    rect = new Rectangle(CoordinateStartX + CoordinateWidth - 2 * LegendNoteWidth, CoordinateStartY - CoordinateHeight + (LegendNoteHeight + padding) * (i) + padding / 2, LegendNoteWidth, LegendNoteHeight);
                    list.Add(rect);
                }
            }
            return list;
        }

        //[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Content)]
        //[System.ComponentModel.Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design",
        //    "System.Drawing.Design.UITypeEditor, System.Drawing")]
        //public struct Titlestruct
        //{
        //    string DataTitle;
        //    Color DataColor;
        //}

    }
}
