﻿namespace Test
{
    partial class ReportViewTestForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.fishYuDataGridView1 = new FishyuSelfControl.FishYuReportView.AutoSortReportView.DataGridViews.FishYuDataGridView();
            this.fishYuDataGridView2 = new FishyuSelfControl.FishYuReportView.AutoSortReportView.DataGridViews.FishYuDataGridView();
            this.SuspendLayout();
            // 
            // fishYuDataGridView1
            // 
            this.fishYuDataGridView1.BrushColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(169)))), ((int)(((byte)(255)))));
            this.fishYuDataGridView1.CellDefaultGridColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(169)))), ((int)(((byte)(255)))));
            this.fishYuDataGridView1.ColumnCellStyle = new FishyuSelfControl.FishYuReportView.AutoSortReportView.DataGridViews.FishYuCellStyle(FishyuSelfControl.FishYuReportView.AutoSortReportView.DataGridViews.Alignment.MiddleCenter, System.Drawing.Color.White, new System.Drawing.Font("Consolas", 8F), System.Drawing.Color.Black, System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(169)))), ((int)(((byte)(255))))), System.Drawing.Color.White);
            this.fishYuDataGridView1.ColumnGridColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(169)))), ((int)(((byte)(255)))));
            this.fishYuDataGridView1.IsEnableAnimation = true;
            this.fishYuDataGridView1.IsEnableResizeView = false;
            this.fishYuDataGridView1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(22)))), ((int)(((byte)(52)))));
            this.fishYuDataGridView1.Location = new System.Drawing.Point(12, 21);
            this.fishYuDataGridView1.Name = "fishYuDataGridView1";
            this.fishYuDataGridView1.Size = new System.Drawing.Size(618, 130);
            this.fishYuDataGridView1.TabIndex = 0;
            this.fishYuDataGridView1.TitleColumns = null;
            // 
            // fishYuDataGridView2
            // 
            this.fishYuDataGridView2.BrushColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(169)))), ((int)(((byte)(255)))));
            this.fishYuDataGridView2.CellDefaultGridColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(169)))), ((int)(((byte)(255)))));
            this.fishYuDataGridView2.ColumnGridColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(169)))), ((int)(((byte)(255)))));
            this.fishYuDataGridView2.IsEnableAnimation = true;
            this.fishYuDataGridView2.IsEnableResizeView = false;
            this.fishYuDataGridView2.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(22)))), ((int)(((byte)(52)))));
            this.fishYuDataGridView2.Location = new System.Drawing.Point(12, 183);
            this.fishYuDataGridView2.Name = "fishYuDataGridView2";
            this.fishYuDataGridView2.Size = new System.Drawing.Size(618, 192);
            this.fishYuDataGridView2.TabIndex = 1;
            this.fishYuDataGridView2.TitleColumns = null;
            // 
            // ReportViewTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 387);
            this.Controls.Add(this.fishYuDataGridView2);
            this.Controls.Add(this.fishYuDataGridView1);
            this.Name = "ReportViewTestForm";
            this.Text = "ReportViewTestForm";
            this.Load += new System.EventHandler(this.ReportViewTestForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private FishyuSelfControl.FishYuReportView.AutoSortReportView.DataGridViews.FishYuDataGridView fishYuDataGridView1;
        private FishyuSelfControl.FishYuReportView.AutoSortReportView.DataGridViews.FishYuDataGridView fishYuDataGridView2;
    }
}