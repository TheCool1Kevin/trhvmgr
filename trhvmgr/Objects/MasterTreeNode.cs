using System.Collections.Generic;

namespace trhvmgr.Objects
{
    public enum NodeType
    {
        HostComputer,
        VirtualMachines,
        VirtualHardDisks
    };

    /// <summary>
    /// Object for the Master View TreeListView
    /// </summary>
    public class MasterTreeNode
    {
        public string Name { get; set; }
        public string Host { get; set; }
        public string Uuid { get; set; }

        public NodeType Type { get; set; }
        public List<MasterTreeNode> Children = new List<MasterTreeNode>();

        public void BurnChildren()
        {
            foreach (var c in Children) c.BurnChildren();
            Children.Clear();
        }

        #region Public Static Methods

        public static MasterTreeNode GetTreeNode(VirtualMachine v)
        {
            return new MasterTreeNode
            {
                Name = v.Name,
                Host = v.Host,
                Uuid = v.Uuid.ToString().ToUpper(),
                Type = NodeType.VirtualMachines,
            };
        }

        public static MasterTreeNode GetTreeNode(DbHostComputer h)
        {
            return new MasterTreeNode
            {
                Name = h.HostName,
                Type = NodeType.HostComputer
            };
        }

        public static MasterTreeNode GetTreeNode(string file, string host, NodeType type)
        {
            return new MasterTreeNode
            {
                Name = file,
                Host = host,
                Type = type
            };
        }

        #endregion
    }
}
