using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.Model
{
    /// <summary>
    /// 版本详情：用于界面显示的版本记录详细信息
    /// </summary>
    [Serializable]
    public class VersionDetail //Add by Lavender Shi 20190827
    {
        private string _SoftVersion;
        private string _UpdatePerson;
        private string _UpdateDate;
        private List<string> _UpdateLog;

        public VersionDetail()
        {
            _UpdateLog = new List<string>();
        }

        /// <summary>
        /// 软件版本号
        /// </summary>
        public string SoftVersion
        {
            get
            {
                return _SoftVersion;
            }

            set
            {
                _SoftVersion = value;
            }
        }

        /// <summary>
        /// 更新人员
        /// </summary>
        public string UpdatePerson
        {
            get
            {
                return _UpdatePerson;
            }

            set
            {
                _UpdatePerson = value;
            }
        }

        /// <summary>
        /// 更新日期
        /// </summary>
        public string UpdateDate
        {
            get
            {
                return _UpdateDate;
            }

            set
            {
                _UpdateDate = value;
            }
        }

        /// <summary>
        /// 更新日志
        /// </summary>
        public List<string> UpdateLog
        {
            get
            {
                return _UpdateLog;
            }

            set
            {
                _UpdateLog = value;
            }
        }
    }
}
