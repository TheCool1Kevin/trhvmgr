using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace trhvmgr.UI
{
    /// <summary>
    /// Textbox with a colored border.
    /// </summary>
    public class ColorTextBox : Panel
    {
        private TextBox textBox;

        /// <summary>
        /// Color of border.
        /// </summary>
        public Color BorderColor { get; set; }

        public override string Text
        {
            get { return textBox.Text; }
            set { textBox.Text = value; }
        }

        public TextBox TextBox
        {
            get { return textBox; }
            set { textBox = value; }
        }

        public ColorTextBox()
        {
            this.DoubleBuffered = true;
            this.Padding = new Padding(2);

            this.TextBox = new TextBox();
            this.TextBox.AutoSize = false;
            this.TextBox.BorderStyle = BorderStyle.None;
            this.TextBox.Dock = DockStyle.Fill;
            this.TextBox.Enter += new EventHandler(this.TextBox_Refresh);
            this.TextBox.Leave += new EventHandler(this.TextBox_Refresh);
            this.TextBox.Resize += new EventHandler(this.TextBox_Refresh);
            this.Controls.Add(this.TextBox);
        }

        private void TextBox_Refresh(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(SystemColors.Window);
            using (Pen borderPen = new Pen(BorderColor))
            {
                e.Graphics.DrawRectangle(borderPen,
                    new Rectangle(0, 0, this.ClientSize.Width - 1, this.ClientSize.Height - 1));
            }
            base.OnPaint(e);
        }
    }
}
