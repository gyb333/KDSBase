using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    [ToolboxBitmap(typeof(ListBox))]
    public class ListBoxBase:ListBox
    {

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ListBoxBase
            // 
            this.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ItemHeight = 12;
            this.ResumeLayout(false);

        }

        public ListBoxBase()
        {
            this.InitializeComponent();

            if (!this.Enabled)
                this.BackColor = UIStyle.EditDisableBackColor;
        }

        //重写系统消息
        //警告：严禁随意修改，不小心会耗费系统大量资源
        //作者：huhaiming,2008
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            base.WndProc(ref m);

            const int WM_PAINT = 0xF;

            if (m.Msg == WM_PAINT && this.BorderStyle == System.Windows.Forms.BorderStyle.None)
            {
                Graphics g = Graphics.FromHwnd(this.Handle);

                g.DrawRectangle(Pens.LightSteelBlue, this.ClientRectangle.Left, this.ClientRectangle.Top, this.ClientRectangle.Width - 1, this.ClientRectangle.Height - 1);

                g.Dispose();
            }
        } 
    }
}
