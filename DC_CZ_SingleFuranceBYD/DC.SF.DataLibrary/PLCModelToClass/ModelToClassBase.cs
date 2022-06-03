using DC.SF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.DataLibrary
{
    /// <summary>
    /// PLC model转换类基类
    /// </summary>
    public class ModelToClassBase
    {
        private CavityOneCraftStatus _craftStatus;

        public ModelToClassBase()
        {
            _craftStatus = CavityOneCraftStatus.Waiting;
        }

        /// <summary>
        /// 工艺状态
        /// </summary>
        public CavityOneCraftStatus CraftStatus
        {
            get
            {
                return _craftStatus;
            }

            set
            {
                _craftStatus = value;
            }
        }
    }
}
