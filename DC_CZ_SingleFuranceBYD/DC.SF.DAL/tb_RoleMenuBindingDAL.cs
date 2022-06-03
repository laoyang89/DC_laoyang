using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DC.SF.DAL.Common;
using System.Collections.Generic;
using DC.SF.Model;
using DC.SF.Common;

namespace DC.SF.DAL
{
    /// <summary>
    /// 数据访问类:tb_RoleMenuBinding
    /// </summary>
    public partial class tb_RoleMenuBindingDAL
    {
        public tb_RoleMenuBindingDAL()
        { }
        #region  BasicMethod

        /// <summary>
        /// 获取所有角色和菜单集合
        /// </summary>
        /// <returns></returns>
        public List<RoleMenuInfo> GetRoleMenuList()
        {
            string sqlstr = @"select t1.Id as RoleId,t1.RoleName,t2.Id as MenuId,t2.MenuName
                                from tb_RoleInfo t1,tb_MenuInfo t2, tb_RoleMenuBinding t3
                               where t1.Id = t3.FK_RoleInfo_Id and t2.Id = t3.FK_MenuInfo_Id    
                                   ";
            DataTable dt = DbHelperSQL.Query(sqlstr).Tables[0];
            return TypeConvertDataModel.TableConvertToList<RoleMenuInfo>(dt);
        }

        public List<RoleMenuInfo> GetRoleMenuList(string strWhere)
        {
            string sqlstr = $@"select t1.Id as RoleId,t1.RoleName,t2.Id as MenuId,t2.MenuName
                                from tb_RoleInfo t1,tb_MenuInfo t2, tb_RoleMenuBinding t3
                               where t1.Id = t3.FK_RoleInfo_Id and t2.Id = t3.FK_MenuInfo_Id 
                                    {strWhere} ;";
            DataTable dt = DbHelperSQL.Query(sqlstr).Tables[0];
            return TypeConvertDataModel.TableConvertToList<RoleMenuInfo>(dt);
        }
       
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(DC.SF.Model.tb_RoleMenuBinding model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tb_RoleMenuBinding(");
            strSql.Append("FK_RoleInfo_Id,FK_MenuInfo_Id)");
            strSql.Append(" values (");
            strSql.Append("@FK_RoleInfo_Id,@FK_MenuInfo_Id)");
            SqlParameter[] parameters = {
                    new SqlParameter("@FK_RoleInfo_Id", SqlDbType.Int,4),
                    new SqlParameter("@FK_MenuInfo_Id", SqlDbType.Int,4)};
            parameters[0].Value = model.FK_RoleInfo_Id;
            parameters[1].Value = model.FK_MenuInfo_Id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(DC.SF.Model.tb_RoleMenuBinding model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_RoleMenuBinding set ");
            strSql.Append("FK_RoleInfo_Id=@FK_RoleInfo_Id,");
            strSql.Append("FK_MenuInfo_Id=@FK_MenuInfo_Id");
            strSql.Append(" where ");
            SqlParameter[] parameters = {
                    new SqlParameter("@FK_RoleInfo_Id", SqlDbType.Int,4),
                    new SqlParameter("@FK_MenuInfo_Id", SqlDbType.Int,4)};
            parameters[0].Value = model.FK_RoleInfo_Id;
            parameters[1].Value = model.FK_MenuInfo_Id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int roleId)
        {
            //该表无主键信息，请自定义主键/条件字段
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tb_RoleMenuBinding ");
            strSql.Append(" where FK_RoleInfo_Id="+roleId);

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DC.SF.Model.tb_RoleMenuBinding GetModel()
        {
            //该表无主键信息，请自定义主键/条件字段
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 FK_RoleInfo_Id,FK_MenuInfo_Id from tb_RoleMenuBinding ");
            strSql.Append(" where ");
            SqlParameter[] parameters = {
            };

            DC.SF.Model.tb_RoleMenuBinding model = new DC.SF.Model.tb_RoleMenuBinding();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DC.SF.Model.tb_RoleMenuBinding DataRowToModel(DataRow row)
        {
            DC.SF.Model.tb_RoleMenuBinding model = new DC.SF.Model.tb_RoleMenuBinding();
            if (row != null)
            {
                if (row["FK_RoleInfo_Id"] != null && row["FK_RoleInfo_Id"].ToString() != "")
                {
                    model.FK_RoleInfo_Id = int.Parse(row["FK_RoleInfo_Id"].ToString());
                }
                if (row["FK_MenuInfo_Id"] != null && row["FK_MenuInfo_Id"].ToString() != "")
                {
                    model.FK_MenuInfo_Id = int.Parse(row["FK_MenuInfo_Id"].ToString());
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select FK_RoleInfo_Id,FK_MenuInfo_Id ");
            strSql.Append(" FROM tb_RoleMenuBinding ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" FK_RoleInfo_Id,FK_MenuInfo_Id ");
            strSql.Append(" FROM tb_RoleMenuBinding ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM tb_RoleMenuBinding ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.Id desc");
            }
            strSql.Append(")AS Row, T.*  from tb_RoleMenuBinding T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@tblName", SqlDbType.VarChar, 255),
					new SqlParameter("@fldName", SqlDbType.VarChar, 255),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@IsReCount", SqlDbType.Bit),
					new SqlParameter("@OrderType", SqlDbType.Bit),
					new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
					};
			parameters[0].Value = "tb_RoleMenuBinding";
			parameters[1].Value = "Id";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}
