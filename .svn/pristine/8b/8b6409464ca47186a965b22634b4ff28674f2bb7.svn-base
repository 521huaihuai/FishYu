using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using ReportFormDesign.DataModels;
using ReportFormDesign.Model;

namespace ReportFormDesign
{
    /// <summary>
    /// 坐标实例对象
    /// </summary>
    public class CoordinateDataBean
    {

        public CoordinateDataBean()
        {
            Title = "报表";
            Lable_X = "x";
            Lable_Y = "y";
        }

        public CoordinateDataBean(List<DataModel> CoordinateModels)
        {
            Title = "报表";
            Lable_X = "x";
            Lable_Y = "y";
        }



        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// x轴数据
        /// </summary>
        public string[] X_Data { get; set; }

        /// <summary>
        /// y轴数据
        /// </summary>
        public float[] Y_Data { get; set; }

        /// <summary>
        /// Y轴标签
        /// </summary>
        public string Lable_Y { get; set; }

        /// <summary>
        /// X轴标签
        /// </summary>
        public string Lable_X { get; set; }
        
        /// <summary>
        /// 其他组件数据源
        /// </summary>
        public string[] CompontData { get; set; }

        /// <summary>
        /// 数据源
        /// </summary>
        public List<DataModel> CoordinateModelList { get; set; }

        /// <summary>
        /// 初始化XY数据(把List数据导入)
        /// </summary>
        public void initXYData()
        {
            if (CoordinateModelList != null)
            {
                X_Data = new string[CoordinateModelList.Count];
                Y_Data = new float[CoordinateModelList.Count];
                DataModel model = null;
                for (int i = 0; i < CoordinateModelList.Count; i++)
                {
                    model = CoordinateModelList[i];
                    X_Data[i] = model.mainText;
                    Y_Data[i] = model.mainData;
                }
            }
        }
    }
}
