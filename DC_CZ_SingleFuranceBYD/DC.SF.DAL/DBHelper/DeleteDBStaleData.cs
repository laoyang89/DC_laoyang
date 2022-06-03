using DC.SF.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.DAL
{
    public class DeleteDBStaleData
    {
        /// <summary>
        /// 批量删除指定时间过期数据
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <param name="whereSql">删除条件，默认为空，例：AND XXX=XX </param>
        public void Delete(string tableName , string whereSql="")
        {

            if(!string.IsNullOrWhiteSpace(tableName) )
            {
                string strSql = $"select count(*) from sysobjects where id = object_id('{tableName}') ";
                int count =(int) DbHelperSQL.ExecuteScalar(strSql);
                if (count > 0)
                {
                    strSql = $"DELETE TOP(100000) FROM  {tableName}  WHERE  1=1  {whereSql} ";
                    DateTime dateTime = DateTime.Now;

                    while (true)
                    {
                        try
                        {
                            int deCount = DbHelperSQL.ExecuteSql(strSql);
                            if (deCount < 100000)
                            {
                                break;
                            }

                            if ((DateTime.Now - dateTime).Minutes > 5)
                            {
                                LogHelper.Current.WriteText("定时删除过期数据超时", $"SQL为：{ strSql}  执行删除过程超过5分钟");
                                break;
                            }
                        }
                        catch (Exception ex)
                        {
                            LogHelper.Current.WriteEx("定时删除过期数据出错"+$"SQL为：{ strSql} ", ex);
                            break;

                        }

                    }
                }
                else
                {
                   // LogHelper.Current.WriteText("定时删除数据库", $"不存在指定数据库[{tableName}]");

                }
                
            }
        }
    }
}
