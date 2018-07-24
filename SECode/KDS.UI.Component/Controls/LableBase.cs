﻿using System;
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
    [ToolboxBitmap(typeof(Label))]
    public class LableBase : Label
    {
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // LableBase
            // 
            this.BackColor = System.Drawing.Color.Transparent;
            this.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ResumeLayout(false);

        }

        public LableBase()
        {
            this.InitializeComponent();
        }
    }
}
