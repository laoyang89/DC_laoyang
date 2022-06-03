using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.Model
{
    /// <summary>
    /// tb_OperateRecord:实体类(操作记录实体)
    /// </summary>
    [Serializable]
    public partial class tb_OperateRecord
    {
        public tb_OperateRecord()
        { }
        #region Model
        private int _id;
        private DateTime? _recordtime;
        private string _employeeName;
        private string _operatecontent;
        private string _employeeid;
        private string _remark;
        /// <summary>
        /// 备注(不同用途，保存不同信息)
        /// </summary>
        public string Remark
        {
            get
            {
                return _remark;
            }
            set
            {
                _remark = value;
            }
        }
        /// <summary>
        /// 自增ID（主键）
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 用户IDCard
        /// </summary>
        [DisplayName("用户ID卡号")]
        public string EmployeeID
        {
            set { _employeeid = value; }
            get { return _employeeid; }
        }
        /// <summary>
        /// 用户ID
        /// </summary>
        [DisplayName("用户名称")]
        public string EmployeeName
        {
            set { _employeeName = value; }
            get { return _employeeName; }
        }
        /// <summary>
        /// 记录时间
        /// </summary>
        [DisplayName("记录时间")]
        public DateTime? RecordTime
        {
            set { _recordtime = value; }
            get { return _recordtime; }
        }
       
        /// <summary>
        /// 操作内容
        /// </summary>
        [DisplayName("操作内容")]
        public string OperateContent
        {
            set { _operatecontent = value; }
            get { return _operatecontent; }
        }
        
        #endregion Model

    }
}
