using System;
namespace DC.SF.Model
{
	/// <summary>
	/// tb_VacuumDegree:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class tb_VacuumDegree
	{
		public tb_VacuumDegree()
		{}
		#region Model
		private int _id;
		private DateTime _recordtime;
		private int _stationnum;
		private decimal _vacuumvalue;
		private int _carrierid;
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
		public DateTime RecordTime
		{
			set{ _recordtime=value;}
			get{return _recordtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int StationNum
		{
			set{ _stationnum=value;}
			get{return _stationnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal VacuumValue
		{
			set{ _vacuumvalue=value;}
			get{return _vacuumvalue;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int CarrierId
		{
			set{ _carrierid=value;}
			get{return _carrierid;}
		}
		#endregion Model

	}
}

