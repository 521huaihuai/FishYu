using System;
using System.Collections.Generic;
using System.Text;
using log4net;

namespace FinshYuUtils
{
    public class Log
    {
        private static ILog s_ilLog;
        public static string LOGNAME = "FishYu Log";

        public static ILog Write
        {
            get { return Log.s_ilLog; }
            set { Log.s_ilLog = value; }
        }

        /// <summary>
        /// 设置日志模块名
        /// </summary>
        /// <param name="szLogModName"></param>
        static void SetLogName(string szLogModName)
        {
            LOGNAME = szLogModName + " Log";
        }

        static Log()
        {
            s_ilLog = LogManager.GetLogger(LOGNAME);
        }
    }
}
