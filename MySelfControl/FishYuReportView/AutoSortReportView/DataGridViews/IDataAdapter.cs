using System;
using System.Collections.Generic;
using System.Text;

namespace FishyuSelfControl.FishYuReportView.AutoSortReportView.DataGridViews
{
    public interface IDataAdapter
    {
        void SetData(object data, Dictionary<string, Cell>cells);
    }
}
