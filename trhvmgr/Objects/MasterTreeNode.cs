using System;
using System.Collections.Generic;

namespace trhvmgr.Objects
{
    public enum NodeType
    {
        HostComputer,
        VirtualMachines,
        VirtualHardDisks
    };

    public class NodeVirtualMachineType
    {
        public VirtualMachineType Value { get; private set; }
        public NodeVirtualMachineType(VirtualMachineType type) { this.Value = type; }
        public override string ToString()
        {
            if(Value != VirtualMachineType.NONE)
                return Value.ToString();
            return "";
        }
    }

    /// <summary>
    /// Object for the Master View TreeListView
    /// </summary>
    public class MasterTreeNode
    {
        public string Name { get; set; }
        public string Host { get; set; }
        public string Uuid { get; set; }

        public NodeVirtualMachineType VmType { get; set; }

        public NodeType Type { get; set; }
        public List<MasterTreeNode> Children = new List<MasterTreeNode>();

        public void BurnChildren()
        {
            foreach (var c in Children) c.BurnChildren();
            Children.Clear();
        }

        #region Public Static Methods

        [Obsolete("Use explicit casts instead")]
        public static MasterTreeNode GetTreeNode(VirtualMachine v) => (MasterTreeNode) v;
        public static explicit operator MasterTreeNode(VirtualMachine v) => new MasterTreeNode
        {
            Name = v.Name,
            Host = v.Host,
            Uuid = v.Uuid.ToString().ToUpper(),
            Type = NodeType.VirtualMachines,
            VmType = new NodeVirtualMachineType(v.Type)
        };

        [Obsolete("Use explicit casts instead")]
        public static MasterTreeNode GetTreeNode(DbHostComputer h) => (MasterTreeNode) h;
        public static explicit operator MasterTreeNode(DbHostComputer h) => new MasterTreeNode
        {
            Name = h.HostName,
            Type = NodeType.HostComputer
        };

        [Obsolete("Use explicit casts instead")]
        public static MasterTreeNode GetTreeNode(HostComputer h) => (MasterTreeNode)h;
        public static explicit operator MasterTreeNode(HostComputer h) => new MasterTreeNode
        {
            Name = h.HostName,
            Type = NodeType.HostComputer
        };

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
