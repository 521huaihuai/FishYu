﻿using System;
using System.Collections.Generic;

namespace ReportFormDesign.Model
{
    class ChildDataModel : DataModel
    {

        public ChildDataModel(string mainText, int mainData, string childText, int childData):base(mainText, mainData)
        {
            this.childText = childText;
            this.childData = childData;
        }

        public string childText { get; set; }
        public int childData { get; set; }
    }
}
