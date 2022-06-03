using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.Model
{
    /// <summary>
    /// 角色-权限类  Ado下的那个类是与数据库对应，只有两个Id的，为了方便展示，创建此关联类
    /// </summary>
    public class RoleMenuInfo
    {
        public int RoleId { get; set; }

        /// <summary>
        /// 角色名
        /// </summary>
        public string RoleName { get; set; }

        public int MenuId { get; set; }

        /// <summary>
        /// 权限名
        /// </summary>
        public string MenuName { get; set; }
    }
}
