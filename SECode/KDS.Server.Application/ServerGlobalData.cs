using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/***********************************************************
 * 功能：全局变量
 *  
 * 创建：胡海明,huhaiming@gmail.com
 * 时间：2008/08
  ************************************************************/
namespace KDS.Server.App
{
    /// <summary>
    /// 服务端应用程序全局变量,huhm2008
    /// </summary>
    public sealed class ServerGlobalData
    {
        private ServerGlobalData()
        {
            //禁止创建此类的实例，huhaiming,2008/07
        }

        #region 常规
        /// <summary>
        /// 应用程序名称,huhm2008
        /// </summary>
        public const string AppName = "KDS分销管理系统";

        /// <summary>
        /// 日志文件类型:Console,Event,None 三者其一
        /// </summary>
        public static string LogFileType = "Event";   

        /// <summary>
        /// 调试模式请设为true，正式发布应设为False
        /// </summary>
#if (DEBUG)
        public static bool DebugMode = true;   //编译模式
#else
        public static bool DebugMode = false;  //Release模式
#endif

        #endregion 


        #region 程序设置

        /// <summary>
        /// 服务端数据缓冲失效时间（分钟）
        /// </summary>
        public static TimeSpan ServerDataCacheTimeout = TimeSpan.FromMinutes(60*12);
        /// <summary>
        /// 用户缓冲数据失效时间（分钟）
        /// </summary>
        public static TimeSpan UserInfoCacheTimeout = TimeSpan.FromMinutes(60*1.5);


        /// <summary>
        /// 服务器定时期执行间隔（分钟）
        /// </summary>
        public static int SysTimerInterval = 10;

        #endregion 


        #region WCFService


        /// <summary>
        /// 数据服务终结点地址
        /// </summary>
        //public static string AbsoluteDataEndpointAddress = "net.tcp://localhost:8001/";

        /// <summary>
        /// 安全服务终结点地址
        /// </summary>
        //public static string AbsoluteSecurityEndpointAddress = "https://localhost/";

        #endregion 

        #region 密钥

        public static string DBConnEncryptKey = @"axHuMtC9x87";
        public static string PasswordEncryptKey = @"Ac&HUH89+RQLxOI$&:)K|><";
        public static string DataEncryptKey = @"x^Hai@MinLx$%2x)(#@X{_=x";
        public static string RegEncryptKey = @"&sHai^edMx#x)s%YHswffx";
        public const bool IsNeedRegService = false;

        #endregion 
    }
}
