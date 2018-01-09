using FishyuSelfControl.FishyuAnimateImage;
using FishyuSelfControl.SimpleMessageBoxs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void simplePictureBox1_Load(object sender, EventArgs e)
        {

        }

        private void simplePictureBox1_OnPictrueBoxClickListenerEvent()
        {
            AnimateWaitForm.AnimatingWait(() =>
            {
                Thread.Sleep(3000);
            }, this);

        }

        private void simplePictureBox2_Load(object sender, EventArgs e)
        {
           
        }

        private void simplePictureBox2_OnPictrueBoxClickListenerEvent()
        {
            AnimateWaitForm.AnimatingWait(() =>
            {
                Thread.Sleep(3000);
            }, this, true);
        }

        private void simplePictureBox3_OnPictrueBoxClickListenerEvent()
        {
            SimpleMessageBox.ShowMessageBox("这是智能指示");
        }
    }
}
