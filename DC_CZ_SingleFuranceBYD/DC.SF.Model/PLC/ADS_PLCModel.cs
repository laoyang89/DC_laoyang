using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.Model
{
    public enum ADSVarType
    {
        Common = 0,
        ArrayWorkPosition,
        ArrayCell
    }

    /// <summary>
    /// 用于倍福PLC中数组的model实体
    /// </summary>
    [Serializable]
    public class ADS_PLCModel
    {
        private string _modelKey;
        private int _modelHandle;
        private Type _modelType;
        private string _modelName;
        private int _modelLength;
        private object _readValue;
        private object _writeValue;

        private object val;
        private object lockVal = new object();//锁 add by liaosc
        public bool WriteFlag { get; set; }
        public ADSVarType VarType { get; set; }

        public ADS_PLCModel()
        {
            _readValue = new object();
            _writeValue = new object();
        }


        public int ModelHandle
        {
            get
            {
                return _modelHandle;
            }

            set
            {
                _modelHandle = value;
            }
        }

        public Type ModelType
        {
            get
            {
                return _modelType;
            }

            set
            {
                _modelType = value;
            }
        }

        public string ModelName
        {
            get
            {
                return _modelName;
            }

            set
            {
                _modelName = value;
            }
        }

        public int ModelLength
        {
            get
            {
                return _modelLength;
            }

            set
            {
                _modelLength = value;
            }
        }

        public object ReadValue
        {
            get
            {
                return _readValue;
            }

            set
            {
                _readValue = value;
            }
        }

        public object WriteValue
        {
            get
            {
                return _writeValue;
            }

            set
            {
                _writeValue = value;
            }
        }

        /// <summary>
        /// 一个倍福PLCModel中的唯一标识
        /// </summary>
        public string ModelKey
        {
            get
            {
                return _modelKey;
            }

            set
            {
                _modelKey = value;
            }
        }
    }
}
