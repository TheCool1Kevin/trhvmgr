using BrightIdeasSoftware;
using LiteDB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using trhvmgr.Database;
using trhvmgr.Lib;
using trhvmgr.Objects;
using trhvmgr.Plugs;
using trhvmgr.UI;

namespace trhvmgr
{
    public partial class MainFrm : Form
    {
        private DatabaseManager databaseManager;
        private readonly int CollectionsResultLimit = 100;

        public MainFrm()
        {
            // UI Initialization
            InitializeComponent();
            this.mainMenu.Renderer = new MainFormMenuStripRenderer();
            this.mainToolstrip.Renderer = new MainFormToolStripRenderer();
            // Database
            databaseManager = SessionManager.GetDatabase();
        }

        #region Public and Private Methods

        internal void SetStatus(string status)
        {
            ThreadManager.Invoke(this, statusStrip, () => this.toolStripStatusLabel.Text = status);
        }

        private void RefreshCollections(WorkerContext ctx = null)
        {
            try
            {
                if (ctx == null)
                    databaseManager.FlushCache();
                else
                {
                    databaseManager.FlushCache(PsStreamEventHandlers.GetUIHandlers(ctx));
                    ctx.s = StatusCode.OK;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Exception", MessageBoxButtons.OK);
                if(ctx != null) ctx.s = StatusCode.FAILED;
                return;
            }
            RefreshUI();
        }

        private void RefreshUI()
        {
            ThreadManager.Invoke(this, collectionsList, () => collectionsList.Items.Clear());
            databaseManager.GetCollectionNames().ToList().ForEach(x =>
                ThreadManager.Invoke(this, collectionsList, () => collectionsList.Items.Add(x)));
            treeListView.SetObjects(databaseManager.Directory.Values);
        }

        private void RefreshBackground()
        {
            SetStatus("Busy.");
            new BackgroundWorkerQueueDialog("Loading servers...", ProgressBarStyle.Marquee)
                    .AppendTask("Connecting machines...", DummyWorker.GetWorker((ctx) => RefreshCollections(ctx)))
                    .ShowDialog();
            SetStatus("Ready.");
        }

        private void ShowAddServerDialog()
        {
            new AddServerDialog().ShowDialog();
            RefreshUI();
        }

        private void ShowAddTemplateDialog()
        {
            new AddTemplateDialog().ShowDialog();
            RefreshBackground();
        }

        private void ShowAddBaseDialog()
        {
            new AddBaseDialog().ShowDialog();
            RefreshUI();
        }

        private void ShowDeployDialog()
        {
            new DeployDialog((treeListView.SelectedObject as MasterTreeNode)?.Name).ShowDialog();
            RefreshBackground();
        }

        private void ShowAppSettingsDialog()
        {
            if(new AppSettingsDialog().ShowDialog() != DialogResult.Cancel)
                RefreshBackground();
        }

        private void ShowInspectDiskDialog()
        {
            var fileDialog = new BrowseVhdDialog();
            if (fileDialog.ShowDialog() == DialogResult.OK)
                new InspectDiskDialog(fileDialog.ComputerName, fileDialog.FileName).ShowDialog();
        }

        private void ShowServerPropertyDialog()
        {
            new ServerProperties((treeListView.SelectedObject as MasterTreeNode)?.Name).ShowDialog();
            RefreshBackground();
        }

        private void StartVmms(string host)
        {
            SetStatus("Busy.");
            new BackgroundWorkerQueueDialog("Starting service...")
                .AppendTask("Starting vmms...", DummyWorker.GetWorker((ctx) =>
                    Interface.StartService(host, "vmms", PsStreamEventHandlers.GetUIHandlers(ctx))
                )).ShowDialog();
            Thread.Sleep(1000);
            SetStatus("Ready.");
        }

        private void StopVmms(string host)
        {
            SetStatus("Busy.");
            new BackgroundWorkerQueueDialog("Stopping service...")
                .AppendTask("Stopping vmms...", DummyWorker.GetWorker((ctx) =>
                    Interface.StopService(host, "vmms", PsStreamEventHandlers.GetUIHandlers(ctx))
                )).ShowDialog();
            Thread.Sleep(1000);
            SetStatus("Ready.");
        }

        private DialogResult GetAuthentication()
        {
            var dlg = new StartupDialog();
            return dlg.ShowDialog();
        }

        private void SetBase(MasterTreeNode node)
        {
            if (node.Type != NodeType.VirtualMachines || node.VmType?.Value != VirtualMachineType.NONE)
                return;
            try
            {
                HyperV.CheckpointVm(node.Host, node.Name, "Base Checkpoint DNR");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Exception", MessageBoxButtons.OK);
            }
            databaseManager.SetVmType(DbVirtualMachine.FromTreeNode(node), VirtualMachineType.BASE);
            RefreshUI();
        }

        private void UnsetBase(MasterTreeNode node)
        {
            if (node.Type != NodeType.VirtualMachines || node.VmType?.Value != VirtualMachineType.BASE)
                return;
            if (MessageBox.Show(
                "This action may corrupt all existing templates of this base VM. Data loss may occur. Are you sure you want to continue?",
                "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Hand) == DialogResult.No)
                return;
            try
            {
                HyperV.RestoreVmSnapshot(node.Host, node.Name, "Base Checkpoint DNR");
                HyperV.RemoveVmSnapshot(node.Host, node.Name, "Base Checkpoint DNR");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Exception", MessageBoxButtons.OK);
            }
            databaseManager.SetVmType(DbVirtualMachine.FromTreeNode(node), VirtualMachineType.NONE);
            RefreshUI();
        }

        #endregion

        #region Tree Code

        private void SetupMasterTree()
        {
            this.treeListView.CanExpandGetter = (x) =>
            {
                return ((MasterTreeNode)x).Children.Count > 0;
            };

            this.treeListView.ChildrenGetter = (x) =>
            {
                return ((MasterTreeNode)x).Children;
            };

            this.olvColumn1.ImageGetter = (x) =>
            {
                if (((MasterTreeNode)x).Type == NodeType.HostComputer)
                    return imageList1.Images[1];
                else if (((MasterTreeNode)x).Type == NodeType.VirtualMachines)
                    return imageList1.Images[2];
                else if (((MasterTreeNode)x).Type == NodeType.VirtualHardDisks)
                    return imageList1.Images[3];
                else if (((MasterTreeNode)x).Type == NodeType.OrphanedVirtualHardDisks)
                    return imageList1.Images[4];
                return imageList1.Images[0];
            };

            this.treeListView.UseCellFormatEvents = true;
            this.treeListView.FormatCell += (o, e) =>
            {
                if(e.ColumnIndex == this.olvColumn1.Index)
                {
                    MasterTreeNode node = (MasterTreeNode) e.Model;
                    if (node.VmType?.Value == VirtualMachineType.BASE)
                        e.SubItem.Font = new Font(e.SubItem.Font, FontStyle.Bold);
                    else if (node.VmType?.Value == VirtualMachineType.TEMPLATE)
                        e.SubItem.Font = new Font(e.SubItem.Font, FontStyle.Italic);
                    else if(node.VmType?.Value == VirtualMachineType.NONE)
                        e.SubItem.ForeColor = Color.Gray;
                }
            };

            this.treeListView.Roots = databaseManager.Directory.Values;

            // TODO: High DPI Awareness
            TreeListView.TreeRenderer renderer = this.treeListView.TreeColumnRenderer;
            renderer.LinePen = new Pen(Color.Firebrick, 0.5f);
            renderer.LinePen.DashStyle = DashStyle.Dot;
            renderer.IsShowGlyphs = true;
            renderer.UseTriangles = true;
            //TreeListView.TreeRenderer.PIXELS_PER_LEVEL = 13;
            renderer.CellPadding = new Rectangle(0, 2, 0, 0);
            renderer.IsShowLines = false;
            this.treeListView.Refresh();
        }

        #endregion

        #region Menu Getters

        private ContextMenuStrip GetContextMenu(object model, OLVColumn col)
        {
            if(model is MasterTreeNode)
            {
                var node = model as MasterTreeNode;
                var menu = new ContextMenuStrip();
                if (node.Type == NodeType.HostComputer)
                {
                    menu.Items.Add("Delete").Click += (o, e) =>
                    {
                        databaseManager.RemoveServer(node.Name);
                        RefreshUI();
                    };
                    menu.Items.Add("Start vmms service").Click += (o, e) => StartVmms(node.Name);
                    menu.Items.Add("Stop vmms service").Click += (o, e) => StopVmms(node.Name);
                    menu.Items.Add("Properties").Click += (o, e) => ShowServerPropertyDialog();
                    return menu;
                }
                else if(node.Type == NodeType.VirtualMachines)
                {
                    if (node.VmType.Value == VirtualMachineType.BASE)
                    {
                        var menuItem1 = menu.Items.Add("Unset as Base");
                        ((ToolStripMenuItem)menuItem1).Checked = true;
                        menuItem1.Click += (o, e) => UnsetBase(node);
                    }
                    else if(node.VmType.Value == VirtualMachineType.NONE)
                        menu.Items.Add("Set as Base").Click += (o, e) => SetBase(node);

                    menu.Items.Add("Delete").Click += (o, e) =>
                    {
                        if (MessageBox.Show("This action is irreversible. Do you want to continue?", "Delete Virtual Machine",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Hand) == DialogResult.No)
                            return;
                        HyperV.RemoveVm(node.Host, node.Name);
                        databaseManager.RemoveVm(Guid.Parse(node.Uuid));
                        RefreshBackground();
                    };

                    menu.Items.Add("Copy UUID").Click += (o, e) => Clipboard.SetText(node.Uuid);

                    return menu;
                }
                else if(node.Type == NodeType.OrphanedVirtualHardDisks)
                {
                    menu.Items.Add("Delete").Click += (o, e) =>
                    {
                        Interface.DeleteItem(node.Host, node.Name);
                        RefreshBackground();
                    };
                    menu.Items.Add("Inspect...").Click += (o, e) =>
                        new InspectDiskDialog(node.Host, node.Name).ShowDialog();
                    menu.Items.Add("Copy Path").Click += (o, e) => Clipboard.SetText(node.Name);
                    return menu;
                }
                else if(node.Type == NodeType.VirtualHardDisks)
                {
                    menu.Items.Add("Inspect...").Click += (o, e) =>
                        new InspectDiskDialog(node.Host, node.Name).ShowDialog();
                    menu.Items.Add("Copy Path").Click += (o, e) => Clipboard.SetText(node.Name);
                    return menu;
                }
                menu = null;
            }
            return null;
        }

        #endregion

        #region UI Events

        private void MainFrm_Load(object sender, EventArgs e)
        {
            if (GetAuthentication() != DialogResult.OK)
                this.Close();

            // Initialize tree
            SetupMasterTree();
            var backgroundWorker = new BackgroundWorkerQueueDialog("Loading servers...", ProgressBarStyle.Marquee);
            backgroundWorker.AppendTask("Connecting machines...", DummyWorker.GetWorker(() => RefreshCollections()));
            backgroundWorker.ShowDialog(FormStartPosition.CenterScreen);
        }

        private void treeListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(treeListView.SelectedItem == null || treeListView.SelectedItem.RowObject == null || !(treeListView.SelectedItem.RowObject is MasterTreeNode))
                return;
            var obj = treeListView.SelectedItem.RowObject as MasterTreeNode;
            if(obj.Type == NodeType.HostComputer)
            {
                startVmmsToolStripMenuItem.Enabled = true;
                stopVmmsToolStripMenuItem.Enabled = true;
            }
            else
            {
                startVmmsToolStripMenuItem.Enabled = false;
                stopVmmsToolStripMenuItem.Enabled = false;
            }

            if(obj.VmType?.Value == VirtualMachineType.TEMPLATE)
                deployToolButton.Enabled = true;
            else
                deployToolButton.Enabled = false;

            if(obj.Type == NodeType.VirtualMachines)
            {
                if (obj.State.vs == VirtualMachineState.Running)
                {
                    stopToolButton.Enabled = true; powerToolButton.Enabled = true;
                    saveToolButton.Enabled = true; startToolButton.Enabled = false;
                    suspendToolButton.Enabled = true;
                }
                else
                {
                    stopToolButton.Enabled = false; powerToolButton.Enabled = false;
                    saveToolButton.Enabled = false; startToolButton.Enabled = true;
                    suspendToolButton.Enabled = false;
                }
                connectToolButton.Enabled = true;
            }
            else
            {
                stopToolButton.Enabled = false; powerToolButton.Enabled = false;
                saveToolButton.Enabled = false; startToolButton.Enabled = false;
                suspendToolButton.Enabled = false;

                connectToolButton.Enabled = false;
            }
        }

        private void collectionsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (collectionsList.SelectedItem != null && !collectionsList.SelectedItem.Equals("[QUERY]"))
            {
                FillDataGridView(databaseManager.GetCollection(collectionsList.SelectedItem.ToString()).
                    Find(Query.All(), 0, CollectionsResultLimit));
                queryText.Text = $@"db.{collectionsList.SelectedItem}.find limit {CollectionsResultLimit}";
            }
        }

        private void queryText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                RunQuery(queryText.Text);
                if (!collectionsList.Items.Contains("[QUERY]"))
                {
                    collectionsList.Items.Add("[QUERY]");
                }
                collectionsList.SelectedItem = "[QUERY]";
            }
        }

        private void dataGridView_CellValuePushed(object sender, DataGridViewCellValueEventArgs e)
        {
            RefreshCollections();
        }

        private void treeListView_ItemActivate(object sender, EventArgs e)
        {
            object model = this.treeListView.SelectedObject;
            if (model != null)
                this.treeListView.ToggleExpansion(model);
        }

        private void tabControl_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPageIndex == 1)
                MessageBox.Show("Unstable feature!", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }

        private void treeListView_CellRightClick(object sender, CellRightClickEventArgs e)
        {
            e.MenuStrip = this.GetContextMenu(e.Model, e.Column);
        }

        #endregion

        #region UI Toolstrip Button Events

        private void addServerToolButton_Click(object sender, EventArgs e) => ShowAddServerDialog();

        private void addTemplateToolButton_Click(object sender, EventArgs e) => ShowAddTemplateDialog();

        private void addBaseToolButton_Click(object sender, EventArgs e) => ShowAddBaseDialog();

        private void toolBtnRefreshCol_Click(object sender, EventArgs e) => RefreshBackground();

        private void deployToolButton_Click(object sender, EventArgs e) => ShowDeployDialog();

        #endregion

        #region UI Menu Button Events

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e) => ShowAppSettingsDialog();

        private void inspectDiskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var vhd = treeListView.SelectedObject as MasterTreeNode;
            if (vhd.Type == NodeType.VirtualHardDisks || vhd.Type == NodeType.OrphanedVirtualHardDisks)
                new InspectDiskDialog(vhd.Host, vhd.Name).ShowDialog();
            else
                ShowInspectDiskDialog();
        }

        private void startVmmsToolStripMenuItem_Click(object sender, EventArgs e) => StartVmms((treeListView.SelectedObject as MasterTreeNode)?.Name);

        private void stopVmmsToolStripMenuItem_Click(object sender, EventArgs e) => StopVmms((treeListView.SelectedObject as MasterTreeNode)?.Name);

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            GetAuthentication();
            RefreshBackground();
        }

        private void addServerToolStripMenuItem_Click(object sender, EventArgs e) => ShowAddServerDialog();

        private void newTemplateToolStripMenuItem_Click(object sender, EventArgs e) => ShowAddTemplateDialog();

        private void refreshListToolStripMenuItem_Click(object sender, EventArgs e) => RefreshBackground();

        private void startToolButton_Click(object sender, EventArgs e)
        {
            var node = treeListView.SelectedObject as MasterTreeNode;
            if (node == null) return;
            BackgroundWorkerQueueDialog bg = new BackgroundWorkerQueueDialog("Setting machine state...");
            if (node.State?.vs == VirtualMachineState.Paused)
                bg.AppendTask("", DummyWorker.GetWorker((ctx) => HyperV.ResumeVm(node.Host, node.Name, PsStreamEventHandlers.GetUIHandlers(ctx))));
            else
                bg.AppendTask("", DummyWorker.GetWorker((ctx) => HyperV.StartVm(node.Host, node.Name, PsStreamEventHandlers.GetUIHandlers(ctx))));
            bg.ShowDialog();
            RefreshBackground();
        }

        private void powerToolButton_Click(object sender, EventArgs e)
        {
            var node = treeListView.SelectedObject as MasterTreeNode;
            if (node == null) return;
            new BackgroundWorkerQueueDialog("Setting machine state...")
                .AppendTask("", DummyWorker.GetWorker((ctx) =>
                    HyperV.StopVm(node.Host, node.Name, false, PsStreamEventHandlers.GetUIHandlers(ctx))
                )).ShowDialog();
            RefreshBackground();
        }

        private void stopToolButton_Click(object sender, EventArgs e)
        {
            var node = treeListView.SelectedObject as MasterTreeNode;
            if (node == null) return;
            new BackgroundWorkerQueueDialog("Setting machine state...")
                .AppendTask("", DummyWorker.GetWorker((ctx) =>
                    HyperV.StopVm(node.Host, node.Name, true, PsStreamEventHandlers.GetUIHandlers(ctx))
                )).ShowDialog();
            RefreshBackground();
        }

        private void saveToolButton_Click(object sender, EventArgs e)
        {
            var node = treeListView.SelectedObject as MasterTreeNode;
            if (node == null) return;
            new BackgroundWorkerQueueDialog("Setting machine state...")
                .AppendTask("", DummyWorker.GetWorker((ctx) =>
                    HyperV.SaveVm(node.Host, node.Name, PsStreamEventHandlers.GetUIHandlers(ctx))
                )).ShowDialog();
            RefreshBackground();
        }

        private void suspendToolButton_Click(object sender, EventArgs e)
        {
            var node = treeListView.SelectedObject as MasterTreeNode;
            if (node == null) return;
            new BackgroundWorkerQueueDialog("Setting machine state...")
                .AppendTask("", DummyWorker.GetWorker((ctx) =>
                    HyperV.SuspendVm(node.Host, node.Name, PsStreamEventHandlers.GetUIHandlers(ctx))
                )).ShowDialog();
            RefreshBackground();
        }

        private void connectToolButton_Click(object sender, EventArgs e)
        {
            var node = treeListView.SelectedObject as MasterTreeNode;
            if (node == null || node.Type != NodeType.VirtualMachines) return;
            string path = Path.Combine(".\\Third-Party", "vmconnect.exe");
            Process vmConnection = new Process();
            vmConnection.StartInfo.FileName = path;
            vmConnection.StartInfo.Arguments = $"\"{node.Host}\" \"{node.Name}\"";
            //vmConnection.StartInfo.Verb = "runas";
            vmConnection.Start();
        }

        #endregion

        // TODO: Database code really broken :(
        #region Database Code

        // Modified from falahati/LiteDBViewer
        private void FillDataGridView(IEnumerable<BsonDocument> documents)
        {
            if (collectionsList.Items.Contains("[QUERY]"))
                collectionsList.Items.Remove("[QUERY]");
            dataGridView.DataSource = null;
            var ds = new LiteDataTable();
            if (documents == null)
                return;
            foreach (var doc in documents)
            {
                var row = ds.NewRow() as LiteDataRow;
                if (row == null) continue;
                row.UnderlyingValue = doc;
                foreach (var prop in doc.RawValue)
                {
                    if (prop.Value.IsMaxValue || prop.Value.IsMinValue)
                        continue;
                    if (!ds.Columns.Contains(prop.Key))
                        ds.Columns.Add(new DataColumn(prop.Key, typeof(string)));
                    switch (prop.Value.Type)
                    {
                        case BsonType.Null:
                            row[prop.Key] = "[NULL]";
                            break;
                        case BsonType.Document:
                            row[prop.Key] = prop.Value.AsDocument.RawValue.ContainsKey("_type")
                                ? $"[OBJECT: {prop.Value.AsDocument.RawValue["_type"]}]"
                                : "[OBJECT]";
                            break;
                        case BsonType.Array:
                            row[prop.Key] = $"[ARRAY({prop.Value.AsArray.Count})]";
                            break;
                        case BsonType.Binary:
                            row[prop.Key] = $"[BINARY({prop.Value.AsBinary.Length})]";
                            break;
                        case BsonType.DateTime:
                            row[prop.Key] = prop.Value.AsDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
                            break;
                        case BsonType.String:
                            row[prop.Key] = prop.Value.AsString;
                            break;
                        case BsonType.Int32:
                        case BsonType.Int64:
                            row[prop.Key] = prop.Value.AsInt64.ToString();
                            break;
                        case BsonType.Decimal:
                        case BsonType.Double:
                            row[prop.Key] = prop.Value.AsDecimal.ToString(CultureInfo.InvariantCulture);
                            break;
                        default:
                            row[prop.Key] = prop.Value.ToString();
                            break;
                    } // End switch
                } // End foreach
                ds.Rows.Add(row);
            } // End foreach

            // Attatch Event Handlers
            ds.RowChanging += Ds_RowChanging;
            ds.ColumnChanging += Ds_ColumnChanging;
            ds.RowDeleting += Ds_RowDeleting;
            ds.TableNewRow += Ds_TableNewRow;
            dataGridView.DataSource = ds;
        }

        private void Ds_TableNewRow(object sender, DataTableNewRowEventArgs e)
        {
            RefreshCollections();
        }

        private void Ds_RowDeleting(object sender, DataRowChangeEventArgs e)
        {
            RefreshCollections();
        }

        private void Ds_ColumnChanging(object sender, DataColumnChangeEventArgs e)
        {
            BsonDocument doc = ((LiteDataRow)e.Row).UnderlyingValue;
            if (doc == null) return;
            if (e.Column.ColumnName == "_id")
            {
                databaseManager.GetCollection(collectionsList.SelectedItem.ToString()).Delete(doc["_id"]);
                doc[e.Column.ColumnName] = new BsonValue(e.ProposedValue);
                databaseManager.GetCollection(collectionsList.SelectedItem.ToString()).Insert(doc);
            }
            else
            {
                doc[e.Column.ColumnName] = new BsonValue(e.ProposedValue);
                databaseManager.GetCollection(collectionsList.SelectedItem.ToString()).Update(doc);
            }
            RefreshCollections();
        }

        private void Ds_RowChanging(object sender, DataRowChangeEventArgs e)
        {
            if(e.Action == DataRowAction.Add)
            {
                BsonDocument doc = new BsonDocument();
                for (int i = 0; i < e.Row.Table.Columns.Count; i++)
                    doc[e.Row.Table.Columns[i].ColumnName] = "";
                if(collectionsList.SelectedItem != null)
                    databaseManager.GetCollection(collectionsList.SelectedItem.ToString()).Insert(doc);
            }
            else if(e.Action == DataRowAction.Delete)
            {
                databaseManager.GetCollection(collectionsList.SelectedItem.ToString()).Delete(((LiteDataRow)e.Row).UnderlyingValue["_id"]);
            }
            RefreshCollections();
        }

        private void RunQuery(string query)
        {
            try
            {
                queryText.Text = query;
                FillDataGridView(null);
                var results = databaseManager.EngineRun(query);
                RefreshCollections();
                FillDataGridView(
                    results.Select(
                        item =>
                            item.IsDocument
                                ? item.AsDocument
                                : new BsonDocument(new Dictionary<string, BsonValue> { { "[Result]", item } })));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Bad Query", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            RefreshCollections();
        }

        #endregion
    }
}
