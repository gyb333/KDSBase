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
    [ToolboxBitmap(typeof(GroupBox))]
    public class SeperateLine:GroupBox
    {

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // SeperateLine
            // 
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Size = new System.Drawing.Size(500, 2);
            this.SizeChanged += new System.EventHandler(this.SeperateLine_SizeChanged);
            this.ResumeLayout(false);

        }

        public SeperateLine()
        {
            this.InitializeComponent();

            this.Text = "";
        }

        private void SeperateLine_SizeChanged(object sender, EventArgs e)
        {
            this.Size = new Size(this.Size.Width, 2);
        }



    }
}
