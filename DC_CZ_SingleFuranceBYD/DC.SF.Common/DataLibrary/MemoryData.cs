using DC.SF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.Common
{
    public class MemoryData
    {
        public MemoryData()
        {
            _currentUser = new tb_UserInfo();
        }

        private static tb_UserInfo _currentUser;
        /// <summary>
        /// 当前登录用户
        /// </summary>
        public static tb_UserInfo CurrentUser
        {
            get
            {
                return _currentUser;
            }
            set
            {
                _currentUser = value;
            }
        }
    }
}
