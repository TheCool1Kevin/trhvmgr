using BrightIdeasSoftware;
using LiteDB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using trhvmgr.Database;
using trhvmgr.Lib;
using trhvmgr.Objects;
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
            databaseManager = SessionManager.Instance.Database;
        }

        #region Tree Code

        private void RefreshCollections()
        {
            try
            {
                databaseManager.RegenerateTree();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Exception", MessageBoxButtons.OK);
                return;
            }
            RefreshUI();
        }

        private void RefreshUI()
        {
            collectionsList.Items.Clear();
            databaseManager.GetCollectionNames().ToList().ForEach(x => collectionsList.Items.Add(x));
            treeListView.SetObjects(databaseManager.TreeNodes);
        }

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
                return imageList1.Images[0];
            };

            this.treeListView.Roots = databaseManager.TreeNodes;

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

        private ContextMenuStrip GetContextMenu(object model, OLVColumn col)
        {
            if(model is MasterTreeNode)
            {
                var node = model as MasterTreeNode;
                if(node.Type == NodeType.HostComputer)
                {
                    var menu = new ContextMenuStrip();
                    menu.Items.Add("Delete").Click += (o, e) =>
                        {
                            SessionManager.Instance.Database.RemoveServer(node.Name);
                            RefreshUI();
                        };
                    return menu;
                }
            }

            return null;
        }

        #endregion

        #region UI Events

        private void MainFrm_Load(object sender, EventArgs e)
        {
            // Get auth
            var dlg = new StartupDialog();
            dlg.ShowDialog();
            if (dlg.DialogResult != DialogResult.OK)
                this.Close();

            // Initialize tree
            SetupMasterTree();
            var backgroundWorker = new BackgroundWorkerQueueDialog("Loading servers...", ProgressBarStyle.Marquee);
            backgroundWorker.AppendTask("Connecting machines...", DummyWorker.GetWorker(RefreshCollections));
            backgroundWorker.ShowDialog(FormStartPosition.CenterScreen);
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

        #region UI Button Events

        private void addServerToolButton_Click(object sender, EventArgs e)
        {
            new AddServerDialog().ShowDialog();
            treeListView.SetObjects(databaseManager.TreeNodes);
        }

        private void addTemplateToolButton_Click(object sender, EventArgs e)
        {
            new AddTemplateDialog().ShowDialog();
            treeListView.SetObjects(databaseManager.TreeNodes);
        }

        private void addVhdToolButton_Click(object sender, EventArgs e)
        {
            new AddVhdDialog().ShowDialog();
            treeListView.SetObjects(databaseManager.TreeNodes);
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
