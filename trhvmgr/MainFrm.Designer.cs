using System.Drawing;
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
                //ColorTable.ToolStripGradientBegin,
                Color.White,
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
            this.addVhdToolButton = new System.Windows.Forms.ToolStripButton();
            this.addTemplateToolButton = new System.Windows.Forms.ToolStripButton();
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
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripSpringLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.collectionsList = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.queryText = new System.Windows.Forms.TextBox();
            this.mainToolstrip.SuspendLayout();
            this.mainMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView)).BeginInit();
            this.statusStrip.SuspendLayout();
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
            this.mainToolstrip.ImageScalingSize = new System.Drawing.Size(25, 25);
            this.mainToolstrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addServerToolButton,
            this.addVhdToolButton,
            this.addTemplateToolButton,
            this.toolStripSeparator1,
            this.toolbtnStartVm,
            this.toolbtnPauseVm,
            this.toolbtnStopVm,
            this.toolStripSeparator2,
            this.toolStripButton3,
            this.toolStripButton5,
            this.toolStripButton4});
            this.mainToolstrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.mainToolstrip.Location = new System.Drawing.Point(0, 24);
            this.mainToolstrip.Name = "mainToolstrip";
            this.mainToolstrip.Padding = new System.Windows.Forms.Padding(4);
            this.mainToolstrip.Size = new System.Drawing.Size(718, 41);
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
            this.addServerToolButton.Size = new System.Drawing.Size(33, 33);
            this.addServerToolButton.Text = "Add New Server";
            this.addServerToolButton.Click += new System.EventHandler(this.addServerToolButton_Click);
            // 
            // addVhdToolButton
            // 
            this.addVhdToolButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addVhdToolButton.Image = ((System.Drawing.Image)(resources.GetObject("addVhdToolButton.Image")));
            this.addVhdToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addVhdToolButton.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.addVhdToolButton.Name = "addVhdToolButton";
            this.addVhdToolButton.Padding = new System.Windows.Forms.Padding(2);
            this.addVhdToolButton.Size = new System.Drawing.Size(33, 33);
            this.addVhdToolButton.Text = "Import VHD";
            this.addVhdToolButton.Click += new System.EventHandler(this.addVhdToolButton_Click);
            // 
            // addTemplateToolButton
            // 
            this.addTemplateToolButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addTemplateToolButton.Image = ((System.Drawing.Image)(resources.GetObject("addTemplateToolButton.Image")));
            this.addTemplateToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addTemplateToolButton.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.addTemplateToolButton.Name = "addTemplateToolButton";
            this.addTemplateToolButton.Padding = new System.Windows.Forms.Padding(2);
            this.addTemplateToolButton.Size = new System.Drawing.Size(33, 33);
            this.addTemplateToolButton.Text = "New Template VM";
            this.addTemplateToolButton.Click += new System.EventHandler(this.addTemplateToolButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 33);
            // 
            // toolbtnStartVm
            // 
            this.toolbtnStartVm.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolbtnStartVm.Image = ((System.Drawing.Image)(resources.GetObject("toolbtnStartVm.Image")));
            this.toolbtnStartVm.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolbtnStartVm.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.toolbtnStartVm.Name = "toolbtnStartVm";
            this.toolbtnStartVm.Padding = new System.Windows.Forms.Padding(2);
            this.toolbtnStartVm.Size = new System.Drawing.Size(33, 33);
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
            this.toolbtnPauseVm.Size = new System.Drawing.Size(33, 33);
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
            this.toolbtnStopVm.Size = new System.Drawing.Size(33, 33);
            this.toolbtnStopVm.Text = "Stop VM";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 33);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Padding = new System.Windows.Forms.Padding(2);
            this.toolStripButton3.Size = new System.Drawing.Size(33, 33);
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
            this.toolStripButton5.Size = new System.Drawing.Size(33, 33);
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
            this.toolStripButton4.Size = new System.Drawing.Size(33, 33);
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
            this.mainMenu.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.mainMenu.Size = new System.Drawing.Size(718, 24);
            this.mainMenu.TabIndex = 0;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
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
            this.treeListView.EmptyListMsgFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeListView.FullRowSelect = true;
            this.treeListView.Location = new System.Drawing.Point(2, 2);
            this.treeListView.MultiSelect = false;
            this.treeListView.Name = "treeListView";
            this.treeListView.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.Submenu;
            this.treeListView.ShowCommandMenuOnRightClick = true;
            this.treeListView.ShowGroups = false;
            this.treeListView.ShowImagesOnSubItems = true;
            this.treeListView.ShowItemToolTips = true;
            this.treeListView.Size = new System.Drawing.Size(708, 323);
            this.treeListView.SmallImageList = this.imageList1;
            this.treeListView.TabIndex = 3;
            this.treeListView.TileSize = new System.Drawing.Size(16, 16);
            this.treeListView.UseCompatibleStateImageBehavior = false;
            this.treeListView.UseFilterIndicator = true;
            this.treeListView.UseFiltering = true;
            this.treeListView.UseHotItem = true;
            this.treeListView.UseTranslucentHotItem = true;
            this.treeListView.UseTranslucentSelection = true;
            this.treeListView.View = System.Windows.Forms.View.Details;
            this.treeListView.VirtualMode = true;
            this.treeListView.CellRightClick += new System.EventHandler<BrightIdeasSoftware.CellRightClickEventArgs>(this.treeListView_CellRightClick);
            this.treeListView.ItemActivate += new System.EventHandler(this.treeListView_ItemActivate);
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "Name";
            this.olvColumn1.IsTileViewColumn = true;
            this.olvColumn1.Text = "Name";
            this.olvColumn1.UseInitialLetterForGroup = true;
            this.olvColumn1.Width = 317;
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
            this.olvColumn3.Width = 304;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "help.png");
            this.imageList1.Images.SetKeyName(1, "computer.png");
            this.imageList1.Images.SetKeyName(2, "monitor_window.png");
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel,
            this.toolStripSpringLabel,
            this.toolStripProgressBar});
            this.statusStrip.Location = new System.Drawing.Point(0, 422);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.statusStrip.Size = new System.Drawing.Size(718, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(42, 17);
            this.toolStripStatusLabel.Text = "Ready.";
            // 
            // toolStripSpringLabel
            // 
            this.toolStripSpringLabel.Name = "toolStripSpringLabel";
            this.toolStripSpringLabel.Size = new System.Drawing.Size(549, 17);
            this.toolStripSpringLabel.Spring = true;
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(112, 16);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 65);
            this.tabControl.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl.Name = "tabControl";
            this.tabControl.Padding = new System.Drawing.Point(6, 7);
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(718, 357);
            this.tabControl.TabIndex = 4;
            this.tabControl.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl_Selected);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.treeListView);
            this.tabPage1.Location = new System.Drawing.Point(4, 30);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage1.Size = new System.Drawing.Size(710, 323);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Master Directory";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.splitContainer1);
            this.tabPage2.Controls.Add(this.panel1);
            this.tabPage2.Location = new System.Drawing.Point(4, 30);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage2.Size = new System.Drawing.Size(710, 323);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Database";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 5);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Query:";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(2, 26);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2);
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
            this.splitContainer1.Size = new System.Drawing.Size(706, 295);
            this.splitContainer1.SplitterDistance = 235;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 3;
            // 
            // collectionsList
            // 
            this.collectionsList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.collectionsList.FormattingEnabled = true;
            this.collectionsList.Location = new System.Drawing.Point(0, 13);
            this.collectionsList.Margin = new System.Windows.Forms.Padding(2);
            this.collectionsList.Name = "collectionsList";
            this.collectionsList.Size = new System.Drawing.Size(235, 282);
            this.collectionsList.TabIndex = 3;
            this.collectionsList.SelectedIndexChanged += new System.EventHandler(this.collectionsList_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Collections:";
            // 
            // dataGridView
            // 
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(0, 0);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowTemplate.Height = 24;
            this.dataGridView.Size = new System.Drawing.Size(468, 295);
            this.dataGridView.TabIndex = 0;
            this.dataGridView.CellValuePushed += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.dataGridView_CellValuePushed);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.queryText);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(706, 24);
            this.panel1.TabIndex = 6;
            // 
            // queryText
            // 
            this.queryText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.queryText.Location = new System.Drawing.Point(45, 2);
            this.queryText.Margin = new System.Windows.Forms.Padding(2);
            this.queryText.Name = "queryText";
            this.queryText.Size = new System.Drawing.Size(662, 20);
            this.queryText.TabIndex = 4;
            this.queryText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.queryText_KeyDown);
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Menu;
            this.ClientSize = new System.Drawing.Size(718, 444);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.mainToolstrip);
            this.Controls.Add(this.mainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenu;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VM Manager";
            this.Load += new System.EventHandler(this.MainFrm_Load);
            this.mainToolstrip.ResumeLayout(false);
            this.mainToolstrip.PerformLayout();
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
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
        private StatusStrip statusStrip;
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
        private ToolStripButton addVhdToolButton;
        private ToolStripButton toolStripButton3;
        private ToolStripButton toolStripButton4;
        private ToolStripButton toolStripButton5;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton addTemplateToolButton;
        private Panel panel1;
        private ImageList imageList1;
        private ListBox collectionsList;
    }
}