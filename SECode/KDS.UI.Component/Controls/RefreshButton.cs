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
    public class RefreshButton:ButtonBase
    {
        private ToolTip toolTip1;
        private IContainer components;
    
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // RefreshButton
            // 
            this.FlatAppearance.BorderSize = 0;
            this.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Image = global::KDS.UI.Component.Properties.Resources.RefreshDocViewHS;
            this.Size = new System.Drawing.Size(23, 23);
            this.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this, "点击刷新数据");
            this.ResumeLayout(false);

        }

        public RefreshButton()
        {
            this.InitializeComponent();
            this.Text = "";
        }
    }
}
