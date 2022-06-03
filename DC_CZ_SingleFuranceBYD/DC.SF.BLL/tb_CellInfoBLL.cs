using System;
using System.Data;
using System.Collections.Generic;
using DC.SF.Model;
using DC.SF.DataLibrary;
using DC.SF.Common;
using System.Linq;

namespace DC.SF.BLL
{
	/// <summary>
	/// tb_CellInfo
	/// </summary>
	public partial class tb_CellInfoBLL
	{
		private readonly DC.SF.DAL.tb_CellInfoDAL dal=new DC.SF.DAL.tb_CellInfoDAL();
		public tb_CellInfoBLL()
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
		public int  Add(DC.SF.Model.tb_CellInfo model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(DC.SF.Model.tb_CellInfo model)
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
		public DC.SF.Model.tb_CellInfo GetModel(int Id)
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
		/// 获得数据列表
		/// </summary>
		public List<DC.SF.Model.tb_CellInfo> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<DC.SF.Model.tb_CellInfo> DataTableToList(DataTable dt)
		{
			List<DC.SF.Model.tb_CellInfo> modelList = new List<DC.SF.Model.tb_CellInfo>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				DC.SF.Model.tb_CellInfo model;
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
        /// 按层板插入每一层电池
        /// </summary>
        public void InsertCellsByLayer(List<CellInfo> lstCellInfo,int CarrierId)
        {
            for (int i = 0; i < ConfigData.LayersCount; i++)
            {
                List<CellInfo> lst = lstCellInfo.Where(r => r.LayerNum == i + 1).ToList();
                if (lst != null && lst.Count > 0)
                {
                    dal.InsertCellsByLayer(lst,CarrierId);
                }
            }
        }

        public void DeleteCellById(int carrierId)
        {
            dal.DeleteCellById(carrierId);
        }

        /// <summary>
        /// 按条码查询电池信息
        /// </summary>
        /// <param name="cellCode"></param>
        /// <returns></returns>
        public DataTable QueryCellInfoByCellCode(string cellCode)
        {
            return dal.QueryCellInfoByCellCode(cellCode);
        }
        #endregion  ExtensionMethod
    }
}

