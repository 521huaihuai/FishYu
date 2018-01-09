using System;
using System.Collections.Generic;
using System.Text;
using ReportFormDesign.Model;
using ReportFormDesign.DrawUtils;
using ReportFormDesign.ReportViewPanel;

namespace ReportFormDesign.Adapter
{
    public class SimapleListViewAdapter : ReportViewAdapter
    {

        public AreaPositionRect posRectData;
        /// <summary>
        /// 下一个数据源
        /// </summary>
        /// <returns></returns>
        public override bool next()
        {
            index++;
            if (list.Count <= index)
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
                return list[index];
            }
            return null;
        }

        public override DrawUtils.AreaPositionRect getPositionRect()
        {
            if (index > -1 && index > list.Count)
            {
                if (posRectData != null)
                {

                    DataModel datamode = list[index];
                    int height = posRectData.bottom - posRectData.top;
                    int width = posRectData.right - posRectData.left;
                    if (ViewPanel.isVerticalShowData)
                    {
                        return new DrawUtils.AreaPositionRect(posRectData.left, index * (height + ViewPanel.padding) + posRectData.top, posRectData.right, posRectData.bottom + index * (height + ViewPanel.padding));
                    }
                    else
                    {
                        return new DrawUtils.AreaPositionRect(posRectData.left + index * (width + ViewPanel.padding), posRectData.top, posRectData.right + index * (width + ViewPanel.padding), posRectData.bottom);
                    }


                }
            }
            return null;
        }

        public override void setBasePostitionRect(DrawUtils.AreaPositionRect posRectData)
        {
            this.posRectData = posRectData;
        }

        public override int getCount()
        {
            if (list != null)
            {
                return list.Count;
            }
            return -1;
        }
    }
}
