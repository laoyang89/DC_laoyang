using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DC.SF.Model;
using System.Collections.Generic;
using DC.SF.DataLibrary;
using DC.SF.Common;

namespace DC.SF.DAL
{
	/// <summary>
	/// 数据访问类:EquipmentParameters
	/// </summary>
	public partial class EquipmentParametersDAL
	{
		public EquipmentParametersDAL()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long SystemAutoID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from EquipmentParameters");
			strSql.Append(" where SystemAutoID=@SystemAutoID");
			SqlParameter[] parameters = {
					new SqlParameter("@SystemAutoID", SqlDbType.BigInt)
			};
			parameters[0].Value = SystemAutoID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}

        /// <summary>
        /// 比亚迪单体炉温度按层板插入数据
        /// </summary>
        /// <param name="lstTemperature"></param>
        /// <returns></returns>
        public bool InsertList(List<tb_TemperatureInfoBYD> lstTemperature,double vacuumValue, long carrierid,short cavityStatus)
        {
            StringBuilder sb = new StringBuilder();
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[]{
                new DataColumn("SystemAutoID",typeof(int)),
                new DataColumn("Eno",typeof(string)),
                new DataColumn("WorkStationNo",typeof(string)),
                new DataColumn("LayerNum",typeof(int)),
                new DataColumn("TemperatureSV",typeof(decimal)),
                new DataColumn("TemperatureControl",typeof(decimal)),
                new DataColumn("TemperaturePV1",typeof(decimal)),
                new DataColumn("TemperaturePV2",typeof(decimal)),
                new DataColumn("TemperaturePV3",typeof(decimal)),
                new DataColumn("VacLimitsMax",typeof(decimal)),
                new DataColumn("VacLimitsMin",typeof(decimal)),
                new DataColumn("VacPV",typeof(decimal)),
                new DataColumn("SamplingTime",typeof(DateTime)),
                new DataColumn("CarSystemID",typeof(int)),
                new DataColumn("Remark",typeof(string)),
                new DataColumn("CavityStatus",typeof(int)),
                new DataColumn("ProductionStatus",typeof(int)),
                new DataColumn("SaveTempInterval",typeof(int))
            });
            for (int i = 0; i < lstTemperature.Count; i++)
            {
                tb_TemperatureInfoBYD temp = lstTemperature[i];
                int productionStatus = 0;
                if (cavityStatus > 8 && cavityStatus < 14)
                {
                    //真空度超过上限
                    if (MemoryData.ElectricSettingsModel.VacuumUpValue < vacuumValue ||
                    MemoryData.ElectricSettingsModel.VacuumUpValue < vacuumValue ||
                    MemoryData.ElectricSettingsModel.VacuumUpValue < vacuumValue ||
                    MemoryData.ElectricSettingsModel.VacuumUpValue < vacuumValue)
                    {
                        productionStatus = 3;
                    }
                    //温度超过上限
                    if ((int)MemoryData.ElectricSettingsModel.OverHeatOutage < temp.Temperature1 ||
                   (int)MemoryData.ElectricSettingsModel.OverHeatOutage < temp.Temperature2 ||
                   (int)MemoryData.ElectricSettingsModel.OverHeatOutage < temp.Temperature3 ||
                   (int)MemoryData.ElectricSettingsModel.OverHeatOutage < temp.Temperature4)
                    {
                        productionStatus = 1;
                    }
                    //温度低于下限
                    if ((int)MemoryData.ElectricSettingsModel.LowTempWarning > temp.Temperature1 ||
                        (int)MemoryData.ElectricSettingsModel.LowTempWarning > temp.Temperature2 ||
                        (int)MemoryData.ElectricSettingsModel.LowTempWarning > temp.Temperature3 ||
                        (int)MemoryData.ElectricSettingsModel.LowTempWarning > temp.Temperature4)
                    {
                        productionStatus = 2;
                    }
                    //故障停机，传感线断了
                    if (temp.Temperature1 == 850 || temp.Temperature2 == 850 || temp.Temperature3 == 850 || temp.Temperature4 == 850)
                    {
                        productionStatus = 4;
                    }
                
                }
                DataRow dr = dt.NewRow();
                dr["Eno"] = MemoryData.MesSettingsModel.BYDMesEquipmentID;
                dr["WorkStationNo"] = temp.StationNum;
                dr["LayerNum"] = temp.LayerNum;
                dr["TemperatureSV"] = MemoryData.ElectricSettingsModel.SettingTempValue;
                dr["TemperatureControl"] = temp.Temperature1;
                dr["TemperaturePV1"] = temp.Temperature2;
                dr["TemperaturePV2"] = temp.Temperature3;
                dr["TemperaturePV3"] = temp.Temperature4;
                dr["VacLimitsMax"] = MemoryData.ElectricSettingsModel.VacuumUpValue;
                dr["VacLimitsMin"] = MemoryData.ElectricSettingsModel.VacuumDownValue;
                dr["VacPV"] = vacuumValue;
                dr["SamplingTime"] = temp.RecordTime;
                dr["CarSystemID"] = carrierid;
                dr["Remark"] = EnumHelper.GetEnumDescript((BYD_VacuumStatus)cavityStatus);
                dr["CavityStatus"] = cavityStatus;
                dr["ProductionStatus"] = productionStatus;
                dr["SaveTempInterval"] = MemoryData.GeneralSettingsModel.SaveTempInterval;
                dt.Rows.Add(dr);
                //sb.Append(string.Format(@" insert into EquipmentParameters (Eno, WorkStationNo,LayerNum,TemperatureSV,
                //                                TemperatureControl, TemperaturePV1, TemperaturePV2,TemperaturePV3,
                //                                VacLimitsMax,VacLimitsMin,VacPV,SamplingTime,CarSystemID,Remark,CavityStatus,ProductionStatus) 
                //                            values ('{0}', '{1}', {2}, {3}, {4},{5},{6},{7},{8},{9},{10},'{11}',{12},'{13}',{14},{15}); ",
                //                            MemoryData.MesSettingsModel.BYDMesEquipmentID, temp.StationNum, temp.LayerNum,MemoryData.GeneralSettingsModel.SettingTempValue,
                //                            temp.Temperature1, temp.Temperature2,temp.Temperature3, temp.Temperature4,
                //                            MemoryData.GeneralSettingsModel.VacuumUpValue, MemoryData.GeneralSettingsModel.VacuumDownValue, vacuumValue, temp.RecordTime, carrierid, EnumHelper.GetEnumDescript((BYD_VacuumStatus)cavityStatus),cavityStatus, productionStatus));
            }
            DbHelperSQL.BulkToDB(dt, "EquipmentParameters");
            return true;
            //int num = DbHelperSQL.ExecuteSql(sb.ToString());
            //if (num == lstTemperature.Count)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(DC.SF.Model.EquipmentParameters model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into EquipmentParameters(");
			strSql.Append("Eno,WorkStationNo,LayerNum,TemperatureSV,TemperatureControl,TemperaturePV1,TemperaturePV2,TemperaturePV3,VacLimitsMax,VacLimitsMin,VacPV,SamplingTime,CarSystemID,Remark,CavityStatus,ProductionStatus,SaveTempInterval)");
			strSql.Append(" values (");
			strSql.Append("@Eno,@WorkStationNo,@LayerNum,@TemperatureSV,@TemperatureControl,@TemperaturePV1,@TemperaturePV2,@TemperaturePV3,@VacLimitsMax,@VacLimitsMin,@VacPV,@SamplingTime,@CarSystemID,@Remark,@CavityStatus,@ProductionStatus,@SaveTempInterval)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Eno", SqlDbType.VarChar,20),
					new SqlParameter("@WorkStationNo", SqlDbType.VarChar,50),
					new SqlParameter("@LayerNum", SqlDbType.Int,4),
					new SqlParameter("@TemperatureSV", SqlDbType.Decimal,9),
					new SqlParameter("@TemperatureControl", SqlDbType.Decimal,9),
					new SqlParameter("@TemperaturePV1", SqlDbType.Decimal,9),
					new SqlParameter("@TemperaturePV2", SqlDbType.Decimal,9),
					new SqlParameter("@TemperaturePV3", SqlDbType.Decimal,9),
					new SqlParameter("@VacLimitsMax", SqlDbType.Decimal,9),
					new SqlParameter("@VacLimitsMin", SqlDbType.Decimal,9),
					new SqlParameter("@VacPV", SqlDbType.Decimal,9),
					new SqlParameter("@SamplingTime", SqlDbType.DateTime),
					new SqlParameter("@CarSystemID", SqlDbType.BigInt,8),
					new SqlParameter("@Remark", SqlDbType.VarChar,50),
                    new SqlParameter("@CavityStatus", SqlDbType.Int,4),
                    new SqlParameter("@ProductionStatus", SqlDbType.Int,4),
                    new SqlParameter("@SaveTempInterval", SqlDbType.Int,4)};
			parameters[0].Value = model.Eno;
			parameters[1].Value = model.WorkStationNo;
			parameters[2].Value = model.LayerNum;
			parameters[3].Value = model.TemperatureSV;
			parameters[4].Value = model.TemperatureControl;
			parameters[5].Value = model.TemperaturePV1;
			parameters[6].Value = model.TemperaturePV2;
			parameters[7].Value = model.TemperaturePV3;
			parameters[8].Value = model.VacLimitsMax;
			parameters[9].Value = model.VacLimitsMin;
			parameters[10].Value = model.VacPV;
			parameters[11].Value = model.SamplingTime;
			parameters[12].Value = model.CarSystemID;
			parameters[13].Value = model.Remark;
            parameters[14].Value = model.CavityStatus;
            parameters[15].Value = model.ProductionStatus;
            parameters[16].Value = model.SaveTempInterval;

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
		public bool Update(DC.SF.Model.EquipmentParameters model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update EquipmentParameters set ");
			strSql.Append("Eno=@Eno,");
			strSql.Append("WorkStationNo=@WorkStationNo,");
			strSql.Append("LayerNum=@LayerNum,");
			strSql.Append("TemperatureSV=@TemperatureSV,");
			strSql.Append("TemperatureControl=@TemperatureControl,");
			strSql.Append("TemperaturePV1=@TemperaturePV1,");
			strSql.Append("TemperaturePV2=@TemperaturePV2,");
			strSql.Append("TemperaturePV3=@TemperaturePV3,");
			strSql.Append("VacLimitsMax=@VacLimitsMax,");
			strSql.Append("VacLimitsMin=@VacLimitsMin,");
			strSql.Append("VacPV=@VacPV,");
			strSql.Append("SamplingTime=@SamplingTime,");
			strSql.Append("CarSystemID=@CarSystemID,");
			strSql.Append("Remark=@Remark");
            strSql.Append("CavityStatus=@CavityStatus");
            strSql.Append("ProductionStatus=@ProductionStatus");
            strSql.Append("SaveTempInterval=@SaveTempInterval");
            strSql.Append(" where SystemAutoID=@SystemAutoID");
			SqlParameter[] parameters = {
					new SqlParameter("@Eno", SqlDbType.VarChar,20),
					new SqlParameter("@WorkStationNo", SqlDbType.VarChar,50),
					new SqlParameter("@LayerNum", SqlDbType.Int,4),
					new SqlParameter("@TemperatureSV", SqlDbType.Decimal,9),
					new SqlParameter("@TemperatureControl", SqlDbType.Decimal,9),
					new SqlParameter("@TemperaturePV1", SqlDbType.Decimal,9),
					new SqlParameter("@TemperaturePV2", SqlDbType.Decimal,9),
					new SqlParameter("@TemperaturePV3", SqlDbType.Decimal,9),
					new SqlParameter("@VacLimitsMax", SqlDbType.Decimal,9),
					new SqlParameter("@VacLimitsMin", SqlDbType.Decimal,9),
					new SqlParameter("@VacPV", SqlDbType.Decimal,9),
					new SqlParameter("@SamplingTime", SqlDbType.DateTime),
					new SqlParameter("@CarSystemID", SqlDbType.BigInt,8),
					new SqlParameter("@Remark", SqlDbType.VarChar,50),
                    new SqlParameter("@CavityStatus", SqlDbType.Int,4),
                    new SqlParameter("@ProductionStatus", SqlDbType.Int,4),
                    new SqlParameter("@SaveTempInterval", SqlDbType.Int,4),
                    new SqlParameter("@SystemAutoID", SqlDbType.BigInt,8)};
			parameters[0].Value = model.Eno;
			parameters[1].Value = model.WorkStationNo;
			parameters[2].Value = model.LayerNum;
			parameters[3].Value = model.TemperatureSV;
			parameters[4].Value = model.TemperatureControl;
			parameters[5].Value = model.TemperaturePV1;
			parameters[6].Value = model.TemperaturePV2;
			parameters[7].Value = model.TemperaturePV3;
			parameters[8].Value = model.VacLimitsMax;
			parameters[9].Value = model.VacLimitsMin;
			parameters[10].Value = model.VacPV;
			parameters[11].Value = model.SamplingTime;
			parameters[12].Value = model.CarSystemID;
			parameters[13].Value = model.Remark;
            parameters[14].Value = model.CavityStatus;
            parameters[15].Value = model.ProductionStatus;
            parameters[15].Value = model.SaveTempInterval;
            parameters[16].Value = model.SystemAutoID;

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
        public bool UpdateWhere(string strSet,string strWhere)
        {
            string sql = $"update EquipmentParameters set  {strSet} where 1=1  {strWhere}; ";
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
			strSql.Append("delete from EquipmentParameters ");
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
			strSql.Append("delete from EquipmentParameters ");
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
		public DC.SF.Model.EquipmentParameters GetModel(long SystemAutoID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 SystemAutoID,Eno,WorkStationNo,LayerNum,TemperatureSV,TemperatureControl,TemperaturePV1,TemperaturePV2,TemperaturePV3,VacLimitsMax,VacLimitsMin,VacPV,SamplingTime,CarSystemID,Remark,CavityStatus,ProductionStatus,SaveTempInterval from EquipmentParameters ");
			strSql.Append(" where SystemAutoID=@SystemAutoID");
			SqlParameter[] parameters = {
					new SqlParameter("@SystemAutoID", SqlDbType.BigInt)
			};
			parameters[0].Value = SystemAutoID;

			DC.SF.Model.EquipmentParameters model=new DC.SF.Model.EquipmentParameters();
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
		public DC.SF.Model.EquipmentParameters DataRowToModel(DataRow row)
		{
			DC.SF.Model.EquipmentParameters model=new DC.SF.Model.EquipmentParameters();
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
				if(row["WorkStationNo"]!=null)
				{
					model.WorkStationNo=row["WorkStationNo"].ToString();
				}
				if(row["LayerNum"]!=null && row["LayerNum"].ToString()!="")
				{
					model.LayerNum=int.Parse(row["LayerNum"].ToString());
				}
				if(row["TemperatureSV"]!=null && row["TemperatureSV"].ToString()!="")
				{
					model.TemperatureSV=decimal.Parse(row["TemperatureSV"].ToString());
				}
				if(row["TemperatureControl"]!=null && row["TemperatureControl"].ToString()!="")
				{
					model.TemperatureControl=decimal.Parse(row["TemperatureControl"].ToString());
				}
				if(row["TemperaturePV1"]!=null && row["TemperaturePV1"].ToString()!="")
				{
					model.TemperaturePV1=decimal.Parse(row["TemperaturePV1"].ToString());
				}
				if(row["TemperaturePV2"]!=null && row["TemperaturePV2"].ToString()!="")
				{
					model.TemperaturePV2=decimal.Parse(row["TemperaturePV2"].ToString());
				}
				if(row["TemperaturePV3"]!=null && row["TemperaturePV3"].ToString()!="")
				{
					model.TemperaturePV3=decimal.Parse(row["TemperaturePV3"].ToString());
				}
				if(row["VacLimitsMax"]!=null && row["VacLimitsMax"].ToString()!="")
				{
					model.VacLimitsMax=decimal.Parse(row["VacLimitsMax"].ToString());
				}
				if(row["VacLimitsMin"]!=null && row["VacLimitsMin"].ToString()!="")
				{
					model.VacLimitsMin=decimal.Parse(row["VacLimitsMin"].ToString());
				}
				if(row["VacPV"]!=null && row["VacPV"].ToString()!="")
				{
					model.VacPV=decimal.Parse(row["VacPV"].ToString());
				}
				if(row["SamplingTime"]!=null && row["SamplingTime"].ToString()!="")
				{
					model.SamplingTime=DateTime.Parse(row["SamplingTime"].ToString());
				}
				if(row["CarSystemID"]!=null && row["CarSystemID"].ToString()!="")
				{
					model.CarSystemID=long.Parse(row["CarSystemID"].ToString());
				}
				if(row["Remark"]!=null)
				{
					model.Remark=row["Remark"].ToString();
				}
                if (row["CavityStatus"] != null && row["CavityStatus"].ToString() != "")
                {
                    model.CavityStatus = int.Parse(row["CavityStatus"].ToString());
                }
                if (row["ProductionStatus"] != null && row["ProductionStatus"].ToString() != "")
                {
                    model.ProductionStatus = int.Parse(row["ProductionStatus"].ToString());
                }
                if (row["SaveTempInterval"] != null && row["SaveTempInterval"].ToString() != "")
                {
                    model.SaveTempInterval = int.Parse(row["SaveTempInterval"].ToString());
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
			strSql.Append("select SystemAutoID,Eno,WorkStationNo,LayerNum,TemperatureSV,TemperatureControl,TemperaturePV1,TemperaturePV2,TemperaturePV3,VacLimitsMax,VacLimitsMin,VacPV,SamplingTime,CarSystemID,Remark,CavityStatus,ProductionStatus,SaveTempInterval ");
			strSql.Append(" FROM EquipmentParameters  ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere + "  Order by SamplingTime ");
            }
            else
            {
                strSql.Append("  Order by SamplingTime ");
            }
			return DbHelperSQL.Query(strSql.ToString());
		}
        /// <summary>
        /// 读取指定列名的数据(去重)
        /// </summary>
        /// <param name="colName"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetTableColData(string colName,string strWhere)
        {
            
            string strSql = $"SELECT distinct  {colName} FROM EquipmentParameters WHERE 1=1  {strWhere} " ;

            return DbHelperSQL.Query(strSql);
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
			strSql.Append(" SystemAutoID,Eno,WorkStationNo,LayerNum,TemperatureSV,TemperatureControl,TemperaturePV1,TemperaturePV2,TemperaturePV3,VacLimitsMax,VacLimitsMin,VacPV,SamplingTime,CarSystemID,Remark,CavityStatus,ProductionStatus,SaveTempInterval ");
			strSql.Append(" FROM EquipmentParameters ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

        #endregion  BasicMethod

        #region  ExtensionMethod
        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM EquipmentParameters ");
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
                strSql.Append("order by T.SystemAutoID desc");
            }
            strSql.Append(")AS Row, T.*  from EquipmentParameters T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }
        public void ChangeTempCarriered(int carrierid)
        {
            string sqlstr = string.Format(@"update EquipmentParameters
                                            set CarSystemID = {0}
                                            where WorkStationNo = '{1}' and CarSystemID = 0", carrierid, DateTime.Now.Date.ToString("yyyy-MM-dd 00:00:00"));
            DbHelperSQL.ExecuteSql(sqlstr);
        }

        /// <summary>
        /// 比亚迪单体炉温度查询
        /// </summary>
        /// <param name="carrierId">小车唯一id</param>
        /// <param name="stationNum">站点ID</param>
        /// <param name="timeFlag">是否开启时间筛选</param>
        /// <param name="dtBegin">开始时间</param>
        /// <param name="dtEnd">结束时间</param>
        /// <param name="PageIndex">第几页</param>
        /// <param name="PageCount">每页条数</param>
        /// <param name="layCount">小车层板数</param>
        /// <param name="totalRecord">总条数</param>
        /// <returns></returns>
        public DataTable GetTemperature(string carrierId, string stationNum, bool timeFlag, DateTime? dtBegin, DateTime? dtEnd, int PageIndex, int PageCount, int layCount, ref int totalRecord)
        {
            StringBuilder sqlWhere = new StringBuilder();
            sqlWhere.Append(carrierId == "" ? " " : " and t2.CarNo = " + carrierId);
            sqlWhere.Append(stationNum == "" ? " " : " and t1.WorkStationNo = " + stationNum);
            if (timeFlag)
            {
                sqlWhere.Append(dtBegin == null ? " " : string.Format(" and t1.SamplingTime > '{0}' ", dtBegin.Value.ToString("yyyy-MM-dd HH:mm:ss")));
                sqlWhere.Append(dtEnd == null ? " " : string.Format(" and t1.SamplingTime < '{0}' ", dtEnd.Value.ToString("yyyy-MM-dd HH:mm:ss")));
            }


            int beginRecord = (PageIndex - 1) * PageCount + 1;
            int endRecord = PageIndex * PageCount;

            string sqlstr = "", sqlCount = "";

            DataTable dt = new DataTable();

            string layer1Sql = "", layer2Sql = "", layer3Sql = "";
            for (int i = 1; i <= layCount; i++)
            {
                layer1Sql += string.Format(" [{0}] '第{0}层温度',", i);
                layer2Sql += string.Format(" [{0}],", i);
                layer3Sql += string.Format(" [{0}],", i);
            }

            layer1Sql = layer1Sql.Substring(0, layer1Sql.Length - 1);
            layer3Sql = layer3Sql.Substring(0, layer3Sql.Length - 1);

            sqlstr = string.Format(@"select ttt.Eno '设备编号', --CarrierId
                                                    ttt.WorkStationNo-1001 '腔体编号',--StationNum-1001
                                                    ttt.CarNo '小车编号',--CarrierNum
                                                    ttt.SamplingTime '记录时间',--RecordTime
                                                    ttt.VacPV '真空度',--VacuumValue
                                                    {3}
                                                     from ( 
                                                           select Eno,WorkStationNo,SamplingTime,TemperatureControl,CarSystemID ,LayerNum,CarNo,VacPV
                                                           from( 
                                                           select t1.Eno, t1.SamplingTime,t1.TemperatureControl,t1.WorkStationNo,t1.CarSystemID,t1.VacPV,LayerNum 
                                                            ,t2.CarNo,ROW_NUMBER() over (order by t1.SamplingTime  asc ) rowNooo
                                                           from EquipmentParameters t1 left join CarDistribution t2
                                                           on t1.CarSystemID = t2.SystemAutoID
                                                           where 1=1  {0}
                                                            ) tt where tt.rowNooo between {1} and {2} 
                                                       )tbb
                                                       pivot( sum(TemperatureControl) for LayerNum in ({4}) ) ttt ",
                                              sqlWhere.ToString(),
                                              (PageIndex - 1) * PageCount * layCount + 1,
                                              PageIndex * PageCount * layCount, layer1Sql, layer3Sql);


            sqlCount = string.Format(@"select COUNT(1) recordCount
                                                 from (
                                                       select t1.SamplingTime,t1.TemperatureControl,t1.LayerNum
                                                       from EquipmentParameters t1 left join CarDistribution t2 
                                                       on t1.CarSystemID = t2.SystemAutoID
                                                       where 1=1 {0}
                                                       )tb
                                                       pivot
                                                       (
                                                       sum(TemperatureControl) for LayerNum in( {1})
                                                       )as pvt", sqlWhere, layer3Sql);

            DataSet dataSet = DbHelperSQL.Query(sqlstr);
            if (dataSet.Tables.Count > 0)
            {
                dt = dataSet.Tables[0];
            }

            totalRecord = Convert.ToInt32(DbHelperSQL.ExecuteScalar(sqlCount));
            return dt;
        }

        /// <summary>
        /// 获得时间间隔
        /// </summary>
        public DataSet GetIntervalDataSet(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SaveTempInterval FROM EquipmentParameters ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere + "  Order by SamplingTime ");
            }
            else
            {
                strSql.Append("  Order by SamplingTime ");
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
		/// 将DataRow中的数据转换为int类型
		/// </summary>
		public int SaveTempIntervalToInt(DataRow row)
        {
            int model =0;
            if (row != null)
            {
                if (row.Table.Columns.Contains("SaveTempInterval") && row["SaveTempInterval"] != null && row["SaveTempInterval"].ToString() != "")
                {
                    model = int.Parse(row["SaveTempInterval"].ToString());
                }
            }
            return model;
        }

        #endregion  ExtensionMethod
    }
}

