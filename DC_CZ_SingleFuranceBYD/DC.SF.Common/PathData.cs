using System;

namespace DC.SF.Common
{
    /// <summary>
    /// 路径信息类
    /// </summary>
    public class PathData
    {
        #region 文件路径区
        /// <summary>
        /// 读取保存在XML文档中的工位 文件路径
        /// </summary>
        public static readonly string StationXMLPath = AppDomain.CurrentDomain.BaseDirectory + @"Settings\Station.xml";

        /// <summary>
        /// PLC模块XML路径
        /// </summary>
        public static readonly string PLCModelListXMLPath = AppDomain.CurrentDomain.BaseDirectory + @"Settings\PLCModelList.xml";

        /// <summary>
        /// 帮助文档Pdf路径
        /// </summary>
        public static readonly string PdfHelpDocumentationPath = AppDomain.CurrentDomain.BaseDirectory + @"\Document\比亚迪单体炉使用说明.pdf";

        /// <summary>
        /// 将数据类保存在xml文件中的文件路径
        /// </summary>
        public static readonly string SaveDataXMLPath = AppDomain.CurrentDomain.BaseDirectory + @"Settings\SaveData.xml";

        /// <summary>
        /// 腔体字典xml文件路径
        /// </summary>
        //public static readonly string DicMiniCavityXMLPath = AppDomain.CurrentDomain.BaseDirectory + @"Settings\DicMiniCavity.xml";

        /// <summary>
        /// 小车字典xml文件路径
        /// </summary>
        public static readonly string CarInfoXMLPath = AppDomain.CurrentDomain.BaseDirectory + @"Settings\CarInfo.xml";

        /// <summary>
        /// 保存数据路径
        /// </summary>
        public static readonly string SaveDataPath = AppDomain.CurrentDomain.BaseDirectory + @"..\MIBData\";
        #endregion

        #region 目录区

        #endregion

        #region 网址区

        #endregion
    }
}
