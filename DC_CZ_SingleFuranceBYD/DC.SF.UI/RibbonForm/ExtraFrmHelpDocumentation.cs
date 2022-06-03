using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DC.SF.Common;
using System.IO;
using DC.SF.DataLibrary;

namespace DC.SF.UI
{
    /// <summary>
    /// 帮助文档界面
    /// </summary>
    public partial class ExtraFrmHelpDocumentation : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 私有构造函数
        /// </summary>
        private ExtraFrmHelpDocumentation()
        {
            InitializeComponent();
        }

        private static ExtraFrmHelpDocumentation instnce;
        /// <summary>
        /// 单例模式 一次可以仅可以显示一个该窗体
        /// </summary>
        public static ExtraFrmHelpDocumentation Instnce
        {
            get
            {
                if (instnce == null || instnce.IsDisposed)
                {
                    instnce = new ExtraFrmHelpDocumentation();
                }
                return instnce;
            }

            set
            {
                instnce = value;
            }
        }

        /// <summary>
        /// 界面加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExtraFrmHelpDocumentation_Load(object sender, EventArgs e)
        {
            string path = Path.GetDirectoryName(PathData.PdfHelpDocumentationPath);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (!File.Exists(PathData.PdfHelpDocumentationPath))
            {
                MessageBox.Show("未检测到帮助文档，请联系大成软件工程师索取！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                this.pdfViewer1.DocumentFilePath = PathData.PdfHelpDocumentationPath;
                //this.pdfViewer1.DocumentFilePath = @"C:\Users\mythshenhua\Desktop\ASC真空炉软件现场问题处理说明书.pdf";
            }
            catch (Exception)
            {
                MessageBox.Show("加载帮助文档异常！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }          
        }
    }
}