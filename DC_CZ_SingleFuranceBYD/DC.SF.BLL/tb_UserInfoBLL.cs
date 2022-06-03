using System;
using System.Data;
using System.Collections.Generic;
using DC.SF.Model;
using DC.SF.Common;

namespace DC.SF.BLL
{
    /// <summary>
    /// tb_UserInfo
    /// </summary>
    public partial class tb_UserInfoBLL
    {
        private readonly DC.SF.DAL.tb_UserInfoDAL dal = new DC.SF.DAL.tb_UserInfoDAL();
        public tb_UserInfoBLL()
        { }
        #region  BasicMethod

        public bool CheckUserAndPwd(string userName, string pwd)
        {
            return dal.CheckUserAndPwd(userName, pwd);
        }

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
        public int Add(DC.SF.Model.tb_UserInfo model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(DC.SF.Model.tb_UserInfo model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 禁用or启用用户
        /// </summary>
        /// <param name="id"></param>
        /// <param name="able"></param>
        /// <returns></returns>
        public bool EnDisAbleUser(int id,bool able)
        {
            tb_UserInfo user = GetModel(id);
            user.IsActived = able;
            return Update(user);
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
        public DC.SF.Model.tb_UserInfo GetModel(int Id)
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
        /// 获取直接展示在界面上的用户列表
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public DataTable GetUserList(string where)
        {
            return dal.GetUserList(where);
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
        public List<DC.SF.Model.tb_UserInfo> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            if (ds != null)
            {
                return DC.SF.Common.TypeConvertDataModel.TableConvertToList<tb_UserInfo>(ds.Tables[0]);
            }
            else
            {
                return new List<tb_UserInfo>();
            }
          
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
        
        
        public DC.SF.Model.tb_UserInfo GetUserModelByUserName(string username)
        {
            string strWhere = $"and UserName='{username}'  ";
            return dal.GetUserModel(strWhere);
        }
        /// <summary>
        /// 根据用户名称和密码获取Model
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DC.SF.Model.tb_UserInfo GetUserModel(string username,string password)
        {
            string strWhere =$"and UserName='{username}' and UserPassword='{password}' ";
            return dal.GetUserModel(strWhere);
        }
        /// <summary>
        /// 根据IDCarID获取用户信息
        /// </summary>
        public DC.SF.Model.tb_UserInfo GetUserByIDCard(string idCard)
        {
            return dal.GetUserByIDCard(idCard);
        }
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        public DataSet GetUserList()
        {
            return dal.GetUserList();
        }
        #endregion  ExtensionMethod
    }
}
