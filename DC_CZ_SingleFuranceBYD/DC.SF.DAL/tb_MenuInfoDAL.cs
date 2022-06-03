using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DC.SF.DAL.Common;

namespace DC.SF.DAL
{
    /// <summary>
    /// 数据访问类:tb_MenuInfo
    /// </summary>
    public partial class tb_MenuInfoDAL
    {
        public tb_MenuInfoDAL()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("Id", "tb_MenuInfo");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from tb_MenuInfo");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)
            };
            parameters[0].Value = Id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DC.SF.Model.tb_MenuInfo GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,MenuName,ParentId,IsActived from tb_MenuInfo ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)
            };
            parameters[0].Value = Id;

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
        public DC.SF.Model.tb_MenuInfo DataRowToModel(DataRow row)
        {
            DC.SF.Model.tb_MenuInfo model = new DC.SF.Model.tb_MenuInfo();
            if (row != null)
            {
                if (row["Id"] != null && row["Id"].ToString() != "")
                {
                    model.Id = int.Parse(row["Id"].ToString());
                }
                if (row["MenuName"] != null)
                {
                    model.MenuName = row["MenuName"].ToString();
                }
                if (row["ParentId"] != null && row["ParentId"].ToString() != "")
                {
                    model.ParentId = int.Parse(row["ParentId"].ToString());
                }
                if (row["IsActived"] != null && row["ParentId"].ToString() != "")
                {
                    model.IsActived =bool.Parse(row["IsActived"].ToString());
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string where)
        {
            string sqlstr = "select Id,MenuName,ParentId,IsActived FROM tb_MenuInfo " + where + " order by Id asc";
            return DbHelperSQL.Query(sqlstr);
        }

        public DataSet GetList(int roleId)
        {
            string sqlstr = @" select t1.* 
                                from tb_MenuInfo t1, 
                                     tb_RoleMenuBinding t2,
                                     tb_RoleInfo t3
                               where t1.IsActived=1
                                 and t1.Id = t2.FK_MenuInfo_Id 
                                 and t3.Id = t2.FK_RoleInfo_Id
                                 and t3.Id=@RoleId";
            return DbHelperSQL.Query(sqlstr, new SqlParameter("@RoleId",roleId));
        }
        
        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}
