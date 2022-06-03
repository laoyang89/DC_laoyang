using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.Model
{
    /// <summary>
    /// tb_RoleInfo:实体类(角色信息实体)
    /// </summary>
    [Serializable]
    public partial class tb_RoleInfo
    {
        public tb_RoleInfo()
        { }
        #region Model
        private int _id;
        private string _rolename;
        /// <summary>
        /// 自增ID（主键）
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName
        {
            set { _rolename = value; }
            get { return _rolename; }
        }
        #endregion Model

    }
}
