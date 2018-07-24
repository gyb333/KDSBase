using System;
using System.Diagnostics;


/***********************************************************
 * 功能：Log记录
 * 说明：
 *       1.封装了日志记录
 * 
 * 创建：胡海明,huhaiming@gmail.com
 * 时间：2008/08
 ************************************************************/
namespace KDS.SECommon
{
    /// <summary>
    /// 日志类型
    /// huhm2008
    /// </summary>
    public enum MsgType
    {
        /// <summary>
        /// 系统消息
        /// </summary>
        System,

        /// <summary>
        /// 异常
        /// </summary>
        Error,

        /// <summary>
        /// 普通消息
        /// </summary>
        Message
    }

    /// <summary>
    /// 日志类型
    /// huhm2008
    /// </summary>
    public enum LogFileType
    {
        /// <summary>
        /// 控制台
        /// </summary>
        Console,


        /// <summary>
        /// 事务日志
        /// </summary>
        Event,


        /// <summary>
        /// 不记录日志
        /// </summary>
        None
    }


    /// <summary>
    /// 日志处理
    /// huhm2008
    /// </summary>
    public sealed class LogHelper
    {
        /// <summary>
        /// 写日志
        /// huhm2008
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="logFileType">日志文件类型</param>
        /// <param name="msgType">消息类型</param>
        public static void Write(string message,string logFileType,MsgType msgType)
        {
            switch (logFileType)
            {
                //日志方式
                case "Event":   //LogFileType.Event:
                    EventLogEntryType et;
                    switch (msgType)
                    {
                        case MsgType.Error:
                            et = EventLogEntryType.Error;
                            break;
                        default:    //System&Message
                            et = EventLogEntryType.Information;
                            break;
                    }

                    EventLog eventLog = new EventLog("Application");
                    try
                    {
                        eventLog.WriteEntry("{" + AppDomain.CurrentDomain.ApplicationIdentity + "}[" + msgType.ToString() + "]("+System.DateTime.Now+")" + message, et);
                    }
                    catch
                    { }
                    finally
                    {
                        if (eventLog!=null)
                            eventLog.Dispose();
                    }

                    break;


                //控制台方式
                case "Console": //LogFileType.Console:
                    ConsoleColor fColor=Console.ForegroundColor;
                    switch (msgType)
                    {
                        case MsgType.Error:
                            Console.ForegroundColor = ConsoleColor.Red;
                            break;
                        case MsgType.System:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            break;
                        default:    //Message
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                    }
                    try
                    {
                        Console.WriteLine("{" + AppDomain.CurrentDomain.FriendlyName + "}[" + msgType.ToString() + "](" + System.DateTime.Now + ")" + message);
                    }
                    catch
                    { }

                    Console.ForegroundColor=fColor;
                    break;

                //不记录  None方式
                default:
                    break;
            }
        }
    }
}
