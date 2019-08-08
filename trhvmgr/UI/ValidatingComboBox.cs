using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using trhvmgr.Lib;

namespace trhvmgr.UI
{
    public partial class ValidatingComboBox : UserControl
    {
        #region UI Properties and Events

        [Description("Label text displayed"), Category("Label")]
        public string LabelText
        {
            get { return label1.Text; }
            set { label1.Text = value; }
        }

        [Description("Label text width"), Category("Label")]
        public int LabelWidth
        {
            get { return label1.Width; }
            set { label1.Width = value; }
        }

        [Description("Label text auto size"), Category("Label")]
        public bool LabelAutoSize
        {
            get { return label1.AutoSize; }
            set { label1.AutoSize = value; }
        }

        [Description("Button visible"), Category("Label")]
        public bool ButtonVisible
        {
            get { return button1.Visible; }
            set { button1.Visible = value; }
        }

        [Description("Button image"), Category("Label")]
        public Image ButtonImage
        {
            get { return button1.Image; }
            set { button1.Image = value; }
        }

        [Description("Button tooltip title text"), Category("Label")]
        public string ButtonTooltipTitle
        {
            get { return toolTip1.ToolTipTitle; }
            set { toolTip1.ToolTipTitle = value; }
        }

        [Description("Button tooltip text"), Category("Label")]
        public string ButtonTooltip
        {
            get; set;
        }

        [Description("Error tooltip title text"), Category("Label")]
        public string ErrorTooltipTitle
        {
            get { return toolTip2.ToolTipTitle; }
            set { toolTip2.ToolTipTitle = value; }
        }

        [Description("Error tooltip text"), Category("Label")]
        public string ErrorTooltip
        {
            get; set;
        }

        [Description("Textbox text"), Category("Label")]
#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
        public int SelectedIndex
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword
        {
            get { return comboBox1.SelectedIndex; }
            set { comboBox1.SelectedIndex = value; }
        }

        [Description("Error image"), Category("Label")]
        public Image ErrorImage
        {
            get; set;
        }

        private bool _enabled = true;
        [Description("Enabled"), Category("Label")]
        public new bool Enabled
        {
            get
            {
                return _enabled;
            }

            set
            {
                _enabled = value;
                this.button1.Enabled = _enabled;
                this.comboBox1.Enabled = _enabled;
                this.pictureBox1.Enabled = _enabled;
            }
        }

        [Description("Button clicked event"), Category("Action")]
        public event EventHandler ButtonClick;

        #endregion

        #region Constructor

        public ValidatingComboBox()
        {
            InitializeComponent();
            pictureBox1.Image = null;
            comboBox1.BorderColor = Color.Black;
        }

        #endregion

        #region Global Variables

        public ColorComboBox ComboBox
        {
            get
            {
                return comboBox1;
            }

            private set
            {
                comboBox1 = value;
            }
        }

        private tribool _isvalid = tribool.NEUTRAL;
        public tribool IsValid
        {
            get { return _isvalid; }
            set
            {
                _isvalid = value;
                if (_isvalid == tribool.TRUE)
                {
                    pictureBox1.Image = null;
                    comboBox1.BorderColor = Color.Green;
                    this.Refresh();
                }
                else if(_isvalid == tribool.NEUTRAL)
                {
                    pictureBox1.Image = null;
                    comboBox1.BorderColor = Color.Black;
                    this.Refresh();
                }
                else
                {
                    pictureBox1.Image = ErrorImage;
                    comboBox1.BorderColor = Color.Red;
                    this.Refresh();
                }
            }
        }

        #endregion

        #region UI Events

        private void button1_Click(object sender, EventArgs e)
        {
            this.ButtonClick?.Invoke(this, e);
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            this.toolTip1.Show(this.ButtonTooltip, button1, 30, 0);
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            if(_isvalid == tribool.FALSE)
                this.toolTip2.Show(this.ErrorTooltip, pictureBox1, 30, 0);
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            this.toolTip1.Hide(button1);
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            this.toolTip2.Hide(pictureBox1);
        }

        #endregion
    }
}
