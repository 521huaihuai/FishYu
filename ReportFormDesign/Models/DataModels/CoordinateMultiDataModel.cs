using System;
using System.Collections.Generic;
using System.Text;
using ReportFormDesign.Model;

namespace ReportFormDesign.Models.DataModels
{
    public class CoordinateMultiDataModel : DataModel
    {
        public List<DataModel> dataList = new List<DataModel>();

        public CoordinateMultiDataModel()
        {
            Area.IsCoordinateModel = true;
        }

        public void AddModel(DataModel model)
        {
            dataList.Add(model);
        }
    }
}
