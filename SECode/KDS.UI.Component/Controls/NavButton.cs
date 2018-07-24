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
    public class NavButton:ButtonBase
    {
        private ToolTip toolTip1;
        private IContainer components;
    
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // NavButton
            // 
            this.FlatAppearance.BorderSize = 0;
            this.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Image = global::KDS.UI.Component.Properties.Resources.Nav1;
            this.Size = new System.Drawing.Size(23, 23);
            this.toolTip1.SetToolTip(this, "导航按钮，点击打印对话框");
            this.ResumeLayout(false);

        }

        public NavButton()
        {
            this.InitializeComponent();
            this.Text = "";
        }
    }
}
