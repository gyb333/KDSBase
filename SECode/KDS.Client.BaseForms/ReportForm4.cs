//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using System.Windows.Forms;
//using C1.Win.C1Preview;
//using C1.C1Preview;

//using KDS.UI.Component.Forms;
//using C1.Win.C1TrueDBGrid;

///* ==========================================================================
// *  报表窗体基类
// *  
// *  作者：胡海明 (huhaiming@gmail.com)
// *  日期：2008/08
// *
// *==========================================================================*/
//namespace KDS.Client.BaseForms
//{
//    /// <summary>
//    /// 报表窗体基类
//    /// 提供报表窗体的公共行为
//    /// Copyrights by huhaiming@gmail.com 2001~2008, 
//    /// </summary>
//    public class ReportForm2: BaseForm
//    {
//        private C1TrueDBGrid PreviewGrid;

//        /// <summary>
//        /// 是否允许打印
//        /// </summary>
        
//        public bool EnabledPrint 
//        {
//            set
//            {
//                PreviewGrid.PrintInfo.PrintPreview();
//                PreviewGrid.PreviewNavigationPanel
//                //c1PrintPreviewControl1.ToolBars.File.Print.Visible = value;
//            }
//        }

//        /// <summary>
//        /// 是否允许导出
//        /// </summary>
//        public bool EnabledExport
//        {
//            set
//            {
//                //c1PrintPreviewControl1.ToolBars.File.Save.Visible = value;
//            }
//        }

//        /// <summary>
//        /// 是否已执行打印
//        /// </summary>
//        public bool HasBeenPrinted = false;

//        private void InitializeComponent()
//        {
//            this.SuspendLayout();
//            // 
//            // ReportForm2
//            // 
//            this.ClientSize = new System.Drawing.Size(792, 527);
//            this.Name = "ReportForm2";
//            this.Text = "报表预览";
//            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
//            this.ResumeLayout(false);
//            this.PerformLayout();

//        }

//        public ReportForm2()
//        {
//            InitializeComponent();
//        }


//        /// <summary>
//        /// 设置预览的对象,huhm2008
//        /// </summary>
//        /// <param name="previewObject">预览的对象如DbGrid</param>
//        public void SetPreviewObject(Object previewObject)
//        {
//            //c1PrintDocument1.Body.Children.Add(new RenderC1Printable(previewObject));
//            //c1PrintDocument1.Generate();
//        }

//        private void c1PrintPreviewControl1_Click(object sender, EventArgs e)
//        {
//            if (1 == 1)
//            {
//                HasBeenPrinted = true;
//            }
//        }

//        //private void reportViewer1_Print(object sender, System.ComponentModel.CancelEventArgs e)
//        //{
//        //    if (!this.EnabledPrint)
//        //    {
//        //        e.Cancel = true;
//        //        MyMessageBox.Show("您没权限或系统不允许打印。");
//        //    }
//        //}

//        //private void reportViewer1_ReportExport(object sender, ReportExportEventArgs e)
//        //{
//        //    if (!this.EnabledExport)
//        //    {
//        //        e.Cancel = true;
//        //        MyMessageBox.Show("您没权限或系统不允许导出。");
//        //    }
//        //}



//    }
//}
