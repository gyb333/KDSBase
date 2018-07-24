using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;

/* ==========================================================================
 *  等待窗体
 *  
 *  作者：胡海明 (huhaiming@gmail.com)
 *  日期：2008/08
 * 
 *  功能：

 *==========================================================================*/
namespace KDS.UI.Component.Forms
{
    /// <summary>
    /// 等待窗体
    /// huhm2008
    /// </summary>
    public class MyWaitWindow: BaseForm
    {
        private int mProgress=0;
        /// <summary>
        /// 获取或设置当前进度
        /// </summary>
        public int Progress
        {
            get 
            { 
                return mProgress; 
            }

            set 
            {
                mProgress=value;
                mProgress = mProgress > 100 ? 100 : mProgress;
                mProgress = mProgress < 0 ? 0 : mProgress;

                this.progressBar1.Value = mProgress;
            }
        }


        private LableBase lblMsg;
        private PictureBox pictureBox1;
        private ProgressBar progressBar1;
    
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MyWaitWindow));
            this.lblMsg = new KDS.UI.Component.LableBase();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
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
            // progressBar1
            // 
            this.progressBar1.AccessibleDescription = null;
            this.progressBar1.AccessibleName = null;
            resources.ApplyResources(this.progressBar1, "progressBar1");
            this.progressBar1.BackgroundImage = null;
            this.progressBar1.Font = null;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.UseWaitCursor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.AccessibleDescription = null;
            this.pictureBox1.AccessibleName = null;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = null;
            this.pictureBox1.Font = null;
            this.pictureBox1.Image = global::KDS.UI.Component.Properties.Resources.find1;
            this.pictureBox1.ImageLocation = null;
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            this.pictureBox1.UseWaitCursor = true;
            // 
            // MyWaitWindow
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.BackColor = System.Drawing.Color.LightBlue;
            this.BackgroundImage = null;
            this.ControlBox = false;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.lblMsg);
            this.Font = null;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MyWaitWindow";
            this.ShowIcon = false;
            this.UseWaitCursor = true;
            this.Controls.SetChildIndex(this.lblMsg, 0);
            this.Controls.SetChildIndex(this.progressBar1, 0);
            this.Controls.SetChildIndex(this.pictureBox1, 0);
            this.Controls.SetChildIndex(this._txtDefaultFocus, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public MyWaitWindow()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 显示等待窗
        /// </summary>
        /// <param name="msg">消息描述</param>
        /// <param name="showProgress">是否显示进度条</param>
        public void ShowMsg(string msg, bool showProgress)
        {
            this.lblMsg.Text = msg;
            this.progressBar1.Visible = showProgress;
            this.Visible = true;
            this.BringToFront();
            this.Refresh();
        }
    }
}
