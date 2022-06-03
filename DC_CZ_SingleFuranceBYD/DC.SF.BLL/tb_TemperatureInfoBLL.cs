using System;
using System.Data;
using System.Collections.Generic;
using DC.SF.Model;
namespace DC.SF.BLL
{
	/// <summary>
	/// tb_TemperatureInfo
	/// </summary>
	public partial class tb_TemperatureInfoBLL
	{
		private readonly DC.SF.DAL.tb_TemperatureInfoDAL dal=new DC.SF.DAL.tb_TemperatureInfoDAL();
		public tb_TemperatureInfoBLL()
		{}
		#region  BasicMethod

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
		public int  Add(DC.SF.Model.tb_TemperatureInfo model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(DC.SF.Model.tb_TemperatureInfo model)
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
		public DC.SF.Model.tb_TemperatureInfo GetModel(int Id)
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
		public List<DC.SF.Model.tb_TemperatureInfo> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<DC.SF.Model.tb_TemperatureInfo> DataTableToList(DataTable dt)
		{
			List<DC.SF.Model.tb_TemperatureInfo> modelList = new List<DC.SF.Model.tb_TemperatureInfo>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				DC.SF.Model.tb_TemperatureInfo model;
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
        /// 批量插入温度数据
        /// </summary>
        public bool InsertList(List<tb_TemperatureInfo> lstTemperature)
        {
            return dal.InsertList(lstTemperature);
        }

        public DataTable GetTemperature(string carrierId, string stationNum, DateTime? dtBegin, DateTime? dtEnd, int PageIndex, int PageCount, int layCount, ref int totalRecord, EnumMachineType type = EnumMachineType.ChenHuaFurnance)
        {
            return dal.GetTemperature(carrierId,stationNum,dtBegin,dtEnd,PageIndex,PageCount,layCount,ref totalRecord,type);
        }

        public void ChangeTempCarriered(int carrierid)
        {
            dal.ChangeTempCarriered(carrierid);
        }

        public DataTable QueryTemperature(int layerNum, int carrierId, int? stationNum = null)
        {
            return dal.QueryTemperature(layerNum, carrierId, stationNum);
        }
        #endregion  ExtensionMethod
    }
}

