using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using DC.SF.Model;

namespace DC.SF.DAL
{
    //tb_ScanCellCode
    public partial class tb_ScanCellCodeDAL
	{
        /// <summary>
        /// 获取当前最后一个编码
        /// </summary>
        /// <returns></returns>
        public int GetRankCodeAtPresent()
        {
            string strSql = $"select top 1 RankCode as dataCount from tb_ScanCellCode Order by ScanTime desc ;";
            DataSet ds = DbHelperSQL.Query(strSql);
            if (ds.Tables.Count > 0)
            {
                return int.Parse(ds.Tables[0].Rows[0]["dataCount"].ToString());
            }
            return 0;
        }

        public bool Exists(int RankCode)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from tb_ScanCellCode");
			strSql.Append(" where ");
            strSql.Append(" RankCode = @RankCode  ");
            SqlParameter[] parameters = {
                    new SqlParameter("@RankCode", SqlDbType.Int,4)          };
            parameters[0].Value = RankCode;

            return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}
        
        /// <summary>
        /// 批量添加缓存条码
        /// </summary>
        /// <param name="listScan"></param>
        /// <returns></returns>
        public int AddAndUpdateList(List<tb_ScanCellCode> listScan)
        {
            int  count = 0;
            int index = 0;
            StringBuilder sb = new StringBuilder();
            foreach (tb_ScanCellCode item in listScan)
            {

                if (Exists(item.RankCode))
                {
                    sb.Append($"update tb_scancellcode set  " +
                        $"CellCode='{item.CellCode}',CarID={item.CarID},ScanTime='{item.ScanTime}',CellType={item.CellType},CellModelType='{item.CellModelType}',CellPackage='{item.CellPackage}' where  RankCode={item.RankCode};");

                }
                else
                {
                    sb.Append($"insert into tb_scancellcode (RankCode,CellCode,CarID,ScanTime,CellType,CellModelType,CellPackage) " +
                        $"values ({item.RankCode},'{item.CellCode}',{item.CarID},'{item.ScanTime}',{item.CellType} , '{item.CellModelType}','{item.CellPackage}');");
                }
                index++;
                if (index % 100 == 0)
                {
                    count += DbHelperSQL.ExecuteSql(sb.ToString());
                    sb.Clear();
                }
            }
            if (!string.IsNullOrWhiteSpace(sb.ToString()))
            {
                count += DbHelperSQL.ExecuteSql(sb.ToString());
            }

            return count;
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(tb_ScanCellCode model)
		{
            if (Exists(model.RankCode))
            {
                 return Update(model);
            }
            else
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into tb_ScanCellCode(");
                strSql.Append("RankCode,CellCode,ScanTime,CellType,CellModelType,CellPackage,CarID");
                strSql.Append(") values (");
                strSql.Append("@RankCode,@CellCode,@ScanTime,@CellType,@CellModelType,@CellPackage,@CarID");
                strSql.Append(") ");

                SqlParameter[] parameters = {
                        new SqlParameter("@RankCode", SqlDbType.Int,4) ,
                        new SqlParameter("@CellCode", SqlDbType.VarChar,50) ,
                        new SqlParameter("@ScanTime", SqlDbType.DateTime) ,
                        new SqlParameter("@CellType", SqlDbType.Int,4) ,
                        new SqlParameter("@CellModelType", SqlDbType.VarChar,50) ,
                        new SqlParameter("@CellPackage", SqlDbType.VarChar,50) ,
                        new SqlParameter("@CarID", SqlDbType.Int,4)

                };

                parameters[0].Value = model.RankCode;
                parameters[1].Value = model.CellCode;
                parameters[2].Value = model.ScanTime;
                parameters[3].Value = model.CellType;
                parameters[4].Value = model.CellModelType;
                parameters[5].Value = model.CellPackage;
                parameters[6].Value = model.CarID;
                int num= DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
                if (num > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
			

        }

        /// <summary>
        /// 修改 编码数组对应的小车ID
        /// </summary>
        /// <param name="rankcodes"></param>
        /// <param name="cardbid"></param>
        /// <returns></returns>
        public int UpdateCarDBID(int[] rankcodes, int cardbid)
        {
            string strSql = $"update tb_ScanCellCode set CarID={cardbid} where RankCode in ({string.Join(",", rankcodes)}); ";
            int count = DbHelperSQL.ExecuteSql(strSql);
            return count;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(tb_ScanCellCode model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update tb_ScanCellCode set ");
                                             
            strSql.Append(" CellCode = @CellCode , ");                                    
            strSql.Append(" ScanTime = @ScanTime , ");                                    
            strSql.Append(" CellType = @CellType , ");                                    
            strSql.Append(" CellModelType = @CellModelType , ");                                    
            strSql.Append(" CellPackage = @CellPackage , ");                                    
            strSql.Append(" CarID = @CarID  ");            			
			strSql.Append(" where RankCode=@RankCode  ");

            SqlParameter[] parameters = {
                        new SqlParameter("@CellCode", SqlDbType.VarChar,50) ,
                        new SqlParameter("@ScanTime", SqlDbType.DateTime) ,
                        new SqlParameter("@CellType", SqlDbType.Int,4) ,
                        new SqlParameter("@CellModelType", SqlDbType.VarChar,50) ,
                        new SqlParameter("@CellPackage", SqlDbType.VarChar,50) ,
                        new SqlParameter("@CarID", SqlDbType.Int,4),
                        new SqlParameter("@RankCode", SqlDbType.Int,4) 
            };
            
            parameters[0].Value = model.CellCode;                        
            parameters[1].Value = model.ScanTime;                        
            parameters[2].Value = model.CellType;                        
            parameters[3].Value = model.CellModelType;                        
            parameters[4].Value = model.CellPackage;                        
            parameters[5].Value = model.CarID;
            parameters[6].Value = model.RankCode;
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
		public bool Delete(int RankCode)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from tb_ScanCellCode ");
			strSql.Append(" where RankCode=@RankCode ");
						SqlParameter[] parameters = {
					new SqlParameter("@RankCode", SqlDbType.Int,4)			};
			parameters[0].Value = RankCode;


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
		/// 得到一个对象实体
		/// </summary>
		public tb_ScanCellCode GetModel(int RankCode)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select RankCode, CellCode, ScanTime, CellType, CellModelType, CellPackage, CarID  ");			
			strSql.Append("  from tb_ScanCellCode ");
			strSql.Append(" where RankCode=@RankCode ");
						SqlParameter[] parameters = {
					new SqlParameter("@RankCode", SqlDbType.Int,4)			};
			parameters[0].Value = RankCode;

			
			tb_ScanCellCode model=new tb_ScanCellCode();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
                if (ds.Tables[0].Rows[0]["RankCode"].ToString() != "")
                {
                    model.RankCode = int.Parse(ds.Tables[0].Rows[0]["RankCode"].ToString());
                }
                model.CellCode = ds.Tables[0].Rows[0]["CellCode"].ToString();
                if (ds.Tables[0].Rows[0]["ScanTime"].ToString() != "")
                {
                    model.ScanTime = DateTime.Parse(ds.Tables[0].Rows[0]["ScanTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CellType"].ToString() != "")
                {
                    model.CellType = int.Parse(ds.Tables[0].Rows[0]["CellType"].ToString());
                }
                model.CellModelType = ds.Tables[0].Rows[0]["CellModelType"].ToString();
                model.CellPackage = ds.Tables[0].Rows[0]["CellPackage"].ToString();
                if (ds.Tables[0].Rows[0]["CarID"].ToString() != "")
                {
                    model.CarID = int.Parse(ds.Tables[0].Rows[0]["CarID"].ToString());
                }

                return model;
			}
			else
			{
				return null;
			}
		}
		
		
		/// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="strWhere">Where条件</param>
        /// <param name="extraStr">排序条件</param>
        /// <returns></returns>
		public DataSet GetList(string strWhere, string extraStr)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM tb_ScanCellCode ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(extraStr + "; ");
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
			strSql.Append(" * ");
			strSql.Append(" FROM tb_ScanCellCode ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

   
	}
}

