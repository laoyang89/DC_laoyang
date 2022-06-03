using System;

using System.Data;
using System.Text;
using System.Data.SqlClient;
using DC.SF.DAL.Common;
using System.Collections.Generic;
using DC.SF.Model;
using DC.SF.Common;

namespace DC.SF.DAL
{
    /// <summary>
    /// 数据访问类:tb_CarrierInfo
    /// </summary>
    public partial class tb_CarrierInfoDAL
    {
        public tb_CarrierInfoDAL()
        { }
        #region  BasicMethod


        public DataTable GetRepeatCell(DateTime dtbegin, DateTime dtend, int carrierNum, string cellcode)
        {
            StringBuilder sbwhere = new StringBuilder();
            //string carrierWhere = carrierNum == 0 ? " " : string.Format(" and t2.CarrierNum = {0} ", carrierNum);
            if (carrierNum != 0)
            {
                sbwhere.Append(string.Format(" and t2.CarrierNum = {0} ", carrierNum));
            }
            string sqlstr =string.Format(@" select CellCode '重复条码',count(1) '重复次数'  from tb_CellInfo t1 left join tb_CarrierInfo t2
                                             on t1.CarrierId = t2.Id
                                             where ScanTime between '{0}'  and  '{1}'
                                             {2} and CellCode like '%{3}%'
                                             group by CellCode having COUNT(1) > 1",dtbegin,dtend,sbwhere,cellcode);
            return DbHelperSQL.Query(sqlstr).Tables[0];
        }


        public DataTable GetPageQuery(DateTime dtbegin, DateTime dtend, int carrierNum, string cellcode, int pageIndex, int pagesize, ref int totalCount)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sbwhere = new StringBuilder();
            //string carrierWhere = carrierNum == 0 ? " " : string.Format(" and t2.CarrierNum = {0} ", carrierNum);
            if(carrierNum!=0)
            {
                sbwhere.Append(string.Format(" and t2.CarrierNum = {0} ", carrierNum));
            }

            //string sqlstr = string.Format(@"select t1.CellCode '条码',t1.ScanTime '扫码时间',t1.LayerNumber '层号' ,
            //                                       t1.RowNum '排号',t1.ColumnNum '列号',t1.CarrierId '工艺编号',
            //                                       t3.CarrierNum '小车号',t3.BeginTime '工艺开始时间',t3.EndTime '工艺结束时间'   
            //                                 from tb_CellInfo  t1, (select id,ROW_NUMBER() over(order by scanTime desc) rowno from tb_CellInfo 
            //                                where  ScanTime between '{0}' and '{1}' 
            //                                 and CellCode like '%{2}%') t2,tb_CarrierInfo t3
            //                                    where t2.Id = t1.Id and t3.Id = t1.CarrierId {3}
            //                                      and t2.rowno between {4} and {5}  ",dtbegin,dtend,cellcode,carrierWhere, (pageIndex - 1) * pagesize + 1, pageIndex * pagesize);

            string sqlstr = string.Format(@" select tb.CellCode '条码',tb.ScanTime '扫码时间',
	                                            	tb.LayerNumber '层号' ,tb.RowNum '排号',
	                                            	tb.ColumnNum '列号',tb.CarrierId '工艺编号',
	                                            	tb.CarrierNum '小车号',tb.BeginTime '工艺开始时间',tb.EndTime '工艺结束时间' from
                                                      (
                                                    select t1.*,t2.CarrierNum,t2.BeginTime,t2.EndTime, ROW_NUMBER() over(order by ScanTime asc) orderNum 
                                                from tb_CellInfo  t1, tb_CarrierInfo t2
                                                   where  t2.Id = t1.CarrierId  {3} and t1.ScanTime between '{0}'  and  '{1}'
                                                and CellCode like '%{2}%'
                                                   ) tb
                                                   where tb.orderNum between {4} and {5}", dtbegin, dtend, cellcode, sbwhere.ToString(), (pageIndex - 1) * pagesize + 1, pageIndex * pagesize);

            string sqlcount = string.Format(@"select COUNT(1) rowtotal from tb_CellInfo  t1,tb_CarrierInfo t2
                                            where  t2.Id = t1.CarrierId {0}
                                                and ScanTime between '{1}' and '{2}' 
                                                and CellCode like '%{3}%'  ", sbwhere.ToString(), dtbegin,dtend,cellcode);
            sb.AppendLine(sqlstr);
            sb.AppendLine(sqlcount);


            DataSet ds = DbHelperSQL.Query(sb.ToString());
            totalCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);

            return ds.Tables[0];

        }


        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("Id", "tb_CarrierInfo");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from tb_CarrierInfo");
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
        public int Add(DC.SF.Model.tb_CarrierInfo model)
        {
            //StringBuilder strSql=new StringBuilder();
            //strSql.Append("insert into tb_CarrierInfo(");
            //strSql.Append("ProduceId,CarrierNum,BeginTime,EndTime,VacuumValue)");
            //strSql.Append(" values (");
            //strSql.Append("@ProduceId,@CarrierNum,@BeginTime,@EndTime,@VacuumValue)");
            //strSql.Append(";select @@IDENTITY");
            //SqlParameter[] parameters = {
            //		new SqlParameter("@ProduceId", SqlDbType.Int,16),
            //		new SqlParameter("@CarrierNum", SqlDbType.Int,4),
            //		new SqlParameter("@BeginTime", SqlDbType.DateTime),
            //		new SqlParameter("@EndTime", SqlDbType.DateTime),
            //		new SqlParameter("@VacuumValue", SqlDbType.Decimal,5)};
            //parameters[0].Value = model.ProduceId;
            //parameters[1].Value = model.CarrierNum;
            //parameters[2].Value = model.BeginTime;
            //parameters[3].Value = model.EndTime;
            //parameters[4].Value = model.VacuumValue;

            //object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
            //if (obj == null)
            //{
            //	return 0;
            //}
            //else
            //{
            //	return Convert.ToInt32(obj);
            //}
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("insert into tb_CarrierInfo (ProduceId,CarrierNum) values ((select (ISNULL(MAX(ProduceId),0)+1) as maxid from tb_CarrierInfo), {0}) ", model.CarrierNum));
            sb.Append(";select @@IDENTITY");
            object obj = DbHelperSQL.GetSingle(sb.ToString());
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
        public bool Update(DC.SF.Model.tb_CarrierInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_CarrierInfo set ");
            strSql.Append("ProduceId=@ProduceId,");
            strSql.Append("CarrierNum=@CarrierNum,");
            strSql.Append("BeginTime=@BeginTime,");
            strSql.Append("EndTime=@EndTime,");
            strSql.Append("VacuumValue=@VacuumValue");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
                    new SqlParameter("@ProduceId", SqlDbType.UniqueIdentifier,16),
                    new SqlParameter("@CarrierNum", SqlDbType.Int,4),
                    new SqlParameter("@BeginTime", SqlDbType.DateTime),
                    new SqlParameter("@EndTime", SqlDbType.DateTime),
                    new SqlParameter("@VacuumValue", SqlDbType.Decimal,5),
                    new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = model.ProduceId;
            parameters[1].Value = model.CarrierNum;
            parameters[2].Value = model.BeginTime;
            parameters[3].Value = model.EndTime;
            parameters[4].Value = model.VacuumValue;
            parameters[5].Value = model.Id;

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
            strSql.Append("delete from tb_CarrierInfo ");
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
            strSql.Append("delete from tb_CarrierInfo ");
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
        public DC.SF.Model.tb_CarrierInfo GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,ProduceId,CarrierNum,BeginTime,EndTime,VacuumValue from tb_CarrierInfo ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)
            };
            parameters[0].Value = Id;

            DC.SF.Model.tb_CarrierInfo model = new DC.SF.Model.tb_CarrierInfo();
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
        public DC.SF.Model.tb_CarrierInfo DataRowToModel(DataRow row)
        {
            DC.SF.Model.tb_CarrierInfo model = new DC.SF.Model.tb_CarrierInfo();
            if (row != null)
            {
                if (row["Id"] != null && row["Id"].ToString() != "")
                {
                    model.Id = int.Parse(row["Id"].ToString());
                }
                if (row["ProduceId"] != null && row["ProduceId"].ToString() != "")
                {
                    model.ProduceId = int.Parse(row["ProduceId"].ToString());
                }
                if (row["CarrierNum"] != null && row["CarrierNum"].ToString() != "")
                {
                    model.CarrierNum = int.Parse(row["CarrierNum"].ToString());
                }
                if (row["BeginTime"] != null && row["BeginTime"].ToString() != "")
                {
                    model.BeginTime = DateTime.Parse(row["BeginTime"].ToString());
                }
                if (row["EndTime"] != null && row["EndTime"].ToString() != "")
                {
                    model.EndTime = DateTime.Parse(row["EndTime"].ToString());
                }
                if (row["VacuumValue"] != null && row["VacuumValue"].ToString() != "")
                {
                    model.VacuumValue = decimal.Parse(row["VacuumValue"].ToString());
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
            strSql.Append("select Id,ProduceId,CarrierNum,BeginTime,EndTime,VacuumValue ");
            strSql.Append(" FROM tb_CarrierInfo ");
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
            strSql.Append(" Id,ProduceId,CarrierNum,BeginTime,EndTime,VacuumValue ");
            strSql.Append(" FROM tb_CarrierInfo ");
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
            strSql.Append("select count(1) FROM tb_CarrierInfo ");
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
            strSql.Append(")AS Row, T.*  from tb_CarrierInfo T ");
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
			parameters[0].Value = "tb_CarrierInfo";
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
        /// 返回最大生产ID
        /// </summary>
        /// <returns></returns>
        public int GetMaxProduceId(int carrierNum)
        {
            string sql = @"select ISNULL(MAX(ProduceId),0) maxproId from tb_CarrierInfo ";
            sql += string.Format(@" where CarrierNum={0}", carrierNum);
            object obj = DbHelperSQL.ExecuteScalar(sql);
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
        /// 更新载体结束时间，及真空度
        /// </summary>
        /// <param name="id"></param>
        /// <param name="endTime"></param>
        /// <param name="vacuum"></param>
        /// <returns></returns>
        public bool UpdateEndTimeAndVacuum(int id, DateTime endTime, float vacuum)
        {
            string sql = string.Format("update tb_CarrierInfo set EndTime = '{0}', VacuumValue = {1} where Id = {2}", endTime, vacuum, id);
            int num = DbHelperSQL.ExecuteSql(sql);
            if (num > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 更新载体结束时间
        /// </summary>
        /// <param name="id"></param>
        /// <param name="endTime"></param>
        /// <param name="vacuum"></param>
        /// <returns></returns>
        public bool UpdateEndTime(int id, DateTime endTime)
        {
            string sql = string.Format("update tb_CarrierInfo set EndTime = '{0}'where Id = {1}", endTime,id);
            int num = DbHelperSQL.ExecuteSql(sql);
            if (num > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 更新载体开始时间
        /// </summary>
        /// <param name="id"></param>
        /// <param name="beginTime"></param>
        /// <returns></returns>
        public bool UpdateBeginTime(int id, DateTime beginTime)
        {
            string sql = string.Format("update tb_CarrierInfo set BeginTime = '{0}' where Id = {1}", beginTime, id);
            int num = DbHelperSQL.ExecuteSql(sql);
            if (num > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion  ExtensionMethod
    }
}

