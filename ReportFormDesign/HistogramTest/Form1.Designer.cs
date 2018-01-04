namespace HistogramTest
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            ReportFormDesign.CoordinateDataBean coordinateDataBean1 = new ReportFormDesign.CoordinateDataBean();
            ReportFormDesign.ToolTips.CoordinateHelpLineToolTip coordinateHelpLineToolTip1 = new ReportFormDesign.ToolTips.CoordinateHelpLineToolTip();
            ReportFormDesign.Model.FollowPopView followPopView1 = new ReportFormDesign.Model.FollowPopView();
            ReportFormDesign.Animals.LinearInterpolation linearInterpolation1 = new ReportFormDesign.Animals.LinearInterpolation();
            ReportFormDesign.Animals.SimpleAnimalion simpleAnimalion1 = new ReportFormDesign.Animals.SimpleAnimalion();
            ReportFormDesign.Model.FollowPopViewForTitle followPopViewForTitle1 = new ReportFormDesign.Model.FollowPopViewForTitle();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.coordinateMultiGroupRadiusRectAngleReportView1 = new ReportFormDesign.ReportViewPanel.SelfDefineReportView.CoordinateMultiModelsReportViews.CoordinateMultiGroupRadiusRectAngleReportView();
            this.histogram_Rectangle_ReportView1 = new ReportFormDesign.ReportViewPanel.Histogram_Rectangle_ReportView();
            this.pie_Chart_SingleReportView1 = new ReportFormDesign.ReportViewPanel.Pie_Chart_SingleReportView();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48.28947F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 51.71053F));
            this.tableLayoutPanel1.Controls.Add(this.coordinateMultiGroupRadiusRectAngleReportView1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.histogram_Rectangle_ReportView1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pie_Chart_SingleReportView1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 53.11653F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 46.88347F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(760, 369);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // coordinateMultiGroupRadiusRectAngleReportView1
            // 
            this.coordinateMultiGroupRadiusRectAngleReportView1._Interpolation = null;
            this.coordinateMultiGroupRadiusRectAngleReportView1.AllSumSize = 10F;
            this.coordinateMultiGroupRadiusRectAngleReportView1.AutoScroll = true;
            this.coordinateMultiGroupRadiusRectAngleReportView1.AutoUpdateTime = 300;
            this.coordinateMultiGroupRadiusRectAngleReportView1.BoardColor = System.Drawing.Color.Blue;
            this.coordinateMultiGroupRadiusRectAngleReportView1.BottomPadding = 0;
            this.coordinateMultiGroupRadiusRectAngleReportView1.ConformCount = 0;
            coordinateDataBean1.CompontData = null;
            coordinateDataBean1.CoordinateModelList = null;
            coordinateDataBean1.Lable_X = "x";
            coordinateDataBean1.Lable_Y = "y";
            coordinateDataBean1.Title = "报表";
            coordinateDataBean1.X_Data = null;
            coordinateDataBean1.Y_Data = null;
            this.coordinateMultiGroupRadiusRectAngleReportView1.CoordinateDataModelBean = coordinateDataBean1;
            this.coordinateMultiGroupRadiusRectAngleReportView1.CoordinateHeight = 106;
            this.coordinateMultiGroupRadiusRectAngleReportView1.CoordinateModels = null;
            this.coordinateMultiGroupRadiusRectAngleReportView1.CoordinateStartX = 28;
            this.coordinateMultiGroupRadiusRectAngleReportView1.CoordinateStartY = 166;
            this.coordinateMultiGroupRadiusRectAngleReportView1.Count_X = 9;
            this.coordinateMultiGroupRadiusRectAngleReportView1.Count_Y = 4;
            this.coordinateMultiGroupRadiusRectAngleReportView1.DataColor = System.Drawing.Color.White;
            this.coordinateMultiGroupRadiusRectAngleReportView1.DataSize = 8F;
            this.coordinateMultiGroupRadiusRectAngleReportView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.coordinateMultiGroupRadiusRectAngleReportView1.DrawAllSumLocation = ReportFormDesign.ReportViewPanel.SelfDefineReportView.CoordinateReportViews.CoordinateReportView.AllSumLocation.none;
            this.coordinateMultiGroupRadiusRectAngleReportView1.EStartX = 28;
            this.coordinateMultiGroupRadiusRectAngleReportView1.EStartY = 8;
            this.coordinateMultiGroupRadiusRectAngleReportView1.EViewHeight = 158;
            this.coordinateMultiGroupRadiusRectAngleReportView1.EViewWidth = 331;
            this.coordinateMultiGroupRadiusRectAngleReportView1.FillBrushAlpha = 30;
            this.coordinateMultiGroupRadiusRectAngleReportView1.FrameColor = System.Drawing.Color.White;
            this.coordinateMultiGroupRadiusRectAngleReportView1.IsAutoReSizeView = true;
            this.coordinateMultiGroupRadiusRectAngleReportView1.IsDrawBoard = true;
            this.coordinateMultiGroupRadiusRectAngleReportView1.IsDrawCurve = false;
            this.coordinateMultiGroupRadiusRectAngleReportView1.IsDrawDetailData = true;
            this.coordinateMultiGroupRadiusRectAngleReportView1.IsDrawFrame = true;
            this.coordinateMultiGroupRadiusRectAngleReportView1.IsDrawTitle = true;
            this.coordinateMultiGroupRadiusRectAngleReportView1.IsEnableArrow = true;
            this.coordinateMultiGroupRadiusRectAngleReportView1.IsEnableGridLine = false;
            this.coordinateMultiGroupRadiusRectAngleReportView1.IsFillPathCurve = false;
            this.coordinateMultiGroupRadiusRectAngleReportView1.IsHorizontally = true;
            this.coordinateMultiGroupRadiusRectAngleReportView1.IsLableFontBold = true;
            this.coordinateMultiGroupRadiusRectAngleReportView1.IsNeedDataPartitioning = false;
            this.coordinateMultiGroupRadiusRectAngleReportView1.IsNeedReLocationToopTip = false;
            this.coordinateMultiGroupRadiusRectAngleReportView1.IsNotAllowShowAnimalion = false;
            this.coordinateMultiGroupRadiusRectAngleReportView1.IsRadiusRectAngle = false;
            this.coordinateMultiGroupRadiusRectAngleReportView1.IsShowViewByAnimalioned = false;
            this.coordinateMultiGroupRadiusRectAngleReportView1.IsShowX_Scale = false;
            this.coordinateMultiGroupRadiusRectAngleReportView1.IsShowY_Scale = true;
            this.coordinateMultiGroupRadiusRectAngleReportView1.LableAllNum = " / 个";
            this.coordinateMultiGroupRadiusRectAngleReportView1.LableFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.coordinateMultiGroupRadiusRectAngleReportView1.LableSize = 10F;
            this.coordinateMultiGroupRadiusRectAngleReportView1.LableX = "";
            this.coordinateMultiGroupRadiusRectAngleReportView1.LableY = "";
            this.coordinateMultiGroupRadiusRectAngleReportView1.LeftPadding = 0;
            this.coordinateMultiGroupRadiusRectAngleReportView1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(169)))), ((int)(((byte)(255)))));
            this.coordinateMultiGroupRadiusRectAngleReportView1.Location = new System.Drawing.Point(370, 3);
            this.coordinateMultiGroupRadiusRectAngleReportView1.MaxNum = 0F;
            this.coordinateMultiGroupRadiusRectAngleReportView1.MultiPadding = 9;
            this.coordinateMultiGroupRadiusRectAngleReportView1.MyAnimalion = null;
            coordinateHelpLineToolTip1.BackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            coordinateHelpLineToolTip1.CoordinateHeight = 106;
            coordinateHelpLineToolTip1.CoordinatePeerDistance = 5F;
            coordinateHelpLineToolTip1.CoordinateStartX = 28;
            coordinateHelpLineToolTip1.CoordinateStartY = 166;
            coordinateHelpLineToolTip1.CoordinateWidth = 301;
            coordinateHelpLineToolTip1.DataColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            coordinateHelpLineToolTip1.DataSize = 10;
            coordinateHelpLineToolTip1.Height = 40;
            coordinateHelpLineToolTip1.HelpLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(36)))), ((int)(((byte)(169)))), ((int)(((byte)(255)))));
            coordinateHelpLineToolTip1.HelpLineColorAlpha = 150;
            coordinateHelpLineToolTip1.IsDrawBackGround = false;
            coordinateHelpLineToolTip1.IsDrawXYPoint = false;
            coordinateHelpLineToolTip1.IsVisible = true;
            coordinateHelpLineToolTip1.LocalPosition = new System.Drawing.Point(0, 0);
            coordinateHelpLineToolTip1.Padding = 10;
            coordinateHelpLineToolTip1.PaddingIn = 20;
            coordinateHelpLineToolTip1.PeerHelpLineWidth = 5F;
            coordinateHelpLineToolTip1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(169)))), ((int)(((byte)(255)))));
            coordinateHelpLineToolTip1.TextSize = 10F;
            coordinateHelpLineToolTip1.Width = 100;
            this.coordinateMultiGroupRadiusRectAngleReportView1.MyToolTip = coordinateHelpLineToolTip1;
            this.coordinateMultiGroupRadiusRectAngleReportView1.Name = "coordinateMultiGroupRadiusRectAngleReportView1";
            this.coordinateMultiGroupRadiusRectAngleReportView1.NewFormBackColor = System.Drawing.Color.Empty;
            this.coordinateMultiGroupRadiusRectAngleReportView1.NewFormBackgroundImage = null;
            this.coordinateMultiGroupRadiusRectAngleReportView1.OffsetX = 0;
            this.coordinateMultiGroupRadiusRectAngleReportView1.PerMax = 50F;
            this.coordinateMultiGroupRadiusRectAngleReportView1.RightPadding = 30;
            this.coordinateMultiGroupRadiusRectAngleReportView1.Size = new System.Drawing.Size(387, 190);
            this.coordinateMultiGroupRadiusRectAngleReportView1.StrokenWidth = 5F;
            this.coordinateMultiGroupRadiusRectAngleReportView1.TabIndex = 0;
            this.coordinateMultiGroupRadiusRectAngleReportView1.Tension = 0.5F;
            this.coordinateMultiGroupRadiusRectAngleReportView1.TextColor = System.Drawing.Color.White;
            this.coordinateMultiGroupRadiusRectAngleReportView1.TextRotate = 30F;
            this.coordinateMultiGroupRadiusRectAngleReportView1.TextSize = 9.675F;
            this.coordinateMultiGroupRadiusRectAngleReportView1.Title = "异常显示";
            this.coordinateMultiGroupRadiusRectAngleReportView1.TopPadding = 52;
            // 
            // histogram_Rectangle_ReportView1
            // 
            this.histogram_Rectangle_ReportView1.AutoScroll = true;
            this.histogram_Rectangle_ReportView1.AutoUpdateTime = 300;
            this.histogram_Rectangle_ReportView1.BackGroundAlpha = 60;
            this.histogram_Rectangle_ReportView1.BoardColor = System.Drawing.Color.White;
            this.histogram_Rectangle_ReportView1.DataColor = System.Drawing.Color.White;
            this.histogram_Rectangle_ReportView1.DataSize = 10F;
            this.histogram_Rectangle_ReportView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.histogram_Rectangle_ReportView1.EStartX = 0;
            this.histogram_Rectangle_ReportView1.EStartY = 0;
            this.histogram_Rectangle_ReportView1.EViewHeight = 0;
            this.histogram_Rectangle_ReportView1.EViewWidth = 0;
            this.histogram_Rectangle_ReportView1.FrameColor = System.Drawing.Color.White;
            this.histogram_Rectangle_ReportView1.HisTextLocation = ReportFormDesign.ReportViewPanel.HisTextLocations.none;
            this.histogram_Rectangle_ReportView1.IsAutoReSizeView = true;
            this.histogram_Rectangle_ReportView1.IsDrawBoard = false;
            this.histogram_Rectangle_ReportView1.IsDrawColorfulRectAngle = true;
            this.histogram_Rectangle_ReportView1.IsDrawFrame = true;
            this.histogram_Rectangle_ReportView1.IsDrawTitle = true;
            this.histogram_Rectangle_ReportView1.IsDrawValue = true;
            this.histogram_Rectangle_ReportView1.IsEnableScrollerBar = false;
            this.histogram_Rectangle_ReportView1.IsHorizontally = true;
            this.histogram_Rectangle_ReportView1.IsNeedReLocationToopTip = true;
            this.histogram_Rectangle_ReportView1.IsNotAllowShowAnimalion = false;
            this.histogram_Rectangle_ReportView1.IsShowPropertyBer = true;
            this.histogram_Rectangle_ReportView1.IsTwoLineShow = false;
            this.histogram_Rectangle_ReportView1.LeftPadding = 20;
            this.histogram_Rectangle_ReportView1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(169)))), ((int)(((byte)(255)))));
            this.histogram_Rectangle_ReportView1.Location = new System.Drawing.Point(3, 3);
            this.histogram_Rectangle_ReportView1.MyAnimalion = null;
            followPopView1.BackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(207)))), ((int)(((byte)(202)))));
            followPopView1.DataColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            followPopView1.DataSize = 8;
            followPopView1.Height = 40;
            followPopView1.IsDrawBackGround = false;
            followPopView1.IsVisible = true;
            followPopView1.LocalPosition = new System.Drawing.Point(0, 0);
            followPopView1.Padding = 20;
            followPopView1.PaddingIn = 10;
            followPopView1.TextColor = System.Drawing.Color.Black;
            followPopView1.TextSize = 8F;
            followPopView1.Width = 100;
            this.histogram_Rectangle_ReportView1.MyToolTip = followPopView1;
            this.histogram_Rectangle_ReportView1.Name = "histogram_Rectangle_ReportView1";
            this.histogram_Rectangle_ReportView1.NewFormBackColor = System.Drawing.Color.Empty;
            this.histogram_Rectangle_ReportView1.NewFormBackgroundImage = null;
            this.histogram_Rectangle_ReportView1.PropertycontentFont = new System.Drawing.Font("幼圆", 10F);
            this.histogram_Rectangle_ReportView1.RePortHisViewStyle = ReportFormDesign.ReportViewPanel.RePortHisViewStyles.Arc_angle_rectangle;
            this.histogram_Rectangle_ReportView1.RightPadding = 20;
            this.histogram_Rectangle_ReportView1.RowHeight = 25;
            this.histogram_Rectangle_ReportView1.Size = new System.Drawing.Size(361, 190);
            this.histogram_Rectangle_ReportView1.StrokenWidth = 1F;
            this.histogram_Rectangle_ReportView1.TabIndex = 1;
            this.histogram_Rectangle_ReportView1.TextAndTextPadding = 4;
            this.histogram_Rectangle_ReportView1.TextColor = System.Drawing.Color.White;
            this.histogram_Rectangle_ReportView1.TextLeaveOutIsEnd = true;
            this.histogram_Rectangle_ReportView1.TextShowCount = 10;
            this.histogram_Rectangle_ReportView1.TextSize = 8F;
            // 
            // pie_Chart_SingleReportView1
            // 
            linearInterpolation1.Value = 4F;
            this.pie_Chart_SingleReportView1._Interpolation = linearInterpolation1;
            simpleAnimalion1.AnimalInterface = this.pie_Chart_SingleReportView1;
            simpleAnimalion1.AnimalionTime = 8F;
            simpleAnimalion1.AnimalionUsedTime = 0F;
            simpleAnimalion1.AnimalionUsedTimeMilliseconds = 0F;
            simpleAnimalion1.DelayTime = 40;
            simpleAnimalion1.IsAnimailing = false;
            simpleAnimalion1.IsEndAnimalion = false;
            simpleAnimalion1.IsPrepareAnimaled = false;
            this.pie_Chart_SingleReportView1._MyAnimalion = simpleAnimalion1;
            followPopViewForTitle1.BackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            followPopViewForTitle1.DataColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            followPopViewForTitle1.DataSize = 10;
            followPopViewForTitle1.Height = 40;
            followPopViewForTitle1.IsDrawBackGround = false;
            followPopViewForTitle1.IsVisible = true;
            followPopViewForTitle1.LocalPosition = new System.Drawing.Point(0, 0);
            followPopViewForTitle1.Padding = 20;
            followPopViewForTitle1.PaddingIn = 10;
            followPopViewForTitle1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            followPopViewForTitle1.TextSize = 10F;
            followPopViewForTitle1.Width = 100;
            this.pie_Chart_SingleReportView1._MyToolTip = followPopViewForTitle1;
            this.pie_Chart_SingleReportView1.AnimalionTextSize = 9F;
            this.pie_Chart_SingleReportView1.AutoScroll = true;
            this.pie_Chart_SingleReportView1.AutoUpdateTime = 300;
            this.pie_Chart_SingleReportView1.BoardColor = System.Drawing.Color.White;
            this.pie_Chart_SingleReportView1.DataColor = System.Drawing.Color.White;
            this.pie_Chart_SingleReportView1.DataSize = 5.071429F;
            this.pie_Chart_SingleReportView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pie_Chart_SingleReportView1.EStartX = 145;
            this.pie_Chart_SingleReportView1.EStartY = 48;
            this.pie_Chart_SingleReportView1.EViewHeight = 71;
            this.pie_Chart_SingleReportView1.EViewWidth = 71;
            this.pie_Chart_SingleReportView1.FrameColor = System.Drawing.Color.White;
            this.pie_Chart_SingleReportView1.IsAutoReSizeView = true;
            this.pie_Chart_SingleReportView1.IsDescendingOrder = false;
            this.pie_Chart_SingleReportView1.IsDrawBoard = false;
            this.pie_Chart_SingleReportView1.IsDrawFrame = true;
            this.pie_Chart_SingleReportView1.IsDrawLegendNote = false;
            this.pie_Chart_SingleReportView1.IsDrawTitle = true;
            this.pie_Chart_SingleReportView1.IsHorizontally = true;
            this.pie_Chart_SingleReportView1.IsNeedReLocationToopTip = true;
            this.pie_Chart_SingleReportView1.IsNotAllowShowAnimalion = false;
            this.pie_Chart_SingleReportView1.IsShowViewByAnimalioned = false;
            this.pie_Chart_SingleReportView1.LegendNoteFontTextSize = 8F;
            this.pie_Chart_SingleReportView1.LegendNoteHeight = 20;
            this.pie_Chart_SingleReportView1.LegendNoteUpDownPadding = 5;
            this.pie_Chart_SingleReportView1.LegendNoteWidth = 30;
            this.pie_Chart_SingleReportView1.LegengNoteTextLimiteCount = 10;
            this.pie_Chart_SingleReportView1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(169)))), ((int)(((byte)(255)))));
            this.pie_Chart_SingleReportView1.LineHeight = 30F;
            this.pie_Chart_SingleReportView1.Location = new System.Drawing.Point(3, 199);
            this.pie_Chart_SingleReportView1.MaxIndex = 28F;
            this.pie_Chart_SingleReportView1.MaxLineWidth = 40F;
            this.pie_Chart_SingleReportView1.MaxNum = 100F;
            this.pie_Chart_SingleReportView1.MyAnimalColor = System.Drawing.Color.White;
            this.pie_Chart_SingleReportView1.Name = "pie_Chart_SingleReportView1";
            this.pie_Chart_SingleReportView1.NewFormBackColor = System.Drawing.Color.Empty;
            this.pie_Chart_SingleReportView1.NewFormBackgroundImage = null;
            this.pie_Chart_SingleReportView1.Size = new System.Drawing.Size(361, 167);
            this.pie_Chart_SingleReportView1.StrokenWidth = 17.75F;
            this.pie_Chart_SingleReportView1.TabIndex = 2;
            this.pie_Chart_SingleReportView1.TextColor = System.Drawing.Color.White;
            this.pie_Chart_SingleReportView1.TextLimiteCount = 10;
            this.pie_Chart_SingleReportView1.TextSize = 5.916667F;
            this.pie_Chart_SingleReportView1.Title = null;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(760, 369);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private ReportFormDesign.ReportViewPanel.Histogram_Rectangle_ReportView histogram_Rectangle_ReportView1;
        private ReportFormDesign.ReportViewPanel.SelfDefineReportView.CoordinateMultiModelsReportViews.CoordinateMultiGroupRadiusRectAngleReportView coordinateMultiGroupRadiusRectAngleReportView1;
        private ReportFormDesign.ReportViewPanel.Pie_Chart_SingleReportView pie_Chart_SingleReportView1;







    }
}

