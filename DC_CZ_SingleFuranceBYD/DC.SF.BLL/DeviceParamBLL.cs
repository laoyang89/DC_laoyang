using DC.SF.Common;
using DC.SF.DAL;
using DC.SF.DataLibrary;
using DC.SF.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.BLL
{
    public partial class DeviceParamBLL
    {
        private readonly DeviceParamDAL dal = new DeviceParamDAL();
        public DeviceParamBLL()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long SystemAutoID)
        {
            return dal.Exists(SystemAutoID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(DeviceParam model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(DeviceParam model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(long SystemAutoID)
        {

            return dal.Delete(SystemAutoID);
        }
        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string SystemAutoIDlist)
        {
            return dal.DeleteList(SystemAutoIDlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DeviceParam GetModel(long SystemAutoID)
        {

            return dal.GetModel(SystemAutoID);
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="ParamName"></param>
        /// <returns></returns>
        public DeviceParam GetModelByName(string ParamName)
        {

            return dal.GetModelByName(ParamName);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<DeviceParam> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<DeviceParam> DataTableToList(DataTable dt)
        {
            List<DeviceParam> modelList = new List<DeviceParam>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                DeviceParam model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new DeviceParam();
                    if (dt.Rows[n]["SystemAutoID"].ToString() != "")
                    {
                        model.SystemAutoID = long.Parse(dt.Rows[n]["SystemAutoID"].ToString());
                    }
                    model.ParamName = dt.Rows[n]["ParamName"].ToString();
                    model.ParamDisplay = dt.Rows[n]["ParamDisplay"].ToString();
                    if (dt.Rows[n]["ParamValue"].ToString() != "")
                    {
                        model.ParamValue = int.Parse(dt.Rows[n]["ParamValue"].ToString());
                    }
                    model.PlcAddress = dt.Rows[n]["PlcAddress"].ToString();
                    if (dt.Rows[n]["MesUploadParamID"].ToString() != "")
                    {
                        model.MesUploadParamID = int.Parse(dt.Rows[n]["MesUploadParamID"].ToString());
                    }
                    if (dt.Rows[n]["IsActived"].ToString() != "")
                    {
                        if ((dt.Rows[n]["IsActived"].ToString() == "1") || (dt.Rows[n]["IsActived"].ToString().ToLower() == "true"))
                        {
                            model.IsActived = true;
                        }
                        else
                        {
                            model.IsActived = false;
                        }
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }
        #endregion
        /// <summary>
        /// 软件启动的时候执行，从DeviceParam数据表中获取数据写给plc，并刷新到ElectricSettings实例中
        /// </summary>
        public void DBToElecSet(ElectricSettings electricModel)
        {
            try
            {
                if (electricModel==null)
                {
                    return;
                }
                DataTable deviceParamDt = GetAllList().Tables[0];   //从DeviceParam数据表中读取
                int rowsCount = deviceParamDt.Rows.Count;
                if (rowsCount <= 0)
                {
                    return;
                }
                string msg = string.Empty;
                for (int n = 0; n < rowsCount; n++)
                {
                    if (bool.Parse(deviceParamDt.Rows[n]["IsActived"].ToString()))
                    {
                        //先写给PLC
                        if(!short.TryParse(deviceParamDt.Rows[n]["ParamValue"].ToString(),out short propValue))
                        {
                            LogHelper.Current.WriteText("软件启动对工艺参数的设置", $"\"{deviceParamDt.Rows[n]["ParamDisplay"].ToString()}\"的参数值\"{deviceParamDt.Rows[n]["ParamValue"].ToString()}\"转化为short时失败，默认写0", LogHelper.LOG_TYPE_WARN);
                            propValue = 0;
                        }
                        MemoryData.PlcClient.WriteValue(deviceParamDt.Rows[n]["PlcAddress"].ToString(), propValue, DataType.Int16, ref msg);
                        //再刷新到ElectricSettings实例
                        PropertyInfo prop = electricModel.GetType().GetProperties().Where(p=> p.Name== deviceParamDt.Rows[n]["ParamName"].ToString()).FirstOrDefault();
                        if (prop==default(PropertyInfo))
                        {
                            //LogHelper.Current.WriteText("软件启动对工艺参数的设置", $"在{electricModel.GetType()}中未找到数据表中{deviceParamDt.Rows[n]["ParamName"].ToString()}对应的属性", LogHelper.LOG_TYPE_WARN);
                            continue;
                        }
                        prop.SetValue(electricModel, Convert.ChangeType(propValue, prop.PropertyType));
                    }
                }
                if (MemoryData.PlcClient.WriteValue("3401", 1, DataType.Int16, ref msg))//工艺参数发送标志位
                {
                    LogHelper.Current.WriteText("软件启动对工艺参数的设置", $"写入plc\"3401\"成功", LogHelper.LOG_TYPE_INFO);
                }
                else
                {
                    LogHelper.Current.WriteText("软件启动对工艺参数的设置", $"写入plc\"3401\"失败", LogHelper.LOG_TYPE_INFO);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Current.WriteEx($"软件启动对工艺参数的设置", ex);
            }
        }
        /// <summary>
        /// 从ElectricSettings实例中获取数据刷新到DeviceParam数据表
        /// </summary>
        /// <param name="electricModel"></param>
        public void ElecSetToDB(ElectricSettings electricModel)
        {
            try
            {
                if (electricModel == null)
                {
                    return;
                }
                foreach (PropertyInfo prop in MemoryData.GeneralSettingsModel.GetType().GetProperties())
                {
                    DeviceParam deviceParamModel = GetModelByName(prop.Name);
                    if (deviceParamModel==default(DeviceParam))
                    {
                        LogHelper.Current.WriteText("从缓存读取电气设置参数到数据库", $"在数据库中未找到{prop.Name}对应的数据", LogHelper.LOG_TYPE_WARN);
                        continue;
                    }
                    if (!int.TryParse(prop.GetValue(MemoryData.GeneralSettingsModel, null).ToString(), out int paramValue))
                    {
                        LogHelper.Current.WriteText("从缓存读取电气设置参数到数据库", $"缓存中{prop.Name}的数值{prop.GetValue(MemoryData.GeneralSettingsModel, null).ToString()}转化失败", LogHelper.LOG_TYPE_WARN);
                        continue;
                    }
                    deviceParamModel.ParamValue = paramValue;
                    Update(deviceParamModel);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Current.WriteEx($"从缓存读取电气设置参数到数据库过程中发生异常", ex);
            }
        }

    }
}
