using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace FishyuSelfControl.FishYuReportView.AutoSortReportView.DataGridViews
{
    public interface ICellSelfDefineView
    {
        void DrawSelfDefineCell(Graphics graphics, Cell cell);
    }
}
