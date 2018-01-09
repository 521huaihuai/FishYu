using System;
using System.Collections.Generic;
using System.Text;
using ReportFormDesign.DrawUtils;

namespace ReportFormDesign.Model
{
    public abstract class ReportViewAdapter
    {


        public AreaPositionRect PosRectData { get; set; }

        public List<DataModel> DataList { get; set; }
        protected int index = -1;

        /// <summary>
        /// 在设置水平展示的情况下, 水平展示的最大报表数量
        /// </summary>
        public int LevelShowCount { get; set; }
        public ReportViewAdapter()
        {
            IsVerticalShowData = true;
        }
        public void setData(List<DataModel> list)
        {
            if (list != null)
            {

                this.DataList = list;
            }
            else
            {
                list = new List<DataModel>();
            }
        }

        //下一个数据
        public abstract bool next();

        //获取index
        public abstract int getIndex();

        //获取数据源
        public abstract DataModel getItem();

        //获取下一个数据的位置信息
        public abstract AreaPositionRect getPositionRect();

        //设置最基础的位置边界信息
        public abstract void setBasePostitionRect(AreaPositionRect posRectData);

        //得到数据的条目数量
        public abstract int getCount();


        public void setIndex(int index)
        {
            this.index = index;
        }


        internal void NotifyDataChangeUpdate(List<DataModel> modles)
        {
            if (modles != null)
            {
                this.DataList = modles;
            }
        }

        /// <summary>
        /// 是否垂直展示数据
        /// </summary>
        public bool IsVerticalShowData { get; set; }
    }
}
