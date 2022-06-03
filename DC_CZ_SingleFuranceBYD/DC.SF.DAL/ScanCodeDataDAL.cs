using DC.SF.DAL.Common;
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
    /// <summary>
	/// 扫码记录访问类:ScanCodeData
	/// </summary>
    public class ScanCodeDataDAL
    {
        #region  BasicMethod
        /// <summary>
		/// 增加一条数据
		/// </summary>
		public long Add(tb_ScanCodeData model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ScanCodeData(");
            strSql.Append("CarID,ScanTime,CellCode,PLCCellCode,Reason,CodeStatus,MESStatus)");
            strSql.Append(" values (");
            strSql.Append("@CarID,@ScanTime,@CellCode,@PLCCellCode,@Reason,@CodeStatus,@MESStatus)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@CarID", SqlDbType.Int),
                    new SqlParameter("@ScanTime",SqlDbType.DateTime),
                    new SqlParameter("@CellCode", SqlDbType.VarChar,100),
                    new SqlParameter("@PLCCellCode", SqlDbType.VarChar,50),
                    new SqlParameter("@Reason", SqlDbType.VarChar,150),
                    new SqlParameter("@CodeStatus", SqlDbType.VarChar,10),
                    new SqlParameter("@MESStatus", SqlDbType.VarChar,10)
            };
            parameters[0].Value = model.CarID;
            parameters[1].Value = model.ScanTime;
            parameters[2].Value = model.CellCode;
            parameters[3].Value = model.PLCCellCode;
            parameters[4].Value = model.Reason;
            parameters[5].Value = model.CodeStatus;
            parameters[6].Value = model.MESStatus;

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
        /// 获取数据列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CarID,ScanTime,CellCode,PLCCellCode,Reason,CodeStatus,MESStatus ");
            strSql.Append(" FROM ScanCodeData ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
		/// 得到一个对象实体
		/// </summary>
		public DC.SF.Model.tb_ScanCodeData DataRowToModel(DataRow row)
        {
            DC.SF.Model.tb_ScanCodeData model = new DC.SF.Model.tb_ScanCodeData();
            if (row != null)
            {
                if (row["SystemAutoID"] != null && row["SystemAutoID"].ToString() != "")
                {
                    model.SystemAutoID = long.Parse(row["SystemAutoID"].ToString());
                }
                if (row["CarID"] != null && row["CarID"].ToString() != "")
                {
                    model.CarID = int.Parse(row["CarID"].ToString());
                }
                if (row["ScanTime"] != null && row["ScanTime"].ToString() != "")
                {
                    model.ScanTime = DateTime.Parse(row["ScanTime"].ToString());
                }
                if (row["CellCode"] != null && row["CellCode"].ToString() != "")
                {
                    model.CellCode = row["CellCode"].ToString();
                }
                if (row["PLCCellCode"] != null && row["PLCCellCode"].ToString() != "")
                {
                    model.PLCCellCode = row["PLCCellCode"].ToString();
                }
                if (row["Reason"] != null && row["Reason"].ToString() != "")
                {
                    model.Reason = row["Reason"].ToString();
                }
                if (row["CodeStatus"] != null && row["CodeStatus"].ToString() != "")
                {
                    model.CodeStatus = row["CodeStatus"].ToString();
                }
                if (row["MESStatus"] != null && row["MESStatus"].ToString() != "")
                {
                    model.MESStatus = row["MESStatus"].ToString();
                }
            }
            return model;
        }

        /// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT SystemAutoID,CarID as '托盘编号',ScanTime as '扫码时间',CellCode as '电芯条码',PLCCellCode as 'PLC编码',Reason as '备注',CodeStatus as '电芯状态',MESStatus as 'MES状态' FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.SystemAutoID desc");
            }
            strSql.Append(")AS Row, T.*  from ScanCodeData T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);

            strSql.Append($" ; SELECT COUNT(1) as pCount FROM ScanCodeData WHERE {strWhere} ");
            return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion
    }
}
