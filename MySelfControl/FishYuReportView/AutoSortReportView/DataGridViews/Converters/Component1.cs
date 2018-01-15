using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;

namespace FishyuSelfControl.FishYuReportView.AutoSortReportView.DataGridViews.Converters
{
    public partial class Component1 : Component
    {
        public Component1()
        {
            InitializeComponent();
        }

        public Component1(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        //使用自定义TypeConverter生成设计时代码 
        private Person _Person = new Person("12", "22" , 12);

        [Browsable(true)]
        public Person Person
        {
            get
            {
                return _Person;
            }

            set
            {
                this._Person = value;
            }
        }
    }
}
