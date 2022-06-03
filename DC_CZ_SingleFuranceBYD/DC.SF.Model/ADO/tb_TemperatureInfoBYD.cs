using System;
namespace DC.SF.Model
{
	/// <summary>
	/// tb_TemperatureInfoBYD:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class tb_TemperatureInfoBYD
	{
		public tb_TemperatureInfoBYD()
		{}
		#region Model
		private int _id;
		private DateTime? _recordtime;
		private decimal? _temperature1;
		private decimal? _temperature2;
		private decimal? _temperature3;
		private decimal? _temperature4;
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
		public decimal? Temperature1
		{
			set{ _temperature1=value;}
			get{return _temperature1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Temperature2
		{
			set{ _temperature2=value;}
			get{return _temperature2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Temperature3
		{
			set{ _temperature3=value;}
			get{return _temperature3;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Temperature4
		{
			set{ _temperature4=value;}
			get{return _temperature4;}
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

