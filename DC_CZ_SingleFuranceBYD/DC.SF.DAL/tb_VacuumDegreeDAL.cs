using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DC.SF.DAL.Common;

namespace DC.SF.DAL
{
	/// <summary>
	/// 数据访问类:tb_VacuumDegree
	/// </summary>
	public partial class tb_VacuumDegreeDAL
	{
		public tb_VacuumDegreeDAL()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("Id", "tb_VacuumDegree"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from tb_VacuumDegree");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
			parameters[0].Value = Id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(DC.SF.Model.tb_VacuumDegree model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into tb_VacuumDegree(");
			strSql.Append("RecordTime,StationNum,VacuumValue,CarrierId)");
			strSql.Append(" values (");
			strSql.Append("@RecordTime,@StationNum,@VacuumValue,@CarrierId)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@RecordTime", SqlDbType.DateTime),
					new SqlParameter("@StationNum", SqlDbType.Int,4),
					new SqlParameter("@VacuumValue", SqlDbType.Float,8),
					new SqlParameter("@CarrierId", SqlDbType.Int,4)};
			parameters[0].Value = model.RecordTime;
			parameters[1].Value = model.StationNum;
			parameters[2].Value = model.VacuumValue;
			parameters[3].Value = model.CarrierId;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}


        public DataTable GetDegree(string carrierId, string stationNum, DateTime? dtBegin, DateTime? dtEnd, int PageIndex, int PageCount, ref int totalRecord)
        {
            string sqlWhere = "";
            sqlWhere += carrierId == "" ? "" : " and CarrierNum =" + carrierId;
            sqlWhere += stationNum == "" ? "" : " and StationNum = " + stationNum;
            sqlWhere += dtBegin == null ? "" : string.Format(" and RecordTime > '{0}' ", dtBegin.Value.ToString("yyyy-MM-dd 00:00:00"));
            sqlWhere += dtEnd == null ? "" : string.Format(" and RecordTime < '{0}' ", dtEnd.Value.ToString("yyyy-MM-dd 23:59:59"));

            int beginRecord = (PageIndex - 1) * PageCount + 1;
            int endRecord = PageIndex * PageCount;

            string sqlstr = "", sqlCount = "";
            sqlstr = string.Format(@" select RecordTime '记录时间',
	                                        CarrierNum '小车号',
                                      	    StationNum-1001 '腔体编号',
                                      	    VacuumValue '真空度'
                                      	    from(
                                                select t2.*,t1.CarrierNum,ROW_NUMBER() over(order by t2.RecordTime asc) rowNo from tb_CarrierInfo t1,tb_VacuumDegree t2
                                                 where t1.Id = t2.CarrierId {0} )tt
                                      where tt.rowNo between {1} and {2} ", sqlWhere,beginRecord,endRecord);

            sqlCount = string.Format(@"select COUNT(1) from tb_CarrierInfo t1,tb_VacuumDegree t2
                                    where t1.Id = t2.CarrierId {0} ", sqlWhere);


            DataTable dt = new DataTable();
            
            dt = DbHelperSQL.Query(sqlstr).Tables[0];
            totalRecord = Convert.ToInt32(DbHelperSQL.ExecuteScalar(sqlCount));
            return dt;



        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(DC.SF.Model.tb_VacuumDegree model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update tb_VacuumDegree set ");
			strSql.Append("RecordTime=@RecordTime,");
			strSql.Append("StationNum=@StationNum,");
			strSql.Append("VacuumValue=@VacuumValue,");
			strSql.Append("CarrierId=@CarrierId");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@RecordTime", SqlDbType.DateTime),
					new SqlParameter("@StationNum", SqlDbType.Int,4),
					new SqlParameter("@VacuumValue", SqlDbType.Float,8),
					new SqlParameter("@CarrierId", SqlDbType.Int,4),
					new SqlParameter("@Id", SqlDbType.Int,4)};
			parameters[0].Value = model.RecordTime;
			parameters[1].Value = model.StationNum;
			parameters[2].Value = model.VacuumValue;
			parameters[3].Value = model.CarrierId;
			parameters[4].Value = model.Id;

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
		public bool Delete(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from tb_VacuumDegree ");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
			parameters[0].Value = Id;

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
		public bool DeleteList(string Idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from tb_VacuumDegree ");
			strSql.Append(" where Id in ("+Idlist + ")  ");
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
		public DC.SF.Model.tb_VacuumDegree GetModel(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Id,RecordTime,StationNum,VacuumValue,CarrierId from tb_VacuumDegree ");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
			parameters[0].Value = Id;

			DC.SF.Model.tb_VacuumDegree model=new DC.SF.Model.tb_VacuumDegree();
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
		public DC.SF.Model.tb_VacuumDegree DataRowToModel(DataRow row)
		{
			DC.SF.Model.tb_VacuumDegree model=new DC.SF.Model.tb_VacuumDegree();
			if (row != null)
			{
				if(row["Id"]!=null && row["Id"].ToString()!="")
				{
					model.Id=int.Parse(row["Id"].ToString());
				}
				if(row["RecordTime"]!=null && row["RecordTime"].ToString()!="")
				{
					model.RecordTime=DateTime.Parse(row["RecordTime"].ToString());
				}
				if(row["StationNum"]!=null && row["StationNum"].ToString()!="")
				{
					model.StationNum=int.Parse(row["StationNum"].ToString());
				}
				if(row["VacuumValue"]!=null && row["VacuumValue"].ToString()!="")
				{
					model.VacuumValue=decimal.Parse(row["VacuumValue"].ToString());
				}
				if(row["CarrierId"]!=null && row["CarrierId"].ToString()!="")
				{
					model.CarrierId=int.Parse(row["CarrierId"].ToString());
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
			strSql.Append("select Id,RecordTime,StationNum,VacuumValue,CarrierId ");
			strSql.Append(" FROM tb_VacuumDegree ");
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
			strSql.Append(" Id,RecordTime,StationNum,VacuumValue,CarrierId ");
			strSql.Append(" FROM tb_VacuumDegree ");
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
			strSql.Append("select count(1) FROM tb_VacuumDegree ");
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
				strSql.Append("order by T.Id desc");
			}
			strSql.Append(")AS Row, T.*  from tb_VacuumDegree T ");
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
			parameters[0].Value = "tb_VacuumDegree";
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

