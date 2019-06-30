using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using trhvmgr.Lib;

namespace trhvmgr.UI
{
    public partial class BackgroundWorkerQueueDialog : Form
    {
        private BackgroundWorkerQueue queue;
        public BackgroundWorkerQueueDialog(string title)
        {
            InitializeComponent();
            this.Text = title;
            queue = new BackgroundWorkerQueue(this.backgroundWorker1);
            backgroundWorker1.ProgressChanged += BackgroundWorker1_ProgressChanged;
            progressBar1.Maximum = 100;
            progressBar1.Value = 0;
            backgroundWorker1.RunWorkerCompleted += BackgroundWorker1_RunWorkerCompleted;
        }

        #region Public Methods

        /// <summary>
        /// Adds a task to the end of the queue.
        /// </summary>
        /// <param name="description">Description of task will be displayed beneath the progress bar.</param>
        /// <param name="f">The function should take in a WorkerContext and return a WorkerContext with a modified status code.</param>
        public void AppendTask(string description, Func<WorkerContext, WorkerContext> f)
        {
            queue.AppendTask(description, f);
        }

        /// <summary>
        /// Gets the BackgroundWorkerQueue "backend."
        /// </summary>
        /// <returns>The BackgroundWorkerQueue associated with this dialog.</returns>
        public BackgroundWorkerQueue GetWorker()
        {
            return queue;
        }

        #endregion

        #region UI Events

        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
        }

        private void BackgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            label1.Text = queue.GetStatusText();
            progressBar1.Value = (int)((queue.GetCurrentTask() * 100.0 + e.ProgressPercentage) / (float) queue.GetTaskCount());
        }

        private void BackgroundWorkerQueueDialog_Shown(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }

        #endregion
    }
}
