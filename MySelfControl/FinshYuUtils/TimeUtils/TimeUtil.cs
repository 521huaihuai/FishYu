#region Version Info
/* ======================================================================== 
* 本类功能概述:时间帮助类
* 
* 作者：zjm 
* 时间：2018/1/5 9:48:19 
* 文件名：TimeUtil 
* 版本：V1.0.1 
* 
* 修改者： 时间： 
* 修改说明： 
* ======================================================================== 
*/
#endregion
using System;
using System.Collections.Generic;
using System.Text;

namespace FinshYuUtils.TimeUtils
{
    public class TimeUtil
    {
        public static string ConvertDateTime2String(DateTime dateTime, string format = "yyyy-MM-dd HH:mm:ss")
        {
            if (dateTime != null)
            {
                return dateTime.ToString(format);
            }
            return "";
        }


        public static DateTime ConvertString2DateTime(string dateTime)
        {
            if (!string.IsNullOrEmpty(dateTime))
            {
                return Convert.ToDateTime(dateTime);
            }
            return DateTime.MinValue;
        }


        public static DateTime ConvertTimeStamp2DateTime(long? timeStamp)
        {
            if (timeStamp.HasValue)
            {
                DateTime dateStart = new DateTime(1970, 1, 1, 8, 0, 0);
                TimeSpan toNow = new TimeSpan(timeStamp.Value * 10000);
                DateTime targetDt = dateStart.Add(toNow);
                return targetDt;
            }
            return DateTime.MinValue;
        }

        public static string ConvertTimeStamp2DateTimeString(long? timeStamp, string format = "yyyy-MM-dd HH:mm:ss")
        {
            return ConvertTimeStamp2DateTime(timeStamp).ToString(format);
        }

        public static string ConvertTimeStamp2DateTimeString(object timeStamp, string format = "yyyy-MM-dd HH:mm:ss")
        {
            long result = 0;
            long.TryParse(timeStamp + "", out result);
            return ConvertTimeStamp2DateTime(result).ToString(format);
        }

        public static DateTime ConvertLong2DateTime(long? time)
        {
            if (time.HasValue)
            {
                DateTime dateStart = new DateTime(1970, 1, 1, 8, 0, 0);
                TimeSpan toNow = new TimeSpan(time.Value * 10000000);
                DateTime targetDt = dateStart.Add(toNow);
                return targetDt;
            }
            return DateTime.MinValue;
        }

        public static DateTime GetTodayStartTime()
        {
            string time = DateTime.Now.ToString("yyyy-MM-dd");
            return Convert.ToDateTime(time + " 00:00:00");
        }

        public static DateTime GetTodayEndTime()
        {
            string time = DateTime.Now.ToString("yyyy-MM-dd");
            return Convert.ToDateTime(time + " 23:59:59");
        }
    }
}
