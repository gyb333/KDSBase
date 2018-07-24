using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using KDS.UI.Component;
/* ==========================================================================
 *  基础控件_DataPageControl
 *  
 *  作者：胡海明 (huhaiming@gmail.com)
 *  日期：2008/08
 *==========================================================================*/
namespace KDS.UI.Component.UserControls
{
    /// <summary>
    /// 分页控件
    /// </summary>
    [ToolboxBitmap(typeof(TextBox))]
    [DefaultEvent("RequestNewPage")]
    [DefaultProperty("Text")]
    public partial class DataPageControl : UserControl
    {
        private int mPageNo = 1;
        /// <summary>
        /// 页码
        /// </summary>
        public int PageNo
        {
            get
            {
                return this.mPageNo;
            }
            set
            {
                this.mPageNo = value;
                this.RefreshStatus();
            }
        }

        private int mTotalPage = 0;
        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPage
        {
            get
            {
                return this.mTotalPage;
            }
            set
            {
                this.mTotalPage = value;
                this.RefreshStatus();
            }
        }

        private string mText;
        /// <summary>
        /// Text属性
        /// </summary>
        [Category("DataEditProperty")]
        [Bindable(true)]
        [Description("Text字段")]
        public new string Text
        {
            get { return this.mText; }
            set
            {
                this.mText = value;
                this.txtPageNo.Text= value;
            }
        }


        ///// <summary>
        ///// Enabled
        ///// </summary>
        //[Category("DataEditProperty")]
        //public new bool Enabled
        //{
        //    get { return this.txtPageNo.Enabled; }
        //    set
        //    {
        //        base.Enabled = value;
        //        this.btnFirst.Enabled = value;
        //        this.btnPrior.Enabled = value;
        //        this.btnNext.Enabled = value;
        //        this.btnLast.Enabled = value;
        //        this.txtPageNo.Enabled = value;
        //    }
        //}

        /// <summary>
        /// DataPageControl
        /// </summary>
        public DataPageControl()
        {
            this.InitializeComponent();
        }


        /// <summary>
        /// 请求新数据页时触发的事件
        /// </summary>
        [Category("DataEditEvents")]
        [Description("请求新数据页时触发的事件")]
        public event EventHandler RequestNewPage;


        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (this.RequestNewPage != null && this.mPageNo>1  && this.mTotalPage>0 )
            {
                this.PageNo = 1;
                this.RequestNewPage(sender, e);
            }
        }

        private void btnPrior_Click(object sender, EventArgs e)
        {
            if (this.RequestNewPage != null && this.mPageNo >1 && this.mTotalPage > 0)
            {
                this.PageNo -= 1;
                this.RequestNewPage(sender, e);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (this.RequestNewPage != null && this.mPageNo < mTotalPage && this.mTotalPage > 0)
            {
                this.PageNo += 1;
                this.RequestNewPage(sender, e);
            }

        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (this.RequestNewPage != null && this.mPageNo < mTotalPage && this.mTotalPage > 0)
            {
                this.PageNo = this.mTotalPage;
                this.RequestNewPage(sender, e);
            }
        }

        private void txtPageNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.RequestNewPage != null && this.mTotalPage > 0)
                {
                    int newPage = this.PageNo;
                    try
                    {
                        newPage = Convert.ToInt32(this.txtPageNo.Text);
                    }
                    catch
                    {
                    }

                    if (newPage != this.PageNo)
                    {
                        this.PageNo = Math.Max(Math.Min(newPage,this.mTotalPage),1);
                        this.RequestNewPage(sender, e);
                    }
                }
            }
        }

        private void txtPageNo_Leave(object sender, EventArgs e)
        {
            //this.txtPageNo.Text = this.mText;
            this.RefreshStatus();
        }


        private void RefreshStatus()
        {
            this.Text = this.mPageNo.ToString() + "/" + this.mTotalPage.ToString();

            this.txtPageNo.Enabled = (this.mTotalPage > 0);

            this.btnFirst.Enabled = ( (this.mPageNo > 1) && (this.mTotalPage > 0) );
            this.btnPrior.Enabled = this.btnFirst.Enabled;

            this.btnNext.Enabled = ( (this.mPageNo < this.mTotalPage) && (this.mTotalPage>0) );
            this.btnLast.Enabled = this.btnNext.Enabled;
        }

        private void txtPageNo_Enter(object sender, EventArgs e)
        {
            this.txtPageNo.SelectAll();
        }
    }
}