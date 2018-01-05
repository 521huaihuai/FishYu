using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using ZjmSelfControl;

namespace FishyuSelfControl.FishyuAnimateImage
{
    public partial class WaitForm : Form
    {
        private Form form;
        //IntPtr hDesktop;
        public const int GW_CHILD = 5;

        public WaitForm(Control parent, bool isInMainThread)
        {
            InitializeComponent();
            //isMouseDown = false;
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
            FormBorderStyle = FormBorderStyle.None;
            BackColor = Color.White;
            ShowInTaskbar = false;
            TransparencyKey = Color.White;
            StartPosition = FormStartPosition.Manual;
            if (parent != null)
            {
                Location = parent.Location;
                CalculateParentLocation(parent);
                Size = parent.Size;
            }
            else
            {
                Height = Screen.PrimaryScreen.Bounds.Height;
                Width = Screen.PrimaryScreen.Bounds.Width;
            }
            TopMost = true;
            SetTopLevel(true);
            if (isInMainThread)
            {
                form = new Form()
                {
                    FormBorderStyle = FormBorderStyle.None,
                    ShowInTaskbar = false,
                    Opacity = 0.01,//背景透明不穿透鼠标
                    TopMost = true,//让不穿透鼠标透明窗体画板为最上层
                    StartPosition = FormStartPosition.Manual
                };//不穿透鼠标透明窗体
                if (parent != null)
                {
                    form.Location = parent.Location;
                    form.Size = parent.Size;
                }
                else
                {
                    form.WindowState = FormWindowState.Maximized;//最大化
                }
                form.Show();
            }
            //hDesktop = GetDesktopHandle(DesktopLayer.Progman);
            //EmbedDesktop(this, this.Handle, this.hDesktop);
        }

        private void CalculateParentLocation(Control parent)
        {
            if (parent.Parent != null)
            {
                Location = new Point(Location.X + parent.Parent.Location.X, Location.Y + parent.Parent.Location.Y);
                CalculateParentLocation(parent.Parent);
            }
            else
            {
                return;
            }
        }

        //public IntPtr GetDesktopHandle(DesktopLayer layer)
        //{ 
        //    //hWnd = new HandleRef();
        //    HandleRef hWnd;
        //    IntPtr hDesktop = new IntPtr();
        //    switch (layer)
        //    {
        //        case DesktopLayer.Progman:
        //            hDesktop = Win32Support.FindWindow("Progman", null);//第一层桌面
        //            break;
        //        case DesktopLayer.SHELLDLL:
        //            hDesktop = Win32Support.FindWindow("Progman", null);//第一层桌面
        //            hWnd = new HandleRef(this, hDesktop);
        //            hDesktop = Win32Support.GetWindow(hWnd, GW_CHILD);//第2层桌面
        //            break;
        //        case DesktopLayer.FolderView:
        //            hDesktop = Win32Support.FindWindow("Progman", null);//第一层桌面
        //            hWnd = new HandleRef(this, hDesktop);
        //            hDesktop = Win32Support.GetWindow(hWnd, GW_CHILD);//第2层桌面
        //            hWnd = new HandleRef(this, hDesktop);
        //            hDesktop = Win32Support.GetWindow(hWnd, GW_CHILD);//第3层桌面
        //            break;
        //    }
        //    return hDesktop;
        //}

        //public void EmbedDesktop(Object embeddedWindow, IntPtr childWindow, IntPtr parentWindow)
        //{
        //    Form window = (Form)embeddedWindow;
        //    HandleRef HWND_BOTTOM = new HandleRef(embeddedWindow, new IntPtr(1));
        //    const int SWP_FRAMECHANGED = 0x0020;//发送窗口大小改变消息
        //    Win32Support.SetParent(childWindow, parentWindow);
        //    Win32Support.SetWindowPos(new HandleRef(window, childWindow), HWND_BOTTOM, 300, 300, window.Width, window.Height, SWP_FRAMECHANGED);
        //}


        protected override void OnClosed(EventArgs e)
        {
            if (form != null && !form.IsDisposed)
            {
                form.Close();
                form = null;
            }
            base.OnClosed(e);
        }
    }
}
