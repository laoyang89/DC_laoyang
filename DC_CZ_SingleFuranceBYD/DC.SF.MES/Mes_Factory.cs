using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using DC.SF.Common;

namespace DC.SF.MES
{
    public class Mes_Factory
    {
        /// <summary>
        /// 通过反射来创建Mes对象
        /// </summary>
        /// <param name="ClassName"></param>
        /// <returns></returns>
        /// 情况说明：为什么要通过工厂来创建呢，因为这样的话就可以，实现不改代码改配置就可以实现动态调用
        /// 成功解耦，为了扩展和升级带来便利
        public static MesBase CreateMesObject(string ClassName)
        {
            MesBase mesObject = null;
            try
            {
                mesObject = (MesBase)Assembly.GetExecutingAssembly().CreateInstance("DC.SF.MES."+ClassName);
            }
            catch (Exception ex)
            {
                LogHelper.Current.WriteEx("创建Mes对象异常", ex, LogHelper.LOG_TYPE_ERROR);
            }
            return mesObject;
        }
    }
}
