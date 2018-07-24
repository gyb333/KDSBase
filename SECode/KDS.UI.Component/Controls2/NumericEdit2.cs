using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using C1.Win.C1Input;
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
    [ToolboxBitmap(typeof(TextBox))]
    public class NumericEdit2: C1NumericEdit
    {
        private string mOldInputValue;

        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // NumericEdit2
            // 
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CustomFormat = "N2";
            this.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.Size = new System.Drawing.Size(200, 19);
            this.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
            this.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.ValidationError += new C1.Win.C1Input.ValidationErrorEventHandler(this.NumericEdit2_ValidationError);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
        }

        public NumericEdit2()
        {
            
            this.InitializeComponent();
        }

        private void NumericEdit2_ValidationError(object sender, ValidationErrorEventArgs e)
        {
            e.ErrorInfo.ErrorMessage = "请输入一个合法的数字或0。";
            e.ErrorInfo.ErrorMessageCaption = "检查";
        }

        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);

            this.mOldInputValue = this.Text;
        }

        /// <summary>
        /// 输入时是否改变了值
        /// </summary>
        /// <returns></returns>
        public bool IsInputChanged()
        {
            if (this.mOldInputValue != this.Text)
                return true;
            else
                return false;
        }
    }
}
