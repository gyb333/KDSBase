using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;

/* ==========================================================================
 *  消息窗体
 *  
 *  作者：胡海明 (huhaiming@gmail.com)
 *  日期：2008/08
 * 
 *  功能：

 *==========================================================================*/
namespace KDS.UI.Component.Forms
{
    /// <summary>
    /// 消息窗体
    /// huhm2008
    /// </summary>
    public class MyMessageWindow: BaseForm
    {

        private LableBase lblMsg;
        private PictureBox pictureBox1;
    
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MyMessageWindow));
            this.lblMsg = new KDS.UI.Component.LableBase();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // _txtDefaultFocus
            // 
            this._txtDefaultFocus.AccessibleDescription = null;
            this._txtDefaultFocus.AccessibleName = null;
            resources.ApplyResources(this._txtDefaultFocus, "_txtDefaultFocus");
            this._txtDefaultFocus.BackgroundImage = null;
            this._txtDefaultFocus.Font = null;
            this._txtDefaultFocus.UseWaitCursor = true;
            // 
            // lblMsg
            // 
            this.lblMsg.AccessibleDescription = null;
            this.lblMsg.AccessibleName = null;
            resources.ApplyResources(this.lblMsg, "lblMsg");
            this.lblMsg.BackColor = System.Drawing.Color.Transparent;
            this.lblMsg.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.UseWaitCursor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.AccessibleDescription = null;
            this.pictureBox1.AccessibleName = null;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = null;
            this.pictureBox1.Font = null;
            this.pictureBox1.Image = global::KDS.UI.Component.Properties.Resources.info;
            this.pictureBox1.ImageLocation = null;
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            this.pictureBox1.UseWaitCursor = true;
            // 
            // MyMessageWindow
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.BackColor = System.Drawing.Color.LightBlue;
            this.BackgroundImage = null;
            this.ControlBox = false;
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.pictureBox1);
            this.Font = null;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = null;
            this.Name = "MyMessageWindow";
            this.ShowIcon = false;
            this.UseWaitCursor = true;
            this.Controls.SetChildIndex(this.pictureBox1, 0);
            this.Controls.SetChildIndex(this.lblMsg, 0);
            this.Controls.SetChildIndex(this._txtDefaultFocus, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public MyMessageWindow()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 消息窗体
        /// </summary>
        /// <param name="msg">消息描述</param>
        public void ShowMsg(string msg)
        {
            this.lblMsg.Text = msg;
            this.Visible = true;
            this.BringToFront();
            this.Refresh();
        }

        /// <summary>
        /// 显示消息窗体
        /// </summary>
        /// <param name="msg">消息描述</param>
        /// <param name="waitMilliSeconds">等待毫秒数</param>
        public static void Show(string msg, int waitMilliSeconds)
        {
            MyMessageWindow myMessageWindow = new MyMessageWindow();
            myMessageWindow.ShowMsg(msg);

            if (waitMilliSeconds < 500 || waitMilliSeconds > 10 * 1000)
            {
                waitMilliSeconds = 1 * 1000;
            }

            System.Threading.Thread.Sleep(waitMilliSeconds);
            myMessageWindow.Close();
        }

        /// <summary>
        /// 显示消息窗体（等待1秒）
        /// </summary>
        /// <param name="msg">消息描述</param>
        public static void Show(string msg)
        {
            MyMessageWindow.Show(msg, 1000);
        }
    }
}
