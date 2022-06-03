using System;
namespace DC.SF.Model
{
	/// <summary>
	/// AlarmRule:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class AlarmRule
	{
		public AlarmRule()
		{}
        /// <summary>
        /// SystemAutoID
        /// </summary>		
        private int _systemautoid;
        public int SystemAutoID
        {
            get { return _systemautoid; }
            set { _systemautoid = value; }
        }
        /// <summary>
        /// PLC报警地址位
        /// </summary>		
        private int _alarmaddress;
        public int AlarmAddress
        {
            get { return _alarmaddress; }
            set { _alarmaddress = value; }
        }
        /// <summary>
        /// PLC报警序号
        /// </summary>		
        private int _alarmindex;
        public int AlarmIndex
        {
            get { return _alarmindex; }
            set { _alarmindex = value; }
        }
        /// <summary>
        /// 报警内容
        /// </summary>		
        private string _alarmcontent;
        public string AlarmContent
        {
            get { return _alarmcontent; }
            set { _alarmcontent = value; }
        }
        /// <summary>
        /// 报警对应腔体序号
        /// </summary>		
        private string _workstationno;
        public string WorkStationNo
        {
            get { return _workstationno; }
            set { _workstationno = value; }
        }

    }
}

