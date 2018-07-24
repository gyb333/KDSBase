using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KDS.Model;
using KDS.Server.App;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;
using System.Transactions;
/***********************************************************
 * 功能：服务端业务逻辑抽象基类
 *  
 * 创建：胡海明,huhaiming@gmail.com
 * 时间：2008/08
  ************************************************************/
namespace KDS.Server.BLBase
{
    /// <summary>
    /// 服务端BL抽象基类
    /// 最终业务类的实现要重载：InitProperty()
    /// 
    /// huhm2008
    /// </summary>
    public abstract class ServerBLBase
    {
        /// <summary>
        /// 当前会话中的UserInfo实例
        /// </summary>
        protected T_UserInfo mUserInfo;

        /// <summary>
        /// 本BL的应用权限标识，用于控制权限（初始-1，必须设置>=0）
        /// </summary>
        protected int mAppID = -1;

        /// <summary>
        /// 服务端业务逻辑
        /// </summary>
        /// <param name="userInfo">当前会话的UserInfo实例</param>
        public ServerBLBase(T_UserInfo userInfo)
        {
            this.InitProperty();

            this.mUserInfo = userInfo;

            this.AfterInitProperty();
        }

        /// <summary>
        /// 服务端业务逻辑
        /// </summary>
        /// <param name="userInfo">当前会话的UserInfo实例</param>
        /// <param name="appID">AppID，应用程序标识</param>
        public ServerBLBase(T_UserInfo userInfo,int appID)
        {
            this.InitProperty();

            this.mAppID = appID;
            this.mUserInfo = userInfo;

            this.AfterInitProperty();
        }

        /// <summary>
        /// 初始化属性：（构造函数前调用。如属性mAppID设置，顺序应：1.先设置本类属性；2.再执行基类；）
        /// 最终业务类的实现要设置的有：mAppID
        /// </summary>
        protected virtual void InitProperty()
        {

        }


        /// <summary>
        /// 初始化属性后的处理（构造函数后调用。如权限检查等，顺序应：1.先设置本类属性；2.再执行基类；）
        /// </summary>
        protected virtual void AfterInitProperty()
        {
            if (this.mAppID < 0)
                throw new Exception("服务-请设置AppID属性");

            if (!this.CheckUserFuncAccess("查看"))
                throw new Exception("服务端-您没有权限");
        }

        /// <summary>
        /// 获取用户是否有指定APP模块的功能权限
        /// 以下情况始终返回True：1.管理员(UserLevel=1普通管理员或-1超级管理员) ；appID小于或等于0
        /// </summary>
        /// <param name="appID">AppID</param>
        /// <param name="funcValue">功能权限标识，如："修改"</param>
        /// <returns>bool值：有/无</returns>
        /// <remarks>数据库中的FuncValue形如："[新增],[修改]"</remarks>
        protected bool CheckUserFuncAccess(string funcValue)
        {
            bool lRetVal = true;
            int appID = this.mAppID;

            if (appID == 0)
                return true;

            if (this.mUserInfo == null)
            {
                lRetVal = false;
                return lRetVal;
            }

            try
            {
                if ( (this.mUserInfo.UserLevel == -1) || (this.mUserInfo.UserLevel == 1) )
                {
                    // 即true;
                }
                else
                {
                    string funcString = "";
                    if (this.mUserInfo.UserFuncAccessInfo.TryGetValue(appID, out funcString))
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



        /// <summary>
        /// 获取用户是否有指定对象的的数据访问权限
        /// 以下情况始终返回True：1.管理员(UserLevel=1普通管理员或-1超级管理员) 2.用户的权限对象值为：[-999,]
        /// </summary>
        /// <param name="objectName">对象名称，如："Brand"</param>
        /// <param name="objectValue">对象值（如BrandID的值：10001等）</param>
        /// <returns>bool值：有/无。</returns>
        /// <remarks>数据库中的objectValue形如："123,456,789" 或此字段的全部权限："-999"</remarks>
        protected bool CheckUserObjectAccess(string objectName, int objectValue)
        {
            bool lRetVal = true;

            if (this.mUserInfo == null)
            {
                lRetVal = false;
                return lRetVal;
            }

            try
            {
                if ( (this.mUserInfo.UserLevel == -1) || (this.mUserInfo.UserLevel == 1) )
                {
                    // 即true;
                }
                else
                {
                    string valueString = "";
                    if (this.mUserInfo.UserObjectAccessInfo.TryGetValue(objectName, out valueString))
                    {
                        string valueString2 = valueString+",";
                        if (valueString2.IndexOf("-999,") >= 0)  //全部
                        {
                            //即true
                        }
                        else
                        {
                            lRetVal = (valueString2.IndexOf(objectValue.ToString() + ",") >= 0);
                        }
                    }
                    else
                    {
                        if (this.mUserInfo.UserObjectAccessInfo.Count > 0)
                        {
                            //即true
                        }
                        else
                        {
                            lRetVal = false;
                        }
                    }
                }
            }
            catch
            {
                lRetVal = false;
                throw;
            }

            return lRetVal;
        }


        /// <summary>
        //  获取用户指定对象的的数据访问权限的SQL WHERE语句，如：and 1=1 、and BrandID in (123,456)
        /// 以下情况始终返回and 1=1：1.管理员(UserLevel=1普通管理员或-1超级管理员) 2.用户的权限对象值为：[-999]
        /// 为了提高SQL检索性能，授权时对象的ValueList不应太多值否则太多的条件如 in (1,2,3, ... x,y,z....)
        /// </summary>
        /// <param name="objectName">对象名称，如：Brand</param>
        /// <param name="objectFieldName">对象当前SQL语句对应的字段名，如tp.BrandID</param>
        /// <returns>返回SQL条件语句，如："and 1=1 "、" and BrandID in (123,456) "</returns>
        /// <remarks>数据库中的objectValue形如："123,456,789" 或此字段的全部权限："-999"</remarks>
        protected string GetUserObjectAccessSqlWhere(string objectName, string objectFieldName)
        {
            string cRetVal = "";

            if (this.mUserInfo == null)
            {
                cRetVal = " and 1=2 ";
                return cRetVal;
            }

            try
            {
                if ((this.mUserInfo.UserLevel == -1) || (this.mUserInfo.UserLevel == 1))
                {
                    //即OK 
                }
                else
                {
                    string valueString = "";
                    if (this.mUserInfo.UserObjectAccessInfo.TryGetValue(objectName, out valueString))
                    {
                        string valueString2 = valueString + ",";
                        if (valueString2.IndexOf("-999,")>=0)  //全部
                        {
                            //即OK 
                        }
                        else
                        {
                            cRetVal = " and " + objectFieldName + " in (" + valueString + ")";
                        }
                    }
                    else
                    {
                        if (this.mUserInfo.UserObjectAccessInfo.Count > 0)
                        {
                            //即OK
                        }
                        else
                        {
                            cRetVal = " and 1=2 ";
                        }
                    }
                }
            }
            catch
            {
                cRetVal = " and 1=2 ";
                throw;
            }

            if (cRetVal == "")
                cRetVal = " and 1=1 ";
            return cRetVal;
        }

        /// <summary>
        /// 获取用户指定对象的的数据访问权限的SQL WHERE语句(支持默认值)，如：and 1=1 、and BrandID in (123,456)
        /// 以下情况始终返回and 1=1：1.管理员(UserLevel=1普通管理员或-1超级管理员) 2.用户的权限对象值为：[-999]
        /// 为了提高SQL检索性能，授权时对象的ValueList不应太多值否则太多的条件如 in (1,2,3, ... x,y,z....)
        /// </summary>
        /// <param name="objectName">对象名称，如：Brand</param>
        /// <param name="objectFieldName">对象当前SQL语句对应的字段名，如tp.BrandID</param>
        /// <param name="defaultValue">默认值，如0，此值不能为-999</param>
        /// <param name="allowNull">是否允许Null值，默认为true</param>
        /// <returns>返回SQL条件语句，如："and 1=1 "、" and BrandID in (123,456) "</returns>
        /// <remarks>数据库中的objectValue形如："123,456,789" 或此字段的全部权限："-999"</remarks>
        protected string GetUserObjectAndDefaultObjectAccessSqlWhere(string objectName, string objectFieldName, string defaultValue, bool allowNull = true)
        {
            string cRetVal = "";

            if (this.mUserInfo == null)
            {
                cRetVal = " and 1=2 ";
                return cRetVal;
            }

            if ((defaultValue + ",").IndexOf("-999,") >= 0)
            {
                throw new Exception("默认值不能为-999");
            }

            try
            {
                if ((this.mUserInfo.UserLevel == -1) || (this.mUserInfo.UserLevel == 1))
                {
                    //即OK 
                }
                else
                {
                    string valueString = "";
                    if (this.mUserInfo.UserObjectAccessInfo.TryGetValue(objectName, out valueString))
                    {
                        string valueString2 = valueString + ",";
                        if (valueString2.IndexOf("-999,") >= 0)  //全部
                        {
                            //即OK 
                        }
                        else
                        {
                            if (allowNull)
                            {
                                cRetVal = " and (" + objectFieldName + " in (" + defaultValue + "," + valueString + ")" + " or " + objectFieldName + " is null )";
                            }
                            else
                            {
                                cRetVal = " and " + objectFieldName + " in (" + defaultValue + "," + valueString + ")";
                            }
                        }
                    }
                    else
                    {
                        if (this.mUserInfo.UserObjectAccessInfo.Count > 0)
                        {
                            //即OK
                        }
                        else
                        {
                            cRetVal = " and 1=2 ";
                        }
                    }
                }
            }
            catch
            {
                cRetVal = " and 1=2 ";
                throw;
            }

            if (cRetVal == "")
                cRetVal = " and 1=1 ";
            return cRetVal;
        }

        /// <summary>
        /// 从缓存中取系统参数
        /// </summary>
        /// <param name="distributorID"></param>
        /// <param name="companyID"></param>
        /// <param name="keyName"></param>
        /// <returns>string类型的参数值</returns>
        protected string GetSysData(int distributorID, string keyName)
        {
            string retVal = "";

            if (this.mUserInfo == null)
            {
                retVal = "";
                return retVal;
            }

            try
            {
                DataTable dtSysData;

                if (ServerApp.GetInstance().SysDataCache.TryGetValue(this.mUserInfo.CompanyID, out dtSysData))
                {
                    DataRow[] rows = dtSysData.Select("DistributorID = "+ distributorID.ToString()+" and KeyName='" + keyName+"'");
                    if (rows == null || rows.Length == 0)
                    {
                        retVal = "";
                    }
                    else
                    {
                        retVal= rows[0]["KeyValue"].ToString();
                    }
                }
            }
            catch
            {
                throw;
            }

            return retVal;
        }

        /// <summary>
        /// 根据TableName获取表主键
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns>主键值：0-失败；其他成功</returns>
        protected object GetIdentity(string tableName)
        {
            object retVal = null;

            try
            {
                using (TransactionScope scope=new TransactionScope(TransactionScopeOption.Suppress))
                {
                    string sqlCommand = "prGetIdentity";
                    Database db = DatabaseFactory.CreateDatabase();
                    DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
                    db.AddInParameter(dbCommand, "chvTableName", DbType.String, tableName);
                    db.AddOutParameter(dbCommand, "intSysID", DbType.Int32, 4);

                    db.ExecuteNonQuery(dbCommand);

                    scope.Complete();

                    retVal=db.GetParameterValue(dbCommand, "intSysID");
                }
            }
            catch
            {
                throw;
            }

            return retVal;
        }

    }
}
