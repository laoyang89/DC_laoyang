using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
namespace DC.SF.DAL
{
	/// <summary>
	/// 数据访问类:CarDistribution
	/// </summary>
	public partial class CarDistributionDAL
	{
		public CarDistributionDAL()
		{}

		#region  BasicMethod
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long SystemAutoID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from CarDistribution");
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
		public long Add(DC.SF.Model.CarDistribution model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into CarDistribution(");
			strSql.Append("Eno,CarNo,LoadingTime,EnterTime,OutTime,UnloadTime,SamplingTime,CarNoDesc)");
			strSql.Append(" values (");
			strSql.Append("@Eno,@CarNo,@LoadingTime,@EnterTime,@OutTime,@UnloadTime,@SamplingTime,@CarNoDesc)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Eno", SqlDbType.VarChar,20),
					new SqlParameter("@CarNo", SqlDbType.VarChar,50),
					new SqlParameter("@LoadingTime", SqlDbType.DateTime),
					new SqlParameter("@EnterTime", SqlDbType.DateTime),
					new SqlParameter("@OutTime", SqlDbType.DateTime),
					new SqlParameter("@UnloadTime", SqlDbType.DateTime),
					new SqlParameter("@SamplingTime", SqlDbType.DateTime),
                    new SqlParameter("@CarNoDesc", SqlDbType.VarChar,50)};
			parameters[0].Value = model.Eno;
			parameters[1].Value = model.CarNo;
			parameters[2].Value = model.LoadingTime;
			parameters[3].Value = model.EnterTime;
			parameters[4].Value = model.OutTime;
			parameters[5].Value = model.UnloadTime;
			parameters[6].Value = model.SamplingTime;
            parameters[7].Value = model.CarNoDesc;

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
		public bool Update(DC.SF.Model.CarDistribution model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update CarDistribution set ");
			strSql.Append("Eno=@Eno,");
			strSql.Append("CarNo=@CarNo,");
			strSql.Append("LoadingTime=@LoadingTime,");
			strSql.Append("EnterTime=@EnterTime,");
			strSql.Append("OutTime=@OutTime,");
			strSql.Append("UnloadTime=@UnloadTime,");
			strSql.Append("SamplingTime=@SamplingTime,");
            strSql.Append("CarNoDesc=@CarNoDesc");
            strSql.Append(" where SystemAutoID=@SystemAutoID");
			SqlParameter[] parameters = {
					new SqlParameter("@Eno", SqlDbType.VarChar,20),
					new SqlParameter("@CarNo", SqlDbType.VarChar,50),
					new SqlParameter("@LoadingTime", SqlDbType.DateTime),
					new SqlParameter("@EnterTime", SqlDbType.DateTime),
					new SqlParameter("@OutTime", SqlDbType.DateTime),
					new SqlParameter("@UnloadTime", SqlDbType.DateTime),
					new SqlParameter("@SamplingTime", SqlDbType.DateTime),
                    new SqlParameter("@CarNoDesc", SqlDbType.VarChar,50),
                    new SqlParameter("@SystemAutoID", SqlDbType.BigInt,8)};
			parameters[0].Value = model.Eno;
			parameters[1].Value = model.CarNo;
			parameters[2].Value = model.LoadingTime;
			parameters[3].Value = model.EnterTime;
			parameters[4].Value = model.OutTime;
			parameters[5].Value = model.UnloadTime;
			parameters[6].Value = model.SamplingTime;
            parameters[7].Value = model.CarNoDesc;
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
			strSql.Append("delete from CarDistribution ");
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
			strSql.Append("delete from CarDistribution ");
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
		public DC.SF.Model.CarDistribution GetModel(long SystemAutoID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 SystemAutoID,Eno,CarNo,LoadingTime,EnterTime,OutTime,UnloadTime,SamplingTime,CarNoDesc from CarDistribution ");
			strSql.Append(" where SystemAutoID=@SystemAutoID");
			SqlParameter[] parameters = {
					new SqlParameter("@SystemAutoID", SqlDbType.BigInt)
			};
			parameters[0].Value = SystemAutoID;

			DC.SF.Model.CarDistribution model=new DC.SF.Model.CarDistribution();
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
		public DC.SF.Model.CarDistribution DataRowToModel(DataRow row)
		{
			DC.SF.Model.CarDistribution model=new DC.SF.Model.CarDistribution();
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
				if(row["CarNo"]!=null)
				{
					model.CarNo=row["CarNo"].ToString();
				}
				if(row["LoadingTime"]!=null && row["LoadingTime"].ToString()!="")
				{
					model.LoadingTime=DateTime.Parse(row["LoadingTime"].ToString());
				}
				if(row["EnterTime"]!=null && row["EnterTime"].ToString()!="")
				{
					model.EnterTime=DateTime.Parse(row["EnterTime"].ToString());
				}
				if(row["OutTime"]!=null && row["OutTime"].ToString()!="")
				{
					model.OutTime=DateTime.Parse(row["OutTime"].ToString());
				}
				if(row["UnloadTime"]!=null && row["UnloadTime"].ToString()!="")
				{
					model.UnloadTime=DateTime.Parse(row["UnloadTime"].ToString());
				}
				if(row["SamplingTime"]!=null && row["SamplingTime"].ToString()!="")
				{
					model.SamplingTime=DateTime.Parse(row["SamplingTime"].ToString());
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
			strSql.Append("select SystemAutoID,Eno,CarNo,LoadingTime,EnterTime,OutTime,UnloadTime,SamplingTime,CarNoDesc ");
			strSql.Append(" FROM CarDistribution ");
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
			strSql.Append(" SystemAutoID,Eno,CarNo,LoadingTime,EnterTime,OutTime,UnloadTime,SamplingTime,CarNoDesc ");
			strSql.Append(" FROM CarDistribution ");
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
			strSql.Append("select count(1) FROM CarDistribution ");
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
			strSql.Append(")AS Row, T.*  from CarDistribution T ");
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

		#endregion  ExtensionMethod
	}
}

