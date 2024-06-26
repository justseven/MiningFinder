using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiderService.Until
{
    internal class NPOIHelper
    {
        /// <summary>
        /// 从DataTable导出EXCEL
        /// </summary>
        /// <param name="SourceTable"></param>
        /// <returns></returns>
        public static Stream RenderDataTableToExcel(DataTable SourceTable)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            MemoryStream ms = new MemoryStream();
            HSSFSheet sheet = (HSSFSheet)workbook.CreateSheet();
            HSSFRow headerRow = (HSSFRow)sheet.CreateRow(0);

            // handling header.
            foreach (DataColumn column in SourceTable.Columns)
                headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);

            // handling value.
            int rowIndex = 1;

            foreach (DataRow row in SourceTable.Rows)
            {
                HSSFRow dataRow = (HSSFRow)sheet.CreateRow(rowIndex);

                foreach (DataColumn column in SourceTable.Columns)
                {
                    dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                }

                rowIndex++;
            }

            workbook.Write(ms);
            ms.Flush();
            ms.Position = 0;

            sheet = null;
            headerRow = null;
            workbook = null;

            return ms;
        }
        /// <summary>
        /// 从DataTable导出
        /// </summary>
        /// <param name="SourceTable">源集合</param>
        /// <param name="FileName">导出路径</param>
        public static int RenderDataTableToExcel(DataTable SourceTable, string FileName)
        {
            try
            {
                MemoryStream ms = RenderDataTableToExcel(SourceTable) as MemoryStream;
                FileStream fs = new FileStream(FileName, FileMode.Create, FileAccess.Write);
                byte[] data = ms.ToArray();

                fs.Write(data, 0, data.Length);
                fs.Flush();
                fs.Close();

                data = null;
                ms = null;
                fs = null;
                return 1;
            }
            catch (Exception ex)
            {
                string str = ex.Message;
                return 0;
            }
        }

        /// <summary>
        /// 读取excel转为DataTable
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// <param name="sheetName">指定sheet</param>
        /// <param name="isColumnName">第一行是否为列名</param>
        /// <returns></returns>
        public static DataTable ExcelToDataTable(string fileName, string sheetName, bool isColumnName)
        {
            IWorkbook workbook = null;
            ISheet sheet = null;
            //初始化开始行
            int startRow = 0;
            DataTable dt = new DataTable();
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            //低于2007版本
            if (Path.GetExtension(fileName) == ".xls")
            {
                workbook = new HSSFWorkbook(fs);
            }
            //2007及以上版本
            else if (Path.GetExtension(fileName) == ".xlsx")
            {
                workbook = new XSSFWorkbook(fs);
            }
            //判断是否指定sheet上传
            if (sheetName != null)
            {
                //获取指定sheet
                sheet = workbook.GetSheet(sheetName);
                if (sheet == null)
                {
                    //获取不到时取第一个sheet
                    sheet = workbook.GetSheetAt(0);
                }
            }
            else
            {
                sheet = workbook.GetSheetAt(0);
            }
            if (sheet != null)
            {
                //sheet中第一行
                IRow firstRow = sheet.GetRow(0);
                //判断第一行是否是列名
                if (isColumnName)
                {
                    //遍历第一行的单元格
                    for (int i = firstRow.FirstCellNum; i < firstRow.LastCellNum; i++)
                    {
                        //得到列名
                        ICell cell = firstRow.GetCell(i);
                        if (cell != null)
                        {
                            //得到列名的值,若列名不是字符则不能使用StringCellValue，最好使用ToString()
                            string cellValue = cell.ToString();
                            if (cellValue != null)
                            {
                                try
                                {
                                    //将列放入datatable中
                                    DataColumn column = new DataColumn(cellValue);
                                    dt.Columns.Add(column);
                                }
                                catch
                                {
                                    throw new Exception("列名有误！");
                                }
                            }
                        }
                        startRow = sheet.FirstRowNum + 1;
                    }
                }
                //遍历所有行
                for (int i = startRow; i <= sheet.LastRowNum; i++)
                {
                    //得到i行
                    IRow row = sheet.GetRow(i);
                    if (row == null)
                    {
                        continue;
                    }
                    //datatable新增行
                    DataRow dr = dt.NewRow();
                    //遍历i行的单元格
                    for (int j = row.FirstCellNum; j < row.LastCellNum; j++)
                    {
                        if (row.GetCell(j) != null)
                        {
                            dr[j] = row.GetCell(j).ToString();
                        }
                    }
                    try
                    {
                        //将行放入datatable中
                        dt.Rows.Add(dr);
                    }
                    catch
                    {
                        throw new Exception("第" + i + "行有误！");
                    }
                }
            }
            return dt;
        }

        public static DataTable RenderDataTableFromExcel(String path, string SheetName)
        {
            HSSFWorkbook workbook;
            using (FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                workbook = new HSSFWorkbook(file);
            }
            HSSFSheet sheet = (HSSFSheet)workbook.GetSheet(SheetName);

            DataTable table = new DataTable();

            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

            HSSFRow headerRow = (HSSFRow)sheet.GetRow(0);
            int cellCount = headerRow.LastCellNum;

            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            {
                DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                table.Columns.Add(column);
            }

            int rowCount = sheet.LastRowNum;

            for (int i = (sheet.FirstRowNum + 1); i < sheet.LastRowNum; i++)
            {

                HSSFRow row = (HSSFRow)sheet.GetRow(i);
                DataRow dataRow = table.NewRow();

                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    dataRow[j] = row.GetCell(j).ToString();

                }
                table.Rows.Add(dataRow);
            }
            workbook = null;
            sheet = null;
            return table;
        }

        public static DataTable RenderDataTableFromExcel(Stream ExcelFileStream, string SheetName, int HeaderRowIndex)
        {
            HSSFWorkbook workbook = new HSSFWorkbook(ExcelFileStream);
            HSSFSheet sheet = (HSSFSheet)workbook.GetSheet(SheetName);

            DataTable table = new DataTable();

            HSSFRow headerRow = (HSSFRow)sheet.GetRow(HeaderRowIndex);
            int cellCount = headerRow.LastCellNum;

            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            {
                DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                table.Columns.Add(column);
            }

            int rowCount = sheet.LastRowNum;

            for (int i = (sheet.FirstRowNum + 1); i < sheet.LastRowNum; i++)
            {
                HSSFRow row = (HSSFRow)sheet.GetRow(i);
                DataRow dataRow = table.NewRow();

                for (int j = row.FirstCellNum; j < cellCount; j++)
                    dataRow[j] = row.GetCell(j).ToString();
            }

            ExcelFileStream.Close();
            workbook = null;
            sheet = null;
            return table;
        }

        public static DataTable RenderDataTableFromExcel(Stream ExcelFileStream, int SheetIndex, int HeaderRowIndex)
        {
            HSSFWorkbook workbook = new HSSFWorkbook(ExcelFileStream);
            HSSFSheet sheet = (HSSFSheet)workbook.GetSheetAt(SheetIndex);

            DataTable table = new DataTable();

            HSSFRow headerRow = (HSSFRow)sheet.GetRow(HeaderRowIndex);
            int cellCount = headerRow.LastCellNum;

            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            {
                DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                table.Columns.Add(column);
            }

            int rowCount = sheet.LastRowNum;

            for (int i = (sheet.FirstRowNum + 1); i < sheet.LastRowNum; i++)
            {
                HSSFRow row = (HSSFRow)sheet.GetRow(i);
                DataRow dataRow = table.NewRow();

                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    if (row.GetCell(j) != null)
                        dataRow[j] = row.GetCell(j).ToString();
                }

                table.Rows.Add(dataRow);
            }

            ExcelFileStream.Close();
            workbook = null;
            sheet = null;
            return table;
        }

        /// <summary>读取excel
        /// 默认第一行为标头
        /// </summary>
        /// <param name="path">excel文档路径</param>
        /// <returns></returns>
        public static DataTable RenderDataTableFromExcel(string path)
        {
            DataTable dt = new DataTable();

            HSSFWorkbook hssfworkbook;
            using (FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                hssfworkbook = new HSSFWorkbook(file);
            }
            HSSFSheet sheet = (HSSFSheet)hssfworkbook.GetSheetAt(0);
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

            HSSFRow headerRow = (HSSFRow)sheet.GetRow(0);
            int cellCount = headerRow.LastCellNum;

            for (int j = 0; j < cellCount; j++)
            {
                HSSFCell cell = (HSSFCell)headerRow.GetCell(j);
                dt.Columns.Add(cell.ToString());
            }

            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            {
                HSSFRow row = (HSSFRow)sheet.GetRow(i);
                DataRow dataRow = dt.NewRow();

                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    if (row.GetCell(j) != null)
                        dataRow[j] = row.GetCell(j).ToString();
                }

                dt.Rows.Add(dataRow);
            }

            //while (rows.MoveNext())
            //{
            //    HSSFRow row = (HSSFRow)rows.Current;
            //    DataRow dr = dt.NewRow();

            //    for (int i = 0; i < row.LastCellNum; i++)
            //    {
            //        HSSFCell cell = row.GetCell(i);


            //        if (cell == null)
            //        {
            //            dr[i] = null;
            //        }
            //        else
            //        {
            //            dr[i] = cell.ToString();
            //        }
            //    }
            //    dt.Rows.Add(dr);
            //}

            return dt;
        }
    }
}
