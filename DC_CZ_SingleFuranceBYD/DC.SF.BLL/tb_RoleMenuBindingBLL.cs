using System;
using System.Data;
using System.Collections.Generic;
using DC.SF.Model;
using DC.SF.Common;

namespace DC.SF.BLL
{
    /// <summary>
    /// tb_RoleMenuBinding
    /// </summary>
    public partial class tb_RoleMenuBindingBLL
    {
        private readonly DC.SF.DAL.tb_RoleMenuBindingDAL dal = new DC.SF.DAL.tb_RoleMenuBindingDAL();
        public tb_RoleMenuBindingBLL()
        { }
        #region  BasicMethod

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(DC.SF.Model.tb_RoleMenuBinding model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 保存用户权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="menuIds"></param>
        /// <returns></returns>
        public bool SaveRight(int roleId,int[] menuIds)
        {
            try
            {
                Delete(roleId);

                for (int i = 0; i < menuIds.Length; i++)
                {
                    tb_RoleMenuBinding model = new tb_RoleMenuBinding();
                    model.FK_RoleInfo_Id = roleId;
                    model.FK_MenuInfo_Id = menuIds[i];
                    Add(model);
                }
                return true;
            }
            catch(Exception ex)
            {
                LogHelper.Current.WriteEx("保存用户权限异常", ex);
                return false;
            }

        }

        /// <summary>
        /// 获取所有角色和菜单集合
        /// </summary>
        /// <returns></returns>
        public List<RoleMenuInfo> GetRoleMenuList()
        {
            return dal.GetRoleMenuList();
        }
        /// <summary>
        /// 获取角色和菜单集合
        /// </summary>
        /// <returns></returns>
        public List<RoleMenuInfo> GetRoleMenuList(string strWhere)
        {
            return dal.GetRoleMenuList(strWhere);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(DC.SF.Model.tb_RoleMenuBinding model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int roleId)
        {
            //该表无主键信息，请自定义主键/条件字段
            return dal.Delete(roleId);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DC.SF.Model.tb_RoleMenuBinding GetModel()
        {
            //该表无主键信息，请自定义主键/条件字段
            return dal.GetModel();
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
        public List<DC.SF.Model.tb_RoleMenuBinding> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<DC.SF.Model.tb_RoleMenuBinding> DataTableToList(DataTable dt)
        {
            List<DC.SF.Model.tb_RoleMenuBinding> modelList = new List<DC.SF.Model.tb_RoleMenuBinding>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                DC.SF.Model.tb_RoleMenuBinding model;
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

        #endregion  ExtensionMethod
    }
}
