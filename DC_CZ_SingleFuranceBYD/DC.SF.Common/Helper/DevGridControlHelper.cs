using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;

namespace DC.SF.Common
{
    /// <summary>
    /// Dev GridControl控件辅助类
    /// </summary>
    public class DevGridControlHelper //Add by Lavende Shi 20191219
    {
        /// <summary>
        /// 动态绑定GridView的列
        /// </summary>
        /// <param name="t">数据源类型</param>
        /// <param name="gv">表格数据视图</param>
        /// <param name="horzAlignment">文本水平对齐方式</param>
        /// <param name="isColumnAutoWidth">是否自动匹配列宽</param>
        public static void BingGridColumn(Type t, GridView gv, DevExpress.Utils.HorzAlignment horzAlignment = DevExpress.Utils.HorzAlignment.Default, bool isColumnAutoWidth = true)
        {
            gv.Columns.Clear();
            var props = t.GetProperties().Where(prop => prop.GetCustomAttributes(typeof(DisplayNameAttribute), false).Any());          
            gv.OptionsView.ColumnAutoWidth = isColumnAutoWidth;
            foreach (var prop in props)
            {
                DisplayNameAttribute attrib = (DisplayNameAttribute)prop.GetCustomAttributes(typeof(DisplayNameAttribute), false).FirstOrDefault();
                GridColumn gc = new GridColumn();
                gc.Name = attrib.DisplayName;
                gc.Caption = attrib.DisplayName;
                gc.FieldName = prop.Name;
                gc.Visible = true;
                if (horzAlignment != DevExpress.Utils.HorzAlignment.Default)
                {
                    gc.AppearanceCell.Options.UseTextOptions = true;
                    gc.AppearanceCell.TextOptions.HAlignment = horzAlignment;
                }
                //也可直接定义整张表的文本对齐方式 如下
                //gv.Appearance.Row.Options.UseTextOptions = true;
                //gv.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                gv.Columns.Add(gc);
            }
            gv.BestFitColumns();
        }

        /// <summary>
        /// GridControl 导出CSV报表
        /// </summary>
        /// <param name="gridControl">GridControl实例</param>
        /// <param name="path">文件路径</param>
        /// <param name="msg">输出信息</param>
        /// <returns></returns>
        public static bool GridControlExportToCsv(GridControl gridControl, string path, out string msg)
        {
            string dir = Path.GetDirectoryName(path);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            try
            {
                gridControl.ExportToCsv(path);
                msg = "CSV文件导出成功";
                return true;
            }
            catch (Exception ex)
            {
                msg = string.Format("CSV文件导出失败，原因：{0}", ex.Message + Environment.NewLine + ex.StackTrace);
                return false;
            }         
        }

        /// <summary>
        /// 将GridView数据转成DataTable
        /// </summary>
        /// <param name="gv">GridView 实例</param>
        /// <returns></returns>
        public static DataTable GetGridViewToTable(GridView gv)
        {
            DataTable dt = new DataTable();
            // 列强制转换
            for (int count = 0; count < gv.Columns.Count; count++)
            {
                DataColumn dc = new DataColumn(gv.Columns[count].Name.ToString());
                dt.Columns.Add(dc);
            }
            // 循环行
            for (int count = 0; count < gv.DataRowCount; count++)
            {
                DataRow dr = dt.NewRow();
                for (int countsub = 0; countsub < gv.Columns.Count; countsub++)
                {
                    GridColumn gc = gv.Columns[countsub];
                    dr[countsub] = Convert.ToString(gv.GetRowCellValue(count, gc));
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
    }
}
