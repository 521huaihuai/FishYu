using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using ReportFormDesign.DrawUtils;

namespace ReportFormDesign.Model
{
    /// <summary>
    /// 绘制弹出窗口
    /// </summary>
    public class FollowPopViewForRate : ReportViewToolTip
    {

        public override void detailDraw(Graphics g, DataModel model, Brush TextBrush, Brush DataBrush, Brush BackGroundBrush, Font TextFont, Font DataFont)
        {
            if (model != null)
            {
                GraphicsPath path = null;
                if (IsOverWidth(model))
                {
                    string tip = "字数过长, 请双击大屏展示";
                    TextFont.Dispose();
                    TextFont = new Font("Consolas", 9, FontStyle.Bold);
                    ResizeWithAndHeight(g, tip, "", "", TextFont, DataFont);
                    Rectangle rect = new Rectangle(LocalPosition.X + Padding, LocalPosition.Y, Width, Height);
                    path = ReportViewUtils.CreateRoundedRectanglePath(rect, Height / 3);
                    //绘制底色
                    g.FillPath(BackGroundBrush, path);
                    TextBrush.Dispose();
                    TextBrush = new SolidBrush(ReportViewUtils.perferRed);
                    ReportViewUtils.drawString(g, LocationModel.Location_Center, tip, TextFont, TextBrush, LocalPosition.X + Padding, LocalPosition.Y, this.Width, this.Height);
                }
                else
                {
                    if (model is ReportFormDesign.DataModels.AutoSortDataModel)
                    {
                        ReportFormDesign.DataModels.AutoSortDataModel autoModel = model as ReportFormDesign.DataModels.AutoSortDataModel;
                        Rectangle rect = new Rectangle(LocalPosition.X + Padding, LocalPosition.Y, Width, Height);
                        path = ReportViewUtils.CreateRoundedRectanglePath(rect, Height / 3);
                        //绘制底色
                        g.FillPath(BackGroundBrush, path);
                        ReportViewUtils.drawString(g, LocationModel.Location_Up, autoModel.mainText, TextFont, TextBrush, LocalPosition.X + Padding, LocalPosition.Y, this.Width, this.Height);
                        string rate = "未检测";
                        if ( autoModel.MaxData != 0)
                        {
                            rate = Math.Round(autoModel.mainData * 100.0f / autoModel.MaxData, 2) + "%";
                        }
                        ReportViewUtils.drawString(g, LocationModel.Location_Down, rate, TextFont, TextBrush, LocalPosition.X + Padding, LocalPosition.Y, this.Width, this.Height);
                    }
                    
                }
                path.Dispose();
            }
        }

        private bool IsOverWidth(DataModel model)
        {
            int width = model.Area.right - model.Area.left;
            return this.Width > width;
        }
    }
}
