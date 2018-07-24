using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using KDS.UI.Component.Forms;
using C1.Win.C1Command;
/* ==========================================================================
 *  基础控件
 *  
 *  作者：胡海明 (huhaiming@gmail.com)
 *  日期：2008/08
 *==========================================================================*/
namespace KDS.UI.Component
{
    public class DockingTabPage2 : C1DockingTabPage
    {
        public DockingTabPage2()
        {


        }

        /// <summary>
        /// 窗体的引用
        /// </summary>
        public BaseForm RunningForm;

        public int AppID;
        public string AppCommand;
        public string DLLName;
        public int SubFuncID;

        public int AuxIntProp1;
        public int AuxIntProp2;

        public string AuxStrProp1;
        public string AuxStrProp2;

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // DockingTabPage2
            // 
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ResumeLayout(false);

        }
    }
}
