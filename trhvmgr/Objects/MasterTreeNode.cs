using System;
using System.Collections.Generic;

namespace trhvmgr.Objects
{
    public enum NodeType
    {
        Other = 0,
        HostComputer,
        VirtualMachines,
        VirtualHardDisks,
        OrphanedVirtualHardDisks
    };

    public class NodeState
    {
        private NodeType id = NodeType.Other;
        public VirtualMachineState vs;
        public HostState hs;
        public NodeState(VirtualMachineState st)
        {
            id = NodeType.VirtualMachines;
            vs = st;   
        }
        public NodeState(HostState st)
        {
            id = NodeType.HostComputer;
            hs = st;
        }
        public NodeState()
        {
            id = NodeType.Other;
        }
        public override string ToString()
        {
            if (id == NodeType.HostComputer) return hs.ToString();
            if (id == NodeType.VirtualMachines) return vs.ToString();
            return "";
        }
    }

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
        public string IpAddress { get; set; }

        public NodeVirtualMachineType VmType { get; set; }

        public NodeType Type { get; set; }
        public NodeState State { get; set; }

        public List<MasterTreeNode> Children = new List<MasterTreeNode>();

        public string ParentHost { get; set; }
        public Guid ParentUuid { get; set; }

        public MasterTreeNode()
        {
            State = new NodeState();
        }

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
            State = new NodeState(v.State),
            Type = NodeType.VirtualMachines,
            VmType = new NodeVirtualMachineType(v.Type),
            ParentHost = v.ParentHost,
            ParentUuid = v.ParentUuid
        };

        [Obsolete("Use explicit casts instead")]
        public static MasterTreeNode GetTreeNode(DbHostComputer h) => (MasterTreeNode) h;
        public static explicit operator MasterTreeNode(DbHostComputer h) => new MasterTreeNode
        {
            Name = h.HostName,
            State = new NodeState(HostState.Unknown),
            Type = NodeType.HostComputer
        };

        [Obsolete("Use explicit casts instead")]
        public static MasterTreeNode GetTreeNode(HostComputer h) => (MasterTreeNode)h;
        public static explicit operator MasterTreeNode(HostComputer h) => new MasterTreeNode
        {
            Name = h.HostName,
            State = new NodeState(h.State),
            Type = NodeType.HostComputer
        };

        public static MasterTreeNode GetTreeNode(string file, string host, NodeType type)
        {
            return new MasterTreeNode
            {
                Name = file,
                Host = host,
                Type = type,
                State = new NodeState()
            };
        }

        #endregion
    }
}
