namespace KDS.Server.RegKey
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnGetServerRegKey = new System.Windows.Forms.Button();
            this.txtServerRegCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtServerSN = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnGetDbEncryptPwd = new System.Windows.Forms.Button();
            this.txtDbEncryptPwd = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDbOrgPwd = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnTest = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(382, 369);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "退出(&X)";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(94, 20);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(364, 21);
            this.txtPassword.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "操作密码：";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnGetServerRegKey);
            this.groupBox2.Controls.Add(this.txtServerRegCode);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtServerSN);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(31, 70);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(431, 129);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "服务注册";
            // 
            // btnGetServerRegKey
            // 
            this.btnGetServerRegKey.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnGetServerRegKey.Location = new System.Drawing.Point(350, 100);
            this.btnGetServerRegKey.Name = "btnGetServerRegKey";
            this.btnGetServerRegKey.Size = new System.Drawing.Size(75, 23);
            this.btnGetServerRegKey.TabIndex = 9;
            this.btnGetServerRegKey.Text = "计算(&C)";
            this.btnGetServerRegKey.UseVisualStyleBackColor = true;
            this.btnGetServerRegKey.Click += new System.EventHandler(this.btnGetServerRegKey_Click);
            // 
            // txtServerRegCode
            // 
            this.txtServerRegCode.Location = new System.Drawing.Point(63, 72);
            this.txtServerRegCode.Name = "txtServerRegCode";
            this.txtServerRegCode.Size = new System.Drawing.Size(364, 21);
            this.txtServerRegCode.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "注册码：";
            // 
            // txtServerSN
            // 
            this.txtServerSN.Location = new System.Drawing.Point(63, 35);
            this.txtServerSN.Name = "txtServerSN";
            this.txtServerSN.Size = new System.Drawing.Size(364, 21);
            this.txtServerSN.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "序列号：";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnGetDbEncryptPwd);
            this.groupBox1.Controls.Add(this.txtDbEncryptPwd);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtDbOrgPwd);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(31, 223);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(431, 129);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据连接";
            // 
            // btnGetDbEncryptPwd
            // 
            this.btnGetDbEncryptPwd.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnGetDbEncryptPwd.Location = new System.Drawing.Point(350, 100);
            this.btnGetDbEncryptPwd.Name = "btnGetDbEncryptPwd";
            this.btnGetDbEncryptPwd.Size = new System.Drawing.Size(75, 23);
            this.btnGetDbEncryptPwd.TabIndex = 9;
            this.btnGetDbEncryptPwd.Text = "计算(&C)";
            this.btnGetDbEncryptPwd.UseVisualStyleBackColor = true;
            this.btnGetDbEncryptPwd.Click += new System.EventHandler(this.btnGetDbEncryptPwd_Click);
            // 
            // txtDbEncryptPwd
            // 
            this.txtDbEncryptPwd.Location = new System.Drawing.Point(63, 72);
            this.txtDbEncryptPwd.Name = "txtDbEncryptPwd";
            this.txtDbEncryptPwd.Size = new System.Drawing.Size(364, 21);
            this.txtDbEncryptPwd.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "加密密文：";
            // 
            // txtDbOrgPwd
            // 
            this.txtDbOrgPwd.Location = new System.Drawing.Point(63, 35);
            this.txtDbOrgPwd.Name = "txtDbOrgPwd";
            this.txtDbOrgPwd.Size = new System.Drawing.Size(364, 21);
            this.txtDbOrgPwd.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 5;
            this.label5.Text = "明文密码：";
            // 
            // btnTest
            // 
            this.btnTest.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnTest.Location = new System.Drawing.Point(37, 369);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 24;
            this.btnTest.Text = "测试";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Visible = false;
            this.btnTest.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(480, 406);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "服务器注册";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtServerRegCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtServerSN;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGetServerRegKey;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnGetDbEncryptPwd;
        private System.Windows.Forms.TextBox txtDbEncryptPwd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDbOrgPwd;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnTest;
    }
}

