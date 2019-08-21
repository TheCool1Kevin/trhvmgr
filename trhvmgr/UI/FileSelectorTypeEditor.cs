using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace trhvmgr.UI
{
    /// <summary>
    /// Customer UITypeEditor that pops up a
    /// file selector dialog
    /// </summary>
    public class JsonFileSelectorTypeEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            if (context == null || context.Instance == null)
                return base.GetEditStyle(context);
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService editorService;

            if (context == null || context.Instance == null || provider == null)
                return value;

            try
            {
                // get the editor service, just like in windows forms
                editorService = (IWindowsFormsEditorService)
                   provider.GetService(typeof(IWindowsFormsEditorService));

                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
                dlg.CheckFileExists = true;

                string filename = (string)value;
                if (!File.Exists(filename))
                    filename = null;
                dlg.FileName = filename;

                using (dlg)
                {
                    DialogResult res = dlg.ShowDialog();
                    if (res == DialogResult.OK)
                    {
                        filename = dlg.FileName;
                    }
                }
                return filename;

            }
            finally
            {
                editorService = null;
            }
        }
    } // class FileSelectorTypeEditor

} // namespace Winterdom.Design.TypeEditors