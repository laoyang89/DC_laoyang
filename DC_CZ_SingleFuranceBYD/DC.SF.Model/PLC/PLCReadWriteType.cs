using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.Model
{
    /// <summary>
    /// 读取写入类型枚举
    /// </summary>
    public enum EnumRWType
    {
        [Description("Int32")]
        Int32=1,

        [Description("Int16")]
        Int16,

        [Description("String")]
        String,

        [Description("Float")]
        Float,

        [Description("Double")]
        Double,

        [Description("Boolen")]
        Boolen,

        [Description("Byte")]
        Byte
    }

    /// <summary>
    /// 读取写入数组类型枚举
    /// </summary>
    public enum EnumRWArrType
    {
        [Description("ArrayInt32")]
        ArrayInt32 =1,

        [Description("ArrayInt16")]
        ArrayInt16,

        [Description("ArrayFloat")]
        ArrayFloat,

        [Description("ArrayDouble")]
        ArrayDouble,

        [Description("ArrayBoolen")]
        ArrayBoolen,

        [Description("ArrayByte")]
        ArrayByte
    }
}
