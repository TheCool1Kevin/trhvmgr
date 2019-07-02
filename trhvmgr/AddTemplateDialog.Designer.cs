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
            this.cancelbtn = new System.Windows.Forms.Button();
            this.addbtn = new System.Windows.Forms.Button();
            this.validatingComboBox1 = new trhvmgr.UI.ValidatingComboBox();
            this.validatingTextbox1 = new trhvmgr.UI.ValidatingTextbox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.validatingComboBox1);
            this.groupBox1.Controls.Add(this.cancelbtn);
            this.groupBox1.Controls.Add(this.addbtn);
            this.groupBox1.Controls.Add(this.validatingTextbox1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(8);
            this.groupBox1.Size = new System.Drawing.Size(413, 117);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Template VM Properties";
            // 
            // cancelbtn
            // 
            this.cancelbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelbtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelbtn.Location = new System.Drawing.Point(347, 83);
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
            this.addbtn.Location = new System.Drawing.Point(287, 83);
            this.addbtn.Margin = new System.Windows.Forms.Padding(2);
            this.addbtn.Name = "addbtn";
            this.addbtn.Padding = new System.Windows.Forms.Padding(2);
            this.addbtn.Size = new System.Drawing.Size(56, 24);
            this.addbtn.TabIndex = 6;
            this.addbtn.Text = "Add";
            this.addbtn.UseVisualStyleBackColor = true;
            this.addbtn.Click += new System.EventHandler(this.addbtn_Click);
            // 
            // validatingComboBox1
            // 
            this.validatingComboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.validatingComboBox1.ButtonImage = ((System.Drawing.Image)(resources.GetObject("validatingComboBox1.ButtonImage")));
            this.validatingComboBox1.ButtonTooltip = null;
            this.validatingComboBox1.ButtonTooltipTitle = "";
            this.validatingComboBox1.ButtonVisible = false;
            this.validatingComboBox1.ErrorImage = ((System.Drawing.Image)(resources.GetObject("validatingComboBox1.ErrorImage")));
            this.validatingComboBox1.ErrorTooltip = null;
            this.validatingComboBox1.ErrorTooltipTitle = "Validation Error";
            this.validatingComboBox1.IsValid = trhvmgr.Lib.tribool.NEUTRAL;
            this.validatingComboBox1.LabelAutoSize = false;
            this.validatingComboBox1.LabelText = "Base VM Image:";
            this.validatingComboBox1.LabelWidth = 110;
            this.validatingComboBox1.Location = new System.Drawing.Point(10, 46);
            this.validatingComboBox1.Margin = new System.Windows.Forms.Padding(2);
            this.validatingComboBox1.Name = "validatingComboBox1";
            this.validatingComboBox1.SelectedIndex = -1;
            this.validatingComboBox1.Size = new System.Drawing.Size(393, 21);
            this.validatingComboBox1.TabIndex = 8;
            // 
            // validatingTextbox1
            // 
            this.validatingTextbox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.validatingTextbox1.ButtonImage = ((System.Drawing.Image)(resources.GetObject("validatingTextbox1.ButtonImage")));
            this.validatingTextbox1.ButtonTooltip = null;
            this.validatingTextbox1.ButtonTooltipTitle = "";
            this.validatingTextbox1.ButtonVisible = false;
            this.validatingTextbox1.ErrorImage = ((System.Drawing.Image)(resources.GetObject("validatingTextbox1.ErrorImage")));
            this.validatingTextbox1.ErrorTooltip = "Field cannot be blank. Enter a\nvalid name for your new VM.";
            this.validatingTextbox1.ErrorTooltipTitle = "Validation Error";
            this.validatingTextbox1.IsValid = trhvmgr.Lib.tribool.NEUTRAL;
            this.validatingTextbox1.LabelAutoSize = false;
            this.validatingTextbox1.LabelText = "Template VM Name: ";
            this.validatingTextbox1.LabelWidth = 110;
            this.validatingTextbox1.Location = new System.Drawing.Point(10, 23);
            this.validatingTextbox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.validatingTextbox1.Name = "validatingTextbox1";
            this.validatingTextbox1.Size = new System.Drawing.Size(393, 18);
            this.validatingTextbox1.TabIndex = 3;
            // 
            // AddTemplateDialog
            // 
            this.AcceptButton = this.addbtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelbtn;
            this.ClientSize = new System.Drawing.Size(429, 133);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AddTemplateDialog";
            this.Padding = new System.Windows.Forms.Padding(8);
            this.Text = "Add Template VM";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private UI.ValidatingTextbox validatingTextbox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button cancelbtn;
        private System.Windows.Forms.Button addbtn;
        private UI.ValidatingComboBox validatingComboBox1;
    }
}