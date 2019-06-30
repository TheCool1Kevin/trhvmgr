using LiteDB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using trhvmgr.Database;
using trhvmgr.Objects;

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

            // Database Initialization
            databaseManager = new DatabaseManager();
            RefreshCollections();
        }

        #region Tree Code

        private void SetupMasterTree()
        {
            this.treeListView.CanExpandGetter = delegate (object x)
            {
                return ((MasterTreeNode)x).Children.Count > 0;
            };

            this.treeListView.ChildrenGetter = delegate (object x)
            {
                return ((MasterTreeNode) x).Children;
            };

            this.treeListView.Roots = databaseManager.TreeNodes;
        }

        #endregion

        #region UI Events

        private void MainFrm_Load(object sender, EventArgs e)
        {

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

        }

        #endregion

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
            dataGridView.DataSource = ds;
        }

        private void Ds_RowDeleting(object sender, DataRowChangeEventArgs e)
        {
            databaseManager.GetCollection(collectionsList.SelectedItem.ToString()).Delete(((LiteDataRow)e.Row).UnderlyingValue["_id"].AsObjectId);
        }

        private void Ds_ColumnChanging(object sender, DataColumnChangeEventArgs e)
        {
            BsonDocument doc = ((LiteDataRow)e.Row).UnderlyingValue;
            doc[e.Column.ColumnName] = new BsonValue(e.ProposedValue);
            databaseManager.GetCollection(collectionsList.SelectedItem.ToString()).Update(doc);
        }

        private void Ds_RowChanging(object sender, DataRowChangeEventArgs e)
        {
            
        }

        private void RefreshCollections()
        {
            databaseManager.RegenerateTree();
            collectionsList.Items.Clear();
            databaseManager.GetCollectionNames().ToList().ForEach(x => collectionsList.Items.Add(x));
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
        }

        #endregion

        private void addServerToolButton_Click(object sender, EventArgs e)
        {
            new AddServerDialog().ShowDialog();
        }
    }
}
