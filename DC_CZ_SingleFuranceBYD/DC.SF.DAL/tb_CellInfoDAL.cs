using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DC.SF.DAL.Common;
using System.Collections.Generic;
using DC.SF.DataLibrary;
using DC.SF.Model;

namespace DC.SF.DAL
{
	/// <summary>
	/// 数据访问类:tb_CellInfo
	/// </summary>
	public partial class tb_CellInfoDAL
	{
		public tb_CellInfoDAL()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("Id", "tb_CellInfo"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from tb_CellInfo");
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
		public int Add(DC.SF.Model.tb_CellInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into tb_CellInfo(");
			strSql.Append("ScanTime,CellCode,RankCode,LayerNumber,CarrierId)");
			strSql.Append(" values (");
			strSql.Append("@ScanTime,@CellCode,@RankCode,@LayerNumber,@CarrierId)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@ScanTime", SqlDbType.DateTime),
					new SqlParameter("@CellCode", SqlDbType.VarChar,50),
					new SqlParameter("@RankCode", SqlDbType.Int,4),
					new SqlParameter("@LayerNumber", SqlDbType.Int,4),
					new SqlParameter("@CarrierId", SqlDbType.Int,4)};
			parameters[0].Value = model.ScanTime;
			parameters[1].Value = model.CellCode;
			parameters[2].Value = model.RankCode;
			parameters[3].Value = model.LayerNumber;
			parameters[4].Value = model.CarrierId;

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
		public bool Update(DC.SF.Model.tb_CellInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update tb_CellInfo set ");
			strSql.Append("ScanTime=@ScanTime,");
			strSql.Append("CellCode=@CellCode,");
			strSql.Append("RankCode=@RankCode,");
			strSql.Append("LayerNumber=@LayerNumber,");
			strSql.Append("CarrierId=@CarrierId");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@ScanTime", SqlDbType.DateTime),
					new SqlParameter("@CellCode", SqlDbType.VarChar,50),
					new SqlParameter("@RankCode", SqlDbType.Int,4),
					new SqlParameter("@LayerNumber", SqlDbType.Int,4),
					new SqlParameter("@CarrierId", SqlDbType.Int,4),
					new SqlParameter("@Id", SqlDbType.Int,4)};
			parameters[0].Value = model.ScanTime;
			parameters[1].Value = model.CellCode;
			parameters[2].Value = model.RankCode;
			parameters[3].Value = model.LayerNumber;
			parameters[4].Value = model.CarrierId;
			parameters[5].Value = model.Id;

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
			strSql.Append("delete from tb_CellInfo ");
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
			strSql.Append("delete from tb_CellInfo ");
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
		public DC.SF.Model.tb_CellInfo GetModel(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Id,ScanTime,CellCode,RankCode,LayerNumber,CarrierId,RowNum,ColumnNum from tb_CellInfo ");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
			parameters[0].Value = Id;

			DC.SF.Model.tb_CellInfo model=new DC.SF.Model.tb_CellInfo();
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
		public DC.SF.Model.tb_CellInfo DataRowToModel(DataRow row)
		{
			DC.SF.Model.tb_CellInfo model=new DC.SF.Model.tb_CellInfo();
			if (row != null)
			{
				if(row["Id"]!=null && row["Id"].ToString()!="")
				{
					model.Id=int.Parse(row["Id"].ToString());
				}
				if(row["ScanTime"]!=null && row["ScanTime"].ToString()!="")
				{
					model.ScanTime=DateTime.Parse(row["ScanTime"].ToString());
				}
				if(row["CellCode"]!=null)
				{
					model.CellCode=row["CellCode"].ToString();
				}
				if(row["RankCode"]!=null && row["RankCode"].ToString()!="")
				{
					model.RankCode=int.Parse(row["RankCode"].ToString());
				}
				if(row["LayerNumber"]!=null && row["LayerNumber"].ToString()!="")
				{
					model.LayerNumber=int.Parse(row["LayerNumber"].ToString());
				}

                if (row["RowNum"] != null && row["RowNum"].ToString() != "")
                {
                    model.RowNum = int.Parse(row["RowNum"].ToString());
                }

                if (row["ColumnNum"] != null && row["ColumnNum"].ToString() != "")
                {
                    model.ColumnNum = int.Parse(row["ColumnNum"].ToString());
                }

                if (row["CarrierId"]!=null && row["CarrierId"].ToString()!="")
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
			strSql.Append("select Id,ScanTime,CellCode,RankCode,LayerNumber,CarrierId,RowNum,ColumnNum ");
			strSql.Append(" FROM tb_CellInfo ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}


		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM tb_CellInfo ");
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
			strSql.Append(")AS Row, T.*  from tb_CellInfo T ");
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
			parameters[0].Value = "tb_CellInfo";
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
        /// 按层板插入每一层电池
        /// </summary>
        /// <returns></returns>
        public bool InsertCellsByLayer(List<CellInfo> lstCellInfo,int CarrierInfoId)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < lstCellInfo.Count; i++)
            {
                CellInfo cell = lstCellInfo[i];
                sb.Append(string.Format(" insert into tb_CellInfo (ScanTime, CellCode, RankCode, LayerNumber, CarrierId, RowNum, ColumnNum) values ('{0}','{1}',{2},{3},{4},{5},{6})  ", cell.ScanTime, cell.CellCode, cell.RankCode, cell.LayerNum, CarrierInfoId, cell.RowNum, cell.ColumnNum));
            }
            int num = DbHelperSQL.ExecuteSql(sb.ToString());
            if (num == lstCellInfo.Count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public void DeleteCellById(int carrierId)
        {
            string sqlstr =string.Format( " delete from tb_CellInfo where CarrierId = {0}",carrierId);
            DbHelperSQL.ExecuteSql(sqlstr);
        }

        /// <summary>
        /// 按条码查询电池信息
        /// </summary>
        /// <param name="cellCode"></param>
        /// <returns></returns>
        public DataTable QueryCellInfoByCellCode(string cellCode)
        {
            string sql = string.Format(@"select CellCode as 条码,
                                               ScanTime as 扫码时间,
                                               t2.CarrierNum as 小车编号,
                                               t2.BeginTime as 开始工艺时间,
                                               t2.EndTime as 结束工艺时间,
                                               CarrierNum as 载体编号,
                                               LayerNumber as 层号,
                                               RowNum as 排,
                                               ColumnNum as 列 ,
                                               t1.CarrierId as 工艺编号                         
                                               from tb_CellInfo as t1
                                               inner join tb_CarrierInfo as t2
                                               on t1.CarrierId = t2.id
                            where CellCode like '%{0}%' ", cellCode);
            DataSet ds = DbHelperSQL.Query(sql);
            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            return null;
        }

        #endregion  ExtensionMethod
    }
}

