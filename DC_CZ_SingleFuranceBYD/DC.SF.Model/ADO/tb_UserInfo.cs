using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.Model
{
    /// <summary>
    /// tb_UserInfo:实体类(用户信息实体)
    /// </summary>
    [Serializable]
    public partial class tb_UserInfo
    {
        public tb_UserInfo()
        { }
        #region Model
        private int _id;
        private string _username;
        private string _userpassword;
        private DateTime? _createtime;
        private int _fk_roleinfo_id;
        private bool _isActived;
        private string _userIDCard;
        /// <summary>
        /// 层生产ID（外键）
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 用户名称
        /// </summary>
        [DisplayName("用户名称")]
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 用户密码
        /// </summary>
        [DisplayName("用户密码")]
        public string UserPassword
        {
            set { _userpassword = value; }
            get { return _userpassword; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public DateTime? CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 角色ID（外键）
        /// </summary>
        [DisplayName("权限类别")]
        public int FK_RoleInfo_Id
        {
            set { _fk_roleinfo_id = value; }
            get { return _fk_roleinfo_id; }
        }

        /// <summary>
        /// 是否启用
        /// </summary>
        [DisplayName("是否启用")]
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
        /// <summary>
        /// IDCard
        /// </summary>
        [DisplayName("用户ID卡号")]
        public string UserIDCard
        {
            set { _userIDCard = value; }
            get { return _userIDCard; }
        }
        #endregion Model

    }
}
