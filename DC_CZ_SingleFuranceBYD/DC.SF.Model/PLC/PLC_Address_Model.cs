using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.Model
{
    /// <summary>
    /// 按地址位来读取的PLC模型，如欧姆龙，区别于以前按数组来读写的倍福
    /// </summary>
    public class PLC_Address_Model
    {
        public PLC_Address_Model()
        {
            this.IsActive = true;
            this.ReadValue = default(object);
            this.VariableHandle = 0;
        }

        [DisplayName("工位关键字")]
        /// <summary>
        /// 模型关键字，不可重复，英文名
        /// </summary>
        public string Key { get; set; }

        [DisplayName("工位名称")]
        /// <summary>
        /// 模型名字,中文
        /// </summary>
        public string ModelName { get; set; }

        /// <summary>
        /// 模型所在工位枚举
        /// </summary>
        public BYD_EnumStation BYD_EnumStation { get; set; }

        /// <summary>
        /// 工位类型，流转工位还是真空工位
        /// </summary>
        public EnumStationType EnumStationType { get; set; }

        /// <summary>
        /// 倍福PLC专用
        /// </summary>
        public int VariableHandle { get; set; }

        [DisplayName("PLC所属区")]
        /// <summary>
        /// 如果是欧姆龙三菱这种PLC，就是指某个区，如果是倍福，那就是主结构体
        /// </summary>
        public string Part { get; set; }

        [DisplayName("工位起始地址")]
        /// <summary>
        /// 模型起始位置
        /// </summary>
        public int StartAddress { get; set; }

        /// <summary>
        /// 模型在对应的PLC中，占用short的长度
        /// </summary>
        public int WordLength { get; set; }

        /// <summary>
        /// 模型设计的变量长度，注意跟上面长度的区分
        /// </summary>
        public int ArrLength { get; set; }


        /// <summary>
        /// 本循环工位是否启用，参与逻辑运算
        /// 为什么会有这么个设计呢，因为每一种型号的炉子，真空腔的个数都不一样
        /// 比如demo机，没有前摆渡，asc有四个真空腔，而tsc只有一个真空腔
        /// </summary>
        [DisplayName("工位是否启用")]
        public bool IsActive { get; set; }

        /// <summary>
        /// 读取值
        /// </summary>
        public object ReadValue { get; set; }        


        public override string ToString()
        {
            return this.ModelName + $"起始位：{this.WordLength}" + $"长度：{this.WordLength}";
        }
    }
}
