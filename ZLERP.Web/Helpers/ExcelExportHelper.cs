using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace ZLERP.Web.Helpers
{
    public class ExcelExportHelper
    {
        /// <summary>
        /// 将单个数据表保存到Excel
        /// </summary>
        /// <param name="dataTable">数据表</param>
        /// <param name="fileName">文件名</param>
        public static void ExportExcel(DataTable dataTable, string fileName = null)
        {
            HSSFWorkbook workBook = new HSSFWorkbook();
            Sheet sheet = workBook.CreateSheet("Sheet1");

            Font font = workBook.CreateFont();
            font.Boldweight = 700;
            CellStyle style = workBook.CreateCellStyle();
            style.Alignment = HorizontalAlignment.CENTER;
            style.SetFont(font);

            int rownum = 0;
            Row row = sheet.CreateRow(rownum++);
            Cell cell;

            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                cell = row.CreateCell(i);
                cell.SetCellValue(dataTable.Columns[i].ColumnName);
                cell.CellStyle = style;
            }

            CellStyle dateStyle = workBook.CreateCellStyle();
            DataFormat format = workBook.CreateDataFormat();
            dateStyle.DataFormat = format.GetFormat("yyyy-MM-dd HH:mm:ss");

            foreach (DataRow dataRow in dataTable.Rows)
            {
                row = sheet.CreateRow(rownum++);
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    cell = row.CreateCell(i);
                    string strValue = dataRow[i].ToString();
                    if (string.IsNullOrWhiteSpace(strValue))
                        cell.SetCellValue("");
                    else
                    {
                        switch (dataTable.Columns[i].DataType.ToString())
                        {
                            case "System.DateTime":
                                DateTime dateTime;
                                DateTime.TryParse(strValue, out dateTime);
                                cell.SetCellValue(dateTime);
                                cell.CellStyle = dateStyle;
                                break;
                            case "System.Boolean":
                                bool bValue;
                                bool.TryParse(strValue, out bValue);
                                cell.SetCellValue(bValue);
                                break;
                            case "System.Int16":
                            case "System.Int32":
                            case "System.Int64":
                            case "System.Byte":
                                int iValue = 0;
                                int.TryParse(strValue, out iValue);
                                cell.SetCellValue(iValue);
                                break;
                            case "System.Decimal":
                            case "System.Double":
                                double dValue = 0;
                                double.TryParse(strValue, out dValue);
                                cell.SetCellValue(dValue);
                                break;
                            default:
                                cell.SetCellValue(strValue);
                                break;
                        }
                    }
                }
            }
            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                sheet.AutoSizeColumn(i);
            }

            workBook.Write(HttpContext.Current.Response.OutputStream);
            workBook.Dispose();

            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";

            if (string.IsNullOrEmpty(fileName))
            {
                fileName = DateTime.Now.ToString("yyyyMMddHHmmss");
            }

            HttpContext.Current.Response.AppendHeader("Content-Disposition", "Attachment; FileName=" + GetToExcelName(fileName) + ".xls");
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 将数据集多个表保存到Excel
        /// </summary>
        /// <param name="dataSet">数据集</param>
        /// <param name="fileName">文件名</param>
        public static void ExportExcel(DataSet dataSet, string fileName = null)
        {
            HSSFWorkbook workBook = new HSSFWorkbook();
            int tableCount = 1;
            foreach (DataTable dataTable in dataSet.Tables)
            {
                Sheet sheet = workBook.CreateSheet("Sheet" + tableCount++);

                Font font = workBook.CreateFont();
                font.Boldweight = 700;
                CellStyle style = workBook.CreateCellStyle();
                style.Alignment = HorizontalAlignment.CENTER;
                style.SetFont(font);

                int rownum = 0;
                Row row = sheet.CreateRow(rownum++);
                Cell cell;

                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    cell = row.CreateCell(i);
                    cell.SetCellValue(dataTable.Columns[i].ColumnName);
                    cell.CellStyle = style;
                }

                CellStyle dateStyle = workBook.CreateCellStyle();
                DataFormat format = workBook.CreateDataFormat();
                dateStyle.DataFormat = format.GetFormat("yyyy-MM-dd HH:mm:ss");

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    row = sheet.CreateRow(rownum++);
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        cell = row.CreateCell(i);
                        string strValue = dataRow[i].ToString();
                        if (string.IsNullOrWhiteSpace(strValue))
                            cell.SetCellValue("");
                        else
                        {
                            switch (dataTable.Columns[i].DataType.ToString())
                            {
                                case "System.DateTime":
                                    DateTime dateTime;
                                    DateTime.TryParse(strValue, out dateTime);
                                    cell.SetCellValue(dateTime);
                                    cell.CellStyle = dateStyle;
                                    break;
                                case "System.Boolean":
                                    bool bValue;
                                    bool.TryParse(strValue, out bValue);
                                    cell.SetCellValue(bValue);
                                    break;
                                case "System.Int16":
                                case "System.Int32":
                                case "System.Int64":
                                case "System.Byte":
                                    int iValue = 0;
                                    int.TryParse(strValue, out iValue);
                                    cell.SetCellValue(iValue);
                                    break;
                                case "System.Decimal":
                                case "System.Double":
                                    double dValue = 0;
                                    double.TryParse(strValue, out dValue);
                                    cell.SetCellValue(dValue);
                                    break;
                                default:
                                    cell.SetCellValue(strValue);
                                    break;
                            }
                        }
                    }
                }
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    sheet.AutoSizeColumn(i);
                }
            }

            workBook.Write(HttpContext.Current.Response.OutputStream);
            workBook.Dispose();

            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";

            if (string.IsNullOrEmpty(fileName))
            {
                fileName = DateTime.Now.ToString("yyyyMMddHHmmss");
            }

            HttpContext.Current.Response.AppendHeader("Content-Disposition", "Attachment; FileName=" + GetToExcelName(fileName) + ".xls");
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 转换中文excel名称，防止乱码
        /// </summary>
        /// <param name="fileName">中文名称</param>
        /// <returns></returns>
        public static string GetToExcelName(string fileName)
        {
            string browser = HttpContext.Current.Request.Browser.Browser.ToLower();

            if (browser.IndexOf("firefox") == -1)
            {
                fileName = HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8);
            }

            return fileName;
        }
    }
}