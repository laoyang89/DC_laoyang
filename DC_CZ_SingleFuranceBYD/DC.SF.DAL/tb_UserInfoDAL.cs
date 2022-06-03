using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DC.SF.DAL.Common;

namespace DC.SF.DAL
{
    /// <summary>
    /// 数据访问类:tb_UserInfo
    /// </summary>
    public partial class tb_UserInfoDAL
    {
        public tb_UserInfoDAL()
        { }
        #region  BasicMethod

        /// <summary>
        /// 检查用户名密码是否正确
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public bool CheckUserAndPwd(string userName,string pwd)
        {
            string sqlstr = "select count(1) from tb_UserInfo where UserName=@UserName and UserPassword=@UserPassword";
            object obj = DbHelperSQL.ExecuteScalar(sqlstr, new SqlParameter("@UserName", userName), new SqlParameter("@UserPassword", pwd));
            int rowCount;
            if(int.TryParse(obj.ToString(),out rowCount))
            {
                return rowCount > 0 ? true : false;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("Id", "tb_UserInfo");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from tb_UserInfo");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)
            };
            parameters[0].Value = Id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(DC.SF.Model.tb_UserInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tb_UserInfo(");
            strSql.Append("UserName,UserPassword,CreateTime,FK_RoleInfo_Id,IsActived,UserIDCard)");
            strSql.Append(" values (");
            strSql.Append("@UserName,@UserPassword,@CreateTime,@FK_RoleInfo_Id,@IsActived,@UserIDCard)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@UserName", SqlDbType.VarChar,50),
                    new SqlParameter("@UserPassword", SqlDbType.VarChar,50),
                    new SqlParameter("@CreateTime", SqlDbType.DateTime),
                    new SqlParameter("@FK_RoleInfo_Id", SqlDbType.Int,4),
                    new SqlParameter("@IsActived",SqlDbType.Bit),
                    new SqlParameter("@UserIDCard", SqlDbType.VarChar,50)
                    };
            parameters[0].Value = model.UserName;
            parameters[1].Value = model.UserPassword;
            parameters[2].Value = model.CreateTime;
            parameters[3].Value = model.FK_RoleInfo_Id;
            parameters[4].Value = model.IsActived;
            parameters[5].Value = model.UserIDCard;
            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
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
        /// 更新一条数据
        /// </summary>
        public bool Update(DC.SF.Model.tb_UserInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_UserInfo set ");
            strSql.Append("UserName=@UserName,");
            strSql.Append("UserPassword=@UserPassword,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("IsActived=@IsActived,");
            strSql.Append("FK_RoleInfo_Id=@FK_RoleInfo_Id,");
            strSql.Append("UserIDCard=@UserIDCard");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
                    new SqlParameter("@UserName", SqlDbType.VarChar,50),
                    new SqlParameter("@UserPassword", SqlDbType.VarChar,50),
                    new SqlParameter("@CreateTime", SqlDbType.DateTime),
                    new SqlParameter("@FK_RoleInfo_Id", SqlDbType.Int,4),
                    new SqlParameter("@Id", SqlDbType.Int,4),
                    new SqlParameter("@IsActived",SqlDbType.Bit),
                    new SqlParameter("@UserIDCard", SqlDbType.VarChar,50)
            };
            parameters[0].Value = model.UserName;
            parameters[1].Value = model.UserPassword;
            parameters[2].Value = model.CreateTime;
            parameters[3].Value = model.FK_RoleInfo_Id;
            parameters[4].Value = model.Id;
            parameters[5].Value = model.IsActived;
            parameters[6].Value = model.UserIDCard ;

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
        public bool Delete(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tb_UserInfo ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)
            };
            parameters[0].Value = Id;

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
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string Idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tb_UserInfo ");
            strSql.Append(" where Id in (" + Idlist + ")  ");
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
        public DC.SF.Model.tb_UserInfo GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,UserName,UserPassword,CreateTime,FK_RoleInfo_Id, UserIDCard from tb_UserInfo ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)
            };
            parameters[0].Value = Id;

            DC.SF.Model.tb_UserInfo model = new DC.SF.Model.tb_UserInfo();
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
        public DC.SF.Model.tb_UserInfo DataRowToModel(DataRow row)
        {
            DC.SF.Model.tb_UserInfo model = new DC.SF.Model.tb_UserInfo();
            if (row != null)
            {
                if (row["Id"] != null && row["Id"].ToString() != "")
                {
                    model.Id = int.Parse(row["Id"].ToString());
                }
                if (row["UserName"] != null)
                {
                    model.UserName = row["UserName"].ToString();
                }
                if (row["UserPassword"] != null)
                {
                    model.UserPassword = row["UserPassword"].ToString();
                }
                if (row["CreateTime"] != null && row["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(row["CreateTime"].ToString());
                }
                if (row["FK_RoleInfo_Id"] != null && row["FK_RoleInfo_Id"].ToString() != "")
                {
                    model.FK_RoleInfo_Id = int.Parse(row["FK_RoleInfo_Id"].ToString());
                }
                if(row["UserIDCard"] != null && row["UserIDCard"].ToString() != "")
                {
                    model.UserIDCard= row["UserIDCard"].ToString();
                }
                if(row["IsActived"] != null && row["IsActived"].ToString() != "")
                {
                    model.IsActived = row["IsActived"].ToString().ToLower() == "true" ? true : false;
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
            strSql.Append("select Id,UserName,UserPassword,CreateTime,FK_RoleInfo_Id,IsActived , UserIDCard");
            strSql.Append(" FROM tb_UserInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取直接展示在界面上的用户列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetUserList(string where)
        {
            string sqlstr = @"select t1.Id,
	                                 t1.UserName as '用户名',
                                     t1.UserIDCard as 'ID卡',
	                                 t2.RoleName as '角色',
	                                 t1.CreateTime as '添加日期',
	                                case t1.IsActived when 1 then '是' when 0 then '否' end as '是否启用'
                               from tb_UserInfo t1 ,tb_RoleInfo t2
                              where t1.FK_RoleInfo_Id = t2.Id and t2.RoleName<>'超级管理员'";
            if (where != "")
            {
                sqlstr += " and UserName like '%" + where + "%'";
            }
            DataTable dt = DbHelperSQL.Query(sqlstr).Tables[0];
            return dt;
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
            strSql.Append(" Id,UserName,UserPassword,CreateTime,FK_RoleInfo_Id,UserIDCard ");
            strSql.Append(" FROM tb_UserInfo ");
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
            strSql.Append("select count(1) FROM tb_UserInfo ");
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
            strSql.Append(")AS Row, T.*  from tb_UserInfo T ");
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
			parameters[0].Value = "tb_UserInfo";
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
        /// <summary>
        /// 根据指定条件获取用户
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DC.SF.Model.tb_UserInfo GetUserModel(string strWhere)
        {
            string sql = $"select * from dbo.tb_UserInfo where 1=1  {strWhere}; ";

            DataSet ds = DbHelperSQL.Query(sql);
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
        /// 根据IDCarID获取用户信息
        /// </summary>
        public DC.SF.Model.tb_UserInfo GetUserByIDCard(string idCard)
        {
            string sql = $"select * from dbo.tb_UserInfo where UserIDCard='{idCard}'; ";

            DataSet ds = DbHelperSQL.Query(sql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }
        public DataSet GetUserList()
        {
            String strSQL = "SELECT tb_UserInfo.UserName FROM tb_UserInfo  ";
            return DbHelperSQL.Query(strSQL);
        }
        #endregion  ExtensionMethod
    }
}
