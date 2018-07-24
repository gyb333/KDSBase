using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

using KDS.UI.Component.Forms;

using System.Drawing.Printing;
using System.Drawing.Imaging;
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
    public class ReportForm: BaseForm
    {
        /// <summary>
        /// ReportViewer控件
        /// </summary>
        public ReportViewer reportViewer1;

        /// <summary>
        /// 是否允许打印
        /// </summary>
        public bool EnabledPrint=false;

        /// <summary>
        /// 是否已执行打印
        /// </summary>
        public bool HasBeenPrinted = false;

        /// <summary>
        /// 是否允许导出
        /// </summary>
        public bool EnabledExport=false;

        private void InitializeComponent()
        {
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(792, 527);
            this.reportViewer1.TabIndex = 1;
            this.reportViewer1.ReportExport += new Microsoft.Reporting.WinForms.ExportEventHandler(this.reportViewer1_ReportExport);
            this.reportViewer1.Print += new System.ComponentModel.CancelEventHandler(this.reportViewer1_Print);
            // 
            // ReportForm
            // 
            this.ClientSize = new System.Drawing.Size(792, 527);
            this.Controls.Add(this.reportViewer1);
            this.Name = "ReportForm";
            this.Text = "报表预览";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Controls.SetChildIndex(this.reportViewer1, 0);
            this.Controls.SetChildIndex(this._txtDefaultFocus, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public ReportForm()
        {
            InitializeComponent();
        }


        private void reportViewer1_Print(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!this.EnabledPrint)
            {
                e.Cancel = true;
                MyMessageBox.Show("您没权限或系统不允许打印。");
            }
            else
            {
                this.HasBeenPrinted = true;
            }
        }

        private void reportViewer1_ReportExport(object sender, ReportExportEventArgs e)
        {
            if (!this.EnabledExport)
            {
                e.Cancel = true;
                MyMessageBox.Show("您没权限或系统不允许导出。");
            }
        }



        //private void test()
        //{
        //    if (!this.CanPrintNow)
        //    {
        //        throw new InvalidOperationException();
        //    }
        //    using (PrintDialog dialog = new PrintDialog())
        //    {
        //        dialog.PrinterSettings = this.CreateDefaultPrintSettings();
        //        dialog.AllowSelection = false;
        //        dialog.AllowSomePages = true;
        //        dialog.UseEXDialog = true;
        //        DialogResult result = dialog.ShowDialog(this);
        //        this.SelectedPrinterSettings = dialog.PrinterSettings;
        //        if (result == DialogResult.OK)
        //        {
        //            if ((this.CurrentReport.FileManager.Status == FileManagerStatus.Aborted) || (this.CurrentReport.FileManager.Status == FileManagerStatus.NotStarted))
        //            {
        //                int num;
        //                int toPage;
        //                bool flag = dialog.PrinterSettings.PrintRange == PrintRange.AllPages;
        //                if (!flag)
        //                {
        //                    num = 1;
        //                    toPage = dialog.PrinterSettings.ToPage;
        //                }
        //                else
        //                {
        //                    num = 0;
        //                    toPage = 0;
        //                }
        //                string deviceInfo = this.CreateEMFDeviceInfo(num, toPage);
        //                this.BackgroundThread.BeginRender("IMAGE", true, deviceInfo, new CreateAndRegisterStream(this.CreateStreamEMFPrintOnly), new InternalRenderingCompleteDelegate(this.OnRenderingCompletePrintOnly), new PostRenderArgs(false, !flag), this.Report);
        //            }
        //            PrintDocument document = new ReportPrintDocument(this.CurrentReport.FileManager, (PageSettings)this.PageSettings.Clone());
        //            document.DocumentName = this.Report.DisplayNameForUse;
        //            document.PrinterSettings = dialog.PrinterSettings;
        //            document.Print();
        //        }
        //    }
        //}

        public void ShowPrint(IWin32Window owner, bool lPrintMode)
        {
            if (lPrintMode)
            {
                this.HasBeenPrinted = true;

                this.reportViewer1.RenderingComplete += new RenderingCompleteEventHandler(reportViewer1_RenderingComplete);
            }
            else
            {
                this.reportViewer1.ShowPrintButton = false;
            }
            this.ShowDialog(owner);
        }

        void reportViewer1_RenderingComplete(object sender, RenderingCompleteEventArgs e)
        {
            this.reportViewer1.PrintDialog();

            this.Close();
        }

    }
}
