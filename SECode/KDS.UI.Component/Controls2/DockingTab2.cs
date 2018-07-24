using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using C1.Win.C1Command;
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
    [ToolboxBitmap(typeof(TabControl))]
    public class DockingTab2: C1DockingTab 
    {
        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // DockingTab2
            // 
            this.TabsSpacing = 5;
            this.TabStyle = C1.Win.C1Command.TabStyleEnum.Office2007;
            this.VisualStyle = C1.Win.C1Command.VisualStyle.Office2007Blue;
            this.VisualStyleBase = C1.Win.C1Command.VisualStyle.Office2007Blue;
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        public DockingTab2()
        {
            this.InitializeComponent();
        }


    }
}
