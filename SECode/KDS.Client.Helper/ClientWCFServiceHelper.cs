using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.ServiceModel.Channels;
using KDS.Client.App;
/* ==========================================================================
 *  ServiceHelper类
 *  
 *  作者：胡海明 (huhaiming@gmail.com)
 *  日期：2008/08
 * 
 *  说明：
 *        1.封装WCF代理模型；
 *==========================================================================*/
namespace KDS.Client.Helper
{
    /// <summary>
    /// 服务类型
    /// huhm2008
    /// </summary>
    public enum ServiceType
    {
        /// <summary>
        /// 数据服务
        /// </summary>
        Data,

        /// <summary>
        /// 安全传输服务
        /// </summary>
        Security
    }

    /// <summary>
    /// 服务地址类型
    /// huhm2008
    /// </summary>
    public enum ServiceAddressType
    {
        /// <summary>
        /// 安全服务
        /// </summary>
        Security,

        /// <summary>
        /// 系统公共服务
        /// </summary>
        Sys,

        /// <summary>
        /// KDS专用服务
        /// </summary>
        KDS,

        /// <summary>
        /// KBP专用服务
        /// </summary>
        KBP,

        /// <summary>
        /// Sync服务
        /// </summary>
        Sync
    }

    /// <summary>
    /// WCF服务,huhm2008
    /// </summary>
    public sealed class ClientWCFServiceHelper
    {
        readonly static Dictionary<Type, object> channelsCache = new Dictionary<Type, object>();
        readonly static object channelsLock = new object();

        private ClientWCFServiceHelper()
        {

        }

        /// <summary>
        /// 获取数据传输的Binding
        /// huhm2008
        /// </summary>
        /// <returns></returns>
        public static NetTcpBinding GetDataBinding(int openTimeout, int receiveTimeout)
        {
            NetTcpBinding netTcpBinding;

            //全局的TCP设置
            netTcpBinding = new NetTcpBinding("Client_TcpBinding");
            netTcpBinding.ReceiveTimeout = TimeSpan.FromMinutes(receiveTimeout);
            netTcpBinding.SendTimeout = TimeSpan.FromMinutes(receiveTimeout);
            netTcpBinding.CloseTimeout = TimeSpan.FromMinutes(openTimeout);
            netTcpBinding.OpenTimeout = TimeSpan.FromMinutes(openTimeout);
            

            //Fixed 取默认值
            return netTcpBinding;
        }

        /// <summary>
        /// 获取数据传输的Binding
        /// huhm2008
        /// </summary>
        /// <returns></returns>
        public static NetTcpBinding GetDataBinding()
        {
            NetTcpBinding netTcpBinding;

            //全局的TCP设置
            netTcpBinding = new NetTcpBinding("Client_TcpBinding");
            //netTcpBinding.Security.Mode = SecurityMode.None;
            //netTcpBinding.SendTimeout = ClientGlobalData.ClientSendTimeout;
            //netTcpBinding.ReceiveTimeout = ClientGlobalData.ClientReceiveTimeout;
            //netTcpBinding.MaxReceivedMessageSize = ClientGlobalData.ClientMaxReceivedMessageSize;


            //Fixed 取默认值
            return netTcpBinding;
        }


        /// <summary>
        /// 获取安全通道的Binding
        /// huhm2008
        /// </summary>
        /// <returns></returns>
        public static WSHttpBinding GetSecurityBinding()
        {
            WSHttpBinding wsHttpBinding = new WSHttpBinding();

            throw new Exception("功能预留，待补充--huhaiming,2008.");

            return wsHttpBinding;
        }

        /// <summary>
        /// 获取终结点
        /// huhm2008
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <param name="relativeAddress">契约的相对地址</param>
        /// <returns></returns>
        public static EndpointAddress GetEndPointAddress(ServiceType serviceType, string relativeAddress)
        {
            EndpointAddress endpointAddress;
            string strEndpointAddress = "";

            //DEBUG-MODIFY：最后改为动态地址，待修改，huhaiming,2008/08
            //客户端从服务器获取地址，不使用基于配置的方法
            string absoluteDataEndpointAddress = ClientGlobalData.ServerInfo.AbsoluteDataEndpointAddress;
            string absoluteSecurityEndpointAddress = ClientGlobalData.ServerInfo.AbsoluteSecurityEndpointAddress;

            switch (serviceType)
            {
                case ServiceType.Data:
                    strEndpointAddress = absoluteDataEndpointAddress + relativeAddress;
                    break;

                case ServiceType.Security:
                    strEndpointAddress = absoluteSecurityEndpointAddress + relativeAddress;
                    break;
            }

            endpointAddress = new EndpointAddress(strEndpointAddress);

            return endpointAddress;
        }


        /// <summary>
        /// 创建通道实例，huhm2008
        /// </summary>
        /// <typeparam name="T">服务接口</typeparam>
        /// <param name="binding">使用的绑定</param>
        /// <param name="endpointAddress">终结点</param>
        /// <returns>已封装了客户端行为的接口通道新实例</returns>
        public static T CreateChannel<T>(Binding binding, EndpointAddress endpointAddress)
        {
            ChannelFactory<T> factory = new ChannelFactory<T>(binding, endpointAddress);
            factory.Endpoint.Behaviors.Add(new ClientEndpointBehavior());
            return factory.CreateChannel();

            //Type serviceType = typeof(T);
            //object service;

            //lock (channelsLock)
            //{
            //    if (!channelsCache.TryGetValue(serviceType, out service))
            //    {
            //        ChannelFactory<T> factory = new ChannelFactory<T>(binding, endpointAddress);
            //        factory.Endpoint.Behaviors.Add(new ClientEndpointBehavior());
            //        service = factory.CreateChannel();

            //        var communicationObject = (ICommunicationObject)service;
            //        //communicationObject.Faulted += OnChannelFaulted;
            //        channelsCache.Add(serviceType, service);
            //        communicationObject.Open(); /* Explicit opening of the channel 
            //                                     * avoids a performance hit.  */
            //    }
            //}

            //return (T)service;
        }


        /// <summary>
        /// 创建通道实例，huhm2008
        /// 默认将缓存通道
        /// </summary>
        /// <typeparam name="T">服务接口</typeparam>
        /// <param name="serviceAddressType">服务地址类型</param>
        /// <param name="openTimeout">打开连接超时时间默认2分钟</param>
        /// <param name="receiveTimeout">接收数据超时时间默认5分钟</param>
        /// <returns>已封装了客户端行为的接口通道新实例</returns>
        public static T CreateChannel<T>(ServiceAddressType serviceAddressType, int openTimeout, int receiveTimeout)
        {
            return ClientWCFServiceHelper.CreateChannel<T>(ClientWCFServiceHelper.GetDataBinding(openTimeout, receiveTimeout),
                ClientWCFServiceHelper.GetEndPointAddress(ServiceType.Data, serviceAddressType.ToString()));
        }


        /// <summary>
        /// 创建通道实例，huhm2008
        /// </summary>
        /// <typeparam name="T">服务接口</typeparam>
        /// <param name="serviceAddressType">服务地址类型</param>
        /// <returns>已封装了客户端行为的接口通道新实例</returns>
        public static T CreateChannel<T>(ServiceAddressType serviceAddressType)
        {
            return ClientWCFServiceHelper.CreateChannel<T>(ClientWCFServiceHelper.GetDataBinding(),
                ClientWCFServiceHelper.GetEndPointAddress(ServiceType.Data, serviceAddressType.ToString()));
        }

        void OnChannelFaulted(object sender, EventArgs e)
        {
            var communicationObject = (ICommunicationObject)sender;
            communicationObject.Faulted -= OnChannelFaulted;

            lock (channelsLock)
            {
                var keys = from pair in channelsCache
                           where pair.Value == communicationObject
                           select pair.Key;

                foreach (var key in keys.ToList())
                {
                    channelsCache.Remove(key);
                }
            }
        }

    }
}
