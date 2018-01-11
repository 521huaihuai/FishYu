using System;
using System.Collections.Generic;
using System.Text;

namespace FinshYuUtils
{
    public class SystemUtil
    {
        private const string Windows2000 = "5.0";
        private const string WindowsXP = "5.1";
        private const string Windows2003 = "5.2";
        private const string Windows2008 = "6.0";
        private const string Windows7 = "6.1";
        private const string Windows8OrWindows81 = "6.2";
        private const string Windows10 = "10.0";
        public const string XP = "WindowsXP";


        private static string _systemVerionName;
        public static string SystemVerionName { get { return _systemVerionName; } }

        static SystemUtil()
        {
            GetOSystem();
        }

        private static void GetOSystem()
        {
            switch (System.Environment.OSVersion.Version.Major + "." + System.Environment.OSVersion.Version.Minor)
            {
                case Windows2000:
                    setOSystemName("Windows2000");
                    break;
                case WindowsXP:
                    setOSystemName("WindowsXP");
                    break;
                case Windows2003:
                    setOSystemName("Windows2003");
                    break;
                case Windows2008:
                    setOSystemName("Windows2008");
                    break;
                case Windows7:
                    setOSystemName("Windows7");
                    break;
                case Windows8OrWindows81:
                    setOSystemName("Windows8.OrWindows8.1");
                    break;
                case Windows10:
                    setOSystemName("Windows10");
                    break;
            }
        }

        private static void setOSystemName(string p)
        {
            _systemVerionName = p;
        }

    }
}
