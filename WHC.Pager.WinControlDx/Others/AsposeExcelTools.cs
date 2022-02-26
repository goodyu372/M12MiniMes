﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;
using System.IO;
using System.Drawing;
using Aspose.Cells;
using Aspose.Cells.Drawing;

namespace WHC.Pager.WinControl
{
    internal class AsposeExcelTools
    {
        /// <summary>
        /// DataTabel转换成Excel文件
        /// </summary>
        /// <param name="datatable">DataTable对象</param>
        /// <param name="colNameList">返回的字段列表</param>
        /// <param name="fromfile">Excel文件的全路径</param>
        /// <param name="error">错误信息:返回错误信息，没有错误返回""</param>
        /// <param name="beginColumn">开始列的序号</param>
        /// <param name="beginRow">开始的行序号</param>
        /// <returns>true:函数正确执行 false:函数执行错误</returns>
        public static bool DataTableInsertToExcel(DataTable datatable, ArrayList colNameList, string fromfile, out string error, int beginRow, int beginColumn)
        {
            error = "";
            if (datatable == null)
            {
                error = "DataTableToExcel:datatable 为空";
                return false;
            }

            Aspose.Cells.Workbook workbook = new Aspose.Cells.Workbook(fromfile);
            Aspose.Cells.Worksheet sheet = workbook.Worksheets[0];
            Aspose.Cells.Cells cells = sheet.Cells;
            //-------------插入数据-------------
            int nRow = 0;
            foreach (DataRow row in datatable.Rows)
            {
                nRow++;

                try
                {
                    cells.InsertRow(beginRow);
                    for (int i = 0; i < colNameList.Count; i++)
                    {
                        string colName = colNameList[i].ToString();
                        for (int j = 0; j < datatable.Columns.Count; j++)
                        {
                            if (colName == datatable.Columns[j].ColumnName)
                            {
                                object temp = row[datatable.Columns[j].ColumnName];
                                cells[beginRow, beginColumn + i].PutValue(row[datatable.Columns[j].ColumnName]);
                                break;
                            }
                        }
                    }
                }
                catch (System.Exception e)
                {
                    error = error + " DataTableInsertToExcel: " + e.Message;
                }


            }
            //-------------保存-------------
            workbook.Save(fromfile);
            return true;

        }
        
        /// <summary>
        /// 把DataTabel转换成Excel文件
        /// </summary>
        /// <param name="datatable">DataTable对象</param>
        /// <param name="filepath">目标文件路径,Excel文件的全路径</param>
        /// <param name="error">错误信息:返回错误信息，没有错误返回""</param>
        /// <returns></returns>
        public static bool DataTableToExcel(DataTable datatable, string filepath, out string error)
        {
            error = "";
            try
            {
                if (datatable == null)
                {
                    error = "DataTableToExcel:datatable 为空";
                    return false;
                }

                Aspose.Cells.Workbook workbook = new Aspose.Cells.Workbook();
                Aspose.Cells.Worksheet sheet = workbook.Worksheets[0];
                Aspose.Cells.Cells cells = sheet.Cells;

                int nRow = 0;
                foreach (DataRow row in datatable.Rows)
                {
                    nRow++;
                    try
                    {
                        for (int i = 0; i < datatable.Columns.Count; i++)
                        {                           
                            if (row[i].GetType().ToString() == "System.Drawing.Bitmap")
                            {
                                //------插入图片数据-------
                                System.Drawing.Image image = (System.Drawing.Image)row[i];
                                MemoryStream mstream = new MemoryStream();
                                image.Save(mstream, System.Drawing.Imaging.ImageFormat.Jpeg);
                                sheet.Pictures.Add(nRow, i, mstream);
                            }
                            else
                            {
                                cells[nRow, i].PutValue(row[i]);
                            }
                        }
                    }
                    catch (System.Exception e)
                    {
                        error = error + " DataTableToExcel: " + e.Message;
                    }
                }

                workbook.Save(filepath);
                return true;
            }
            catch (System.Exception e)
            {
                error = error + " DataTableToExcel: " + e.Message;
                return false;
            }
        }

        /// <summary>
        /// 把DataTabel转换成Excel文件
        /// </summary>
        /// <param name="datatable">DataTable对象</param>
        /// <param name="filepath">目标文件路径,Excel文件的全路径</param>
        /// <param name="error">错误信息:返回错误信息，没有错误返回""</param>
        /// <returns></returns>
        public static bool DataTableToExcel2(DataTable datatable, string filepath, out string error)
        {
            error = "";
            Aspose.Cells.Workbook wb = new Aspose.Cells.Workbook();

            try
            {
                if (datatable == null)
                {
                    error = "DataTableToExcel:datatable 为空";
                    return false;
                }

                //为单元格添加样式    
                Aspose.Cells.Style style = wb.Styles[wb.Styles.Add()];
                //设置居中
                style.HorizontalAlignment = Aspose.Cells.TextAlignmentType.Center;
                //设置背景颜色
                style.ForegroundColor = System.Drawing.Color.FromArgb(153, 204, 0);
                style.Pattern = BackgroundType.Solid;
                style.Font.IsBold = true;

                int rowIndex = 0;
                for (int i = 0; i < datatable.Columns.Count; i++)
                {
                    DataColumn col = datatable.Columns[i];
                    string columnName = col.Caption ?? col.ColumnName;
                    wb.Worksheets[0].Cells[rowIndex, i].PutValue(columnName);
                    wb.Worksheets[0].Cells[rowIndex, i].SetStyle(style);
                }
                rowIndex++;

                foreach (DataRow row in datatable.Rows)
                {
                    for (int i = 0; i < datatable.Columns.Count; i++)
                    {
                        wb.Worksheets[0].Cells[rowIndex, i].PutValue(row[i].ToString());
                    }
                    rowIndex++;
                }

                for (int k = 0; k < datatable.Columns.Count; k++)
                {
                    wb.Worksheets[0].AutoFitColumn(k, 0, 150);
                }
                wb.Worksheets[0].FreezePanes(1, 0, 1, datatable.Columns.Count);
                wb.Save(filepath);
                return true;
            }
            catch (Exception e)
            {
                error = error + " DataTableToExcel: " + e.Message;
                return false;
            }

        }

        /// <summary>
        /// DataSet导出到Excel文件
        /// 默认导出第一页的数据
        /// </summary>
        /// <param name="dataset">DataSet</param>
        /// <param name="filepath">Excel文件的全路径</param>
        /// <param name="error">错误信息:返回错误信息，没有错误返回""</param>
        /// <returns>true:函数正确执行 false:函数执行错误</returns>
        public static bool DataSetToExcel(DataSet dataset, string filepath, out string error)
        {
            if (DataTableToExcel(dataset.Tables[0], filepath, out error))
            {
                return true;
            }
            else
            {
                error = "DataTableToExcel: " + error;
                return false;
            }
        }

        /// <summary>
        /// Excel文件转换为DataTable（第一个sheet）
        /// </summary>
        /// <param name="filepath">Excel文件的全路径</param>
        /// <param name="datatable">DataTable:返回值</param>
        /// <param name="error">错误信息:返回错误信息，没有错误返回""</param>
        /// <returns>true:函数正确执行 false:函数执行错误</returns>
        public static bool ExcelFileToDataTable(string filepath, out DataTable datatable, out string error)
        {
            bool exportColumnName = true;
            return ExcelFileToDataTable(filepath, out datatable, exportColumnName, out error);
        }

        /// <summary>
        /// Excel文件转换为DataTable.
        /// </summary>
        /// <param name="filepath">Excel文件的全路径</param>
        /// <param name="datatable">DataTable:返回值</param>
        /// <param name="error">错误信息:返回错误信息，没有错误返回""</param>
        /// <returns>true:函数正确执行 false:函数执行错误</returns>
        public static bool ExcelFileToDataTable(string filepath, out DataTable datatable, bool exportColumnName, out string error)
        {
            error = "";
            datatable = null;
            try
            {
                if (File.Exists(filepath) == false)
                {
                    error = "文件不存在";
                    datatable = null;
                    return false;
                }
                Aspose.Cells.Workbook workbook = new Aspose.Cells.Workbook(filepath);
                Aspose.Cells.Worksheet worksheet = workbook.Worksheets[0];
                datatable = worksheet.Cells.ExportDataTableAsString(0, 0, worksheet.Cells.MaxRow + 1, worksheet.Cells.MaxColumn + 1, exportColumnName);
                //-------------图片处理-------------
                Aspose.Cells.Drawing.PictureCollection pictures = worksheet.Pictures;
                if (pictures.Count > 0)
                {
                    string error2 = "";
                    if (InsertPicturesIntoDataTable(pictures, datatable, out datatable, out error2) == false)
                    {
                        error = error + error2;
                    }
                }
                return true;
            }
            catch (System.Exception e)
            {
                error = e.Message;
                return false;
            }

        }

        /// <summary>
        /// 把所有Sheet里面的内容，分别导入到不同的DataTable对象里面
        /// </summary>
        /// <param name="filepath">Excel文件的全路径</param>
        /// <param name="datatables">DataTable对象集合</param>
        /// <param name="error">错误信息:返回错误信息，没有错误返回""</param>
        /// <returns></returns>
        public static bool ExcelFileToDataTables(string filepath, out DataTable[] datatables, out string error)
        {
            bool exportColumnName = true;
            return ExcelFileToDataTables(filepath, out datatables, exportColumnName, out error);
        }

        /// <summary>
        /// 把所有Sheet里面的内容，分别导入到不同的DataTable对象里面
        /// </summary>
        /// <param name="filepath">Excel文件的全路径</param>
        /// <param name="datatables">DataTable对象集合</param>
        /// <param name="error">错误信息:返回错误信息，没有错误返回""</param>
        /// <returns></returns>
        public static bool ExcelFileToDataTables(string filepath, out DataTable[] datatables, bool exportColumnName, out string error)
        {
            error = "";
            datatables = null;
            int nSheetsCount = 0;
            try
            {
                if (File.Exists(filepath) == false)
                {
                    error = "文件不存在";
                    datatables = null;
                    return false;
                }
                Aspose.Cells.Workbook workbook = new Aspose.Cells.Workbook(filepath);
                nSheetsCount = workbook.Worksheets.Count;
                datatables = new DataTable[nSheetsCount];
                for (int i = 0; i < nSheetsCount; i++)
                {
                    Aspose.Cells.Worksheet worksheet = workbook.Worksheets[i];
                    datatables[i] = worksheet.Cells.ExportDataTableAsString(0, 0, worksheet.Cells.MaxRow + 1, worksheet.Cells.MaxColumn + 1, exportColumnName);
                    //-------------图片处理-------------
                    Aspose.Cells.Drawing.PictureCollection pictures = worksheet.Pictures;
                    if (pictures.Count > 0)
                    {
                        string error2 = "";
                        if (InsertPicturesIntoDataTable(pictures, datatables[i], out datatables[i], out error2) == false)
                        {
                            error = error + error2;
                        }
                    }
                }

                return true;
            }
            catch (System.Exception e)
            {
                error = e.Message;
                return false;
            }

        }

        /// <summary>
        /// Excel文件转换为DataTable.(指定开始行列，以及导入的记录行数）
        /// </summary>
        /// <param name="filepath">Excel文件的全路径</param>
        /// <param name="datatable">DataTable:返回值</param>
        /// <param name="iFirstRow">起始行</param>
        /// <param name="iFirstCol">起始列</param>
        /// <param name="rowNum">导入行数</param>
        /// <param name="colNum">列数</param>
        /// <param name="error">错误信息:返回错误信息，没有错误返回""</param>
        /// <returns></returns>
        public static bool ExcelFileToDataTable(string filepath, out DataTable datatable, int iFirstRow, int iFirstCol, int rowNum, int colNum, bool exportColumnName, out string error)
        {
            error = "";
            datatable = null;
            try
            {
                if (File.Exists(filepath) == false)
                {
                    error = "文件不存在";
                    datatable = null;
                    return false;
                }
                Aspose.Cells.Workbook workbook = new Aspose.Cells.Workbook(filepath);
                Aspose.Cells.Worksheet worksheet = workbook.Worksheets[0];
                datatable = worksheet.Cells.ExportDataTableAsString(iFirstRow, iFirstCol, rowNum + 1, colNum + 1, exportColumnName);
                return true;
            }
            catch (System.Exception e)
            {
                error = e.Message;
                return false;
            }

        }

        /// <summary>
        /// Excel文件转换为DataSet.
        /// </summary>
        /// <param name="filepath">Excel文件的全路径</param>
        /// <param name="dataset">DataSet:返回值</param>
        /// <param name="error">错误信息:返回错误信息，没有错误返回""</param>
        /// <returns>true:函数正确执行 false:函数执行错误</returns>
        public static bool ExcelFileToDataSet(string filepath, out DataSet dataset, out string error)
        {
            DataTable datatable = new DataTable();
            dataset = new System.Data.DataSet();

            if (ExcelFileToDataTable(filepath, out datatable, out error))
            {
                dataset.Tables.Add(datatable);
                return true;
            }
            else
            {
                error = "ExcelFileToDataSet: " + error;
                return false;
            }
        }

        /// <summary>
        /// 获取Excel文件里面的图片，并把图片存储到图片对象列表中
        /// </summary>
        /// <param name="filepath">Excel文件的全路径</param>
        /// <param name="pictures">图片对象列表</param>
        /// <param name="error">错误信息:返回错误信息，没有错误返回""</param>
        /// <returns></returns>
        public static bool GetPicturesFromExcelFile(string filepath, out PictureCollection[] pictures, out string error)
        {
            error = "";
            pictures = null;
            try
            {
                if (File.Exists(filepath) == false)
                {
                    error = "文件不存在";
                    pictures = null;
                    return false;
                }
                Aspose.Cells.Workbook workbook = new Aspose.Cells.Workbook(filepath);
                pictures = new Aspose.Cells.Drawing.PictureCollection[workbook.Worksheets.Count];
                for (int i = 0; i < workbook.Worksheets.Count; i++)
                {
                    //pictures.Add();
                    pictures[i] = workbook.Worksheets[i].Pictures;
                }
                return true;
            }
            catch (System.Exception e)
            {
                error = e.Message;
                return false;
            }
        }

        private static bool InsertPicturesIntoDataTable(Aspose.Cells.Drawing.PictureCollection pictures, DataTable fromdatatable, out DataTable datatable, out string error)
        {
            error = "";
            datatable = fromdatatable;
            //把图片按位置插入Table中
            DataRow[] rows = datatable.Select();
            foreach (Picture picture in pictures)
            {
                try
                {
                    System.Console.WriteLine(picture.GetType().ToString());

                    //----把图片转换成System.Drawing.Image----
                    MemoryStream mstream = new MemoryStream();
                    mstream.Write(picture.Data, 0, picture.Data.Length);
                    System.Drawing.Image image = System.Drawing.Image.FromStream(mstream);
                    //----Image放入DataTable------
                    //datatable.Columns[picture.UpperLeftColumn].DataType = image.GetType();
                    rows[picture.UpperLeftRow][picture.UpperLeftColumn] = image;
                }
                catch (System.Exception e)
                {
                    error = error + " InsertPicturesIntoDataTable: " + e.Message;
                }
            }
            return true;
        }

        /// <summary>
        /// 把Excel文件内容导入到List对象
        /// </summary>
        /// <param name="filepath">Excel文件的全路径</param>
        /// <param name="lists">列表对象</param>
        /// <param name="error">错误信息:返回错误信息，没有错误返回""</param>
        /// <returns></returns>
        public static bool ExcelFileToLists(string filepath, out IList[] lists, out string error)
        {
            error = "";
            lists = null;
            DataTable datatable = new DataTable();
            IList list = new ArrayList();
            Aspose.Cells.Drawing.PictureCollection[] pictures;
            if (ExcelFileToDataTable(filepath, out datatable, out error) && GetPicturesFromExcelFile(filepath, out pictures, out error))
            {
                lists = new ArrayList[datatable.Rows.Count];
                //------------DataTable转换成IList[]--------------
                //数据
                int nRow = 0;
                foreach (DataRow row in datatable.Rows)
                {
                    lists[nRow] = new ArrayList(datatable.Columns.Count);
                    for (int i = 0; i <= datatable.Columns.Count - 1; i++)
                    {
                        lists[nRow].Add(row[i]);
                    }
                    nRow++;
                }
                //图片
                for (int i = 0; i < pictures.Length; i++)
                {
                    foreach (Picture picture in pictures[i])
                    {
                        try
                        {
                            //----把图片转换成System.Drawing.Image----
                            //MemoryStream mstream = new MemoryStream();
                            //mstream.Write(picture.Data, 0, picture.Data.Length);
                            //System.Drawing.Image image = System.Drawing.Image.FromStream(mstream);
                            //----Image放入IList------
                            //图片有可能越界
                            if (picture.UpperLeftRow <= datatable.Rows.Count && picture.UpperLeftColumn <= datatable.Columns.Count)
                            {
                                lists[picture.UpperLeftRow][picture.UpperLeftColumn] = picture.Data;
                            }

                        }
                        catch (System.Exception e)
                        {
                            error = error + e.Message;
                        }
                    }
                }
            }
            else
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 把列表导出到Excle文件
        /// </summary>
        /// <param name="filepath">Excel文件的全路径</param>
        /// <param name="lists">列表对象</param>
        /// <param name="error">错误信息:返回错误信息，没有错误返回""</param>
        /// <returns></returns>
        public static bool ListsToExcelFile(string filepath, IList[] lists, out string error)
        {
            error = "";
            Aspose.Cells.Workbook workbook = new Aspose.Cells.Workbook();
            Aspose.Cells.Worksheet sheet = workbook.Worksheets[0];
            Aspose.Cells.Cells cells = sheet.Cells;

            int nRow = 0;
            sheet.Pictures.Clear();
            cells.Clear();
            foreach (IList list in lists)
            {

                for (int i = 0; i <= list.Count - 1; i++)
                {
                    try
                    {
                        System.Console.WriteLine(i.ToString() + "  " + list[i].GetType());
                        if (list[i].GetType().ToString() == "System.Drawing.Bitmap")
                        {
                            //插入图片数据
                            System.Drawing.Image image = (System.Drawing.Image)list[i];
                            MemoryStream mstream = new MemoryStream();
                            image.Save(mstream, System.Drawing.Imaging.ImageFormat.Jpeg);
                            sheet.Pictures.Add(nRow, i, mstream);
                        }
                        else
                        {
                            cells[nRow, i].PutValue(list[i]);
                        }
                    }
                    catch (System.Exception e)
                    {
                        error = error + e.Message;
                    }
                }

                nRow++;
            }

            workbook.Save(filepath);
            return true;
        }
    }
}
