using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using KDS.Model;
using KDS.Client.App;

using KDS.UI.Component.Forms;
/* ==========================================================================
 *  条件窗体基类
 *  
 *  作者：胡海明 (huhaiming@gmail.com)
 *  日期：2008/08
 *
 *==========================================================================*/
namespace KDS.Client.BaseForms
{
    /// <summary>
    /// 报表窗体基类
    /// 提供条件窗体的公共行为
    /// Copyrights by huhaiming@gmail.com 2001~2008, 
    /// </summary>
    public class FilterForm : BaseForm
    {
        /// <summary>
        /// 用户选择的条件（参数名/值）
        /// </summary>
        public HDbParameter DbParaData;


        /// <summary>
        /// 窗体子功能ID（可选项，初始-1，需要时请设置>0）（适用于一个窗体执行多个功能时的功能区分，如销量报表包括：1111-品牌分析；1112-客户渠道分析等）
        /// </summary>
        public int FormSubFuncID = -1;


        /// <summary>
        /// 报表过滤条件描述
        /// </summary>
        public string ReportFilterDesc = "";

        /// <summary>
        /// 用户选择汇总的字段列表（中文描述）
        /// </summary>
        public string[] ReportGroupFieldCaptionList;

        /// <summary>
        /// 用户选择汇总的字段列表（中文描述）
        /// </summary>
        public string[] ReportSumFieldCaptionList;


        protected ClientApp mClientApp;
        protected int mBillTypeID;

        private ToolStrip toolStrip1;
        private ToolStripButton tsbFilter;
        private ToolStripButton tsbCancel;
        private KDS.UI.Component.ButtonBase btnClose;
        private ToolStripButton tsbLoadFilter;
        private ToolStripButton tsbSaveFilter;

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FilterForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbFilter = new System.Windows.Forms.ToolStripButton();
            this.tsbLoadFilter = new System.Windows.Forms.ToolStripButton();
            this.tsbSaveFilter = new System.Windows.Forms.ToolStripButton();
            this.tsbCancel = new System.Windows.Forms.ToolStripButton();
            this.btnClose = new KDS.UI.Component.ButtonBase();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbFilter,
            this.tsbLoadFilter,
            this.tsbSaveFilter,
            this.tsbCancel});
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Name = "toolStrip1";
            // 
            // tsbFilter
            // 
            resources.ApplyResources(this.tsbFilter, "tsbFilter");
            this.tsbFilter.Name = "tsbFilter";
            this.tsbFilter.Click += new System.EventHandler(this.tsbFilter_Click);
            // 
            // tsbLoadFilter
            // 
            resources.ApplyResources(this.tsbLoadFilter, "tsbLoadFilter");
            this.tsbLoadFilter.Name = "tsbLoadFilter";
            this.tsbLoadFilter.Click += new System.EventHandler(this.tsbLoadFilter_Click);
            // 
            // tsbSaveFilter
            // 
            resources.ApplyResources(this.tsbSaveFilter, "tsbSaveFilter");
            this.tsbSaveFilter.Name = "tsbSaveFilter";
            this.tsbSaveFilter.Click += new System.EventHandler(this.tsbSaveFilter_Click);
            // 
            // tsbCancel
            // 
            resources.ApplyResources(this.tsbCancel, "tsbCancel");
            this.tsbCancel.Name = "tsbCancel";
            this.tsbCancel.Click += new System.EventHandler(this.tsbCancel_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // FilterForm
            // 
            this.CancelButton = this.btnClose;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.toolStrip1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FilterForm";
            this.ShowIcon = false;
            this.Controls.SetChildIndex(this.toolStrip1, 0);
            this.Controls.SetChildIndex(this.btnClose, 0);
            this.Controls.SetChildIndex(this._txtDefaultFocus, 0);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public FilterForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public FilterForm(ClientApp clientApp)
        {
            this.mClientApp = clientApp;

            this.InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.tsbCancel_Click(sender, e);
        }

        private void tsbCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }


        private void tsbSaveFilter_Click(object sender, EventArgs e)
        {
            try
            {
                this.SaveFilter();
            }
            catch (Exception ex)
            {
                MyMessageBox.Show(ex.Message);
            }
        }

        private void tsbFilter_Click(object sender, EventArgs e)
        {
            try
            {
                this.GetFilter();
            }
            catch (Exception ex)
            {
                MyMessageBox.Show(ex.Message);
            }
        }




        /// <summary>
        /// 获取用户选择的条件
        /// </summary>
        protected virtual void GetFilter()
        {
            this.EndEdit();
        }


        /// <summary>
        /// 载入保存的条件
        /// </summary>
        protected virtual void LoadFilter()
        {
            this.EndEdit();
        }

        /// <summary>
        /// 保存条件
        /// </summary>
        protected virtual void SaveFilter()
        {
            this.EndEdit();
        }

        private void tsbLoadFilter_Click(object sender, EventArgs e)
        {
            try
            {
                this.LoadFilter();
            }
            catch (Exception ex)
            {
                MyMessageBox.Show(ex.Message);
            }
        }


        /// <summary>
        /// 结束编辑写入缓冲
        /// </summary>
        protected virtual void EndEdit()
        {
            try
            {
                this.SetDefaultFocus();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 取消编辑回退缓冲
        /// </summary>
        protected virtual void CancelEdit()
        {
            this.SetDefaultFocus();
        }
    }
}
