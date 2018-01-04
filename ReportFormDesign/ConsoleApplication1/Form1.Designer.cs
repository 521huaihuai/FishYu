namespace ConsoleApplication1
{
    partial class Form1
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
            this.radius_Rectangle_ReportView1 = new ReportFormDesign.ReportViewPanel.Radius_Rectangle_ReportView();
            this.SuspendLayout();
            // 
            // radius_Rectangle_ReportView1
            // 
            this.radius_Rectangle_ReportView1.AutoScroll = true;
            this.radius_Rectangle_ReportView1.BackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(22)))), ((int)(((byte)(52)))));
            this.radius_Rectangle_ReportView1.DataColor = System.Drawing.Color.White;
            this.radius_Rectangle_ReportView1.DataSize = 8;
            this.radius_Rectangle_ReportView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radius_Rectangle_ReportView1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(169)))), ((int)(((byte)(255)))));
            this.radius_Rectangle_ReportView1.Location = new System.Drawing.Point(0, 0);
            this.radius_Rectangle_ReportView1.Name = "radius_Rectangle_ReportView1";
            this.radius_Rectangle_ReportView1.Size = new System.Drawing.Size(503, 391);
            this.radius_Rectangle_ReportView1.StrokenWidth = 5F;
            this.radius_Rectangle_ReportView1.TabIndex = 2;
            this.radius_Rectangle_ReportView1.TextColor = System.Drawing.Color.White;
            this.radius_Rectangle_ReportView1.TextSize = 8;
            this.radius_Rectangle_ReportView1.Paint += new System.Windows.Forms.PaintEventHandler(this.radius_Rectangle_ReportView1_Paint);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 391);
            this.Controls.Add(this.radius_Rectangle_ReportView1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ReportFormDesign.ReportViewPanel.Radius_Rectangle_ReportView radius_Rectangle_ReportView1;
    }
}