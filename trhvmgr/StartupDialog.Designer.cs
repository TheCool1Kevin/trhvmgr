namespace trhvmgr
{
    partial class StartupDialog
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartupDialog));
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.loginBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dbfileTxt = new trhvmgr.UI.ValidatingTextbox();
            this.serverTxt = new trhvmgr.UI.ValidatingTextbox();
            this.pswTxt = new trhvmgr.UI.ValidatingTextbox();
            this.userTxt = new trhvmgr.UI.ValidatingTextbox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(12, 160);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(154, 17);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "Launch interactive console";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // loginBtn
            // 
            this.loginBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.loginBtn.Location = new System.Drawing.Point(178, 156);
            this.loginBtn.Name = "loginBtn";
            this.loginBtn.Size = new System.Drawing.Size(75, 23);
            this.loginBtn.TabIndex = 5;
            this.loginBtn.Text = "Login";
            this.loginBtn.UseVisualStyleBackColor = true;
            this.loginBtn.Click += new System.EventHandler(this.loginBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.Location = new System.Drawing.Point(259, 156);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 6;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.dbfileTxt);
            this.groupBox1.Controls.Add(this.serverTxt);
            this.groupBox1.Controls.Add(this.pswTxt);
            this.groupBox1.Controls.Add(this.userTxt);
            this.groupBox1.Location = new System.Drawing.Point(12, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(5);
            this.groupBox1.Size = new System.Drawing.Size(322, 116);
            this.groupBox1.TabIndex = 10000;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Login as MCA Administrator";
            // 
            // dbfileTxt
            // 
            this.dbfileTxt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dbfileTxt.ButtonImage = ((System.Drawing.Image)(resources.GetObject("dbfileTxt.ButtonImage")));
            this.dbfileTxt.ButtonTooltip = "Opens file dialog to browse for database file.";
            this.dbfileTxt.ButtonTooltipTitle = "Browse for file";
            this.dbfileTxt.ButtonVisible = true;
            this.dbfileTxt.ErrorImage = ((System.Drawing.Image)(resources.GetObject("dbfileTxt.ErrorImage")));
            this.dbfileTxt.ErrorTooltip = null;
            this.dbfileTxt.ErrorTooltipTitle = "";
            this.dbfileTxt.IsValid = trhvmgr.Lib.tribool.NEUTRAL;
            this.dbfileTxt.LabelAutoSize = false;
            this.dbfileTxt.LabelText = "Connection string";
            this.dbfileTxt.LabelWidth = 100;
            this.dbfileTxt.Location = new System.Drawing.Point(7, 86);
            this.dbfileTxt.Margin = new System.Windows.Forms.Padding(2);
            this.dbfileTxt.Name = "dbfileTxt";
            this.dbfileTxt.Size = new System.Drawing.Size(308, 18);
            this.dbfileTxt.TabIndex = 3;
            // 
            // serverTxt
            // 
            this.serverTxt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.serverTxt.ButtonImage = ((System.Drawing.Image)(resources.GetObject("serverTxt.ButtonImage")));
            this.serverTxt.ButtonTooltip = "Sees if the server is online";
            this.serverTxt.ButtonTooltipTitle = "Check server status";
            this.serverTxt.ButtonVisible = true;
            this.serverTxt.ErrorImage = null;
            this.serverTxt.ErrorTooltip = null;
            this.serverTxt.ErrorTooltipTitle = "";
            this.serverTxt.IsValid = trhvmgr.Lib.tribool.NEUTRAL;
            this.serverTxt.LabelAutoSize = false;
            this.serverTxt.LabelText = "Server";
            this.serverTxt.LabelWidth = 100;
            this.serverTxt.Location = new System.Drawing.Point(7, 64);
            this.serverTxt.Margin = new System.Windows.Forms.Padding(2);
            this.serverTxt.Name = "serverTxt";
            this.serverTxt.Size = new System.Drawing.Size(308, 18);
            this.serverTxt.TabIndex = 2;
            // 
            // pswTxt
            // 
            this.pswTxt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pswTxt.ButtonImage = ((System.Drawing.Image)(resources.GetObject("pswTxt.ButtonImage")));
            this.pswTxt.ButtonTooltip = null;
            this.pswTxt.ButtonTooltipTitle = "AAA";
            this.pswTxt.ButtonVisible = false;
            this.pswTxt.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pswTxt.ErrorImage")));
            this.pswTxt.ErrorTooltip = null;
            this.pswTxt.ErrorTooltipTitle = "";
            this.pswTxt.IsValid = trhvmgr.Lib.tribool.NEUTRAL;
            this.pswTxt.LabelAutoSize = false;
            this.pswTxt.LabelText = "Password";
            this.pswTxt.LabelWidth = 100;
            this.pswTxt.Location = new System.Drawing.Point(7, 42);
            this.pswTxt.Margin = new System.Windows.Forms.Padding(2);
            this.pswTxt.Name = "pswTxt";
            this.pswTxt.Size = new System.Drawing.Size(308, 18);
            this.pswTxt.TabIndex = 1;
            // 
            // userTxt
            // 
            this.userTxt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.userTxt.ButtonImage = ((System.Drawing.Image)(resources.GetObject("userTxt.ButtonImage")));
            this.userTxt.ButtonTooltip = null;
            this.userTxt.ButtonTooltipTitle = "AAA";
            this.userTxt.ButtonVisible = false;
            this.userTxt.ErrorImage = ((System.Drawing.Image)(resources.GetObject("userTxt.ErrorImage")));
            this.userTxt.ErrorTooltip = null;
            this.userTxt.ErrorTooltipTitle = "";
            this.userTxt.IsValid = trhvmgr.Lib.tribool.NEUTRAL;
            this.userTxt.LabelAutoSize = false;
            this.userTxt.LabelText = "Username";
            this.userTxt.LabelWidth = 100;
            this.userTxt.Location = new System.Drawing.Point(7, 20);
            this.userTxt.Margin = new System.Windows.Forms.Padding(2);
            this.userTxt.Name = "userTxt";
            this.userTxt.Size = new System.Drawing.Size(308, 18);
            this.userTxt.TabIndex = 0;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(12, 134);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(272, 17);
            this.checkBox2.TabIndex = 4;
            this.checkBox2.Text = "Save this credential in Windows Credential Manager";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // StartupDialog
            // 
            this.AcceptButton = this.loginBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelBtn;
            this.ClientSize = new System.Drawing.Size(346, 189);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.loginBtn);
            this.Controls.Add(this.checkBox1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "StartupDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.Load += new System.EventHandler(this.StartupDialog_Load);
            this.Shown += new System.EventHandler(this.StartupDialog_Shown);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button loginBtn;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private UI.ValidatingTextbox userTxt;
        private UI.ValidatingTextbox pswTxt;
        private UI.ValidatingTextbox serverTxt;
        private UI.ValidatingTextbox dbfileTxt;
        private System.Windows.Forms.CheckBox checkBox2;
    }
}