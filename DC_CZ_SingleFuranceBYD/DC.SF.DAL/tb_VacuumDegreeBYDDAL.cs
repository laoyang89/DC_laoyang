using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DC.SF.DAL.Common;
using DC.SF.Model;

namespace DC.SF.DAL
{
	/// <summary>
	/// 数据访问类:tb_VacuumDegreeBYD
	/// </summary>
	public partial class tb_VacuumDegreeBYDDAL
	{
		public tb_VacuumDegreeBYDDAL()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("Id", "tb_VacuumDegreeBYD"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from tb_VacuumDegreeBYD");
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
		public int Add(tb_VacuumDegreeBYD model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into tb_VacuumDegreeBYD(");
			strSql.Append("RecordTime,StationNum,VacuumValue,ChuDian1,ChuDian2,ChuDian3,ChuDian4,ChuDian5,ChuDian6,ChuDian7,ChuDian8,ChuDian9,ChuDian10,ChuDian11,ChuDian12,CarrierId)");
			strSql.Append(" values (");
			strSql.Append("@RecordTime,@StationNum,@VacuumValue,@ChuDian1,@ChuDian2,@ChuDian3,@ChuDian4,@ChuDian5,@ChuDian6,@ChuDian7,@ChuDian8,@ChuDian9,@ChuDian10,@ChuDian11,@ChuDian12,@CarrierId)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@RecordTime", SqlDbType.DateTime),
					new SqlParameter("@StationNum", SqlDbType.Int,4),
					new SqlParameter("@VacuumValue", SqlDbType.Float,8),
					new SqlParameter("@ChuDian1", SqlDbType.Float,8),
					new SqlParameter("@ChuDian2", SqlDbType.Float,8),
					new SqlParameter("@ChuDian3", SqlDbType.Float,8),
					new SqlParameter("@ChuDian4", SqlDbType.Float,8),
					new SqlParameter("@ChuDian5", SqlDbType.Float,8),
					new SqlParameter("@ChuDian6", SqlDbType.Float,8),
					new SqlParameter("@ChuDian7", SqlDbType.Float,8),
					new SqlParameter("@ChuDian8", SqlDbType.Float,8),
					new SqlParameter("@ChuDian9", SqlDbType.Float,8),
					new SqlParameter("@ChuDian10", SqlDbType.Float,8),
					new SqlParameter("@ChuDian11", SqlDbType.Float,8),
					new SqlParameter("@ChuDian12", SqlDbType.Float,8),
					new SqlParameter("@CarrierId", SqlDbType.Int,4)};
			parameters[0].Value = model.RecordTime;
			parameters[1].Value = model.StationNum;
			parameters[2].Value = model.VacuumValue;
			parameters[3].Value = model.ChuDian1;
			parameters[4].Value = model.ChuDian2;
			parameters[5].Value = model.ChuDian3;
			parameters[6].Value = model.ChuDian4;
			parameters[7].Value = model.ChuDian5;
			parameters[8].Value = model.ChuDian6;
			parameters[9].Value = model.ChuDian7;
			parameters[10].Value = model.ChuDian8;
			parameters[11].Value = model.ChuDian9;
			parameters[12].Value = model.ChuDian10;
			parameters[13].Value = model.ChuDian11;
			parameters[14].Value = model.ChuDian12;
			parameters[15].Value = model.CarrierId;

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
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(tb_VacuumDegreeBYD model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update tb_VacuumDegreeBYD set ");
			strSql.Append("RecordTime=@RecordTime,");
			strSql.Append("StationNum=@StationNum,");
			strSql.Append("VacuumValue=@VacuumValue,");
			strSql.Append("ChuDian1=@ChuDian1,");
			strSql.Append("ChuDian2=@ChuDian2,");
			strSql.Append("ChuDian3=@ChuDian3,");
			strSql.Append("ChuDian4=@ChuDian4,");
			strSql.Append("ChuDian5=@ChuDian5,");
			strSql.Append("ChuDian6=@ChuDian6,");
			strSql.Append("ChuDian7=@ChuDian7,");
			strSql.Append("ChuDian8=@ChuDian8,");
			strSql.Append("ChuDian9=@ChuDian9,");
			strSql.Append("ChuDian10=@ChuDian10,");
			strSql.Append("ChuDian11=@ChuDian11,");
			strSql.Append("ChuDian12=@ChuDian12,");
			strSql.Append("CarrierId=@CarrierId");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@RecordTime", SqlDbType.DateTime),
					new SqlParameter("@StationNum", SqlDbType.Int,4),
					new SqlParameter("@VacuumValue", SqlDbType.Float,8),
					new SqlParameter("@ChuDian1", SqlDbType.Float,8),
					new SqlParameter("@ChuDian2", SqlDbType.Float,8),
					new SqlParameter("@ChuDian3", SqlDbType.Float,8),
					new SqlParameter("@ChuDian4", SqlDbType.Float,8),
					new SqlParameter("@ChuDian5", SqlDbType.Float,8),
					new SqlParameter("@ChuDian6", SqlDbType.Float,8),
					new SqlParameter("@ChuDian7", SqlDbType.Float,8),
					new SqlParameter("@ChuDian8", SqlDbType.Float,8),
					new SqlParameter("@ChuDian9", SqlDbType.Float,8),
					new SqlParameter("@ChuDian10", SqlDbType.Float,8),
					new SqlParameter("@ChuDian11", SqlDbType.Float,8),
					new SqlParameter("@ChuDian12", SqlDbType.Float,8),
					new SqlParameter("@CarrierId", SqlDbType.Int,4),
					new SqlParameter("@Id", SqlDbType.Int,4)};
			parameters[0].Value = model.RecordTime;
			parameters[1].Value = model.StationNum;
			parameters[2].Value = model.VacuumValue;
			parameters[3].Value = model.ChuDian1;
			parameters[4].Value = model.ChuDian2;
			parameters[5].Value = model.ChuDian3;
			parameters[6].Value = model.ChuDian4;
			parameters[7].Value = model.ChuDian5;
			parameters[8].Value = model.ChuDian6;
			parameters[9].Value = model.ChuDian7;
			parameters[10].Value = model.ChuDian8;
			parameters[11].Value = model.ChuDian9;
			parameters[12].Value = model.ChuDian10;
			parameters[13].Value = model.ChuDian11;
			parameters[14].Value = model.ChuDian12;
			parameters[15].Value = model.CarrierId;
			parameters[16].Value = model.Id;

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
			strSql.Append("delete from tb_VacuumDegreeBYD ");
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
			strSql.Append("delete from tb_VacuumDegreeBYD ");
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
		public tb_VacuumDegreeBYD GetModel(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Id,RecordTime,StationNum,VacuumValue,ChuDian1,ChuDian2,ChuDian3,ChuDian4,ChuDian5,ChuDian6,ChuDian7,ChuDian8,ChuDian9,ChuDian10,ChuDian11,ChuDian12,CarrierId from tb_VacuumDegreeBYD ");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
			parameters[0].Value = Id;

			tb_VacuumDegreeBYD model=new tb_VacuumDegreeBYD();
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
		public tb_VacuumDegreeBYD DataRowToModel(DataRow row)
		{
			tb_VacuumDegreeBYD model=new tb_VacuumDegreeBYD();
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
				if(row["ChuDian1"]!=null && row["ChuDian1"].ToString()!="")
				{
					model.ChuDian1=decimal.Parse(row["ChuDian1"].ToString());
				}
				if(row["ChuDian2"]!=null && row["ChuDian2"].ToString()!="")
				{
					model.ChuDian2=decimal.Parse(row["ChuDian2"].ToString());
				}
				if(row["ChuDian3"]!=null && row["ChuDian3"].ToString()!="")
				{
					model.ChuDian3=decimal.Parse(row["ChuDian3"].ToString());
				}
				if(row["ChuDian4"]!=null && row["ChuDian4"].ToString()!="")
				{
					model.ChuDian4=decimal.Parse(row["ChuDian4"].ToString());
				}
				if(row["ChuDian5"]!=null && row["ChuDian5"].ToString()!="")
				{
					model.ChuDian5=decimal.Parse(row["ChuDian5"].ToString());
				}
				if(row["ChuDian6"]!=null && row["ChuDian6"].ToString()!="")
				{
					model.ChuDian6=decimal.Parse(row["ChuDian6"].ToString());
				}
				if(row["ChuDian7"]!=null && row["ChuDian7"].ToString()!="")
				{
					model.ChuDian7=decimal.Parse(row["ChuDian7"].ToString());
				}
				if(row["ChuDian8"]!=null && row["ChuDian8"].ToString()!="")
				{
					model.ChuDian8=decimal.Parse(row["ChuDian8"].ToString());
				}
				if(row["ChuDian9"]!=null && row["ChuDian9"].ToString()!="")
				{
					model.ChuDian9=decimal.Parse(row["ChuDian9"].ToString());
				}
				if(row["ChuDian10"]!=null && row["ChuDian10"].ToString()!="")
				{
					model.ChuDian10=decimal.Parse(row["ChuDian10"].ToString());
				}
				if(row["ChuDian11"]!=null && row["ChuDian11"].ToString()!="")
				{
					model.ChuDian11=decimal.Parse(row["ChuDian11"].ToString());
				}
				if(row["ChuDian12"]!=null && row["ChuDian12"].ToString()!="")
				{
					model.ChuDian12=decimal.Parse(row["ChuDian12"].ToString());
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
			strSql.Append("select Id,RecordTime,StationNum,VacuumValue,ChuDian1,ChuDian2,ChuDian3,ChuDian4,ChuDian5,ChuDian6,ChuDian7,ChuDian8,ChuDian9,ChuDian10,ChuDian11,ChuDian12,CarrierId ");
			strSql.Append(" FROM tb_VacuumDegreeBYD ");
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
			strSql.Append(" Id,RecordTime,StationNum,VacuumValue,ChuDian1,ChuDian2,ChuDian3,ChuDian4,ChuDian5,ChuDian6,ChuDian7,ChuDian8,ChuDian9,ChuDian10,ChuDian11,ChuDian12,CarrierId ");
			strSql.Append(" FROM tb_VacuumDegreeBYD ");
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
			strSql.Append("select count(1) FROM tb_VacuumDegreeBYD ");
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
			strSql.Append(")AS Row, T.*  from tb_VacuumDegreeBYD T ");
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
        /// <summary>
        /// 获取小车及腔体真空度
        /// </summary>
        /// <param name="carrierId"></param>
        /// <param name="stationNum"></param>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageCount"></param>
        /// <param name="totalRecord"></param>
        /// <returns></returns>
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
                                                select t2.*,t1.CarrierNum,ROW_NUMBER() over(order by t2.RecordTime asc) rowNo from tb_CarrierInfo t1,tb_VacuumDegreeBYD t2
                                                 where t1.Id = t2.CarrierId {0} )tt
                                      where tt.rowNo between {1} and {2} ", sqlWhere, beginRecord, endRecord);

            sqlCount = string.Format(@"select COUNT(1) from tb_CarrierInfo t1,tb_VacuumDegreeBYD t2
                                    where t1.Id = t2.CarrierId {0} ", sqlWhere);


            DataTable dt = new DataTable();

            dt = DbHelperSQL.Query(sqlstr).Tables[0];
            totalRecord = Convert.ToInt32(DbHelperSQL.ExecuteScalar(sqlCount));
            return dt;
        }
        #endregion  ExtensionMethod
    }
}

