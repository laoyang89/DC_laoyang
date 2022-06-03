using System;
using System.Data;
using System.Collections.Generic;
using DC.SF.Model;
namespace DC.SF.BLL
{
	/// <summary>
	/// EquipmentParameters
	/// </summary>
	public partial class EquipmentParametersBLL
	{
		private readonly DC.SF.DAL.EquipmentParametersDAL dal=new DC.SF.DAL.EquipmentParametersDAL();
		public EquipmentParametersBLL()
		{}
		#region  BasicMethod
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
		public long Add(DC.SF.Model.EquipmentParameters model)
		{
			return dal.Add(model);
		}

        public bool InsertList(List<tb_TemperatureInfoBYD> lstTemperature, double vacuumValue, long carrierid,short cavityStatus)
        {
            return dal.InsertList(lstTemperature, vacuumValue, carrierid, cavityStatus);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(DC.SF.Model.EquipmentParameters model)
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
            return dal.UpdateWhere(strSet , strWhere);
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
		public DC.SF.Model.EquipmentParameters GetModel(long SystemAutoID)
		{
			
			return dal.GetModel(SystemAutoID);
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
		public List<DC.SF.Model.EquipmentParameters> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
        /// <summary>
        /// 读取指定列名的数据(去重)
        /// </summary>
        /// <param name="colName"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetTableColData(string colName, string strWhere)
        {
            
            if (string.IsNullOrWhiteSpace(colName))
            {
                return null;
            }

            return dal.GetTableColData(colName, strWhere);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<DC.SF.Model.EquipmentParameters> DataTableToList(DataTable dt)
		{
			List<DC.SF.Model.EquipmentParameters> modelList = new List<DC.SF.Model.EquipmentParameters>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				DC.SF.Model.EquipmentParameters model;
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
        #endregion  BasicMethod
        #region  ExtensionMethod
        /// <summary>
        /// 获取记录总数
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
        ///// <summary>
        ///// 分页获取数据列表
        ///// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        /// <summary>
        /// 分页获取温度数据
        /// </summary>
        /// <param name="carrierId">小车唯一id</param>
        /// <param name="stationNum">站点ID</param>
        /// <param name="timeFlag">是否开启时间筛选</param>
        /// <param name="dtBegin">开始时间</param>
        /// <param name="dtEnd">结束时间</param>
        /// <param name="PageIndex">第几页</param>
        /// <param name="PageCount">每页条数</param>
        /// <param name="layCount">小车层板数</param>
        /// <param name="totalRecord">总条数</param>
        /// <returns></returns>
        public DataTable GetTemperature(string carrierId, string stationNum,bool timeFlag, DateTime? dtBegin, DateTime? dtEnd, int PageIndex, int PageCount, int layCount, ref int totalRecord)
        {
            return dal.GetTemperature(carrierId, stationNum, timeFlag, dtBegin, dtEnd, PageIndex, PageCount, layCount, ref totalRecord);
        }
        /// <summary>
        /// 获得时间间隔列表
        /// </summary>
        public List<int> GetRecordInterval(string strWhere)
        {
            DataSet ds = dal.GetIntervalDataSet(strWhere);
            //return dal.GetRecordInterval(strWhere);
            return SaveTempIntervalToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得时间间隔列表
        /// </summary>
        public List<int> SaveTempIntervalToList(DataTable dt)
        {
            List<int> modelList = new List<int>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                int model = 0;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = dal.SaveTempIntervalToInt(dt.Rows[n]);
                    modelList.Add(model);
                }
            }
            return modelList;
        }
        #endregion  ExtensionMethod
    }
}

