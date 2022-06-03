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
using DC.SF.Model;
using System.Reflection;
using System.IO;
using DC.SF.Common;
using System.Xml;

namespace DC.SF.UI
{
    public partial class ChildFrmAbout : DevExpress.XtraEditors.XtraForm //Add by Lavender Shi 20191203
    {
        public ChildFrmAbout()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// 网址
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hllc_Url_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(/*"iexplore.exe",*/ "http://www.dcprecision.cn");
        }

        /// <summary>
        /// 界面加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChildFrmAbout_Load(object sender, EventArgs e)
        {
            Init();
        }

        /// <summary>
        /// 加载初始化函数
        /// </summary>
        private void Init()
        {
            //请在 DC.SF.UI\Document\VersionInfo.xml 文件中修改版本更新记录
            using (Stream sm = Assembly.GetExecutingAssembly().GetManifestResourceStream("DC.SF.UI.Document.VersionInfo.xml"))
            {
                listVersionDetail = SerializerHelper.XmlDeserializer<List<VersionDetail>>(sm);
            }           
            StringBuilder sb = new StringBuilder();
            int termination = listVersionDetail.Count - 10 < 0 ? 0 : listVersionDetail.Count - 10;
            for (int i = listVersionDetail.Count - 1; i >= termination; i--)
            {
                sb.Append(string.Format("版 本 号：{0}\r\n更新人员：{1}\r\n更新日期：{2}\r\n更新日志：\r\n\t{3}\r\n\r\n", listVersionDetail[i].SoftVersion, listVersionDetail[i].UpdatePerson, listVersionDetail[i].UpdateDate, string.Join("\r\n\t", listVersionDetail[i].UpdateLog)));
            }
            rtb_VersionInfo.Text = sb.ToString();
        }

        /// <summary>
        /// 版本详情列表
        /// </summary>
        private List<VersionDetail> listVersionDetail;
    }
}