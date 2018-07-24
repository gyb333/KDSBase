using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KDS.UI.Component.Forms
{
    /// <summary>
    /// 状态消息参数
    /// huhm2008
    /// </summary>
    public class StatusMessageEventArgs : EventArgs
    {
        /// <summary>
        /// 消息窗体类型（1-StatusBar；2-WaitWindow)
        /// </summary>
        public int Type;

        /// <summary>
        /// 消息
        /// </summary>
        public string Message;

        /// <summary>
        /// 进度（>=0实际进度)
        /// </summary>
        public int Progress=0;

        /// <summary>
        /// 是否显示进度条
        /// </summary>
        public bool ShowProgressBar=false;
    }

    /// <summary>
    /// 状态消息委托
    /// huhm2008
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void StatusMessageEventHandler(Object sender, StatusMessageEventArgs e);
}
