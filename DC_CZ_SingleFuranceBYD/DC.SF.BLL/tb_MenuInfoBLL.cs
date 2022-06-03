using System;
using System.Data;
using System.Collections.Generic;
using DC.SF.Model;
using DC.SF.Common;

namespace DC.SF.BLL
{
    /// <summary>
    /// tb_MenuInfo
    /// </summary>
    public partial class tb_MenuInfoBLL
    {
        private readonly DC.SF.DAL.tb_MenuInfoDAL dal = new DC.SF.DAL.tb_MenuInfoDAL();
        public tb_MenuInfoBLL()
        { }
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
        /// 得到一个对象实体
        /// </summary>
        public DC.SF.Model.tb_MenuInfo GetModel(int Id)
        {

            return dal.GetModel(Id);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        //public DataSet GetDataTable()
        //{
        //    return dal.GetList();
        //}

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<DC.SF.Model.tb_MenuInfo> GetModelList(string where)
        {
            DataSet ds = dal.GetList(where);
            return TypeConvertDataModel.TableConvertToList<tb_MenuInfo>(ds.Tables[0]);
        }

        /// <summary>
        /// 获取指定角色的菜单列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public List<DC.SF.Model.tb_MenuInfo> GetRoleMenuList(int roleId)
        {
            DataSet ds = dal.GetList(roleId);
            return TypeConvertDataModel.TableConvertToList<tb_MenuInfo>(ds.Tables[0]);
        }
        
        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}
