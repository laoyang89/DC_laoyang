using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.Model
{
    [Serializable]
    /// <summary>
    /// 报警规则
    /// </summary>
    public class AlarmRuleCache
    {
        private long _recordDBID;
        private string _alarmRuleId;
        private int _alarmCode;
        private string _alarmContent;
        private string _alarmStationNum;
        private DateTime _alarmTime;

        /// <summary>
        /// 报警规则Id
        /// </summary>
        public string AlarmRuleId
        {
            get
            {
                return _alarmRuleId;
            }

            set
            {
                _alarmRuleId = value;
            }
        }
      
        /// <summary>
        /// 报警Code
        /// </summary>
        public int AlarmCode
        {
            get
            {
                return _alarmCode;
            }

            set
            {
                _alarmCode = value;
            }
        }

        /// <summary>
        /// 报警内容
        /// </summary>
        public string AlarmContent
        {
            get
            {
                return _alarmContent;
            }

            set
            {
                _alarmContent = value;
            }
        }

        /// <summary>
        /// 报警工位
        /// </summary>
        public string AlarmStationNum
        {
            get
            {
                return _alarmStationNum;
            }

            set
            {
                _alarmStationNum = value;
            }
        }

        /// <summary>
        /// 报警时间
        /// </summary>
        public DateTime AlarmTime
        {
            get
            {
                return _alarmTime;
            }

            set
            {
                _alarmTime = value;
            }
        }
       
        /// <summary>
        /// 对应数据库报警ID
        /// </summary>
        public long RecordDBID
        {
            get
            {
                return _recordDBID;
            }

            set
            {
                _recordDBID = value;
            }
        }
    }
}
