using DC.SF.Common;
using DevExpress.XtraGrid.Columns;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace DC.SF.UI.ExtraForm
{
    public partial class FrmCopySystemFile : DevExpress.XtraEditors.XtraForm
    {
        public FrmCopySystemFile()
        {
            InitializeComponent();
        }
        public string fileName;
        public List<CopyUI> listCopy = new List<CopyUI>();
        private void FrmCopySystemFile_Load(object sender, EventArgs e)
        {
            Init(fileName);
            InitView();
        }
        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="fileName"></param>
        public void Init(string fileName)
        {
            string path = @"D:\MIB\SysBak\";
            DirectoryInfo root = new DirectoryInfo(path);
            DirectoryInfo[] dics = root.GetDirectories().Where(m => m.CreationTime > DateTime.Now.AddDays(-1)).ToArray();
            foreach (DirectoryInfo item in dics)
            {
                FileInfo[] files = item.GetFiles();
                foreach (FileInfo info in files)
                {
                    if (info.Name == fileName)
                    {
                        CopyUI copy = new CopyUI();
                        copy.FName = DateTime.Parse(item.Name);
                        copy.Name = info.Name;
                        copy.Path = info.FullName;
                        copy.Size = System.Math.Ceiling(info.Length / 1024.0) + " KB";
                        listCopy.Add(copy);
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// 初始化视图
        /// </summary>
        public void InitView()
        {
            gridControl1.Parent = this;
            gridControl1.Dock = DockStyle.Fill;

            gridControl1.DataSource = listCopy;

            GridColumn colGchecked = gridView1.Columns["Gchecked"];

            GridColumn colFName = gridView1.Columns["FName"];

            GridColumn colName = gridView1.Columns["Name"];
            GridColumn colPath = gridView1.Columns["Path"];
            GridColumn colSize = gridView1.Columns["Size"];
            colGchecked.Caption = "选项";
            colFName.Caption = "时间节点";
            colName.Caption = "文件名";
            colPath.Caption = "路径";
            colSize.Caption = "大小";

            colFName.SortIndex = 0;
            colFName.SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
            colFName.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
        }
        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //此处4是单选框所在的列号
            if (e.Column.ColumnHandle != 0)
            {
                return;
            }
            listCopy.ForEach(m => m.Gchecked = false);

            gridControl1.RefreshDataSource();//正常刷新数据
        }
        public class CopyUI
        {
            public bool Gchecked { get; set; }
            public DateTime FName { get; set; }
            public string Name { get; set; }
            public string Path { get; set; }
            public string Size { get; set; }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string savePath = AppDomain.CurrentDomain.BaseDirectory + @"Settings\"+ fileName;
            CopyUI copy = listCopy.FirstOrDefault(m => m.Gchecked);
            if (File.Exists(copy.Path))
            {
                File.Copy(copy.Path, savePath, true);//三个参数分别是源文件路径，存储路径，若存储路径有相同文件是否替换
            }
            else
            {
                MessageBox.Show("复制失败","错误");
                Environment.Exit(0);
            }
            this.DialogResult = DialogResult.OK;
            //Application.ExitThread();
            //Thread thtmp = new Thread(new ParameterizedThreadStart(run));
            //object appName = Application.ExecutablePath;
            //Thread.Sleep(1);
            //thtmp.Start(appName);
        }
        private void run(Object obj)
        {
            Process ps = new Process();
            ps.StartInfo.FileName = obj.ToString();
            ps.Start();

        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
