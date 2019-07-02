using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace trhvmgr.UI
{
    /// <summary>
    /// ComboBox with a colored border.
    /// </summary>
    public class ColorComboBox : ComboBox
    {
        /// <summary>
        /// Color of border.
        /// </summary>
        public Color BorderColor { get; set; }

        public ColorComboBox()
        {
            this.DoubleBuffered = true;
            this.Padding = new Padding(2);
        }

        private const int WM_PAINT = 0xF;
        private int buttonWidth = SystemInformation.HorizontalScrollBarArrowWidth;
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_PAINT)
            {
                using (var g = Graphics.FromHwnd(Handle))
                {
                    // Uncomment this if you don't want the "highlight border".
                    
                    using (var p = new Pen(this.BorderColor, 1))
                    {
                        g.DrawRectangle(p, 0, 0, Width - 1, Height - 1);
                    }
                    /*using (var p = new Pen(this.BorderColor, 1))
                    {
                        g.DrawRectangle(p, 2, 2, Width - buttonWidth - 4, Height - 4);
                    }*/
                }
            }
        }
    }
}
