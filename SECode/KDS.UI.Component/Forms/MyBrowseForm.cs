using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.Drawing;
using System.Data;

/* ==========================================================================
 *  数据浏览窗体基类
 *  
 *  作者：胡海明 (huhaiming@gmail.com)
 *  日期：2008/08
 *
 *==========================================================================*/
namespace KDS.UI.Component.Forms
{
    /// <summary>
    /// 数据浏览窗体
    /// </summary>
    public class MyBrowseForm: BaseForm
    {
        /// <summary>
        /// 选择的DataGridViewRows
        /// </summary>
        public DataGridViewSelectedRowCollection SelectedRows;

        private KDS.UI.Component.DataGridViewBase dataGridView1;
        private KDS.UI.Component.ButtonBase btnOK;
        private KDS.UI.Component.ButtonBase btnCancel;
        private LableBase lblRemark;
        public ButtonBase btnMore;

        private bool mMustChoiceItem;

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MyBrowseForm));
            this.dataGridView1 = new KDS.UI.Component.DataGridViewBase();
            this.btnOK = new KDS.UI.Component.ButtonBase();
            this.btnCancel = new KDS.UI.Component.ButtonBase();
            this.lblRemark = new KDS.UI.Component.LableBase();
            this.btnMore = new KDS.UI.Component.ButtonBase();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // _txtDefaultFocus
            // 
            this._txtDefaultFocus.AccessibleDescription = null;
            this._txtDefaultFocus.AccessibleName = null;
            resources.ApplyResources(this._txtDefaultFocus, "_txtDefaultFocus");
            this._txtDefaultFocus.BackgroundImage = null;
            this._txtDefaultFocus.Font = null;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AccessibleDescription = null;
            this.dataGridView1.AccessibleName = null;
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AllowUserToResizeRows = false;
            resources.ApplyResources(this.dataGridView1, "dataGridView1");
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(241)))), ((int)(((byte)(254)))));
            this.dataGridView1.BackgroundImage = null;
            this.dataGridView1.Font = null;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.DoubleClick += new System.EventHandler(this.dataGridView1_DoubleClick);
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            // 
            // btnOK
            // 
            this.btnOK.AccessibleDescription = null;
            this.btnOK.AccessibleName = null;
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.BackgroundImage = null;
            this.btnOK.Font = null;
            this.btnOK.Name = "btnOK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleDescription = null;
            this.btnCancel.AccessibleName = null;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.BackgroundImage = null;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = null;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblRemark
            // 
            this.lblRemark.AccessibleDescription = null;
            this.lblRemark.AccessibleName = null;
            resources.ApplyResources(this.lblRemark, "lblRemark");
            this.lblRemark.BackColor = System.Drawing.Color.Transparent;
            this.lblRemark.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblRemark.Name = "lblRemark";
            // 
            // btnMore
            // 
            this.btnMore.AccessibleDescription = null;
            this.btnMore.AccessibleName = null;
            resources.ApplyResources(this.btnMore, "btnMore");
            this.btnMore.BackgroundImage = null;
            this.btnMore.Font = null;
            this.btnMore.Name = "btnMore";
            this.btnMore.UseVisualStyleBackColor = true;
            // 
            // MyBrowseForm
            // 
            this.AcceptButton = this.btnOK;
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.BackgroundImage = null;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.btnMore);
            this.Controls.Add(this.lblRemark);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.dataGridView1);
            this.Font = null;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = null;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MyBrowseForm";
            this.Load += new System.EventHandler(this.MyBrowseForm_Load);
            this.Controls.SetChildIndex(this.dataGridView1, 0);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this._txtDefaultFocus, 0);
            this.Controls.SetChildIndex(this.lblRemark, 0);
            this.Controls.SetChildIndex(this.btnMore, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        private void InitControl()
        {
            this.dataGridView1.Left = 1;
            this.dataGridView1.Top = 1;
            this.dataGridView1.Width = this.Width-2;
        }

        public MyBrowseForm()
        {
            this.InitializeComponent();
            this.InitControl();
            this.DialogResult = DialogResult.None;
            this.SelectedRows = null;
        }


        /// <summary>
        /// 显示MyBorwse窗体
        /// </summary>
        /// <param name="owner">父窗体</param>
        /// <param name="dtData">数据源</param>
        /// <param name="titleCaption">标题</param>
        /// <param name="allowMultiSelection">是否允许多行选择</param>
        /// <param name="mustChoiceItem">按OK时返回时是否必选一行</param>
        /// <param name="showMaximizeBox">是否显示最大化按钮</param>
        /// <param name="formWidth">窗体宽</param>
        /// <param name="formHeight">窗体高</param>
        /// <param name="txtRemark">备注描述文本</param>
        /// <returns></returns>
        public DialogResult ShowData(IWin32Window owner,DataTable dtData,string titleCaption,bool allowMultiSelection,bool mustChoiceItem,bool showMaximizeBox ,int formWidth,int formHeight,string txtRemark)
        {
            this.mMustChoiceItem = mustChoiceItem;
            this.dataGridView1.DataSource = dtData;
            this.MaximizeBox = showMaximizeBox;
            this.MinimizeBox = showMaximizeBox;
            this.Width = formWidth;
            this.Height = formHeight;
            this.Text = titleCaption;
            this.lblRemark.Text = txtRemark;
            this.dataGridView1.MultiSelect = allowMultiSelection;

            if (owner == null)
                return this.ShowDialog();
            else
                return this.ShowDialog(owner);
        }


        /// <summary>
        /// 显示MyBorwse窗体
        /// </summary>
        /// <param name="owner">父窗体</param>
        /// <param name="dtData">数据源</param>
        /// <param name="titleCaption">标题</param>
        public DialogResult ShowData(IWin32Window owner,DataTable dtData, string titleCaption, string txtRemark)
        {
            return this.ShowData(owner, dtData, titleCaption, false, false, false, this.Width, this.Height, txtRemark);
        }

        /// <summary>
        /// 显示MyBorwse窗体
        /// </summary>
        /// <param name="dtData">数据源</param>
        /// <param name="titleCaption">标题</param>
        public DialogResult ShowData(DataTable dtData, string titleCaption, string txtRemark)
        {
            return this.ShowData(null, dtData, titleCaption, false, false, false, this.Width, this.Height, txtRemark);
        }

        /// <summary>
        /// 显示MyBorwse窗体
        /// </summary>
        /// <param name="owner">父窗体</param>
        /// <param name="dtData">数据源</param>
        /// <param name="titleCaption">标题</param>
        /// <param name="txtRemark">备注描述文本</param>
        public DialogResult ShowData(IWin32Window owner, DataTable dtData, string titleCaption)
        {
            return this.ShowData(owner, dtData, titleCaption, false, false, false, this.Width, this.Height, "");
        }


        /// <summary>
        /// 显示MyBorwse窗体
        /// </summary>
        /// <param name="dtData">数据源</param>
        /// <param name="titleCaption">标题</param>
        /// <param name="txtRemark">备注描述文本</param>
        public DialogResult ShowData(DataTable dtData, string titleCaption)
        {
            return this.ShowData(null,dtData, titleCaption, false, false, false, this.Width, this.Height, "");
        }






        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.mMustChoiceItem)
            {
                if (this.dataGridView1.SelectedRows.Count <= 0)
                {
                    this.DialogResult = DialogResult.None;
                    MessageBox.Show("请选择数据行。", "选择", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }

            this.SelectedRows = this.dataGridView1.SelectedRows;
            this.DialogResult = DialogResult.OK;
        }

        private void MyBrowseForm_Load(object sender, EventArgs e)
        {
            this.dataGridView1.AutoResizeColumns();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            this.btnOK_Click(sender, e);
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.btnOK_Click(sender, e);
        }

    }
}
