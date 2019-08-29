namespace trhvmgr
{
    partial class DeployDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeployDialog));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.serverComboBox = new trhvmgr.UI.ValidatingComboBox();
            this.vmTextbox = new trhvmgr.UI.ValidatingTextbox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.configComboBox = new trhvmgr.UI.ValidatingComboBox();
            this.tmplComboBox = new trhvmgr.UI.ValidatingComboBox();
            this.adapterComboBox = new trhvmgr.UI.ValidatingComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.adapterComboBox);
            this.groupBox1.Controls.Add(this.serverComboBox);
            this.groupBox1.Controls.Add(this.vmTextbox);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.configComboBox);
            this.groupBox1.Controls.Add(this.tmplComboBox);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(10, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(10);
            this.groupBox1.Size = new System.Drawing.Size(374, 194);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Deploy Virtual Machine";
            // 
            // serverComboBox
            // 
            this.serverComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.serverComboBox.ButtonImage = ((System.Drawing.Image)(resources.GetObject("serverComboBox.ButtonImage")));
            this.serverComboBox.ButtonTooltip = null;
            this.serverComboBox.ButtonTooltipTitle = "AAA";
            this.serverComboBox.ButtonVisible = false;
            this.serverComboBox.ErrorImage = null;
            this.serverComboBox.ErrorTooltip = null;
            this.serverComboBox.ErrorTooltipTitle = "";
            this.serverComboBox.IsValid = trhvmgr.Lib.tribool.NEUTRAL;
            this.serverComboBox.LabelAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.serverComboBox.LabelAutoSize = false;
            this.serverComboBox.LabelText = "Target Site";
            this.serverComboBox.LabelWidth = 120;
            this.serverComboBox.Location = new System.Drawing.Point(12, 97);
            this.serverComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.serverComboBox.Name = "serverComboBox";
            this.serverComboBox.SelectedIndex = -1;
            this.serverComboBox.Size = new System.Drawing.Size(349, 21);
            this.serverComboBox.TabIndex = 7;
            // 
            // vmTextbox
            // 
            this.vmTextbox.ButtonImage = ((System.Drawing.Image)(resources.GetObject("vmTextbox.ButtonImage")));
            this.vmTextbox.ButtonTooltip = null;
            this.vmTextbox.ButtonTooltipTitle = "AAA";
            this.vmTextbox.ButtonVisible = false;
            this.vmTextbox.ErrorImage = null;
            this.vmTextbox.ErrorTooltip = null;
            this.vmTextbox.ErrorTooltipTitle = "";
            this.vmTextbox.IsValid = trhvmgr.Lib.tribool.NEUTRAL;
            this.vmTextbox.LabelAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.vmTextbox.LabelAutoSize = false;
            this.vmTextbox.LabelText = "Deployment Name";
            this.vmTextbox.LabelWidth = 120;
            this.vmTextbox.Location = new System.Drawing.Point(12, 50);
            this.vmTextbox.Margin = new System.Windows.Forms.Padding(2);
            this.vmTextbox.Name = "vmTextbox";
            this.vmTextbox.Size = new System.Drawing.Size(349, 18);
            this.vmTextbox.TabIndex = 6;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(205, 158);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Deploy";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Location = new System.Drawing.Point(286, 158);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // configComboBox
            // 
            this.configComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.configComboBox.ButtonImage = ((System.Drawing.Image)(resources.GetObject("configComboBox.ButtonImage")));
            this.configComboBox.ButtonTooltip = null;
            this.configComboBox.ButtonTooltipTitle = "AAA";
            this.configComboBox.ButtonVisible = false;
            this.configComboBox.ErrorImage = null;
            this.configComboBox.ErrorTooltip = null;
            this.configComboBox.ErrorTooltipTitle = "";
            this.configComboBox.IsValid = trhvmgr.Lib.tribool.NEUTRAL;
            this.configComboBox.LabelAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.configComboBox.LabelAutoSize = false;
            this.configComboBox.LabelText = "Target Configuration";
            this.configComboBox.LabelWidth = 120;
            this.configComboBox.Location = new System.Drawing.Point(12, 25);
            this.configComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.configComboBox.Name = "configComboBox";
            this.configComboBox.SelectedIndex = -1;
            this.configComboBox.Size = new System.Drawing.Size(349, 21);
            this.configComboBox.TabIndex = 3;
            // 
            // tmplComboBox
            // 
            this.tmplComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tmplComboBox.ButtonImage = ((System.Drawing.Image)(resources.GetObject("tmplComboBox.ButtonImage")));
            this.tmplComboBox.ButtonTooltip = null;
            this.tmplComboBox.ButtonTooltipTitle = "AAA";
            this.tmplComboBox.ButtonVisible = false;
            this.tmplComboBox.ErrorImage = null;
            this.tmplComboBox.ErrorTooltip = null;
            this.tmplComboBox.ErrorTooltipTitle = "";
            this.tmplComboBox.IsValid = trhvmgr.Lib.tribool.NEUTRAL;
            this.tmplComboBox.LabelAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.tmplComboBox.LabelAutoSize = false;
            this.tmplComboBox.LabelText = "Template VM";
            this.tmplComboBox.LabelWidth = 120;
            this.tmplComboBox.Location = new System.Drawing.Point(12, 72);
            this.tmplComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.tmplComboBox.Name = "tmplComboBox";
            this.tmplComboBox.SelectedIndex = -1;
            this.tmplComboBox.Size = new System.Drawing.Size(349, 21);
            this.tmplComboBox.TabIndex = 2;
            // 
            // adapterComboBox
            // 
            this.adapterComboBox.ButtonImage = ((System.Drawing.Image)(resources.GetObject("adapterComboBox.ButtonImage")));
            this.adapterComboBox.ButtonTooltip = null;
            this.adapterComboBox.ButtonTooltipTitle = "AAA";
            this.adapterComboBox.ButtonVisible = false;
            this.adapterComboBox.ErrorImage = null;
            this.adapterComboBox.ErrorTooltip = null;
            this.adapterComboBox.ErrorTooltipTitle = "";
            this.adapterComboBox.IsValid = trhvmgr.Lib.tribool.NEUTRAL;
            this.adapterComboBox.LabelAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.adapterComboBox.LabelAutoSize = false;
            this.adapterComboBox.LabelText = "Network Adapter";
            this.adapterComboBox.LabelWidth = 120;
            this.adapterComboBox.Location = new System.Drawing.Point(12, 124);
            this.adapterComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.adapterComboBox.Name = "adapterComboBox";
            this.adapterComboBox.SelectedIndex = -1;
            this.adapterComboBox.Size = new System.Drawing.Size(349, 21);
            this.adapterComboBox.TabIndex = 8;
            // 
            // DeployDialog
            // 
            this.AcceptButton = this.button2;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button1;
            this.ClientSize = new System.Drawing.Size(394, 214);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "DeployDialog";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Deploy Template VM";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DeployDialog_FormClosing);
            this.Load += new System.EventHandler(this.DeployDialog_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private UI.ValidatingComboBox configComboBox;
        private UI.ValidatingComboBox tmplComboBox;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private UI.ValidatingTextbox vmTextbox;
        private UI.ValidatingComboBox serverComboBox;
        private UI.ValidatingComboBox adapterComboBox;
    }
}