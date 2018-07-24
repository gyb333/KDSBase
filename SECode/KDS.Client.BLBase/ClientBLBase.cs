using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KDS.Client.App;
using KDS.Model;
/***********************************************************
 * 功能：客户端业务逻辑基类
 *  
 * 创建：胡海明,huhaiming@gmail.com
 * 时间：2008/08
  ************************************************************/
namespace KDS.Client.BLBase
{
    /// <summary>
    /// 客户端BL基类
    /// 最终业务类的实现要重载：InitProperty()
    /// 
    /// huhm2008
    /// </summary>
    public abstract class ClientBLBase
    {
        /// <summary>
        /// 当前的ClientApp实例
        /// </summary>
        protected ClientApp mClientApp;

        /// <summary>
        /// 本BL的应用权限标识，用于控制权限（初始-1，必须设置>=0）
        /// </summary>
        protected int mAppID = -1;
        /// <summary>
        /// 本BL的应用权限标识，用于控制权限（初始-1，必须设置>=0）
        /// </summary>
        public int AppID
        {
            get
            {
                return this.mAppID;
            }
        }

        /// <summary>
        /// 子功能ID。（可选项，初始-1，需要时请设置>0）。（适用于一个逻辑执行多个功能时的功能区分，如销量报表包括：1111-品牌分析；1112-客户渠道分析等）
        /// </summary>
        public int SubFuncID = -1;


        protected bool mACLView = false;
        /// <summary>
        /// 查看权限
        /// </summary>
        public bool ACLView
        {
            get
            {
                return this.mACLView;
            }
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="clientApp"></param>
        public ClientBLBase(ClientApp clientApp)
        {
            this.InitProperty();

            this.mClientApp = clientApp;

            this.AfterInitProperty();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="clientApp"></param>
        /// <param name="appID">AppID，应用程序标识</param>
        public ClientBLBase(ClientApp clientApp, int appID)
        {
            this.InitProperty();

            this.mClientApp = clientApp;
            this.mAppID = appID;

            this.AfterInitProperty();
        }

        /// <summary>
        /// 初始化属性：（构造函数前调用。如属性mAppID设置，顺序应：1.先设置本类属性；2.再执行基类；）
        /// 最终业务类的实现要设置的有：mAppID、mAllowAdd、mAllowModify、mAllowDelete
        /// </summary>
        protected virtual void InitProperty()
        {

        }

        /// <summary>
        /// 初始化属性后的处理（构造函数后调用。如权限检查等，顺序应：1.先设置本类属性；2.再执行基类；）
        /// </summary>
        protected virtual void AfterInitProperty()
        {
            this.mACLView = this.mClientApp.CheckUserFuncAccess(this.mAppID, "查看");

            if (this.mAppID < 0)
                throw new Exception("客户端-请设置AppID属性");

            if (!this.mACLView)
                throw new Exception("客户端-您没有权限");

        }
    }
}
