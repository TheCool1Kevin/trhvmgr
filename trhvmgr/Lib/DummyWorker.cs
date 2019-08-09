using System;

namespace trhvmgr.Lib
{
    public class DummyWorker
    {
        public static Func<WorkerContext, WorkerContext> GetWorker(Action action) => (ctx) =>
        {
            action.Invoke();
            return ctx;
        };

        public static Func<WorkerContext, WorkerContext> GetWorker(Func<WorkerContext, WorkerContext> action) => (ctx) =>
        {
            return action.Invoke(ctx);
        };
    }
}
