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
    /// <summary>
    /// 日期时间类型
    /// </summary>
    public enum DateTimeType
    {
        /// <summary>
        /// 日期
        /// </summary>
        Date,

        /// <summary>
        /// 日期+时间
        /// </summary>
        DateTime
    }

    [ToolboxBitmap(typeof(DateTimePicker))]
    public class DateTimePickerBase:DateTimePicker
    {
        private DateTimeType mMyDateTimeType = DateTimeType.Date;
        /// <summary>
        /// Name属性
        /// </summary>
        [Category("DataEditProperty")]
        [Description("日期时间类型")]
        public DateTimeType MyDateTimeType
        {
            get { return this.mMyDateTimeType; }
            set
            {
                this.mMyDateTimeType = value;

                if (this.mMyDateTimeType==DateTimeType.Date)
                {
                    this.CustomFormat = "yyyy/MM/dd";
                }
                else
                {
                    this.CustomFormat = "yyyy/MM/dd HH:mm:ss";
                }
            }
        }


        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // DateTimePickerBase
            // 
            this.CustomFormat = "yyyy/MM/dd";
            this.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.ResumeLayout(false);

        }

        public DateTimePickerBase()
        {
            this.InitializeComponent();

            if (!this.Enabled)
                this.BackColor = UIStyle.EditDisableBackColor;
        }

    }
}
