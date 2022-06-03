using System;
namespace DC.SF.Model
{
	/// <summary>
	/// tb_CarrierInfo:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class tb_CarrierInfo
	{
		public tb_CarrierInfo()
		{}
		#region Model
		private int _id;
		private int _produceid;
		private int _carriernum;
		private DateTime? _begintime;
		private DateTime? _endtime;
		private decimal? _vacuumvalue;
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
		public int ProduceId
		{
			set{ _produceid=value;}
			get{return _produceid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int CarrierNum
		{
			set{ _carriernum=value;}
			get{return _carriernum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? BeginTime
		{
			set{ _begintime=value;}
			get{return _begintime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EndTime
		{
			set{ _endtime=value;}
			get{return _endtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? VacuumValue
		{
			set{ _vacuumvalue=value;}
			get{return _vacuumvalue;}
		}
		#endregion Model

	}
}

