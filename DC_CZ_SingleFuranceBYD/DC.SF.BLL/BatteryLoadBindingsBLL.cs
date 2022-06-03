using System;
using System.Data;
using System.Collections.Generic;
using DC.SF.Model;
using DC.SF.Common;
using System.Linq;

namespace DC.SF.BLL
{
	/// <summary>
	/// BatteryLoadBindings
	/// </summary>
	public partial class BatteryLoadBindingsBLL
	{
		private readonly DC.SF.DAL.BatteryLoadBindingsDAL dal=new DC.SF.DAL.BatteryLoadBindingsDAL();
		public BatteryLoadBindingsBLL()
		{}
        #region  BasicMethod

        
        /// <summary>
        /// 比亚迪获取重复条码信息
        /// </summary>
        /// <param name="dtbegin"></param>
        /// <param name="dtend"></param>
        /// <param name="carrierNum"></param>
        /// <param name="cellcode"></param>
        /// <returns></returns>
        public DataTable GetRepeatCell(DateTime dtbegin, DateTime dtend, string carrierNum, string cellcode)
        {
            return dal.GetRepeatCell(dtbegin, dtend, carrierNum, cellcode);
        }
        /// <summary>
        ///  比亚迪获取小车信息
        /// </summary>
        /// <param name="dtbegin">扫码开始时间</param>
        /// <param name="dtend">扫码结束时间</param>
        /// <param name="carrierNum">小车唯一ID</param>
        /// <param name="cellcode">电池条码</param>
        /// <param name="pageIndex">第几页</param>
        /// <param name="pagesize">每页几条数据</param>
        /// <param name="timeFlag">是否开启时间筛选</param>
        /// <param name="totalCount">总条数</param>
        /// <returns></returns>
        public DataTable GetPageQuery(DateTime dtbegin, DateTime dtend, string carrierNum, string cellcode, int pageIndex, int pagesize,bool timeFlag, ref int totalCount)
        {
            return dal.GetPageQuery(dtbegin, dtend, carrierNum, cellcode, pageIndex, pagesize,timeFlag, ref totalCount);
        }

        /// <summary>
        /// 按层板插入每一层电池
        /// </summary>
        public void InsertCellsByLayer(List<CellInfo> lstCellInfo, long CarrierId)
        {
            for (int i = 0; i < ConfigData.LayersCount; i++)
            {
                List<CellInfo> lst = lstCellInfo.Where(r => r.LayerNum == i + 1).ToList();
                if (lst != null && lst.Count > 0)
                {
                    dal.InsertCellsByLayer(lst, CarrierId);
                }
            }
        }
        /// <summary>
        /// 批量导入条码
        /// </summary>
        /// <param name="lstCellInfo"></param>
        /// <param name="CarrierId"></param>
        public void InsertCellsByList(List<CellInfo> lstCellInfo, long CarrierId)
        {
            dal.InsertCellsByList(lstCellInfo, CarrierId);
        }
        /// <summary>
        /// 查询当前电芯是否存在数据库指定小车上
        /// </summary>
        /// <param name="cellInfo"></param>
        /// <param name="CarrierInfoId"></param>
        /// <returns></returns>
        public bool CheckCellToDbID(CellInfo cellInfo, long CarrierInfoId)
        {
            return dal.CheckCellToDbID(cellInfo, CarrierInfoId);
        }
        public void DeleteCellById(long carrierId)
        {
            dal.DeleteCellById(carrierId);
        }


        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long SystemAutoID)
		{
			return dal.Exists(SystemAutoID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public long Add(DC.SF.Model.BatteryLoadBindings model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(DC.SF.Model.BatteryLoadBindings model)
		{
			return dal.Update(model);
		}

        /// <summary>
        /// 根据条件修改
        /// </summary>
        /// <param name="strSet"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public bool UpdateWhere(string strSet, string strWhere)
        {
            return dal.UpdateWhere(strSet, strWhere);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(long SystemAutoID)
		{
			
			return dal.Delete(SystemAutoID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string SystemAutoIDlist )
		{
			return dal.DeleteList(SystemAutoIDlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public DC.SF.Model.BatteryLoadBindings GetModel(string strWhere)
		{
			
			return dal.GetModel(strWhere);
		}


		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<DC.SF.Model.BatteryLoadBindings> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<DC.SF.Model.BatteryLoadBindings> DataTableToList(DataTable dt)
		{
			List<DC.SF.Model.BatteryLoadBindings> modelList = new List<DC.SF.Model.BatteryLoadBindings>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				DC.SF.Model.BatteryLoadBindings model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = dal.DataRowToModel(dt.Rows[n]);
					if (model != null)
					{
						modelList.Add(model);
					}
				}
			}
			return modelList;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			return dal.GetRecordCount(strWhere);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			return dal.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

