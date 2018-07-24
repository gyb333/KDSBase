using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
/* ==========================================================================
 *  基础控件
 *  
 *  作者：胡海明 (huhaiming@gmail.com)
 *  日期：2008/08
 *==========================================================================*/
namespace KDS.UI.Component
{
    [ToolboxBitmap(typeof(TextBox))]
    public class TextBoxBase:TextBox
    {
        private string mOldInputValue;


        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ResumeLayout(false);
        }

        public TextBoxBase()
        { 
            this.InitializeComponent();

            if (this.ReadOnly || !this.Enabled)
                this.BackColor = UIStyle.EditDisableBackColor;
        }

        protected override void OnEnter(EventArgs e)
        {


            base.OnEnter(e);

            this.mOldInputValue = this.Text;
        }


        //protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        //{
        //    //过滤换行符
        //    if (keyData == (Keys)Shortcut.CtrlV && !this.Multiline)  // 快捷键 Ctrl+V 粘贴操作
        //    {
        //        string text = Clipboard.GetText();
        //        if (text.Length>0)
        //        {
        //            text=text.Replace("\n", "");
        //            if (text.Length > 0)
        //            {
        //                Clipboard.SetText(text);
        //            }
        //        }
        //    }

        //    return base.ProcessCmdKey(ref msg, keyData);
        //}


        /// <summary>
        /// 输入时是否改变了值
        /// </summary>
        /// <returns></returns>
        public bool IsInputChanged()
        {
            if (this.mOldInputValue != this.Text)
                return true;
            else
                return false;
        }

 
        //重写系统消息
        //警告：严禁随意修改，不小心会耗费系统大量资源
        //作者：huhaiming,2008
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            base.WndProc(ref m);

            const int WM_PAINT = 0xF;

            if (m.Msg == WM_PAINT && this.BorderStyle == System.Windows.Forms.BorderStyle.FixedSingle)
            {
                Graphics g = Graphics.FromHwnd(this.Handle);

                g.DrawRectangle(Pens.LightSteelBlue, this.ClientRectangle.Left, this.ClientRectangle.Top, this.ClientRectangle.Width - 1, this.ClientRectangle.Height - 1);

                g.Dispose();
            }
        }
    }
}
