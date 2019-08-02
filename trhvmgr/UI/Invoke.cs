using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace trhvmgr.UI
{
    public class ThreadManager
    {
        public static void Invoke(Form w, Control c, Action a)
        {
            if (c.InvokeRequired)
                w.Invoke(a);
            else
                a();
        }
    }
}
