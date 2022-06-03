using System;
using System.Data;
using System.Collections.Generic;
using DC.SF.Model;
using DC.SF.DataLibrary;

namespace DC.SF.BLL
{
	/// <summary>
	/// CarDistribution
	/// </summary>
	public partial class CarDistributionBLL
	{
		private readonly DC.SF.DAL.CarDistributionDAL dal=new DC.SF.DAL.CarDistributionDAL();
		public CarDistributionBLL()
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
		public long Add(int carId,DateTime loadTime)
		{
            DC.SF.Model.CarDistribution model = new CarDistribution();
            model.Eno = MemoryData.MesSettingsModel.BYDMesEquipmentID;  //设备资产编号现在还不知道
            model.CarNo = carId.ToString();
            model.LoadingTime = loadTime;
            model.SamplingTime = loadTime;
            return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(DC.SF.Model.CarDistribution model)
		{
			return dal.Update(model);
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
		public DC.SF.Model.CarDistribution GetModel(long SystemAutoID)
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
		public List<DC.SF.Model.CarDistribution> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<DC.SF.Model.CarDistribution> DataTableToList(DataTable dt)
		{
			List<DC.SF.Model.CarDistribution> modelList = new List<DC.SF.Model.CarDistribution>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				DC.SF.Model.CarDistribution model;
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

