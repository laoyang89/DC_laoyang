using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.Model
{
    /// <summary>
    /// tb_MenuInfo:实体类(菜单信息实体)
    /// </summary>
    [Serializable]
    public partial class tb_MenuInfo
    {
        public tb_MenuInfo()
        { }
        #region Model
        private int _id;
        private string _menuname;
        private int _parentId;
        private bool _isActived;
        /// <summary>
        /// 自增ID（主键）
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName
        {
            set { _menuname = value; }
            get { return _menuname; }
        }

        /// <summary>
        /// 父节点Id
        /// </summary>
        public int ParentId
        {
            get
            {
                return _parentId;
            }

            set
            {
                _parentId = value;
            }
        }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActived
        {
            get
            {
                return _isActived;
            }

            set
            {
                _isActived = value;
            }
        }
        #endregion Model

    }
}
