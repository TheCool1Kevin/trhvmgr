using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace trhvmgr.Database
{
    /// <summary>
    /// DataTable type for displaying a LiteDB database inside a DataGrid control.
    /// </summary>
    [Serializable]
    public class LiteDataTable : DataTable
    {
        public LiteDataTable() { }
        public LiteDataTable(string tableName) : base(tableName) { }
        public LiteDataTable(string tableName, string tableNamespace) : base(tableName, tableNamespace) { }
        protected LiteDataTable(SerializationInfo info, StreamingContext context) : base(info, context) { }
        protected override Type GetRowType()
        {
            return typeof(LiteDataRow);
        }

        protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
        {
            return new LiteDataRow(builder);
        }
    }
}
