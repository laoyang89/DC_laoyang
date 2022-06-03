using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.Model
{
    /// <summary>
    /// ScanCodeData:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class tb_ScanCodeData
    {
        public tb_ScanCodeData()
        { }
        #region Model
        private long _systemAutoID;
        private int _carID;
        private DateTime _scanTime;
        private string _cellCode;
        private string _plcCellCode;
        private string _reasone;
        private string _codeStatus;
        private string _mesStatus;
        private DateTime? _createTime;
        private DateTime? _updateTime;
      
        /// <summary>
        /// 
        /// </summary>
        public long SystemAutoID
        {
            set { _systemAutoID = value; }
            get { return _systemAutoID; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int CarID
        {
            set { _carID = value; }
            get { return _carID; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime ScanTime
        {
            set { _scanTime = value; }
            get { return _scanTime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CellCode
        {
            set { _cellCode = value; }
            get { return _cellCode; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string PLCCellCode
        {
            set { _plcCellCode = value; }
            get { return _plcCellCode; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Reason
        {
            set { _reasone = value; }
            get { return _reasone; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string CodeStatus
        {
            set { _codeStatus = value; }
            get { return _codeStatus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MESStatus
        {
            set { _mesStatus = value; }
            get { return _mesStatus; }
        }
        ///// <summary>
        ///// 
        ///// </summary>
        //public DateTime? CreateTime
        //{
        //    set { _createTime = value; }
        //    get { return _createTime; }
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        //public DateTime? UpdateTime
        //{
        //    set { _updateTime = value; }
        //    get { return _updateTime; }
        //}
        #endregion Model
    }
}
