using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.DataLibrary
{
    /// <summary>
    /// Mes设置类
    /// </summary>
    [Serializable]
    public class MesSettings
    {
        private string mesusername = "DC";
        /// <summary>
        /// MES 用户名称
        /// </summary>
        [DisplayName("MES用户名称")]
        public string MesUserName
        {
            get
            {
                return mesusername;
            }
            set
            {
                mesusername = value;
            }
        }
        private string mesuserpassword = "123";
        /// <summary>
        /// MES 用户密码
        /// </summary>
        [DisplayName("MES用户密码")]
        public string MesUserPassword
        {
            get
            {
                return mesuserpassword;
            }
            set
            {
                mesuserpassword = value;
            }
        }
        private string bydmesequipmentid = "DC123";
        /// <summary>
        /// 设备编号
        /// </summary>
        [DisplayName("设备编号")]
        public string BYDMesEquipmentID
        {
            get
            {
                return bydmesequipmentid;
            }
            set
            {
                bydmesequipmentid = value;
            }
        }

        private string _bydmesactionid = "";
        /// <summary>
        /// BYD 状态ID 留空未用
        /// 
        /// </summary>
        [DisplayName("状态ID")]
        public string BYDMESActionID
        {
            get
            {
                return _bydmesactionid;
            }
            set
            {
                _bydmesactionid = value;
            }
        }
        private string bydmeschecktype = "";
        /// <summary>
        /// BYD 检测类型ID
        /// </summary>
        [DisplayName("检测类型ID")]
        public string BYDMESCheckType
        {
            get
            {
                return bydmeschecktype;
            }
            set
            {
                bydmeschecktype = value;
            }
        }
        private string bydmesterminalid = "";
        /// <summary>
        /// BYD 工站ID
        /// </summary>
        [DisplayName("工站ID")]
        public string BYDMesTerminalID
        {
            get
            {
                return bydmesterminalid;
            }
            set
            {
                bydmesterminalid = value;
            }
        }
        
        private string _bydmesaddress = "http://10.62.170.42:9096";
        /// <summary>
        /// BYD MES WCF地址
        /// 
        /// </summary>
        [DisplayName("WCFIP地址")]
        public string BYDMESAddress
        {
            get
            {
                return _bydmesaddress;
            }
            set
            {
                _bydmesaddress = value;
            }
        }
    }
}
