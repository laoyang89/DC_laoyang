using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.Common
{
    public class CreateFactory
    {
        public static object CreateObject(string AssemblyPath, string classNamespace)
        {
            try
            {
                object objType = Assembly.Load(AssemblyPath).CreateInstance(classNamespace);
                return objType;
            }
            catch (System.Exception ex)
            {
                //string str=ex.Message;// 记录错误日志
                return null;
            }
        }
    }
}
