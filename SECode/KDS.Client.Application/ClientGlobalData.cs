using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KDS.Model;

/***********************************************************
 * 功能：全局变量
 *  
 * 创建：胡海明,huhaiming@gmail.com
 * 时间：2008/08
  ************************************************************/
namespace KDS.Client.App
{
    /// <summary>
    /// 客户端全局数据
    /// </summary>
    public sealed class ClientGlobalData
    {
        private ClientGlobalData()
        {
            //禁止创建此类的实例，huhaiming,2008/07
        }

        #region 常规
        /// <summary>
        /// 应用程序名称,huhm2008
        /// </summary>
        public const string AppName = "KDS System";

        /// <summary>
        /// 数据文件相对路径
        /// </summary>
        public const string DataRelativePath = "Data\\";

        /// <summary>
        /// 应用程序绝对路径（以“\”结尾）
        /// </summary>
        public static string AppAbsolutePath = "";

        /// <summary>
        /// 服务器信息
        /// </summary>
        public static T_ServerInfo ServerInfo = new T_ServerInfo();


        /// <summary>
        /// 是否调试模式,huhm2008
        /// </summary>
        //huhm,201106
        public static bool DebugMode = false;  //Release模式


        /// <summary>
        /// 用户票
        /// </summary>
        public static string UserTicket;

        #endregion



        #region 程序设置

        /// <summary>
        /// 客户端数据缓冲失效时间
        /// </summary>
        public static TimeSpan ClientDataCacheTimeout = TimeSpan.Zero;

        #endregion 


        #region WCFService


        /// <summary>
        /// 数据分页大小（每页记录数）
        /// </summary>
        public static int DataPageSize = 500;     

        #endregion 
    }
}
