using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

/* ==========================================================================
 *  基础控件_Performance
 *  
 *  作者：胡海明 (huhaiming@gmail.com)
 *  日期：2008/08
 *==========================================================================*/
namespace KDS.UI.Component.UserControls
{
    public partial class PerfmonControl : UserControl
    {
        public class SystemMonitor
        {

        }

        public class axSystemMonitor
        {
        }

        public PerfmonControl()
        {
            InitializeComponent();
        }

        public void AddCounter(string counter)
        {
            axSystemMonitor1.Counters.Add(counter);
        }

        public void setSizePerfMon(Size size)
        {
            axSystemMonitor1.Size = size;
        }

        public void setLocationPerfMon(Point point)
        {
            axSystemMonitor1.Location = point;
        }

        private void PerfmonControl_SizeChanged(object sender, EventArgs e)
        {
            this.setSizePerfMon(this.Size);
        }
    }
}