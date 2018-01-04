using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ReportFormDesign.Model
{
    class DataBeanFactory
    {
        internal static DataBean CreateData()
        {

            DataBean bean = new DataBean();
            bean.Title = "实时流量";
            bean.Lable_X = "时间";
            bean.Lable_Y = "流量";
            bean.Y_Data = new int[]{20, 100, 150, 152, 300, 250, 220, 230, 350};
            bean.X_Data = new string[] { "09:25", "09:30", "09:35", "09:40", "09:45", "09:50", "09:55" };
            bean.CompontData = new string[]{"交通事故", "112", "未处理", "0" };
            return bean;
        }

        public static DataBean CreateTestData_02()
        {
            DataBean bean = new DataBean();
            bean.Title = "状态不一致";
            bean.Lable_X = "当前实时数据";
            bean.Lable_Y = "个数";
            bean.Y_Data = new int[] { 20, 100, 150, 152, 300, 250, 220, 230, 350 };
            bean.X_Data = new string[] {};
            bean.CompontData = new string[] {};
            return bean;
        }

        //把数据库获取的数据转化到数据模型中
        public DataBean convertDataModel2ReportData(List<DataModel> dataModels)
        {
            DataBean dataBean = new DataBean();
            int count = dataModels.Count;
            string[] X_Data = new string[count];
            int[] Y_Data = new int[count];
            string[] compontData = new string[count];
            foreach (DataModel item in dataModels)
            {
                //把dataModeld中的字段赋值到Data中


            }
            dataBean.X_Data = X_Data;
            dataBean.Y_Data = Y_Data;
            dataBean.CompontData = compontData;
            return dataBean;
        }

    }
}
