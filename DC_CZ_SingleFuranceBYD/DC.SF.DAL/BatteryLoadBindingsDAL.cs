using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DC.SF.Model;
using System.Collections.Generic;
using DC.SF.DataLibrary;

namespace DC.SF.DAL
{
	/// <summary>
	/// 数据访问类:BatteryLoadBindings
	/// </summary>
	public partial class BatteryLoadBindingsDAL
	{
		public BatteryLoadBindingsDAL()
		{}
        #region  BasicMethod

        

        /// <summary>
        /// 比亚迪重复条码查询小车
        /// </summary>
        /// <param name="dtbegin">扫码开始时间</param>
        /// <param name="dtend">扫码结束时间</param>
        /// <param name="carrierNum">小车唯一ID</param>
        /// <param name="cellcode">电池条码</param>
        /// <returns></returns>
        public DataTable GetRepeatCell(DateTime dtbegin, DateTime dtend, string carrierNum, string cellcode)
        {
            StringBuilder sbwhere = new StringBuilder();
            //string carrierWhere = carrierNum == 0 ? " " : string.Format(" and t2.CarrierNum = {0} ", carrierNum);
            if (carrierNum != "")
            {
                sbwhere.Append(string.Format(" and t2.CarNo = {0} ", carrierNum));
            }
            string sqlstr = string.Format(@" select PLotNo'重复条码',count(1) '重复次数'  from BatteryLoadBindings t1 left join CarDistribution t2
                                             on t2.SystemAutoID = t1.CarSystemID
                                             where ScanTime between '{0}'  and  '{1}'
                                             {2} and PLotNo like '%{3}%'
                                             group by PLotNo having COUNT(1) > 1", dtbegin, dtend, sbwhere, cellcode);
            return DbHelperSQL.Query(sqlstr).Tables[0];
        }

        /// <summary>
        /// 比亚迪查询电池信息
        /// </summary>
        /// <param name="dtbegin">扫码开始时间</param>
        /// <param name="dtend">扫码结束时间</param>
        /// <param name="carrierNum">小车号  </param>
        /// <param name="cellcode">电池条码</param>
        /// <param name="pageIndex">第几页</param>
        /// <param name="pagesize">每页几条数据</param>
        /// <param name="timeFlag">是否开启时间筛选</param>
        /// <param name="totalCount">总条数</param>
        /// <returns></returns>
        public DataTable GetPageQuery(DateTime dtbegin, DateTime dtend, string carrierNum, string cellcode, int pageIndex, int pagesize,bool timeFlag, ref int totalCount)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sbwhere = new StringBuilder();
            //string carrierWhere = carrierNum == 0 ? " " : string.Format(" and t2.CarrierNum = {0} ", carrierNum);
            if (carrierNum != "" )
            {
               sbwhere.Append(string.Format(" and t2.CarNo = {0} ", carrierNum));
            }

            if (timeFlag)
            {
                sbwhere.Append(string.Format(" and t1.ScanTime between '{0}'  and  '{1}' ", dtbegin, dtend)); 
            }

            string sqlstr = string.Format(@"select tb.PLotNo '条码',tb.RankCode '编码',tb.ScanTime '扫码时间', 
                                                   tb.WorkStationNo-1001 '腔体',
                                                   tb.LayerNum '层号' ,tb.RowNum '排号',
                                                   tb.ColumnNum '列号',tb.CarSystemID '工艺编号',
                                                   tb.CarNo '小车号',tb.LoadingTime '上料完成时间',tb.EnterTime '工艺开始时间',tb.OutTime as '工艺结束出炉时间',tb.UnloadTime '下料完成时间',tb.Remark '备注'  from
                                                      (
                                                    select t1.*,t2.CarNo,t2.LoadingTime,t2.UnloadTime,t2.EnterTime,t2.OutTime, ROW_NUMBER() over(order by ScanTime asc) orderNum 
                                                from BatteryLoadBindings  t1 INNER JOIN  dbo.CarDistribution t2
                                                   on  t2.SystemAutoID = t1.CarSystemID  where  1=1  {1}  
                                                and t1.PLotNo like '%{0}%'
                                                   ) tb  
                                                   where tb.orderNum between {2} and {3}", cellcode, sbwhere.ToString(), (pageIndex - 1) * pagesize + 1, pageIndex * pagesize);

            string sqlcount = string.Format(@"select COUNT(1) rowtotal from BatteryLoadBindings  t1 INNER JOIN CarDistribution t2
                                            on  t2.SystemAutoID = t1.CarSystemID  where 1=1  {0} 
                                                and PLotNo like '%{3}%'  ", sbwhere.ToString(), dtbegin, dtend, cellcode);
            
            sb.AppendLine(sqlstr);
            sb.AppendLine(sqlcount);


            DataSet ds = DbHelperSQL.Query(sb.ToString());
            totalCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);

            return ds.Tables[0];
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long SystemAutoID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from BatteryLoadBindings");
			strSql.Append(" where SystemAutoID=@SystemAutoID");
			SqlParameter[] parameters = {
					new SqlParameter("@SystemAutoID", SqlDbType.BigInt)
			};
			parameters[0].Value = SystemAutoID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


        /// <summary>
        /// 按层板插入每一层电池
        /// </summary>
        /// <returns></returns>
        public bool InsertCellsByLayer(List<CellInfo> lstCellInfo, long CarrierInfoId)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < lstCellInfo.Count; i++)
            {
                CellInfo cell = lstCellInfo[i];
                sb.Append(string.Format(@" insert into BatteryLoadBindings (Eno, ScanTime, PLotNo, RankCode, LayerNum, RowNum, ColumnNum, CarSystemID, WorkStationNo) 
                                            values ('{0}','{1}','{2}',{3},{4},{5},{6},{7},{8});  ",
                   MemoryData.MesSettingsModel.BYDMesEquipmentID, cell.ScanTime, cell.CellCode, cell.RankCode, cell.LayerNum, cell.RowNum, cell.ColumnNum,CarrierInfoId,cell.WorkStationNo));
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
        /// <summary>
        /// 批量导入条码
        /// </summary>
        /// <param name="lstCellInfo"></param>
        /// <param name="CarrierInfoId"></param>
        /// <returns></returns>
        public bool InsertCellsByList(List<CellInfo> lstCellInfo, long CarrierInfoId)
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[]{
                new DataColumn("SystemAutoID",typeof(long)),
                new DataColumn("Eno",typeof(string)),
                new DataColumn("ScanTime",typeof(DateTime)),
                new DataColumn("PLotNo",typeof(string)),
                new DataColumn("RankCode",typeof(int)),
                new DataColumn("LayerNum",typeof(int)),
                new DataColumn("RowNum",typeof(int)),
                new DataColumn("ColumnNum",typeof(int)),
                new DataColumn("CarSystemID",typeof(long)),
                new DataColumn("Remark",typeof(string)),
                new DataColumn("WorkStationNo",typeof(string))
            });
            for (int i = 0; i < lstCellInfo.Count; i++)
            {
                CellInfo cellInfo = lstCellInfo[i];
                DataRow dr = dt.NewRow();
                dr["Eno"] = MemoryData.MesSettingsModel.BYDMesEquipmentID;
                dr["ScanTime"] = cellInfo.ScanTime;
                dr["PLotNo"] = cellInfo.CellCode;
                dr["RankCode"] = cellInfo.RankCode;
                dr["LayerNum"] = cellInfo.LayerNum;
                dr["RowNum"] = cellInfo.RowNum;
                dr["ColumnNum"] = cellInfo.ColumnNum;
                dr["CarSystemID"] = CarrierInfoId;
                dr["WorkStationNo"] = cellInfo.WorkStationNo;
                dt.Rows.Add(dr);
            }
            DbHelperSQL.BulkToDB(dt, "BatteryLoadBindings");
            return true;
        }
        /// <summary>
        /// 查询当前电芯是否存在数据库指定小车上
        /// </summary>
        /// <param name="cellInfo"></param>
        /// <param name="CarrierInfoId"></param>
        /// <returns></returns>
        public bool CheckCellToDbID(CellInfo cellInfo , long CarrierInfoId)
        {
            string sql = $@"SELECT COUNT(1) FROM  BatteryLoadBindings WHERE CarSystemID={CarrierInfoId} 
                AND PLotNo='{cellInfo.CellCode}' AND RankCode={cellInfo.RankCode} AND LayerNum={cellInfo.LayerNum} 
                AND RowNum={cellInfo.RowNum} AND ColumnNum={cellInfo.ColumnNum};";
            object obj = DbHelperSQL.GetSingle(sql);
            if (obj == null)
            {
                return false;
            }
            else
            {
                return Convert.ToInt32(obj) == 0 ? false : true;
            }
        }

        public void DeleteCellById(long carrierId)
        {
            string sqlstr = string.Format(" delete from BatteryLoadBindings where CarSystemID = {0}", carrierId);
            DbHelperSQL.ExecuteSql(sqlstr);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(DC.SF.Model.BatteryLoadBindings model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into BatteryLoadBindings(");
			strSql.Append("Eno,ScanTime,PLotNo,RankCode,LayerNum,RowNum,ColumnNum,CarSystemID,Remark,WorkStationNo)");
			strSql.Append(" values (");
			strSql.Append("@Eno,@ScanTime,@PLotNo,@RankCode,@LayerNum,@RowNum,@ColumnNum,@CarSystemID,@Remark,@WorkStationNo)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Eno", SqlDbType.VarChar,20),
					new SqlParameter("@ScanTime", SqlDbType.DateTime),
					new SqlParameter("@PLotNo", SqlDbType.VarChar,50),
					new SqlParameter("@RankCode", SqlDbType.VarChar,50),
					new SqlParameter("@LayerNum", SqlDbType.Int,4),
					new SqlParameter("@RowNum", SqlDbType.Int,4),
					new SqlParameter("@ColumnNum", SqlDbType.Int,4),
					new SqlParameter("@CarSystemID", SqlDbType.BigInt,8),
					new SqlParameter("@Remark", SqlDbType.VarChar,50),
                    new SqlParameter("@WorkStationNo", SqlDbType.VarChar, 50)};
			parameters[0].Value = model.Eno;
			parameters[1].Value = model.ScanTime;
			parameters[2].Value = model.PLotNo;
			parameters[3].Value = model.RankCode;
			parameters[4].Value = model.LayerNum;
			parameters[5].Value = model.RowNum;
			parameters[6].Value = model.ColumnNum;
			parameters[7].Value = model.CarSystemID;
			parameters[8].Value = model.Remark;
            parameters[9].Value = model.WorkStationNo;

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
		public bool Update(DC.SF.Model.BatteryLoadBindings model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update BatteryLoadBindings set ");
			strSql.Append("Eno=@Eno,");
			strSql.Append("ScanTime=@ScanTime,");
			strSql.Append("PLotNo=@PLotNo,");
			strSql.Append("RankCode=@RankCode,");
			strSql.Append("LayerNum=@LayerNum,");
			strSql.Append("RowNum=@RowNum,");
			strSql.Append("ColumnNum=@ColumnNum,");
			strSql.Append("CarSystemID=@CarSystemID,");
			strSql.Append("Remark=@Remark,");
            strSql.Append("WorkStationNo=@WorkStationNo");
            strSql.Append(" where SystemAutoID=@SystemAutoID");
			SqlParameter[] parameters = {
					new SqlParameter("@Eno", SqlDbType.VarChar,20),
					new SqlParameter("@ScanTime", SqlDbType.DateTime),
					new SqlParameter("@PLotNo", SqlDbType.VarChar,50),
					new SqlParameter("@RankCode", SqlDbType.VarChar,50),
					new SqlParameter("@LayerNum", SqlDbType.Int,4),
					new SqlParameter("@RowNum", SqlDbType.Int,4),
					new SqlParameter("@ColumnNum", SqlDbType.Int,4),
					new SqlParameter("@CarSystemID", SqlDbType.BigInt,8),
					new SqlParameter("@Remark", SqlDbType.VarChar,50),
                    new SqlParameter("@WorkStationNo", SqlDbType.VarChar,50),
                    new SqlParameter("@SystemAutoID", SqlDbType.BigInt,8)};
			parameters[0].Value = model.Eno;
			parameters[1].Value = model.ScanTime;
			parameters[2].Value = model.PLotNo;
			parameters[3].Value = model.RankCode;
			parameters[4].Value = model.LayerNum;
			parameters[5].Value = model.RowNum;
			parameters[6].Value = model.ColumnNum;
			parameters[7].Value = model.CarSystemID;
			parameters[8].Value = model.Remark;
            parameters[9].Value = model.WorkStationNo;
			parameters[10].Value = model.SystemAutoID;

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
        /// 根据条件修改
        /// </summary>
        /// <param name="strSet"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public bool UpdateWhere(string strSet, string strWhere)
        {
            string sql = $"update BatteryLoadBindings set  {strSet} where 1=1  {strWhere}; ";
            int rows = DbHelperSQL.ExecuteSql(sql);
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
			strSql.Append("delete from BatteryLoadBindings ");
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
			strSql.Append("delete from BatteryLoadBindings ");
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
		public DC.SF.Model.BatteryLoadBindings GetModel(long SystemAutoID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 SystemAutoID,Eno,ScanTime,PLotNo,RankCode,LayerNum,RowNum,ColumnNum,CarSystemID,Remark,WorkStationNo from BatteryLoadBindings ");
			strSql.Append(" where SystemAutoID=@SystemAutoID");
			SqlParameter[] parameters = {
					new SqlParameter("@SystemAutoID", SqlDbType.BigInt)
			};
			parameters[0].Value = SystemAutoID;

			DC.SF.Model.BatteryLoadBindings model=new DC.SF.Model.BatteryLoadBindings();
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
		public DC.SF.Model.BatteryLoadBindings GetModel(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 SystemAutoID,Eno,ScanTime,PLotNo,RankCode,LayerNum,RowNum,ColumnNum,CarSystemID,Remark,WorkStationNo from BatteryLoadBindings ");
           
            if (!string.IsNullOrWhiteSpace(strWhere))
            {
                strSql.Append("where "+strWhere);
            }
            DC.SF.Model.BatteryLoadBindings model = new DC.SF.Model.BatteryLoadBindings();
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
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
        public DC.SF.Model.BatteryLoadBindings DataRowToModel(DataRow row)
		{
			DC.SF.Model.BatteryLoadBindings model=new DC.SF.Model.BatteryLoadBindings();
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
				if(row["ScanTime"]!=null && row["ScanTime"].ToString()!="")
				{
					model.ScanTime=DateTime.Parse(row["ScanTime"].ToString());
				}
				if(row["PLotNo"]!=null)
				{
					model.PLotNo=row["PLotNo"].ToString();
				}
				if(row["RankCode"]!=null)
				{
					model.RankCode=int.Parse( row["RankCode"].ToString());
				}
				if(row["LayerNum"] !=null && row["LayerNum"].ToString()!="")
				{
					model.LayerNum = int.Parse(row["LayerNum"].ToString());
				}
				if(row["RowNum"]!=null && row["RowNum"].ToString()!="")
				{
					model.RowNum=int.Parse(row["RowNum"].ToString());
				}
				if(row["ColumnNum"]!=null && row["ColumnNum"].ToString()!="")
				{
					model.ColumnNum=int.Parse(row["ColumnNum"].ToString());
				}
				if(row["CarSystemID"]!=null && row["CarSystemID"].ToString()!="")
				{
					model.CarSystemID=long.Parse(row["CarSystemID"].ToString());
				}
				if(row["Remark"]!=null)
				{
					model.Remark=row["Remark"].ToString();
				}
                if (row["WorkStationNo"] != null)
                {
                    model.WorkStationNo = row["WorkStationNo"].ToString();
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
			strSql.Append("select SystemAutoID,Eno,ScanTime,PLotNo,RankCode,LayerNum,RowNum,ColumnNum,CarSystemID,Remark,WorkStationNo ");
			strSql.Append(" FROM BatteryLoadBindings ");
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
			strSql.Append(" SystemAutoID,Eno,ScanTime,PLotNo,RankCode,LayerNum,RowNum,ColumnNum,CarSystemID,Remark,WorkStationNo ");
			strSql.Append(" FROM BatteryLoadBindings ");
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
			strSql.Append("select count(1) FROM BatteryLoadBindings ");
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
			strSql.Append(")AS Row, T.*  from BatteryLoadBindings T ");
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
			parameters[0].Value = "BatteryLoadBindings";
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

		#endregion  ExtensionMethod
	}
}

