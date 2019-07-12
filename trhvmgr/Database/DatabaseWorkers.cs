using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trhvmgr.Lib;

namespace trhvmgr.Database
{
    public class DatabaseWorkers
    {
        public static Func<WorkerContext, WorkerContext> GetPcWorker() => (ctx) =>
        {
            return ctx;
        };
    }
}
