using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace DC.SF.Model
{
    /// <summary>
    /// 模块类型
    /// </summary>
    public enum PLCModelType
    {
        /// <summary>
        /// 命令型   给PLC发操作命令
        /// </summary>
        [Description("命令型模块")]
        OperateOrder =1,


        /// <summary>
        /// 参数型  设置PLC参数
        /// </summary>
        [Description("参数型模块")]
        Parameter,


        /// <summary>
        /// 信息型，需要循环读取
        /// </summary>
        [Description("消息型模块")]
        Information,


        /// <summary>
        /// 报警型 ，需要循环读取
        /// </summary>
        [Description("报警型模块")]
        WarnInfo
    }

    /// <summary>
    /// 按照PLC的功能，将其分成一个一个的模块
    /// </summary>
    [Serializable]
    public class PLCModel
    {
        private static int identityId = 0;
        #region 字段
        private int _ModelId;
        private string _ModelName;
        private int _StartPosition;
        private int _Length;
        private string _PartNum;
        private PLCModelType _modelType;
        private bool _IsCircleRead;
        private List<PLCField> _lstField;
        private List<PLCFieldArray> _lstArrField;      
        #endregion        

        /// <summary>
        /// 当所有字段读取到之后，调用委托进行相应的处理
        /// </summary>
        //public DgFieldDeal _dgFieldDeal;

        #region 属性

        /// <summary>
        /// 构造函数
        /// </summary>
        public PLCModel()
        {
            _StartPosition = 0;
            _modelType = PLCModelType.Information;
            _IsCircleRead = true;
            identityId++;
            _ModelId = identityId;
            _lstField = new List<PLCField>();
            _lstArrField = new List<PLCFieldArray>();
        }

        /// <summary>
        /// 模块名字
        /// </summary>
        public string ModelName
        {
            get
            {
                return _ModelName;
            }
            set
            {
                _ModelName = value;
            }
        }

        /// <summary>
        /// 在PLC中该模块的起始位置
        /// </summary>
        public int StartPosition
        {
            get
            {
                return _StartPosition;
            }
            set
            {
                _StartPosition = value;
            }
        }

        /// <summary>
        /// 该模块在PLC中所属的区号，如“D”区，“M”区等
        /// </summary>
        public string PartNum
        {
            get
            {
                return _PartNum;
            }
            set
            {
                _PartNum = value;
            }
        }

        /// <summary>
        /// 模块占用长度
        /// </summary>
        public int Length
        {
            get
            {
                return _Length;
            }
            set
            {
                _Length = value;
            }
        }

        /// <summary>
        /// PLC模块类型
        /// </summary>
        public PLCModelType ModelType
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

        /// <summary>
        /// 是否需要循环读取
        /// </summary>
        public bool IsCircleRead
        {
            get
            {
                return _IsCircleRead;
            }

            set
            {
                _IsCircleRead = value;
            }
        }
        /// <summary>
        /// 模型ID
        /// </summary>
        public int ModelId
        {
            get
            {
                return _ModelId;
            }
        }

        /// <summary>
        /// PLC字段集合
        /// </summary>
        public List<PLCField> LstField
        {
            get
            {
                return _lstField;
            }

            set
            {
                _lstField = value;
            }
        }

        /// <summary>
        /// 为了将来读取的时候可以省事，将字段进行分类并且按照数组的方式存进字典里
        /// </summary>
        public List<PLCFieldArray> LstArrField
        {
            get
            {
                return _lstArrField;
            }

            set
            {
                _lstArrField = value;
            }
        }

        #endregion
    }
}
