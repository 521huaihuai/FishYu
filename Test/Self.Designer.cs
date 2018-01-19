namespace Test
{
    partial class Self
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.animationClosePictureBox1 = new FishyuSelfControl.CommonPictureBoxs.ClosePictureBoxs.AnimationClosePictureBox();
            this.animationMinPictureBox1 = new FishyuSelfControl.CommonPictureBoxs.MinPictureBoxs.AnimationMinPictureBox();
            this.animationMaxPictureBox1 = new FishyuSelfControl.CommonPictureBoxs.MaxPictureBoxs.AnimationMaxPictureBox();
            this.SuspendLayout();
            // 
            // animationClosePictureBox1
            // 
            this.animationClosePictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.animationClosePictureBox1.IsIntelligence = true;
            this.animationClosePictureBox1.Location = new System.Drawing.Point(76, 3);
            this.animationClosePictureBox1.MyDisplayStyle = FishyuSelfControl.CommonPictureBoxs.ClosePictureBoxs.AnimationClosePictureBox.DisplayStyle.Default;
            this.animationClosePictureBox1.Name = "animationClosePictureBox1";
            this.animationClosePictureBox1.OverCrossColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.animationClosePictureBox1.Size = new System.Drawing.Size(24, 24);
            this.animationClosePictureBox1.TabIndex = 1;
            // 
            // animationMinPictureBox1
            // 
            this.animationMinPictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.animationMinPictureBox1.IsIntelligence = true;
            this.animationMinPictureBox1.Location = new System.Drawing.Point(23, 3);
            this.animationMinPictureBox1.MyDisplayStyle = FishyuSelfControl.CommonPictureBoxs.MinPictureBoxs.AnimationMinPictureBox.DisplayStyle.Default;
            this.animationMinPictureBox1.Name = "animationMinPictureBox1";
            this.animationMinPictureBox1.OverCrossColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.animationMinPictureBox1.Size = new System.Drawing.Size(24, 24);
            this.animationMinPictureBox1.TabIndex = 2;
            // 
            // animationMaxPictureBox1
            // 
            this.animationMaxPictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.animationMaxPictureBox1.IsIntelligence = true;
            this.animationMaxPictureBox1.Location = new System.Drawing.Point(131, 3);
            this.animationMaxPictureBox1.MyDisplayStyle = FishyuSelfControl.CommonPictureBoxs.MaxPictureBoxs.AnimationMaxPictureBox.DisplayStyle.Default;
            this.animationMaxPictureBox1.Name = "animationMaxPictureBox1";
            this.animationMaxPictureBox1.OverCrossColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.animationMaxPictureBox1.Size = new System.Drawing.Size(24, 24);
            this.animationMaxPictureBox1.TabIndex = 3;
            // 
            // Self
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.animationMaxPictureBox1);
            this.Controls.Add(this.animationMinPictureBox1);
            this.Controls.Add(this.animationClosePictureBox1);
            this.Name = "Self";
            this.Size = new System.Drawing.Size(195, 30);
            this.Load += new System.EventHandler(this.Self_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private FishyuSelfControl.CommonPictureBoxs.ClosePictureBoxs.AnimationClosePictureBox animationClosePictureBox1;
        private FishyuSelfControl.CommonPictureBoxs.MinPictureBoxs.AnimationMinPictureBox animationMinPictureBox1;
        private FishyuSelfControl.CommonPictureBoxs.MaxPictureBoxs.AnimationMaxPictureBox animationMaxPictureBox1;
    }
}
