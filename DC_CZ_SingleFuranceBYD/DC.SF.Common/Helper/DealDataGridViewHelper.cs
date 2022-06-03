using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DC.SF.Common
{
    public class DealDataGridViewHelper
    {
        /// <summary>
        /// 动态绑定DataGridView的列
        /// </summary>
        /// <param name="t"></param>
        /// <param name="dgv"></param>
        public static void BingGridColumn(Type t, DataGridView dgv)
        {
            dgv.Columns.Clear();
            var props = t.GetProperties().Where(prop => prop.GetCustomAttributes(typeof(DisplayNameAttribute), false).Any());
            int col_width = dgv.Width / props.Count() - 50;
            foreach (var prop in props)
            {
                DisplayNameAttribute attrib = (DisplayNameAttribute)prop.GetCustomAttributes(typeof(DisplayNameAttribute), false).FirstOrDefault();
                DataGridViewTextBoxColumn dc = new DataGridViewTextBoxColumn();
                dc.Name = prop.Name;
                dc.HeaderText = attrib.DisplayName;
                dc.DataPropertyName = prop.Name;
                dc.Width = col_width;
                dgv.Columns.Add(dc);
            }
        }

        /// <summary>
        /// 将DataGridView数据转成DataTable
        /// </summary>
        /// <param name="dgv"></param>
        /// <returns></returns>
        private DataTable GetDgvToTable(DataGridView dgv)
        {
            DataTable dt = new DataTable();
            // 列强制转换
            for (int count = 0; count < dgv.Columns.Count; count++)
            {
                DataColumn dc = new DataColumn(dgv.Columns[count].Name.ToString());
                dt.Columns.Add(dc);
            }
            // 循环行
            for (int count = 0; count < dgv.Rows.Count; count++)
            {
                DataRow dr = dt.NewRow();
                for (int countsub = 0; countsub < dgv.Columns.Count; countsub++)
                {
                    dr[countsub] = Convert.ToString(dgv.Rows[count].Cells[countsub].Value);
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
    }
}
