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
    public class DataGridViewTextBoxCellBase : DataGridViewTextBoxCell
    {
        public DataGridViewTextBoxCellBase()
        {
            
        }

        private object mOldInputValue;


        protected override void OnEnter(int rowIndex, bool throughMouseClick)
        {
            base.OnEnter(rowIndex, throughMouseClick);

            if (this.RowIndex >= 0)
            {
                this.mOldInputValue = this.Value;
            }
        }


        /// <summary>
        /// 输入时是否改变了值
        /// </summary>
        /// <returns></returns>
        public bool IsInputChanged()
        {
            if (this.mOldInputValue != this.Value)
                return true;
            else
                return false;
        }
         

        /// <summary>
        /// Nav时的事件
        /// </summary>
        public event EventHandler NavClick;

        /// <summary>
        /// 请求新数据触发的事件
        /// </summary>
        public event EventHandler RequestNewData;


        protected override void OnKeyDown(KeyEventArgs e, int rowIndex)
        {
            base.OnKeyDown(e, rowIndex);

            if (this.RowIndex >= 0)
            {
                if (e.KeyCode == Keys.F3)
                {
                    if (!this.ReadOnly && this.NavClick != null)
                    {
                        this.NavClick(null, null);
                    }
                }
            }
        }


        protected override void OnDoubleClick(DataGridViewCellEventArgs e)
        {
            base.OnDoubleClick(e);

            if (this.RowIndex >= 0)
            {
                if (!this.ReadOnly && this.NavClick != null)
                {
                    this.NavClick(null, null);
                }
            }
        }


        protected override void OnLeave(int rowIndex, bool throughMouseClick)
        {
            base.OnLeave(rowIndex, throughMouseClick);

            if (this.RowIndex >= 0)
            {
                if (this.IsInputChanged())
                {
                    if (this.RequestNewData != null)
                        this.RequestNewData(null, null);
                }
            }
        }
    }
}