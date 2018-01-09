using System;
using System.Collections.Generic;
using System.Text;
using ReportFormDesign.DrawUtils;
using ReportFormDesign.Model;

namespace ReportFormDesign.Adapter
{
    class SingleReportViewAdapter : ReportViewAdapter
    {

        public AreaPositionRect posRectData;
        public float startAngle1 = 360;

        /// <summary>
        /// 数据之和
        /// </summary>
        public int maxNum;
        public override bool next()
        {
            index++;
            if (DataList.Count <= index)
            {
                return false;
            }
            return true;
        }

        public override int getIndex()
        {
            return index;
        }

        public override DataModel getItem()
        {
            if (index > -1)
            {
                return DataList[index];
            }
            return null;
        }


        //public void setData(List<DataModel> list)
        //{
        //    if (list != null)
        //    {

        //        this.list = list;
        //    }
        //    foreach (var item in list)
        //    {
        //        maxNum += item.mainData;
        //    }
        //}

        public override DrawUtils.AreaPositionRect getPositionRect()
        {
            DataModel data = DataList[index];
            return data.Area;
        }

        public override void setBasePostitionRect(DrawUtils.AreaPositionRect posRectData)
        {

            this.posRectData = posRectData;
        }

        public override int getCount()
        {
            if (DataList != null)
            {
                return DataList.Count;
            }
            return -1;
        }
    }
}
