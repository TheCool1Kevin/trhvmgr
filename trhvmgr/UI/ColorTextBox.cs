using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace trhvmgr.UI
{
    /// <summary>
    /// Textbox with a colored border.
    /// </summary>
    public class ColorTextBox : TextBox
    {
        /// <summary>
        /// Color of border.
        /// </summary>
        public Color BorderColor { get; set; }

        [DllImport("user32")]
        private static extern IntPtr GetWindowDC(IntPtr hwnd);
        private const int WM_NCPAINT = 0x85;
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_NCPAINT)
            {
                var dc = GetWindowDC(Handle);
                using (Graphics g = Graphics.FromHdc(dc))
                {
                    g.DrawRectangle(new Pen(BorderColor), 0, 0, Width - 1, Height - 1);
                }
            }
        }
    }
}
