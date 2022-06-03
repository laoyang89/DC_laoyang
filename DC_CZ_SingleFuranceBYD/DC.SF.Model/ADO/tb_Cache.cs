using System;

namespace DC.SF.Model
{
    //tb_Cache
    [Serializable]
    public class tb_Cache
	{
   		     
      	/// <summary>
		/// 自增Id
        /// </summary>		
		private int _id;
        public int Id
        {
            get{ return _id; }
            set{ _id = value; }
        }        
		/// <summary>
		/// 组Id，当持久化的数据为字典时，可使用该项以区分。
        /// </summary>		
		private int _groupid;
        public int GroupId
        {
            get{ return _groupid; }
            set{ _groupid = value; }
        }        
		/// <summary>
		/// 变量名称
        /// </summary>		
		private string _variablename;
        public string VariableName
        {
            get{ return _variablename; }
            set{ _variablename = value; }
        }        
		/// <summary>
		/// 变量Json值
        /// </summary>		
		private string _variablevalue;
        public string VariableValue
        {
            get{ return _variablevalue; }
            set{ _variablevalue = value; }
        }        
		/// <summary>
		/// 缓存类型，1：小车对象，2：SaveDataInfo
        /// </summary>		
		private int _cachetype;
        public int CacheType
        {
            get{ return _cachetype; }
            set{ _cachetype = value; }
        }        
		   
	}
}

