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
    public enum HisTextLocations
    {
        none, left,left_up_down, up, right, bottom, center
    }

    public enum RePortHisViewStyles
    {
        Arc_angle_rectangle,
        MultiRectangle,
        Ranking,
        SimpleText
    }

    /// <summary>
    /// 主要负责报表的绘制
    /// 以及popView的基本设置(不负责具体的popView绘制)
    /// </summary>
    public class Histogram_Rectangle_ReportView : ViewPanel
    {
        #region 字段
        private float oldSize;
        private int oldCount = 6;
        private int rank;
        private Label label;
        private int CurrentSpiltLength;
        private int rowHeight = 25;
        private int memberWidth;

        public List<string> PropertyName = new List<string>();
        #endregion

        public Histogram_Rectangle_ReportView()
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
            LeftPadding = 100;
            RightPadding = 100;
            padding = 20;
            TextLeaveOutIsEnd = true;
            StrokenWidth = 1;
            DataSize = 10;
            IsDrawValue = true;
            IsDrawColorfulRectAngle = true;
            IsShowPropertyBer = true;
            PropertycontentFont = new Font("幼圆", 10);
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

            //radius_Rectangle_ReportView1.Height = 2000;
            ReportViewAdapter adapter = new SimpleListViewAdapter();
            //设置数据源
            adapter.setData(list);
            //用于设置起始位置
            adapter.setBasePostitionRect(new AreaPositionRect(0, 2 * padding, this.Width, rowHeight + 30));
            setAdapter(adapter);//设置panel的高度

            
            if (PropertyName == null)
            {
                PropertyName.Add("Member");
                PropertyName.Add("Member of Value");
            }
            
            //初始化分栏的宽度
            memberWidth = (Width - RightPadding - LeftPadding) / 4;

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
            //MultiPadding = (int)(Width * 1.0f / 30);
            if (IsAutoReSizeView)
            {
                if (HisTextLocation == HisTextLocations.up || HisTextLocation == HisTextLocations.bottom)
                {
                    LeftPadding = 40;
                    RightPadding = 40;
                }
                else
                {
                    LeftPadding = padding;
                    RightPadding = padding;
                }
            }
            if (Width > 600)
            {
                oldSize = MyToolTip.TextSize;
                oldCount = TextShowCount;
                MyToolTip.TextSize = 11f;
                if (HisTextLocation == HisTextLocations.center || HisTextLocation == HisTextLocations.up || HisTextLocation == HisTextLocations.bottom)
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
            switch (RePortHisViewStyle)
            {
                case RePortHisViewStyles.Arc_angle_rectangle:
                    DrawArcRectAngle(g, Data, linePen, lineBrush, TextBrush, font_Text);
                    break;
                case RePortHisViewStyles.MultiRectangle:
                    DrawMultiRectAngel(g, Data, linePen, lineBrush, TextBrush, DataBrush, font_Text, font_Data);
                    break;
                case RePortHisViewStyles.Ranking:
                    DrawMultiRanking(g, Data, linePen, lineBrush, TextBrush, DataBrush, font_Text, font_Data);
                    break;
                case RePortHisViewStyles.SimpleText:
                    DrawMultiSimpleText(g, Data, linePen, lineBrush, TextBrush, DataBrush, font_Text, font_Data);
                    break;
                default:
                    break;
            }
        }

        #region 不使用的方法
        private void DrawMultiSimpleText(Graphics g, DataModel Data, Pen linePen, Brush lineBrush, Brush TextBrush, Brush DataBrush, System.Drawing.Font font_Text, System.Drawing.Font font_Data)
        {
            throw new NotImplementedException();
        }

        private void DrawMultiRanking(Graphics g, DataModel Data, Pen linePen, Brush lineBrush, Brush TextBrush, Brush DataBrush, System.Drawing.Font font_Text, System.Drawing.Font font_Data)
        {
            throw new NotImplementedException();
        }

        private void DrawMultiRectAngel(Graphics g, DataModel Data, Pen linePen, Brush lineBrush, Brush TextBrush, Brush DataBrush, System.Drawing.Font font_Text, System.Drawing.Font font_Data)
        {
            throw new NotImplementedException();
        }
        #endregion

        private void DrawArcRectAngle(Graphics g, DataModel Data, Pen linePen, Brush lineBrush, Brush TextBrush, System.Drawing.Font font_Text)
        {
            if (Data.Area.top < 2 * padding)
            {
                return;
            }

            if (Data is AutoSortDataModel)
            {
                AutoSortDataModel model = Data as AutoSortDataModel;
                //建议把绘制的直接写在这里
                int PaddingStart_X = Data.Area.left + LeftPadding + memberWidth + padding / 2;
                //宽高
                int height = Data.Area.bottom - Data.Area.top;

                int width = this.Width - PaddingStart_X - RightPadding ;
                float per = width * 1.0f / model.MaxData;
                float realShowData = per * Data.mainData;
                //Rectangle rect = new Rectangle(PaddingStart_X, Data.Area.top, width, height);
                Rectangle rectReal = new Rectangle(PaddingStart_X, Data.Area.top, (int)realShowData, height);

                if (IsDrawColorfulRectAngle)
                {
                    lineBrush.Dispose();
                    lineBrush = new SolidBrush(Data.ModelColor);
                }
                g.FillRectangle(lineBrush, rectReal);

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
                        if (HisTextLocation == HisTextLocations.none)
                        {
                            HisTextLocation = HisTextLocations.left;
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
                        if (HisTextLocation == HisTextLocations.none)
                        {
                            HisTextLocation = HisTextLocations.left_up_down;
                        }
                    }
                }
                else
                {
                    if (HisTextLocation == HisTextLocations.none)
                    {
                        HisTextLocation = HisTextLocations.left;
                    }
                }
                //g.DrawString(text, font_Text, TextBrush, Data.Area.left + (newStart_X - Data.Area.left - text.Length * TextSize) / 2, Data.Area.top);
                DrawString(g, TextBrush, font_Text, Data, text, firstText);

                if (IsDrawValue)
                {
                    ReportViewUtils.drawString(g, LocationModel.Location_Right_Right, Data.mainData + "", font_Text, TextBrush, PaddingStart_X + realShowData, Data.Area.top, padding, height);
                }

                ///建议把绘制的直接写在这里
                if (Data.Area.IsMouseIn)
                {
                    Color color = Data.ModelColor;
                    Brush myBrush = new SolidBrush(Color.FromArgb(100, 210, 210, 210));
                    g.FillRectangle(myBrush, new Rectangle(Data.Area.left, Data.Area.top - padding / 4,
                         this.Width, Data.Area.bottom - Data.Area.top + padding / 2));
                    myBrush.Dispose();
                }

                if (IsShowPropertyBer)
                {
                    DrawSelfDefine(g, PropertycontentFont, width + RightPadding);
                }
            }
        }

        private void DrawString(Graphics g, Brush TextBrush, System.Drawing.Font font_Text,DataModel data, string text, string moreText)
        {
            int x = data.Area.left + padding ;
            int y = data.Area.top;
            int width = data.Area.right - data.Area.left;
            int height = data.Area.bottom - data.Area.top;
            switch (HisTextLocation)
            {
                case HisTextLocations.left:
                    ReportViewUtils.drawString(g, LocationModel.Location_Right_Right, text, font_Text, TextBrush, x, y, LeftPadding, height);
                    break;
                case HisTextLocations.left_up_down:
                    ReportViewUtils.drawString(g, LocationModel.Location_Up_Right_Right, moreText, font_Text, TextBrush, x, y - TextAndTextPadding, LeftPadding, height);
                    ReportViewUtils.drawString(g, LocationModel.Location_Down_Right_Right, text, font_Text, TextBrush, x, y + TextAndTextPadding, LeftPadding, height);
                    break;
                case HisTextLocations.up:
                    ReportViewUtils.drawString(g, LocationModel.Location_Left_Left, text, font_Text, TextBrush, x + LeftPadding, y - height, width, height);
                    break;
                case HisTextLocations.right:
                    ReportViewUtils.drawString(g, LocationModel.Location_Left_Left, text, font_Text, TextBrush, EStartX + EViewWidth , y, width, height);
                    break;
                case HisTextLocations.bottom:
                    ReportViewUtils.drawString(g, LocationModel.Location_Down_Down, text, font_Text, TextBrush, x + LeftPadding, y + height, width, height);
                    break;
                case HisTextLocations.center:
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

            if (HisTextLocation == HisTextLocations.left || HisTextLocation == HisTextLocations.left_up_down)
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
            if (RePortHisViewStyle == RePortHisViewStyles.SimpleText)
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
        /// 绘制属性栏
        /// </summary>
        /// <param name="g"></param>
        /// <param name="contentFont"></param>
        private void DrawSelfDefine(Graphics g, Font contentFont, int width)
        {
            Brush barBrush = new SolidBrush(Color.Blue);
            Brush contentBrush = new SolidBrush(Color.White);

            //Rectangle rectangle = new Rectangle(0, 0, Width, padding);
            //Brush brush = new SolidBrush(BackColor);
            //g.FillRectangle(brush, rectangle);

            Rectangle memrectangle = new Rectangle(0 + LeftPadding / 2, 0 + padding / 2, memberWidth + LeftPadding / 2, padding);
            g.FillRectangle(barBrush, memrectangle);
            ReportViewUtils.drawString(g, LocationModel.Location_Left, PropertyName[0], contentFont, contentBrush, 0 + LeftPadding / 2, 0 + padding / 2, memberWidth, padding);

            Rectangle numrectangle = new Rectangle(LeftPadding + memberWidth + (padding / 4), 0 + padding / 2, width, padding);
            g.FillRectangle(barBrush, numrectangle);
            ReportViewUtils.drawString(g, LocationModel.Location_Left_Left, PropertyName[1], contentFont, contentBrush, LeftPadding + memberWidth + (padding / 4), 0 + padding / 2, width, padding);

            barBrush.Dispose();
            contentBrush.Dispose();
            //brush.Dispose();
        }

        public override void DrawSelfDefineView(Graphics g, Pen linePen, Brush lineBrush, Brush TextBrush, Brush DataBrush, System.Drawing.Font FontText, System.Drawing.Font FontData)
        {
            throw new NotImplementedException();
        }

        #region 属性
        /// <summary>
        /// 是否绘制属性栏 默认true
        /// </summary>
        public bool IsShowPropertyBer { get; set; }

        /// <summary>
        /// 属性栏内容文本 默认"幼圆", 10
        /// </summary>
        public System.Drawing.Font PropertycontentFont { get; set; }

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
        /// 是否绘制右侧值
        /// </summary>
        public bool IsDrawValue { get; set; }

        /// <summary>
        /// 信息显示的位置
        /// </summary>
        public HisTextLocations HisTextLocation { get; set; }

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
        public RePortHisViewStyles RePortHisViewStyle { get; set; }

        /// <summary>
        /// 右边距 用于绘制一些比率信息(默认100)
        /// </summary>
        public int RightPadding { get; set; }

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
        #endregion

    }
}
