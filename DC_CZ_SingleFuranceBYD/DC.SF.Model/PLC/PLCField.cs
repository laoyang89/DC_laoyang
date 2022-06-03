using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.Model
{
    /// <summary>
    /// PLC模块字段：每一个模块，又分成一个一个的字段，通过模块Id来进行绑定
    /// </summary>
    [Serializable]
    public class PLCField
    {

        #region 字段
        public static int identityId = 0;
        private string _FieldName;
        private int _FieldId;
        private int _FStartPosition;
        private int _FLength;
        private EnumRWType _rwType;
        private object _rwValue;
        private string _UnitStr;
        private string _Key;

        #endregion

        public PLCField()
        {
            identityId++;
            _FieldId = identityId;
            RwType = EnumRWType.Int16;
        }

        /// <summary>
        /// 字段名字
        /// </summary>
        public string FieldName
        {
            get
            {
                return _FieldName;
            }

            set
            {
                _FieldName = value;
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
        public int FStartPosition
        {
            get
            {
                return _FStartPosition;
            }

            set
            {
                _FStartPosition = value;
            }
        }

        /// <summary>
        /// 长度
        /// </summary>
        public int FLength
        {
            get
            {
                return _FLength;
            }

            set
            {
                _FLength = value;
            }
        }

        /// <summary>
        /// 读写数据类型
        /// </summary>
        public EnumRWType RwType
        {
            get
            {
                return _rwType;
            }

            set
            {
                _rwType = value;
            }
        }

        /// <summary>
        /// 数据值
        /// </summary>
        public object RwValue
        {
            get
            {
                return _rwValue;
            }

            set
            {
                _rwValue = value;
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
