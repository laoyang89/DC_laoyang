using System;
using System.ComponentModel;

namespace DC.SF.Model
{
	/// <summary>
	/// tb_CellInfo:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class tb_CellInfo
	{
		public tb_CellInfo()
		{}
		#region Model
		private int _id;
		private DateTime _scantime;
		private string _cellcode;
		private int _rankcode;
		private int _layernumber;
		private int _carrierid;
        private int _rownum;
        private int _columnnum;
        /// <summary>
        /// 
        /// </summary>
        [Browsable(false)]
        public int Id
		{
			set{ _id=value;}
			get{return _id;}
		}

        [DisplayName("扫码时间")]
        /// <summary>
        /// 
        /// </summary>
        public DateTime ScanTime
		{
			set{ _scantime=value;}
			get{return _scantime;}
		}

        [DisplayName("条码")]
		/// <summary>
		/// 
		/// </summary>
		public string CellCode
		{
			set{ _cellcode=value;}
			get{return _cellcode;}
		}

        [DisplayName("编码")]
        /// <summary>
        /// 
        /// </summary>
        public int RankCode
		{
			set{ _rankcode=value;}
			get{return _rankcode;}
		}


        /// <summary>
        /// 呵呵
        /// </summary>
        [DisplayName("层号")]
        public int LayerNumber
		{
			set{ _layernumber=value;}
			get{return _layernumber;}
		}

        //[DisplayName("载体编号")]
        /// <summary>
        /// 
        /// </summary>
        public int CarrierId
		{
			set{ _carrierid=value;}
			get{return _carrierid;}
		}

        [DisplayName("排")]
        /// <summary>
        /// 
        /// </summary>
        public int RowNum
        {
            get
            {
                return _rownum;
            }

            set
            {
                _rownum = value;
            }
        }

        [DisplayName("列")]
        /// <summary>
        /// 
        /// </summary>
        public int ColumnNum
        {
            get
            {
                return _columnnum;
            }

            set
            {
                _columnnum = value;
            }
        }
        #endregion Model

    }
}

