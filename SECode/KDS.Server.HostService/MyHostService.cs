using System;
using System.Collections.Generic;
using System.Text;

using System.ServiceModel;

using KDS.Server.Helper;

//服务及合约
using KDS.Server.SysContract;
using KDS.Server.KBPContract;
using KDS.Server.KDSContract;

using KDS.Server.Service;
/* ==========================================================================
 *  HostService
 *  
 *  作者：胡海明 (huhaiming@gmail.com)
 *  日期：2008/08
 * 
 *
 *==========================================================================*/
namespace KDS.Server.HostService
{
    /// <summary>
    /// HostService服务类,huhm2008
    /// </summary>
    public sealed class MyHostService : IDisposable
    {
        private List<ServiceHost> mHosts;

        public MyHostService()
        {
            mHosts = new List<ServiceHost>();
        }

        /// <summary>
        /// 启动服务,huhaiming2008/08
        /// </summary>
        public void StartUp()
        {
            //DEBUG-huhm待处理: 1.为每个服务独立的Binding 2.PRD环境应配置限流


            //启动服务HSecurityService ------------------------------------------------------------------------------------
            try
            {
                ServiceHost host = new ServiceHost(typeof(SecurityService));

                for (int j = 0; j < host.Description.Endpoints.Count; j++)
                {
                    host.Description.Endpoints[j].Behaviors.Add(new ServiceOperationBehavior());
                }

                host.Open();
                mHosts.Add(host);
            }
            catch
            {
                throw;
            }

            //启动服务SysService ------------------------------------------------------------------------------------
            try
            {
                ServiceHost host = new ServiceHost(typeof(SysService));

                for (int j = 0; j < host.Description.Endpoints.Count; j++)
                {
                    host.Description.Endpoints[j].Behaviors.Add(new ServiceOperationBehavior());
                }

                host.Open();
                mHosts.Add(host);
            }
            catch
            {
                throw;
            }

            //启动服务KDSService ------------------------------------------------------------------------------------
            try
            {
                ServiceHost host = new ServiceHost(typeof(KDSService));

                for (int j = 0; j < host.Description.Endpoints.Count; j++)
                {
                    host.Description.Endpoints[j].Behaviors.Add(new ServiceOperationBehavior());
                }

                host.Open();
                mHosts.Add(host);
            }
            catch
            {
                throw;
            }

            //启动服务KBPService ------------------------------------------------------------------------------------
            try
            {
                ServiceHost host = new ServiceHost(typeof(KBPService));

                for (int j = 0; j < host.Description.Endpoints.Count; j++)
                {
                    host.Description.Endpoints[j].Behaviors.Add(new ServiceOperationBehavior());
                }

                host.Open();
                mHosts.Add(host);
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// 关闭服务
        /// </summary>
        public void Close()
        {
            foreach (ServiceHost host in mHosts)
            {
                if (host != null)
                {
                    host.Close();
                }
            }
        }

        #region IDisposable 成员

        public void Dispose()
        {
            this.Close();
        }

        #endregion
    }

}
