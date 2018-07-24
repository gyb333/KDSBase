using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SqlClient;
using System.ServiceModel;
/***********************************************************
 * 功能：ServerException
 * 说明：
 *       1.封装了错误处理
 * 
 * 创建：胡海明,huhaiming@gmail.com
 * 时间：2008/08
 ************************************************************/
namespace KDS.Server.Helper
{
    /// <summary>
    /// 服务端异常自动包装类
    /// huhm2008
    /// </summary>
    [Serializable]
    public class ServerExceptionHelper
    {
        private ServerExceptionHelper()
        {
            //禁止示例化本类

        }

        /// <summary>
        /// 转换异常消息
        /// huhm2008
        /// </summary>
        /// <param name="ex">异常</param>
        /// <returns></returns>
        public static FaultException TransException(Exception ex)
        {
            string strErrMsg = "";

            if (ex is SqlException)
            {
                SqlException sqlex = ex as SqlException;

                strErrMsg = "[错误编码：" + sqlex.Number.ToString() + "]";

                switch (sqlex.Number)
                {
                    case -2146232060:
                        strErrMsg = strErrMsg + ex.Message + "业务服务器与数据库连接失败。";   //huhm备注：隐藏详细的错误，以避免暴露给客户端
                        break;

                    case 3961: //Field rule violated//数据库 '%1!' 中的快照隔离事务失败，因为自此事务启动后，该语句所访问的对象已由其他并发事务中的 DDL 语句修改。这是不允许的，因为未对元数据进行版本控制。如果与快照隔离混合，对元数据的并发更新可能导致不一致。
                    case 3960: //Other User Lock the table or record//数据库 '%1!' 中的快照隔离事务失败，因为自此事务启动后，该语句所访问的对象已由其他并发事务中的 DDL 语句修改。这是不允许的，因为未对元数据进行版本控制。如果与快照隔离混合，对元数据的并发更新可能导致不一致。
                        strErrMsg = strErrMsg + "更新资料冲突：其它用户在此期间修改了此资料，请放弃此资料的变更，重新录入数据。";
                        break;

                    case 547://%1! 语句与 %2! 约束"%3!"冲突。该冲突发生于数据库"%4!"，表"%5!"%6!%7!%8!。//外键冲突
                        strErrMsg = strErrMsg + "资料已使用，不允许删除或更改。";
                        break;

                    case 1088://Denied to access database//找不到对象 "%1!"，因为它不存在或者您没有所需的权限。
                        strErrMsg = strErrMsg + "不能打开数据库表，可能是没有存取数据表的权限或系统管理在调试程序。";
                        break;

                    case 2627://Key conflict//违反了 %1! 约束 '%2!'。不能在对象 '%3!' 中插入重复键。
                    case 2601://不能在具有唯一索引 '%2!' 的对象 '%1!' 中插入重复键的行。
                        strErrMsg = strErrMsg + "代码或编码不能重复。";
                        break;

                    case 8152:
                        strErrMsg = strErrMsg + "输入的文本长度过长或含有非法字符。";
                        break;

                    default://Other all 
                        strErrMsg = strErrMsg+ex.Message;
                        break;

                }
            }
            else
            {
                strErrMsg = ex.Message;
            }

#if (DEBUG)
            strErrMsg = strErrMsg + Environment.NewLine + Environment.NewLine + 
                "调试信息：对象：" + ex.Source + "，方法" + ex.TargetSite+Environment.NewLine+
                "堆栈："+ex.StackTrace;   //huhm备注: 调试时提供给程序员更多的帮助，为安全起止仅调试时可用
#endif

            return new FaultException(strErrMsg);
        }
    }
}
