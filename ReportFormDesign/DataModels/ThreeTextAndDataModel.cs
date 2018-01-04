using System;
using System.Collections.Generic;
using System.Text;

namespace ReportFormDesign.Model
{
    class ThreeTextAndDataModel : ChildDataModel
    {
        public ThreeTextAndDataModel(string mainText, int mainData, string childText, int childData, string text, int data):base(mainText, mainData, childText, childData)
        {
            this.Text = text;
            this.Data = data;
        }

        public string Text { get; set; }
        public int Data { get; set; }
    }
}
