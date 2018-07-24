using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
/* ==========================================================================
 *  基础控件
 *  
 *  作者：胡海明 (huhaiming@gmail.com)
 *  日期：2008/08
 *==========================================================================*/
namespace KDS.UI.Component
{
    /// <summary>
    /// ButtonBase按钮基类
    /// huhm2008
    /// </summary>
    [ToolboxBitmap(typeof(Button))]
    public class ButtonBase:Button
    {
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ButtonBase
            // 
            this.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Click += new System.EventHandler(this.ButtonBase_Click);
            this.ResumeLayout(false);

        }

        public ButtonBase()
        {
            this.InitializeComponent();
        }

        private void ButtonBase_Click(object sender, EventArgs e)
        {
            if (this.CanFocus)
                this.Focus();
        }
    }
}
