using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using DC.SF.DAL;
using DC.SF.Model;
using System.Linq;

namespace DC.SF.BLL
{

    //tb_ScanCellCode
    public partial class tb_ScanCellCodeBLL
	{
   		     
		private readonly tb_ScanCellCodeDAL dal=new tb_ScanCellCodeDAL();
		public tb_ScanCellCodeBLL()
		{}

        #region  Method
       
        /// <summary>
        /// 批量加入
        /// </summary>
        /// <param name="listScan"></param>
        /// <returns></returns>
        public int AddAndUpdateList(List<tb_ScanCellCode> listScan)
        {
            if (listScan == null)
            {
                return 0;
            }
            return dal.AddAndUpdateList(listScan);
        }
        /// <summary>
        /// 获取最后一个RankCode
        /// </summary>
        /// <returns></returns>
        public int GetRankCodeAtPresent()
        {
            return dal.GetRankCodeAtPresent();
        }

        public int UpdateCarDBID(CH_CarInfo carInfo)
        {
            int[] rankcodes = carInfo.ListCellInfo.Select(m => m.RankCode).ToArray();
            return dal.UpdateCarDBID(rankcodes, carInfo.CraftDBId);
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int RankCode)
		{
			return dal.Exists(RankCode);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(tb_ScanCellCode model)
		{
           return dal.Add(model);
        }

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(tb_ScanCellCode model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int RankCode)
		{
			return dal.Delete(RankCode);
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public tb_ScanCellCode GetModel(int RankCode)
		{
			return dal.GetModel(RankCode);
		}

        ///// <summary>
        ///// 得到一个对象实体，从缓存中
        ///// </summary>
        //public tb_ScanCellCode GetModelByCache(int RankCode)
        //{

        //	string CacheKey = "tb_ScanCellCodeModel-" + RankCode;
        //	object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
        //	if (objModel == null)
        //	{
        //		try
        //		{
        //			objModel = dal.GetModel(RankCode);
        //			if (objModel != null)
        //			{
        //				int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
        //				Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
        //			}
        //		}
        //		catch{}
        //	}
        //	return (tb_ScanCellCode)objModel;
        //}

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="strWhere">Where条件</param>
        /// <param name="extraStr">排序条件</param>
        /// <returns></returns>
        public DataSet GetList(string strWhere,string extraStr)
		{
			return dal.GetList(strWhere, extraStr);
		}
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="strWhere">Where条件</param>
        /// <param name="extraStr">排序条件</param>
        /// <returns></returns>
        public List<tb_ScanCellCode> GetModelList(string strWhere, string extraStr)
		{
			DataSet ds = dal.GetList(strWhere, extraStr);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<tb_ScanCellCode> DataTableToList(DataTable dt)
		{
			List<tb_ScanCellCode> modelList = new List<tb_ScanCellCode>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				tb_ScanCellCode model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new tb_ScanCellCode();
                    if (dt.Rows[n]["RankCode"].ToString() != "")
                    {
                        model.RankCode = int.Parse(dt.Rows[n]["RankCode"].ToString());
                    }
                    model.CellCode = dt.Rows[n]["CellCode"].ToString();
                    if (dt.Rows[n]["ScanTime"].ToString() != "")
                    {
                        model.ScanTime = DateTime.Parse(dt.Rows[n]["ScanTime"].ToString());
                    }
                    if (dt.Rows[n]["CellType"].ToString() != "")
                    {
                        model.CellType = int.Parse(dt.Rows[n]["CellType"].ToString());
                    }
                    model.CellModelType = dt.Rows[n]["CellModelType"].ToString();
                    model.CellPackage = dt.Rows[n]["CellPackage"].ToString();
                    if (dt.Rows[n]["CarID"].ToString() != "")
                    {
                        model.CarID = int.Parse(dt.Rows[n]["CarID"].ToString());
                    }


                    modelList.Add(model);
                }
            }
			return modelList;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("","");
		}
#endregion
   
	}
}