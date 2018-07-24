namespace KDS.UI.Component.UserControls
{
    partial class PerfmonControl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PerfmonControl));
            this.axSystemMonitor1 = new AxSystemMonitor.AxSystemMonitor();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.axSystemMonitor1)).BeginInit();
            this.SuspendLayout();
            // 
            // axSystemMonitor1
            // 
            this.axSystemMonitor1.Enabled = true;
            this.axSystemMonitor1.Location = new System.Drawing.Point(0, 0);
            this.axSystemMonitor1.Name = "axSystemMonitor1";
            this.axSystemMonitor1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axSystemMonitor1.OcxState")));
            this.axSystemMonitor1.Size = new System.Drawing.Size(972, 407);
            this.axSystemMonitor1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Enabled = false;
            this.label1.ForeColor = System.Drawing.Color.Gray;
            this.label1.Location = new System.Drawing.Point(440, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(239, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "Performance component by KDS team, 2008";
            // 
            // PerfmonControl
            // 
            this.Controls.Add(this.label1);
            this.Controls.Add(this.axSystemMonitor1);
            this.Name = "PerfmonControl";
            this.Size = new System.Drawing.Size(972, 407);
            this.SizeChanged += new System.EventHandler(this.PerfmonControl_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.axSystemMonitor1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AxSystemMonitor.AxSystemMonitor axSystemMonitor1;
        private System.Windows.Forms.Label label1;




    }
}
