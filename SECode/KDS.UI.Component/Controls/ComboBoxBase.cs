using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using System.Windows.Forms;
using System.Drawing;

using System.Data;
/* ==========================================================================
 *  基础控件
 *  
 *  作者：胡海明 (huhaiming@gmail.com)
 *  日期：2008/08
 *==========================================================================*/

namespace KDS.UI.Component
{
    [ToolboxBitmap(typeof(ComboBox))]
    public class ComboBoxBase : ComboBox
    {
        private bool lHaveAddAutoCompleteSource = false;  //记住是否已添加过自动搜索数据源
        private string mOldInputValue;

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ComboBoxBase
            // 
            this.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.FormattingEnabled = true;
            this.Leave += new System.EventHandler(this.ComboBoxBase_Leave);
            this.Enter += new System.EventHandler(this.ComboBoxBase_Enter);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ComboBoxBase_KeyDown);
            this.ResumeLayout(false);

        }

        private string mDisableMember = "";
        /// <summary>
        /// 指定数据源搜索的禁止列，此列值为true时仅显示不允许用户选择
        /// </summary>
        [Category("DataEditProperty")]
        [Description("指定数据源搜索的禁止列，此列值为true时仅显示不允许用户选择")]
        [DefaultValue("")]
        public string DisableMemeber
        {
            get { return this.mDisableMember; }
            set
            {
                this.mDisableMember = value;
                //this.DrawMode = DrawMode.OwnerDrawFixed;
            }
        }


        private object mSelectedValueNULLValue = 0;
        /// <summary>
        /// 未选择条目（SelectedValue=NULL）时的缺省值
        /// </summary>
        [Category("DataEditProperty")]
        [Description("未选择条目（SelectedValue=NULL）时的缺省值")]
        [DefaultValue(0)]
        public object SelectedValueNULLValue
        {
            get { return this.mSelectedValueNULLValue; }
            set
            {
                this.mSelectedValueNULLValue = null;
            }
        }

        public ComboBoxBase()
        {
            this.InitializeComponent();

            if (!this.Enabled)
                this.BackColor = UIStyle.EditDisableBackColor;
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

        //重写系统消息
        //警告：严禁随意修改，不小心会耗费系统大量资源
        //作者：huhaiming,2008
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            base.WndProc(ref m);

            const int WM_PAINT = 0xF;

            if (m.Msg == WM_PAINT && this.FlatStyle == System.Windows.Forms.FlatStyle.Popup)
            {
                Graphics g = Graphics.FromHwnd(this.Handle);

                g.DrawRectangle(Pens.LightSteelBlue, this.ClientRectangle.Left, this.ClientRectangle.Top, this.ClientRectangle.Width - 1, this.ClientRectangle.Height - 1);

                g.Dispose();
            }
        }

        private void ComboBoxBase_KeyDown(object sender, KeyEventArgs e)
        {
            //模糊查询
            if (e.KeyCode == Keys.F3)
            {
                this.SearchString();
                e.Handled = true;
            }

            //记住是否已添加过自动搜索数据源
            if (!this.lHaveAddAutoCompleteSource)
            {
                this.lHaveAddAutoCompleteSource = true;
                this.AddAutoCompleteSource();
            }
        }


        private void SearchString()
        {
            if (this.Enabled && this.Items.Count > 0)
            {
                string str = this.Text;
                bool isSetDisableMemeber = (this.DisableMemeber != "" && this.DataSource != null && (this.DataSource is DataTable || this.DataSource is DataView));

                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("模糊查询值列表", typeof(string)));
                for (int i = 0; i < this.Items.Count; i++)
                {
                    string itemText = this.GetItemText(this.Items[i]);
                    if (itemText.IndexOf(str, StringComparison.InvariantCultureIgnoreCase) >= 0)
                    {
                        if (isSetDisableMemeber)
                        {
                            DataRowView dr = (DataRowView)this.Items[i];
                            bool disable = (bool)dr[this.DisableMemeber];
                            if (disable)
                                continue;
                        }

                        dt.Rows.Add(itemText);
                    }
                }

                Forms.MyBrowseForm browseForm = new Forms.MyBrowseForm();
                browseForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;

                if (browseForm.ShowData(this, dt, "模糊查找", false, true, false, 300, 350, "") == DialogResult.OK)
                {
                    this.Text = browseForm.SelectedRows[0].Cells[0].Value.ToString();
                }

                browseForm.Dispose();
                dt.Dispose();

            }
        }

        public new object SelectedValue
        {
            get
            {
                object val = base.SelectedValue;
                if (base.SelectedValue == null)
                {
                    val = this.mSelectedValueNULLValue;
                }
                return val;
            }
            set
            {
                base.SelectedValue = value;
            }
        }


        private void ComboBoxBase_Leave(object sender, EventArgs e)
        {
            //用户输入的文本
            if (this.SelectedIndex == -1 && this.IsInputChanged())
            {
                //输入的文本去匹配选择项
                if (this.Text != "")
                {
                    int pos = -1;
                    for (int i = 0; i < this.Items.Count; i++)
                    {
                        if (string.Compare(this.GetItemText(this.Items[i]), this.Text, StringComparison.InvariantCultureIgnoreCase) == 0)
                        {
                            pos = i;
                            break;
                        }
                    }
                    this.SelectedIndex = pos;
                }

                //输入不存在的值清空文本
                if (this.ValueMember != "" && this.SelectedIndex == -1 && this.Text != "")
                {
                    this.Text = "";
                }


                //写入Binding
                if (this.DataBindings["SelectedValue"] != null)
                {
                    this.DataBindings["SelectedValue"].WriteValue();
                }
            }

            //检查禁止列，禁止用户选择
            if (this.SelectedIndex != -1 && this.IsInputChanged())
            {
                if (this.DisableMemeber != "" && this.SelectedItem is DataRowView && this.SelectedItem != null)
                {
                    DataRowView dr = (DataRowView)this.SelectedItem;
                    if ((bool)dr[this.DisableMemeber])
                        this.SelectedIndex = -1;
                }
            }
        }

        private void ComboBoxBase_Enter(object sender, EventArgs e)
        {
            this.mOldInputValue = this.Text;
        }


        protected override void OnSelectionChangeCommitted(EventArgs e)
        {
            base.OnSelectionChangeCommitted(e);

            //检查禁止列，禁止用户选择
            if (this.SelectedItem != null)
            {
                if (this.DisableMemeber != "" && this.SelectedItem is DataRowView && this.SelectedItem != null)
                {
                    DataRowView dr = (DataRowView)this.SelectedItem;
                    if ((bool)dr[this.DisableMemeber])
                        this.SelectedIndex = -1;
                }
            }
        }

        //自绘Disable状态
        //huhm2011/11
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            base.OnDrawItem(e);
            
            if (e.Index == -1) return;

            if (this.DisableMemeber != "" && this.Items[e.Index] is DataRowView)
            {
                DataRowView dr = (DataRowView)this.Items[e.Index];
                bool disable = (bool)dr[this.DisableMemeber];
                string displayText = dr[this.DisplayMember].ToString();

                e.DrawBackground();
                if (!disable)
                {
                    SolidBrush brush = new SolidBrush(e.ForeColor);
                    e.Graphics.DrawString(displayText, e.Font, brush, e.Bounds, StringFormat.GenericDefault);
                }
                else
                {
                    SolidBrush brush = new SolidBrush(Color.DarkGray);
                    e.Graphics.DrawString(displayText, e.Font, brush, e.Bounds, StringFormat.GenericDefault);
                }
                e.DrawFocusRectangle();
            }
        }


        //自动添加AutoComplete数据源
        private void AddAutoCompleteSource()
        {
            if (this.AutoCompleteMode != AutoCompleteMode.None)
            {
                if (this.DisplayMember != "" && this.DataSource != null && (this.DataSource is DataTable || this.DataSource is DataView))
                {
                    DataView dv;
                    if (this.DataSource is DataView)
                        dv = (DataView)this.DataSource;
                    else
                        dv = ((DataTable)this.DataSource).DefaultView;

                    this.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    foreach (DataRowView dr in dv)
                    {
                        this.AutoCompleteCustomSource.Add(dr[DisplayMember].ToString());
                    }
                }
            }
        }

    }
}
