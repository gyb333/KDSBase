using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;

/***********************************************************
 * 功能：ExceptionMessageHelper
 * 说明：
 *       1.封装了错误消息转换
 * 
 * 创建：胡海明,huhaiming@gmail.com
 * 时间：2008/08
 ************************************************************/
namespace KDS.Client.Helper
{
    public class ExceptionMessageHelper
    {
        private ExceptionMessageHelper()
        {
            //禁止示例化本类
        }

        /// <summary>
        /// 转换异常消息
        /// huhm2008
        /// </summary>
        /// <param name="ex">异常</param>
        /// <returns></returns>
        public static string Trans(Exception ex)
        {
            string strErrMsg = "";

            if (ex is FaultException)
            {
                strErrMsg = "与服务器交互时发生错误：" + ex.Message;
            }
            else if (ex is CommunicationException)
            {
                strErrMsg = "数据通讯失败。可能原因：1.客户端网络中断或网络环境较差；2.防火墙拦截；3.服务器不可用。原始错误信息："+ex.Message;
            }
            else
            {
                strErrMsg = ex.Message;
            }

            return strErrMsg;
        }
    }
}
