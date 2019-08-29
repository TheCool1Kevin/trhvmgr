namespace trhvmgr
{
    partial class BrowseVhdDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BrowseVhdDialog));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.validatingTextbox1 = new trhvmgr.UI.ValidatingTextbox();
            this.serverComboBox = new trhvmgr.UI.ValidatingComboBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Location = new System.Drawing.Point(300, 65);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(219, 65);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "OK";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(12, 65);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 12;
            this.button3.Text = "Browse...";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // validatingTextbox1
            // 
            this.validatingTextbox1.ButtonImage = ((System.Drawing.Image)(resources.GetObject("validatingTextbox1.ButtonImage")));
            this.validatingTextbox1.ButtonTooltip = null;
            this.validatingTextbox1.ButtonTooltipTitle = "AAA";
            this.validatingTextbox1.ButtonVisible = false;
            this.validatingTextbox1.ErrorImage = null;
            this.validatingTextbox1.ErrorTooltip = null;
            this.validatingTextbox1.ErrorTooltipTitle = "";
            this.validatingTextbox1.IsValid = trhvmgr.Lib.tribool.NEUTRAL;
            this.validatingTextbox1.LabelAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.validatingTextbox1.LabelAutoSize = false;
            this.validatingTextbox1.LabelText = "VHD Path";
            this.validatingTextbox1.LabelWidth = 120;
            this.validatingTextbox1.Location = new System.Drawing.Point(11, 36);
            this.validatingTextbox1.Margin = new System.Windows.Forms.Padding(2);
            this.validatingTextbox1.Name = "validatingTextbox1";
            this.validatingTextbox1.Size = new System.Drawing.Size(364, 18);
            this.validatingTextbox1.TabIndex = 11;
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
            this.serverComboBox.Location = new System.Drawing.Point(11, 11);
            this.serverComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.serverComboBox.Name = "serverComboBox";
            this.serverComboBox.SelectedIndex = -1;
            this.serverComboBox.Size = new System.Drawing.Size(364, 21);
            this.serverComboBox.TabIndex = 10;
            // 
            // BrowseVhdDialog
            // 
            this.AcceptButton = this.button2;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button1;
            this.ClientSize = new System.Drawing.Size(387, 100);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.validatingTextbox1);
            this.Controls.Add(this.serverComboBox);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "BrowseVhdDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "BrowseVhdDialog";
            this.Load += new System.EventHandler(this.BrowseVhdDialog_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private UI.ValidatingComboBox serverComboBox;
        private UI.ValidatingTextbox validatingTextbox1;
        private System.Windows.Forms.Button button3;
    }
}