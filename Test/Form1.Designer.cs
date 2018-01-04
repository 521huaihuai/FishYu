namespace Test
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
            this.simplePictureBox1 = new FishyuSelfControl.CommonPictureBoxs.SimplePictureBoxs.SimplePictureBox();
            this.simplePictureBox2 = new FishyuSelfControl.CommonPictureBoxs.SimplePictureBoxs.SimplePictureBox();
            this.SuspendLayout();
            // 
            // simplePictureBox1
            // 
            this.simplePictureBox1.BackColor = System.Drawing.Color.Silver;
            this.simplePictureBox1.ClickColor = System.Drawing.Color.Gray;
            this.simplePictureBox1.Location = new System.Drawing.Point(25, 76);
            this.simplePictureBox1.Name = "simplePictureBox1";
            this.simplePictureBox1.SelectColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.simplePictureBox1.Size = new System.Drawing.Size(92, 38);
            this.simplePictureBox1.TabIndex = 0;
            this.simplePictureBox1.OnPictrueBoxClickListenerEvent += new FishyuSelfControl.CommonPictureBoxs.SimplePictureBoxs.SimplePictureBox.OnPictrueBoxClickListener(this.simplePictureBox1_OnPictrueBoxClickListenerEvent);
            this.simplePictureBox1.Load += new System.EventHandler(this.simplePictureBox1_Load);
            // 
            // simplePictureBox2
            // 
            this.simplePictureBox2.BackColor = System.Drawing.Color.Silver;
            this.simplePictureBox2.ClickColor = System.Drawing.Color.Gray;
            this.simplePictureBox2.Location = new System.Drawing.Point(164, 76);
            this.simplePictureBox2.Name = "simplePictureBox2";
            this.simplePictureBox2.SelectColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.simplePictureBox2.Size = new System.Drawing.Size(92, 38);
            this.simplePictureBox2.TabIndex = 1;
            this.simplePictureBox2.OnPictrueBoxClickListenerEvent += new FishyuSelfControl.CommonPictureBoxs.SimplePictureBoxs.SimplePictureBox.OnPictrueBoxClickListener(this.simplePictureBox2_OnPictrueBoxClickListenerEvent);
            this.simplePictureBox2.Load += new System.EventHandler(this.simplePictureBox2_Load);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.simplePictureBox2);
            this.Controls.Add(this.simplePictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private FishyuSelfControl.CommonPictureBoxs.SimplePictureBoxs.SimplePictureBox simplePictureBox1;
        private FishyuSelfControl.CommonPictureBoxs.SimplePictureBoxs.SimplePictureBox simplePictureBox2;
    }
}