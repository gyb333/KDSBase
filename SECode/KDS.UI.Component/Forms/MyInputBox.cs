using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;

/* ==========================================================================
 *  输入窗体
 *  
 *  作者：胡海明 (huhaiming@gmail.com)
 *  日期：2008/08
 * 
 *  功能：

 *==========================================================================*/
namespace KDS.UI.Component.Forms
{
    /// <summary>
    /// 文本输入框
    /// huhm2008
    /// </summary>
    public class MyInputBox: BaseForm
    {
        private LableBase lblTitle;
        private ButtonBase btnOK;
        private TextBoxBase textBoxBase1;
        private ButtonBase btnCancel;

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MyInputBox));
            this.lblTitle = new KDS.UI.Component.LableBase();
            this.btnOK = new KDS.UI.Component.ButtonBase();
            this.btnCancel = new KDS.UI.Component.ButtonBase();
            this.textBoxBase1 = new KDS.UI.Component.TextBoxBase();
            this.SuspendLayout();
            // 
            // _txtDefaultFocus
            // 
            this._txtDefaultFocus.AccessibleDescription = null;
            this._txtDefaultFocus.AccessibleName = null;
            resources.ApplyResources(this._txtDefaultFocus, "_txtDefaultFocus");
            this._txtDefaultFocus.BackgroundImage = null;
            this._txtDefaultFocus.Font = null;
            // 
            // lblTitle
            // 
            this.lblTitle.AccessibleDescription = null;
            this.lblTitle.AccessibleName = null;
            resources.ApplyResources(this.lblTitle, "lblTitle");
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblTitle.Name = "lblTitle";
            // 
            // btnOK
            // 
            this.btnOK.AccessibleDescription = null;
            this.btnOK.AccessibleName = null;
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.BackgroundImage = null;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOK.Font = null;
            this.btnOK.Name = "btnOK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleDescription = null;
            this.btnCancel.AccessibleName = null;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.BackgroundImage = null;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = null;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // textBoxBase1
            // 
            this.textBoxBase1.AccessibleDescription = null;
            this.textBoxBase1.AccessibleName = null;
            resources.ApplyResources(this.textBoxBase1, "textBoxBase1");
            this.textBoxBase1.BackgroundImage = null;
            this.textBoxBase1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxBase1.Font = null;
            this.textBoxBase1.Name = "textBoxBase1";
            // 
            // MyInputBox
            // 
            this.AcceptButton = this.btnOK;
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.BackgroundImage = null;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.textBoxBase1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblTitle);
            this.Font = null;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = null;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MyInputBox";
            this.ShowIcon = false;
            this.Controls.SetChildIndex(this.lblTitle, 0);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.textBoxBase1, 0);
            this.Controls.SetChildIndex(this._txtDefaultFocus, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public MyInputBox()
        {
            InitializeComponent();

            this.DialogResult = DialogResult.None;
        }


        /// <summary>
        /// 显示输入文本对话框
        /// </summary>
        /// <param name="titleMsg">标题</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="maxLength">文本最大长度</param>
        /// <param name="isPasswordMask">输入文本框是否显示为密码</param>
        /// <returns></returns>
        public static string Show(string titleMsg, string defaultValue, int maxLength,bool isPasswordMask)
        {
            DialogResult retVal;

            MyInputBox myInputBoxDialog = new MyInputBox();
            retVal = myInputBoxDialog.ShowMsg(titleMsg, defaultValue, maxLength, isPasswordMask);
            myInputBoxDialog.Close();

            if (retVal == DialogResult.OK)
            {
                return myInputBoxDialog.textBoxBase1.Text.Trim();
            }
            else
            {
                return string.Empty;
            }
        }


        /// <summary>
        /// 显示输入文本对话框
        /// </summary>
        /// <param name="titleMsg">标题</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static string Show(string titleMsg, string defaultValue)
        {
            return MyInputBox.Show(titleMsg, defaultValue, 200,false);
        }


        /// <summary>
        /// 显示输入文本对话框
        /// </summary>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static string Show(string defaultValue)
        {
            return MyInputBox.Show("请输入文本", defaultValue, 200,false);
        }

        /// <summary>
        /// 显示输入文本对话框
        /// </summary>
        /// <returns></returns>
        public static new string Show()
        {
            return MyInputBox.Show("", "", 200,false);
        }



        private DialogResult ShowMsg(string titleMsg, string defaultValue, int maxLength, bool isPasswordMask)
        {
            this.lblTitle.Text = titleMsg;
            this.textBoxBase1.Text = defaultValue;
            this.textBoxBase1.MaxLength = maxLength;

            if (isPasswordMask)
                this.textBoxBase1.PasswordChar = '*';

            return this.ShowDialog();
        }



        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.textBoxBase1.Text.Trim() != string.Empty)
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                this.DialogResult = DialogResult.None;
                MessageBox.Show("请输入值。", "文本", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
