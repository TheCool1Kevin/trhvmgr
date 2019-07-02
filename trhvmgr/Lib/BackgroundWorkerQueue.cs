using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;

namespace trhvmgr.Lib
{
    /// <summary>
    /// Delegate wrapper for the BackgroundWorker object. Exposes
    /// the ReportProgress() function to the current running task.
    /// </summary>
    public class WorkerDelegate
    {
        private BackgroundWorker _w;

        /// <summary>
        /// Constructor for WorkerDelegate.
        /// </summary>
        /// <param name="w">BackgroundWorker object</param>
        public WorkerDelegate(BackgroundWorker w)
        {
            _w = w;
        }

        /// <summary>
        /// Reports a percent from 0 to 100 - the percent
        /// of the task that has been completed.
        /// </summary>
        /// <param name="percent">Percent of the task out of 100</param>
        public void ReportProgress(float percent)
        {
            _w.ReportProgress((int) (percent));
        }
    }

    /// <summary>
    /// All default status codes for tasks to return.
    /// Note codes 0-10 are reserved here.
    /// </summary>
    public enum StatusCode
    {
        /// <summary>
        /// Task returns OK status.
        /// </summary>
        OK = 1,
        /// <summary>
        /// Task failed to complete execution.
        /// </summary>
        FAILED = 2
    };

    /// <summary>
    /// Context passed to tasks and returned from tasks.
    /// </summary>
    public class WorkerContext : ICloneable
    {
        /// <summary>
        /// Status code from the previous task. Use non-standard
        /// status codes at your own risk.
        /// </summary>
        public int s;
        
        /// <summary>
        /// Object passed down from previous task.
        /// </summary>
        public ICloneable o;

        /// <summary>
        /// WorkerDelegate passed down by BackgroundWorkerQueue.
        /// </summary>
        public readonly WorkerDelegate d;

        /// <summary>
        /// Constructor for WorkerContext.
        /// </summary>
        /// <param name="status">Status code from the previous task. Use non-standard
        /// status codes at your own risk.</param>
        /// <param name="obj">Object passed down from previous task; must be ICloneable.</param>
        /// <param name="del">WorkerDelegate passed down by BackgroundWorkerQueue.</param>
        public WorkerContext(int status, ICloneable obj, WorkerDelegate del)
        {
            s = status;
            o = obj;
            d = del;
        }

        public object Clone()
        {
            var c = this.MemberwiseClone();
            ((WorkerContext)c).o = (ICloneable) o.Clone();
            return c;
        }
    }

    /// <summary>
    /// Allows the creation of BackgroundWorker queues for multiple tasks.
    /// Call the RunWorkerAsync() of the BackgroundWorker associated with
    /// this BackgroundWorkerQueue to start execution.
    /// </summary>
    public class BackgroundWorkerQueue
    {
        private List<Func<WorkerContext, WorkerContext>> _tasks = new List<Func<WorkerContext, WorkerContext>>();
        private List<string> _texts = new List<string>();
        private int _ntasks = 0;
        private BackgroundWorker _w;
        private int _i = 0;

        private void _w_DoWork(object sender, DoWorkEventArgs e)
        {
            WorkerContext res = new WorkerContext((int)StatusCode.OK, null, new WorkerDelegate(_w));
            for (_i = 0; _i < _ntasks; _i++)
            {
                _w.ReportProgress(0);
                res = _tasks[_i].Invoke(res);
                ReturnedObjects.Add((WorkerContext)res.Clone());
                _w.ReportProgress(100);
                Thread.Sleep(500); // 0.5s sleep
            }
        }

        /// <summary>
        /// Returned objects from each task.
        /// </summary>
        public List<WorkerContext> ReturnedObjects { get; private set; }

        /// <summary>
        /// Creates a BackgroundWorkerQueue from a BackgroundWorker.
        /// </summary>
        /// <param name="w">BackgroundWorker to create from.</param>
        public BackgroundWorkerQueue(BackgroundWorker w)
        {
            _w = w;
            _w.DoWork += _w_DoWork;
            ReturnedObjects = new List<WorkerContext>();
        }

        /// <summary>
        /// Adds a new task to the end of the worker queue.
        /// </summary>
        /// <param name="description">Text to display (if any).</param>
        /// <param name="f">Worker function to run, taking in a WorkerContext and returning a WorkerContext</param>
        public void AppendTask(string description, Func<WorkerContext, WorkerContext> f)
        {
            _tasks.Add(f);
            _texts.Add(description);
            _ntasks++;
        }

        public string GetStatusText()
        {
            if (_i >= _texts.Count)
                return _texts[_texts.Count - 1];
            return _texts[_i];
        }

        public int GetCurrentTask()
        {
            return _i;
        }

        public int GetTaskCount()
        {
            return _ntasks;
        }
    }
}
