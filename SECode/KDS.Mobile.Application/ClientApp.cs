using System;
using System.Collections.Generic;
using System.Text;

using KDS.Model;
using System.Configuration;
/* ==========================================================================
 *  客户端应用程序控制类
 *  
 *  作者：胡海明 (huhaiming@gmail.com)
 *  日期：2008/08
 * 
 *  说明：
 *        1.此程序原型来源于CSS系统(2001-2008,huhaiming)的oApp对象模型；
 *        2.客户端应用程序全局控制类；
 *        3.加载权限、缓存、全局参数、服务等对象；
 *==========================================================================*/
namespace KDS.Client.App
{
    /// <summary>
    /// 客户端应用程序对象,huhm2008
    /// </summary>
    public sealed class ClientApp
    {
        private T_UserInfo mCurrentUserInfo=new T_UserInfo();
        /// <summary>
        /// 当前登录用户信息,huhm2008
        /// </summary>
        public T_UserInfo CurrentUserInfo
        {
            get
            {
                return mCurrentUserInfo;
            }
            set
            {
                mCurrentUserInfo = value;
                ClientGlobalData.UserTicket = value.UserTicket;
            }
        }


        /// <summary>
        /// 系统参数Cache，KeyName/KeyValue
        /// </summary>
        public Dictionary<string, string> SysDataCache = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

        /// <summary>
        /// 当前内存Cache数据,TableName/Data 
        /// huhm2008
        /// </summary>
        public Dictionary<string, object> DataCache = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);


        public ClientApp()
        {
            //创建全部变量设置
            this.InitGlobalData();
            this.InitT_UserInfo();
        }


        //初始化全局变量
        private bool InitGlobalData()
        {
            bool retVal=true;

            ClientGlobalData.UserTicket = string.Empty;
 
            return retVal;
        }


        //初始化用户信息
        private bool InitT_UserInfo()
        {
            bool retVal = true;

            this.CurrentUserInfo.UserID = 0;
            this.CurrentUserInfo.CompanyID=0;
            this.CurrentUserInfo.UserLevel = 0;
            this.CurrentUserInfo.UserTicket = string.Empty;

            //继续添加

            return retVal;
        }


        #region ACL
        /// <summary>
        /// 获取用户是否有指定APP模块的功能权限
        /// 以下情况始终返回True：1.管理员(UserLevel=1普通管理员或-1超级管理员) ；appID小于或等于0
        /// </summary>
        /// <param name="appID">AppID</param>
        /// <param name="funcValue">功能权限标识，如：修改</param>
        /// <returns>bool值：有/无</returns>
        /// <remarks>数据库中的FuncValue形如：[新增],[修改],</remarks>
        public bool CheckUserFuncAccess(int appID, string funcValue)
        {
            bool lRetVal = true;

            try
            {
                if ((this.CurrentUserInfo.UserLevel == -1) || (this.CurrentUserInfo.UserLevel == 1) || appID <= 0)
                {
                    // 即true;
                }
                else
                {
                    string funcString = "";
                    if (this.CurrentUserInfo.UserFuncAccessInfo.TryGetValue(appID, out funcString))
                    {
                        lRetVal = (funcString.IndexOf("[" + funcValue + "]") >= 0);
                    }
                    else
                        lRetVal = false;
                }
            }
            catch
            {
                lRetVal = false;
                throw;
            }

            return lRetVal;
        }

        #endregion 


        #region SysData

        /// <summary>
        /// 从缓存中取系统参数
        /// </summary>
        /// <param name="companyID"></param>
        /// <param name="keyName"></param>
        /// <returns>string类型的参数值</returns>
        public string GetSysData(string keyName)
        {
            string retVal = "";

            try
            {
                if (this.SysDataCache.TryGetValue(keyName, out retVal))
                {
                    //ok
                }
                else
                {
                    retVal = "";
                }
            }
            catch
            {
                throw;
            }

            return retVal;
        }

        #endregion 

    }
}
