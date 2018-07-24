using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KDS.UI.Component.Forms;
using KDS.Client.App;
/* ==========================================================================
 *  App管理基类
 *  
 *  作者：胡海明 (huhaiming@gmail.com)
 *  日期：2008/08
 *
 *==========================================================================*/
namespace KDS.Client.BaseForms
{
    /// <summary>
    /// App管理基类接口
    /// huhm2008
    /// </summary>
    public interface IAppManagement
    {
        /// <summary>
        /// 创建程序窗体并返回窗体对象的引用
        /// </summary>
        /// <param name="clientApp">clientApp对象</param>
        /// <param name="appID">应用程序ID</param>
        /// <returns></returns>
        BaseForm CreateRunningForm(ClientApp clientApp,int appID);


        /// <summary>
        /// 创建程序窗体并返回窗体对象的引用
        /// </summary>
        /// <param name="clientApp">clientApp对象</param>
        /// <param name="appID">应用程序ID</param>
        /// <param name="subFuncID">子功能ID</param>
        /// <returns></returns>
        BaseForm CreateRunningForm(ClientApp clientApp,int appID, int subFuncID); 
    }
}
