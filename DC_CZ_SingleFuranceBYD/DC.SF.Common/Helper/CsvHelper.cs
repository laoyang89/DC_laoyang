using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DC.SF.Common.Helper
{
    /// <summary>  
    /// CSV文件辅助类  
    /// </summary>  
    public class CsvHelper
    {
        #region 数据导出到Csv文件
        /// <summary>
        /// 导出到CSV文件
        /// </summary>
        /// <param name="table">报表DataTable</param>
        /// <param name="filePath">导出路径</param>
        /// <param name="msg">输出信息</param>
        /// <param name="columnName">自定义的列名称，以','英文逗号分隔</param>
        /// <param name="tableHeader">表名，一般为空</param>
        /// <returns>是否导出成功</returns>
        public static bool ExportDataTableToCSV(DataTable table, string filePath, out string msg, string columnName = null, string tableHeader = null)
        {
            try
            {
                string dirPath = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);//文件的父目录不存在就创建新目录
                }
                bool IsAppend = false; //false为创建 true为追加
                if (File.Exists(filePath))
                {
                    IsAppend = true;//如果文件存在就不写入表头
                }
                using (FileStream _stream = new FileStream(filePath, FileMode.Create | FileMode.Append, FileAccess.Write))
                {
                    StreamWriter _writer = new StreamWriter(_stream, Encoding.UTF8);
                    if (tableHeader != null)
                    {
                        _writer.WriteLine(tableHeader);
                    }

                    if (columnName != null && !IsAppend)
                    {
                        _writer.WriteLine(columnName);
                    }
                    else if (columnName == null && !IsAppend)
                    {
                        List<string> columnNameList = new List<string>();
                        foreach (DataColumn dc in table.Columns)
                        {
                            columnNameList.Add(dc.ColumnName);
                        }
                        _writer.WriteLine(string.Join(",", columnNameList));
                    }

                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        for (int j = 0; j < table.Columns.Count; j++)
                        {
                            _writer.Write(table.Rows[i][j].ToString());
                            _writer.Write(",");
                        }
                        _writer.WriteLine();
                    }
                    _writer.Close();
                    msg = "导出CSV文件成功";
                    return true;
                }
            }
            catch (Exception ex)
            {
                msg = string.Format("导出CSV文件失败，原因：{0}", ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 将StringBuilder导入到CSV
        /// </summary>
        /// <param name="sb">StringBuilder实例，以','英文逗号分隔，拼接时每行结束要加\r\n</param>
        /// <param name="columnName">表名，以','英文逗号分隔，无需加\r\n</param>
        /// <param name="filePath">完整路径名</param>
        /// <param name="msg">输出信息</param>
        /// <returns></returns>
        public static bool ExportStringBuilderToCSV(StringBuilder sb, string columnName, string filePath, out string msg) //Add by Lavender Shi 20190806
        {
            try
            {
                string dirPath = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);//文件的父目录不存在就创建新目录
                }
                if (!File.Exists(filePath))
                {
                    sb.Insert(0, columnName + "\r\n"); //如果文件不存在就写入表头
                }
                using (FileStream stream = new FileStream(filePath, FileMode.Create | FileMode.Append, FileAccess.Write))
                {
                    byte[] bs = Encoding.GetEncoding("GB2312").GetBytes(sb.ToString());
                    stream.Write(bs, 0, bs.Length);
                }
                msg = "导出CSV文件成功";
                return true;
            }
            catch (Exception ex)
            {
                msg = string.Format("导出CSV文件失败，原因：{0}", ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 列表导入到CSV文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">列表</param>
        /// <param name="path">导出路径</param>
        /// <param name="msg">输出信息</param>
        /// <returns></returns>
        public static bool ExportListToCsv<T>(List<T> list, string path, out string msg) where T : class, new() //Add by Lavender Shi 20200102
        {
            try
            {
                string dir = Path.GetDirectoryName(path);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                bool isCreate = false;
                if (!File.Exists(path))
                {
                    //如果文件不存在就写入表头
                    isCreate = true;
                }
                Type t = typeof(T);
                PropertyInfo[] props = t.GetProperties();
                
                StringBuilder sb = new StringBuilder();
                if (isCreate)
                {
                    foreach (var prop in props)
                    {
                        DisplayNameAttribute attrib = (DisplayNameAttribute)prop.GetCustomAttributes(typeof(DisplayNameAttribute), false).FirstOrDefault();
                        if (attrib != null)
                        {
                            sb.Append(attrib.DisplayName + ",");
                        }
                        else
                        {
                            sb.Append(prop.Name + ",");
                        }
                    }
                    sb.Remove(sb.Length - 1, 1);
                    sb.AppendLine();
                }
                if (list != null)
                {
                    foreach (T item in list)
                    {
                        foreach (PropertyInfo element in props)
                        {
                            sb.Append((element.GetValue(item) == null ? "" : element.GetValue(item).ToString()) + ",");
                        }
                        sb.Remove(sb.Length - 1, 1);
                        sb.AppendLine();
                    }
                }
                using (FileStream fs = File.Open(path, FileMode.Create | FileMode.Append, FileAccess.Write))
                {
                    byte[] bs = Encoding.Default.GetBytes(sb.ToString());
                    fs.Write(bs, 0, bs.Length);
                }
                msg = "导出CSV成功！";
                return true;
            }
            catch (Exception ex)
            {
                msg = string.Format("导出CSV失败，原因：{0}", ex.Message + Environment.NewLine + ex.StackTrace);
                return false;
            }
        }

        /// <summary>
        /// 追加实体到CSV文件
        /// </summary>
        /// <typeparam name="T">实体模板</typeparam>
        /// <param name="model">实体</param>
        /// <param name="path">导出路径</param>
        /// <param name="msg">输出信息</param>
        /// <returns>是否成功</returns>
        public static bool AppendModelToCsv<T>(T model, string path, out string msg) where T : class, new()
        {
            try
            {
                string dir = Path.GetDirectoryName(path);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                bool isCreate = false;
                if (!File.Exists(path))
                {
                    //如果文件不存在就写入表头
                    isCreate = true;
                }
                Type t = typeof(T);
                PropertyInfo[] props = t.GetProperties();
                StringBuilder sb = new StringBuilder();
                if (isCreate)
                {
                    foreach (var prop in props)
                    {
                        DisplayNameAttribute attrib = (DisplayNameAttribute)prop.GetCustomAttributes(typeof(DisplayNameAttribute), false).FirstOrDefault();
                        if (attrib != null)
                        {
                            sb.Append(attrib.DisplayName + ",");
                        }
                        else
                        {
                            sb.Append(prop.Name + ",");
                        }
                    }
                    sb.Remove(sb.Length - 1, 1);
                    sb.AppendLine();
                }
                if (model != null)
                {
                    foreach (PropertyInfo element in props)
                    {
                        sb.Append((element.GetValue(model) == null ? "" : element.GetValue(model).ToString()) + ",");
                    }
                    sb.Remove(sb.Length - 1, 1);
                    sb.AppendLine();
                }
                using (FileStream fs = File.Open(path, FileMode.Create | FileMode.Append, FileAccess.Write))
                {
                    byte[] bs = Encoding.Default.GetBytes(sb.ToString());
                    fs.Write(bs, 0, bs.Length);
                }
                msg = "导出CSV成功！";
                return true;
            }
            catch (Exception ex)
            {
                msg = string.Format("导出CSV失败，原因：{0}", ex.Message + Environment.NewLine + ex.StackTrace);
                return false;
            }
        }

        /// <summary>
        /// 追加字符串拼接内容到CSV文件 
        /// 本方法需保证拼接内容及表头的正确性，以英文“,”作为分隔符
        /// </summary>
        /// <param name="content">拼接内容</param>
        /// <param name="header">表头</param>
        /// <param name="path">导出路径</param>
        /// <param name="msg">输出信息</param>
        /// <returns>是否成功</returns>
        public static bool AppendStringConcatenationToCsv(string content, string header, string path, out string msg)
        {
            try
            {
                if (content.Split(',').Length != header.Split(',').Length)
                {
                    msg = string.Format("导出CSV失败，原因：{0}", "表头字段数目与拼接字符串的元素数目不同");
                    return false;
                }
                string dir = Path.GetDirectoryName(path);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                bool isCreate = false;
                if (!File.Exists(path))
                {
                    //如果文件不存在就写入表头
                    isCreate = true;
                }
                string text = string.Empty;
                if (isCreate)
                {
                    text += header + Environment.NewLine;
                }
                if (content != null)
                {
                    text += content + Environment.NewLine;
                }
                using (FileStream fs = File.Open(path, FileMode.Create | FileMode.Append, FileAccess.Write))
                {
                    byte[] bs = Encoding.Default.GetBytes(text);
                    fs.Write(bs, 0, bs.Length);
                }
                msg = "导出CSV成功！";
                return true;
            }
            catch (Exception ex)
            {
                msg = string.Format("导出CSV失败，原因：{0}", ex.Message + Environment.NewLine + ex.StackTrace);
                return false;
            }
        }
        #endregion

        #region  将CSV文件导入到DataTable
        /// <summary>
        /// 将CSV文件导入到DataTable 无需初始化DataTable
        /// </summary>
        /// <param name="filePath">Csv文件路径</param>
        /// <returns>返回的报表</returns>
        public static DataTable ImportCsvToTable(string filePath)
        {
            try
            {
                //Encoding encoding = Common.GetType(filePath);
                Encoding encoding = Encoding.Default;
                DataTable dt = new DataTable();
                FileStream fs = new FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                //StreamReader sr = new StreamReader(fs, Encoding.UTF8);
                StreamReader sr = new StreamReader(fs, encoding);
                //记录每次读取的一行记录
                string strLine = "";
                //记录每行记录中的各字段内容
                string[] aryLine = null;
                string[] tableHead = null;
                //标示列数
                int columnCount = 0;
                //标示是否是读取的第一行
                bool IsFirst = true;
                //逐行读取CSV中的数据
                while ((strLine = sr.ReadLine()) != null)
                {
                    //strLine = Common.ConvertStringUTF8(strLine, encoding);
                    //strLine = Common.ConvertStringUTF8(strLine);
                    if (IsFirst == true)
                    {
                        tableHead = strLine.Split(',');
                        IsFirst = false;
                        columnCount = tableHead.Length;
                        //创建列
                        for (int i = 0; i < columnCount; i++)
                        {
                            DataColumn dc = new DataColumn(tableHead[i]);
                            dt.Columns.Add(dc);
                        }
                    }
                    else
                    {
                        aryLine = strLine.Split(',');
                        DataRow dr = dt.NewRow();
                        for (int j = 0; j < columnCount; j++)
                        {
                            dr[j] = aryLine[j];
                        }
                        dt.Rows.Add(dr);
                    }
                }
                if (aryLine != null && aryLine.Length > 0)
                {
                    dt.DefaultView.Sort = tableHead[0];
                }
                sr.Close();
                fs.Close();
                return dt;
            }
            catch (Exception ex)
            {
                string msg = string.Format("导入CSV文件到DataTable失败，原因：{0}", ex.Message + Environment.NewLine + ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// 将CSV文件导入到DataTable 需要初始化DataTable
        /// </summary>
        /// <param name="table">导入的DataTable，需初始化列</param>
        /// <param name="filePath">csv文件物理路径</param>
        /// <param name="msg">输出信息</param>
        /// <param name="startRowIndex">数据导入起始行号</param>
        /// <returns>table的引用</returns>
        public static DataTable ImportCsvToTable(DataTable table, string filePath, out string msg, int startRowIndex = 1)
        {
            //DataTable列的初始化实例：
            //DataTable tblDatas = new DataTable("Datas");
            //DataColumn dc = null;
            //dc = tblDatas.Columns.Add("ID", Type.GetType("System.Int32"));
            //dc.AutoIncrement = true;//自动增加
            //dc.AutoIncrementSeed = 1;//起始为1
            //dc.AutoIncrementStep = 1;//步长为1
            //dc.AllowDBNull = false;//
            //dc = tblDatas.Columns.Add("Product", Type.GetType("System.String"));
            //dc = tblDatas.Columns.Add("Version", Type.GetType("System.String"));
            //dc = tblDatas.Columns.Add("Description", Type.GetType("System.String"));
            try
            {
                using (StreamReader reader = new StreamReader(filePath, Encoding.UTF8, false))
                {
                    int j = 0;
                    while (reader.Peek() > -1)
                    {
                        j = j + 1;
                        string _line = reader.ReadLine();
                        if (j >= startRowIndex + 1)
                        {
                            string[] _dataArray = _line.Split(',');
                            DataRow _dataRow = table.NewRow();
                            for (int k = 0; k < table.Columns.Count; k++)
                            {
                                _dataRow[k] = _dataArray[k];
                            }
                            table.Rows.Add(_dataRow);
                        }
                    }
                    msg = "导入CSV文件到DataTable成功";
                    return table;
                }
            }
            catch (Exception ex)
            {
                msg = string.Format("导入CSV文件到DataTable失败，原因：{0}", ex.Message);
                return null;
            }
        }
        #endregion
    }
}
