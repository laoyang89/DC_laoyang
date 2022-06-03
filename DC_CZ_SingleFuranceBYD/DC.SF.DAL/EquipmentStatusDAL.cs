using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
namespace DC.SF.DAL
{
	/// <summary>
	/// 数据访问类:EquipmentStatus
	/// </summary>
	public partial class EquipmentStatusDAL
	{
		public EquipmentStatusDAL()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long SystemAutoID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from EquipmentStatus");
			strSql.Append(" where SystemAutoID=@SystemAutoID");
			SqlParameter[] parameters = {
					new SqlParameter("@SystemAutoID", SqlDbType.BigInt)
			};
			parameters[0].Value = SystemAutoID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public long Add(DC.SF.Model.EquipmentStatus model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into EquipmentStatus(");
			strSql.Append("Eno,StartStatus,EndStatus,Stime,Etime,Remark)");
			strSql.Append(" values (");
			strSql.Append("@Eno,@StartStatus,@EndStatus,@Stime,@Etime,@Remark)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Eno", SqlDbType.VarChar,20),
					new SqlParameter("@StartStatus", SqlDbType.VarChar,20),
                    new SqlParameter("@EndStatus", SqlDbType.VarChar,20),
                    new SqlParameter("@Stime", SqlDbType.DateTime),
                    new SqlParameter("@Etime", SqlDbType.DateTime),
                    new SqlParameter("@Remark", SqlDbType.VarChar,50)};
			parameters[0].Value = model.Eno;
			parameters[1].Value = model.StartStatus;
            parameters[2].Value = model.EndStatus;
            parameters[3].Value = model.Stime;
            parameters[4].Value = model.Etime;
            parameters[5].Value = model.Remark;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt64(obj);
			}
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(DC.SF.Model.EquipmentStatus model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update EquipmentStatus set ");
			strSql.Append("Eno=@Eno,");
			strSql.Append("StartStatus=@StartStatus,");
            strSql.Append("EndStatus=@EndStatus,");
            strSql.Append("Stime=@Stime,");
            strSql.Append("Etime=@Etime,");
            strSql.Append("Remark=@Remark");
			strSql.Append(" where SystemAutoID=@SystemAutoID");
			SqlParameter[] parameters = {
					new SqlParameter("@Eno", SqlDbType.VarChar,20),
					new SqlParameter("@StartStatus", SqlDbType.VarChar,20),
                    new SqlParameter("@EndStatus", SqlDbType.VarChar,20),
                    new SqlParameter("@Stime", SqlDbType.DateTime),
                    new SqlParameter("@Etime", SqlDbType.DateTime),
                    new SqlParameter("@Remark", SqlDbType.VarChar,50),
					new SqlParameter("@SystemAutoID", SqlDbType.BigInt,8)};
			parameters[0].Value = model.Eno;
			parameters[1].Value = model.StartStatus;
            parameters[2].Value = model.EndStatus;
            parameters[3].Value = model.Stime;
            parameters[4].Value = model.Etime;
			parameters[5].Value = model.Remark;
			parameters[6].Value = model.SystemAutoID;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
		public bool Delete(long SystemAutoID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from EquipmentStatus ");
			strSql.Append(" where SystemAutoID=@SystemAutoID");
			SqlParameter[] parameters = {
					new SqlParameter("@SystemAutoID", SqlDbType.BigInt)
			};
			parameters[0].Value = SystemAutoID;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
		public bool DeleteList(string SystemAutoIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from EquipmentStatus ");
			strSql.Append(" where SystemAutoID in ("+SystemAutoIDlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
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
		public DC.SF.Model.EquipmentStatus GetModel(long SystemAutoID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 SystemAutoID,Eno,StartStatus,EndStatus,Stime,Etime,Remark from EquipmentStatus ");
			strSql.Append(" where SystemAutoID=@SystemAutoID");
			SqlParameter[] parameters = {
					new SqlParameter("@SystemAutoID", SqlDbType.BigInt)
			};
			parameters[0].Value = SystemAutoID;

			DC.SF.Model.EquipmentStatus model=new DC.SF.Model.EquipmentStatus();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
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
		public DC.SF.Model.EquipmentStatus DataRowToModel(DataRow row)
		{
			DC.SF.Model.EquipmentStatus model=new DC.SF.Model.EquipmentStatus();
			if (row != null)
			{
				if(row["SystemAutoID"]!=null && row["SystemAutoID"].ToString()!="")
				{
					model.SystemAutoID=long.Parse(row["SystemAutoID"].ToString());
				}
				if(row["Eno"]!=null)
				{
					model.Eno=row["Eno"].ToString();
				}
				if(row["StartStatus"] !=null)
				{
					model.StartStatus=row["StartStatus"].ToString();
				}
                if (row["EndStatus"] != null)
                {
                    model.EndStatus = row["EndStatus"].ToString();
                }
                if (row["Stime"] != null && row["Stime"].ToString() != "")
                {
                    model.Stime = DateTime.Parse(row["Stime"].ToString());
                }
                if (row["Etime"]!=null && row["Etime"].ToString()!="")
				{
					model.Etime = DateTime.Parse(row["Etime"].ToString());
				}
				if(row["Remark"]!=null)
				{
					model.Remark=row["Remark"].ToString();
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select SystemAutoID,Eno,StartStatus,EndStatus,Stime,Etime,Remark ");
			strSql.Append(" FROM EquipmentStatus ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" SystemAutoID,Eno,StartStatus,EndStatus,Stime,Etime,Remark ");
			strSql.Append(" FROM EquipmentStatus ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM EquipmentStatus ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.SystemAutoID desc");
			}
			strSql.Append(")AS Row, T.*  from EquipmentStatus T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE  1=1 " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}
        /// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string startTime,string endTime,  int startIndex, int endIndex, ref int dcount)
        {
            string strWhere = $" AND Stime>='{startTime}' AND Etime<='{endTime}' ";
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT SystemAutoID ,Eno as '设备编号',StartStatus as '起始状态',EndStatus as '结束状态',Stime as '状态开始时间',Etime as '状态结束时间',Remark as '备注' ,  DateDiff(SS,Stime,Etime) as '相隔时间/s' , " +
                " case when isnull(Etime,'')='' then Stime else   Etime end as '添加时间' FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");

            strSql.Append("order by T.SystemAutoID ");

            strSql.Append(")AS Row, T.*  from EquipmentStatus T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE  1=1 " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1} ;", startIndex, endIndex);
            string sqlCount = $"SELECT COUNT(1) AS countNum FROM  EquipmentStatus  WHERE 1=1  {strWhere} ;";
            strSql.Append(sqlCount);
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            dcount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            return ds;
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
			parameters[0].Value = "EquipmentStatus";
			parameters[1].Value = "SystemAutoID";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

        #endregion  BasicMethod
        #region  ExtensionMethod
        public DC.SF.Model.EquipmentStatus GetMaxTimeData(string strWhere)
        {
            string sqlStr = $"SELECT * FROM EquipmentStatus WHERE Stime = (SELECT MAX(Stime) FROM EquipmentStatus where 1=1  {strWhere})  {strWhere}  ";
            DataSet ds = DbHelperSQL.Query(sqlStr);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }
        #endregion  ExtensionMethod
    }
}

