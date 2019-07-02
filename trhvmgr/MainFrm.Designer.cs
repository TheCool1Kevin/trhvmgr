using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace trhvmgr
{
    public class MainFormToolStripRenderer : ToolStripProfessionalRenderer
    {
        public MainFormToolStripRenderer()
        {
            this.RoundedEdges = false;
        }

        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            using (LinearGradientBrush b = new LinearGradientBrush(
                e.AffectedBounds,
                ColorTable.ToolStripGradientMiddle,
                ColorTable.ToolStripGradientEnd,
                LinearGradientMode.Vertical
            ))
            {
                e.Graphics.FillRectangle(b, e.AffectedBounds);
            }
        }
    }

    public class MainFormMenuStripRenderer : ToolStripProfessionalRenderer
    {
        public MainFormMenuStripRenderer()
        {
            this.RoundedEdges = false;
        }

        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            using (LinearGradientBrush b = new LinearGradientBrush(
                e.AffectedBounds,
                ColorTable.ToolStripGradientBegin,
                ColorTable.ToolStripGradientMiddle,
                LinearGradientMode.Vertical
            ))
            {
                e.Graphics.FillRectangle(b, e.AffectedBounds);
            }
        }
    }

    partial class MainFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrm));
            this.mainToolstrip = new System.Windows.Forms.ToolStrip();
            this.addServerToolButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolbtnStartVm = new System.Windows.Forms.ToolStripButton();
            this.toolbtnPauseVm = new System.Windows.Forms.ToolStripButton();
            this.toolbtnStopVm = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.treeListView = new BrightIdeasSoftware.TreeListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripSpringLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.queryText = new System.Windows.Forms.TextBox();
            this.collectionsList = new System.Windows.Forms.ListBox();
            this.mainToolstrip.SuspendLayout();
            this.mainMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainToolstrip
            // 
            this.mainToolstrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mainToolstrip.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.mainToolstrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addServerToolButton,
            this.toolStripButton2,
            this.toolStripButton6,
            this.toolStripSeparator1,
            this.toolbtnStartVm,
            this.toolbtnPauseVm,
            this.toolbtnStopVm,
            this.toolStripSeparator2,
            this.toolStripButton3,
            this.toolStripButton5,
            this.toolStripButton4});
            this.mainToolstrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.mainToolstrip.Location = new System.Drawing.Point(0, 28);
            this.mainToolstrip.Name = "mainToolstrip";
            this.mainToolstrip.Padding = new System.Windows.Forms.Padding(5);
            this.mainToolstrip.Size = new System.Drawing.Size(800, 46);
            this.mainToolstrip.TabIndex = 1;
            // 
            // addServerToolButton
            // 
            this.addServerToolButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addServerToolButton.Image = ((System.Drawing.Image)(resources.GetObject("addServerToolButton.Image")));
            this.addServerToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addServerToolButton.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.addServerToolButton.Name = "addServerToolButton";
            this.addServerToolButton.Padding = new System.Windows.Forms.Padding(2);
            this.addServerToolButton.Size = new System.Drawing.Size(36, 36);
            this.addServerToolButton.Text = "Add New Server";
            this.addServerToolButton.Click += new System.EventHandler(this.addServerToolButton_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Padding = new System.Windows.Forms.Padding(2);
            this.toolStripButton2.Size = new System.Drawing.Size(36, 36);
            this.toolStripButton2.Text = "Import Base VM";
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton6.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton6.Image")));
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Padding = new System.Windows.Forms.Padding(2);
            this.toolStripButton6.Size = new System.Drawing.Size(36, 36);
            this.toolStripButton6.Text = "New Template VM";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 36);
            // 
            // toolbtnStartVm
            // 
            this.toolbtnStartVm.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolbtnStartVm.Image = ((System.Drawing.Image)(resources.GetObject("toolbtnStartVm.Image")));
            this.toolbtnStartVm.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolbtnStartVm.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.toolbtnStartVm.Name = "toolbtnStartVm";
            this.toolbtnStartVm.Padding = new System.Windows.Forms.Padding(2);
            this.toolbtnStartVm.Size = new System.Drawing.Size(36, 36);
            this.toolbtnStartVm.Text = "Start VM";
            // 
            // toolbtnPauseVm
            // 
            this.toolbtnPauseVm.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolbtnPauseVm.Image = ((System.Drawing.Image)(resources.GetObject("toolbtnPauseVm.Image")));
            this.toolbtnPauseVm.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolbtnPauseVm.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.toolbtnPauseVm.Name = "toolbtnPauseVm";
            this.toolbtnPauseVm.Padding = new System.Windows.Forms.Padding(2);
            this.toolbtnPauseVm.Size = new System.Drawing.Size(36, 36);
            this.toolbtnPauseVm.Text = "Pause VM";
            // 
            // toolbtnStopVm
            // 
            this.toolbtnStopVm.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolbtnStopVm.Image = ((System.Drawing.Image)(resources.GetObject("toolbtnStopVm.Image")));
            this.toolbtnStopVm.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolbtnStopVm.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.toolbtnStopVm.Name = "toolbtnStopVm";
            this.toolbtnStopVm.Padding = new System.Windows.Forms.Padding(2);
            this.toolbtnStopVm.Size = new System.Drawing.Size(36, 36);
            this.toolbtnStopVm.Text = "Stop VM";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 36);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Padding = new System.Windows.Forms.Padding(2);
            this.toolStripButton3.Size = new System.Drawing.Size(36, 36);
            this.toolStripButton3.Text = "Power On Host";
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton5.Image")));
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Padding = new System.Windows.Forms.Padding(2);
            this.toolStripButton5.Size = new System.Drawing.Size(36, 36);
            this.toolStripButton5.Text = "Power Off Host";
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Padding = new System.Windows.Forms.Padding(2);
            this.toolStripButton4.Size = new System.Drawing.Size(36, 36);
            this.toolStripButton4.Text = "Restart Host";
            // 
            // mainMenu
            // 
            this.mainMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(800, 28);
            this.mainMenu.TabIndex = 0;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(47, 24);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // treeListView
            // 
            this.treeListView.AllColumns.Add(this.olvColumn1);
            this.treeListView.AllColumns.Add(this.olvColumn2);
            this.treeListView.AllColumns.Add(this.olvColumn3);
            this.treeListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeListView.CellEditUseWholeCell = false;
            this.treeListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2,
            this.olvColumn3});
            this.treeListView.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeListView.EmptyListMsg = "Add server to continue.";
            this.treeListView.FullRowSelect = true;
            this.treeListView.Location = new System.Drawing.Point(3, 3);
            this.treeListView.Margin = new System.Windows.Forms.Padding(4);
            this.treeListView.MultiSelect = false;
            this.treeListView.Name = "treeListView";
            this.treeListView.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.Submenu;
            this.treeListView.ShowCommandMenuOnRightClick = true;
            this.treeListView.ShowGroups = false;
            this.treeListView.ShowImagesOnSubItems = true;
            this.treeListView.ShowItemToolTips = true;
            this.treeListView.Size = new System.Drawing.Size(786, 345);
            this.treeListView.SmallImageList = this.imageList1;
            this.treeListView.TabIndex = 3;
            this.treeListView.TileSize = new System.Drawing.Size(16, 16);
            this.treeListView.UseCompatibleStateImageBehavior = false;
            this.treeListView.UseFilterIndicator = true;
            this.treeListView.UseFiltering = true;
            this.treeListView.UseHotItem = true;
            this.treeListView.UseTranslucentSelection = true;
            this.treeListView.View = System.Windows.Forms.View.Details;
            this.treeListView.VirtualMode = true;
            this.treeListView.ItemActivate += new System.EventHandler(this.treeListView_ItemActivate);
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "Name";
            this.olvColumn1.IsTileViewColumn = true;
            this.olvColumn1.Text = "Name";
            this.olvColumn1.UseInitialLetterForGroup = true;
            this.olvColumn1.Width = 316;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "Host";
            this.olvColumn2.Text = "Host PC";
            this.olvColumn2.Width = 68;
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "Uuid";
            this.olvColumn3.Text = "UUID";
            this.olvColumn3.Width = 113;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "help.png");
            this.imageList1.Images.SetKeyName(1, "computer.png");
            this.imageList1.Images.SetKeyName(2, "monitor_window.png");
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel,
            this.toolStripSpringLabel,
            this.toolStripProgressBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 462);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 25);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(53, 20);
            this.toolStripStatusLabel.Text = "Ready.";
            // 
            // toolStripSpringLabel
            // 
            this.toolStripSpringLabel.Name = "toolStripSpringLabel";
            this.toolStripSpringLabel.Size = new System.Drawing.Size(578, 20);
            this.toolStripSpringLabel.Spring = true;
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(150, 19);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 74);
            this.tabControl.Name = "tabControl";
            this.tabControl.Padding = new System.Drawing.Point(6, 7);
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(800, 388);
            this.tabControl.TabIndex = 4;
            this.tabControl.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl_Selected);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.treeListView);
            this.tabPage1.Location = new System.Drawing.Point(4, 33);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(792, 351);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Master Directory";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.splitContainer1);
            this.tabPage2.Controls.Add(this.panel1);
            this.tabPage2.Location = new System.Drawing.Point(4, 33);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(792, 351);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Database";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Query:";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 32);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.collectionsList);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView);
            this.splitContainer1.Size = new System.Drawing.Size(786, 316);
            this.splitContainer1.SplitterDistance = 262;
            this.splitContainer1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Collections:";
            // 
            // dataGridView
            // 
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(0, 0);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowTemplate.Height = 24;
            this.dataGridView.Size = new System.Drawing.Size(520, 316);
            this.dataGridView.TabIndex = 0;
            this.dataGridView.CellValuePushed += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.dataGridView_CellValuePushed);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.queryText);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(786, 29);
            this.panel1.TabIndex = 6;
            // 
            // queryText
            // 
            this.queryText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.queryText.Location = new System.Drawing.Point(60, 3);
            this.queryText.Name = "queryText";
            this.queryText.Size = new System.Drawing.Size(726, 22);
            this.queryText.TabIndex = 4;
            this.queryText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.queryText_KeyDown);
            // 
            // collectionsList
            // 
            this.collectionsList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.collectionsList.FormattingEnabled = true;
            this.collectionsList.ItemHeight = 16;
            this.collectionsList.Location = new System.Drawing.Point(0, 17);
            this.collectionsList.Name = "collectionsList";
            this.collectionsList.Size = new System.Drawing.Size(262, 299);
            this.collectionsList.TabIndex = 3;
            this.collectionsList.SelectedIndexChanged += new System.EventHandler(this.collectionsList_SelectedIndexChanged);
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Menu;
            this.ClientSize = new System.Drawing.Size(800, 487);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.mainToolstrip);
            this.Controls.Add(this.mainMenu);
            this.MainMenuStrip = this.mainMenu;
            this.Name = "MainFrm";
            this.Text = "VM Manager";
            this.Load += new System.EventHandler(this.MainFrm_Load);
            this.mainToolstrip.ResumeLayout(false);
            this.mainToolstrip.PerformLayout();
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStrip mainToolstrip;
        private ToolStripButton toolbtnStartVm;
        private ToolStripButton toolbtnPauseVm;
        private ToolStripButton toolbtnStopVm;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem viewToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private BrightIdeasSoftware.TreeListView treeListView;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel;
        private ToolStripProgressBar toolStripProgressBar;
        private ToolStripStatusLabel toolStripSpringLabel;
        private TabControl tabControl;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private DataGridView dataGridView;
        private SplitContainer splitContainer1;
        private Label label1;
        private Label label2;
        private TextBox queryText;
        private ToolStripButton addServerToolButton;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton toolStripButton2;
        private ToolStripButton toolStripButton3;
        private ToolStripButton toolStripButton4;
        private ToolStripButton toolStripButton5;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton toolStripButton6;
        private Panel panel1;
        private ImageList imageList1;
        private ListBox collectionsList;
    }
}