using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ReportFormDesign
{
    class Program
    {
        static void Main(string[] args)
        {
            //ReportForm reportForm = new ReportForm();
            //Application.Run(reportForm);

            //ReportViewForCircular circle = new ReportViewForCircular();
            //Application.Run(circle);

            //for (int i = 0; i < 10000000; i++)
            //{
            //    Test test = new Test();
            //    test.Show();
            //    test.Close();
            //}

            //Radius_Rectangle_ReportView_ test1 = new Radius_Rectangle_ReportView_();
            //Application.Run(test1);

            CircleSpliteReportViewTest circle = new CircleSpliteReportViewTest();
            Application.Run(circle);

            //ArcSpliteTest arc = new ArcSpliteTest();
            //Application.Run(arc);
        }
    }
}
