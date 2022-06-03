using System;
namespace DC.SF.Model
{
	/// <summary>
	/// AlarmRecord:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class AlarmRecord
	{
		public AlarmRecord()
		{}
		#region Model
		private long _systemautoid;
		private string _eno;
		private string _status;
		private string _alarmcode;
		private DateTime _stime;
        private DateTime? _etime;
        private string _remark;
        private string _alarmDesc;
        private long _carSystemID;
        /// <summary>
        /// 
        /// </summary>
        public long SystemAutoID
		{
			set{ _systemautoid=value;}
			get{return _systemautoid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Eno
		{
			set{ _eno=value;}
			get{return _eno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AlarmCode
		{
			set{ _alarmcode=value;}
			get{return _alarmcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime Stime
		{
			set{ _stime=value;}
			get{return _stime;}
		}
        /// <summary>
        /// 
        /// </summary>
        public DateTime? Etime
        {
            set { _etime = value; }
            get { return _etime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
        /// <summary>
        /// 
        /// </summary>
        public string AlarmDesc
        {
            get {
                if (!string.IsNullOrWhiteSpace(_alarmcode))
                {
                    _alarmDesc = "E" + _alarmcode.PadLeft(4, '0');
                }
                return _alarmDesc;
            }

        }
        /// <summary>
		/// 
		/// </summary>
		public long CarSystemID
        {
            set { _carSystemID = value; }
            get { return _carSystemID; }
        }
  
        #endregion Model

    }
}

