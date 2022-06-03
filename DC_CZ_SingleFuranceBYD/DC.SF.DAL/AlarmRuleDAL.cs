using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DC.SF.DAL.Common;
using DC.SF.Model;

namespace DC.SF.DAL
{
	/// <summary>
	/// 数据访问类:AlarmRule
	/// </summary>
	public partial class AlarmRuleDAL
	{
		public AlarmRuleDAL()
		{}
        public bool Exists(int SystemAutoID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from AlarmRule");
            strSql.Append(" where ");
            strSql.Append(" SystemAutoID = @SystemAutoID  ");
            SqlParameter[] parameters = {
                    new SqlParameter("@SystemAutoID", SqlDbType.Int,4)
            };
            parameters[0].Value = SystemAutoID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(DC.SF.Model.AlarmRule model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into AlarmRule(");
            strSql.Append("AlarmAddress,AlarmIndex,AlarmContent,WorkStationNo");
            strSql.Append(") values (");
            strSql.Append("@AlarmAddress,@AlarmIndex,@AlarmContent,@WorkStationNo");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@AlarmAddress", SqlDbType.Int,4) ,
                        new SqlParameter("@AlarmIndex", SqlDbType.Int,4) ,
                        new SqlParameter("@AlarmContent", SqlDbType.VarChar,500) ,
                        new SqlParameter("@WorkStationNo", SqlDbType.VarChar,50)

            };
            parameters[0].Value = model.AlarmAddress;
            parameters[1].Value = model.AlarmIndex;
            parameters[2].Value = model.AlarmContent;
            parameters[3].Value = model.WorkStationNo;

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
        public bool Update(DC.SF.Model.AlarmRule model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AlarmRule set ");

            strSql.Append(" AlarmAddress = @AlarmAddress , ");
            strSql.Append(" AlarmIndex = @AlarmIndex , ");
            strSql.Append(" AlarmContent = @AlarmContent , ");
            strSql.Append(" WorkStationNo = @WorkStationNo  ");
            strSql.Append(" where SystemAutoID=@SystemAutoID ");

            SqlParameter[] parameters = {
                        new SqlParameter("@SystemAutoID", SqlDbType.Int,4) ,
                        new SqlParameter("@AlarmAddress", SqlDbType.Int,4) ,
                        new SqlParameter("@AlarmIndex", SqlDbType.Int,4) ,
                        new SqlParameter("@AlarmContent", SqlDbType.VarChar,500) ,
                        new SqlParameter("@WorkStationNo", SqlDbType.VarChar,50)

            };

            parameters[0].Value = model.SystemAutoID;
            parameters[1].Value = model.AlarmAddress;
            parameters[2].Value = model.AlarmIndex;
            parameters[3].Value = model.AlarmContent;
            parameters[4].Value = model.WorkStationNo;
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
        public bool Delete(int SystemAutoID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from AlarmRule ");
            strSql.Append(" where SystemAutoID=@SystemAutoID");
            SqlParameter[] parameters = {
                    new SqlParameter("@SystemAutoID", SqlDbType.Int,4)
            };
            parameters[0].Value = SystemAutoID;


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
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string SystemAutoIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from AlarmRule ");
            strSql.Append(" where ID in (" + SystemAutoIDlist + ")  ");
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
        public DC.SF.Model.AlarmRule GetModel(int SystemAutoID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SystemAutoID, AlarmAddress, AlarmIndex, AlarmContent, WorkStationNo  ");
            strSql.Append("  from AlarmRule ");
            strSql.Append(" where SystemAutoID=@SystemAutoID");
            SqlParameter[] parameters = {
                    new SqlParameter("@SystemAutoID", SqlDbType.Int,4)
            };
            parameters[0].Value = SystemAutoID;


            DC.SF.Model.AlarmRule model = new DC.SF.Model.AlarmRule();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["SystemAutoID"].ToString() != "")
                {
                    model.SystemAutoID = int.Parse(ds.Tables[0].Rows[0]["SystemAutoID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AlarmAddress"].ToString() != "")
                {
                    model.AlarmAddress = int.Parse(ds.Tables[0].Rows[0]["AlarmAddress"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AlarmIndex"].ToString() != "")
                {
                    model.AlarmIndex = int.Parse(ds.Tables[0].Rows[0]["AlarmIndex"].ToString());
                }
                model.AlarmContent = ds.Tables[0].Rows[0]["AlarmContent"].ToString();
                model.WorkStationNo = ds.Tables[0].Rows[0]["WorkStationNo"].ToString();

                return model;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM AlarmRule ");
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
            strSql.Append(" * ");
            strSql.Append(" FROM AlarmRule ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}

