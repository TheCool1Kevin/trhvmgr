using LiteDB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trhvmgr.Database
{
    /// <summary>
    /// Class for displaying a LiteDB row inside of a DataGrid control.
    /// </summary>
    [Serializable]
    public class LiteDataRow : DataRow
    {
        public LiteDataRow(DataRowBuilder builder) : base(builder) { }
        internal BsonDocument UnderlyingValue { get; set; }
    }
}
