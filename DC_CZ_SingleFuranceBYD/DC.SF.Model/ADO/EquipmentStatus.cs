using System;
namespace DC.SF.Model
{
	/// <summary>
	/// EquipmentStatus:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class EquipmentStatus
	{
		public EquipmentStatus()
		{}
		#region Model
		private long _systemautoid;
		private string _eno;
		private string _startstatus;
        private string _endstatus;
        private DateTime _stime;
        private DateTime? _etime;
		private string _remark;
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
		public string StartStatus
		{
			set{ _startstatus = value;}
			get{return _startstatus; }
		}
        public string EndStatus
        {
            set { _endstatus = value; }
            get { return _endstatus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime Stime
        {
            set { _stime = value; }
            get { return _stime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? Etime
		{
			set{ _etime=value;}
			get{return _etime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		#endregion Model

	}
}

