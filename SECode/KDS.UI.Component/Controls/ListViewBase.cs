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
    [ToolboxBitmap(typeof(ListView))]
    public class ListViewBase:ListView
    {

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ListViewBase
            // 
            this.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ResumeLayout(false);

        }

        public ListViewBase()
        {
            this.InitializeComponent();

            if (!this.Enabled)
                this.BackColor = UIStyle.EditDisableBackColor;
        }
    }
}
