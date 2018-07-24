using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Description;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;

using KDS.Client.App;

/* ==========================================================================
 *  ClientMessageInspector类
 *  
 *  作者：胡海明 (huhaiming@gmail.com)
 *  日期：2008/08
 * 
 *  说明：
 *        1.扩展WCF消息MessageHeader；
 *==========================================================================*/
namespace KDS.Client.Helper
{
    public sealed class ClientMessageInspector : IClientMessageInspector
    {
        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
        }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            //DEUBG-MODIFY:待改,huhm2008/08
            MessageHeader mh = MessageHeader.CreateHeader("UserTicket", "HHMUINS", ClientGlobalData.UserTicket);

            request.Headers.Add(mh);

            return null;
        }
    }

    public sealed class ClientEndpointBehavior : BehaviorExtensionElement, IEndpointBehavior
    {
        #region IEndpointBehavior 成员

        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        { }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            clientRuntime.MessageInspectors.Add(new ClientMessageInspector());
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
        }

        public void Validate(ServiceEndpoint endpoint)
        {
           
        }

        #endregion

        public override Type BehaviorType
        {
            get { return typeof(ClientEndpointBehavior); }
        }

        protected override object CreateBehavior()
        {
            return new ClientEndpointBehavior();
        }
    }
}
