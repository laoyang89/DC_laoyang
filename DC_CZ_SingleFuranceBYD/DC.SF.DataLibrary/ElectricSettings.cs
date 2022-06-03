using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.DataLibrary
{
    /// <summary>
    /// 电气设置类
    /// </summary>
    [Serializable]
    public class ElectricSettings
    {
        public ElectricSettings()
        {

        }

        private int settingTempValue = 100;
        private int firstPumpVacuum = 10;
        private int totalProcessTime = 360;
        private int preheatTimeOut = 40;
        private int vacuumValve = 200;
        private int vacuumDownValue = 50;
        private int vacuumUpValue = 30;
        private int nitrogenPressure = 10000;
        private int overHeatWarning = 103;
        private int overHeatOutage = 105;
        private int lowTempWarning = 95;

        /// <summary>
        /// 设定工艺温度
        /// </summary>
        [DisplayName("设定工艺温度")]
        public int SettingTempValue
        {
            get
            {
                return settingTempValue;
            }
            set
            {
                settingTempValue = value;
            }
        }
        /// <summary>
        /// 第一次抽真空时间
        /// </summary>
        [DisplayName("第一次抽真空时间")]
        public int FirstPumpVacuum
        {
            get
            {
                return firstPumpVacuum;
            }
            set
            {
                firstPumpVacuum = value;
            }
        }
        /// <summary>
        /// 总工艺时间
        /// </summary>
        [DisplayName("总工艺时间")]
        public int TotalProcessTime
        {
            get
            {
                return totalProcessTime;
            }
            set
            {
                totalProcessTime = value;
            }
        }
        /// <summary>
        /// 预热超时报警时间
        /// </summary>
        [DisplayName("预热超时报警时间")]
        public int PreheatTimeOut
        {
            get
            {
                return preheatTimeOut;
            }
            set
            {
                preheatTimeOut = value;
            }
        }
        /// <summary>
        /// 有效真空阈值
        /// </summary>
        [DisplayName("有效真空阈值")]
        public int VacuumValve
        {
            get
            {
                return vacuumValve;
            }
            set
            {
                vacuumValve = value;
            }
        }
        /// <summary>
        /// 真空度上限
        /// </summary>
        [DisplayName("真空度上限")]
        public int VacuumUpValue
        {
            get
            {
                return vacuumUpValue;
            }
            set
            {
                vacuumUpValue = value;
            }
        }
        /// <summary>
        /// 真空度下限
        /// </summary>
        [DisplayName("真空度下限")]
        public int VacuumDownValue
        {
            get
            {
                return vacuumDownValue;
            }
            set
            {
                vacuumDownValue = value;
            }
        }
        /// <summary>
        /// 氮气置换压力
        /// </summary>
        [DisplayName("氮气置换压力")]
        public int NitrogenPressure
        {
            get
            {
                return nitrogenPressure;
            }
            set
            {
                nitrogenPressure = value;
            }
        }
        /// <summary>
        /// 超温预警值
        /// </summary>
        [DisplayName("超温预警值")]
        public int OverHeatWarning
        {
            get
            {
                return overHeatWarning;
            }
            set
            {
                overHeatWarning = value;
            }
        }
        /// <summary>
        /// 超温断电值
        /// </summary>
        [DisplayName("超温断电值")]
        public int OverHeatOutage
        {
            get
            {
                return overHeatOutage;
            }
            set
            {
                overHeatOutage = value;
            }
        }
        /// <summary>
        /// 低温报警值
        /// </summary>
        [DisplayName("低温报警值")]
        public int LowTempWarning
        {
            get
            {
                return lowTempWarning;
            }
            set
            {
                lowTempWarning = value;
            }
        }



    }
}
