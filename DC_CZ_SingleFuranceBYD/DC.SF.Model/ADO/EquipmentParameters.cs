using System;
using System.ComponentModel;

namespace DC.SF.Model
{
	/// <summary>
	/// EquipmentParameters:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class EquipmentParameters
	{
		public EquipmentParameters()
		{}
		#region Model
		private long _systemautoid;
		private string _eno;
		private string _workstationno;
		private int? _layernum;
		private decimal _temperaturesv;
		private decimal? _temperaturecontrol;
		private decimal _temperaturepv1;
		private decimal _temperaturepv2;
		private decimal? _temperaturepv3;
		private decimal _vaclimitsmax;
		private decimal _vaclimitsmin;
		private decimal _vacpv;
		private DateTime? _samplingtime;
		private long _carsystemid;
		private string _remark;
        private int? _cavityStatus;
        private int? _productionStatus;
        private int? _saveTempInterval;
        /// <summary>
        /// 
        /// </summary>
        public long SystemAutoID
		{
			set{ _systemautoid=value;}
			get{return _systemautoid;}
		}
        /// <summary>
        /// 设备编号
        /// </summary>
        [DisplayName("设备编号")]
        public string Eno
		{
			set{ _eno=value;}
			get{return _eno;}
		}
        /// <summary>
        /// 工位编号
        /// </summary>
        [DisplayName("工位编号")]
        public string WorkStationNo
		{
			set{ _workstationno=value;}
			get{return _workstationno;}
		}
        /// <summary>
        /// 层板号
        /// </summary>
        [DisplayName("层板号")]
        public int? LayerNum
		{
			set{ _layernum=value;}
			get{return _layernum;}
		}
        /// <summary>
        /// 温度设定值
        /// </summary>
        [DisplayName("温度设定值")]
        public decimal TemperatureSV
		{
			set{ _temperaturesv=value;}
			get{return _temperaturesv;}
		}
        /// <summary>
        /// 主控温度
        /// </summary>
        [DisplayName("主控温度")]
        public decimal? TemperatureControl
		{
			set{ _temperaturecontrol=value;}
			get{return _temperaturecontrol;}
		}
        /// <summary>
        /// 巡检温度1
        /// </summary>
        [DisplayName("巡检温度1")]
        public decimal TemperaturePV1
		{
			set{ _temperaturepv1=value;}
			get{return _temperaturepv1;}
		}
        /// <summary>
        /// 巡检温度2
        /// </summary>
        [DisplayName("巡检温度2")]
        public decimal TemperaturePV2
		{
			set{ _temperaturepv2=value;}
			get{return _temperaturepv2;}
		}
        /// <summary>
        /// 巡检温度3
        /// </summary>
        [DisplayName("巡检温度3")]
        public decimal? TemperaturePV3
		{
			set{ _temperaturepv3=value;}
			get{return _temperaturepv3;}
		}
        /// <summary>
        /// 设定真空上限
        /// </summary>
        [DisplayName("设定真空上限")]
        public decimal VacLimitsMax
		{
			set{ _vaclimitsmax=value;}
			get{return _vaclimitsmax;}
		}
        /// <summary>
        /// 设定真空下限
        /// </summary>
        [DisplayName("设定真空下限")]
        public decimal VacLimitsMin
		{
			set{ _vaclimitsmin=value;}
			get{return _vaclimitsmin;}
		}
        /// <summary>
        /// 实测真空度
        /// </summary>
        [DisplayName("实测真空度")]
        public decimal VacPV
		{
			set{ _vacpv=value;}
			get{return _vacpv;}
		}
        /// <summary>
        /// 采集时间
        /// </summary>
        [DisplayName("采集时间")]
        public DateTime? SamplingTime
		{
			set{ _samplingtime=value;}
			get{return _samplingtime;}
		}
        /// <summary>
        /// 工艺ID
        /// </summary>
        [DisplayName("工艺ID")]
        public long CarSystemID
		{
			set{ _carsystemid=value;}
			get{return _carsystemid;}
		}
        /// <summary>
        /// 工艺状态
        /// </summary>
        [DisplayName("工艺状态")]
        public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
        /// <summary>
        /// 腔体状态ID
        /// </summary>
        [DisplayName("腔体状态ID")]
        public int? CavityStatus
        {
            set { _cavityStatus = value; }
            get { return _cavityStatus; }
        }
        /// <summary>
        /// 工艺状态ID
        /// </summary>
        [DisplayName("工艺状态ID")]
        public int? ProductionStatus
        {
            set { _productionStatus = value; }
            get { return _productionStatus; }
        }
        /// <summary>
        /// 保存温度时间间隔
        /// </summary>
        [DisplayName("保存温度时间间隔")]
        public int? SaveTempInterval
        {
            set { _saveTempInterval = value; }
            get { return _saveTempInterval; }
        }

        #endregion Model

    }
}

