using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.Model
{
    /// <summary>
    /// 参数管控表
    /// </summary>
    public partial class DeviceParam
    {
		private long _systemautoid;
        private string _paramname;
        private string _paramdisplay;
        private int _paramvalue;
        private int _paramminvalue;
        private int _parammaxvalue;
        private string _plcaddress;
        private string _plcdatatype;
        private int _mesuploadparamid;
        private bool _isactived;

        /// <summary>
		/// SystemAutoID
        /// </summary>	
        [DisplayName("SystemAutoID")]
        public long SystemAutoID
        {
            get { return _systemautoid; }
            set { _systemautoid = value; }
        }

        /// <summary>
        /// 参数名称
        /// </summary>
        [DisplayName("参数名称")]
        public string ParamName
        {
            get { return _paramname; }
            set { _paramname = value; }
        }

        /// <summary>
        /// 参数意义
        /// </summary>	
        [DisplayName("参数意义")]
        public string ParamDisplay
        {
            get { return _paramdisplay; }
            set { _paramdisplay = value; }
        }

        /// <summary>
        /// 参数内容
        /// </summary>
        [DisplayName("参数内容")]
        public int ParamValue
        {
            get { return _paramvalue; }
            set { _paramvalue = value; }
        }

        /// <summary>
        /// 参数最小值
        /// </summary>
        [DisplayName("参数最小值")]
        public int ParamMinValue
        {
            get { return _paramminvalue; }
            set { _paramminvalue = value; }
        }

        /// <summary>
        /// 参数最大值
        /// </summary>
        [DisplayName("参数最大值")]
        public int ParamMaxValue
        {
            get { return _parammaxvalue; }
            set { _parammaxvalue = value; }
        }

        /// <summary>
        /// PLC地址位
        /// </summary>
        [DisplayName("PLC地址位")]
        public string PlcAddress
        {
            get { return _plcaddress; }
            set { _plcaddress = value; }
        }

        /// <summary>
        /// PLC数据类型
        /// </summary>
        [DisplayName("PLC数据类型")]
        public string PlcDataType
        {
            get { return _plcdatatype; }
            set { _plcdatatype = value; }
        }

        /// <summary>
        /// MES管控ID
        /// </summary>
        [DisplayName("MES管控ID")]
        public int MesUploadParamID
        {
            get { return _mesuploadparamid; }
            set { _mesuploadparamid = value; }
        }

        /// <summary>
        /// 是否启用
        /// </summary>
        [DisplayName("是否启用")]
        public bool IsActived
        {
            get { return _isactived; }
            set { _isactived = value; }
        }
    }
}
