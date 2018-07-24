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
                //事务计数
                int pos= ex.Message.IndexOf("EXECUTE 后的事务计数");
                int pos2 = ex.Message.IndexOf("当前计数 = 0");
                if (pos >= 0 && pos2>=0)
                    strErrMsg = "服务器返回错误：" + ex.Message.Substring(0, pos);
                else
                    strErrMsg = "服务器返回错误：" + ex.Message;

                //快照隔离
                if (ex.Message.IndexOf("快照隔离事务由于更新冲突而中止。") >= 0)
                    strErrMsg = "数据更新冲突，请重新操作。 (snapshot) ";  // 原始消息：" + ex.Message;

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
