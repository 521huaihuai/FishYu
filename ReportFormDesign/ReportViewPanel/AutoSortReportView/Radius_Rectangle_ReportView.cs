using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ReportFormDesign.CurrentPosition;
using ReportFormDesign.Model;
using ReportFormDesign.DrawUtils;
using System.Drawing.Drawing2D;
using ReportFormDesign.Adapter;
using ReportFormDesign.DataModels;

namespace ReportFormDesign.ReportViewPanel
{


    /// <summary>
    /// 字体位置
    /// </summary>
    public enum TextLocations
    {
        none, left,left_up_down, up, right, bottom, center
    }

    public enum RePortViewStyles
    {
        Arc_angle_rectangle, MultiRectangle,
        Ranking,
        SimpleText
    }


    /// <summary>
    /// 主要负责报表的绘制
    /// 以及popView的基本设置(不负责具体的popView绘制)
    /// </summary>
    public class Radius_Rectangle_ReportView : ViewPanel
    {
        private float oldSize;
        private int oldCount = 6;
        private int rank;
        private Label label;
        private int CurrentSpiltLength;
        private int rowHeight = 25;

        public Radius_Rectangle_ReportView()
        {

            MyToolTip = new FollowPopView();
            MyToolTip.BackGroundColor = ReportViewUtils.perferWhite_Shallow;
            MyToolTip.Padding = 20;
            MyToolTip.TextSize = 8;
            MyToolTip.DataSize = 8;
            MyToolTip.TextColor = Color.Black;
            MyToolTip.DataColor = ReportViewUtils.perferRed;

            //lable = new Label();
            //lable.Location = new Point(0, this.MaxHeight);
            //lable.Text = "";
            //lable.Width = 0;
            //this.Controls.Add(lable);

            BackGroundAlpha = 60;
            TextAndTextPadding = 4;
            TextShowCount = 10;
            MultiRectangleCount = 10;
            MultiPadding = 40;
            LeftPadding = 100;
            RightPadding = 100;
            padding = 20;
            TextLeaveOutIsEnd = true;
            StrokenWidth = 1;
            DataSize = 10;
            IsDrawRatio = true;
            IsDrawColorfulRectAngle = true;
        }


        public void initData(List<DataModel> list)
        {
            if (list == null)
            {
                Random rm = ReportViewUtils.CreateRandom();
                list = new List<DataModel>();
                for (int i = 200; i > 0; i--)
                {
                    list.Add(new AutoSortDataModel("ffas花开的军国的地方计算机对方啊fsdfd空打飞000" + i, (int)(rm.NextDouble() * 100), 100, ReportViewUtils.PerferColors[i % 5]));
                }
            }
            ///以后的自动刷新怎么办?????
            if (RePortViewStyle == RePortViewStyles.Ranking)
            {
                int count = list.Count;
                string[] textAndData = new string[count * 2];
                int k = 0;
                foreach (var item in list)
                {

                    textAndData[2 * k] = item.mainText;
                    textAndData[2 * k + 1] = item.mainData + "";
                    k ++;
                }
                textAndData = ReportViewUtils.SortTextAndData(textAndData);
                list.Clear();
                RankModel model = null;
                for (int i = 0; i < count; i++)
                {
                    model = new RankModel(textAndData[2 * i], int.Parse(textAndData[2 * i + 1]), i + 1);
                    model.ModelColor = ReportViewUtils.PerferColors[i % 5];
                    list.Add(model);
                }
            }
            
            //radius_Rectangle_ReportView1.Height = 2000;
            ReportViewAdapter adapter = new SimpleListViewAdapter();
            //设置数据源
            adapter.setData(list);
            //用于设置起始位置
            adapter.setBasePostitionRect(new AreaPositionRect(0, 20, this.Width, rowHeight + 20));
            setAdapter(adapter);//设置panel的高度
            //ShowScrollerBar();
            if (IsEnableScrollerBar)
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
                                label.Location = new Point(0, list.Count * rowHeight);
                            });
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            MultiPadding = (int)(Width * 1.0f / 30);
            if (IsAutoReSizeView)
            {
                if (TextLocation == TextLocations.up || TextLocation == TextLocations.bottom)
                {
                    LeftPadding = 40;
                    RightPadding = 40;
                }
                else
                {
                    LeftPadding = Width / 4;
                    RightPadding = Width / 4;
                }
            }
            if (Width > 600)
            {
                oldSize = MyToolTip.TextSize;
                oldCount = TextShowCount;
                MyToolTip.TextSize = 11f;
                if (TextLocation == TextLocations.center || TextLocation == TextLocations.up || TextLocation == TextLocations.bottom)
                {
                    //TextShowCount = 50;
                }
                this.VerticalScroll.Enabled = true;
            }
            else
            {
                this.VerticalScroll.Enabled = false;
                if (oldSize != 0)
                {
                    MyToolTip.TextSize = oldSize;
                    TextShowCount = oldCount;
                }
                
            }
            //if (adapter != null)
            //{
            //    int count = adapter.getCount();
            //    //设置高度
            //    Label lable = new Label();
            //    lable.Location = new Point(0, (ViewHeight + padding) * count);
            //    lable.Text = "";
            //    lable.Width = 0;
            //    this.Controls.Add(lable);
            //}
        }

        public override void childPaint(Graphics g, DataModel Data, Pen linePen, Brush lineBrush, Brush TextBrush, Brush DataBrush, System.Drawing.Font font_Text, System.Drawing.Font font_Data)
        {
            switch (RePortViewStyle)
            {
                case RePortViewStyles.Arc_angle_rectangle:
                    DrawArcRectAngle(g, Data, linePen, lineBrush, TextBrush, font_Text);
                    break;
                case RePortViewStyles.MultiRectangle:
                    DrawMultiRectAngel(g, Data, linePen, lineBrush, TextBrush, DataBrush, font_Text, font_Data);
                    break;
                case RePortViewStyles.Ranking:
                    DrawMultiRanking(g, Data, linePen, lineBrush, TextBrush, DataBrush, font_Text, font_Data);
                    break;
                case RePortViewStyles.SimpleText:
                    DrawMultiSimpleText(g, Data, linePen, lineBrush, TextBrush, DataBrush, font_Text, font_Data);
                    break;
                default:
                    break;
            }
        }

        private void DrawMultiSimpleText(Graphics g, DataModel Data, Pen linePen, Brush lineBrush, Brush TextBrush, Brush DataBrush, System.Drawing.Font font_Text, System.Drawing.Font font_Data)
        {
            if (Data is AutoSortDataModel)
            {
                AutoSortDataModel model = Data as AutoSortDataModel;
                EStartX = Data.Area.left + LeftPadding;
                //宽高
                EViewWidth = (this.Width - EStartX - RightPadding);
                EViewHeight = Data.Area.bottom - Data.Area.top;
                //底色
                Brush selfBrush = new SolidBrush(Color.FromArgb(200, 88, 94, 92));
               
                ///建议把绘制的直接写在这里
                if (Data.Area.IsMouseIn)
                {
                    Color color = Data.ModelColor;
                    Brush myBrush = new SolidBrush(Color.FromArgb(BackGroundAlpha, color.R, color.G, color.B));
                    g.FillRectangle(myBrush, new Rectangle(Data.Area.left, Data.Area.top - padding / 2,
                         this.Width, Data.Area.bottom - Data.Area.top + padding));
                    myBrush.Dispose();
                }
                //绘制字体
                string text = Data.mainText;
                string firstText = "";
                if (isOvertTextShowCount(g, text, font_Text))
                {
                    firstText = LimiteText(g, font_Text, text);
                    if (!IsTwoLineShow)
                    {
                        if (TextLeaveOutIsEnd)
                        {
                            text = firstText;
                        }
                        if (TextLocation == TextLocations.none)
                        {
                            TextLocation = TextLocations.center;
                        }
                        //ReportViewUtils.drawString(g, LocationModel.Location_Up_Right_Right, text, font_Text, TextBrush, Data.Area.left, Data.Area.top - TextAndTextPadding, LeftPadding, height);
                    }
                    else
                    {
                        if (CurrentSpiltLength <= text.Length)
                        {
                            text = text.Substring(CurrentSpiltLength);
                        }
                        else
                        {
                            text = "";
                        }
                        if (TextLocation == TextLocations.none)
                        {
                            TextLocation = TextLocations.center;
                        }
                    }
                }
                else
                {
                    if (TextLocation == TextLocations.none)
                    {
                        TextLocation = TextLocations.center;
                    }
                    firstText = text;
                    text = "";
                }
                //text = LimiteText(g, font_Text, text);
                DrawString(g, TextBrush, font_Text, Data, firstText, text);
                selfBrush.Dispose();
            }
        }

        private void DrawMultiRanking(Graphics g, DataModel Data, Pen linePen, Brush lineBrush, Brush TextBrush, Brush DataBrush, System.Drawing.Font font_Text, System.Drawing.Font font_Data)
        {
            if (Data is RankModel)
            {
                RankModel rankmodel = Data as RankModel;
                EStartX = rankmodel.Area.left + 10;
                EStartY = rankmodel.Area.top;
                //宽高
                EViewWidth = (this.Width - 20);
                EViewHeight = rankmodel.Area.bottom - rankmodel.Area.top;
                int rankWidth = EViewWidth / 8;
                //绘制其它色的个数
                if (IsDrawColorfulRectAngle)
                {
                    lineBrush.Dispose();
                    lineBrush = new SolidBrush(rankmodel.ModelColor);
                }
                //绘制排名
                Rectangle rect = new Rectangle(EStartX, EStartY, rankWidth, EViewHeight);
                GraphicsPath path = ReportViewUtils.CreateRoundedRectanglePath(rect, EViewHeight / 4);
                g.FillPath(lineBrush, path);
                ReportViewUtils.drawString(g, rankmodel.Index + "", font_Text, TextBrush, EStartX, EStartY, rankWidth, EViewHeight);
                path.Dispose();

                //绘制名称
                int startText = EStartX + rankWidth + 20;
                int rankTextWidth = EViewWidth / 3;
                string text = rankmodel.mainText;
                text = ReportViewUtils.LimiteText(g, font_Text, text, rankTextWidth);
                ReportViewUtils.drawString(g, text, font_Text, TextBrush, startText, EStartY, rankTextWidth, EViewHeight);

                //绘制数据
                startText += rankTextWidth;
                text = rankmodel.mainData + "";
                text = ReportViewUtils.LimiteText(g, font_Text, text, rankTextWidth);
                ReportViewUtils.drawString(g, text, font_Text, TextBrush, startText, EStartY, rankTextWidth, EViewHeight);

                ///建议把绘制的直接写在这里
                if (rankmodel.Area.IsMouseIn)
                {
                    Color color = rankmodel.ModelColor;
                    Brush myBrush = new SolidBrush(Color.FromArgb(BackGroundAlpha, color.R, color.G, color.B));
                    g.FillRectangle(myBrush, new Rectangle(rankmodel.Area.left, rankmodel.Area.top - padding / 2,
                         this.Width, rankmodel.Area.bottom - rankmodel.Area.top + padding));
                    myBrush.Dispose();
                }
            }
            
        }

        private void DrawMultiRectAngel(Graphics g, DataModel Data, Pen linePen, Brush lineBrush, Brush TextBrush, Brush DataBrush, System.Drawing.Font font_Text, System.Drawing.Font font_Data)
        {
            if (Data is AutoSortDataModel)
            {
                AutoSortDataModel model = Data as AutoSortDataModel;
                EStartX = Data.Area.left + LeftPadding;
                //宽高
                EViewWidth = (this.Width - EStartX - RightPadding);
                EViewHeight = Data.Area.bottom - Data.Area.top;
                TextLocation = TextLocations.left;
                //底色
                Brush selfBrush = new SolidBrush(Color.FromArgb(200, 88, 94, 92));
                string text = Data.mainText;
                text = LimiteText(g, font_Text, text);
                DrawString(g, TextBrush, font_Text, Data, text, "");
                //g.DrawString(Data.mainText, font_Text, DataBrush, startX + (padding - Text_InArc.Length * TextSize) / 2, startY + (height - TextSize) / 2);

                int perWidth = (int)(EViewWidth * 1.0f / MultiRectangleCount);
                //绘制其它色的个数
                int drawCount = (int)(Data.mainData * 1.0f / model.MaxData * MultiRectangleCount);
                if (IsDrawColorfulRectAngle)
                {
                    lineBrush.Dispose();
                    lineBrush = new SolidBrush(Data.ModelColor);
                }
                for (int i = 0; i < MultiRectangleCount; i++)
                {

                    Rectangle rect = new Rectangle(EStartX + (perWidth) * i + MultiPadding, Data.Area.top, perWidth - MultiPadding, EViewHeight);
                    if (i < drawCount)
                    {
                        g.FillRectangle(lineBrush, rect);
                    }
                    else
                    {
                        if (drawCount <= 0)
                        {
                            g.FillRectangle(selfBrush, rect);
                        }
                    }
                }
                int finalX = EStartX + padding * MultiRectangleCount + 2 * padding;
                TextLocation = TextLocations.right;
                DrawString(g, TextBrush, font_Data, Data, Data.mainData + " / " + model.MaxData, "");
                //g.DrawString("/" + MaxNum, font_Data, selfBrush, finalX + (Data_InArc.ToString().Length) * DataSize, startY + (height - DataSize * (Data_InArc.ToString().Length - 1)) / 2);
                ///建议把绘制的直接写在这里
                if (Data.Area.IsMouseIn)
                {
                    Color color = Data.ModelColor;
                    Brush myBrush = new SolidBrush(Color.FromArgb(BackGroundAlpha, color.R, color.G, color.B));
                    g.FillRectangle(myBrush, new Rectangle(Data.Area.left, Data.Area.top - padding / 2,
                         this.Width, Data.Area.bottom - Data.Area.top + padding));
                    myBrush.Dispose();
                }
                selfBrush.Dispose();
            }
        }

        private void DrawArcRectAngle(Graphics g, DataModel Data, Pen linePen, Brush lineBrush, Brush TextBrush, System.Drawing.Font font_Text)
        {
            if (Data is AutoSortDataModel)
            {
                AutoSortDataModel model = Data as AutoSortDataModel;
                ///建议把绘制的直接写在这里
                int PaddingStart_X = Data.Area.left + LeftPadding;
                //宽高
                int height = Data.Area.bottom - Data.Area.top;
                int width = this.Width - PaddingStart_X - 2 * padding - height;

                //int width = this.Width - newStart_X - 2 * padding;
                //圆角的弧度
                int radius = height / 2;
                float per = width * 1.0f / model.MaxData;
                float realShowData = per * Data.mainData;

                Rectangle rect = new Rectangle(PaddingStart_X, Data.Area.top, width + height, height);
                Rectangle rectReal = new Rectangle(PaddingStart_X, Data.Area.top, (int)realShowData + height, height);

                //Rectangle rect = new Rectangle(newStart_X, Data.Area.top, width, height);
                //Rectangle rectReal = new Rectangle(newStart_X, Data.Area.top, (int)realShowData, height);
                //底色
                Brush selfBrush = new SolidBrush(Color.FromArgb(200, 88, 94, 92));
                GraphicsPath path = ReportViewUtils.CreateRoundedRectanglePath(rect, radius);
                //绘制底色
                g.FillPath(selfBrush, path);

                if (IsDrawColorfulRectAngle)
                {
                    lineBrush.Dispose();
                    lineBrush = new SolidBrush(Data.ModelColor);
                }
                //绘制展示色
                path = ReportViewUtils.CreateRoundedRectanglePath(rectReal, radius);
                g.FillPath(lineBrush, path);

                //绘制字体
                string text = Data.mainText;
                string firstText = "";
                if (isOvertTextShowCount(g, text, font_Text))
                {
                    if (TextShowCount < text.Length)
                    {
                        firstText = LimiteText(g, font_Text, text);
                    }
                    else
                    {
                        firstText = text;
                    }

                    if (!IsTwoLineShow)
                    {
                        if (TextLeaveOutIsEnd)
                        {
                            text = firstText;
                        }
                        if (TextLocation == TextLocations.none)
                        {
                            TextLocation = TextLocations.left;
                        }
                        //ReportViewUtils.drawString(g, LocationModel.Location_Up_Right_Right, text, font_Text, TextBrush, Data.Area.left, Data.Area.top - TextAndTextPadding, LeftPadding, height);
                    }
                    else
                    {
                        string endText = text.Substring(TextShowCount);
                        if (isOvertTextShowCount(g, endText, font_Text))
                        {
                            if (TextLeaveOutIsEnd)
                            {
                                text = endText.Substring(0, TextShowCount - 3) + "...";
                            }
                            else
                            {
                                text = "..." + endText.Substring(TextShowCount);
                            }
                        }
                        if (TextLocation == TextLocations.none)
                        {
                            TextLocation = TextLocations.left_up_down;
                        }
                    }
                }
                else
                {
                    if (TextLocation == TextLocations.none)
                    {
                        TextLocation = TextLocations.left;
                    }
                }
                //g.DrawString(text, font_Text, TextBrush, Data.Area.left + (newStart_X - Data.Area.left - text.Length * TextSize) / 2, Data.Area.top);

                DrawString(g, TextBrush, font_Text, Data, text, firstText);

                if (IsDrawRatio)
                {
                    //绘制右侧百分比
                    float perNum = Data.mainData * 1.0f / model.MaxData * 100;
                    string str = (Math.Round(perNum, 2)).ToString();
                    //g.DrawString(str + "%", font_Data, DataBrush, width - (str.Length + 1) * TextSize + startX, startY);
                    ReportViewUtils.drawString(g, LocationModel.Location_Right_Right, str + "%", font_Text, TextBrush, Data.Area.left + LeftPadding, Data.Area.top - height, width + height, height);
                }
                path.Dispose();
                selfBrush.Dispose();

                //utils.drawReportView(g, RePortViewStyle.Arc_Angle_rectangle_EventHandle, Data.Area.left, Data.Area.top, Data.Area.right, Data.Area.bottom - Data.Area.top, linePen.Color, Data.mainText, Data.mainData, 400, TextSize, DataSize);

                ///建议把绘制的直接写在这里
                if (Data.Area.IsMouseIn)
                {
                    Color color = Data.ModelColor;
                    Brush myBrush = new SolidBrush(Color.FromArgb(BackGroundAlpha, color.R, color.G, color.B));
                    g.FillRectangle(myBrush, new Rectangle(Data.Area.left, Data.Area.top - padding / 2,
                         this.Width, Data.Area.bottom - Data.Area.top + padding));
                    myBrush.Dispose();
                }
            }
            
        }

        private void DrawString(Graphics g, Brush TextBrush, System.Drawing.Font font_Text,DataModel data, string text, string moreText)
        {
            int x = data.Area.left;
            int y = data.Area.top;
            int width = data.Area.right - data.Area.left;
            int height = data.Area.bottom - data.Area.top;
            switch (TextLocation)
            {
                case TextLocations.left:
                    ReportViewUtils.drawString(g, LocationModel.Location_Right_Right, text, font_Text, TextBrush, x, y, LeftPadding, height);
                    break;
                case TextLocations.left_up_down:
                    ReportViewUtils.drawString(g, LocationModel.Location_Up_Right_Right, moreText, font_Text, TextBrush, x, y - TextAndTextPadding, LeftPadding, height);
                    ReportViewUtils.drawString(g, LocationModel.Location_Down_Right_Right, text, font_Text, TextBrush, x, y + TextAndTextPadding, LeftPadding, height);
                    break;
                case TextLocations.up:
                    ReportViewUtils.drawString(g, LocationModel.Location_Left_Left, text, font_Text, TextBrush, x + LeftPadding, y - height, width, height);
                    break;
                case TextLocations.right:
                    ReportViewUtils.drawString(g, LocationModel.Location_Left_Left, text, font_Text, TextBrush, EStartX + EViewWidth + MultiPadding, y, width, height);
                    break;
                case TextLocations.bottom:
                    ReportViewUtils.drawString(g, LocationModel.Location_Down_Down, text, font_Text, TextBrush, x + LeftPadding, y + height, width, height);
                    break;
                case TextLocations.center:
                    if (moreText.Trim().Equals(""))
                    {
                        ReportViewUtils.drawString(g, LocationModel.Location_Center, text, font_Text, TextBrush, x, y, width, height);
                    }
                    else
                    {
                        if (IsTwoLineShow)
                        {
                            ReportViewUtils.drawString(g, LocationModel.Location_Center, text, font_Text, TextBrush, x, y - TextAndTextPadding, width, height);
                            ReportViewUtils.drawString(g, LocationModel.Location_Center, moreText, font_Text, TextBrush, x, y + TextAndTextPadding, width, height);
                        }
                        else
                        {
                            ReportViewUtils.drawString(g, LocationModel.Location_Center, text, font_Text, TextBrush, x, y, width, height);
                        }
                    }
                   
                    break;
                default:
                    break;
            }
        }

        public void HideScrollerBar()
        {
            AutoScroll = false;
            label.Location = new Point(0, 0);
        }
        /// <summary>
        /// 限制字数
        /// </summary>
        private string LimiteText(Graphics g, System.Drawing.Font font_Text, string text)
        {
            CurrentSpiltLength = text.Length;
            int times = 0;
            while (isOvertTextShowCount(g, text, font_Text))
            {
                CurrentSpiltLength--;
                times++;
                text = text.Substring(0, CurrentSpiltLength);
                if (times == 1)
                {
                    if (!IsTwoLineShow)
                    {
                        text = text + "...";
                        CurrentSpiltLength += 3;
                    }
                }
            }
            if (times > 0)
            {
                CurrentSpiltLength -= 2;
                text = text.Substring(0, CurrentSpiltLength);
                if (!IsTwoLineShow)
                {
                    text = text + "...";
                }
            }
            return text;
        }


        /// <summary>
        /// 检测字数是否超出
        /// </summary>
        private bool isOvertTextShowCount(Graphics g, string str, System.Drawing.Font font_Text)
        {
            SizeF sf = g.MeasureString(str, font_Text);

            if (TextLocation == TextLocations.left || TextLocation == TextLocations.left_up_down)
            {
                if (sf.Width > LeftPadding)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            if (RePortViewStyle == RePortViewStyles.SimpleText)
            {
                if (sf.Width > (Width - LeftPadding - RightPadding))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            if (str.Length > TextShowCount)
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// 图形距离最左侧的边距
        /// </summary>
        public int LeftPadding { get; set; }

        /// <summary>
        /// 字体省略是否是尾部
        /// </summary>
        public bool TextLeaveOutIsEnd { get; set; }

        /// <summary>
        /// 上下字体之间的间距
        /// </summary>
        public int TextAndTextPadding { get; set; }

        /// <summary>
        /// 是否两行展示信息
        /// </summary>
        public bool IsTwoLineShow { get; set; }

        /// <summary>
        /// 是否绘制右侧比率
        /// </summary>
        public bool IsDrawRatio { get; set; }

        /// <summary>
        /// 信息显示的位置
        /// </summary>
        public TextLocations TextLocation { get; set; }

        /// <summary>
        /// 绘制时颜色多彩化
        /// </summary>
        public bool IsDrawColorfulRectAngle { get; set; }

        /// <summary>
        /// 字体可以展示的最大数量, 超出将隐藏
        /// </summary>
        public int TextShowCount { get; set; }

        /// <summary>
        /// 报表模式选择
        /// </summary>
        public RePortViewStyles RePortViewStyle { get; set; }

        /// <summary>
        /// 默认多矩形绘制的数量为10个. 
        /// </summary>
        public int MultiRectangleCount { get; set; }

        /// <summary>
        /// 多矩形之间的距离(默认20;)
        /// </summary>
        public int MultiPadding { get; set; }

        /// <summary>
        /// 右边距 用于绘制一些比率信息(默认100)
        /// </summary>
        public int RightPadding { get; set; }



        public override void DrawSelfDefineView(Graphics g, Pen linePen, Brush lineBrush, Brush TextBrush, Brush DataBrush, Font FontText, Font FontData)
        {
           // rank = 0;
        }

        /// <summary>
        /// 是否启用滑动条
        /// </summary>
        public bool IsEnableScrollerBar { get; set; }

        /// <summary>
        /// 鼠标移上去后的背景色的透明度(默认60)
        /// </summary>
        public int BackGroundAlpha { get; set; }

        /// <summary>
        /// 每行的高度默认25
        /// </summary>
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

        public int AutoUpdateTime
        {
            get
            {
                return autoUpdateTime;
            }
            set
            {
                this.autoUpdateTime = value;
            }
        }

        public bool IsNeedReLocationToopTip
        {
            get
            {
                return isNeedReLocationToopTip;
            }
            set
            {
                this.isNeedReLocationToopTip = value;
            }
        }

        public bool IsNotAllowShowAnimalion
        {
            get
            {
                return isNotAllowShowAnimalion;
            }
            set
            {
                this.isNotAllowShowAnimalion = value;
            }
        }

        public ReportFormDesign.Animals.Animalion MyAnimalion
        {
            get
            {
                return animalion;
            }
            set
            {
                this.animalion = value;
            }
        }

        public ReportViewToolTip MyToolTip
        {
            get
            {
                return toolTip;
            }
            set
            {
                this.toolTip = value;
            }
        }
    }
}
