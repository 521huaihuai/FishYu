namespace ReportFormDesign
{
    partial class ArcSpliteTest
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
            this.arc_Splite_ReportView1 = new ReportFormDesign.ReportViewPanel.Arc_Splite_ReportView();
            this.SuspendLayout();
            // 
            // arc_Splite_ReportView1
            // 
            this.arc_Splite_ReportView1.BackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(22)))), ((int)(((byte)(52)))));
            this.arc_Splite_ReportView1.DataColor = System.Drawing.Color.White;
            this.arc_Splite_ReportView1.DataSize = 8;
            this.arc_Splite_ReportView1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(169)))), ((int)(((byte)(255)))));
            this.arc_Splite_ReportView1.Location = new System.Drawing.Point(2, 1);
            this.arc_Splite_ReportView1.Name = "arc_Splite_ReportView1";
            this.arc_Splite_ReportView1.Size = new System.Drawing.Size(489, 379);
            this.arc_Splite_ReportView1.StrokenWidth = 5F;
            this.arc_Splite_ReportView1.TabIndex = 0;
            this.arc_Splite_ReportView1.TextColor = System.Drawing.Color.White;
            this.arc_Splite_ReportView1.TextSize = 8;
            // 
            // ArcSpliteTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 415);
            this.Controls.Add(this.arc_Splite_ReportView1);
            this.Name = "ArcSpliteTest";
            this.Text = "ArcSpliteTest";
            this.Load += new System.EventHandler(this.ArcSpliteTest_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ReportViewPanel.Arc_Splite_ReportView arc_Splite_ReportView1;
    }
}