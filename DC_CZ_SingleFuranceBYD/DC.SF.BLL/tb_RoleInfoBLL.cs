using System;
using System.Data;
using System.Collections.Generic;
using DC.SF.Model;
using DC.SF.Common;

namespace DC.SF.BLL
{
    /// <summary>
    /// tb_RoleInfo
    /// </summary>
    public partial class tb_RoleInfoBLL
    {
        private readonly DC.SF.DAL.tb_RoleInfoDAL dal = new DC.SF.DAL.tb_RoleInfoDAL();
        public tb_RoleInfoBLL()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return dal.GetMaxId();
        }

        public tb_RoleInfo GetModel(string where)
        {
            return dal.GetModel(where);
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
        public int Add(DC.SF.Model.tb_RoleInfo model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(DC.SF.Model.tb_RoleInfo model)
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
        public bool DeleteList(string Idlist)
        {
            return dal.DeleteList(Idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DC.SF.Model.tb_RoleInfo GetModel(int Id)
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
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<DC.SF.Model.tb_RoleInfo> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return TypeConvertDataModel.TableConvertToList<tb_RoleInfo>(ds.Tables[0]);
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }

        #endregion  BasicMethod
        #region  ExtensionMethod
        /// <summary>
        /// 获取直接展示在界面上的角色列表
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public DataTable GetRoleList(string where)
        {
            return dal.GetRoleList(where);
        }
        #endregion  ExtensionMethod
    }
}
