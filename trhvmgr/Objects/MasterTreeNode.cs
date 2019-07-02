using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trhvmgr.Objects
{
    public enum NodeType
    {
        HostComputer,
        VirtualMachines
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
    }
}
