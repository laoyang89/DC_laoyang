using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DC.SF.DAL.Common;
using DC.SF.Model;
using DC.SF.DataLibrary;

namespace DC.SF.DAL
{
    /// <summary>
    /// 数据访问类:tb_MachineStatusTime
    /// </summary>
    public partial class tb_MachineStatusTimeDAL
    {

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("Id", "tb_MachineStatusTime");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from tb_MachineStatusTime");
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
        public int Add(tb_MachineStatusTime model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tb_MachineStatusTime(");
            strSql.Append("RecordDate,ClassType,WaitComeTime,WaitOutTime,AutoTime,HandleTime,WarnTime,LoadCount,UnLoadCount,SaveTime,WaitComeTime2,WaitOutTime2,LoadCount2,UnLoadCount2,TotalPower)");
            strSql.Append(" values (");
            strSql.Append("@RecordDate,@ClassType,@WaitComeTime,@WaitOutTime,@AutoTime,@HandleTime,@WarnTime,@LoadCount,@UnLoadCount,@SaveTime,@WaitComeTime2,@WaitOutTime2,@LoadCount2,@UnLoadCount2,@TotalPower)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@RecordDate", SqlDbType.Date,3),
                    new SqlParameter("@ClassType", SqlDbType.VarChar,50),
                    new SqlParameter("@WaitComeTime", SqlDbType.Int,4),
                    new SqlParameter("@WaitOutTime", SqlDbType.Int,4),
                    new SqlParameter("@AutoTime", SqlDbType.Int,4),
                    new SqlParameter("@HandleTime", SqlDbType.Int,4),
                    new SqlParameter("@WarnTime", SqlDbType.Int,4),
                    new SqlParameter("@LoadCount", SqlDbType.Int,4),
                    new SqlParameter("@UnLoadCount", SqlDbType.Int,4),
                    new SqlParameter("@SaveTime", SqlDbType.DateTime),
                    new SqlParameter("@WaitComeTime2", SqlDbType.Int,4),
                    new SqlParameter("@WaitOutTime2", SqlDbType.Int,4),
                    new SqlParameter("@LoadCount2", SqlDbType.Int,4),
                    new SqlParameter("@UnLoadCount2", SqlDbType.Int,4),
                    new SqlParameter("@TotalPower", SqlDbType.Int,4)
            };
            parameters[0].Value = model.RecordDate;
            parameters[1].Value = model.ClassType;
            parameters[2].Value = model.WaitComeTime;
            parameters[3].Value = model.WaitOutTime;
            parameters[4].Value = model.AutoTime;
            parameters[5].Value = model.HandleTime;
            parameters[6].Value = model.WarnTime;
            parameters[7].Value = model.LoadCount;
            parameters[8].Value = model.UnLoadCount;
            parameters[9].Value = model.SaveTime;
            parameters[10].Value = model.WaitComeTime2;
            parameters[11].Value = model.WaitOutTime2;
            parameters[12].Value = model.LoadCount2;
            parameters[13].Value = model.UnLoadCount2;
            parameters[14].Value = model.TotalPower;
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
        public bool Update(tb_MachineStatusTime model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_MachineStatusTime set ");
            strSql.Append("RecordDate=@RecordDate,");
            strSql.Append("ClassType=@ClassType,");
            strSql.Append("WaitComeTime=@WaitComeTime,");
            strSql.Append("WaitOutTime=@WaitOutTime,");
            strSql.Append("AutoTime=@AutoTime,");
            strSql.Append("HandleTime=@HandleTime,");
            strSql.Append("WarnTime=@WarnTime,");
            strSql.Append("LoadCount=@LoadCount,");
            strSql.Append("UnLoadCount=@UnLoadCount,");
            strSql.Append("SaveTime=@SaveTime,");
            strSql.Append("WaitComeTime2=@WaitComeTime2,");
            strSql.Append("WaitOutTime2=@WaitOutTime2,");
            strSql.Append("LoadCount2=@LoadCount2,");
            strSql.Append("UnLoadCount2=@UnLoadCount2");
            strSql.Append("TotalPower=@TotalPower");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
                    new SqlParameter("@RecordDate", SqlDbType.Date,3),
                    new SqlParameter("@ClassType", SqlDbType.VarChar,50),
                    new SqlParameter("@WaitComeTime", SqlDbType.Int,4),
                    new SqlParameter("@WaitOutTime", SqlDbType.Int,4),
                    new SqlParameter("@AutoTime", SqlDbType.Int,4),
                    new SqlParameter("@HandleTime", SqlDbType.Int,4),
                    new SqlParameter("@WarnTime", SqlDbType.Int,4),
                    new SqlParameter("@LoadCount", SqlDbType.Int,4),
                    new SqlParameter("@UnLoadCount", SqlDbType.Int,4),
                    new SqlParameter("@SaveTime", SqlDbType.DateTime),
                    new SqlParameter("@WaitComeTime2", SqlDbType.Int,4),
                    new SqlParameter("@WaitOutTime2", SqlDbType.Int,4),
                    new SqlParameter("@LoadCount2", SqlDbType.Int,4),
                    new SqlParameter("@UnLoadCount2", SqlDbType.Int,4),
                     new SqlParameter("@TotalPower", SqlDbType.Int,4),
                    new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = model.RecordDate;
            parameters[1].Value = model.ClassType;
            parameters[2].Value = model.WaitComeTime;
            parameters[3].Value = model.WaitOutTime;
            parameters[4].Value = model.AutoTime;
            parameters[5].Value = model.HandleTime;
            parameters[6].Value = model.WarnTime;
            parameters[7].Value = model.LoadCount;
            parameters[8].Value = model.UnLoadCount;
            parameters[9].Value = model.SaveTime;
            parameters[10].Value = model.WaitComeTime2;
            parameters[11].Value = model.WaitOutTime2;
            parameters[12].Value = model.LoadCount2;
            parameters[13].Value = model.UnLoadCount2;
            parameters[14].Value = model.TotalPower;

            parameters[15].Value = model.Id;

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
            strSql.Append("delete from tb_MachineStatusTime ");
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
            strSql.Append("delete from tb_MachineStatusTime ");
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
        public DC.SF.Model.tb_MachineStatusTime GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,RecordDate,ClassType,WaitComeTime,WaitOutTime,AutoTime,HandleTime,WarnTime,LoadCount,UnLoadCount,SaveTime,TotalPower from tb_MachineStatusTime ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)
            };
            parameters[0].Value = Id;

            DC.SF.Model.tb_MachineStatusTime model = new DC.SF.Model.tb_MachineStatusTime();
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
        public DC.SF.Model.tb_MachineStatusTime DataRowToModel(DataRow row)
        {
            DC.SF.Model.tb_MachineStatusTime model = new DC.SF.Model.tb_MachineStatusTime();
            if (row != null)
            {
                if (row["Id"] != null && row["Id"].ToString() != "")
                {
                    model.Id = int.Parse(row["Id"].ToString());
                }
                if (row["RecordDate"] != null && row["RecordDate"].ToString() != "")
                {
                    model.RecordDate = DateTime.Parse(row["RecordDate"].ToString());
                }
                if (row["ClassType"] != null)
                {
                    model.ClassType = row["ClassType"].ToString();
                }
                if (row["WaitComeTime"] != null && row["WaitComeTime"].ToString() != "")
                {
                    model.WaitComeTime = int.Parse(row["WaitComeTime"].ToString());
                }
                if (row["WaitOutTime"] != null && row["WaitOutTime"].ToString() != "")
                {
                    model.WaitOutTime = int.Parse(row["WaitOutTime"].ToString());
                }
                if (row["AutoTime"] != null && row["AutoTime"].ToString() != "")
                {
                    model.AutoTime = int.Parse(row["AutoTime"].ToString());
                }
                if (row["HandleTime"] != null && row["HandleTime"].ToString() != "")
                {
                    model.HandleTime = int.Parse(row["HandleTime"].ToString());
                }
                if (row["WarnTime"] != null && row["WarnTime"].ToString() != "")
                {
                    model.WarnTime = int.Parse(row["WarnTime"].ToString());
                }
                if (row["LoadCount"] != null && row["LoadCount"].ToString() != "")
                {
                    model.LoadCount = int.Parse(row["LoadCount"].ToString());
                }
                if (row["UnLoadCount"] != null && row["UnLoadCount"].ToString() != "")
                {
                    model.UnLoadCount = int.Parse(row["UnLoadCount"].ToString());
                }
                if (row["TotalPower"] != null && row["TotalPower"].ToString() != "")
                {
                    model.TotalPower = int.Parse(row["TotalPower"].ToString());
                }
                if (row["SaveTime"] != null && row["SaveTime"].ToString() != "")
                {
                    model.SaveTime = DateTime.Parse(row["SaveTime"].ToString());
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


            ///Id,RecordDate,ClassType,WaitComeTime,WaitOutTime,
            ///AutoTime,HandleTime,WarnTime,LoadCount,UnLoadCount,
            ///SaveTime,WaitComeTime2,WaitOutTime2,LoadCount2,UnLoadCount2
            
            

            if(MemoryData.MachineType == EnumMachineType.SingleFurnance)
            {
                strSql.Append(@"select RecordDate '日期',ClassType '班次', SaveTime '记录时间', WaitComeTime 'A料来料时间' ,WaitOutTime 'A料出料时间',
                AutoTime '自动运行时间', HandleTime  '手动时间', WarnTime '报警时间', LoadCount 'A料入料个数', UnLoadCount 'A料出料个数' ,
                WaitComeTime2 'B料来料时间',WaitOutTime2 'B料出料时间',LoadCount2 'B料入料个数',UnLoadCount2 'B料出料个数' , TotalPower '总用电量' ");
            }
            else if(MemoryData.MachineType == EnumMachineType.ChenHuaFurnance)
            {
                strSql.Append(@"select RecordDate '日期',ClassType '班次', SaveTime '记录时间', WaitComeTime '来料时间' ,WaitOutTime '出料时间',
                AutoTime '自动运行时间', HandleTime  '手动时间', WarnTime '报警时间', LoadCount '入料个数', UnLoadCount '出料个数' , TotalPower '总用电量' ");
            }else if(MemoryData.MachineType == EnumMachineType.BYDSingleFurnance)
            {
                //strSql.Append(@"Select RecordDate '日期',ClassType '班次', SaveTime '记录时间', WaitComeTime '来料时间' ,WaitOutTime '出料时间',
                //AutoTime '自动运行时间', HandleTime  '手动时间', WarnTime '报警时间', LoadCount '入料个数', UnLoadCount '出料个数'
                //,(Select t2.TotalPower-t1.TotalPower from [tb_MachineStatusTime] t2 where t1.Id+1=t2.Id ) as '当班用电量', TotalPower '总用电量'
                // from [tb_MachineStatusTime] t1");
                strSql.Append(@"Select RecordDate '班次日期',ClassType '班次', SaveTime '记录时间', WaitComeTime '来料时间', WaitOutTime '出料时间',
                LoadCount '入料个数', UnLoadCount '出料个数', AutoTime '自动运行时间', HandleTime  '手动时间', WarnTime '报警时间',
                (Select t1.TotalPower-t2.TotalPower from [tb_MachineStatusTime] t2 where t1.Id=t2.Id+1 ) as '当班用电量', TotalPower '总用电量'
                 from [tb_MachineStatusTime] t1");
            }
            
            if(MemoryData.MachineType != EnumMachineType.BYDSingleFurnance)
            {
                strSql.Append(" FROM tb_MachineStatusTime ");
            }

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
            strSql.Append(" Id,RecordDate,ClassType,WaitComeTime,WaitOutTime,AutoTime,HandleTime,WarnTime,LoadCount,UnLoadCount,SaveTime , TotalPower ");
            strSql.Append(" FROM tb_MachineStatusTime ");
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
            strSql.Append("select count(1) FROM tb_MachineStatusTime ");
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
            strSql.Append(")AS Row, T.*  from tb_MachineStatusTime T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}

