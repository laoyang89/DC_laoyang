using System;
namespace DC.SF.Model
{
	/// <summary>
	/// tb_AlarmRecord:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class tb_AlarmRecord
	{
		public tb_AlarmRecord()
		{}
		#region Model
		private int _id;
		private DateTime? _starttime;
		private DateTime? _endtime;
		private int? _alarmrank;
		private string _alarmcontent;
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
		public DateTime? StartTime
		{
			set{ _starttime=value;}
			get{return _starttime;}
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
		public int? AlarmRank
		{
			set{ _alarmrank=value;}
			get{return _alarmrank;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AlarmContent
		{
			set{ _alarmcontent=value;}
			get{return _alarmcontent;}
		}
		#endregion Model

	}
}

