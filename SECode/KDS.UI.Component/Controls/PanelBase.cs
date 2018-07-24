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
    [ToolboxBitmap(typeof(Panel))]
    public class PanelBase : Panel
    {
        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.BackColor = UIStyle.FormBackColor;

            this.ResumeLayout(false);

        }

        public PanelBase()
        {
            this.InitializeComponent();
        }
    }
}
