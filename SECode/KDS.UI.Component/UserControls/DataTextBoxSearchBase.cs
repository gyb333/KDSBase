using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;

using KDS.UI.Component.Forms;
/* ==========================================================================
 *  SearchControlBase基类
 *  
 *  作者：胡海明 (huhaiming@gmail.com)
 *  日期：2008/08
 *
 *==========================================================================*/
namespace KDS.UI.Component.UserControls
{
    /// <summary>
    /// SearchControlBase管理
    /// huhm2008
    /// </summary>
    public abstract class DataTextBoxSearchBase
    {
        private Form mOwner;
        private string mBrowseFormCaption;

        /// <summary>
        /// 浏览搜索的数据源表
        /// </summary>
        protected DataTable mBrowseTable;

        /// <summary>
        /// ID列名
        /// </summary>
        protected string mCodeColumnName = "";



        public string mText = "";
        public int mIDText = 0;
        public string mNameText = "";
        public string mCodeText = "";


        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="browseFormCaption"></param>
        /// <param name="searchText"></param>
        public DataTextBoxSearchBase(Form owner, string browseFormCaption, string searchText)
        {
            this.mOwner = owner;
            this.mBrowseFormCaption = browseFormCaption;
            this.mText = searchText;
        }


        /// <summary>
        /// NavClick前必须处理的事件：1.获取数据源this.mBrowseTable及2.指定代码字段列表this.mCodeColumnName
        /// </summary>
        protected abstract void BeforeNavClick();
        //{
        //    //Sample
        //    //DataTable dt = new T_Currency(mClientApp).GetT_CurrySearch(this.mDataTextBox.Text);

        //    //dt.Columns[1].ColumnName = "货币名称";
        //    //dt.Columns[2].ColumnName = "货币代码";
        //    //dt.Columns[3].ColumnName = "英文名";
        //    //dt.Columns[4].ColumnName = "可用";
        //    //dt.Columns[5].ColumnName = "备注";

        //    //this.mCodeColumnName="货币代码";
        //}

        protected virtual void AfterNavClick()
        {
        }

        public void NavClick()
        {
            try
            {
                this.BeforeNavClick();

                if (this.mBrowseTable == null || this.mCodeColumnName == "")
                {
                    throw new Exception("数据源及代码字段名不能为空。");
                }

                MyBrowseForm browseForm = new MyBrowseForm();
                if (browseForm.ShowData(this.mOwner, this.mBrowseTable, this.mBrowseFormCaption, false, true, false, 750, 460, "") == DialogResult.OK)
                {
                    if (browseForm.SelectedRows.Count > 0)
                    {
                        this.mText = browseForm.SelectedRows[0].Cells[this.mCodeColumnName].Value.ToString();
                    }
                }

                if (browseForm != null)
                    browseForm.Dispose();

                this.AfterNavClick();

            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// 1.根据this.mText去业务逻辑对象中精确查找符合条件的记录值，查找不到清空this.mText，2.并设置给IDText,NameText,CodeText属性
        /// </summary>
        protected abstract void BeforeRequestNewData();
        //{
        //Sample
        //T_Currency currency = new T_Currency(this.mClientApp, 0);
        //Datarow dr = currency.GetT_CurryBrowseByCode(this.mText);

        //if (dr != null)
        //{
        //    this.mIDText = (int)dr[this.mIDColumnName];
        //    this.mNameText = dr[this.mNameColumnName].ToString();
        //    this.mCodeText = dr[this.mCodeColumnName].ToString();
        //}
        //else
        //{
        //    this.mText="";
        //}
        //}

        protected virtual void AfterRequestNewData()
        {

        }


        public void RequestNewData()
        {
            try
            {
                if (this.mText != "")
                {
                    this.BeforeRequestNewData();
                }

                if (this.mText == "")
                {
                    this.mIDText = 0;
                    this.mNameText = "";
                    this.mCodeText = "";
                }

                this.AfterRequestNewData();
            }
            catch
            {
                throw;
            }
        }

    }
}
