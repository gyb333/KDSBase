using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
/* ==========================================================================
 *  数据窗体_编辑工具条控件
 *  
 *  作者：胡海明 (huhaiming@gmail.com)
 *  日期：2008/08
 *  说明：
 *        1.数据窗体编辑控件
 *        2.模型最初来源于CSS
 *==========================================================================*/
namespace KDS.UI.Component
{
    [ToolboxBitmap(typeof(ToolStrip))]
    [DefaultEvent("CloseClick")]
    public class DataEditToolbar: ToolStrip
    {
        //声明控件
        //huhaiming,2008/08
        #region 声明控件

        //tbNew;
        //tbDetail;
        //tbDelete;
         //tbSave;
        //tbMoreFunc;

        //tbFirst;
        //tbPrior;
        //tbNext;
        //tbLast;

        //tbFind;
        //tbRefresh;
        //tbExport;

        //tbPreview;
        //tbPrint;

        //tbClose;

        private System.Windows.Forms.ToolStripButton tbNew;
        private System.Windows.Forms.ToolStripButton tbDetail;
        private System.Windows.Forms.ToolStripButton tbDelete;
        private System.Windows.Forms.ToolStripButton tbSave;
        private System.Windows.Forms.ToolStripButton tbMoreFunc;
        private System.Windows.Forms.ToolStripSeparator tsEdit;
        private System.Windows.Forms.ToolStripButton tbFirst;
        private System.Windows.Forms.ToolStripButton tbPrior;
        private System.Windows.Forms.ToolStripButton tbNext;
        private System.Windows.Forms.ToolStripButton tbLast;
        private System.Windows.Forms.ToolStripSeparator tsNav;
        private System.Windows.Forms.ToolStripButton tbFilter;
        private System.Windows.Forms.ToolStripButton tbFind;
        private System.Windows.Forms.ToolStripButton tbRefresh;
        private System.Windows.Forms.ToolStripButton tbExport;
        private System.Windows.Forms.ToolStripSeparator tsExport;
        private System.Windows.Forms.ToolStripButton tbPreview;
        private System.Windows.Forms.ToolStripButton tbPrint;
        private System.Windows.Forms.ToolStripSeparator tsPrint;
        private System.Windows.Forms.ToolStripButton tbClose;

        #endregion 

        //事件
        //huhaiming,2008/08
        #region 事件

        [Category("DataEditEvents")]
        [Description("New按钮时触发的事件")]
        public event EventHandler NewClick;

        [Category("DataEditEvents")]
        [Description("Detail按钮时触发的事件")]
        public event EventHandler DetailClick;

        [Category("DataEditEvents")]
        [Description("Delete按钮时触发的事件")]
        public event EventHandler DeleteClick;

        [Category("DataEditEvents")]
        [Description("Save按钮时触发的事件")]
        public event EventHandler SaveClick;

        [Category("DataEditEvents")]
        [Description("扩展功能按钮时触发的事件")]
        public event EventHandler MoreFuncClick;

        [Category("DataEditEvents")]
        [Description("First按钮时触发的事件")]
        public event EventHandler FirstClick;

        [Category("DataEditEvents")]
        [Description("Prior按钮时触发的事件")]
        public event EventHandler PriorClick;

        [Category("DataEditEvents")]
        [Description("Next按钮时触发的事件")]
        public event EventHandler NextClick;

        [Category("DataEditEvents")]
        [Description("Last按钮时触发的事件")]
        public event EventHandler LastClick;

        [Category("DataEditEvents")]
        [Description("Filter按钮时触发的事件")]
        public event EventHandler FilterClick;

        [Category("DataEditEvents")]
        [Description("Find按钮时触发的事件")]
        public event EventHandler FindClick;

        [Category("DataEditEvents")]
        [Description("Refresh按钮时触发的事件")]
        public event EventHandler RefreshClick;

        [Category("DataEditEvents")]
        [Description("Export按钮时触发的事件")]
        public event EventHandler ExportClick;

        [Category("DataEditEvents")]
        [Description("Preview按钮时触发的事件")]
        public event EventHandler PreviewClick;

        [Category("DataEditEvents")]
        [Description("Print按钮时触发的事件")]
        public event EventHandler PrintClick;

        [Category("DataEditEvents")]
        [Description("Close按钮时触发的事件")]
        public event EventHandler CloseClick;

        #endregion 


        #region 属性

        //属性
        //huhaiming,2008

        //tbNew;
        [Category("DataEditProperty")]
        public bool NewEnabled
        {
            get { return this.tbNew.Enabled;}
            set { this.tbNew.Enabled = value; }
        }
        [Category("DataEditProperty")]
        public bool NewVisible
        {
            get { return this.tbNew.Visible; }
            set { this.tbNew.Visible = value; this.AdjustSepBar(); }
        }

        //tbDetail;
        [Category("DataEditProperty")]
        public bool DetailEnabled
        {
            get { return this.tbDetail.Enabled; }
            set { this.tbDetail.Enabled = value; }
        }
        [Category("DataEditProperty")]
        public bool DetailVisible
        {
            get { return this.tbDetail.Visible; }
            set { this.tbDetail.Visible = value; this.AdjustSepBar(); }
        }

        //tbDelete;
        [Category("DataEditProperty")]
        public bool DeleteEnabled
        {
            get { return this.tbDelete.Enabled; }
            set { this.tbDelete.Enabled = value; }
        }
        [Category("DataEditProperty")]
        public bool DeleteVisible
        {
            get { return this.tbDelete.Visible; }
            set { this.tbDelete.Visible = value; this.AdjustSepBar(); }
        }


        //tbSave;
        [Category("DataEditProperty")]
        public bool SaveEnabled
        {
            get { return this.tbSave.Enabled; }
            set { this.tbSave.Enabled = value; }
        }
        [Category("DataEditProperty")]
        public bool SaveVisible
        {
            get { return this.tbSave.Visible; }
            set { this.tbSave.Visible = value; this.AdjustSepBar(); }
        }

        //tbMoreFunc;
        [Category("DataEditProperty")]
        public bool MoreFuncEnabled
        {
            get { return this.tbMoreFunc.Enabled; }
            set { this.tbMoreFunc.Enabled = value; }
        }
        [Category("DataEditProperty")]
        public bool MoreFuncVisible
        {
            get { return this.tbMoreFunc.Visible; }
            set { this.tbMoreFunc.Visible = value; this.AdjustSepBar(); }
        }

        //tbFirst;
        [Category("DataEditProperty")]
        public bool FirstEnabled
        {
            get { return this.tbFirst.Enabled; }
            set { this.tbFirst.Enabled = value; }
        }
        [Category("DataEditProperty")]
        public bool FirstVisible
        {
            get { return this.tbFirst.Visible; }
            set { this.tbFirst.Visible = value; this.AdjustSepBar(); }
        }

        //tbPrior;
        [Category("DataEditProperty")]
        public bool PriorEnabled
        {
            get { return this.tbPrior.Enabled; }
            set { this.tbPrior.Enabled = value; }
        }
        [Category("DataEditProperty")]
        public bool PriorVisible
        {
            get { return this.tbPrior.Visible; }
            set { this.tbPrior.Visible = value; this.AdjustSepBar(); }
        }

        //tbNext;
        [Category("DataEditProperty")]
        public bool NextEnabled
        {
            get { return this.tbNext.Enabled; }
            set { this.tbNext.Enabled = value; }
        }
        [Category("DataEditProperty")]
        public bool NextVisible
        {
            get { return this.tbNext.Visible; }
            set { this.tbNext.Visible = value; this.AdjustSepBar(); }
        }

        //tbLast;
        [Category("DataEditProperty")]
        public bool LastEnabled
        {
            get { return this.tbLast.Enabled; }
            set { this.tbLast.Enabled = value; }
        }
        [Category("DataEditProperty")]
        public bool LastVisible
        {
            get { return this.tbLast.Visible; }
            set { this.tbLast.Visible = value; this.AdjustSepBar(); }
        }

        //tbFilter;
        [Category("DataEditProperty")]
        public bool FilterEnabled
        {
            get { return this.tbFilter.Enabled; }
            set { this.tbFilter.Enabled = value; }
        }
        [Category("DataEditProperty")]
        public bool FilterVisible
        {
            get { return this.tbFilter.Visible; }
            set { this.tbFilter.Visible = value; this.AdjustSepBar(); }
        }

        //tbFind;
        [Category("DataEditProperty")]
        public bool FindEnabled
        {
            get { return this.tbFind.Enabled; }
            set { this.tbFind.Enabled = value; }
        }
        [Category("DataEditProperty")]
        public bool FindVisible
        {
            get { return this.tbFind.Visible; }
            set { this.tbFind.Visible = value; this.AdjustSepBar(); }
        }

        //tbRefresh;
        [Category("DataEditProperty")]
        public bool RefreshEnabled
        {
            get { return this.tbRefresh.Enabled; }
            set { this.tbRefresh.Enabled = value; }
        }
        [Category("DataEditProperty")]
        public bool RefreshVisible
        {
            get { return this.tbRefresh.Visible; }
            set { this.tbRefresh.Visible = value; this.AdjustSepBar(); }
        }

        //tbExport;
        [Category("DataEditProperty")]
        public bool ExportEnabled
        {
            get { return this.tbExport.Enabled; }
            set { this.tbExport.Enabled = value; }
        }
        [Category("DataEditProperty")]
        public bool ExportVisible
        {
            get { return this.tbExport.Visible; }
            set { this.tbExport.Visible = value; this.AdjustSepBar(); }
        }

        //tbPreview;
        [Category("DataEditProperty")]
        public bool PreviewEnabled
        {
            get { return this.tbPreview.Enabled; }
            set { this.tbPreview.Enabled = value; }
        }
        [Category("DataEditProperty")]
        public bool PreviewVisible
        {
            get { return this.tbPreview.Visible; }
            set { this.tbPreview.Visible = value; this.AdjustSepBar(); }
        }

        //tbPrint;
        [Category("DataEditProperty")]
        public bool PrintEnabled
        {
            get { return this.tbPrint.Enabled; }
            set { this.tbPrint.Enabled = value; }
        }
        [Category("DataEditProperty")]
        public bool PrintVisible
        {
            get { return this.tbPrint.Visible; }
            set { this.tbPrint.Visible = value; this.AdjustSepBar(); }
        }

        //tbClose;

        #endregion 


        #region 初始化

        //初始化控件
        //huhaiming,2008/08
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataEditToolbar));
            this.tsEdit = new System.Windows.Forms.ToolStripSeparator();
            this.tsNav = new System.Windows.Forms.ToolStripSeparator();
            this.tsExport = new System.Windows.Forms.ToolStripSeparator();
            this.tsPrint = new System.Windows.Forms.ToolStripSeparator();
            this.tbNew = new System.Windows.Forms.ToolStripButton();
            this.tbDetail = new System.Windows.Forms.ToolStripButton();
            this.tbDelete = new System.Windows.Forms.ToolStripButton();
            this.tbSave = new System.Windows.Forms.ToolStripButton();
            this.tbMoreFunc = new System.Windows.Forms.ToolStripButton();
            this.tbFirst = new System.Windows.Forms.ToolStripButton();
            this.tbPrior = new System.Windows.Forms.ToolStripButton();
            this.tbNext = new System.Windows.Forms.ToolStripButton();
            this.tbLast = new System.Windows.Forms.ToolStripButton();
            this.tbFilter = new System.Windows.Forms.ToolStripButton();
            this.tbFind = new System.Windows.Forms.ToolStripButton();
            this.tbRefresh = new System.Windows.Forms.ToolStripButton();
            this.tbExport = new System.Windows.Forms.ToolStripButton();
            this.tbPreview = new System.Windows.Forms.ToolStripButton();
            this.tbPrint = new System.Windows.Forms.ToolStripButton();
            this.tbClose = new System.Windows.Forms.ToolStripButton();
            this.SuspendLayout();
            // 
            // tsEdit
            // 
            this.tsEdit.AccessibleDescription = null;
            this.tsEdit.AccessibleName = null;
            resources.ApplyResources(this.tsEdit, "tsEdit");
            this.tsEdit.Name = "tsEdit";
            // 
            // tsNav
            // 
            this.tsNav.AccessibleDescription = null;
            this.tsNav.AccessibleName = null;
            resources.ApplyResources(this.tsNav, "tsNav");
            this.tsNav.Name = "tsNav";
            // 
            // tsExport
            // 
            this.tsExport.AccessibleDescription = null;
            this.tsExport.AccessibleName = null;
            resources.ApplyResources(this.tsExport, "tsExport");
            this.tsExport.Name = "tsExport";
            // 
            // tsPrint
            // 
            this.tsPrint.AccessibleDescription = null;
            this.tsPrint.AccessibleName = null;
            resources.ApplyResources(this.tsPrint, "tsPrint");
            this.tsPrint.Name = "tsPrint";
            // 
            // tbNew
            // 
            this.tbNew.AccessibleDescription = null;
            this.tbNew.AccessibleName = null;
            resources.ApplyResources(this.tbNew, "tbNew");
            this.tbNew.BackgroundImage = null;
            this.tbNew.Image = global::KDS.UI.Component.Properties.Resources.AddTableHS;
            this.tbNew.Name = "tbNew";
            this.tbNew.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.tbNew.Click += new System.EventHandler(this.tbNew_Click);
            // 
            // tbDetail
            // 
            this.tbDetail.AccessibleDescription = null;
            this.tbDetail.AccessibleName = null;
            resources.ApplyResources(this.tbDetail, "tbDetail");
            this.tbDetail.BackgroundImage = null;
            this.tbDetail.Name = "tbDetail";
            this.tbDetail.Click += new System.EventHandler(this.tbDetail_Click);
            // 
            // tbDelete
            // 
            this.tbDelete.AccessibleDescription = null;
            this.tbDelete.AccessibleName = null;
            resources.ApplyResources(this.tbDelete, "tbDelete");
            this.tbDelete.BackgroundImage = null;
            this.tbDelete.Image = global::KDS.UI.Component.Properties.Resources.DeleteHS;
            this.tbDelete.Name = "tbDelete";
            this.tbDelete.Click += new System.EventHandler(this.tbDelete_Click);
            // 
            // tbSave
            // 
            this.tbSave.AccessibleDescription = null;
            this.tbSave.AccessibleName = null;
            resources.ApplyResources(this.tbSave, "tbSave");
            this.tbSave.BackgroundImage = null;
            this.tbSave.Image = global::KDS.UI.Component.Properties.Resources.saveHS;
            this.tbSave.Name = "tbSave";
            this.tbSave.Click += new System.EventHandler(this.tbSave_Click);
            // 
            // tbMoreFunc
            // 
            this.tbMoreFunc.AccessibleDescription = null;
            this.tbMoreFunc.AccessibleName = null;
            resources.ApplyResources(this.tbMoreFunc, "tbMoreFunc");
            this.tbMoreFunc.BackgroundImage = null;
            this.tbMoreFunc.Image = global::KDS.UI.Component.Properties.Resources.FormulaEvaluatorHS;
            this.tbMoreFunc.Name = "tbMoreFunc";
            this.tbMoreFunc.Click += new System.EventHandler(this.tbMoreFunc_Click);
            // 
            // tbFirst
            // 
            this.tbFirst.AccessibleDescription = null;
            this.tbFirst.AccessibleName = null;
            resources.ApplyResources(this.tbFirst, "tbFirst");
            this.tbFirst.BackgroundImage = null;
            this.tbFirst.Image = global::KDS.UI.Component.Properties.Resources.DataContainer_MoveFirstHS;
            this.tbFirst.Name = "tbFirst";
            this.tbFirst.Click += new System.EventHandler(this.tbFirst_Click);
            // 
            // tbPrior
            // 
            this.tbPrior.AccessibleDescription = null;
            this.tbPrior.AccessibleName = null;
            resources.ApplyResources(this.tbPrior, "tbPrior");
            this.tbPrior.BackgroundImage = null;
            this.tbPrior.Image = global::KDS.UI.Component.Properties.Resources.DataContainer_MovePreviousHS;
            this.tbPrior.Name = "tbPrior";
            this.tbPrior.Click += new System.EventHandler(this.tbPrior_Click);
            // 
            // tbNext
            // 
            this.tbNext.AccessibleDescription = null;
            this.tbNext.AccessibleName = null;
            resources.ApplyResources(this.tbNext, "tbNext");
            this.tbNext.BackgroundImage = null;
            this.tbNext.Image = global::KDS.UI.Component.Properties.Resources.DataContainer_MoveNextHS;
            this.tbNext.Name = "tbNext";
            this.tbNext.Click += new System.EventHandler(this.tbNext_Click);
            // 
            // tbLast
            // 
            this.tbLast.AccessibleDescription = null;
            this.tbLast.AccessibleName = null;
            resources.ApplyResources(this.tbLast, "tbLast");
            this.tbLast.BackgroundImage = null;
            this.tbLast.Image = global::KDS.UI.Component.Properties.Resources.DataContainer_MoveLastHS;
            this.tbLast.Name = "tbLast";
            this.tbLast.Click += new System.EventHandler(this.tbLast_Click);
            // 
            // tbFilter
            // 
            this.tbFilter.AccessibleDescription = null;
            this.tbFilter.AccessibleName = null;
            resources.ApplyResources(this.tbFilter, "tbFilter");
            this.tbFilter.BackgroundImage = null;
            this.tbFilter.Image = global::KDS.UI.Component.Properties.Resources.Filter2HS;
            this.tbFilter.Name = "tbFilter";
            this.tbFilter.Click += new System.EventHandler(this.tbFilter_Click);
            // 
            // tbFind
            // 
            this.tbFind.AccessibleDescription = null;
            this.tbFind.AccessibleName = null;
            resources.ApplyResources(this.tbFind, "tbFind");
            this.tbFind.BackgroundImage = null;
            this.tbFind.Image = global::KDS.UI.Component.Properties.Resources.ActualSizeHS;
            this.tbFind.Name = "tbFind";
            this.tbFind.Click += new System.EventHandler(this.tbFind_Click);
            // 
            // tbRefresh
            // 
            this.tbRefresh.AccessibleDescription = null;
            this.tbRefresh.AccessibleName = null;
            resources.ApplyResources(this.tbRefresh, "tbRefresh");
            this.tbRefresh.BackgroundImage = null;
            this.tbRefresh.Image = global::KDS.UI.Component.Properties.Resources.RefreshDocViewHS;
            this.tbRefresh.Name = "tbRefresh";
            this.tbRefresh.Click += new System.EventHandler(this.tbRefresh_Click);
            // 
            // tbExport
            // 
            this.tbExport.AccessibleDescription = null;
            this.tbExport.AccessibleName = null;
            resources.ApplyResources(this.tbExport, "tbExport");
            this.tbExport.BackgroundImage = null;
            this.tbExport.Image = global::KDS.UI.Component.Properties.Resources.excel;
            this.tbExport.Name = "tbExport";
            this.tbExport.Click += new System.EventHandler(this.tbExport_Click);
            // 
            // tbPreview
            // 
            this.tbPreview.AccessibleDescription = null;
            this.tbPreview.AccessibleName = null;
            resources.ApplyResources(this.tbPreview, "tbPreview");
            this.tbPreview.BackgroundImage = null;
            this.tbPreview.Image = global::KDS.UI.Component.Properties.Resources.PrintPreviewHS;
            this.tbPreview.Name = "tbPreview";
            this.tbPreview.Click += new System.EventHandler(this.tbPreview_Click);
            // 
            // tbPrint
            // 
            this.tbPrint.AccessibleDescription = null;
            this.tbPrint.AccessibleName = null;
            resources.ApplyResources(this.tbPrint, "tbPrint");
            this.tbPrint.BackgroundImage = null;
            this.tbPrint.Image = global::KDS.UI.Component.Properties.Resources.PrintHS;
            this.tbPrint.Name = "tbPrint";
            this.tbPrint.Click += new System.EventHandler(this.tbPrint_Click);
            // 
            // tbClose
            // 
            this.tbClose.AccessibleDescription = null;
            this.tbClose.AccessibleName = null;
            resources.ApplyResources(this.tbClose, "tbClose");
            this.tbClose.BackgroundImage = null;
            this.tbClose.Image = global::KDS.UI.Component.Properties.Resources.Exit2;
            this.tbClose.Name = "tbClose";
            this.tbClose.Click += new System.EventHandler(this.tbClose_Click);
            // 
            // DataEditToolbar
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            this.AllowDrop = true;
            resources.ApplyResources(this, "$this");
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImage = null;
            this.Font = null;
            this.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbNew,
            this.tbDetail,
            this.tbDelete,
            this.tbSave,
            this.tbMoreFunc,
            this.tsEdit,
            this.tbFirst,
            this.tbPrior,
            this.tbNext,
            this.tbLast,
            this.tsNav,
            this.tbFilter,
            this.tbFind,
            this.tbRefresh,
            this.tbExport,
            this.tsExport,
            this.tbPreview,
            this.tbPrint,
            this.tsPrint,
            this.tbClose});
            this.Name = "DataEditToolstrip";
            this.ResumeLayout(false);

        }

        void tbClose_Click(object sender, EventArgs e)
        {
            if (this.CloseClick != null)
                this.CloseClick(sender, e);
        }

        void tbPrint_Click(object sender, EventArgs e)
        {
            if (this.PrintClick != null)
                this.PrintClick(sender, e);
        }

        void tbPreview_Click(object sender, EventArgs e)
        {
            if (this.PreviewClick != null)
                this.PreviewClick(sender, e);
        }

        void tbExport_Click(object sender, EventArgs e)
        {
            if (this.ExportClick != null)
                this.ExportClick(sender, e);
        }

        void tbRefresh_Click(object sender, EventArgs e)
        {
            if (this.RefreshClick != null)
                this.RefreshClick(sender, e);
        }

        void tbFilter_Click(object sender, EventArgs e)
        {
            if (this.FilterClick != null)
                this.FilterClick(sender, e);
        }

        void tbFind_Click(object sender, EventArgs e)
        {
            if (this.FindClick != null)
                this.FindClick(sender, e);
        }

        void tbLast_Click(object sender, EventArgs e)
        {
            if (this.LastClick != null)
                this.LastClick(sender, e);
        }

        void tbNext_Click(object sender, EventArgs e)
        {
            if (this.NextClick != null)
                this.NextClick(sender, e);
        }

        void tbPrior_Click(object sender, EventArgs e)
        {
            if (this.PriorClick != null)
                this.PriorClick(sender, e);
        }

        void tbFirst_Click(object sender, EventArgs e)
        {
            if (this.FirstClick != null)
                this.FirstClick(sender, e);
        }

        void tbMoreFunc_Click(object sender, EventArgs e)
        {
            if (this.MoreFuncClick != null)
                this.MoreFuncClick(sender, e);
        }

        void tbSave_Click(object sender, EventArgs e)
        {
            if (this.SaveClick != null)
                this.SaveClick(sender,e);
        }


        void tbDetail_Click(object sender, EventArgs e)
        {
            if (this.DetailClick != null)
                this.DetailClick(sender, e);
        }

        void tbDelete_Click(object sender, EventArgs e)
        {
            if (this.DeleteClick != null)
                this.DeleteClick(sender, e);
        }

        void tbNew_Click(object sender, EventArgs e)
        {
            if (this.NewClick != null)
                this.NewClick(sender, e);
        }

        #endregion 

        public DataEditToolbar()
        {
            this.InitializeComponent();

            this.AdjustSepBar();
        }

        //自动调整分隔条
        //huhaiming,2008
        private void AdjustSepBar()
        {
            //tbNew;
            //tbDetail;
            //tbDelete;
            //tbSave;
            //tbMoreFunc;
            if (!this.tbNew.Visible && !this.tbDetail.Visible && !this.tbDelete.Visible 
                && !this.tbSave.Visible && !this.tbMoreFunc.Visible)
                this.tsEdit.Visible = false;
            else
                this.tsEdit.Visible = true;

            //tbFirst;
            //tbPrior;
            //tbNext;
            //tbLast;
            if (!this.tbFirst.Visible && !this.tbPrior.Visible && !this.tbNext.Visible && !this.tbLast.Visible)
                this.tsNav.Visible = false;
            else
                this.tsNav.Visible = true;

            //tbFilter
            //tbFind;
            //tbRefresh;
            //tbExport;
            if (!this.tbFilter.Visible && !this.tbFind.Visible && !this.tbRefresh.Visible && !this.tbExport.Visible)
                this.tsExport.Visible = false;
            else
                this.tsExport.Visible = true;


            //tbPreview;
            //tbPrint;
            if (!this.tbPreview.Visible && !this.tbPrint.Visible)
                this.tsPrint.Visible = false;
            else
                this.tsPrint.Visible = true;

            //tbClose;


        }
    }
}
