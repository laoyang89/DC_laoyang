using DC.SF.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.DataLibrary
{
    /// <summary>
    /// 报警信息
    /// </summary>
    public class WarningInfo
    {
        private string[] _LayerTempWarnArr;
        private string _VacuumWarn;
        private string _HeatOverTime;
        private string _DoorControl;
        private string _FanWarn;
        private string _ContractWarn;

        public WarningInfo()
        {
            _LayerTempWarnArr = new string[ConfigData.LayersCount];
        }


        /// <summary>
        /// 层板温度报警信息
        /// </summary>
        public string[] LayerTempWarnArr
        {
            get
            {
                return _LayerTempWarnArr;
            }

            set
            {
                _LayerTempWarnArr = value;
            }
        }

        /// <summary>
        /// 真空异常
        /// </summary>
        public string VacuumWarn
        {
            get
            {
                return _VacuumWarn;
            }

            set
            {
                _VacuumWarn = value;
            }
        }

        /// <summary>
        /// 加热超时
        /// </summary>
        public string HeatOverTime
        {
            get
            {
                return _HeatOverTime;
            }

            set
            {
                _HeatOverTime = value;
            }
        }

        /// <summary>
        /// 门禁报警
        /// </summary>
        public string DoorControl
        {
            get
            {
                return _DoorControl;
            }

            set
            {
                _DoorControl = value;
            }
        }

        /// <summary>
        /// 风扇报警
        /// </summary>
        public string _FanWarn1
        {
            get
            {
                return _FanWarn;
            }

            set
            {
                _FanWarn = value;
            }
        }

        /// <summary>
        /// 辅助触点报警
        /// </summary>
        public string ContractWarn
        {
            get
            {
                return _ContractWarn;
            }

            set
            {
                _ContractWarn = value;
            }
        }
    }
}
