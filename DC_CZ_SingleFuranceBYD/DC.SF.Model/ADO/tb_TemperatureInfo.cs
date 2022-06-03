using System;
namespace DC.SF.Model
{
	/// <summary>
	/// tb_TemperatureInfo:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class tb_TemperatureInfo
	{
		public tb_TemperatureInfo()
		{}
		#region Model
		private int _id;
		private DateTime? _recordtime;
		private decimal? _controlvalue;
		private decimal? _patrolvalue;
		private int? _layernum;
		private int _carrierid;
		private int? _stationnum;
		/// <summary>
		/// 
		/// </summary>
		public int Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? RecordTime
		{
			set{ _recordtime=value;}
			get{return _recordtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? ControlValue
		{
			set{ _controlvalue=value;}
			get{return _controlvalue;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? PatrolValue
		{
			set{ _patrolvalue=value;}
			get{return _patrolvalue;}
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
		public int CarrierId
		{
			set{ _carrierid=value;}
			get{return _carrierid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? StationNum
		{
			set{ _stationnum=value;}
			get{return _stationnum;}
		}
		#endregion Model

	}
}

