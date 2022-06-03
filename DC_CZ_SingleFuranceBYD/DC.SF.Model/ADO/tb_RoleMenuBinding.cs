using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.Model
{
    /// <summary>
    /// tb_RoleMenuBinding:实体类(角色-菜单绑定实体)
    /// </summary>
    [Serializable]
    public partial class tb_RoleMenuBinding
    {
        public tb_RoleMenuBinding()
        { }
        #region Model
        private int _fk_roleinfo_id;
        private int _fk_menuinfo_id;
        /// <summary>
        /// 角色ID（外键）
        /// </summary>
        public int FK_RoleInfo_Id
        {
            set { _fk_roleinfo_id = value; }
            get { return _fk_roleinfo_id; }
        }
        /// <summary>
        /// 菜单ID（外键）
        /// </summary>
        public int FK_MenuInfo_Id
        {
            set { _fk_menuinfo_id = value; }
            get { return _fk_menuinfo_id; }
        }
        #endregion Model

    }
}
