using FishyuAnimation.Animations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FishYuAnimationWinFormDemo
{
    public partial class Form1 : Form
    {
        private TranslationAnimation animation;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            simpleAnimationControl1.animation.StartAnimalion();
            simpleAnimationControl2.animation.StartAnimalion();
            simpleAnimationControl3.animation.StartAnimalion();
            animation = new TranslationAnimation();
            animation.TranslateStart(simpleAnimationControl1, new Point(Width, Height));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            animation.PauseAnimation();
            if (simpleAnimationControl1.animation.AnimationState == FishyuAnimation.Animations.Animation.AnimationStates.AnimationPause)
            {
                simpleAnimationControl1.animation.RecoveryAnimation();
            }
            else
            {
                simpleAnimationControl1.animation.PauseAnimation();
            }
            
        }
    }
}
