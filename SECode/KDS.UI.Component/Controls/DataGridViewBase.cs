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
    [ToolboxBitmap(typeof(DataGridView))]
    public class DataGridViewBase : DataGridView
    {
        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // DataGridViewBase
            // 
            this.AllowUserToResizeRows = false;
            this.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.RowTemplate.Height = 23;
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }



        private void InitControl()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();

            this.BackgroundColor = KDS.UI.Component.UIStyle.GridBackColor;
            this.RowHeadersVisible = false;

            //this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            //this.UpdateStyles();
            this.DoubleBuffered = true;

            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
        }

        public DataGridViewBase()
        {
            this.InitializeComponent();
            this.InitControl();
        }
    }
}
