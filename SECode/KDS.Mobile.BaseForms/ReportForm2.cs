using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using C1.Win.C1Preview;
using C1.C1Preview;

using KDS.UI.Component.Forms;

/* ==========================================================================
 *  报表窗体基类
 *  
 *  作者：胡海明 (huhaiming@gmail.com)
 *  日期：2008/08
 *
 *==========================================================================*/
namespace KDS.Client.BaseForms
{
    /// <summary>
    /// 报表窗体基类
    /// 提供报表窗体的公共行为
    /// Copyrights by huhaiming@gmail.com 2001~2008, 
    /// </summary>
    public class ReportForm2: BaseForm
    {

        /// <summary>
        /// 是否允许打印
        /// </summary>
        
        public bool EnabledPrint 
        {
            set
            {
                c1PrintPreviewControl1.ToolBars.File.Print.Visible = value;
            }
        }
        private C1.C1Preview.C1PrintDocument c1PrintDocument1;
        private C1.Win.C1Preview.C1PrintPreviewControl c1PrintPreviewControl1;

        /// <summary>
        /// 是否允许导出
        /// </summary>
        public bool EnabledExport
        {
            set
            {
                c1PrintPreviewControl1.ToolBars.File.Save.Visible = value;
            }
        }

        /// <summary>
        /// 是否已执行打印
        /// </summary>
        public bool HasBeenPrinted = false;

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportForm2));
            this.c1PrintDocument1 = new C1.C1Preview.C1PrintDocument();
            this.c1PrintPreviewControl1 = new C1.Win.C1Preview.C1PrintPreviewControl();
            ((System.ComponentModel.ISupportInitialize)(this.c1PrintPreviewControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1PrintPreviewControl1.PreviewPane)).BeginInit();
            this.c1PrintPreviewControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // c1PrintDocument1
            // 
            this.c1PrintDocument1.PageLayouts.Default.PageSettings = new C1.C1Preview.C1PageSettings(false, System.Drawing.Printing.PaperKind.A4, false, "25.4mm", "25.4mm", "25.4mm", "25.4mm");
            // 
            // c1PrintPreviewControl1
            // 
            this.c1PrintPreviewControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1PrintPreviewControl1.ImageSet = C1.Win.C1Preview.ImageSetEnum.Adobe;
            this.c1PrintPreviewControl1.Location = new System.Drawing.Point(0, 0);
            this.c1PrintPreviewControl1.Name = "c1PrintPreviewControl1";
            this.c1PrintPreviewControl1.NavigationPanelVisible = false;
            // 
            // c1PrintPreviewControl1.OutlineView
            // 
            this.c1PrintPreviewControl1.PreviewOutlineView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1PrintPreviewControl1.PreviewOutlineView.Location = new System.Drawing.Point(0, 0);
            this.c1PrintPreviewControl1.PreviewOutlineView.Name = "OutlineView";
            this.c1PrintPreviewControl1.PreviewOutlineView.Size = new System.Drawing.Size(165, 427);
            this.c1PrintPreviewControl1.PreviewOutlineView.TabIndex = 0;
            // 
            // c1PrintPreviewControl1.PreviewPane
            // 
            this.c1PrintPreviewControl1.PreviewPane.Document = this.c1PrintDocument1;
            this.c1PrintPreviewControl1.PreviewPane.IntegrateExternalTools = true;
            this.c1PrintPreviewControl1.PreviewPane.TabIndex = 0;
            // 
            // c1PrintPreviewControl1.PreviewTextSearchPanel
            // 
            this.c1PrintPreviewControl1.PreviewTextSearchPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.c1PrintPreviewControl1.PreviewTextSearchPanel.Location = new System.Drawing.Point(530, 0);
            this.c1PrintPreviewControl1.PreviewTextSearchPanel.MinimumSize = new System.Drawing.Size(200, 240);
            this.c1PrintPreviewControl1.PreviewTextSearchPanel.Name = "PreviewTextSearchPanel";
            this.c1PrintPreviewControl1.PreviewTextSearchPanel.Size = new System.Drawing.Size(200, 453);
            this.c1PrintPreviewControl1.PreviewTextSearchPanel.TabIndex = 0;
            this.c1PrintPreviewControl1.PreviewTextSearchPanel.Visible = false;
            // 
            // c1PrintPreviewControl1.ThumbnailView
            // 
            this.c1PrintPreviewControl1.PreviewThumbnailView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1PrintPreviewControl1.PreviewThumbnailView.HideSelection = false;
            this.c1PrintPreviewControl1.PreviewThumbnailView.Location = new System.Drawing.Point(0, 0);
            this.c1PrintPreviewControl1.PreviewThumbnailView.Name = "ThumbnailView";
            this.c1PrintPreviewControl1.PreviewThumbnailView.OwnerDraw = true;
            this.c1PrintPreviewControl1.PreviewThumbnailView.Size = new System.Drawing.Size(165, 427);
            this.c1PrintPreviewControl1.PreviewThumbnailView.TabIndex = 0;
            this.c1PrintPreviewControl1.PreviewThumbnailView.ThumbnailSize = new System.Drawing.Size(96, 96);
            this.c1PrintPreviewControl1.PreviewThumbnailView.UseImageAsThumbnail = false;
            this.c1PrintPreviewControl1.Size = new System.Drawing.Size(792, 527);
            this.c1PrintPreviewControl1.StatusBarVisible = false;
            this.c1PrintPreviewControl1.TabIndex = 3;
            this.c1PrintPreviewControl1.Text = "c1PrintPreviewControl1";
            // 
            // 
            // 
            this.c1PrintPreviewControl1.ToolBars.File.Open.Image = ((System.Drawing.Image)(resources.GetObject("c1PrintPreviewControl1.ToolBars.File.Open.Image")));
            this.c1PrintPreviewControl1.ToolBars.File.Open.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c1PrintPreviewControl1.ToolBars.File.Open.Name = "btnFileOpen";
            this.c1PrintPreviewControl1.ToolBars.File.Open.Size = new System.Drawing.Size(32, 22);
            this.c1PrintPreviewControl1.ToolBars.File.Open.Tag = "C1PreviewActionEnum.FileOpen";
            this.c1PrintPreviewControl1.ToolBars.File.Open.ToolTipText = "Open File";
            // 
            // 
            // 
            this.c1PrintPreviewControl1.ToolBars.File.PageSetup.Image = ((System.Drawing.Image)(resources.GetObject("c1PrintPreviewControl1.ToolBars.File.PageSetup.Image")));
            this.c1PrintPreviewControl1.ToolBars.File.PageSetup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c1PrintPreviewControl1.ToolBars.File.PageSetup.Name = "btnPageSetup";
            this.c1PrintPreviewControl1.ToolBars.File.PageSetup.Size = new System.Drawing.Size(23, 22);
            this.c1PrintPreviewControl1.ToolBars.File.PageSetup.Tag = "C1PreviewActionEnum.PageSetup";
            this.c1PrintPreviewControl1.ToolBars.File.PageSetup.ToolTipText = "Page Setup";
            // 
            // 
            // 
            this.c1PrintPreviewControl1.ToolBars.File.Print.Image = ((System.Drawing.Image)(resources.GetObject("c1PrintPreviewControl1.ToolBars.File.Print.Image")));
            this.c1PrintPreviewControl1.ToolBars.File.Print.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c1PrintPreviewControl1.ToolBars.File.Print.Name = "btnPrint";
            this.c1PrintPreviewControl1.ToolBars.File.Print.Size = new System.Drawing.Size(23, 22);
            this.c1PrintPreviewControl1.ToolBars.File.Print.Tag = "C1PreviewActionEnum.Print";
            this.c1PrintPreviewControl1.ToolBars.File.Print.ToolTipText = "Print";
            // 
            // 
            // 
            this.c1PrintPreviewControl1.ToolBars.File.Reflow.Image = ((System.Drawing.Image)(resources.GetObject("c1PrintPreviewControl1.ToolBars.File.Reflow.Image")));
            this.c1PrintPreviewControl1.ToolBars.File.Reflow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c1PrintPreviewControl1.ToolBars.File.Reflow.Name = "btnReflow";
            this.c1PrintPreviewControl1.ToolBars.File.Reflow.Size = new System.Drawing.Size(23, 22);
            this.c1PrintPreviewControl1.ToolBars.File.Reflow.Tag = "C1PreviewActionEnum.Reflow";
            this.c1PrintPreviewControl1.ToolBars.File.Reflow.ToolTipText = "Reflow";
            // 
            // 
            // 
            this.c1PrintPreviewControl1.ToolBars.File.Save.Image = ((System.Drawing.Image)(resources.GetObject("c1PrintPreviewControl1.ToolBars.File.Save.Image")));
            this.c1PrintPreviewControl1.ToolBars.File.Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c1PrintPreviewControl1.ToolBars.File.Save.Name = "btnFileSave";
            this.c1PrintPreviewControl1.ToolBars.File.Save.Size = new System.Drawing.Size(23, 22);
            this.c1PrintPreviewControl1.ToolBars.File.Save.Tag = "C1PreviewActionEnum.FileSave";
            this.c1PrintPreviewControl1.ToolBars.File.Save.ToolTipText = "Save File";
            // 
            // 
            // 
            this.c1PrintPreviewControl1.ToolBars.Navigation.GoFirst.Image = ((System.Drawing.Image)(resources.GetObject("c1PrintPreviewControl1.ToolBars.Navigation.GoFirst.Image")));
            this.c1PrintPreviewControl1.ToolBars.Navigation.GoFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c1PrintPreviewControl1.ToolBars.Navigation.GoFirst.Name = "btnGoFirst";
            this.c1PrintPreviewControl1.ToolBars.Navigation.GoFirst.Size = new System.Drawing.Size(23, 22);
            this.c1PrintPreviewControl1.ToolBars.Navigation.GoFirst.Tag = "C1PreviewActionEnum.GoFirst";
            this.c1PrintPreviewControl1.ToolBars.Navigation.GoFirst.ToolTipText = "Go To First Page";
            // 
            // 
            // 
            this.c1PrintPreviewControl1.ToolBars.Navigation.GoLast.Image = ((System.Drawing.Image)(resources.GetObject("c1PrintPreviewControl1.ToolBars.Navigation.GoLast.Image")));
            this.c1PrintPreviewControl1.ToolBars.Navigation.GoLast.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c1PrintPreviewControl1.ToolBars.Navigation.GoLast.Name = "btnGoLast";
            this.c1PrintPreviewControl1.ToolBars.Navigation.GoLast.Size = new System.Drawing.Size(23, 22);
            this.c1PrintPreviewControl1.ToolBars.Navigation.GoLast.Tag = "C1PreviewActionEnum.GoLast";
            this.c1PrintPreviewControl1.ToolBars.Navigation.GoLast.ToolTipText = "Go To Last Page";
            // 
            // 
            // 
            this.c1PrintPreviewControl1.ToolBars.Navigation.GoNext.Image = ((System.Drawing.Image)(resources.GetObject("c1PrintPreviewControl1.ToolBars.Navigation.GoNext.Image")));
            this.c1PrintPreviewControl1.ToolBars.Navigation.GoNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c1PrintPreviewControl1.ToolBars.Navigation.GoNext.Name = "btnGoNext";
            this.c1PrintPreviewControl1.ToolBars.Navigation.GoNext.Size = new System.Drawing.Size(23, 22);
            this.c1PrintPreviewControl1.ToolBars.Navigation.GoNext.Tag = "C1PreviewActionEnum.GoNext";
            this.c1PrintPreviewControl1.ToolBars.Navigation.GoNext.ToolTipText = "Go To Next Page";
            // 
            // 
            // 
            this.c1PrintPreviewControl1.ToolBars.Navigation.GoPrev.Image = ((System.Drawing.Image)(resources.GetObject("c1PrintPreviewControl1.ToolBars.Navigation.GoPrev.Image")));
            this.c1PrintPreviewControl1.ToolBars.Navigation.GoPrev.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c1PrintPreviewControl1.ToolBars.Navigation.GoPrev.Name = "btnGoPrev";
            this.c1PrintPreviewControl1.ToolBars.Navigation.GoPrev.Size = new System.Drawing.Size(23, 22);
            this.c1PrintPreviewControl1.ToolBars.Navigation.GoPrev.Tag = "C1PreviewActionEnum.GoPrev";
            this.c1PrintPreviewControl1.ToolBars.Navigation.GoPrev.ToolTipText = "Go To Previous Page";
            // 
            // 
            // 
            this.c1PrintPreviewControl1.ToolBars.Navigation.HistoryNext.Image = ((System.Drawing.Image)(resources.GetObject("c1PrintPreviewControl1.ToolBars.Navigation.HistoryNext.Image")));
            this.c1PrintPreviewControl1.ToolBars.Navigation.HistoryNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c1PrintPreviewControl1.ToolBars.Navigation.HistoryNext.Name = "btnHistoryNext";
            this.c1PrintPreviewControl1.ToolBars.Navigation.HistoryNext.Size = new System.Drawing.Size(32, 22);
            this.c1PrintPreviewControl1.ToolBars.Navigation.HistoryNext.Tag = "C1PreviewActionEnum.HistoryNext";
            this.c1PrintPreviewControl1.ToolBars.Navigation.HistoryNext.ToolTipText = "Next View";
            // 
            // 
            // 
            this.c1PrintPreviewControl1.ToolBars.Navigation.HistoryPrev.Image = ((System.Drawing.Image)(resources.GetObject("c1PrintPreviewControl1.ToolBars.Navigation.HistoryPrev.Image")));
            this.c1PrintPreviewControl1.ToolBars.Navigation.HistoryPrev.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c1PrintPreviewControl1.ToolBars.Navigation.HistoryPrev.Name = "btnHistoryPrev";
            this.c1PrintPreviewControl1.ToolBars.Navigation.HistoryPrev.Size = new System.Drawing.Size(32, 22);
            this.c1PrintPreviewControl1.ToolBars.Navigation.HistoryPrev.Tag = "C1PreviewActionEnum.HistoryPrev";
            this.c1PrintPreviewControl1.ToolBars.Navigation.HistoryPrev.ToolTipText = "Previous View";
            // 
            // 
            // 
            this.c1PrintPreviewControl1.ToolBars.Navigation.LblOfPages.Name = "lblOfPages";
            this.c1PrintPreviewControl1.ToolBars.Navigation.LblOfPages.Size = new System.Drawing.Size(31, 22);
            this.c1PrintPreviewControl1.ToolBars.Navigation.LblOfPages.Tag = "C1PreviewActionEnum.GoPageCount";
            this.c1PrintPreviewControl1.ToolBars.Navigation.LblOfPages.Text = "of 0";
            // 
            // 
            // 
            this.c1PrintPreviewControl1.ToolBars.Navigation.LblPage.Name = "lblPage";
            this.c1PrintPreviewControl1.ToolBars.Navigation.LblPage.Size = new System.Drawing.Size(37, 22);
            this.c1PrintPreviewControl1.ToolBars.Navigation.LblPage.Tag = "C1PreviewActionEnum.GoPageLabel";
            this.c1PrintPreviewControl1.ToolBars.Navigation.LblPage.Text = "Page";
            this.c1PrintPreviewControl1.ToolBars.Navigation.ToolTipPageNo = null;
            // 
            // 
            // 
            this.c1PrintPreviewControl1.ToolBars.Page.Continuous.Checked = true;
            this.c1PrintPreviewControl1.ToolBars.Page.Continuous.CheckState = System.Windows.Forms.CheckState.Checked;
            this.c1PrintPreviewControl1.ToolBars.Page.Continuous.Image = ((System.Drawing.Image)(resources.GetObject("c1PrintPreviewControl1.ToolBars.Page.Continuous.Image")));
            this.c1PrintPreviewControl1.ToolBars.Page.Continuous.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c1PrintPreviewControl1.ToolBars.Page.Continuous.Name = "btnPageContinuous";
            this.c1PrintPreviewControl1.ToolBars.Page.Continuous.Size = new System.Drawing.Size(23, 22);
            this.c1PrintPreviewControl1.ToolBars.Page.Continuous.Tag = "C1PreviewActionEnum.PageContinuous";
            this.c1PrintPreviewControl1.ToolBars.Page.Continuous.ToolTipText = "Continuous View";
            // 
            // 
            // 
            this.c1PrintPreviewControl1.ToolBars.Page.Facing.Image = ((System.Drawing.Image)(resources.GetObject("c1PrintPreviewControl1.ToolBars.Page.Facing.Image")));
            this.c1PrintPreviewControl1.ToolBars.Page.Facing.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c1PrintPreviewControl1.ToolBars.Page.Facing.Name = "btnPageFacing";
            this.c1PrintPreviewControl1.ToolBars.Page.Facing.Size = new System.Drawing.Size(23, 22);
            this.c1PrintPreviewControl1.ToolBars.Page.Facing.Tag = "C1PreviewActionEnum.PageFacing";
            this.c1PrintPreviewControl1.ToolBars.Page.Facing.ToolTipText = "Pages Facing View";
            // 
            // 
            // 
            this.c1PrintPreviewControl1.ToolBars.Page.FacingContinuous.Image = ((System.Drawing.Image)(resources.GetObject("c1PrintPreviewControl1.ToolBars.Page.FacingContinuous.Image")));
            this.c1PrintPreviewControl1.ToolBars.Page.FacingContinuous.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c1PrintPreviewControl1.ToolBars.Page.FacingContinuous.Name = "btnPageFacingContinuous";
            this.c1PrintPreviewControl1.ToolBars.Page.FacingContinuous.Size = new System.Drawing.Size(23, 22);
            this.c1PrintPreviewControl1.ToolBars.Page.FacingContinuous.Tag = "C1PreviewActionEnum.PageFacingContinuous";
            this.c1PrintPreviewControl1.ToolBars.Page.FacingContinuous.ToolTipText = "Pages Facing Continuous View";
            // 
            // 
            // 
            this.c1PrintPreviewControl1.ToolBars.Page.Single.Image = ((System.Drawing.Image)(resources.GetObject("c1PrintPreviewControl1.ToolBars.Page.Single.Image")));
            this.c1PrintPreviewControl1.ToolBars.Page.Single.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c1PrintPreviewControl1.ToolBars.Page.Single.Name = "btnPageSingle";
            this.c1PrintPreviewControl1.ToolBars.Page.Single.Size = new System.Drawing.Size(23, 22);
            this.c1PrintPreviewControl1.ToolBars.Page.Single.Tag = "C1PreviewActionEnum.PageSingle";
            this.c1PrintPreviewControl1.ToolBars.Page.Single.ToolTipText = "Single Page View";
            // 
            // 
            // 
            this.c1PrintPreviewControl1.ToolBars.Text.Find.Image = ((System.Drawing.Image)(resources.GetObject("c1PrintPreviewControl1.ToolBars.Text.Find.Image")));
            this.c1PrintPreviewControl1.ToolBars.Text.Find.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c1PrintPreviewControl1.ToolBars.Text.Find.Name = "btnFind";
            this.c1PrintPreviewControl1.ToolBars.Text.Find.Size = new System.Drawing.Size(23, 22);
            this.c1PrintPreviewControl1.ToolBars.Text.Find.Tag = "C1PreviewActionEnum.Find";
            this.c1PrintPreviewControl1.ToolBars.Text.Find.ToolTipText = "Find Text";
            // 
            // 
            // 
            this.c1PrintPreviewControl1.ToolBars.Text.Hand.Checked = true;
            this.c1PrintPreviewControl1.ToolBars.Text.Hand.CheckState = System.Windows.Forms.CheckState.Checked;
            this.c1PrintPreviewControl1.ToolBars.Text.Hand.Image = ((System.Drawing.Image)(resources.GetObject("c1PrintPreviewControl1.ToolBars.Text.Hand.Image")));
            this.c1PrintPreviewControl1.ToolBars.Text.Hand.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c1PrintPreviewControl1.ToolBars.Text.Hand.Name = "btnHandTool";
            this.c1PrintPreviewControl1.ToolBars.Text.Hand.Size = new System.Drawing.Size(23, 22);
            this.c1PrintPreviewControl1.ToolBars.Text.Hand.Tag = "C1PreviewActionEnum.HandTool";
            this.c1PrintPreviewControl1.ToolBars.Text.Hand.ToolTipText = "Hand Tool";
            // 
            // 
            // 
            this.c1PrintPreviewControl1.ToolBars.Text.SelectText.Image = ((System.Drawing.Image)(resources.GetObject("c1PrintPreviewControl1.ToolBars.Text.SelectText.Image")));
            this.c1PrintPreviewControl1.ToolBars.Text.SelectText.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c1PrintPreviewControl1.ToolBars.Text.SelectText.Name = "btnSelectTextTool";
            this.c1PrintPreviewControl1.ToolBars.Text.SelectText.Size = new System.Drawing.Size(23, 22);
            this.c1PrintPreviewControl1.ToolBars.Text.SelectText.Tag = "C1PreviewActionEnum.SelectTextTool";
            this.c1PrintPreviewControl1.ToolBars.Text.SelectText.ToolTipText = "Text Select Tool";
            // 
            // 
            // 
            this.c1PrintPreviewControl1.ToolBars.Zoom.DropZoomFactor.Name = "dropZoomFactor";
            this.c1PrintPreviewControl1.ToolBars.Zoom.DropZoomFactor.Size = new System.Drawing.Size(13, 22);
            this.c1PrintPreviewControl1.ToolBars.Zoom.DropZoomFactor.Tag = "C1PreviewActionEnum.ZoomFactor";
            this.c1PrintPreviewControl1.ToolBars.Zoom.ToolTipZoomFactor = null;
            // 
            // 
            // 
            this.c1PrintPreviewControl1.ToolBars.Zoom.ZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("c1PrintPreviewControl1.ToolBars.Zoom.ZoomIn.Image")));
            this.c1PrintPreviewControl1.ToolBars.Zoom.ZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c1PrintPreviewControl1.ToolBars.Zoom.ZoomIn.Name = "btnZoomIn";
            this.c1PrintPreviewControl1.ToolBars.Zoom.ZoomIn.Size = new System.Drawing.Size(23, 22);
            this.c1PrintPreviewControl1.ToolBars.Zoom.ZoomIn.Tag = "C1PreviewActionEnum.ZoomIn";
            this.c1PrintPreviewControl1.ToolBars.Zoom.ZoomIn.ToolTipText = "Zoom In";
            // 
            // 
            // 
            this.c1PrintPreviewControl1.ToolBars.Zoom.ZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("c1PrintPreviewControl1.ToolBars.Zoom.ZoomOut.Image")));
            this.c1PrintPreviewControl1.ToolBars.Zoom.ZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c1PrintPreviewControl1.ToolBars.Zoom.ZoomOut.Name = "btnZoomOut";
            this.c1PrintPreviewControl1.ToolBars.Zoom.ZoomOut.Size = new System.Drawing.Size(23, 22);
            this.c1PrintPreviewControl1.ToolBars.Zoom.ZoomOut.Tag = "C1PreviewActionEnum.ZoomOut";
            this.c1PrintPreviewControl1.ToolBars.Zoom.ZoomOut.ToolTipText = "Zoom Out";
            // 
            // 
            // 
            this.c1PrintPreviewControl1.ToolBars.Zoom.ZoomTool.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.c1PrintPreviewControl1.ToolBars.Zoom.ZoomInTool,
            this.c1PrintPreviewControl1.ToolBars.Zoom.ZoomOutTool});
            this.c1PrintPreviewControl1.ToolBars.Zoom.ZoomTool.Image = ((System.Drawing.Image)(resources.GetObject("c1PrintPreviewControl1.ToolBars.Zoom.ZoomTool.Image")));
            this.c1PrintPreviewControl1.ToolBars.Zoom.ZoomTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c1PrintPreviewControl1.ToolBars.Zoom.ZoomTool.Name = "btnZoomTool";
            this.c1PrintPreviewControl1.ToolBars.Zoom.ZoomTool.Size = new System.Drawing.Size(32, 22);
            this.c1PrintPreviewControl1.ToolBars.Zoom.ZoomTool.Tag = "C1PreviewActionEnum.ZoomInTool";
            this.c1PrintPreviewControl1.ToolBars.Zoom.ZoomTool.ToolTipText = "Zoom In Tool";
            this.c1PrintPreviewControl1.Click += new System.EventHandler(this.c1PrintPreviewControl1_Click);
            // 
            // ReportForm2
            // 
            this.ClientSize = new System.Drawing.Size(792, 527);
            this.Controls.Add(this.c1PrintPreviewControl1);
            this.Name = "ReportForm2";
            this.Text = "报表预览";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Controls.SetChildIndex(this._txtDefaultFocus, 0);
            this.Controls.SetChildIndex(this.c1PrintPreviewControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.c1PrintPreviewControl1.PreviewPane)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1PrintPreviewControl1)).EndInit();
            this.c1PrintPreviewControl1.ResumeLayout(false);
            this.c1PrintPreviewControl1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public ReportForm2()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 设置预览的对象,huhm2008
        /// </summary>
        /// <param name="previewObject">预览的对象如DbGrid</param>
        public void SetPreviewObject(Object previewObject)
        {
            c1PrintDocument1.Body.Children.Add(new RenderC1Printable(previewObject));
            c1PrintDocument1.Generate();
        }

        private void c1PrintPreviewControl1_Click(object sender, EventArgs e)
        {
            if (1 == 1)
            {
                HasBeenPrinted = true;
            }
        }

        //private void reportViewer1_Print(object sender, System.ComponentModel.CancelEventArgs e)
        //{
        //    if (!this.EnabledPrint)
        //    {
        //        e.Cancel = true;
        //        MyMessageBox.Show("您没权限或系统不允许打印。");
        //    }
        //}

        //private void reportViewer1_ReportExport(object sender, ReportExportEventArgs e)
        //{
        //    if (!this.EnabledExport)
        //    {
        //        e.Cancel = true;
        //        MyMessageBox.Show("您没权限或系统不允许导出。");
        //    }
        //}



    }
}
