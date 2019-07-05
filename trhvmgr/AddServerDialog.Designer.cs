namespace trhvmgr
{
    partial class AddServerDialog
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddServerDialog));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.macText = new trhvmgr.UI.ValidatingTextbox();
            this.ipText = new trhvmgr.UI.ValidatingTextbox();
            this.hostnameText = new trhvmgr.UI.ValidatingTextbox();
            this.cancelbtn = new System.Windows.Forms.Button();
            this.addbtn = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.listBox1);
            this.groupBox1.Controls.Add(this.macText);
            this.groupBox1.Controls.Add(this.ipText);
            this.groupBox1.Controls.Add(this.hostnameText);
            this.groupBox1.Controls.Add(this.cancelbtn);
            this.groupBox1.Controls.Add(this.addbtn);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(8);
            this.groupBox1.Size = new System.Drawing.Size(302, 301);
            this.groupBox1.TabIndex = 100;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Server Properties";
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox1.AutoSize = true;
            this.checkBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkBox1.Location = new System.Drawing.Point(10, 271);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(151, 17);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Text = "Don\'t overwrite information";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.MouseLeave += new System.EventHandler(this.checkBox1_MouseLeave);
            this.checkBox1.MouseHover += new System.EventHandler(this.checkBox1_MouseHover);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 99);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 13);
            this.label1.TabIndex = 100;
            this.label1.Text = "Detected Virtual Machines:";
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.HorizontalScrollbar = true;
            this.listBox1.Location = new System.Drawing.Point(10, 117);
            this.listBox1.Margin = new System.Windows.Forms.Padding(2);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(283, 134);
            this.listBox1.TabIndex = 100;
            // 
            // macText
            // 
            this.macText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.macText.ButtonImage = ((System.Drawing.Image)(resources.GetObject("macText.ButtonImage")));
            this.macText.ButtonTooltip = "Automatically get MAC address given server host name.";
            this.macText.ButtonTooltipTitle = "Autofill MAC Address";
            this.macText.ButtonVisible = true;
            this.macText.ErrorImage = ((System.Drawing.Image)(resources.GetObject("macText.ErrorImage")));
            this.macText.ErrorTooltip = "Unable to get MAC address.\nIs machine turned on?";
            this.macText.ErrorTooltipTitle = "Network Error";
            this.macText.IsValid = trhvmgr.Lib.tribool.NEUTRAL;
            this.macText.LabelAutoSize = false;
            this.macText.LabelText = "Server Mac:";
            this.macText.LabelWidth = 95;
            this.macText.Location = new System.Drawing.Point(10, 68);
            this.macText.Margin = new System.Windows.Forms.Padding(2);
            this.macText.Name = "macText";
            this.macText.Size = new System.Drawing.Size(283, 18);
            this.macText.TabIndex = 2;
            // 
            // ipText
            // 
            this.ipText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ipText.ButtonImage = ((System.Drawing.Image)(resources.GetObject("ipText.ButtonImage")));
            this.ipText.ButtonTooltip = "Automatically get IP address given server host name.";
            this.ipText.ButtonTooltipTitle = "Autofill IP Address";
            this.ipText.ButtonVisible = true;
            this.ipText.ErrorImage = ((System.Drawing.Image)(resources.GetObject("ipText.ErrorImage")));
            this.ipText.ErrorTooltip = "Unable to get IP address.\nIs machine turned on?";
            this.ipText.ErrorTooltipTitle = "Network Error";
            this.ipText.IsValid = trhvmgr.Lib.tribool.NEUTRAL;
            this.ipText.LabelAutoSize = false;
            this.ipText.LabelText = "Server IP:";
            this.ipText.LabelWidth = 95;
            this.ipText.Location = new System.Drawing.Point(10, 46);
            this.ipText.Margin = new System.Windows.Forms.Padding(2);
            this.ipText.Name = "ipText";
            this.ipText.Size = new System.Drawing.Size(283, 18);
            this.ipText.TabIndex = 1;
            // 
            // hostnameText
            // 
            this.hostnameText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hostnameText.ButtonImage = ((System.Drawing.Image)(resources.GetObject("hostnameText.ButtonImage")));
            this.hostnameText.ButtonTooltip = null;
            this.hostnameText.ButtonTooltipTitle = "";
            this.hostnameText.ButtonVisible = false;
            this.hostnameText.ErrorImage = ((System.Drawing.Image)(resources.GetObject("hostnameText.ErrorImage")));
            this.hostnameText.ErrorTooltip = "Check your spelling. Unable to find machine.\nIs machine turned on?";
            this.hostnameText.ErrorTooltipTitle = "Network Error";
            this.hostnameText.IsValid = trhvmgr.Lib.tribool.NEUTRAL;
            this.hostnameText.LabelAutoSize = false;
            this.hostnameText.LabelText = "Server Name:";
            this.hostnameText.LabelWidth = 95;
            this.hostnameText.Location = new System.Drawing.Point(10, 23);
            this.hostnameText.Margin = new System.Windows.Forms.Padding(2);
            this.hostnameText.Name = "hostnameText";
            this.hostnameText.Size = new System.Drawing.Size(283, 18);
            this.hostnameText.TabIndex = 0;
            // 
            // cancelbtn
            // 
            this.cancelbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelbtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelbtn.Location = new System.Drawing.Point(236, 266);
            this.cancelbtn.Margin = new System.Windows.Forms.Padding(2);
            this.cancelbtn.Name = "cancelbtn";
            this.cancelbtn.Padding = new System.Windows.Forms.Padding(2);
            this.cancelbtn.Size = new System.Drawing.Size(56, 24);
            this.cancelbtn.TabIndex = 5;
            this.cancelbtn.Text = "Cancel";
            this.cancelbtn.UseVisualStyleBackColor = true;
            // 
            // addbtn
            // 
            this.addbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.addbtn.Location = new System.Drawing.Point(175, 266);
            this.addbtn.Margin = new System.Windows.Forms.Padding(2);
            this.addbtn.Name = "addbtn";
            this.addbtn.Padding = new System.Windows.Forms.Padding(2);
            this.addbtn.Size = new System.Drawing.Size(56, 24);
            this.addbtn.TabIndex = 4;
            this.addbtn.Text = "Add";
            this.addbtn.UseVisualStyleBackColor = true;
            this.addbtn.Click += new System.EventHandler(this.addbtn_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Warning;
            this.toolTip1.ToolTipTitle = "Surpress validation";
            // 
            // AddServerDialog
            // 
            this.AcceptButton = this.addbtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelbtn;
            this.ClientSize = new System.Drawing.Size(318, 317);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "AddServerDialog";
            this.Padding = new System.Windows.Forms.Padding(8);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add New Server";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button cancelbtn;
        private System.Windows.Forms.Button addbtn;
        private UI.ValidatingTextbox hostnameText;
        private UI.ValidatingTextbox ipText;
        private UI.ValidatingTextbox macText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}