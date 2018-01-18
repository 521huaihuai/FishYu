/* ======================================================================== 
* 本类功能概述:列工厂
* 
* 作者：zjm 
* 时间：2018/1/15 10:50:36 
* 文件名：ColumnFactory 
* 版本：V1.0.1 
* 
* 修改者： 时间： 
* 修改说明： 
* ======================================================================== 
*/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace FishyuSelfControl.FishYuReportView.AutoSortReportView.DataGridViews
{
    public class ColumnFactory
    {

        private static ColumnFactory _instance = new ColumnFactory();

        private ColumnFactory()
        {

        }

        public static ColumnFactory Instance { get { return _instance; } }


        /// <summary>
        /// 创建一列默认样式, 默认type为文本格式
        /// </summary>
        /// <param name="name">列名</param>
        /// <param name="pIndex">某父列的Index</param>
        /// <param name="index">该列的index</param>
        /// <param name="rowIndex"></param>
        /// <param name="pIndex"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="fishYuCellStyle"></param>
        /// <returns></returns>
        public Column CreateTitleColumn(string name, int index, int rowIndex = 0, int pIndex = 0, int width = 100, int height = 35, FishYuCellStyle fishYuCellStyle = null)
        {
            Column column = new Column();
            column.Name = name;
            column.Index = index;
            column.PColumnIndex = pIndex;
            column.RowIndex = rowIndex;
            column.PRowIndex = rowIndex - 1;
            column.Width = width;
            column.Height = height;
            if (fishYuCellStyle == null)
            {
                fishYuCellStyle = FishYuCellStyle.DefaultCellStyle;
            }
            column.CellStyle = fishYuCellStyle;
            return column;
        }

        public List<Column> SortColumns(List<Column>columns)
        {
            List<Column> list = new List<Column>();
            if (columns != null)
            {
                int minRowIndex = -1;

                Dictionary<int, Dictionary<int, Column>> allDict = new Dictionary<int, Dictionary<int, Column>>();

                // 初始赋值
                foreach (var item in columns)
                {
                    if (!allDict.ContainsKey(item.RowIndex))
                    {
                        Dictionary<int, Column> dictionary = new Dictionary<int, Column>();
                        dictionary.Add(item.Index, item);
                        allDict.Add(item.RowIndex, dictionary);
                    }
                    else
                    {
                        if (!allDict[item.RowIndex].ContainsKey(item.Index))
                        {
                            allDict[item.RowIndex].Add(item.Index, item);
                        }
                    }
                }


                // 建立关系
                foreach (var item in columns)
                {
                    if (minRowIndex > item.PRowIndex)
                    {
                        minRowIndex = item.PRowIndex;
                    }
                    if (!allDict.ContainsKey(item.PRowIndex))
                    {
                        Column column = new Column();
                        Dictionary<int, Column> dictionary = new Dictionary<int, Column>();
                        dictionary.Add(item.PColumnIndex, column);
                        allDict.Add(item.PRowIndex, dictionary);
                        allDict[item.PRowIndex][item.PColumnIndex].ChildColumns = new List<Column>();
                        allDict[item.PRowIndex][item.PColumnIndex].ChildColumns.Add(item);
                    }
                    else
                    {
                        if (allDict[item.PRowIndex].ContainsKey(item.PColumnIndex))
                        {
                            allDict[item.PRowIndex][item.PColumnIndex].AddColumn(item);
                        }
                        else
                        {
                            allDict[item.PRowIndex].Add(item.PColumnIndex, item);
                        }
                    }
                }



                if (allDict[minRowIndex].Values.Count > 0)
                {
                    ResizeColumn(allDict[minRowIndex][0], new Point(0, 0));
                    return allDict[minRowIndex][0].ChildColumns;
                }
            }
            return list;
        }

        private void ResizeColumn(Column column, Point point)
        {
            if (column != null && column.ChildColumns != null)
            {
                foreach (var item in column.ChildColumns)
                {
                    item.X = point.X;
                    item.Y = point.Y;
                    ResizeColumn(item, new Point(item.X, item.Height + point.Y));
                    point.X = item.X + item.Width;
                }
            }
        }
    }
}