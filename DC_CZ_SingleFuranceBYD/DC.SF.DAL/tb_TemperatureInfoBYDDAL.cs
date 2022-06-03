using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DC.SF.DAL.Common;
using DC.SF.Model;
using System.Collections.Generic;

namespace DC.SF.DAL
{
    /// <summary>
    /// 数据访问类:tb_TemperatureInfoBYD
    /// </summary>
    public partial class tb_TemperatureInfoBYDDAL
    {
        public tb_TemperatureInfoBYDDAL()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("Id", "tb_TemperatureInfoBYD");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from tb_TemperatureInfoBYD");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)
            };
            parameters[0].Value = Id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(tb_TemperatureInfoBYD model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tb_TemperatureInfoBYD(");
            strSql.Append("RecordTime,Temperature1,Temperature2,Temperature3,Temperature4,LayerNum,CarrierId,StationNum)");
            strSql.Append(" values (");
            strSql.Append("@RecordTime,@Temperature1,@Temperature2,@Temperature3,@Temperature4,@LayerNum,@CarrierId,@StationNum)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@RecordTime", SqlDbType.DateTime),
                    new SqlParameter("@Temperature1", SqlDbType.Decimal,5),
                    new SqlParameter("@Temperature2", SqlDbType.Decimal,5),
                    new SqlParameter("@Temperature3", SqlDbType.Decimal,5),
                    new SqlParameter("@Temperature4", SqlDbType.Decimal,5),
                    new SqlParameter("@LayerNum", SqlDbType.Int,4),
                    new SqlParameter("@CarrierId", SqlDbType.Int,4),
                    new SqlParameter("@StationNum", SqlDbType.Int,4)};
            parameters[0].Value = model.RecordTime;
            parameters[1].Value = model.Temperature1;
            parameters[2].Value = model.Temperature2;
            parameters[3].Value = model.Temperature3;
            parameters[4].Value = model.Temperature4;
            parameters[5].Value = model.LayerNum;
            parameters[6].Value = model.CarrierId;
            parameters[7].Value = model.StationNum;

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
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(tb_TemperatureInfoBYD model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_TemperatureInfoBYD set ");
            strSql.Append("RecordTime=@RecordTime,");
            strSql.Append("Temperature1=@Temperature1,");
            strSql.Append("Temperature2=@Temperature2,");
            strSql.Append("Temperature3=@Temperature3,");
            strSql.Append("Temperature4=@Temperature4,");
            strSql.Append("LayerNum=@LayerNum,");
            strSql.Append("CarrierId=@CarrierId,");
            strSql.Append("StationNum=@StationNum");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
                    new SqlParameter("@RecordTime", SqlDbType.DateTime),
                    new SqlParameter("@Temperature1", SqlDbType.Decimal,5),
                    new SqlParameter("@Temperature2", SqlDbType.Decimal,5),
                    new SqlParameter("@Temperature3", SqlDbType.Decimal,5),
                    new SqlParameter("@Temperature4", SqlDbType.Decimal,5),
                    new SqlParameter("@LayerNum", SqlDbType.Int,4),
                    new SqlParameter("@CarrierId", SqlDbType.Int,4),
                    new SqlParameter("@StationNum", SqlDbType.Int,4),
                    new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = model.RecordTime;
            parameters[1].Value = model.Temperature1;
            parameters[2].Value = model.Temperature2;
            parameters[3].Value = model.Temperature3;
            parameters[4].Value = model.Temperature4;
            parameters[5].Value = model.LayerNum;
            parameters[6].Value = model.CarrierId;
            parameters[7].Value = model.StationNum;
            parameters[8].Value = model.Id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
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

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tb_TemperatureInfoBYD ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)
            };
            parameters[0].Value = Id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
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
        public bool DeleteList(string Idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tb_TemperatureInfoBYD ");
            strSql.Append(" where Id in (" + Idlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
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
        public tb_TemperatureInfoBYD GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,RecordTime,Temperature1,Temperature2,Temperature3,Temperature4,LayerNum,CarrierId,StationNum from tb_TemperatureInfoBYD ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)
            };
            parameters[0].Value = Id;

            tb_TemperatureInfoBYD model = new tb_TemperatureInfoBYD();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
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
        public tb_TemperatureInfoBYD DataRowToModel(DataRow row)
        {
            tb_TemperatureInfoBYD model = new tb_TemperatureInfoBYD();
            if (row != null)
            {
                if (row["Id"] != null && row["Id"].ToString() != "")
                {
                    model.Id = int.Parse(row["Id"].ToString());
                }
                if (row["RecordTime"] != null && row["RecordTime"].ToString() != "")
                {
                    model.RecordTime = DateTime.Parse(row["RecordTime"].ToString());
                }
                if (row["Temperature1"] != null && row["Temperature1"].ToString() != "")
                {
                    model.Temperature1 = decimal.Parse(row["Temperature1"].ToString());
                }
                if (row["Temperature2"] != null && row["Temperature2"].ToString() != "")
                {
                    model.Temperature2 = decimal.Parse(row["Temperature2"].ToString());
                }
                if (row["Temperature3"] != null && row["Temperature3"].ToString() != "")
                {
                    model.Temperature3 = decimal.Parse(row["Temperature3"].ToString());
                }
                if (row["Temperature4"] != null && row["Temperature4"].ToString() != "")
                {
                    model.Temperature4 = decimal.Parse(row["Temperature4"].ToString());
                }
                if (row["LayerNum"] != null && row["LayerNum"].ToString() != "")
                {
                    model.LayerNum = int.Parse(row["LayerNum"].ToString());
                }
                if (row["CarrierId"] != null && row["CarrierId"].ToString() != "")
                {
                    model.CarrierId = int.Parse(row["CarrierId"].ToString());
                }
                if (row["StationNum"] != null && row["StationNum"].ToString() != "")
                {
                    model.StationNum = int.Parse(row["StationNum"].ToString());
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,RecordTime,Temperature1,Temperature2,Temperature3,Temperature4,LayerNum,CarrierId,StationNum ");
            strSql.Append(" FROM tb_TemperatureInfoBYD ");
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
            strSql.Append(" Id,RecordTime,Temperature1,Temperature2,Temperature3,Temperature4,LayerNum,CarrierId,StationNum ");
            strSql.Append(" FROM tb_TemperatureInfoBYD ");
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
            strSql.Append("select count(1) FROM tb_TemperatureInfoBYD ");
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
            strSql.Append(")AS Row, T.*  from tb_TemperatureInfoBYD T ");
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
        /// 比亚迪单体炉温度按层板插入数据
        /// </summary>
        /// <param name="lstTemperature"></param>
        /// <returns></returns>
        public bool InsertList(List<tb_TemperatureInfoBYD> lstTemperature)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < lstTemperature.Count; i++)
            {
                tb_TemperatureInfoBYD temp = lstTemperature[i];
                sb.Append(string.Format(@" insert into tb_TemperatureInfoBYD (RecordTime, Temperature1,Temperature2,Temperature3,Temperature4, LayerNum, CarrierId,StationNum) 
                                            values ('{0}', {1}, {2}, {3}, {4},{5},{6},{7}) ; ", temp.RecordTime, temp.Temperature1, temp.Temperature2,
                                            temp.Temperature3, temp.Temperature4, temp.LayerNum, temp.CarrierId, temp.StationNum));
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

        /// <summary>
        /// 比亚迪单体炉温度查询
        /// </summary>
        /// <param name="carrierId"></param>
        /// <param name="stationNum"></param>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageCount"></param>
        /// <param name="layCount"></param>
        /// <param name="totalRecord"></param>
        /// <returns></returns>
        public DataTable GetTemperature(string carrierId, string stationNum, DateTime? dtBegin, DateTime? dtEnd, int PageIndex, int PageCount, int layCount, ref int totalRecord)
        {
            StringBuilder sqlWhere = new StringBuilder();
            sqlWhere.Append(carrierId == "" ? " " : " and CarrierNum = " + carrierId);
            sqlWhere.Append(stationNum == "" ? " " : " and StationNum = " + stationNum);
            sqlWhere.Append(dtBegin == null ? " " : string.Format(" and RecordTime > '{0}' ", dtBegin.Value.ToString("yyyy-MM-dd HH:mm:ss")));
            sqlWhere.Append(dtEnd == null ? "" : string.Format(" and RecordTime < '{0}' ", dtEnd.Value.ToString("yyyy-MM-dd HH:mm:ss")));

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


            sqlstr = string.Format(@"select ttt.CarrierId '生产编号', 
                                                ttt.StationNum-1001 '腔体编号',
                                                ttt.CarrierNum '载体编号',
                                                ttt.RecordTime '记录时间',
                                                tv.VacuumValue '真空度',
                                                tv.ChuDian1 '触点温度1',
                                                {3}
                                                  from (
                                           select RecordTime,StationNum,Temperature1,CarrierId ,LayerNum,tt.CarrierNum,ProduceId
                                           from(
                                           select ProduceId, RecordTime,Temperature1,StationNum,CarrierId,LayerNum ,t2.CarrierNum,ROW_NUMBER() over (order by recordtime desc) rowNooo
                                           from tb_TemperatureInfoBYD t1 left join tb_CarrierInfo t2
                                           on t1.CarrierId = t2.Id
                                           where 1=1  {0}
                                           ) tt where tt.rowNooo between {1} and {2} )
                                           tbb
                                           pivot( sum(Temperature1) for LayerNum in ({4}) ) ttt left join tb_VacuumDegreeBYD tv
                                            on ttt.CarrierId = tv.CarrierId and convert(varchar, ttt.RecordTime,120) = convert(varchar,tv.RecordTime,120)",
                                        sqlWhere.ToString(),
                                        (PageIndex - 1) * PageCount * layCount + 1,
                                        PageIndex * PageCount * layCount, layer1Sql, layer3Sql);


            sqlCount = string.Format(@"select COUNT(1) recordCount
                                                 from (
                                                       select RecordTime,Temperature1,LayerNum
                                                       from tb_TemperatureInfoBYD t1 left join tb_CarrierInfo t2 
                                                         on t1.CarrierId = t2.Id
                                                       where 1=1 {0}
                                                       )tb
                                                       pivot
                                                       (
                                                       sum(Temperature1) for LayerNum in( {1})
                                                       )as pvt", sqlWhere, layer3Sql);

            DataSet dataSet = DbHelperSQL.Query(sqlstr);
            if (dataSet.Tables.Count > 0)
            {
                dt = dataSet.Tables[0];
            }

            totalRecord = Convert.ToInt32(DbHelperSQL.ExecuteScalar(sqlCount));
            return dt;
        }
        #endregion  ExtensionMethod
    }
}

