using FinshYuUtils;
using FinshYuUtils.ExcelUtils;
using FinshYuUtils.JumpViewUtils;
using FishyuSelfControl.FishyuAnimateImage;
using FishyuSelfControl.SimpleMessageBoxs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace FishyuSelfControl.CommonDataGridViews
{
    public class ExcelFactory
    {

        /// <summary>
        /// 需要配合waitAct中进行ExportInfoData()操作
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="waitAct">调用ExportInfoData()</param>
        public static void OpenSaveDialog(string fileName, WaitAction waitAct)
        {
            string strFileName = string.Format("{0}{1:yyyyMMddHHmmss}", fileName, DateTime.Now);
            SaveFileDialog SaveFileDlg = new SaveFileDialog();
            SaveFileDlg.Filter = "Excel文件(*.xls)|*.xls|Excel文件(*.xlsx)|*.xlsx";
            SaveFileDlg.FileName = strFileName;
            if (DialogResult.OK == SaveFileDlg.ShowDialog())
            {
                strFileName = SaveFileDlg.FileName;
                try
                {
                    waitAct(strFileName);
                }
                catch (Exception ex)
                {
                    //Log.Write.Error(ex.Message, ex);
                }
            }

            SaveFileDlg.Dispose();
        }


        public static void OpenSaveDialog(string fileName, DataGridView uvDataGridView1, string title = "")
        {
            string strFileName = string.Format("{0}{1:yyyyMMddHHmmss}", fileName, DateTime.Now);
            SaveFileDialog SaveFileDlg = new SaveFileDialog();
            SaveFileDlg.Filter = "Excel文件(*.xls)|*.xls|Excel文件(*.xlsx)|*.xlsx";
            SaveFileDlg.FileName = strFileName;
            if (DialogResult.OK == SaveFileDlg.ShowDialog())
            {
                strFileName = SaveFileDlg.FileName;
                try
                {
                    ExportInfoData(strFileName, uvDataGridView1, title);
                }
                catch (Exception ex)
                {
                    Log.Write.Error(ex.Message, ex);
                }
            }

            SaveFileDlg.Dispose();
        }

        /// <summary>
        /// 导出数据
        /// </summary>
        public static bool ExportInfoData(string strFileName, DataGridView uvDataGridView1, string title = "标题")
        {
            CSharpExcel xlsApp = new CSharpExcel();
            if (strFileName.ToLower().EndsWith(".xls"))
            {
                xlsApp.CreateExcel(ExcelFileType.XLS);
            }
            else
            {
                xlsApp.CreateExcel(ExcelFileType.XLSX);
            }

            /* 统计图表sheet */
            xlsApp.ReNameSheet(0, title);

            int c = 0;
            /* 写入列名 */
            for (int i = 0; i < uvDataGridView1.Columns.Count; i++)
            {
                if (uvDataGridView1.Columns[i].Visible)
                {
                    xlsApp.WriteData(uvDataGridView1.Columns[i].HeaderText, 0, c++);
                }
            }

            try
            {
                /* 按行写入所有数据 */
                for (int r = 0; r < uvDataGridView1.Rows.Count; r++)
                {
                    c = 0;
                    for (int i = 0; i < uvDataGridView1.Columns.Count; i++)
                    {
                        if (uvDataGridView1.Columns[i].Visible)
                        {
                            if (null != uvDataGridView1.Rows[r].Cells[uvDataGridView1.Columns[i].Name].Value)
                            {
                                xlsApp.WriteData(uvDataGridView1.Rows[r].Cells[uvDataGridView1.Columns[i].Name].Value.ToString(), r + 1, c++);
                            }
                            else
                            {
                                xlsApp.WriteData("-", r + 1, c++);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write.Error(ex.Message);
            }


            //    //加单引号限制显示为文本模式
            //    xlsApp.WriteData(uvDataGridView1.Rows[r].Cells["policeName"].Value.ToString(), r + 1, 0);
            //    xlsApp.WriteData(uvDataGridView1.Rows[r].Cells["eyeCameraTotal1"].Value.ToString(), r + 1, 1);
            //    xlsApp.WriteData(uvDataGridView1.Rows[r].Cells["cameraTrb1"].Value.ToString(), r + 1, 2);
            //    xlsApp.WriteData(uvDataGridView1.Rows[r].Cells["eCameraTotal1"].Value.ToString(), r + 1, 3);
            //    xlsApp.WriteData(uvDataGridView1.Rows[r].Cells["eTrouble"].Value.ToString(), r + 1, 4); ;
            //    xlsApp.WriteData(uvDataGridView1.Rows[r].Cells["cameraTotal2"].Value.ToString(), r + 1, 5);
            //    xlsApp.WriteData(uvDataGridView1.Rows[r].Cells["cameraTrb2"].Value.ToString(), r + 1, 6);
            //    xlsApp.WriteData(uvDataGridView1.Rows[r].Cells["yidongCameraTotal"].Value.ToString(), r + 1, 7);
            //    xlsApp.WriteData(uvDataGridView1.Rows[r].Cells["yingdongTrb"].Value.ToString(), r + 1, 8);
            //    xlsApp.WriteData(uvDataGridView1.Rows[r].Cells["liantongCameara"].Value.ToString(), r + 1, 9); ;
            //    xlsApp.WriteData(uvDataGridView1.Rows[r].Cells["liantongTrb"].Value.ToString(), r + 1, 10);
            //    xlsApp.WriteData(uvDataGridView1.Rows[r].Cells["cameraTotalNum"].Value.ToString(), r + 1, 11);
            //    xlsApp.WriteData(uvDataGridView1.Rows[r].Cells["trbTotalNum"].Value.ToString(), r + 1, 12);
            //    xlsApp.WriteData(uvDataGridView1.Rows[r].Cells["trbRate"].Value.ToString(), r + 1, 13);
            //}

            //行标题居中加粗
            xlsApp.SetCellStyle(0, 0, 0, (c - 1), 1, true, true, false, 12);

            if (false == xlsApp.SaveAs(strFileName, true))
            {
                xlsApp.Close();
                return false;
            }
            else
            {
                return true;
            }
        }


        /// <summary>
        /// 导出数据到模板(默认异步)
        /// 注意不要进行子线程调用
        /// </summary>
        /// <param name="excelTemplatePath">excel模板所在位置</param>
        /// <param name="rowTags">数据源</param>
        /// <param name="action">具体的数据赋值操作</param>
        /// <param name="startRowIndex">从第几行开始赋值</param>
        /// <param name="control">waitForm所在的窗体位置, 可以为空</param>
        /// <param name="isInMainThread">是否在主线程中执行</param>
        /// <returns></returns>
        public static bool Export2ExcelTemplate(string excelTemplatePath, List<object> rowTags, DetailExportTemplateAction action, Control control = null, int startRowIndex = 2, bool isInMainThread = false)
        {
            SaveFileDialog sfd = null;
            bool isSuccess = false;
            try
            {
                if (File.Exists(excelTemplatePath))
                {
                    sfd = new SaveFileDialog();
                    sfd.FileName = Path.GetFileNameWithoutExtension(excelTemplatePath);
                    if (excelTemplatePath.Contains("xlsx"))
                    {
                        sfd.Filter = "Excle文件|*.xlsx";
                    }
                    else
                    {
                        sfd.Filter = "Excle文件|*.xls";
                    }

                    if (DialogResult.OK == sfd.ShowDialog())
                    {
                        if (File.Exists(sfd.FileName))
                        {
                            File.Delete(sfd.FileName);
                        }

                        File.Copy(excelTemplatePath, sfd.FileName);
                        AnimateWaitForm.AnimatingWait(() =>
                        {
                            isSuccess = export2Template(sfd.FileName, rowTags, action, startRowIndex);
                        }, control, isInMainThread, true);
                    }
                }
                else
                {
                    SimpleMessageBox.ShowMessageBox("模板不存在！");
                }
            }
            catch (Exception err)
            {
                Log.Write.Error(err.Message);
            }
            finally
            {
                if (null != sfd)
                {
                    sfd.Dispose();
                }
            }
            return isSuccess;
        }

        private static bool export2Template(string filePath, List<object> rowTags, DetailExportTemplateAction action, int startIndex)
        {
            CSharpExcel excel = new CSharpExcel();
            try
            {
                excel.Open(filePath);

                if (!excel.ActivateSheet("Template"))
                {
                    return false;
                }

                //设置单元格格式,防止科学计数
                excel.SetWriteDataFormat(3, 1, rowTags.Count + 3, 46);

                if (rowTags.Count > 0)
                {
                    for (int i = 0; i < rowTags.Count; i++)
                    {
                        action(rowTags[i], excel, i + startIndex);
                        //excel.WriteData(devInfo.strAssetCode, i + start, 0);
                    }
                }

                if (false == excel.SaveAs(filePath, true))
                {
                    SimpleMessageBox.ShowMessageBox("文件保存失败, 请确认文件未被占用 !");
                    return false;
                }

                excel.Close();
                return true;
            }
            catch (Exception ex)
            {
                Log.Write.Error(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// 需要配合ExportInfoData(fileName, ..)使用
        /// </summary>
        /// <param name="fileName"></param>
        public delegate void WaitAction(string fileName);


        /// <summary>
        /// 具体导出每行赋值的过程
        /// 例如 : excel.WriteData(data, startRow, startColumn);
        /// </summary>
        /// <param name="rowTag">每行的Tag</param>
        /// <param name="excel">excel操作对象</param>
        /// <param name="rowIndex">当前第几行</param>
        public delegate void DetailExportTemplateAction(object rowTag, CSharpExcel excel, int rowIndex);
    }
}
