using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FinshYuUtils.ImageUtils
{
    public class StaticImageLoad
    {

        /// <summary>
        /// 加载图片资源(静态)
        /// </summary>
        /// <param name="relativePath">相对路径</param>
        /// <returns>图片</returns>
        public static Image LoadImage(string relativePath)
        {
            if (string.IsNullOrEmpty(relativePath))
            {
                return null;
            }
            if (!relativePath.Substring(0, 1).Equals("\\"))
            {
                relativePath = "\\" + relativePath;
            }
            if (LicenseManager.UsageMode == LicenseUsageMode.Runtime)
            {
                return Image.FromFile(Application.StartupPath + relativePath);
            }
            return null;
        }
    }
}
