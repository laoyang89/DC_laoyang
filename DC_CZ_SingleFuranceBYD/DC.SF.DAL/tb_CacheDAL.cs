using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using DC.SF.Model;

namespace DC.SF.DAL
{
    /// <summary>
    /// 数据访问类:tb_Cache
    /// </summary>
    public partial class tb_CacheDAL
	{
   		     
		public bool Exists(int Id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from tb_Cache");
            strSql.Append(" where ");
            strSql.Append(" Id = @Id  ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)
            };
            parameters[0].Value = Id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(tb_Cache model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into tb_Cache(");			
            strSql.Append("GroupId,VariableName,VariableValue,CacheType");
			strSql.Append(") values (");
            strSql.Append("@GroupId,@VariableName,@VariableValue,@CacheType");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			SqlParameter[] parameters = {
			            new SqlParameter("@GroupId", SqlDbType.Int,4) ,            
                        new SqlParameter("@VariableName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@VariableValue", SqlDbType.NText) ,            
                        new SqlParameter("@CacheType", SqlDbType.Int,4)             
              
            };
			            
            parameters[0].Value = model.GroupId;                        
            parameters[1].Value = model.VariableName;                        
            parameters[2].Value = model.VariableValue;                        
            parameters[3].Value = model.CacheType;                        
			   
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
		public bool Update(tb_Cache model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update tb_Cache set ");
			                                                
            strSql.Append(" GroupId = @GroupId , ");                                    
            strSql.Append(" VariableName = @VariableName , ");                                    
            strSql.Append(" VariableValue = @VariableValue , ");                                    
            strSql.Append(" CacheType = @CacheType  ");            			
			strSql.Append(" where Id=@Id ");

            SqlParameter[] parameters = {
                        new SqlParameter("@Id", SqlDbType.Int,4) ,
                        new SqlParameter("@GroupId", SqlDbType.Int,4) ,
                        new SqlParameter("@VariableName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@VariableValue", SqlDbType.NText) ,
                        new SqlParameter("@CacheType", SqlDbType.Int,4)

            };

            parameters[0].Value = model.Id;                        
            parameters[1].Value = model.GroupId;                        
            parameters[2].Value = model.VariableName;                        
            parameters[3].Value = model.VariableValue;                        
            parameters[4].Value = model.CacheType;                        
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
			strSql.Append("delete from tb_Cache ");
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
		/// 批量删除一批数据
		/// </summary>
		public bool DeleteList(string Idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from tb_Cache ");
			strSql.Append(" where ID in ("+Idlist + ")  ");
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
        /// 删除所有信息
        /// </summary>
        /// <returns></returns>
		public bool DeleteAllData()
        {
            string strSql = "delete  from  tb_Cache";
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
		public tb_Cache GetModel(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select Id, GroupId, VariableName, VariableValue, CacheType  ");			
			strSql.Append("  from tb_Cache ");
			strSql.Append(" where Id=@Id");
						SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
			parameters[0].Value = Id;

			
			tb_Cache model=new tb_Cache();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["GroupId"].ToString() != "")
                {
                    model.GroupId = int.Parse(ds.Tables[0].Rows[0]["GroupId"].ToString());
                }
                model.VariableName = ds.Tables[0].Rows[0]["VariableName"].ToString();
                model.VariableValue = ds.Tables[0].Rows[0]["VariableValue"].ToString();
                if (ds.Tables[0].Rows[0]["CacheType"].ToString() != "")
                {
                    model.CacheType = int.Parse(ds.Tables[0].Rows[0]["CacheType"].ToString());
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM tb_Cache ");
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
			strSql.Append(" * ");
			strSql.Append(" FROM tb_Cache ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}
        /// <summary>
        /// 更新缓存
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="cacheType"></param>
        /// <param name="variableName"></param>
        /// <param name="variableValue"></param>
        /// <returns></returns>
        public bool UpdateNewCache(int groupId, int cacheType, string variableName, string variableValue)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_Cache set ");
            strSql.Append("VariableValue=@VariableValue");
            strSql.Append(" where GroupId=@GroupId AND VariableName=@VariableName AND CacheType=@CacheType;");
            SqlParameter[] parameters = {
                    new SqlParameter("@VariableValue", SqlDbType.NText),
                    new SqlParameter("@GroupId", SqlDbType.Int,11),
                    new SqlParameter("@VariableName", SqlDbType.VarChar,50),
                    new SqlParameter("@CacheType",SqlDbType.Int,11)};
            parameters[0].Value = variableValue;
            parameters[1].Value = groupId;
            parameters[2].Value = variableName;
            parameters[3].Value = cacheType;

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

    }
}

