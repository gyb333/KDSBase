using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KDS.Model;
using System.Configuration;
using System.Data;
using KDS.SECommon;
/* ==========================================================================
 *  服务端应用程序控制类
 *  
 *  作者：胡海明 (huhaiming@gmail.com)
 *  日期：2008/08
 * 
 *  说明：
 *        1.此程序原型来源于CSS系统(2001-2008,huhaiming)的oApp对象模型；
 *        2.客户端应用程序全局控制类；
 *        3.加载权限、缓存、全局参数、服务等对象；
 *==========================================================================*/

namespace KDS.Server.App
{
    /// <summary>
    /// 服务应用程序类,huhm2008
    /// </summary>
    public sealed class ServerApp
    {
        /// <summary>
        /// 当前内存Cache数据,TableName/Data
        /// 是线程安全的，huhm2008
        /// </summary>
        public Dictionary<string, object> DataCache = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);


        //V2.4, Add by huhm,2011/03
        /// <summary>
        /// 当前锁定登录的信息，CompanyID/LockLoginInfo
        /// </summary>
        public Dictionary<int, T_LockLoginInfo> LockLoginInfo = new Dictionary<int, T_LockLoginInfo>();

        /// <summary>
        /// 用户Cache, UserTicket/UserInfo
        /// 是线程安全的，huhm2008
        /// </summary>
        public Dictionary<string, T_UserInfo> UserInfoCache = new Dictionary<string, T_UserInfo>();


        /// <summary>
        /// 系统参数Cache, CompanyID/Data
        /// 是线程安全的，huhm2008
        /// </summary>
        public Dictionary<int, DataTable> SysDataCache = new Dictionary<int, DataTable>();


        /// <summary>
        /// CPU负载
        /// </summary>
        public System.Diagnostics.PerformanceCounter CPUPerformanceCounter = new System.Diagnostics.PerformanceCounter("Processor", "% Processor Time", "_Total");


        //私有变量
        private static ServerApp mInstance = null;
        private static readonly object padlock = new object(); //同步锁

        //定时器变量-用于定时任务的处理(每10分钟触发一次）
        private System.Timers.Timer mSysTimer = new System.Timers.Timer(1000*60*10); //10分钟

        /// <summary>
        /// 获取类的实例
        /// </summary>
        /// <returns>实例</returns>
        public static ServerApp GetInstance()
        {

            if (mInstance == null)
            {
                lock (padlock)  //解决多线程冲突问题,huhm2008
                {
                    if (mInstance == null)
                    {
                        mInstance = new ServerApp();
                    }
                }
            }

            return mInstance;
        }

        #region 初始化

        private ServerApp()
        {
            this.InitGlobalData();             //创建全部变量设置
            this.CheckRegInfo();               //检查注册信息
            this.RegSysEvent();                //注册系统事件
        }


        //初始化全局变量
        private void InitGlobalData()
        {
            ServerGlobalData.LogFileType = ConfigurationManager.AppSettings["LogFileType"];
            ServerGlobalData.ServerDataCacheTimeout = TimeSpan.FromMinutes(int.Parse(ConfigurationManager.AppSettings["ServerDataCacheTimeout"]));
            ServerGlobalData.UserInfoCacheTimeout = TimeSpan.FromMinutes(int.Parse(ConfigurationManager.AppSettings["UserInfoCacheTimeout"]));
            ServerGlobalData.SysTimerInterval = int.Parse(ConfigurationManager.AppSettings["SysTimerInterval"]);

            CPUPerformanceCounter.NextValue();
        }

        //服务端事件注册
        private void RegSysEvent()
        {
            this.mSysTimer.Elapsed += new System.Timers.ElapsedEventHandler(mSysTimer_Elapsed);
            this.mSysTimer.Interval = ServerGlobalData.SysTimerInterval * 60 * 1000;
            this.mSysTimer.Start();
        }

        //定时期的任务处理
        private void mSysTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //定期移去过期的用户缓冲信息
            //foreach (KeyValuePair<string, T_UserInfo> user in ServerApp.GetInstance().UserInfoCache)
            //if (System.DateTime.Now - user.Value.ActivityDate > ServerGlobalData.UserInfoCacheTimeout)
            //{
            //    if (ServerApp.GetInstance().UserInfoCache.ContainsKey(user.Value.UserTicket))
            //        ServerApp.GetInstance().UserInfoCache.Remove(user.Value.UserTicket);
            //}

            List<string> userTickets = new List<string>();
            foreach (KeyValuePair<string, T_UserInfo> user in ServerApp.GetInstance().UserInfoCache)
            {
                if (System.DateTime.Now - user.Value.ActivityDate > ServerGlobalData.UserInfoCacheTimeout)
                {
                    userTickets.Add(user.Value.UserTicket);
                }
            }

            for (int i = userTickets.Count - 1; i >= 0; i--)
            {
                if (ServerApp.GetInstance().UserInfoCache.ContainsKey(userTickets[i]))
                    ServerApp.GetInstance().UserInfoCache.Remove(userTickets[i]);
            }
        }

        //检测是否注册
        private void CheckRegInfo()
        {
            if (ServerGlobalData.IsNeedRegService)
            {
                string[] hardwareInfo;     //硬件信息
                string hardwareSN = "";    //硬件序列号
                string serverRegKey = "";  //注册码

                serverRegKey = ConfigurationSettings.AppSettings["ServerRegKey"];

                //根据硬件信息得到加密后的序列号
                hardwareInfo = EncryptHelper.GetHardwareSN();
                hardwareSN = EncryptHelper.MD5Encrypt("HUHM"+hardwareInfo[0]+hardwareInfo[1]);

                //比较加密后的序列号与注册码是否一致
                if (EncryptHelper.AESEncrypt(hardwareSN, "xxK9stU" + ServerGlobalData.RegEncryptKey) != serverRegKey)
                    throw new Exception("Invalid RegKey,Hardware SN is(无效的注册码,机器序列号是): " + hardwareSN);

            }
        }

        #endregion 
 
    }
}
