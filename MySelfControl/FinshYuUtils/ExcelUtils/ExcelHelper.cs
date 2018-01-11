using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using NPOI.HSSF.UserModel;
using NPOI.HPSF;
using NPOI.SS.UserModel;
using System.IO;
using NPOI.HSSF.Util;
using NPOI.XSSF.UserModel;

namespace FinshYuUtils.ExcelUtils
{
    public class CSharpExcel
    {
        private string _myFileName;
        private IWorkbook _myExcel;
        private ISheet _activeSheet;
        private object _objOpt = System.Reflection.Missing.Value;
        private bool _isRowOverMax;

        /// <summary>
        /// 构造函数，不创建Excel工作薄
        /// </summary>
        public CSharpExcel()
        {
            //请不要删除以下信息
        }

        /// <summary>
        /// 创建Excel工作薄
        /// </summary>
        public void CreateExcel(ExcelFileType fileType = ExcelFileType.XLS)
        {
            if (ExcelFileType.XLS == fileType)
            {
                _myExcel = new HSSFWorkbook();
            }
            else
            {
                _myExcel = new XSSFWorkbook();
            }
            
            _myExcel.CreateSheet();
            _myExcel.SetActiveSheet(0);
            _activeSheet = _myExcel.GetSheetAt(0);
        }

        /// <summary>
        /// 显示Excel
        /// </summary>
//         public void ShowExcel()
//         {
//             _myExcel.= true;
//         }

        /// <summary>
        /// 将数据写入Excel
        /// </summary>
        /// <param name="data">要写入的二维数组数据</param>
        /// <param name="startRow">Excel中的起始行</param>
        /// <param name="startColumn">Excel中的起始列</param>
        public void WriteData(string[,] data, int startRow, int startColumn)
        {
            int rowNumber = data.GetLength(0);
            int columnNumber = data.GetLength(1);

            for (int rowIndex = 0; rowIndex < rowNumber; rowIndex++)
            {
                IRow row = _activeSheet.GetRow(rowIndex);
                if (null == row)
                {
                    row = _activeSheet.CreateRow(rowIndex);
                }

                for (int colIndex = startColumn; colIndex < columnNumber; colIndex++)
                {
                    ICell cell = row.GetCell(colIndex);
                    if (null == cell)
                    {
                        cell = row.CreateCell(colIndex);
                    }
                    cell.SetCellValue(data[rowIndex, colIndex]);
                    //_myExcel.Cells[startRow + i, startColumn + j] = data[i, j];//"'" + 
                }
            }

        }

        /// <summary>
        /// 将数据写入Excel
        /// </summary>
        /// <param name="data">要写入的字符串</param>
        /// <param name="starRow">写入的行</param>
        /// <param name="startColumn">写入的列</param>
        public void WriteData(string data, int rowIndex, int columnIndex)
        {
            try
            {
                if (rowIndex > 65535 && _myExcel.GetType().Name == "HSSFWorkbook")
                {
                    if (!_isRowOverMax)
                    {
                        //Log.Write.Error("XLS 文件只支持65536行数据");
                        _isRowOverMax = true;
                    }
                }
                else
                {
                    _isRowOverMax = false;
                    IRow row = _activeSheet.GetRow(rowIndex);
                    if (null == row)
                    {
                        row = _activeSheet.CreateRow(rowIndex);
                    }
                    ICell cell = row.GetCell(columnIndex);
                    if (null == cell)
                    {
                        cell = row.CreateCell(columnIndex);
                    }
                    cell.SetCellValue(data);
                }
                
            }
            catch (Exception e)
            {
                //Log.Write.Error(e);
            }
        }

        /// <summary>
        /// 将数据写入Excel
        /// </summary>
        /// <param name="data">要写入的数据表</param>
        /// <param name="startRow">Excel中的起始行</param>
        /// <param name="startColumn">Excel中的起始列</param>
        public void WriteData(System.Data.DataTable data, int startRow, int startColumn)
        {
            for (int rowIndex = startRow; rowIndex < startRow + data.Rows.Count; rowIndex++)
            {
                IRow row = _activeSheet.GetRow(rowIndex);
                if (null == row)
                {
                    row = _activeSheet.CreateRow(rowIndex);
                }

                for (int colIndex = startColumn; colIndex < startColumn + data.Columns.Count; colIndex++)
                {
                    ICell cell = row.GetCell(colIndex);
                    if (null == cell)
                    {
                        cell = row.CreateCell(colIndex);
                    }
                    cell.SetCellValue(data.Rows[rowIndex][colIndex].ToString());
                }
            }
        }

        /// <summary>
        /// 获取单元格字符内容
        /// </summary>
        /// <param name="iRowIndex"></param>
        /// <param name="iColumnIndex"></param>
        /// <returns></returns>
        public string ReadstringCellValue(int iRowIndex, int iColumnIndex)
        {
            IRow row = _activeSheet.GetRow(iRowIndex);
            if (null == row)
            {
                return null;
            }

            ICell cell = row.GetCell(iColumnIndex);
            if (null == cell)
            {
                return null;
            }
            
            return cell.StringCellValue;
        }

        public void SetWriteDataFormat(int startRow, int startColumn, int endRow, int endColumn)
        {
            ICellStyle cellStyle = _myExcel.CreateCellStyle();
            cellStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("@");

            for(int rowIndex = startRow; rowIndex < endRow; rowIndex++)
            {
                IRow row = _activeSheet.GetRow(rowIndex);
                if (null == row )
                {
                    row = _activeSheet.CreateRow(rowIndex);
                }

                for(int colIndex = startColumn; colIndex < endColumn; colIndex++)
                {
                    ICell cell = row.GetCell(colIndex);
                    if (null == cell)
                    {
                        cell = row.CreateCell(colIndex);
                    }
                    cell.CellStyle = cellStyle;
                }
            }
//             Excel.Worksheet worksheet = (Excel.Worksheet)_myExcel.ActiveSheet;
//             Excel.Range range = worksheet.get_Range(_myExcel.Cells[startRow, startColumn], _myExcel.Cells[endRow, endColumn]);
//             range.NumberFormat = "@";
        }

        /// <summary>
        /// 读取指定单元格数据
        /// </summary>
        /// <param name="row">行序号</param>
        /// <param name="column">列序号</param>
        /// <returns>该格的数据</returns>
        public string ReadData(int rowIndex, int columnIndex)
        {
            if (rowIndex > 0)
            {
                rowIndex--;
            }
            if (columnIndex > 0)
            {
                columnIndex--;
            }
            IRow row = _activeSheet.GetRow(rowIndex);

            if (null != row)
            {
                ICell cell = row.GetCell(columnIndex);

                if (null != cell)
                {
                    return cell.ToString();
                }
            }

            return string.Empty;
            
        }

        /// <summary>
        /// 读取指定单元格数据
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>
        /// <returns></returns>
        public string ReadCellData(int rowIndex, int columnIndex)
        {
            if ((rowIndex <= -1) || (columnIndex <= -1))
            {
                return "";
            }

            IRow row = _activeSheet.GetRow(rowIndex);

            if (null != row)
            {
                ICell cell = row.GetCell(columnIndex);

                if (null != cell)
                {
                    return cell.ToString();
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// 向Excel中插入图片
        /// </summary>
        /// <param name="pictureName">图片的绝对路径加文件名</param>
        public void InsertPictures(string pictureName)
        {
            try
            {
                if (_myExcel is HSSFWorkbook)
                {
                    HSSFPatriarch patriarch = (HSSFPatriarch)_activeSheet.CreateDrawingPatriarch();
                    //create the anchor
                    HSSFClientAnchor anchor;
                    anchor = new HSSFClientAnchor(10, 10, 0, 0, 1, 1, 11, 26);
                    anchor.AnchorType = 2;

                    HSSFPicture picture = (HSSFPicture)patriarch.CreatePicture(anchor, LoadImage(pictureName, _myExcel));
                    picture.LineStyle = LineStyle.DashDotGel;
                }
                else if (_myExcel is XSSFWorkbook)
                {
                    XSSFDrawing drawing = (XSSFDrawing)_activeSheet.CreateDrawingPatriarch();
                    //create the anchor
                    XSSFClientAnchor anchor;
                    anchor = new XSSFClientAnchor(10, 10, 0, 0, 1, 1, 11, 26);
                    anchor.AnchorType = 2;

                    XSSFPicture picture = (XSSFPicture)drawing.CreatePicture(anchor, LoadImage(pictureName, _myExcel));
                    picture.LineStyle = LineStyle.DashDotGel;
                }

            }
            catch (Exception exception)
            {
                //Log.Write.Error("InsertPictures to file fail", exception);
            }
        }

        public static int LoadImage(string path, IWorkbook wb)
        {
            FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[file.Length];
            file.Read(buffer, 0, (int)file.Length);
            file.Close();
            return wb.AddPicture(buffer, PictureType.JPEG);
        }

        /// <summary>
        /// 向Excel中插入图片
        /// </summary>
        /// <param name="pictureName">图片的绝对路径加文件名</param>
        /// <param name="left">左边距</param>
        /// <param name="top">右边距</param>
        /// <param name="width">宽</param>
        /// <param name="heigth">高</param>
        public void InsertPictures(string pictureName, int row, int column, int rowEnd, int columnEnd)
        {
            try
            {
                if (_myExcel is HSSFWorkbook)
                {
                    HSSFPatriarch patriarch = (HSSFPatriarch)_activeSheet.CreateDrawingPatriarch();
                    //create the anchor
                    HSSFClientAnchor anchor;

                    anchor = new HSSFClientAnchor(10, 10, 0, 0, column, row, columnEnd, rowEnd);
                    anchor.AnchorType = 2;

                    HSSFPicture picture = (HSSFPicture)patriarch.CreatePicture(anchor, LoadImage(pictureName, _myExcel));
                    picture.LineStyle = LineStyle.DashDotGel;
                }
                else if (_myExcel is XSSFWorkbook)
                {
                    XSSFDrawing drawing = (XSSFDrawing)_activeSheet.CreateDrawingPatriarch();
                    //create the anchor
                    XSSFClientAnchor anchor;
                    anchor = new XSSFClientAnchor(10, 10, 0, 0, column, row, columnEnd, rowEnd);
                    anchor.AnchorType = 2;

                    XSSFPicture picture = (XSSFPicture)drawing.CreatePicture(anchor, LoadImage(pictureName, _myExcel));
                    picture.LineStyle = LineStyle.DashDotGel;
                }

            }
            catch (Exception exception)
            {
                //Log.Write.Error("InsertPictures to file fail", exception);
            }
        }

        /// <summary>
        /// 明细打印居中, 加页脚
        /// </summary>
        public void PageSetup()
        {
            //明细打印居中
            _activeSheet.HorizontallyCenter = true;

            //加页脚
            _activeSheet.Footer.Left = "添加页脚Test";
            //_activeSheet.Footer.Center = "共&N页 第&P页";
        }

        /// <summary>
        /// 重命名工作表
        /// </summary>
        /// <param name="sheetNum">工作表序号，从左到右，从0开始</param>
        /// <param name="newSheetName">新的工作表名</param>
        public void ReNameSheet(int sheetNum, string newSheetName)
        {
            _myExcel.SetSheetName(sheetNum, newSheetName);
        }

        /// <summary>
        /// 重命名工作表
        /// </summary>
        /// <param name="oldSheetName">原有工作表名</param>
        /// <param name="newSheetName">新的工作表名</param>
        public void ReNameSheet(string oldSheetName, string newSheetName)
        {
            _myExcel.SetSheetName(_myExcel.GetSheetIndex(oldSheetName), newSheetName);
        }

        /// <summary>
        /// 新建工作表
        /// </summary>
        /// <param name="sheetName">工作表名</param>
        public void CreateWorkSheet(string sheetName)
        {
            _myExcel.CreateSheet(sheetName);
        }

        /// <summary>
        /// 激活工作表
        /// </summary>
        /// <param name="sheetName">工作表名</param>
        public bool ActivateSheet(string sheetName)
        {
            try
            {
                ISheet sheet = _myExcel.GetSheet(sheetName);
                if (null == sheet)
                {
                    return false;
                }

                _myExcel.SetActiveSheet(_myExcel.GetSheetIndex(sheet));
                _activeSheet = sheet;
            }
            catch (Exception ex)
            {
                //Log.Write.Error("激活工作表" + sheetName + "失败");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 激活工作表
        /// </summary>
        /// <param name="sheetNum">工作表序号</param>
        public bool ActivateSheet(int sheetNum)
        {
            try
            {
                _myExcel.SetActiveSheet(sheetNum);
                ISheet sheet = _myExcel.GetSheetAt(sheetNum);
                if (null != sheet)
                {
                    _activeSheet = sheet;
                }
            }
            catch (Exception ex)
            {
                //Log.Write.Error("激活第个" + sheetNum.ToString() + "工作表失败");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 删除一个工作表
        /// </summary>
        /// <param name="SheetName">删除的工作表名</param>
        public void DeleteSheet(int sheetNum)
        {
            _myExcel.RemoveSheetAt(sheetNum);
        }

        /// <summary>
        /// 删除一个工作表
        /// </summary>
        /// <param name="SheetName">删除的工作表序号</param>
        public void DeleteSheet(string sheetName)
        {
            _myExcel.RemoveSheetAt(_myExcel.GetSheetIndex(sheetName));
        }

        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="startRow">起始行</param>
        /// <param name="startColumn">起始列</param>
        /// <param name="endRow">结束行</param>
        /// <param name="endColumn">结束列</param>
        public void CellsUnite(int startRow, int startColumn, int endRow, int endColumn)
        {
            _activeSheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(startRow, endRow, startColumn, endColumn));
       }

        /// <summary>
        /// 单元格垂直对齐方式
        /// </summary>
        /// <param name="startRow">起始行</param>
        /// <param name="startColumn">起始列</param>
        /// <param name="endRow">结束行</param>
        /// <param name="endColumn">结束列</param>
        /// <param name="hAlign">水平对齐</param>
        /// <param name="vAlign">垂直对齐</param>
        public void CellsVerticalAlignment(int startRow, int startColumn, int endRow, int endColumn, VerticalAlignment vAlign)
        {
            ICellStyle style = _myExcel.CreateCellStyle();
            style.VerticalAlignment = vAlign;

            for (int rowIndex = startRow; rowIndex <= endRow; rowIndex++)
            {
                IRow row = _activeSheet.GetRow(rowIndex);
                if (null == row)
                {
                    row = _activeSheet.CreateRow(rowIndex);
                }

                for (int colIndex = startColumn; colIndex <= endColumn; colIndex++)
                {
                    ICell cell = row.GetCell(colIndex);
                    if (null == cell)
                    {
                        cell = row.CreateCell(colIndex);
                    }
                    cell.CellStyle = style;
                }
            }
        }

        /// <summary>
        /// 单元格文字对齐方式
        /// </summary>
        /// <param name="startRow">起始行</param>
        /// <param name="startColumn">起始列</param>
        /// <param name="endRow">结束行</param>
        /// <param name="endColumn">结束列</param>
        /// <param name="hAlign">水平对齐</param>
        /// <param name="vAlign">垂直对齐</param>
        public void CellsAlignment(int startRow, int startColumn, int endRow, int endColumn, HorizontalAlignment hAlign, VerticalAlignment vAlign)
        {
            ICellStyle style = _myExcel.CreateCellStyle();
            style.Alignment = hAlign;
            style.VerticalAlignment = vAlign;

            for (int rowIndex = startRow; rowIndex <= endRow; rowIndex++)
            {
                IRow row = _activeSheet.GetRow(rowIndex);
                if (null == row)
                {
                    row = _activeSheet.CreateRow(rowIndex);
                }

                for (int colIndex = startColumn; colIndex <= endColumn; colIndex++)
                {
                    ICell cell = row.GetCell(colIndex);
                    if (null == cell)
                    {
                        cell = row.CreateCell(colIndex);
                    }
                    cell.CellStyle = style;
                }
            }
// 
//             Excel.Range range = _myExcel.get_Range(_myExcel.Cells[startRow, startColumn], _myExcel.Cells[endRow, endColumn]);
//             range.HorizontalAlignment = hAlign;
//             range.VerticalAlignment = vAlign;
        }

        /// <summary>
        /// 单元格背景色及填充方式
        /// </summary>
        /// <param name="startRow">起始行</param>
        /// <param name="startColumn">起始列</param>
        /// <param name="endRow">结束行</param>
        /// <param name="endColumn">结束列</param>
        /// <param name="color">颜色索引</param>
        public void CellsBackColor(int startRow, int startColumn, int endRow, int endColumn, ColorIndex foreColor, ColorIndex backColor, FillPattern pattern)
        {
            ICellStyle style = _myExcel.CreateCellStyle();
            style.FillForegroundColor = (short)foreColor;
            style.FillPattern = pattern;
            style.FillBackgroundColor = (short)backColor;

            for (int rowIndex = startRow; rowIndex <= endRow; rowIndex++)
            {
                IRow row = _activeSheet.GetRow(rowIndex);
                if (null == row)
                {
                    row = _activeSheet.CreateRow(rowIndex);
                }

                for (int colIndex = startColumn; colIndex <= endColumn; colIndex++)
                {
                    ICell cell = row.GetCell(colIndex);
                    if (null == cell)
                    {
                        cell = row.CreateCell(colIndex);
                    }
                    cell.CellStyle = style;
                }
            }
        }

        /// <summary>
        /// 设置行高
        /// </summary>
        /// <param name="startRow">起始行</param>
        /// <param name="endRow">结束行</param>
        /// <param name="height">行高</param>
        public void SetRowHeight(int startRow, int endRow, int height)
        {
            //获取当前正在使用的工作表
            for (int rowIndex = startRow; rowIndex <= endRow; rowIndex++)
            {
                IRow row = _activeSheet.GetRow(rowIndex);
                if (null == row)
                {
                    row = _activeSheet.CreateRow(rowIndex);
                }

                row.Height = (short)(height * 20);
            }
        }
        
        /// <summary>
        /// 设置列宽
        /// </summary>
        /// <param name="startColumn">起始列</param>
        /// <param name="endColumn">结束列</param>
        /// <param name="width"></param>
        public void SetColumnWidth(int startColumn, int endColumn, int width)
        {
            for (int colIndex = startColumn; colIndex <= endColumn; colIndex++)
            {
                _activeSheet.SetColumnWidth(colIndex, width * 256);                
            }
        }

        /// <summary>
        /// 自动调整列宽
        /// </summary>
        /// <param name="columnNum">列号</param>
        public void ColumnAutoFit(int columnNum)
        {
            _activeSheet.AutoSizeColumn(columnNum);
        }

        /// <summary>
        /// 字体颜色
        /// </summary>
        /// <param name="startRow">起始行</param>
        /// <param name="startColumn">起始列</param>
        /// <param name="endRow">结束行</param>
        /// <param name="endColumn">结束列</param>
        /// <param name="color">颜色索引</param>
        public void FontColor(int startRow, int startColumn, int endRow, int endColumn, ColorIndex color)
        {
            ICellStyle style = _myExcel.CreateCellStyle();
            //新建一个字体样式对象
            IFont font = _myExcel.CreateFont();
            font.Color = (short)color;
            style.SetFont(font);

            for (int rowIndex = startRow; rowIndex <= endRow; rowIndex++)
            {
                IRow row = _activeSheet.GetRow(rowIndex);
                if (null == row)
                {
                    row = _activeSheet.CreateRow(rowIndex);
                }

                for (int colIndex = startColumn; colIndex <= endColumn; colIndex++)
                {
                    ICell cell = row.GetCell(colIndex);
                    if (null == cell)
                    {
                        cell = row.CreateCell(colIndex);
                    }
                    cell.CellStyle = style;
                }
            }
        }

        /// <summary>
        /// 设置某单元格背景色
        /// </summary>
        /// <param name="iRow"></param>
        /// <param name="iColumn"></param>
        /// <param name="backColor"></param>
        public void SetCellsBackColor(int iRow, int iColumn, ColorIndex backColor)
        {
            ICellStyle style = _myExcel.CreateCellStyle();
            style.FillForegroundColor = (short)backColor;
            style.FillPattern = FillPattern.SolidForeground;

            IRow row = _activeSheet.GetRow(iRow);
            if (null == row)
            {
                row = _activeSheet.CreateRow(iRow);
            }

            ICell cell = row.GetCell(iColumn);
            if (null == cell)
            {
                cell = row.CreateCell(iColumn);
            }
            cell.CellStyle = style;
        }

        /// <summary>
        /// 设置某单元格为超链接格式
        /// </summary>
        /// <param name="iRow"></param>
        /// <param name="iColumn"></param>
        /// <param name="backColor"></param>
        public void SetCellsHyperlink(int iRow, int iColumn, HyperlinkType hyperlinkType)
        {
            IRow row = _activeSheet.GetRow(iRow);
            if (null == row)
            {
                row = _activeSheet.CreateRow(iRow);
            }

            ICell cell = row.GetCell(iColumn);
            if (null == cell)
            {
                cell = row.CreateCell(iColumn);
            }

            //设置超链接
            HSSFHyperlink link = new HSSFHyperlink(hyperlinkType);
            link.Address = ReadstringCellValue(iRow, iColumn);
            cell.Hyperlink = link;
        }

        /// <summary>
        /// 设置单元格字体样式(居中、是否边框、是否加粗、是否斜体、字体大小、字体类型名称、字体颜色、下划线模式) alignmentType意义：0居左，1居中
        /// </summary>
        /// <param name="startRow"></param>
        /// <param name="startColumn"></param>
        /// <param name="endRow"></param>
        /// <param name="endColumn"></param>
        /// <param name="alignmentType"></param>
        /// <param name="isFrame"></param>
        /// <param name="isBold"></param>
        /// <param name="isItalic"></param>
        /// <param name="fontSize"></param>
        /// <param name="color"></param>
        /// <param name="underline"></param>
        public void SetCellStyle(int startRow, int startColumn, int endRow, int endColumn, int alignmentType, bool isFrame,
                                 bool isBold, bool isItalic, short fontSize, string strFontName = "宋体", 
                                 ColorIndex color = ColorIndex.Black, ColorIndex backColor = ColorIndex.White, FontUnderlineType underline = FontUnderlineType.None)
        {
            ICellStyle style = _myExcel.CreateCellStyle();
            
            if (0 == alignmentType)       //居左
            {
                style.Alignment = HorizontalAlignment.Left;
                style.VerticalAlignment = VerticalAlignment.Center;
            }
            else if (1 == alignmentType)  //居中
            {
                style.Alignment = HorizontalAlignment.Center;
                style.VerticalAlignment = VerticalAlignment.Center;
            }

            //边框
            if (isFrame)
            {
                style.BorderBottom = BorderStyle.Thin;
                style.BorderLeft = BorderStyle.Thin;
                style.BorderRight = BorderStyle.Thin;
                style.BorderTop = BorderStyle.Thin;
                style.BottomBorderColor = HSSFColor.Black.Index;
                style.LeftBorderColor = HSSFColor.Black.Index;
                style.RightBorderColor = HSSFColor.Black.Index;
                style.TopBorderColor = HSSFColor.Black.Index;
            }

            //背景色(默认白色，不起作用)
            if (ColorIndex.White != backColor)
            {
                style.FillForegroundColor = (short)backColor;
                style.FillPattern = FillPattern.SolidForeground;
            }
            
            //新建一个字体样式对象
            IFont font = _myExcel.CreateFont();
            //加粗
            if (isBold)
            {
                font.Boldweight = Convert.ToInt16(NPOI.SS.UserModel.FontBoldWeight.Bold);
            }
            //是否斜体
            font.IsItalic = isItalic;
            //字号大小
            font.FontHeightInPoints = fontSize;
            //字体颜色
            font.Color = (short)color;
            //下划线
            font.Underline = underline;
            //字体类型
            font.FontName = strFontName;
            style.SetFont(font);

            for (int rowIndex = startRow; rowIndex <= endRow; rowIndex++)
            {
                IRow row = _activeSheet.GetRow(rowIndex);
                if (null == row)
                {
                    row = _activeSheet.CreateRow(rowIndex);
                }

                for (int colIndex = startColumn; colIndex <= endColumn; colIndex++)
                {
                    ICell cell = row.GetCell(colIndex);
                    if (null == cell)
                    {
                        cell = row.CreateCell(colIndex);
                    }
                    cell.CellStyle = style;
                }
            }
        }

        /// <summary>
        /// 字体样式(加粗)
        /// </summary>
        /// <param name="startRow">起始行</param>
        /// <param name="startColumn">起始列</param>
        /// <param name="endRow">结束行</param>
        /// <param name="endColumn">结束列</param>
        public void FontBold(int startRow, int startColumn, int endRow, int endColumn)
        {
            ICellStyle style = _myExcel.CreateCellStyle();
            //新建一个字体样式对象
            IFont font = _myExcel.CreateFont();
            font.Boldweight = Convert.ToInt16(NPOI.SS.UserModel.FontBoldWeight.Bold);
            style.SetFont(font);            

            for (int rowIndex = startRow; rowIndex <= endRow; rowIndex++)
            {
                IRow row = _activeSheet.GetRow(rowIndex);
                if (null == row)
                {
                    row = _activeSheet.CreateRow(rowIndex);
                }

                for (int colIndex = startColumn; colIndex <= endColumn; colIndex++)
                {
                    ICell cell = row.GetCell(colIndex);
                    if (null == cell)
                    {
                        cell = row.CreateCell(colIndex);
                    }
                    cell.CellStyle = style;
                }
            }
        }

        /// <summary>
        /// 字体样式(斜体)
        /// </summary>
        /// <param name="startRow">起始行</param>
        /// <param name="startColumn">起始列</param>
        /// <param name="endRow">结束行</param>
        /// <param name="endColumn">结束列</param>
        public void FontItalic(int startRow, int startColumn, int endRow, int endColumn)
        {
            ICellStyle style = _myExcel.CreateCellStyle();
            //新建一个字体样式对象
            IFont font = _myExcel.CreateFont();
            font.IsItalic = true;
            style.SetFont(font);

            for (int rowIndex = startRow; rowIndex <= endRow; rowIndex++)
            {
                IRow row = _activeSheet.GetRow(rowIndex);
                if (null == row)
                {
                    row = _activeSheet.CreateRow(rowIndex);
                }

                for (int colIndex = startColumn; colIndex <= endColumn; colIndex++)
                {
                    ICell cell = row.GetCell(colIndex);
                    if (null == cell)
                    {
                        cell = row.CreateCell(colIndex);
                    }
                    cell.CellStyle = style;
                }
            }
        }

        /// <summary>
        /// 字体样式(下划线)
        /// </summary>
        /// <param name="startRow">起始行</param>
        /// <param name="startColumn">起始列</param>
        /// <param name="endRow">结束行</param>
        /// <param name="endColumn">结束列</param>
        /// <param name="underline">下划线类型</param>
        public void FontUnderline(int startRow, int startColumn, int endRow, int endColumn, FontUnderlineType underline)
        {
            ICellStyle style = _myExcel.CreateCellStyle();
            //新建一个字体样式对象
            IFont font = _myExcel.CreateFont();
            font.Underline = underline;
            style.SetFont(font);

            for (int rowIndex = startRow; rowIndex <= endRow; rowIndex++)
            {
                IRow row = _activeSheet.GetRow(rowIndex);
                if (null == row)
                {
                    row = _activeSheet.CreateRow(rowIndex);
                }

                for (int colIndex = startColumn; colIndex <= endColumn; colIndex++)
                {
                    ICell cell = row.GetCell(colIndex);
                    if (null == cell)
                    {
                        cell = row.CreateCell(colIndex);
                    }
                    cell.CellStyle = style;
                }
            }
        }

        /// <summary>
        /// 字体样式(字号)
        /// </summary>
        /// <param name="startRow">起始行</param>
        /// <param name="startColumn">起始列</param>
        /// <param name="endRow">结束行</param>
        /// <param name="endColumn">结束列</param>
        /// <param name="fontSize">字号</param>
        public void FontSize(int startRow, int startColumn, int endRow, int endColumn, short fontSize)
        {
            ICellStyle style = _myExcel.CreateCellStyle();
            //新建一个字体样式对象
            IFont font = _myExcel.CreateFont();
            font.FontHeightInPoints = fontSize;
            style.SetFont(font);

            for (int rowIndex = startRow; rowIndex <= endRow; rowIndex++)
            {
                IRow row = _activeSheet.GetRow(rowIndex);
                if (null == row)
                {
                    row = _activeSheet.CreateRow(rowIndex);
                }

                for (int colIndex = startColumn; colIndex <= endColumn; colIndex++)
                {
                    ICell cell = row.GetCell(colIndex);
                    if (null == cell)
                    {
                        cell = row.CreateCell(colIndex);
                    }
                    cell.CellStyle = style;
                }
            }
        }

        /// <summary>
        /// 字体样式(加粗,斜体,下划线)
        /// </summary>
        /// <param name="startRow">起始行</param>
        /// <param name="startColumn">起始列</param>
        /// <param name="endRow">结束行</param>
        /// <param name="endColumn">结束列</param>
        /// <param name="isBold">是否加粗</param>
        /// <param name="isItalic">是否斜体</param>
        /// <param name="underline">下划线类型</param>
        public void FontStyle(int startRow, int startColumn, int endRow, int endColumn, short boldWeight, bool isItalic, FontUnderlineType underline)
        {
            ICellStyle style = _myExcel.CreateCellStyle();
            IFont font = _myExcel.CreateFont();
            font.Boldweight = boldWeight;
            font.IsItalic = isItalic;
            font.Underline = underline;
            style.SetFont(font);

            for (int rowIndex = startRow; rowIndex <= endRow; rowIndex++)
            {
                IRow row = _activeSheet.GetRow(rowIndex);
                if (null == row)
                {
                    row = _activeSheet.CreateRow(rowIndex);
                }

                for (int colIndex = startColumn; colIndex <= endColumn; colIndex++)
                {
                    ICell cell = row.GetCell(colIndex);
                    if (null == cell)
                    {
                        cell = row.CreateCell(colIndex);
                    }
                    cell.CellStyle = style;
                }
            }
        }

        /// <summary>
        /// 单元格字体大小
        /// </summary>
        /// <param name="startRow">起始行</param>
        /// <param name="startColumn">起始列</param>
        /// <param name="endRow">结束行</param>
        /// <param name="endColumn">结束列</param>
        public void SetFontSize(int startRow, int startColumn, int endRow, int endColumn, short fontSize)
        {
            ICellStyle style = _myExcel.CreateCellStyle();
            //新建一个字体样式对象
            IFont font = _myExcel.CreateFont();
            font.FontHeightInPoints = fontSize;
            style.SetFont(font);

            for (int rowIndex = startRow; rowIndex <= endRow; rowIndex++)
            {
                IRow row = _activeSheet.GetRow(rowIndex);
                if (null == row)
                {
                    row = _activeSheet.CreateRow(rowIndex);
                }

                for (int colIndex = startColumn; colIndex <= endColumn; colIndex++)
                {
                    ICell cell = row.GetCell(colIndex);
                    if (null == cell)
                    {
                        cell = row.CreateCell(colIndex);
                    }
                    cell.CellStyle = style;
                }
            }
        }

        /// <summary>
        /// 设置边框
        /// </summary>
        /// <param name="startRow"></param>
        /// <param name="startColumn"></param>
        /// <param name="endRow"></param>
        /// <param name="endColumn"></param>
        public void SetFrame(int startRow, int startColumn, int endRow, int endColumn)
        {
            ICellStyle style = _myExcel.CreateCellStyle();
            style.BorderBottom = BorderStyle.Thin;
            style.BorderLeft = BorderStyle.Thin;
            style.BorderRight = BorderStyle.Thin;
            style.BorderTop = BorderStyle.Thin;
            style.BottomBorderColor = HSSFColor.Black.Index;
            style.LeftBorderColor = HSSFColor.Black.Index;
            style.RightBorderColor = HSSFColor.Black.Index;
            style.TopBorderColor = HSSFColor.Black.Index;

            for (int rowIndex = startRow; rowIndex <= endRow; rowIndex++)
            {
                IRow row = _activeSheet.GetRow(rowIndex);
                if (null == row)
                {
                    row = _activeSheet.CreateRow(rowIndex);
                }

                for (int colIndex = startColumn; colIndex <= endColumn; colIndex++)
                {
                    ICell cell = row.GetCell(colIndex);
                    if (null == cell)
                    {
                        cell = row.CreateCell(colIndex);
                    }
                    cell.CellStyle = style;
                }
            }
        }

        /// <summary>
        /// 水平居中，垂直居中
        /// </summary>
        /// <param name="startRow"></param>
        /// <param name="startColumn"></param>
        /// <param name="endRow"></param>
        /// <param name="endColumn"></param>
        public void SetBothAlignment(int startRow, int startColumn, int endRow, int endColumn)
        {
            ICellStyle style = _myExcel.CreateCellStyle();
            style.Alignment = HorizontalAlignment.Center;
            style.VerticalAlignment = VerticalAlignment.Center;

            for (int rowIndex = startRow; rowIndex <= endRow; rowIndex++)
            {
                IRow row = _activeSheet.GetRow(rowIndex);
                if (null == row)
                {
                    row = _activeSheet.CreateRow(rowIndex);
                }

                for (int colIndex = startColumn; colIndex <= endColumn; colIndex++)
                {
                    ICell cell = row.GetCell(colIndex);
                    if (null == cell)
                    {
                        cell = row.CreateCell(colIndex);
                    }
                    cell.CellStyle = style;
                }
            }
        }

        /// <summary>
        /// 水平居中，垂直居左
        /// </summary>
        /// <param name="startRow"></param>
        /// <param name="startColumn"></param>
        /// <param name="endRow"></param>
        /// <param name="endColumn"></param>
        public void SetAlignmentLeft(int startRow, int startColumn, int endRow, int endColumn)
        {
            ICellStyle style = _myExcel.CreateCellStyle();
            style.Alignment = HorizontalAlignment.Left;
            style.VerticalAlignment = VerticalAlignment.Center;

            for (int rowIndex = startRow; rowIndex <= endRow; rowIndex++)
            {
                IRow row = _activeSheet.GetRow(rowIndex);
                if (null == row)
                {
                    row = _activeSheet.CreateRow(rowIndex);
                }

                for (int colIndex = startColumn; colIndex <= endColumn; colIndex++)
                {
                    ICell cell = row.GetCell(colIndex);
                    if (null == cell)
                    {
                        cell = row.CreateCell(colIndex);
                    }
                    cell.CellStyle = style;
                }
            }
        }

        /// <summary>
        /// 单元格字体及大小
        /// </summary>
        /// <param name="startRow">起始行</param>
        /// <param name="startColumn">起始列</param>
        /// <param name="endRow">结束行</param>
        /// <param name="endColumn">结束列</param>
        /// <param name="fontName">字体名称</param>
        /// <param name="fontSize">字体大小</param>
        public void FontNameSize(int startRow, int startColumn, int endRow, int endColumn, string fontName, short fontSize)
        {
            ICellStyle style = _myExcel.CreateCellStyle();
            IFont font = _myExcel.CreateFont();
            font.FontName = fontName;
            font.FontHeightInPoints = fontSize;
            style.SetFont(font);

            for (int rowIndex = startRow; rowIndex <= endRow; rowIndex++)
            {
                IRow row = _activeSheet.GetRow(rowIndex);
                if (null == row)
                {
                    row = _activeSheet.CreateRow(rowIndex);
                }

                for (int colIndex = startColumn; colIndex <= endColumn; colIndex++)
                {
                    ICell cell = row.GetCell(colIndex);
                    if (null == cell)
                    {
                        cell = row.CreateCell(colIndex);
                    }
                    cell.CellStyle = style;
                }
            }
        }

        /// <summary>
        /// 打开一个存在的Excel文件
        /// </summary>
        /// <param name="fileName">Excel完整路径加文件名</param>
        public bool Open(string fileName)
        {
            _myExcel = WorkbookFactory.Create(fileName);
            if (null == _myExcel)
            {
                return false;
            }

            _activeSheet = _myExcel.GetSheetAt(0);
            if (null == _activeSheet)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 检测注册表，看是否安装了OFFICE
        /// </summary>
        /// <returns></returns>
        public bool ExistsRegedit()
        {
            bool ifused = false;
            RegistryKey rk = Registry.LocalMachine;
            RegistryKey akey = rk.OpenSubKey(@"SOFTWARE\Microsoft\Office\11.0\Excel\InstallRoot\");//查询2003

            RegistryKey akey07 = rk.OpenSubKey(@"SOFTWARE\Microsoft\Office\12.0\Excel\InstallRoot\");//查询2007

            RegistryKey akey10 = rk.OpenSubKey(@"SOFTWARE\Microsoft\Office\14.0\Excel\InstallRoot\");//查询2010

            //检查本机是否安装Office2003
            if (akey != null)
            {
                string file03 = akey.GetValue("Path").ToString();
                if (System.IO.File.Exists(file03 + "Excel.exe"))
                {
                    ifused = true;
                }
            }

            //检查本机是否安装Office2007
            if (akey07 != null)
            {
                string file07 = akey07.GetValue("Path").ToString();
                if (System.IO.File.Exists(file07 + "Excel.exe"))
                {
                    ifused = true;
                }
            }

            //检查本机是否安装Office2010
            if (akey10 != null)
            {
                string file10 = akey10.GetValue("Path").ToString();
                if (System.IO.File.Exists(file10 + "Excel.exe"))
                {
                    ifused = true;
                }
            }
            return ifused;
        }


        /// <summary>
        /// 保存Excel
        /// </summary>
        /// <returns>保存成功返回True</returns>
        public bool Save()
        {
            if (_myFileName == "")
            {
                return false;
            }
            else
            {
                try
                {
                    WriteToFile(_myFileName);
                    return true;
                }
                catch (Exception err)
                {
                    //Log.Write.Error(err.Message);
                    return false;
                }
            }
        }

        private void WriteToFile(string fileName)
        {   
            FileStream file = new FileStream(fileName, FileMode.Create);
            _myExcel.Write(file);
            file.Close();
        }

        /// <summary>
        /// Excel文档另存为
        /// </summary>
        /// <param name="fileName">保存完整路径加文件名</param>
        /// <returns>保存成功返回True</returns>
        public bool SaveAs(string fileName)
        {
            try
            {
                if (System.IO.File.Exists(fileName))
                {
                    return false;
                }
                WriteToFile(_myFileName);
                return true;

            }
            catch (Exception err)
            {
                //Log.Write.Error(err.Message);
                return false;
            }
        }

        /// <summary>
        /// Excel文档另存为
        /// </summary>
        /// <param name="fileName">保存完整路径加文件名</param>
        /// <returns>保存成功返回True</returns>
        public bool SaveAs(string fileName, bool isCover)
        {
            try
            {
                if (System.IO.File.Exists(fileName))
                {
                    if (isCover)
                    {
                        System.IO.File.Delete(fileName);
                    }
                }
                WriteToFile(fileName);
                return true;

            }
            catch (Exception err)
            {
                //Log.Write.Error(err.ToString());
                return false;
            }
        }

        /// <summary>
        /// Excel文档另存为
        /// </summary>
        /// <param name="fileName">保存完整路径加文件名</param>
        /// <param name="isCover">是否覆盖，true为是</param>
        /// <param name="ex">返回出现的异常信息</param>
        /// <returns></returns>
        public bool SaveAs(string fileName, bool isCover, ref Exception ex)
        {
            try
            {
                if (System.IO.File.Exists(fileName))
                {
                    if (isCover)
                    {
                        System.IO.File.Delete(fileName);
                    }
                }
                WriteToFile(fileName);
                return true;

            }
            catch (Exception err)
            {
                ex = err;
                //Log.Write.Error(err.ToString());
                return false;
            }
        }

        /// <summary>
        /// 关闭Excel
        /// </summary>
        public void Close()
        {
            _myExcel = null;
            GC.Collect();
        }

        /// <summary>
        /// 关闭Excel
        /// </summary>
        /// <param name="isSave">是否保存</param>
        public void Close(bool isSave)
        {
            if (isSave)
            {
                WriteToFile(_myFileName);
            }
            Close();
        }

        /// <summary>
        /// 关闭Excel
        /// </summary>
        /// <param name="isSave">是否保存</param>
        /// <param name="fileName">存储文件名</param>
        public void Close(bool isSave, string fileName)
        {
            if (isSave)
            {
                WriteToFile(fileName);
            }
            Close();
        }

        #region 私有成员
        private string GetColumnName(int number)
        {
            int h, l;
            h = number / 26;
            l = number % 26;
            if (l == 0)
            {
                h -= 1;
                l = 26;
            }
            string s = GetLetter(h) + GetLetter(l);
            return s;
        }

        private string GetLetter(int number)
        {
            switch (number)
            {
                case 1:
                    return "A";
                case 2:
                    return "B";
                case 3:
                    return "C";
                case 4:
                    return "D";
                case 5:
                    return "E";
                case 6:
                    return "F";
                case 7:
                    return "G";
                case 8:
                    return "H";
                case 9:
                    return "I";
                case 10:
                    return "J";
                case 11:
                    return "K";
                case 12:
                    return "L";
                case 13:
                    return "M";
                case 14:
                    return "N";
                case 15:
                    return "O";
                case 16:
                    return "P";
                case 17:
                    return "Q";
                case 18:
                    return "R";
                case 19:
                    return "S";
                case 20:
                    return "T";
                case 21:
                    return "U";
                case 22:
                    return "V";
                case 23:
                    return "W";
                case 24:
                    return "X";
                case 25:
                    return "Y";
                case 26:
                    return "Z";
                default:
                    return "";
            }
        }
        #endregion
    }

    /// <summary>
    /// 常用颜色定义,对就Excel中颜色名
    /// </summary>
    public enum ColorIndex
    {
        Aqua = 49,
        Automatic= 64,
        Black = 8,
        Blue = 12,
        BlueGrey= 54,
        BrightGreen= 11,
        Brown= 60,
        Coral= 29,
        CornflowerBlue= 24,
        DarkBlue= 18,
        DarkGreen= 58,
        DarkRed= 16,
        DarkTeal= 56,
        DarkYellow= 19,
        Gold= 51,
        Green= 17,
        Grey25Percent= 22,
        Grey40Percent= 55,
        Grey50Percent= 23,
        Grey80Percent= 63,
        Indigo= 62,
        Lavender= 46,
        LemonChiffon= 26,
        LightBlue= 48,
        LightCornflowerBlue= 31,
        LightGreen= 42,
        LightOrange= 52,
        LightTurquoise= 41,
        LightYellow= 43,
        Lime= 50,
        Maroon= 25,
        OliveGreen= 59,
        Orange= 53,
        Orchid= 28,
        PaleBlue= 44,
        Pink= 14,
        Plum= 61,
        Red= 10,
        Rose= 45,
        RoyalBlue= 30,
        SeaGreen= 57,
        SkyBlue= 40,
        Tan= 47,
        Teal= 21,
        Turquoise= 15,
        Violet= 20,
        White= 9,
        Yellow=13
    }

    public enum ExcelFileType
    {
        XLS,
        XLSX
    }
}
