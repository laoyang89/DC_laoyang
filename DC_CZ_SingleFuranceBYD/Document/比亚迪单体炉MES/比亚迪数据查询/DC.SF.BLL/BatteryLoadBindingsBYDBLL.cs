using DC.SF.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.BLL
{
    /// <summary>
    /// BatteryLoadBindings
    /// </summary>
    public partial class BatteryLoadBindingsBYDBLL
    {
        private readonly BatteryLoadBindingsBYDDAL dal = new BatteryLoadBindingsBYDDAL();
        public BatteryLoadBindingsBYDBLL()
        { }

        #region  ExtensionMethod

       /// <summary>
       /// 比亚迪获取重复条码信息
       /// </summary>
       /// <param name="dtbegin"></param>
       /// <param name="dtend"></param>
       /// <param name="carrierNum"></param>
       /// <param name="cellcode"></param>
       /// <returns></returns>
        public DataTable GetRepeatCell(DateTime dtbegin, DateTime dtend, int carrierNum, string cellcode)
        {
            return dal.GetRepeatCell(dtbegin, dtend, carrierNum, cellcode);
        }
        /// <summary>
        /// 比亚迪获取小车信息
        /// </summary>
        /// <param name="dtbegin"></param>
        /// <param name="dtend"></param>
        /// <param name="carrierNum"></param>
        /// <param name="cellcode"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pagesize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataTable GetPageQuery(DateTime dtbegin, DateTime dtend, int carrierNum, string cellcode, int pageIndex, int pagesize, ref int totalCount)
        {
            return dal.GetPageQuery(dtbegin, dtend, carrierNum, cellcode, pageIndex, pagesize, ref totalCount);
        }
         #endregion
        }
}
