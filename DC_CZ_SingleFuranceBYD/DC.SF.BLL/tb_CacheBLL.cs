using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using DC.SF.DAL;
using DC.SF.Model;
using System.Diagnostics;
using System.Reflection;
using DC.SF.Common;
using DC.SF.DataLibrary;
using System.Linq;
using System.Threading.Tasks;

namespace DC.SF.BLL
{

    //tb_Cache
    public partial class tb_CacheBLL
	{
   		     
		private readonly tb_CacheDAL dal=new tb_CacheDAL();
		public tb_CacheBLL()
		{}
		
		#region  Method
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Id)
		{
			return dal.Exists(Id);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(tb_Cache model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(tb_Cache model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int Id)
		{
			
			return dal.Delete(Id);
		}
		/// <summary>
		/// 批量删除一批数据
		/// </summary>
		public bool DeleteList(string Idlist )
		{
			return dal.DeleteList(Idlist );
		}
        /// <summary>
        /// 删除所有数据
        /// </summary>
        public bool DeleteAll()
        {
            return dal.DeleteAllData();
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public tb_Cache GetModel(int Id)
		{
			
			return dal.GetModel(Id);
		}

		///// <summary>
		///// 得到一个对象实体，从缓存中
		///// </summary>
		//public tb_Cache GetModelByCache(int Id)
		//{
			
		//	string CacheKey = "tb_CacheModel-" + Id;
		//	object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
		//	if (objModel == null)
		//	{
		//		try
		//		{
		//			objModel = dal.GetModel(Id);
		//			if (objModel != null)
		//			{
		//				int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
		//				Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
		//			}
		//		}
		//		catch{}
		//	}
		//	return (tb_Cache)objModel;
		//}

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
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<tb_Cache> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<tb_Cache> DataTableToList(DataTable dt)
		{
			List<tb_Cache> modelList = new List<tb_Cache>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				tb_Cache model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new tb_Cache();
                    if (dt.Rows[n]["Id"].ToString() != "")
                    {
                        model.Id = int.Parse(dt.Rows[n]["Id"].ToString());
                    }
                    if (dt.Rows[n]["GroupId"].ToString() != "")
                    {
                        model.GroupId = int.Parse(dt.Rows[n]["GroupId"].ToString());
                    }
                    model.VariableName = dt.Rows[n]["VariableName"].ToString();
                    model.VariableValue = dt.Rows[n]["VariableValue"].ToString();
                    if (dt.Rows[n]["CacheType"].ToString() != "")
                    {
                        model.CacheType = int.Parse(dt.Rows[n]["CacheType"].ToString());
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
        /// <summary>
        /// 更新新的缓存项
        /// </summary>
        /// <param name="groupId">组Id</param>
        /// <param name="cacheType">缓存类型</param>
        /// <param name="variableName">变量名称</param>
        /// <param name="variableValue">变量Json值</param>
        /// <returns>是否更新成功</returns>
        public bool UpdateNewCache(int groupId, int cacheType, string variableName, string variableValue)
        {
            return dal.UpdateNewCache(groupId, cacheType, variableName, variableValue);
        }
        /// <summary>
        /// 更新小车对象字典缓存到数据库
        /// </summary>
        /// <param name="dicCarInfo"></param>
        public  void UpdateCarInfoCache(Dictionary<short, CH_CarInfo> dicCarInfo)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Type type = typeof(CH_CarInfo);
            PropertyInfo[] props = type.GetProperties();
            foreach (KeyValuePair<short, CH_CarInfo> item in dicCarInfo)
            {
                foreach (PropertyInfo element in props)
                {
                    if (element.Name == "ListTempInfo"
                        || element.Name == "ListVacuumValue"
                        || element.Name== "ListCellInfo"
                        )
                    {
                        continue;
                    }
                    object value = element.GetValue(item.Value);
                    string json = JsonConvert.SerializeObject(value);
                    // 这里不需要判断数据库没有该属性，因为软件开启的时候已经插入数据库这些信息。
                    UpdateNewCache(item.Key, 1, element.Name, json);
                }
               
            }
            stopwatch.Stop();
            LogHelper.Current.WriteText("保存CarInfo对象到数据库", $"耗时：{stopwatch.ElapsedMilliseconds} 毫秒");
        }
        /// <summary>
        /// SaveData对象缓存到数据库
        /// </summary>
        /// <param name="saveData"></param>
        public  void UpdateSaveDataCache(SaveData saveData)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Type type = typeof(SaveData);
            PropertyInfo[] props = type.GetProperties();
            foreach (var item in props)
            {
                if (item.Name == "ListScanCell" || item.Name == "ListCurrentAlarm" || item.Name== "ListTrayInfos")
                {
                    continue;
                }
                object value = item.GetValue(saveData);
                string json = JsonConvert.SerializeObject(value);
                // 这里不需要判断数据库没有该属性，因为软件开启的时候已经插入数据库这些信息。
                UpdateNewCache(1, 2, item.Name, json);
            }
            stopwatch.Stop();
            LogHelper.Current.WriteText("保存SaveData对象到数据库", $"耗时：{stopwatch.ElapsedMilliseconds} 毫秒");
        }

        /// <summary>
        /// 根据小车Id获取小车对象值
        /// </summary>
        /// <param name="carId">小车Id</param>
        /// <returns>小车对象</returns>
        public  CH_CarInfo GetCarInfoCache(int carId)
        {
            string json = GetCarInfoJson(carId);
            CH_CarInfo carInfo = JsonConvert.DeserializeJsonToT<CH_CarInfo>(json);
            return carInfo;
        }
        /// <summary>
        /// 获取SaveData对象
        /// </summary>
        /// <returns>SaveData对象</returns>
        public  SaveData GetSaveDataCache()
        {
            string json = GetSaveDataJson();
            SaveData saveData = JsonConvert.DeserializeJsonToT<SaveData>(json);
            return saveData;
        }

        /// <summary>
        /// 根据小车Id获取小车对象Json值
        /// </summary>
        /// <param name="carId">小车Id</param>
        /// <returns>小车对象Json值</returns>
        private  string GetCarInfoJson(int carId)
        {
            Type type = typeof(CH_CarInfo);
            PropertyInfo[] props = type.GetProperties();
            StringBuilder sb = new StringBuilder();
            CH_CarInfo carInfo = new CH_CarInfo();
            sb.Append("{");
            foreach (var item in props)
            {
                if (item.Name == "ListTempInfo"
                        || item.Name == "ListVacuumValue"
                        || item.Name == "ListCellInfo"
                        )
                {
                    continue;
                }
                string where = $"GroupId={carId} AND VariableName='{item.Name}' AND CacheType=1;";
                var list = GetModelList(where);
                if (list.Any())
                {
                    string value = list.First().VariableValue;
                    sb.Append($"\"{item.Name}\":{value},");
                }
                else
                {
                    tb_Cache tb_Cache = new tb_Cache();
                    tb_Cache.GroupId = carId;
                    tb_Cache.VariableName = item.Name;
                    tb_Cache.VariableValue = JsonConvert.SerializeObject(item.GetValue(carInfo));
                    tb_Cache.CacheType = 1;
                    Add(tb_Cache);
                }
            }
            if (sb.Length > 1)
            {
                sb.Remove(sb.Length - 1, 1);
            }
            sb.Append("}");
            string json = sb.ToString();
            return json;
        }

        /// <summary>
        /// 获取SaveData类的Json值
        /// </summary>
        /// <returns>SaveData类的Json值</returns>
        private  string GetSaveDataJson()
        {
            Type type = typeof(SaveData);
            PropertyInfo[] props = type.GetProperties();
            StringBuilder sb = new StringBuilder();
            SaveData saveData = new SaveData();
            sb.Append("{");
            foreach (var item in props)
            {
                if (item.Name == "ListScanCell" || item.Name == "ListCurrentAlarm" || item.Name == "ListTrayInfos")
                {
                    continue;
                }
                string where = $"GroupId={1} AND VariableName='{item.Name}' AND CacheType=2;";
                var list = GetModelList(where);
                if (list.Any())
                {
                    string value = list.First().VariableValue;
                    sb.Append($"\"{item.Name}\":{value},");
                }
                else
                {
                    tb_Cache tb_Cache = new tb_Cache();
                    tb_Cache.GroupId = 1;
                    tb_Cache.VariableName = item.Name;
                    tb_Cache.VariableValue = JsonConvert.SerializeObject(item.GetValue(saveData));
                    tb_Cache.CacheType = 2;
                    Add(tb_Cache);
                }
            }
            if (sb.Length > 1)
            {
                sb.Remove(sb.Length - 1, 1);
            }
            sb.Append("}");
            string json = sb.ToString();
            return json;
        }

        /// <summary>
        /// 设置小车对象字典缓存到数据库（仅更新程序第一次使用）
        /// </summary>
        /// <param name="dicCarInfo">小车对象字典</param>
        public  void SetCarInfoCache(Dictionary<short, CH_CarInfo> dicCarInfo)
        {
            Type type = typeof(CH_CarInfo);
            PropertyInfo[] props = type.GetProperties();
            foreach (var item in dicCarInfo)
            {
                foreach (var element in props)
                {
                    string values= JsonConvert.SerializeObject(element.GetValue(item.Value));
                    if (element.Name == "ListTempInfo"
                       || element.Name == "ListVacuumValue"
                       )
                    {
                        values = "[]";
                    }
                    tb_Cache tb_Cache = new tb_Cache();
                    tb_Cache.GroupId = item.Key;
                    tb_Cache.VariableName = element.Name;
                    tb_Cache.VariableValue = values;
                    tb_Cache.CacheType = 1;
                    Add(tb_Cache);
                }
            }
        }

        /// <summary>
        /// 设置SaveData对象缓存到数据库（仅更新程序第一次使用）
        /// </summary>
        /// <param name="saveData">SaveData类实例</param>
        public void SetSaveDataCache(SaveData saveData)
        {
            Type type = typeof(SaveData);
            PropertyInfo[] props = type.GetProperties();
            List<tb_ScanCellCode> listScanCode = null;
            foreach (var item in props)
            {
                string values = JsonConvert.SerializeObject(item.GetValue(saveData));
                if (item.Name == "ListScanCell"
                   )
                {
                    List<ScanCellInfo> listScan = JsonConvert.DeserializeJsonToT<List<ScanCellInfo>>(values);
                    listScanCode= DeepCopyHelper.MapperByList<tb_ScanCellCode, ScanCellInfo>(listScan);
                    values = "[]";
                }
                tb_Cache tb_Cache = new tb_Cache();
                tb_Cache.GroupId = 1;
                tb_Cache.VariableName = item.Name;
                tb_Cache.VariableValue = JsonConvert.SerializeObject(item.GetValue(saveData));
                tb_Cache.CacheType = 2;
                Add(tb_Cache);
            }
            Task.Run(() =>
            {
                try
                {
                    int okScan = scancellCodeBLL.AddAndUpdateList(listScanCode);

                    if (okScan == listScanCode.Count())
                    {
                        LogHelper.Current.WriteText("第一次启动导入数据成功", $"成功条数{okScan}，总{listScanCode.Count()}");
                    }
                    else
                    {
                        LogHelper.Current.WriteText("第一次启动导入数据失败", $"成功条数{okScan}，总{listScanCode.Count()}");
                    }
                }catch(Exception ex)
                {
                    LogHelper.Current.WriteEx("导入条码数据异常",ex);
                }
               
            });
        }

        private tb_ScanCellCodeBLL scancellCodeBLL = new tb_ScanCellCodeBLL();
        #endregion

    }
}