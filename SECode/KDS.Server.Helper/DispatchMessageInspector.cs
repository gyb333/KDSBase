using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Description;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
/* ==========================================================================
 *  DispatchMessageInspector类
 *  
 *  作者：胡海明 (huhaiming@gmail.com)
 *  日期：2008/08
 * 
 *  说明：
 *        1.扩展WCF消息MessageHeader；
 *==========================================================================*/
namespace KDS.Server.Helper
{
    public sealed class DispatchMessageInspector : IDispatchMessageInspector
    {
        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            return null;
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            return; 
        }
    }


    public sealed class ServiceOperationBehavior : BehaviorExtensionElement, IEndpointBehavior
    {

        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            endpointDispatcher.DispatchRuntime.MessageInspectors.Add(new DispatchMessageInspector());
        }

        public void Validate(ServiceEndpoint endpoint)
        {
        }

        public override Type BehaviorType
        {
            get
            {
                return typeof(ServiceOperationBehavior);
            }
        }
        protected override object CreateBehavior()
        {
            return new ServiceOperationBehavior();
        }
    }

}
