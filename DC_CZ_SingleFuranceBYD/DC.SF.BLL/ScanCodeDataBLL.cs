using DC.SF.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.BLL
{
    public class ScanCodeDataBLL
    {
        private readonly DC.SF.DAL.ScanCodeDataDAL dal = new DC.SF.DAL.ScanCodeDataDAL();
        public ScanCodeDataBLL()
        { }
        #region  BasicMethod
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(tb_ScanCodeData model)
        {
            return dal.Add(model);
        }
        /// <summary>
        /// 获取一个数据列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public List<tb_ScanCodeData> GetModeList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
		/// 获得数据列表
		/// </summary>
		public List<DC.SF.Model.tb_ScanCodeData> DataTableToList(DataTable dt)
        {
            List<DC.SF.Model.tb_ScanCodeData> modelList = new List<DC.SF.Model.tb_ScanCodeData>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                DC.SF.Model.tb_ScanCodeData model;
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
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetListByPage(strWhere,orderby,startIndex,endIndex);
        }
        #endregion

    }
}
