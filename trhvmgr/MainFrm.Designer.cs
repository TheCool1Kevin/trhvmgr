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
            this.addBaseToolButton = new System.Windows.Forms.ToolStripButton();
            this.addTemplateToolButton = new System.Windows.Forms.ToolStripButton();
            this.deployToolButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.startToolButton = new System.Windows.Forms.ToolStripButton();
            this.suspendToolButton = new System.Windows.Forms.ToolStripButton();
            this.stopToolButton = new System.Windows.Forms.ToolStripButton();
            this.powerToolButton = new System.Windows.Forms.ToolStripButton();
            this.saveToolButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.connectToolButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolBtnRefreshCol = new System.Windows.Forms.ToolStripButton();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inspectDiskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newTemplateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.logoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startVmmsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopVmmsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.treeListView = new BrightIdeasSoftware.TreeListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn5 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripSpringLabel = new System.Windows.Forms.ToolStripStatusLabel();
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
            this.mainToolstrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addServerToolButton,
            this.addBaseToolButton,
            this.addTemplateToolButton,
            this.deployToolButton,
            this.toolStripSeparator1,
            this.startToolButton,
            this.suspendToolButton,
            this.stopToolButton,
            this.powerToolButton,
            this.saveToolButton,
            this.toolStripSeparator2,
            this.connectToolButton,
            this.toolStripSeparator5,
            this.toolBtnRefreshCol});
            this.mainToolstrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.mainToolstrip.Location = new System.Drawing.Point(0, 24);
            this.mainToolstrip.Name = "mainToolstrip";
            this.mainToolstrip.Padding = new System.Windows.Forms.Padding(4);
            this.mainToolstrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.mainToolstrip.Size = new System.Drawing.Size(718, 33);
            this.mainToolstrip.TabIndex = 1;
            // 
            // addServerToolButton
            // 
            this.addServerToolButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addServerToolButton.Image = global::trhvmgr.Properties.Resources.computer__plus;
            this.addServerToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addServerToolButton.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.addServerToolButton.Name = "addServerToolButton";
            this.addServerToolButton.Padding = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.addServerToolButton.Size = new System.Drawing.Size(25, 25);
            this.addServerToolButton.Text = "Add New Server";
            this.addServerToolButton.Click += new System.EventHandler(this.addServerToolButton_Click);
            // 
            // addBaseToolButton
            // 
            this.addBaseToolButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addBaseToolButton.Image = global::trhvmgr.Properties.Resources.application__plus;
            this.addBaseToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addBaseToolButton.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.addBaseToolButton.Name = "addBaseToolButton";
            this.addBaseToolButton.Padding = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.addBaseToolButton.Size = new System.Drawing.Size(25, 25);
            this.addBaseToolButton.Text = "Create Base VM";
            this.addBaseToolButton.Click += new System.EventHandler(this.addBaseToolButton_Click);
            // 
            // addTemplateToolButton
            // 
            this.addTemplateToolButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addTemplateToolButton.Image = global::trhvmgr.Properties.Resources.blueprint__plus;
            this.addTemplateToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addTemplateToolButton.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.addTemplateToolButton.Name = "addTemplateToolButton";
            this.addTemplateToolButton.Padding = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.addTemplateToolButton.Size = new System.Drawing.Size(25, 25);
            this.addTemplateToolButton.Text = "New Template VM";
            this.addTemplateToolButton.Click += new System.EventHandler(this.addTemplateToolButton_Click);
            // 
            // deployToolButton
            // 
            this.deployToolButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.deployToolButton.Enabled = false;
            this.deployToolButton.Image = global::trhvmgr.Properties.Resources.application_export;
            this.deployToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deployToolButton.Name = "deployToolButton";
            this.deployToolButton.Size = new System.Drawing.Size(23, 22);
            this.deployToolButton.Text = "Deploy VM";
            this.deployToolButton.Click += new System.EventHandler(this.deployToolButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // startToolButton
            // 
            this.startToolButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.startToolButton.Enabled = false;
            this.startToolButton.Image = global::trhvmgr.Properties.Resources.control;
            this.startToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.startToolButton.Name = "startToolButton";
            this.startToolButton.Size = new System.Drawing.Size(23, 22);
            this.startToolButton.Text = "Start Virtual Machine";
            this.startToolButton.Click += new System.EventHandler(this.startToolButton_Click);
            // 
            // suspendToolButton
            // 
            this.suspendToolButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.suspendToolButton.Enabled = false;
            this.suspendToolButton.Image = global::trhvmgr.Properties.Resources.control_pause;
            this.suspendToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.suspendToolButton.Name = "suspendToolButton";
            this.suspendToolButton.Size = new System.Drawing.Size(23, 22);
            this.suspendToolButton.Text = "Suspend Virtual Machine";
            this.suspendToolButton.Click += new System.EventHandler(this.suspendToolButton_Click);
            // 
            // stopToolButton
            // 
            this.stopToolButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.stopToolButton.Enabled = false;
            this.stopToolButton.Image = global::trhvmgr.Properties.Resources.control_stop_square;
            this.stopToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stopToolButton.Name = "stopToolButton";
            this.stopToolButton.Size = new System.Drawing.Size(23, 22);
            this.stopToolButton.Text = "Stop Virtual Machine";
            this.stopToolButton.Click += new System.EventHandler(this.stopToolButton_Click);
            // 
            // powerToolButton
            // 
            this.powerToolButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.powerToolButton.Enabled = false;
            this.powerToolButton.Image = global::trhvmgr.Properties.Resources.control_power;
            this.powerToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.powerToolButton.Name = "powerToolButton";
            this.powerToolButton.Size = new System.Drawing.Size(23, 22);
            this.powerToolButton.Text = "Shutdown Virtual Machine";
            this.powerToolButton.Click += new System.EventHandler(this.powerToolButton_Click);
            // 
            // saveToolButton
            // 
            this.saveToolButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveToolButton.Enabled = false;
            this.saveToolButton.Image = global::trhvmgr.Properties.Resources.disk_return;
            this.saveToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolButton.Name = "saveToolButton";
            this.saveToolButton.Size = new System.Drawing.Size(23, 22);
            this.saveToolButton.Text = "Save Virtual Machine State";
            this.saveToolButton.Click += new System.EventHandler(this.saveToolButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // connectToolButton
            // 
            this.connectToolButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.connectToolButton.Enabled = false;
            this.connectToolButton.Image = ((System.Drawing.Image)(resources.GetObject("connectToolButton.Image")));
            this.connectToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.connectToolButton.Name = "connectToolButton";
            this.connectToolButton.Size = new System.Drawing.Size(23, 22);
            this.connectToolButton.Text = "Connect to Virtual Machine";
            this.connectToolButton.Click += new System.EventHandler(this.connectToolButton_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // toolBtnRefreshCol
            // 
            this.toolBtnRefreshCol.Image = global::trhvmgr.Properties.Resources.page_refresh;
            this.toolBtnRefreshCol.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnRefreshCol.Name = "toolBtnRefreshCol";
            this.toolBtnRefreshCol.Padding = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.toolBtnRefreshCol.Size = new System.Drawing.Size(92, 22);
            this.toolBtnRefreshCol.Text = "Refresh List";
            this.toolBtnRefreshCol.Click += new System.EventHandler(this.toolBtnRefreshCol_Click);
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
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.inspectDiskToolStripMenuItem,
            this.addServerToolStripMenuItem,
            this.newTemplateToolStripMenuItem,
            this.refreshListToolStripMenuItem,
            this.toolStripSeparator4,
            this.logoutToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // inspectDiskToolStripMenuItem
            // 
            this.inspectDiskToolStripMenuItem.Image = global::trhvmgr.Properties.Resources.magnifier;
            this.inspectDiskToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.inspectDiskToolStripMenuItem.Name = "inspectDiskToolStripMenuItem";
            this.inspectDiskToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.inspectDiskToolStripMenuItem.Text = "Inspect Disk";
            this.inspectDiskToolStripMenuItem.Click += new System.EventHandler(this.inspectDiskToolStripMenuItem_Click);
            // 
            // addServerToolStripMenuItem
            // 
            this.addServerToolStripMenuItem.Image = global::trhvmgr.Properties.Resources.computer__plus;
            this.addServerToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.addServerToolStripMenuItem.Name = "addServerToolStripMenuItem";
            this.addServerToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.addServerToolStripMenuItem.Text = "Add Server";
            this.addServerToolStripMenuItem.Click += new System.EventHandler(this.addServerToolStripMenuItem_Click);
            // 
            // newTemplateToolStripMenuItem
            // 
            this.newTemplateToolStripMenuItem.Image = global::trhvmgr.Properties.Resources.blueprint__plus;
            this.newTemplateToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.newTemplateToolStripMenuItem.Name = "newTemplateToolStripMenuItem";
            this.newTemplateToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.newTemplateToolStripMenuItem.Text = "New Template";
            this.newTemplateToolStripMenuItem.Click += new System.EventHandler(this.newTemplateToolStripMenuItem_Click);
            // 
            // refreshListToolStripMenuItem
            // 
            this.refreshListToolStripMenuItem.Image = global::trhvmgr.Properties.Resources.page_refresh;
            this.refreshListToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.refreshListToolStripMenuItem.Name = "refreshListToolStripMenuItem";
            this.refreshListToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.refreshListToolStripMenuItem.Text = "Refresh List";
            this.refreshListToolStripMenuItem.Click += new System.EventHandler(this.refreshListToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(147, 6);
            // 
            // logoutToolStripMenuItem
            // 
            this.logoutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("logoutToolStripMenuItem.Image")));
            this.logoutToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.logoutToolStripMenuItem.Name = "logoutToolStripMenuItem";
            this.logoutToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.logoutToolStripMenuItem.Text = "Logout";
            this.logoutToolStripMenuItem.Click += new System.EventHandler(this.logoutToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startVmmsToolStripMenuItem,
            this.stopVmmsToolStripMenuItem,
            this.toolStripSeparator3});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.editToolStripMenuItem.Text = "&Action";
            // 
            // startVmmsToolStripMenuItem
            // 
            this.startVmmsToolStripMenuItem.Enabled = false;
            this.startVmmsToolStripMenuItem.Name = "startVmmsToolStripMenuItem";
            this.startVmmsToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.startVmmsToolStripMenuItem.Text = "Start vmms service";
            this.startVmmsToolStripMenuItem.Click += new System.EventHandler(this.startVmmsToolStripMenuItem_Click);
            // 
            // stopVmmsToolStripMenuItem
            // 
            this.stopVmmsToolStripMenuItem.Enabled = false;
            this.stopVmmsToolStripMenuItem.Name = "stopVmmsToolStripMenuItem";
            this.stopVmmsToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.stopVmmsToolStripMenuItem.Text = "Stop vmms service";
            this.stopVmmsToolStripMenuItem.Click += new System.EventHandler(this.stopVmmsToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(170, 6);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.settingsToolStripMenuItem.Text = "Application Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
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
            this.treeListView.AllColumns.Add(this.olvColumn4);
            this.treeListView.AllColumns.Add(this.olvColumn5);
            this.treeListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeListView.CellEditUseWholeCell = false;
            this.treeListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2,
            this.olvColumn3,
            this.olvColumn4,
            this.olvColumn5});
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
            this.treeListView.Size = new System.Drawing.Size(708, 331);
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
            this.treeListView.SelectedIndexChanged += new System.EventHandler(this.treeListView_SelectedIndexChanged);
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
            this.olvColumn3.DisplayIndex = 4;
            this.olvColumn3.FillsFreeSpace = true;
            this.olvColumn3.Text = "UUID";
            this.olvColumn3.Width = 100;
            // 
            // olvColumn4
            // 
            this.olvColumn4.AspectName = "VmType";
            this.olvColumn4.DisplayIndex = 2;
            this.olvColumn4.IsEditable = false;
            this.olvColumn4.Text = "VMType";
            // 
            // olvColumn5
            // 
            this.olvColumn5.AspectName = "State";
            this.olvColumn5.DisplayIndex = 3;
            this.olvColumn5.Text = "State";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "question-white.png");
            this.imageList1.Images.SetKeyName(1, "monitor-network.png");
            this.imageList1.Images.SetKeyName(2, "applications-blue.png");
            this.imageList1.Images.SetKeyName(3, "drive-medium.png");
            this.imageList1.Images.SetKeyName(4, "drive--exclamation.png");
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel,
            this.toolStripSpringLabel});
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
            this.toolStripSpringLabel.Size = new System.Drawing.Size(665, 17);
            this.toolStripSpringLabel.Spring = true;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 57);
            this.tabControl.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl.Name = "tabControl";
            this.tabControl.Padding = new System.Drawing.Point(6, 7);
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(718, 365);
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
            this.tabPage1.Size = new System.Drawing.Size(710, 331);
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
            this.tabPage2.Size = new System.Drawing.Size(710, 331);
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
            this.splitContainer1.Size = new System.Drawing.Size(706, 303);
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
            this.collectionsList.Size = new System.Drawing.Size(235, 290);
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
            this.dataGridView.Size = new System.Drawing.Size(468, 303);
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
        private ToolStripButton addBaseToolButton;
        private ToolStripButton addTemplateToolButton;
        private Panel panel1;
        private ImageList imageList1;
        private ListBox collectionsList;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton toolBtnRefreshCol;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton startToolButton;
        private ToolStripButton stopToolButton;
        private ToolStripButton saveToolButton;
        private BrightIdeasSoftware.OLVColumn olvColumn4;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private BrightIdeasSoftware.OLVColumn olvColumn5;
        private ToolStripMenuItem stopVmmsToolStripMenuItem;
        private ToolStripMenuItem inspectDiskToolStripMenuItem;
        private ToolStripButton deployToolButton;
        private ToolStripMenuItem startVmmsToolStripMenuItem;
        private ToolStripMenuItem addServerToolStripMenuItem;
        private ToolStripMenuItem newTemplateToolStripMenuItem;
        private ToolStripMenuItem logoutToolStripMenuItem;
        private ToolStripMenuItem refreshListToolStripMenuItem;
        private ToolStripButton powerToolButton;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripButton suspendToolButton;
        private ToolStripButton connectToolButton;
        private ToolStripSeparator toolStripSeparator5;
    }
}