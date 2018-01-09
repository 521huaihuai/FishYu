using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ReportFormDesign.Model
{
    class DataBeanFactory
    {
        internal static CoordinateDataBean CreateData()
        {

            CoordinateDataBean bean = new CoordinateDataBean();
            bean.Title = "实时流量";
            bean.Lable_X = "时间";
            bean.Lable_Y = "流量";
            bean.Y_Data = new float[] { 20, 100, 150, 152, 300, 250, 220, 230, 350 };
            bean.X_Data = new string[] { "09:25", "09:30", "09:35", "09:40", "09:45", "09:50", "09:55" };
            bean.CompontData = new string[]{"交通事故", "112", "未处理", "0" };
            return bean;
        }

        public static CoordinateDataBean CreateTestData_02()
        {
            CoordinateDataBean bean = new CoordinateDataBean();
            bean.Title = "状态不一致";
            bean.Lable_X = "当前实时数据";
            bean.Lable_Y = "个数";
            bean.Y_Data = new float[] { 20, 100, 150, 152, 300, 250, 220, 230, 350 };
            bean.X_Data = new string[] {};
            bean.CompontData = new string[] {};
            return bean;
        }

        //把数据库获取的数据转化到数据模型中
        public CoordinateDataBean ConvertDataModel2CoordinateDataModel(List<DataModel> dataModels)
        {
            CoordinateDataBean dataBean = new CoordinateDataBean();
            int count = dataModels.Count;
            string[] X_Data = new string[count];
            float[] Y_Data = new float[count];
            //把dataModeld中的字段赋值到Data中
            DataModel item = null;
            for (int i = 0; i < count; i++)
            {
                item = dataModels[i];
                X_Data[i] = item.mainText;
                Y_Data[i] = item.mainData;
            }
            dataBean.X_Data = X_Data;
            dataBean.Y_Data = Y_Data;
            return dataBean;
        }

    }
}
