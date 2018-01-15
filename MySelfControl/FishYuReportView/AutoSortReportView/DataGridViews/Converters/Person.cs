/* ======================================================================== 
* 本类功能概述:
* 
* 作者：zjm 
* 时间：2018/1/15 16:02:43 
* 文件名：Person 
* 版本：V1.0.1 
* 
* 修改者： 时间： 
* 修改说明： 
* ======================================================================== 
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FishyuSelfControl.FishYuReportView.AutoSortReportView.DataGridViews.Converters
{
    [TypeConverter(typeof(PersonConverter))] //自定义类型转换器
    public class Person
    {
        private string _FirstName = "";
        private string _LastName = "";
        private int _Age = 0;

        public Person(string firstName, string lastName, int age)
        {
            _FirstName = firstName;
            _LastName = lastName;
            _Age = age;
        }

        public int Age
        {
            get { return _Age; }
            set { _Age = value; }
        }

        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }

        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }

    }
}