using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.Model
{
    public class Mes_LoadModel
    {
        public string CarNo { get; set; }   //小车号

        public string MachineNo { get; set; } //设备编号

        /// <summary>
        /// 电池条码列表
        /// </summary>
        public List<string> ListBarcode   
        {
            get
            {
                return _ListBarcode;
            }

            set
            {
                _ListBarcode = value;
            }
        }

        private List<string> _ListBarcode;

        /// <summary>
        /// token值
        /// </summary>
        public string token { get; set; }

        public Mes_LoadModel()
        {
            _ListBarcode = new List<string>();
        }
    }
}
