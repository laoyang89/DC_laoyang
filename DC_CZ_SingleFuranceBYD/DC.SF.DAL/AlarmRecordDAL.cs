using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
namespace DC.SF.DAL
{
	/// <summary>
	/// 数据访问类:AlarmRecord
	/// </summary>
	public partial class AlarmRecordDAL
	{
		public AlarmRecordDAL()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long SystemAutoID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from AlarmRecord");
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
		public long Add(DC.SF.Model.AlarmRecord model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into AlarmRecord(");
			strSql.Append("Eno,Status,AlarmCode,Stime,Etime,Remark,AlarmDesc,CarSystemID)");
			strSql.Append(" values (");
			strSql.Append("@Eno,@Status,@AlarmCode,@Stime,@Etime,@Remark,@AlarmDesc,@CarSystemID)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Eno", SqlDbType.VarChar,20),
					new SqlParameter("@Status", SqlDbType.VarChar,10),
					new SqlParameter("@AlarmCode", SqlDbType.VarChar,20),
					new SqlParameter("@Stime", SqlDbType.DateTime),
                    new SqlParameter("@Etime", SqlDbType.DateTime),
                    new SqlParameter("@Remark", SqlDbType.VarChar,50),
                    new SqlParameter("@AlarmDesc", SqlDbType.VarChar,20),
                    new SqlParameter("@CarSystemID", SqlDbType.BigInt,8)};
			parameters[0].Value = model.Eno;
			parameters[1].Value = model.Status;
			parameters[2].Value = model.AlarmCode;
			parameters[3].Value = model.Stime;
            parameters[4].Value = model.Etime;
            parameters[5].Value = model.Remark;
            parameters[6].Value = model.AlarmDesc;
            parameters[7].Value = model.CarSystemID;

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
		public bool Update(DC.SF.Model.AlarmRecord model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update AlarmRecord set ");
			strSql.Append("Eno=@Eno,");
			strSql.Append("Status=@Status,");
			strSql.Append("AlarmCode=@AlarmCode,");
			strSql.Append("Stime=@Stime,");
            strSql.Append("Etime=@Etime,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("AlarmDesc=@AlarmDesc,");
            strSql.Append("CarSystemID=@CarSystemID");
            strSql.Append(" where SystemAutoID=@SystemAutoID");
			SqlParameter[] parameters = {
					new SqlParameter("@Eno", SqlDbType.VarChar,20),
					new SqlParameter("@Status", SqlDbType.VarChar,10),
					new SqlParameter("@AlarmCode", SqlDbType.VarChar,20),
					new SqlParameter("@Stime", SqlDbType.DateTime),
                    new SqlParameter("@Etime", SqlDbType.DateTime),
                    new SqlParameter("@Remark", SqlDbType.VarChar,50),
                    new SqlParameter("@AlarmDesc", SqlDbType.VarChar,20),
                    new SqlParameter("@CarSystemID", SqlDbType.BigInt,8),
                    new SqlParameter("@SystemAutoID", SqlDbType.BigInt,8)};
			parameters[0].Value = model.Eno;
			parameters[1].Value = model.Status;
			parameters[2].Value = model.AlarmCode;
			parameters[3].Value = model.Stime;
            parameters[4].Value = model.Etime;
            parameters[5].Value = model.Remark;
            parameters[6].Value = model.AlarmDesc;
            parameters[7].Value = model.CarSystemID;
            parameters[8].Value = model.SystemAutoID;
           

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
			strSql.Append("delete from AlarmRecord ");
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
			strSql.Append("delete from AlarmRecord ");
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
		public DC.SF.Model.AlarmRecord GetModel(long SystemAutoID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 *  from AlarmRecord ");//SystemAutoID,Eno,Status,AlarmCode,Stime,Remark,AlarmDesc,
            strSql.Append(" where SystemAutoID=@SystemAutoID");
			SqlParameter[] parameters = {
					new SqlParameter("@SystemAutoID", SqlDbType.BigInt)
			};
			parameters[0].Value = SystemAutoID;

			DC.SF.Model.AlarmRecord model=new DC.SF.Model.AlarmRecord();
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
		public DC.SF.Model.AlarmRecord DataRowToModel(DataRow row)
		{
			DC.SF.Model.AlarmRecord model=new DC.SF.Model.AlarmRecord();
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
				if(row["Status"]!=null)
				{
					model.Status=row["Status"].ToString();
				}
				if(row["AlarmCode"]!=null)
				{
					model.AlarmCode=row["AlarmCode"].ToString();
				}

				if(row["Stime"]!=null && row["Stime"].ToString()!="")
				{
					model.Stime=DateTime.Parse(row["Stime"].ToString());
				}
                if (row["Etime"] != null && row["Etime"].ToString() != "")
                {
                    model.Etime = DateTime.Parse(row["Etime"].ToString());
                }
                if (row["Remark"]!=null)
				{
					model.Remark=row["Remark"].ToString();
				}
                if (row["CarSystemID"] != null && row["CarSystemID"].ToString() != "")
                {
                    model.CarSystemID = long.Parse(row["CarSystemID"].ToString());
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
			strSql.Append("select  * ");//SystemAutoID,Eno,Status,AlarmCode,Stime,Remark,AlarmDesc,CarSystemAutoID
            strSql.Append(" FROM AlarmRecord ");
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
			strSql.Append("  *  ");//SystemAutoID,Eno,Status,AlarmCode,Stime,Remark,AlarmDesc,CarSystemAutoID
            strSql.Append(" FROM AlarmRecord ");
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
			strSql.Append("select count(1) FROM AlarmRecord ");
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
			strSql.Append(")AS Row, T.*  from AlarmRecord T ");
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
			parameters[0].Value = "AlarmRecord";
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
        public DataSet GetAlarmData(string strWhere,int startIndex,int endIndex)
        {
            string strSql = @"SELECT 
                                      ROW_NUMBER() OVER (ORDER BY t1.Stime ASC) AS Row,
                                      t1.Eno as '设备编号' ,
                                      t1.Stime '报警开始时间',
                                      t1.Etime '报警结束时间',
                                      t1.AlarmDesc as'报警编号',
                                      t1.Remark as'报警状态',
                                      t2.AlarmContent as '报警内容' ,
                                      t1.CarSystemID as '小车ID',
                                      t3.CarNo as '小车编号' 
                                      FROM dbo.AlarmRecord t1 inner join dbo.AlarmRule t2 on t1.AlarmCode=t2.SystemAutoID 
                                      left join dbo.CarDistribution t3 on t1.CarSystemID=t3.SystemAutoID WHERE 1=1 " + strWhere + "  ";
            strSql = $"SELECT * FROM ( { strSql } ) TT  WHERE TT.Row between {startIndex} and {endIndex}";

            strSql+= "  ;select count(1) rcount from  dbo.AlarmRecord t1 left join dbo.CarDistribution t3 on t1.CarSystemID=t3.SystemAutoID  where 1=1  " + strWhere;
            DataSet ds= DbHelperSQL.Query(strSql);
            return ds;
        }
		#endregion  ExtensionMethod
	}
}

