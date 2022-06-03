using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.Model
{
    /// <summary>
    /// PLCModel模块字段组
    /// </summary>
    [Serializable]
    public class PLCFieldArray
    {
        public static int identityId = 0;
        private string _FieldArrayName;
        private int _FieldId;
        private int _FAStartPosition;
        private int _FALength;
        private EnumRWArrType _rwArrType;
        private object _rwArrValue;
        private string _UnitStr;
        private string _Key;

        public PLCFieldArray()
        {
            identityId++;
            _FieldId = identityId;
            _rwArrType = EnumRWArrType.ArrayInt16;
        }

        /// <summary>
        /// 字段名字
        /// </summary>
        public string FieldArrayName
        {
            get
            {
                return _FieldArrayName;
            }

            set
            {
                _FieldArrayName = value;
            }
        }

        /// <summary>
        /// 键名
        /// </summary>
        public string Key
        {
            get
            {
                return _Key;
            }

            set
            {
                _Key = value;
            }
        }

        /// <summary>
        /// 字段Id
        /// </summary>
        public int FieldId
        {
            get
            {
                return _FieldId;
            }
        }

        /// <summary>
        /// PLC中地址开始位置
        /// </summary>
        public int FAStartPosition
        {
            get
            {
                return _FAStartPosition;
            }

            set
            {
                _FAStartPosition = value;
            }
        }

        /// <summary>
        /// 长度
        /// </summary>
        public int FALength
        {
            get
            {
                return _FALength;
            }

            set
            {
                _FALength = value;
            }
        }

        /// <summary>
        /// 读写数据类型
        /// </summary>
        public EnumRWArrType RwArrType
        {
            get
            {
                return _rwArrType;
            }

            set
            {
                _rwArrType = value;
            }
        }

        /// <summary>
        /// 数据值
        /// </summary>
        public object RwArrValue
        {
            get
            {
                return _rwArrValue;
            }

            set
            {
                _rwArrValue = value;
            }
        }

        /// <summary>
        /// 单位
        /// </summary>
        public string UnitStr
        {
            get
            {
                return _UnitStr;
            }

            set
            {
                _UnitStr = value;
            }
        }

    }
}
