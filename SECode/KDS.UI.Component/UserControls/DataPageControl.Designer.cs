namespace KDS.UI.Component.UserControls
{
    partial class DataPageControl
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
            this.txtPageNo = new KDS.UI.Component.TextBoxBase();
            this.btnNext = new KDS.UI.Component.NavButton();
            this.btnPrior = new KDS.UI.Component.NavButton();
            this.btnFirst = new KDS.UI.Component.NavButton();
            this.btnLast = new KDS.UI.Component.NavButton();
            this.SuspendLayout();
            // 
            // txtPageNo
            // 
            this.txtPageNo.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.txtPageNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPageNo.Location = new System.Drawing.Point(53, 3);
            this.txtPageNo.Name = "txtPageNo";
            this.txtPageNo.Size = new System.Drawing.Size(78, 21);
            this.txtPageNo.TabIndex = 1;
            this.txtPageNo.Text = "0001/0001";
            this.txtPageNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPageNo_KeyDown);
            this.txtPageNo.Leave += new System.EventHandler(this.txtPageNo_Leave);
            this.txtPageNo.Enter += new System.EventHandler(this.txtPageNo_Enter);
            // 
            // btnNext
            // 
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnNext.Image = global::KDS.UI.Component.Properties.Resources.DataContainer_MoveNextHS;
            this.btnNext.Location = new System.Drawing.Point(138, 1);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(23, 23);
            this.btnNext.TabIndex = 5;
            this.btnNext.TabStop = false;
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrior
            // 
            this.btnPrior.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPrior.Image = global::KDS.UI.Component.Properties.Resources.DataContainer_MovePreviousHS;
            this.btnPrior.Location = new System.Drawing.Point(24, 2);
            this.btnPrior.Name = "btnPrior";
            this.btnPrior.Size = new System.Drawing.Size(23, 23);
            this.btnPrior.TabIndex = 4;
            this.btnPrior.TabStop = false;
            this.btnPrior.UseVisualStyleBackColor = true;
            this.btnPrior.Click += new System.EventHandler(this.btnPrior_Click);
            // 
            // btnFirst
            // 
            this.btnFirst.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFirst.Image = global::KDS.UI.Component.Properties.Resources.DataContainer_MoveFirstHS;
            this.btnFirst.Location = new System.Drawing.Point(0, 2);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(23, 23);
            this.btnFirst.TabIndex = 3;
            this.btnFirst.TabStop = false;
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // btnLast
            // 
            this.btnLast.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnLast.Image = global::KDS.UI.Component.Properties.Resources.DataContainer_MoveLastHS;
            this.btnLast.Location = new System.Drawing.Point(162, 1);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(23, 23);
            this.btnLast.TabIndex = 2;
            this.btnLast.TabStop = false;
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // DataPageControl
            // 
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPrior);
            this.Controls.Add(this.btnFirst);
            this.Controls.Add(this.btnLast);
            this.Controls.Add(this.txtPageNo);
            this.Name = "DataPageControl";
            this.Size = new System.Drawing.Size(188, 25);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private KDS.UI.Component.TextBoxBase txtPageNo;
        private KDS.UI.Component.NavButton btnLast;
        private KDS.UI.Component.NavButton btnFirst;
        private KDS.UI.Component.NavButton btnPrior;
        private KDS.UI.Component.NavButton btnNext;

    }
}
