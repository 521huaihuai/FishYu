/*
* ==============================================================================
*
* File name: EnLargeTest
* Description: 
*
* Version: 1.0
* Created: 2018/3/23 13:38:50
*
* Author: zjm
*
* ==============================================================================
*/
using FishyuSelfControl.CommonPictureBoxs.EnlargePopuPicture;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test.DemoTest.EnLargeViewTest
{
    public partial class EnLargeTest : Form
    {
        private IEnlargPicture enlargPicture;

        public EnLargeTest()
        {
            InitializeComponent();

            LocationChanged += EnLargeTest_LocationChanged;
        }

        private void EnLargeTest_LocationChanged(object sender, EventArgs e)
        {
            enlargPicture.ChangeStartPosition(Location);
        }

        private void EnLargeTest_Load(object sender, EventArgs e)
        {
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            enlargPicture.MoveEnlargePicture(e.Location, "");
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            enlargPicture.HidePopuView();
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            enlargPicture = new EnlargeImageImp(pictureBox1.Image, Location, pictureBox1.Width, pictureBox1.Height);
        }
    }
}
