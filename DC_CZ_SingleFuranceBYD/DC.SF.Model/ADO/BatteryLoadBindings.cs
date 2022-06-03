using DC.SF.Model.Attributes;
using System;
namespace DC.SF.Model
{
	/// <summary>
	/// BatteryLoadBindings:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class BatteryLoadBindings
	{
		public BatteryLoadBindings()
		{}
		#region Model
		private long _systemautoid;
		private string _eno;
		private DateTime? _scantime;
		private string _plotno;
		private int _rankcode;
		private int? _layernum;
		private int? _rownum;
		private int? _columnnum;
		private long _carsystemid;
		private string _remark;
        private string _workstationno;
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
		public DateTime? ScanTime
		{
			set{ _scantime=value;}
			get{return _scantime;}
		}
		/// <summary>
		/// 
		/// </summary>
        [MuchAttribute("CellCode")]
		public string PLotNo
		{
			set{ _plotno=value;}
			get{return _plotno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int RankCode
		{
			set{ _rankcode=value;}
			get{return _rankcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? LayerNum
		{
			set{ _layernum=value;}
			get{return _layernum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? RowNum
		{
			set{ _rownum=value;}
			get{return _rownum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ColumnNum
		{
			set{ _columnnum=value;}
			get{return _columnnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long CarSystemID
		{
			set{ _carsystemid=value;}
			get{return _carsystemid;}
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
        public string WorkStationNo
        {
            set { _workstationno = value; }
            get { return _workstationno; }
        }
        #endregion Model

    }
}

