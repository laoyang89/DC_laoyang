using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DC.SF.DAL.Common;
using DC.SF.Model;
using System.Collections.Generic;
using DC.SF.DataLibrary;

namespace DC.SF.DAL
{
	/// <summary>
	/// 数据访问类:tb_TemperatureInfo
	/// </summary>
	public partial class tb_TemperatureInfoDAL
	{
		public tb_TemperatureInfoDAL()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("Id", "tb_TemperatureInfo"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from tb_TemperatureInfo");
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
		public int Add(DC.SF.Model.tb_TemperatureInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into tb_TemperatureInfo(");
			strSql.Append("RecordTime,ControlValue,PatrolValue,LayerNum,CarrierId,StationNum)");
			strSql.Append(" values (");
			strSql.Append("@RecordTime,@ControlValue,@PatrolValue,@LayerNum,@CarrierId,@StationNum)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@RecordTime", SqlDbType.DateTime),
					new SqlParameter("@ControlValue", SqlDbType.Decimal,5),
					new SqlParameter("@PatrolValue", SqlDbType.Decimal,5),
					new SqlParameter("@LayerNum", SqlDbType.Int,4),
					new SqlParameter("@CarrierId", SqlDbType.Int,4),
					new SqlParameter("@StationNum", SqlDbType.Int,4)};
			parameters[0].Value = model.RecordTime;
			parameters[1].Value = model.ControlValue;
			parameters[2].Value = model.PatrolValue;
			parameters[3].Value = model.LayerNum;
			parameters[4].Value = model.CarrierId;
			parameters[5].Value = model.StationNum;

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
		public bool Update(DC.SF.Model.tb_TemperatureInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update tb_TemperatureInfo set ");
			strSql.Append("RecordTime=@RecordTime,");
			strSql.Append("ControlValue=@ControlValue,");
			strSql.Append("PatrolValue=@PatrolValue,");
			strSql.Append("LayerNum=@LayerNum,");
			strSql.Append("CarrierId=@CarrierId,");
			strSql.Append("StationNum=@StationNum");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@RecordTime", SqlDbType.DateTime),
					new SqlParameter("@ControlValue", SqlDbType.Decimal,5),
					new SqlParameter("@PatrolValue", SqlDbType.Decimal,5),
					new SqlParameter("@LayerNum", SqlDbType.Int,4),
					new SqlParameter("@CarrierId", SqlDbType.Int,4),
					new SqlParameter("@StationNum", SqlDbType.Int,4),
					new SqlParameter("@Id", SqlDbType.Int,4)};
			parameters[0].Value = model.RecordTime;
			parameters[1].Value = model.ControlValue;
			parameters[2].Value = model.PatrolValue;
			parameters[3].Value = model.LayerNum;
			parameters[4].Value = model.CarrierId;
			parameters[5].Value = model.StationNum;
			parameters[6].Value = model.Id;

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
			strSql.Append("delete from tb_TemperatureInfo ");
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
			strSql.Append("delete from tb_TemperatureInfo ");
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
		public DC.SF.Model.tb_TemperatureInfo GetModel(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Id,RecordTime,ControlValue,PatrolValue,LayerNum,CarrierId,StationNum from tb_TemperatureInfo ");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
			parameters[0].Value = Id;

			DC.SF.Model.tb_TemperatureInfo model=new DC.SF.Model.tb_TemperatureInfo();
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
		public DC.SF.Model.tb_TemperatureInfo DataRowToModel(DataRow row)
		{
			DC.SF.Model.tb_TemperatureInfo model=new DC.SF.Model.tb_TemperatureInfo();
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
				if(row["ControlValue"]!=null && row["ControlValue"].ToString()!="")
				{
					model.ControlValue=decimal.Parse(row["ControlValue"].ToString());
				}
				if(row["PatrolValue"]!=null && row["PatrolValue"].ToString()!="")
				{
					model.PatrolValue=decimal.Parse(row["PatrolValue"].ToString());
				}
				if(row["LayerNum"]!=null && row["LayerNum"].ToString()!="")
				{
					model.LayerNum=int.Parse(row["LayerNum"].ToString());
				}
				if(row["CarrierId"]!=null && row["CarrierId"].ToString()!="")
				{
					model.CarrierId=int.Parse(row["CarrierId"].ToString());
				}
				if(row["StationNum"]!=null && row["StationNum"].ToString()!="")
				{
					model.StationNum=int.Parse(row["StationNum"].ToString());
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
			strSql.Append("select Id,RecordTime,ControlValue,PatrolValue,LayerNum,CarrierId,StationNum ");
			strSql.Append(" FROM tb_TemperatureInfo ");
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
			strSql.Append(" Id,RecordTime,ControlValue,PatrolValue,LayerNum,CarrierId,StationNum ");
			strSql.Append(" FROM tb_TemperatureInfo ");
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
			strSql.Append("select count(1) FROM tb_TemperatureInfo ");
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
			strSql.Append(")AS Row, T.*  from tb_TemperatureInfo T ");
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
			parameters[0].Value = "tb_TemperatureInfo";
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
        /// 批量插入温度数据
        /// </summary>
        /// <returns></returns>
        public bool InsertList(List<tb_TemperatureInfo> lstTemperature)
        {
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < lstTemperature.Count; i++)
			{
				tb_TemperatureInfo temp = lstTemperature[i];
				sb.Append(string.Format(" insert into tb_TemperatureInfo (RecordTime, ControlValue, PatrolValue, LayerNum, CarrierId,StationNum) values ('{0}', {1}, {2}, {3}, {4},{5}) \r\n ", temp.RecordTime, temp.ControlValue, temp.PatrolValue, temp.LayerNum, temp.CarrierId, temp.StationNum));
			}
			int num = DbHelperSQL.ExecuteSql(sb.ToString());
			if (num == lstTemperature.Count)
			{
				return true;
			}
			else
			{
				return false;
			}
		}


        public DataTable GetTemperature(string carrierId,string stationNum,DateTime? dtBegin,DateTime? dtEnd,int PageIndex,int PageCount,int layCount,ref int totalRecord, EnumMachineType type = EnumMachineType.ChenHuaFurnance)
        {
            StringBuilder sqlWhere = new StringBuilder();
            sqlWhere.Append(carrierId == "" ? " " : " and CarrierNum = " + carrierId);
            sqlWhere.Append(stationNum == "" ? " " : " and StationNum = " + stationNum);
            sqlWhere.Append(dtBegin == null ? " " : string.Format(" and RecordTime > '{0}' ", dtBegin.Value.ToString("yyyy-MM-dd HH:mm:ss")));
            sqlWhere.Append( dtEnd == null ? "" : string.Format(" and RecordTime < '{0}' ", dtEnd.Value.ToString("yyyy-MM-dd HH:mm:ss")));

            int beginRecord = (PageIndex - 1) * PageCount + 1;
            int endRecord = PageIndex * PageCount;

            string sqlstr = "", sqlCount = "";
            
            DataTable dt = new DataTable();
            if(type == EnumMachineType.ChenHuaFurnance)  //这里为什么要这样分呢，因为陈化炉一个腔体就只有两个温度，如果是隧道炉，一层两个温度
            {
                sqlstr = string.Format(@"select ProduceId '生产编号',CarrierNum '载体编号',StationNum-1001 '腔体编号',RecordTime '记录时间',
                                                BeginTime '开始陈化时间' ,EndTime '结束陈化时间',
                                                ControlValue '侧温1温度',PatrolValue '侧温2温度',ABS( ControlValue-PatrolValue) '温差'	    
                                                 from (
                                                 select t1.*,t2.ProduceId,t2.CarrierNum,t2.BeginTime,t2.EndTime,ROW_NUMBER() over(order by t1.RecordTime asc) rowNum from tb_CarrierInfo t2 ,tb_TemperatureInfo t1 
                                                 where t1.CarrierId = t2.Id {0} )tt
                                                 where  tt.rowNum between {1} and {2} ", sqlWhere.ToString(), beginRecord,endRecord);

                sqlCount = string.Format(@"select COUNT(1) recordCount
                                                    from tb_TemperatureInfo t1
                                                    left join tb_CarrierInfo t2
                                                    on t1.CarrierId = t2.Id
                                                    where 1=1 {0}",sqlWhere.ToString());                
            }
            else
            {
                string layer1Sql = "", layer2Sql = "", layer3Sql = "";
                for (int i = 1; i <= layCount; i++)
                {
                    layer1Sql += string.Format(" [{0}] '第{0}层温度',", i);
                    layer2Sql += string.Format(" [{0}],", i);
                    layer3Sql += string.Format(" [{0}],", i);
                }
                
                layer1Sql = layer1Sql.Substring(0, layer1Sql.Length - 1);
                layer3Sql = layer3Sql.Substring(0,layer3Sql.Length-1);

                ///这里通过sql语句对行列进行反转，将一层的温度显示到一行，我简直就是个天才
                //sqlstr = string.Format(@"select  ProduceId '生产编号', 
                //                                 tt.StationNum-1001 '腔体编号',
                //                                 tt.CarrierNum '载体编号',
                //                                 tt.RecordTime '记录时间',
                //                                 tv.VacuumValue '真空度',
                //                                    {3}
                //                            from(
                //                                select Id,ProduceId,CarrierNum,StationNum,RecordTime,{4}
                //                                       ROW_NUMBER() over(order by recordtime desc) rowNum
                //                                 from (
                //                                       select t2.id,RecordTime,CarrierId,StationNum,ControlValue,LayerNum ,t2.ProduceId,t2.CarrierNum
                //                                       from tb_TemperatureInfo t1 inner join tb_CarrierInfo t2 
                //                                         on t1.CarrierId = t2.Id
                //                                       where 1=1 {0}
                //                                       )tb
                //                                       pivot
                //                                       (
                //                                       sum(ControlValue) for LayerNum in({5})
                //                                       )as pvt) tt left join tb_VacuumDegree tv 
                //                                                         on tt.Id = tv.CarrierId and convert(varchar, tt.RecordTime,120) = convert(varchar,tv.RecordTime,120)
                //                            where tt.rowNum between {1} and {2}", sqlWhere,beginRecord,endRecord,layer1Sql,layer2Sql,layer3Sql);


                sqlstr = string.Format(@"select ttt.CarrierId '生产编号', 
                                                ttt.StationNum-1001 '腔体编号',
                                                ttt.CarrierNum '载体编号',
                                                ttt.RecordTime '记录时间',
                                                tv.VacuumValue '真空度',
                                                {3}
                                                  from (
                                           select RecordTime,StationNum,ControlValue,CarrierId ,LayerNum,tt.CarrierNum,ProduceId
                                           from(
                                           select ProduceId, RecordTime,ControlValue,StationNum,CarrierId,LayerNum ,t2.CarrierNum,ROW_NUMBER() over (order by recordtime desc) rowNooo
                                           from tb_TemperatureInfo t1 left join tb_CarrierInfo t2
                                           on t1.CarrierId = t2.Id
                                           where 1=1  {0}
                                           ) tt where tt.rowNooo between {1} and {2} )
                                           tbb
                                           pivot( sum(ControlValue) for LayerNum in ({4}) ) ttt left join tb_VacuumDegree tv
                                            on ttt.CarrierId = tv.CarrierId and convert(varchar, ttt.RecordTime,120) = convert(varchar,tv.RecordTime,120)",
                                            sqlWhere.ToString(),
                                            (PageIndex-1)*PageCount * layCount+1,
                                            PageIndex*PageCount*layCount, layer1Sql, layer3Sql);


                sqlCount = string.Format(@"select COUNT(1) recordCount
                                                 from (
                                                       select RecordTime,ControlValue,LayerNum
                                                       from tb_TemperatureInfo t1 inner join tb_CarrierInfo t2 
                                                         on t1.CarrierId = t2.Id
                                                       where 1=1 {0}
                                                       )tb
                                                       pivot
                                                       (
                                                       sum(ControlValue) for LayerNum in( {1})
                                                       )as pvt", sqlWhere,layer3Sql);
            }
            DataSet dataSet = DbHelperSQL.Query(sqlstr);
            if(dataSet.Tables.Count>0)
            {
                dt = dataSet.Tables[0];
            }
            
            totalRecord = Convert.ToInt32(DbHelperSQL.ExecuteScalar(sqlCount));
            return dt;
        }


        public void ChangeTempCarriered(int carrierid)
        {
            string sqlstr = string.Format(@"update tb_TemperatureInfo
                                            set CarrierId = {0}
                                            where RecordTime > '{1}' and CarrierId = 0",carrierid,DateTime.Now.Date.ToString("yyyy-MM-dd 00:00:00"));
            DbHelperSQL.ExecuteSql(sqlstr);
        }

        public DataTable QueryTemperature(int layerNum, int carrierId, int? stationNum = null)
        {
            string laywhere = MemoryData.MachineType == EnumMachineType.ChenHuaFurnance ? " " : " and LayerNum = " + layerNum;
            ///樓上是天才，那你們請叫我大成鬼才
            string sql = string.Format(@"select RecordTime, ControlValue 
                            from tb_TemperatureInfo
                            where CarrierId={1} {0} ", laywhere, carrierId);
            if (stationNum != null)
            {
                string extraCondition = string.Format(@" and StationNum={0} ", stationNum);
                sql += extraCondition;
            }
            DataTable dt = DbHelperSQL.Query(sql).Tables[0];
            return dt;
        }
        #endregion  ExtensionMethod
    }
}

