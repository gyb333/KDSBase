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
    public class DataGridViewEdit : DataGridViewBase
    {
        private object mCellOldInputValue;

        /// <summary>
        /// Nav时的事件
        /// </summary>
        [Category("DataEditEvents")]
        [Description("Nav按钮时触发的事件")]
        public event DataGridViewCellEventHandler NavClick;


        /// <summary>
        /// 请求新数据触发的事件
        /// </summary>
        [Category("DataEditEvents")]
        [Description("请求新数据时触发的事件")]
        public event DataGridViewCellEventHandler RequestNewData;


        /// <summary>
        /// 请求新数据触发的事件
        /// </summary>
        [Category("DataEditEvents")]
        [Description("请求新增行触发的事件")]
        public event EventHandler RequestNewRow;


        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // DataGridViewEdit
            // 
            this.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.RowTemplate.Height = 23;
            this.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewEdit_CellDoubleClick);
            this.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewEdit_CellValidated);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DataGridViewEdit_KeyDown);
            this.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewEdit_CellEnter);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }


        public DataGridViewEdit()
        {
            this.InitializeComponent();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Enter:
                    if (this.CurrentCell != null)
                    {
                        if (this.CurrentCell.ColumnIndex == this.ColumnCount - 1 && this.RequestNewRow != null)
                        {
                            this.RequestNewRow(this, null);
                            return true;
                        }
                    }

                    System.Windows.Forms.SendKeys.Send("{tab}");
                    return true;


                case Keys.F3:
                    this.DataGridViewEdit_KeyDown(this, new KeyEventArgs(keyData));
                    return true;

                
                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            } 
        }


        /// <summary>
        /// 输入时是否改变了值
        /// </summary>
        /// <returns></returns>
        public bool IsInputChanged()
        {
            string orgValue= (this.mCellOldInputValue==null? "":this.mCellOldInputValue.ToString());
            string curValue= (this.CurrentCell.Value==null? "":this.CurrentCell.Value.ToString());

            if (orgValue != curValue)
                return true;
            else
                return false;
        }

        private void DataGridViewEdit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3 && !this.ReadOnly)
            {
                DataGridViewCell cell = this.CurrentCell;
                if (cell!=null && !cell.ReadOnly && this.NavClick != null)
                {
                    DataGridViewCellEventArgs ex = new DataGridViewCellEventArgs(this.CurrentCell.ColumnIndex, this.CurrentCell.RowIndex);
                    this.NavClick(sender, ex);
                }
            }
        }


        private void DataGridViewEdit_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!this.ReadOnly)
            {
                DataGridViewCell cell = this.CurrentCell;

                if (cell != null && !cell.ReadOnly && this.NavClick != null)
                {
                    this.NavClick(sender, e);
                }
            }
        }


        private void DataGridViewEdit_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (!this.ReadOnly)
            {
                this.mCellOldInputValue = this.CurrentCell.Value;
            }
        }


        private void DataGridViewEdit_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (!this.ReadOnly)
            {
                DataGridViewCell cell = this.CurrentCell;

                if (cell != null && !cell.ReadOnly)
                {
                    if (this.IsInputChanged())
                    {
                        if (this.RequestNewData != null)
                            this.RequestNewData(sender, e);
                    }
                }
            }
        }


    }
}
