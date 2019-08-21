namespace trhvmgr
{
    partial class AddTemplateDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddTemplateDialog));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.configComboBox = new trhvmgr.UI.ValidatingComboBox();
            this.adapterComboBox = new trhvmgr.UI.ValidatingComboBox();
            this.serverComboBox = new trhvmgr.UI.ValidatingComboBox();
            this.baseComboBox = new trhvmgr.UI.ValidatingComboBox();
            this.cancelbtn = new System.Windows.Forms.Button();
            this.addbtn = new System.Windows.Forms.Button();
            this.vmTextbox = new trhvmgr.UI.ValidatingTextbox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.configComboBox);
            this.groupBox1.Controls.Add(this.adapterComboBox);
            this.groupBox1.Controls.Add(this.serverComboBox);
            this.groupBox1.Controls.Add(this.baseComboBox);
            this.groupBox1.Controls.Add(this.cancelbtn);
            this.groupBox1.Controls.Add(this.addbtn);
            this.groupBox1.Controls.Add(this.vmTextbox);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(8);
            this.groupBox1.Size = new System.Drawing.Size(385, 194);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Template VM Properties";
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
            this.configComboBox.LabelText = "Configuration Template";
            this.configComboBox.LabelWidth = 120;
            this.configComboBox.Location = new System.Drawing.Point(10, 23);
            this.configComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.configComboBox.Name = "configComboBox";
            this.configComboBox.SelectedIndex = -1;
            this.configComboBox.Size = new System.Drawing.Size(365, 21);
            this.configComboBox.TabIndex = 11;
            // 
            // adapterComboBox
            // 
            this.adapterComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.adapterComboBox.LabelText = "Network Switch";
            this.adapterComboBox.LabelWidth = 120;
            this.adapterComboBox.Location = new System.Drawing.Point(10, 123);
            this.adapterComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.adapterComboBox.Name = "adapterComboBox";
            this.adapterComboBox.SelectedIndex = -1;
            this.adapterComboBox.Size = new System.Drawing.Size(365, 21);
            this.adapterComboBox.TabIndex = 10;
            // 
            // serverComboBox
            // 
            this.serverComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.serverComboBox.ButtonImage = ((System.Drawing.Image)(resources.GetObject("serverComboBox.ButtonImage")));
            this.serverComboBox.ButtonTooltip = null;
            this.serverComboBox.ButtonTooltipTitle = "";
            this.serverComboBox.ButtonVisible = false;
            this.serverComboBox.ErrorImage = ((System.Drawing.Image)(resources.GetObject("serverComboBox.ErrorImage")));
            this.serverComboBox.ErrorTooltip = null;
            this.serverComboBox.ErrorTooltipTitle = "Validation Error";
            this.serverComboBox.IsValid = trhvmgr.Lib.tribool.NEUTRAL;
            this.serverComboBox.LabelAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.serverComboBox.LabelAutoSize = false;
            this.serverComboBox.LabelText = "Target Server Site";
            this.serverComboBox.LabelWidth = 120;
            this.serverComboBox.Location = new System.Drawing.Point(10, 96);
            this.serverComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.serverComboBox.Name = "serverComboBox";
            this.serverComboBox.SelectedIndex = -1;
            this.serverComboBox.Size = new System.Drawing.Size(365, 21);
            this.serverComboBox.TabIndex = 9;
            // 
            // baseComboBox
            // 
            this.baseComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.baseComboBox.ButtonImage = ((System.Drawing.Image)(resources.GetObject("baseComboBox.ButtonImage")));
            this.baseComboBox.ButtonTooltip = null;
            this.baseComboBox.ButtonTooltipTitle = "";
            this.baseComboBox.ButtonVisible = false;
            this.baseComboBox.ErrorImage = ((System.Drawing.Image)(resources.GetObject("baseComboBox.ErrorImage")));
            this.baseComboBox.ErrorTooltip = null;
            this.baseComboBox.ErrorTooltipTitle = "Validation Error";
            this.baseComboBox.IsValid = trhvmgr.Lib.tribool.NEUTRAL;
            this.baseComboBox.LabelAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.baseComboBox.LabelAutoSize = false;
            this.baseComboBox.LabelText = "Base VM Image";
            this.baseComboBox.LabelWidth = 120;
            this.baseComboBox.Location = new System.Drawing.Point(10, 71);
            this.baseComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.baseComboBox.Name = "baseComboBox";
            this.baseComboBox.SelectedIndex = -1;
            this.baseComboBox.Size = new System.Drawing.Size(365, 21);
            this.baseComboBox.TabIndex = 8;
            // 
            // cancelbtn
            // 
            this.cancelbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelbtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelbtn.Location = new System.Drawing.Point(319, 160);
            this.cancelbtn.Margin = new System.Windows.Forms.Padding(2);
            this.cancelbtn.Name = "cancelbtn";
            this.cancelbtn.Padding = new System.Windows.Forms.Padding(2);
            this.cancelbtn.Size = new System.Drawing.Size(56, 24);
            this.cancelbtn.TabIndex = 7;
            this.cancelbtn.Text = "Cancel";
            this.cancelbtn.UseVisualStyleBackColor = true;
            // 
            // addbtn
            // 
            this.addbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.addbtn.Location = new System.Drawing.Point(259, 160);
            this.addbtn.Margin = new System.Windows.Forms.Padding(2);
            this.addbtn.Name = "addbtn";
            this.addbtn.Padding = new System.Windows.Forms.Padding(2);
            this.addbtn.Size = new System.Drawing.Size(56, 24);
            this.addbtn.TabIndex = 6;
            this.addbtn.Text = "Add";
            this.addbtn.UseVisualStyleBackColor = true;
            this.addbtn.Click += new System.EventHandler(this.addbtn_Click);
            // 
            // vmTextbox
            // 
            this.vmTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.vmTextbox.ButtonImage = ((System.Drawing.Image)(resources.GetObject("vmTextbox.ButtonImage")));
            this.vmTextbox.ButtonTooltip = null;
            this.vmTextbox.ButtonTooltipTitle = "";
            this.vmTextbox.ButtonVisible = false;
            this.vmTextbox.ErrorImage = ((System.Drawing.Image)(resources.GetObject("vmTextbox.ErrorImage")));
            this.vmTextbox.ErrorTooltip = "Field cannot be blank. Enter a\nvalid name for your new VM.";
            this.vmTextbox.ErrorTooltipTitle = "Validation Error";
            this.vmTextbox.IsValid = trhvmgr.Lib.tribool.NEUTRAL;
            this.vmTextbox.LabelAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.vmTextbox.LabelAutoSize = false;
            this.vmTextbox.LabelText = "Template VM Name";
            this.vmTextbox.LabelWidth = 120;
            this.vmTextbox.Location = new System.Drawing.Point(10, 48);
            this.vmTextbox.Margin = new System.Windows.Forms.Padding(2);
            this.vmTextbox.Name = "vmTextbox";
            this.vmTextbox.Size = new System.Drawing.Size(365, 18);
            this.vmTextbox.TabIndex = 3;
            // 
            // AddTemplateDialog
            // 
            this.AcceptButton = this.addbtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelbtn;
            this.ClientSize = new System.Drawing.Size(401, 210);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AddTemplateDialog";
            this.Padding = new System.Windows.Forms.Padding(8);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Template VM";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AddTemplateDialog_FormClosing);
            this.Load += new System.EventHandler(this.AddTemplateDialog_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private UI.ValidatingTextbox vmTextbox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button cancelbtn;
        private System.Windows.Forms.Button addbtn;
        private UI.ValidatingComboBox baseComboBox;
        private UI.ValidatingComboBox serverComboBox;
        private UI.ValidatingComboBox adapterComboBox;
        private UI.ValidatingComboBox configComboBox;
    }
}