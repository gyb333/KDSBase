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
    /// 消息提示对话框
    /// huhm2008
    /// </summary>
    public class MyMessageBox: BaseForm
    {
        private int mFormHeight;
        private LableBase lblMsg;
        private ButtonBase btnOK;
        private ButtonBase btnMore;
        private TextBoxBase txtMsg;
        private GroupBox groupBox1;
        private ButtonBase btnCancel;
        private PictureBox pictureBox1;
    
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MyMessageBox));
            this.lblMsg = new KDS.UI.Component.LableBase();
            this.btnOK = new KDS.UI.Component.ButtonBase();
            this.btnMore = new KDS.UI.Component.ButtonBase();
            this.txtMsg = new KDS.UI.Component.TextBoxBase();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnCancel = new KDS.UI.Component.ButtonBase();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMsg
            // 
            this.lblMsg.BackColor = System.Drawing.Color.Transparent;
            this.lblMsg.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            resources.ApplyResources(this.lblMsg, "lblMsg");
            this.lblMsg.Name = "lblMsg";
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnMore
            // 
            resources.ApplyResources(this.btnMore, "btnMore");
            this.btnMore.Name = "btnMore";
            this.btnMore.UseVisualStyleBackColor = true;
            this.btnMore.Click += new System.EventHandler(this.btnMore_Click);
            // 
            // txtMsg
            // 
            this.txtMsg.BackColor = System.Drawing.Color.White;
            this.txtMsg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.txtMsg, "txtMsg");
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.ReadOnly = true;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Gainsboro;
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::KDS.UI.Component.Properties.Resources.info;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // MyMessageBox
            // 
            this.AcceptButton = this.btnOK;
            this.CancelButton = this.btnCancel;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtMsg);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.btnMore);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MyMessageBox";
            this.ShowIcon = false;
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.pictureBox1, 0);
            this.Controls.SetChildIndex(this.btnMore, 0);
            this.Controls.SetChildIndex(this.lblMsg, 0);
            this.Controls.SetChildIndex(this.txtMsg, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this._txtDefaultFocus, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public MyMessageBox()
        {
            InitializeComponent();
            mFormHeight = this.Height;
            this.txtMsg.BackColor = UIStyle.FormBackColor;
        }

        /// <summary>
        /// 显示窗体
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="errMsg">错误消息</param>
        /// <param name="moreMsg">详细的消息</param>
        /// <param name="cancelButtonVisible">是否显示取消按钮</param>
        /// <returns>DialogResult</returns>
        public static DialogResult Show(IWin32Window owner, string errMsg, string moreMsg, bool cancelButtonVisible)
        {
            DialogResult retVal;

            MyMessageBox myMessageBox = new MyMessageBox();
            retVal = myMessageBox.ShowMsg(owner, errMsg, moreMsg, cancelButtonVisible);
            myMessageBox.Close();

            return retVal;
        }

        /// <summary>
        /// 显示窗体
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="errMsg">错误消息</param>
        /// <param name="moreMsg">详细的消息</param>
        /// <returns>DialogResult</returns>
        public static DialogResult Show(IWin32Window owner, string errMsg, string moreMsg)
        {
            return Show(null, errMsg, moreMsg, false);
        }


        /// <summary>
        /// 显示窗体
        /// </summary>
        /// <param name="errMsg">错误消息</param>
        /// <param name="moreMsg">详细的消息</param>
        /// <returns>DialogResult</returns>
        public static DialogResult Show(string errMsg, string moreMsg)
        {
            return Show(null, errMsg, moreMsg,false);
        }

        /// <summary>
        /// 显示窗体
        /// </summary>
        /// <param name="errMsg">错误消息</param>
        /// <param name="moreMsg">详细的消息</param>
        /// <param name="cancelButtonVisible">是否显示取消按钮</param>
        /// <returns>DialogResult</returns>
        public static DialogResult Show(string errMsg, string moreMsg, bool cancelButtonVisible)
        {
            return Show(null, errMsg, moreMsg, cancelButtonVisible);
        }

        /// <summary>
        /// 显示窗体
        /// </summary>
        /// <param name="errMsg">错误消息</param>
        /// <returns>DialogResult</returns>
        public static DialogResult Show(string errMsg)
        {
            string errMsg1=errMsg;
            string moreMsg="";

            if (errMsg.Contains(Environment.NewLine))
            {
                int pos=errMsg.IndexOf(Environment.NewLine);
                errMsg1 = errMsg.Substring(0,pos);
                moreMsg = errMsg;
            }
            

            return Show(null,errMsg1, moreMsg, false);
        }


        /// <summary>
        /// 显示窗体
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="errMsg">错误消息</param>
        /// <param name="cancelButtonVisible">详细的消息</param>
        /// <returns>DialogResult</returns>
        private DialogResult ShowMsg(IWin32Window owner,string errMsg, string moreMsg, bool cancelButtonVisible)
        {
            this.lblMsg.Text = errMsg;
            if (moreMsg != "")
            {
                this.txtMsg.Text = moreMsg;
                this.ExpandForm(true);
            }
            else
            {
                this.ExpandForm(false);
            }

            if (cancelButtonVisible)
            {
                this.btnCancel.Visible = cancelButtonVisible;
                this.CancelButton = this.btnCancel;
            }
            else
            {
                this.CancelButton = this.btnOK;
            }

            this.BringToFront();
            if (owner == null)
            {
                return this.ShowDialog();
            }
            else
            {
                return this.ShowDialog(owner);
            }
        }

        private void btnMore_Click(object sender, EventArgs e)
        {
            this.ExpandForm(this.Height != this.mFormHeight ? true : false);
        }

        private void ExpandForm(bool expand)
        {
            if (expand)
            {
                this.Height = this.mFormHeight;
                this.btnMore.Text = "详细(&D)<<";
            }
            else
            {
                this.Height = this.groupBox1.Top + 20 ;
                this.btnMore.Text = "详细(&D)>>";
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
