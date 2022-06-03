using System;
namespace DC.SF.Model
{
	/// <summary>
	/// CarDistribution:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class CarDistribution
	{
		public CarDistribution()
		{}
		#region Model
		private long _systemautoid;
		private string _eno;
		private string _carno;
		private DateTime? _loadingtime;
		private DateTime? _entertime;
		private DateTime? _outtime;
		private DateTime? _unloadtime;
		private DateTime? _samplingtime;
        private string _carNoDesc;
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
		public string CarNo
		{
			set{ _carno=value;}
			get{return _carno;}
		}
		/// <summary>
		/// 上料完成时间
		/// </summary>
		public DateTime? LoadingTime
		{
			set{ _loadingtime=value;}
			get{return _loadingtime;}
		}
		/// <summary>
		/// 工艺开始时间
		/// </summary>
		public DateTime? EnterTime
		{
			set{ _entertime=value;}
			get{return _entertime;}
		}
		/// <summary>
		/// 工艺结束时间
		/// </summary>
		public DateTime? OutTime
		{
			set{ _outtime=value;}
			get{return _outtime;}
		}
		/// <summary>
		/// 下料完成时间
		/// </summary>
		public DateTime? UnloadTime
		{
			set{ _unloadtime=value;}
			get{return _unloadtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? SamplingTime
		{
			set{ _samplingtime=value;}
			get{return _samplingtime;}
		}
        /// <summary>
        /// 小车号别称，用于BYD MES
        /// </summary>
        public string CarNoDesc
        {
            get {
                if (!string.IsNullOrWhiteSpace(_carno))
                {
                    _carNoDesc = "XC" + _carno.PadLeft(3, '0');
                }
                return _carNoDesc;
            }
        }
        #endregion Model

    }
}

