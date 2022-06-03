using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DC.SF.DAL.Common;
using DC.SF.Common;

namespace DC.SF.DAL
{
    /// <summary>
    /// 数据访问类:tb_OperateRecord
    /// </summary>
    public partial class tb_OperateRecordDAL
    {
        public tb_OperateRecordDAL()
        { }
        #region  BasicMethod
        


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(DC.SF.Model.tb_OperateRecord model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tb_OperateRecord(");
            strSql.Append("RecordTime,EmployeeName,OperateContent,EmployeeID)");
            strSql.Append(" values (");
            strSql.Append("@RecordTime,@EmployeeName,@OperateContent,@EmployeeID)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@RecordTime", SqlDbType.DateTime),
                    new SqlParameter("@EmployeeName", SqlDbType.VarChar,50),
                    new SqlParameter("@OperateContent", SqlDbType.VarChar,256),
                    new SqlParameter("@EmployeeID", SqlDbType.VarChar,50)
            };
            parameters[0].Value = model.RecordTime;
            parameters[1].Value = model.EmployeeName;
            parameters[2].Value = model.OperateContent;
            parameters[3].Value = model.EmployeeID;

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


        public bool ClearDBLog()
        {
            string sqlstr = @" use master  go
                                alter database DB_SingleFurance set recovery simple with no_wait
                                alter database DB_SingleFurance set recovery simple

                                use DB_SingleFurance  go
                                dbcc shrinkfile('DB_SingleFurance_log',10,truncateonly)

                                use master  go
                                alter database DB_SingleFurance set recovery full with no_wait
                                alter database DB_SingleFurance set recovery full";
            try
            {
                DbHelperSQL.ExecuteSql(sqlstr);
                return true;
            }
            catch(Exception ex)
            {
                LogHelper.Current.WriteEx("清除db 日志异常", ex);
                return false;
            }
        }

        

        

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,RecordTime,EmployeeName,OperateContent,EmployeeID ");
            strSql.Append(" FROM tb_OperateRecord ");
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
            strSql.Append(" Id,RecordTime,EmployeeName,OperateContent,EmployeeID ");
            strSql.Append(" FROM tb_OperateRecord ");
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
            strSql.Append("select count(1) FROM tb_OperateRecord ");
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
            strSql.Append(")AS Row, T.*  from tb_OperateRecord T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }



        #endregion  BasicMethod
        #region  ExtensionMethod
        public DataSet PagingQuery(DateTime startTime, DateTime endTime, int currentIndex, int pageSize, string userName, string opType)
        {
            DateTime dtStart = Convert.ToDateTime(startTime.ToShortDateString() + " 00:00:00");
            DateTime dtEnd = Convert.ToDateTime(endTime.ToString("yyyy-MM-dd") + " 23:59:59");
            string sqlWhere = string.Format(" and RecordTime >= '{0}' and RecordTime <= '{1}' ", dtStart.ToString("yyyy-MM-dd HH:mm:ss"), dtEnd.ToString("yyyy-MM-dd HH:mm:ss"));
            int startIndex = (currentIndex - 1) * pageSize;
            int endIndex = currentIndex * pageSize;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from tb_OperateRecord t1 where 1=1 " + sqlWhere);
            strSql.Append(string.Format(@" select t3.rownum as 行号, t1.RecordTime as 记录时间, t1.OperateContent as 操作内容, t1.EmployeeName as 操作员
            from tb_OperateRecord t1, (select ROW_NUMBER() over(order by RecordTime desc) rownum, id from tb_OperateRecord) t3
            where t3.Id = t1.Id "));
            strSql.Append(sqlWhere);
            strSql.Append(string.Format(" and t3.rownum >= {0} and t3.rownum <= {1}", startIndex, endIndex));
            if (userName!="")
            {
                strSql.Append($" and t1.EmployeeName='{userName}'");
            }
            if (opType!="")
            {
                strSql.Append($" and t1.OperateContent='{opType}'");
            }
            
            return DbHelperSQL.Query(strSql.ToString());
        }
        public DataSet OpTypeList()
        {
            String strSQL = "SELECT tb_OperateRecord.OperateContent FROM tb_OperateRecord group by tb_OperateRecord.OperateContent";
            return DbHelperSQL.Query(strSQL);
        }

        public void ClearDBData()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(" delete from tb_CellInfo ");
            sb.AppendLine(" delete from tb_TemperatureInfo ");
            sb.AppendLine(" delete from tb_CarrierInfo ");
            sb.AppendLine(" delete from tb_OperateRecord ");

            string sqlstr = sb.ToString();
            DbHelperSQL.Query(sqlstr);
        }


        #endregion  ExtensionMethod
    }
}
