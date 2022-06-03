using DC.SF.DAL.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.DAL
{
    /// <summary>
    /// 数据访问类:BatteryLoadBindings
    /// </summary>
    public partial class BatteryLoadBindingsBYDDAL
    {
        public BatteryLoadBindingsBYDDAL()
        { }
        /// <summary>
        /// 比亚迪重复条码查询小车
        /// </summary>
        /// <param name="dtbegin"></param>
        /// <param name="dtend"></param>
        /// <param name="carrierNum"></param>
        /// <param name="cellcode"></param>
        /// <returns></returns>
        public DataTable GetRepeatCell(DateTime dtbegin, DateTime dtend, int carrierNum, string cellcode)
        {
            StringBuilder sbwhere = new StringBuilder();
            //string carrierWhere = carrierNum == 0 ? " " : string.Format(" and t2.CarrierNum = {0} ", carrierNum);
            if (carrierNum != 0)
            {
                sbwhere.Append(string.Format(" and t2.CarNo = {0} ", carrierNum));
            }
            string sqlstr = string.Format(@" select PLotNo'重复条码',count(1) '重复次数'  from BatteryLoadBindings t1 left join CarDistribution t2
                                             on t2.SystemAutoID = t1.CarSystemID
                                             where ScanTime between '{0}'  and  '{1}'
                                             {2} and PLotNo like '%{3}%'
                                             group by PLotNo having COUNT(1) > 1", dtbegin, dtend, sbwhere, cellcode);
            return DbHelperSQL.Query(sqlstr).Tables[0];
        }
        /// <summary>
        /// 比亚迪查询电池信息
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
            StringBuilder sb = new StringBuilder();
            StringBuilder sbwhere = new StringBuilder();
            //string carrierWhere = carrierNum == 0 ? " " : string.Format(" and t2.CarrierNum = {0} ", carrierNum);
            if (carrierNum != 0)
            {
                sbwhere.Append(string.Format(" and t2.CarNo = {0} ", carrierNum));
            }
            
            string sqlstr = string.Format(@" select tb.PLotNo '条码',tb.ScanTime '扫码时间',
                                                    tb.LayerNumber '层号' ,tb.RowNum '排号',
                                                    tb.ColumnNum '列号',tb.SystemAutoID '工艺编号',
                                                    tb.CarNo '小车号',tb.LoadingTime '工艺开始时间',tb.UnloadTime '工艺结束时间'  from
                                                      (
                                                    select t1.*,t2.CarNo,t2.LoadingTime,t2.UnloadTime, ROW_NUMBER() over(order by ScanTime asc) orderNum 
                                                from BatteryLoadBindings  t1, dbo.CarDistribution t2
                                                   where   t2.SystemAutoID = t1.CarSystemID   {3} and t1.ScanTime between '{0}'  and  '{1}'
                                                and t1.PLotNo like '%{2}%'
                                                   ) tb
                                                   where tb.orderNum between {4} and {5}", dtbegin, dtend, cellcode, sbwhere.ToString(), (pageIndex - 1) * pagesize + 1, pageIndex * pagesize);

            string sqlcount = string.Format(@"select COUNT(1) rowtotal from BatteryLoadBindings  t1,CarDistribution t2
                                            where  t2.SystemAutoID = t1.CarSystemID  {0}
                                                and ScanTime between '{1}' and '{2}' 
                                                and PLotNo like '%{3}%'  ", sbwhere.ToString(), dtbegin, dtend, cellcode);
            sb.AppendLine(sqlstr);
            sb.AppendLine(sqlcount);


            DataSet ds = DbHelperSQL.Query(sb.ToString());
            totalCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);

            return ds.Tables[0];
        }
     }
}
