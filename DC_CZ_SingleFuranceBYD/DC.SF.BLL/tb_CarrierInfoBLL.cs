using System;
using System.Data;
using System.Collections.Generic;
using DC.SF.Model;
namespace DC.SF.BLL
{
	/// <summary>
	/// tb_CarrierInfo
	/// </summary>
	public partial class tb_CarrierInfoBLL
	{
		private readonly DC.SF.DAL.tb_CarrierInfoDAL dal=new DC.SF.DAL.tb_CarrierInfoDAL();
		public tb_CarrierInfoBLL()
		{}
        #region  BasicMethod

        public DataTable GetPageQuery(DateTime dtbegin, DateTime dtend, int carrierNum, string cellcode, int pageIndex, int pagesize, ref int totalCount)
        {
            return dal.GetPageQuery(dtbegin, dtend, carrierNum, cellcode, pageIndex, pagesize, ref totalCount);
        }

        public DataTable GetRepeatCell(DateTime dtbegin, DateTime dtend, int carrierNum, string cellcode)
        {
            return dal.GetRepeatCell(dtbegin, dtend, carrierNum, cellcode);
        }

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
		{
			return dal.GetMaxId();
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Id)
		{
			return dal.Exists(Id);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(int carrierNum)
		{
            tb_CarrierInfo carrierModel = new tb_CarrierInfo();
            carrierModel.CarrierNum = carrierNum;
            return dal.Add(carrierModel);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(DC.SF.Model.tb_CarrierInfo model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int Id)
		{
			
			return dal.Delete(Id);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string Idlist )
		{
			return dal.DeleteList(Idlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public DC.SF.Model.tb_CarrierInfo GetModel(int Id)
		{
			
			return dal.GetModel(Id);
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
		public List<DC.SF.Model.tb_CarrierInfo> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<DC.SF.Model.tb_CarrierInfo> DataTableToList(DataTable dt)
		{
			List<DC.SF.Model.tb_CarrierInfo> modelList = new List<DC.SF.Model.tb_CarrierInfo>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				DC.SF.Model.tb_CarrierInfo model;
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
        /// <summary>
        /// 返回最大生产ID
        /// </summary>
        public int GetMaxProduceId(int carrierNum)
        {
            return dal.GetMaxProduceId(carrierNum);
        }

        /// <summary>
        /// 更新载体结束时间，及真空度
        /// </summary>
        /// <returns></returns>
        public bool UpdateEndTimeAndVacuum(int id, DateTime endTime, float vacuum)
        {
            return dal.UpdateEndTimeAndVacuum(id, endTime, vacuum);
        }

        /// <summary>
        /// 更新载体结束时间
        /// </summary>
        /// <param name="id"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public bool UpdateEndTime(int id, DateTime endTime)
        {
            return dal.UpdateEndTime(id, endTime);
        }

        /// <summary>
        /// 更新载体开始时间
        /// </summary>
        /// <param name="id"></param>
        /// <param name="beginTime"></param>
        /// <returns></returns>
        public bool UpdateBeginTime(int id, DateTime beginTime)
        {
            return dal.UpdateBeginTime(id, beginTime);
        }
        #endregion  ExtensionMethod
    }
}

