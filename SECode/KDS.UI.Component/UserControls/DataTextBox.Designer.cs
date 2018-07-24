namespace KDS.UI.Component.UserControls
{
    partial class DataTextBox
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
            this.textBoxBase1 = new KDS.UI.Component.TextBoxBase();
            this.navButton1 = new KDS.UI.Component.NavButton();
            this.SuspendLayout();
            // 
            // textBoxBase1
            // 
            this.textBoxBase1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxBase1.Location = new System.Drawing.Point(0, 1);
            this.textBoxBase1.Name = "textBoxBase1";
            this.textBoxBase1.Size = new System.Drawing.Size(166, 21);
            this.textBoxBase1.TabIndex = 1;
            this.textBoxBase1.DoubleClick += new System.EventHandler(this.textBoxBase1_DoubleClick);
            this.textBoxBase1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxBase1_KeyDown);
            // 
            // navButton1
            // 
            this.navButton1.FlatAppearance.BorderSize = 0;
            this.navButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.navButton1.Image = global::KDS.UI.Component.Properties.Resources.Nav1;
            this.navButton1.Location = new System.Drawing.Point(166, 1);
            this.navButton1.Name = "navButton1";
            this.navButton1.Size = new System.Drawing.Size(23, 23);
            this.navButton1.TabIndex = 2;
            this.navButton1.TabStop = false;
            this.navButton1.UseVisualStyleBackColor = true;
            this.navButton1.Click += new System.EventHandler(this.navButton1_Click);
            // 
            // DataTextBox
            // 
            this.Controls.Add(this.navButton1);
            this.Controls.Add(this.textBoxBase1);
            this.Name = "DataTextBox";
            this.Size = new System.Drawing.Size(192, 23);
            this.Leave += new System.EventHandler(this.DataTextBox_Leave);
            this.Enter += new System.EventHandler(this.DataTextBox_Enter);
            this.SizeChanged += new System.EventHandler(this.DataTextBox_SizeChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private KDS.UI.Component.TextBoxBase textBoxBase1;
        private NavButton navButton1;

    }
}
