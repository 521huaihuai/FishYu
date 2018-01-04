using System;
using System.ComponentModel;

namespace FinshYuUtils.CommonUtils
{
    public class DesignModeUtil : Component
    {
        /// <summary>  
        /// 是否处于设计器模式  
        /// </summary>  
        private bool isDesignMode = false;
        /// <summary>  
        /// 唯一实例  
        /// </summary>  
        private static DesignModeUtil instance = null;

        /// <summary>  
        /// 创建一个新的DesignModeUtil对象  
        /// </summary>  
        private DesignModeUtil()
        {
        }

        /// <summary>  
        /// 获取DesignModeUtil唯一对象  
        /// </summary>  
        public static DesignModeUtil Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DesignModeUtil();

                    instance.isDesignMode = instance.GetIsDesignMode();
                    //if (instance.isDesignMode)
                    //{
                    //    Console.WriteLine("design");
                    //}
                }

                return instance;
            }
        }

        /// <summary>  
        /// 获取是否处于设计器模式  
        /// </summary>  
        public bool IsDesignMode
        {
            get
            {
                return isDesignMode;
            }
        }

        /// <summary>  
        /// 获取当前是否处于设计器模式  
        /// </summary>  
        /// <remarks>  
        /// 在程序初始化时获取一次比较准确，若需要时获取可能由于布局嵌套导致获取不正确，如GridControl-GridView组合。  
        /// </remarks>  
        /// <returns>是否为设计器模式</returns>  
        private bool GetIsDesignMode()
        {
            return (this.GetService(typeof(System.ComponentModel.Design.IDesignerHost)) != null
                || LicenseManager.UsageMode == LicenseUsageMode.Designtime);
        }
    }
}