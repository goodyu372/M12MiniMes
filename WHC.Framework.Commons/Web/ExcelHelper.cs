using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WHC.Framework.Commons.Web
{
    /// <summary>
    /// Web页面中导出Excel/Word的相关操作（把内容生成HTML格式导出方式）
    /// </summary>
    public class ExcelHelper
    {
        /// <summary>
        /// Web导出Excel文件，文件名为随机时间数字。
        /// </summary>
        /// <param name="dataGrid"></param>
        public static void ExportExcel(DataGrid dataGrid)
        {
            string str = DateTime.Now.ToFileTime() + ".xls";
            HttpResponse response = HttpContext.Current.Response;
            response.Charset = "GB2312";
            response.ContentEncoding = Encoding.GetEncoding("GB2312");
            response.ContentType = "application/ms-excel/msword";
            response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(str));
            StringWriter writer = new StringWriter();
            HtmlTextWriter writer2 = new HtmlTextWriter(writer);
            writer2.WriteLine("<meta http-equiv=\"Content-Type\" content=\"text/html;charset=GB2312\">");

            #region 采用了自定义生成页面的方式，则屏蔽该代码
            foreach (DataGridColumn column in dataGrid.Columns)
            {
                if (((column is ButtonColumn) || (column is EditCommandColumn)) ||
                    (column is HyperLinkColumn))
                {
                    column.Visible = false;
                }
            }
            if (dataGrid.Items.Count > 0)
            {
                TableCellCollection cells = dataGrid.Items[0].Cells;
                for (int i = 0; i < cells.Count; i++)
                {
                    foreach (Control control in cells[i].Controls)
                    {
                        if ((!(control is Label) && !(control is LiteralControl))
                            && !(control is DataBoundLiteralControl) && !(control is HyperLink))
                        {
                            dataGrid.Columns[i].Visible = false;
                            break;
                        }

                        HyperLink hyperLink = control as HyperLink;
                        if (hyperLink != null)
                        {
                            if (hyperLink.Text == "查看" || hyperLink.Text == "编辑")
                            {
                                dataGrid.Columns[i].Visible = false;
                            }
                        }
                    }
                }
            }
            #endregion

            writer2.WriteLine(RenderDataGrid(dataGrid));
            //dataGrid.RenderControl(writer2);
            response.Write(writer.ToString());
            response.End();
        }

        /// <summary>
        /// 把Web中的DataGrid生成对应的HTML内容
        /// </summary>
        /// <param name="dataGrid">Web中的DataGrid对象</param>
        /// <returns></returns>
        public static string RenderDataGrid(DataGrid dataGrid)
        {
            string strFormat = "<td>{0}</td>";
            StringBuilder sb = new StringBuilder();
            string header = "";
            for (int i = 0; i < dataGrid.Columns.Count; i++)
            {
                if (dataGrid.Columns[i].Visible)
                {
                    header += string.Format(strFormat, dataGrid.Columns[i].HeaderText) + "\r\n";
                }
            }
            sb.Append(string.Format("<tr height=40 bgcolor='#C0C0C0'>{0}</tr> \r\n", header));

            for (int k = 0; k < dataGrid.Items.Count; k++)
            {
                TableCellCollection cells = dataGrid.Items[k].Cells;

                ITextControl txtControl = null;
                string txtContent = "";
                for (int i = 0; i < cells.Count; i++)
                {
                    if (dataGrid.Columns[i].Visible)
                    {
                        string txtStr = "";
                        foreach (Control control in cells[i].Controls)
                        {
                            txtControl = control as ITextControl;
                            if (txtControl != null)
                            {
                                txtStr += txtControl.Text + "  ";
                            }
                            else
                            {
                                HyperLink webCtrl = control as HyperLink;
                                if (webCtrl != null)
                                {
                                    txtStr += webCtrl.Text + "  ";
                                }
                            }
                        }
                        txtContent += string.Format(strFormat, txtStr) + "\r\n";
                    }
                }
                if (!string.IsNullOrEmpty(txtContent))
                {
                    sb.Append(string.Format("<tr>{0}</tr> \r\n", txtContent));
                }
            }

            return string.Format("<Table border=1>{0}</Table> \r\n", sb.ToString());
        }

        /// <summary>
        /// 指定HTML内容及Excel文件名方式，Web导出Excel文件
        /// </summary>
        /// <param name="htmlString">HTML内容</param>
        /// <param name="fileName"></param>
        public static void ExportExcel(string htmlString, string fileName)
        {
            //确保加上后缀名
            if (fileName.IndexOf(".xls", StringComparison.OrdinalIgnoreCase) < 0)
            {
                fileName = fileName + ".xls";
            }

            HttpResponse response = HttpContext.Current.Response;
            response.Charset = "GB2312";
            response.ContentEncoding = Encoding.GetEncoding("GB2312");
            response.ContentType = "application/ms-excel/msword";
            response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName));
            StringWriter writer = new StringWriter();
            HtmlTextWriter writer2 = new HtmlTextWriter(writer);
            writer2.WriteLine("<meta http-equiv=\"Content-Type\" content=\"text/html;charset=GB2312\">");

            writer2.WriteLine(htmlString);
            response.Write(writer.ToString());
            response.Flush();
            response.End();
        }

        /// <summary>
        /// 在Web中导出HTML内容为Word文档格式，指定文件名
        /// </summary>
        /// <param name="htmlString"></param>
        /// <param name="fileName"></param>
        public static void ExportWord(string htmlString, string fileName)
        {
            //确保加上后缀名
            if (fileName.IndexOf(".doc", StringComparison.OrdinalIgnoreCase) < 0)
            {
                fileName = fileName + ".doc";
            }

            HttpResponse response = HttpContext.Current.Response;
            response.Charset = "GB2312";
            response.ContentEncoding = Encoding.GetEncoding("GB2312");
            response.ContentType = "application/ms-word";
            response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName));
            StringWriter writer = new StringWriter();
            HtmlTextWriter writer2 = new HtmlTextWriter(writer);
            writer2.WriteLine("<meta http-equiv=\"Content-Type\" content=\"text/html;charset=GB2312\">");

            writer2.WriteLine(htmlString);
            response.Write(writer.ToString());
            response.Flush();
            response.End();
        }

        /// <summary>
        /// 将对应的Web控件转换为HTML内容
        /// </summary>
        /// <param name="control">Web控件</param>
        /// <returns></returns>
        public static string RenderControl(Control control)
        {
            StringWriter writer = new StringWriter();
            HtmlTextWriter writer2 = new HtmlTextWriter(writer);
            control.RenderControl(writer2);
            return writer.ToString();
        }
    }
}
