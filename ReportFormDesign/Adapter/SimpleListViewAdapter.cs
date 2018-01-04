using System;
using System.Collections.Generic;
using System.Text;
using ReportFormDesign.Model;
using ReportFormDesign.DrawUtils;
using ReportFormDesign.ReportViewPanel;
using System.Windows.Forms;

namespace ReportFormDesign.Adapter
{
    /// <summary>
    /// 适合多张报表的展示
    /// </summary>
    public class SimpleListViewAdapter : ReportViewAdapter
    {


        public SimpleListViewAdapter()
        {
            LevelShowCount = 3;
        }

        /// <summary>
        /// 下一个数据源
        /// </summary>
        /// <returns></returns>
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

        public override DrawUtils.AreaPositionRect getPositionRect()
        {
            if (index > -1)
            {
                if (PosRectData != null)
                {
                    if (DataList.Count == 0)
                    {
                        //MessageBox.Show("没有数据源!");//这里处理了bb的异常信息
                        return null;
                    }
                    return DataList[index].Area;
                    //PosRectData = datamode.Area;
                    //int height = PosRectData.bottom - PosRectData.top;
                    //int width = PosRectData.right - PosRectData.left;
                    //if (IsVerticalShowData)
                    //{
                    //    return new DrawUtils.AreaPositionRect(PosRectData.left, index * (height + PosRectData.PaddingIn) + PosRectData.top, PosRectData.right, PosRectData.bottom + index * (height + PosRectData.PaddingIn));
                    //}
                    //else
                    //{
                    //    int yCount = index / LevelShowCount;
                    //    int xCount = index % LevelShowCount;
                    //    return new DrawUtils.AreaPositionRect(PosRectData.left + xCount * (width + PosRectData.PaddingIn), PosRectData.top + yCount * (height + PosRectData.PaddingIn + BottomPadding), PosRectData.right + xCount * (width + PosRectData.PaddingIn), PosRectData.bottom + yCount * (height + PosRectData.PaddingIn + BottomPadding));
                    //}
                   
                    
                }
            }
            return null;
        }

        public override void setBasePostitionRect(DrawUtils.AreaPositionRect posRectData)
        {
            this.PosRectData = posRectData;
            int height = PosRectData.bottom - PosRectData.top;
            int width = PosRectData.right - PosRectData.left;
            foreach (var item in DataList)
            {
                index++;
                if (IsVerticalShowData)
                {
                    item.Area =  new DrawUtils.AreaPositionRect(PosRectData.left, index * (height + PosRectData.PaddingIn) + PosRectData.top, PosRectData.right, PosRectData.bottom + index * (height + PosRectData.PaddingIn));
                    item.Area.PaddingIn = posRectData.PaddingIn;
                }
                else
                {
                    int yCount = index / LevelShowCount;
                    int xCount = index % LevelShowCount;
                    item.Area = new DrawUtils.AreaPositionRect(PosRectData.left + xCount * (width + PosRectData.PaddingIn), PosRectData.top + yCount * (height + PosRectData.PaddingIn + BottomPadding), PosRectData.right + xCount * (width + PosRectData.PaddingIn), PosRectData.bottom + yCount * (height + PosRectData.PaddingIn + BottomPadding));
                }
               
            }
            index = -1;
        }

        public override int getCount()
        {
            if (DataList != null)
            {
                return DataList.Count;
            }
            return -1;
        }

        /// <summary>
        /// 
        /// </summary>
        public int VerticalShowCount { get; set; }

        /// <summary>
        /// 多条数据之间间距
        /// </summary>
        public int BottomPadding { get; set; }
    }
}
