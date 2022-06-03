using System;
using System.Data;
using System.Collections.Generic;
using DC.SF.Model;
namespace DC.SF.BLL
{
	/// <summary>
	/// AlarmRule
	/// </summary>
	public partial class AlarmRuleBLL
	{
		private readonly DC.SF.DAL.AlarmRuleDAL dal=new DC.SF.DAL.AlarmRuleDAL();
		public AlarmRuleBLL()
		{}
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int SystemAutoID)
        {
            return dal.Exists(SystemAutoID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(DC.SF.Model.AlarmRule model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(DC.SF.Model.AlarmRule model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int SystemAutoID)
        {

            return dal.Delete(SystemAutoID);
        }
        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string SystemAutoIDlist)
        {
            return dal.DeleteList(SystemAutoIDlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DC.SF.Model.AlarmRule GetModel(int SystemAutoID)
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
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<DC.SF.Model.AlarmRule> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<DC.SF.Model.AlarmRule> DataTableToList(DataTable dt)
        {
            List<DC.SF.Model.AlarmRule> modelList = new List<DC.SF.Model.AlarmRule>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                DC.SF.Model.AlarmRule model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new DC.SF.Model.AlarmRule();
                    if (dt.Rows[n]["SystemAutoID"].ToString() != "")
                    {
                        model.SystemAutoID = int.Parse(dt.Rows[n]["SystemAutoID"].ToString());
                    }
                    if (dt.Rows[n]["AlarmAddress"].ToString() != "")
                    {
                        model.AlarmAddress = int.Parse(dt.Rows[n]["AlarmAddress"].ToString());
                    }
                    if (dt.Rows[n]["AlarmIndex"].ToString() != "")
                    {
                        model.AlarmIndex = int.Parse(dt.Rows[n]["AlarmIndex"].ToString());
                    }
                    model.AlarmContent = dt.Rows[n]["AlarmContent"].ToString();
                    model.WorkStationNo = dt.Rows[n]["WorkStationNo"].ToString();


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
            return GetList("");
        }
        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}

