using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using KDS.UI.Component;
/* ==========================================================================
 *  基础控件_DataTextBox
 *  
 *  作者：胡海明 (huhaiming@gmail.com)
 *  日期：2008/08
 *==========================================================================*/
namespace KDS.UI.Component.UserControls
{
    [ToolboxBitmap(typeof(TextBox))]
    [DefaultEvent("RequestNewData")]
    [DefaultProperty("NameText")]
    public partial class DataTextBox : UserControl
    {
        public DataTextBox()
        {
            this.InitializeComponent();

            this.SetSize();
        }

        void DataBindings_CollectionChanged(object sender, CollectionChangeEventArgs e)
        {
            //e.Action = CollectionChangeAction.Refresh; 
        }


        private void SetSize()
        {
            this.Height = this.navButton1.Height;

            this.textBoxBase1.Left = 0;
            this.textBoxBase1.Top = 1;
            this.textBoxBase1.Width = Math.Max(this.Width - this.navButton1.Width - 2, 0);

            this.navButton1.Top = 0;
            this.navButton1.Left = this.textBoxBase1.Width + 1;
        }


        [Category("DataEditProperty")]
        public bool Enabled
        {
            get { return this.textBoxBase1.Enabled; }
            set
            {
                this.textBoxBase1.Enabled = value;
                this.navButton1.Enabled = value;
            }
        }

        //[Category("DataEditProperty")]
        //public override bool Visible
        //{
        //    get { return this.textBoxBase1.Visible; }
        //    set
        //    {
        //        this.textBoxBase1.Visible = value;
        //        this.navButton1.Visible = value;
        //    }
        //}

        [Category("DataEditProperty")]
        public bool ReadOnly
        {
            get { return this.textBoxBase1.ReadOnly; }
            set
            {
                this.textBoxBase1.ReadOnly = value;
                this.navButton1.Enabled = (this.textBoxBase1.Enabled && !this.textBoxBase1.ReadOnly);
            }
        }

        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
                this.textBoxBase1.BackColor = value;
            }
        }


        private int mIDText=0;
        /// <summary>
        /// ID属性
        /// </summary>
        [Category("DataEditProperty")]
        [Bindable(true)]
        [Description("ID字段")]
        [DefaultValue("")]
        public int IDText
        {
            get { return this.mIDText; }
            set
            {
                if (value == null)
                {
                    value = 0;
                }
                if (value != this.IDText)
                {
                    this.mIDText = value;
                }
            }
        }


        /// <summary>
        /// Text属性
        /// </summary>
        [Category("DataEditProperty")]
        [Bindable(true)]
        [Description("Text")]
        [DefaultValue("")]
        public new string Text
        {
            get { return this.textBoxBase1.Text; }
            set
            {
                if (value == null)
                {
                    value = "";
                }
                if (value != this.Text)
                {
                    this.textBoxBase1.Text = value;
                }
            }
        }

        private string mCodeText = "";
        /// <summary>
        /// Code属性
        /// </summary>
        [Category("DataEditProperty")]
        [Bindable(true)]
        [Description("代码字段")]
        [DefaultValue("")]
        public string CodeText
        {
            get { return this.mCodeText; }
            set
            {
                if (value == null)
                {
                    value = "";
                }
                if (value != this.CodeText)
                {
                    this.mCodeText = value;

                    if (this.Focused)
                        this.textBoxBase1.Text = value;
                }
            }
        }


        private string mNameText = "";
        /// <summary>
        /// Name属性
        /// </summary>
        [Category("DataEditProperty")]
        [Bindable(false)]
        [Description("NameText")]
        [DefaultValue("")]
        public string NameText
        {
            get { return this.mNameText; }
            set
            {
                if (value == null)
                {
                    value = "";
                }
                if (value != this.mNameText)
                {
                    this.mNameText = value;

                    if (!this.Focused)
                        this.textBoxBase1.Text = value;
                }

            }
        }


        [Category("DataEditEvents")]
        [Description("Nav按钮时触发的事件")]
        public event EventHandler NavClick;


        [Category("DataEditEvents")]
        [Description("请求新数据时触发的事件")]
        public event EventHandler RequestNewData;



        private void textBoxBase1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
            {
                if (!this.ReadOnly && this.NavClick != null)
                {
                    this.NavClick(sender, null);
                }
            }
        }

        private void textBoxBase1_DoubleClick(object sender, EventArgs e)
        {
            if (!this.ReadOnly && this.NavClick != null)
            {
                this.NavClick(sender, e);
            }
        }

        private void navButton1_Click(object sender, EventArgs e)
        {
            if (!this.ReadOnly && this.NavClick != null)
            {
                this.NavClick(sender, e);
            }
        }


        private void DataTextBox_SizeChanged(object sender, EventArgs e)
        {
            this.SetSize();
        }


        private void DataTextBox_Enter(object sender, EventArgs e)
        {
            this.textBoxBase1.Focus();
            this.Text = this.mCodeText;
        }


        private void DataTextBox_Leave(object sender, EventArgs e)
        {
            if (this.textBoxBase1.IsInputChanged())
            {
                if (!this.ReadOnly && this.RequestNewData != null)
                {
                    this.RequestNewData(sender, e);

                    if (this.DataBindings["IDText"] != null)
                    {
                        this.DataBindings["IDText"].WriteValue();
                        this.DataBindings["CodeText"].WriteValue();
                        this.DataBindings["NameText"].WriteValue();
                    }
                }
            }

            this.Text = this.mNameText;
        }

    }
}