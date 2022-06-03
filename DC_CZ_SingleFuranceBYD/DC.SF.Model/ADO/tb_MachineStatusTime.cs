using System;
namespace DC.SF.Model
{
    /// <summary>
    /// tb_MachineStatusTime:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class tb_MachineStatusTime
    {
        public tb_MachineStatusTime()
        { }
        #region Model
        private int _id;
        private DateTime? _recorddate;
        private string _classtype;
        private int? _waitcometime;
        private int? _waitouttime;
        private int? _autotime;
        private int? _handletime;
        private int? _warntime;
        private int? _loadcount;
        private int? _unloadcount;
        private DateTime? _savetime;
        private int? _waitComeTime2;
        private int? _waitOutTime2;
        private int? _LoadCount2;
        private int? _unLoadCount2;
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? RecordDate
        {
            set { _recorddate = value; }
            get { return _recorddate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ClassType
        {
            set { _classtype = value; }
            get { return _classtype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? WaitComeTime
        {
            set { _waitcometime = value; }
            get { return _waitcometime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? WaitOutTime
        {
            set { _waitouttime = value; }
            get { return _waitouttime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? AutoTime
        {
            set { _autotime = value; }
            get { return _autotime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? HandleTime
        {
            set { _handletime = value; }
            get { return _handletime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? WarnTime
        {
            set { _warntime = value; }
            get { return _warntime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? LoadCount
        {
            set { _loadcount = value; }
            get { return _loadcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? UnLoadCount
        {
            set { _unloadcount = value; }
            get { return _unloadcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? SaveTime
        {
            set { _savetime = value; }
            get { return _savetime; }
        }

        public int? WaitComeTime2
        {
            get
            {
                return _waitComeTime2;
            }

            set
            {
                _waitComeTime2 = value;
            }
        }

        public int? WaitOutTime2
        {
            get
            {
                return _waitOutTime2;
            }

            set
            {
                _waitOutTime2 = value;
            }
        }

        public int? LoadCount2
        {
            get
            {
                return _LoadCount2;
            }

            set
            {
                _LoadCount2 = value;
            }
        }

        public int? UnLoadCount2
        {
            get
            {
                return _unLoadCount2;
            }

            set
            {
                _unLoadCount2 = value;
            }
        }
        private int? _totalPower = 0;
        /// <summary>
        /// 总电量
        /// </summary>
        public int? TotalPower
        {
            get
            {
                return _totalPower;
            }

            set
            {
                _totalPower = value;
            }
        }
        #endregion Model

    }
}

