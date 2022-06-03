using System;
using System.Data;
using System.Collections.Generic;
using DC.SF.Model;
using DC.SF.Common;

namespace DC.SF.BLL
{
    /// <summary>
    /// tb_OperateRecord
    /// </summary>
    public partial class tb_OperateRecordBLL
    {
        private DC.SF.DAL.tb_OperateRecordDAL dal = new DC.SF.DAL.tb_OperateRecordDAL();
        public tb_OperateRecordBLL()
        { }
        #region  BasicMethod
        

        /// <summary>
        /// 添加操作日志
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public int AddOprRecord(string content,string employeeName,string employeeID)
        {
            tb_OperateRecord record = new tb_OperateRecord();
            record.EmployeeName = employeeName;
            record.RecordTime = DateTime.Now;
            record.OperateContent = content;
            record.EmployeeID = employeeID;
            return dal.Add(record);
        }

        public bool ClearDBLog()
        {
            return dal.ClearDBLog();
        }
        public void ClearDBData()
        {
            dal.ClearDBData();
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public  int Add(DC.SF.Model.tb_OperateRecord model)
        {
            return dal.Add(model);
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
        public List<DC.SF.Model.tb_OperateRecord> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DC.SF.Common.TypeConvertDataModel.TableConvertToList<tb_OperateRecord>(ds.Tables[0]);
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
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
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
        public DataSet PagingQuery(DateTime startTime, DateTime endTime, int currentIndex, int pageSize, string userName, string opType)
        {
            return dal.PagingQuery(startTime, endTime, currentIndex, pageSize, userName, opType);
        }
        public DataSet OpTypeList()
        {
            return dal.OpTypeList();
        }
        #endregion  ExtensionMethod
    }
}
