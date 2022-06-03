using DC.SF.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.DAL
{
    public partial class DeviceParamDAL
    {
        public bool Exists(long SystemAutoID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from DeviceParam");
            strSql.Append(" where ");
            strSql.Append(" SystemAutoID = @SystemAutoID  ");
            SqlParameter[] parameters = {
                    new SqlParameter("@SystemAutoID", SqlDbType.BigInt)
            };
            parameters[0].Value = SystemAutoID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(DeviceParam model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into DeviceParam(");
            strSql.Append("ParamName,ParamDisplay,ParamValue,ParamMinValue,ParamMaxValue,PlcAddress,PlcDataType,MesUploadParamID,IsActived");
            strSql.Append(") values (");
            strSql.Append("@ParamName,@ParamDisplay,@ParamValue,@ParamMinValue,@ParamMaxValue,@PlcAddress,@PlcDataType,@MesUploadParamID,@IsActived");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@ParamName", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@ParamDisplay", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@ParamValue", SqlDbType.Int,4) ,
                        new SqlParameter("@ParamMinValue", SqlDbType.Int,4) ,
                        new SqlParameter("@ParamMaxValue", SqlDbType.Int,4) ,
                        new SqlParameter("@PlcAddress", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@PlcAddress", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@PlcDataType", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@MesUploadParamID", SqlDbType.Int,4) ,
                        new SqlParameter("@IsActived", SqlDbType.Bit,1)

            };

            parameters[0].Value = model.ParamName;
            parameters[1].Value = model.ParamDisplay;
            parameters[2].Value = model.ParamValue;
            parameters[3].Value = model.ParamMinValue;
            parameters[4].Value = model.ParamMaxValue;
            parameters[5].Value = model.PlcAddress;
            parameters[6].Value = model.PlcDataType;
            parameters[7].Value = model.MesUploadParamID;
            parameters[8].Value = model.IsActived;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
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
        public bool Update(DeviceParam model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update DeviceParam set ");

            strSql.Append(" ParamName = @ParamName , ");
            strSql.Append(" ParamDisplay = @ParamDisplay , ");
            strSql.Append(" ParamValue = @ParamValue , ");
            strSql.Append(" ParamMinValue = @ParamMinValue , ");
            strSql.Append(" ParamMaxValue = @ParamMaxValue , ");
            strSql.Append(" PlcAddress = @PlcAddress , ");
            strSql.Append(" PlcDataType = @PlcDataType , ");
            strSql.Append(" MesUploadParamID = @MesUploadParamID , ");
            strSql.Append(" IsActived = @IsActived  ");
            strSql.Append(" where SystemAutoID=@SystemAutoID ");

            SqlParameter[] parameters = {
                        new SqlParameter("@SystemAutoID", SqlDbType.BigInt,8) ,
                        new SqlParameter("@ParamName", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@ParamDisplay", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@ParamValue", SqlDbType.Int,4) ,
                        new SqlParameter("@ParamMinValue", SqlDbType.Int,4) ,
                        new SqlParameter("@ParamMaxValue", SqlDbType.Int,4) ,
                        new SqlParameter("@PlcAddress", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@PlcDataType", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@MesUploadParamID", SqlDbType.Int,4) ,
                        new SqlParameter("@IsActived", SqlDbType.Bit,1)

            };

            parameters[0].Value = model.SystemAutoID;
            parameters[1].Value = model.ParamName;
            parameters[2].Value = model.ParamDisplay;
            parameters[3].Value = model.ParamValue;
            parameters[4].Value = model.ParamMinValue;
            parameters[5].Value = model.ParamMaxValue;
            parameters[6].Value = model.PlcAddress;
            parameters[7].Value = model.PlcDataType;
            parameters[8].Value = model.MesUploadParamID;
            parameters[9].Value = model.IsActived;
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
        public bool Delete(long SystemAutoID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from DeviceParam ");
            strSql.Append(" where SystemAutoID=@SystemAutoID");
            SqlParameter[] parameters = {
                    new SqlParameter("@SystemAutoID", SqlDbType.BigInt)
            };
            parameters[0].Value = SystemAutoID;


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
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string SystemAutoIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from DeviceParam ");
            strSql.Append(" where ID in (" + SystemAutoIDlist + ")  ");
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
        public DeviceParam GetModel(long SystemAutoID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SystemAutoID, ParamName,ParamDisplay,ParamValue,ParamMinValue,ParamMaxValue,PlcAddress,PlcDataType,MesUploadParamID,IsActived  ");
            strSql.Append("  from DeviceParam ");
            strSql.Append(" where SystemAutoID=@SystemAutoID");
            SqlParameter[] parameters = {
                    new SqlParameter("@SystemAutoID", SqlDbType.BigInt)
            };
            parameters[0].Value = SystemAutoID;


            DeviceParam model = new DeviceParam();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["SystemAutoID"].ToString() != "")
                {
                    model.SystemAutoID = long.Parse(ds.Tables[0].Rows[0]["SystemAutoID"].ToString());
                }
                model.ParamName = ds.Tables[0].Rows[0]["ParamName"].ToString();
                model.ParamDisplay = ds.Tables[0].Rows[0]["ParamDisplay"].ToString();
                if (ds.Tables[0].Rows[0]["ParamValue"].ToString() != "")
                {
                    model.ParamValue = int.Parse(ds.Tables[0].Rows[0]["ParamValue"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ParamMinValue"].ToString() != "")
                {
                    model.ParamMinValue = int.Parse(ds.Tables[0].Rows[0]["ParamMinValue"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ParamMaxValue"].ToString() != "")
                {
                    model.ParamMaxValue = int.Parse(ds.Tables[0].Rows[0]["ParamMaxValue"].ToString());
                }

                model.PlcAddress = ds.Tables[0].Rows[0]["PlcAddress"].ToString();
                model.PlcDataType = ds.Tables[0].Rows[0]["PlcDataType"].ToString();

                if (ds.Tables[0].Rows[0]["MesUploadParamID"].ToString() != "")
                {
                    model.MesUploadParamID = int.Parse(ds.Tables[0].Rows[0]["MesUploadParamID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsActived"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsActived"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsActived"].ToString().ToLower() == "true"))
                    {
                        model.IsActived = true;
                    }
                    else
                    {
                        model.IsActived = false;
                    }
                }

                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DeviceParam GetModelByName(string ParamName)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SystemAutoID, ParamName,ParamDisplay,ParamValue,ParamMinValue,ParamMaxValue,PlcAddress,PlcDataType,MesUploadParamID,IsActived  ");
            strSql.Append("  from DeviceParam ");
            strSql.Append(" where ParamName=@ParamName");
            SqlParameter[] parameters = {
                    new SqlParameter("@ParamName", SqlDbType.NVarChar,50)
            };
            parameters[0].Value = ParamName;


            DeviceParam model = new DeviceParam();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["SystemAutoID"].ToString() != "")
                {
                    model.SystemAutoID = long.Parse(ds.Tables[0].Rows[0]["SystemAutoID"].ToString());
                }
                model.ParamName = ds.Tables[0].Rows[0]["ParamName"].ToString();
                model.ParamDisplay = ds.Tables[0].Rows[0]["ParamDisplay"].ToString();
                if (ds.Tables[0].Rows[0]["ParamValue"].ToString() != "")
                {
                    model.ParamValue = int.Parse(ds.Tables[0].Rows[0]["ParamValue"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ParamMinValue"].ToString() != "")
                {
                    model.ParamMinValue = int.Parse(ds.Tables[0].Rows[0]["ParamMinValue"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ParamMaxValue"].ToString() != "")
                {
                    model.ParamMaxValue = int.Parse(ds.Tables[0].Rows[0]["ParamMaxValue"].ToString());
                }
                model.PlcAddress = ds.Tables[0].Rows[0]["PlcAddress"].ToString();
                model.PlcDataType = ds.Tables[0].Rows[0]["PlcDataType"].ToString();
                if (ds.Tables[0].Rows[0]["MesUploadParamID"].ToString() != "")
                {
                    model.MesUploadParamID = int.Parse(ds.Tables[0].Rows[0]["MesUploadParamID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsActived"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsActived"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsActived"].ToString().ToLower() == "true"))
                    {
                        model.IsActived = true;
                    }
                    else
                    {
                        model.IsActived = false;
                    }
                }

                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM DeviceParam ");
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
            strSql.Append(" * ");
            strSql.Append(" FROM DeviceParam ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

    }
}
