using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MathArts
{
    public partial class Frm_MathArtsPropertiesDialog : Form
    {
        #region constructors
        public Frm_MathArtsPropertiesDialog(uint _colorModulator=1)
        {
            InitializeComponent();
            tb_ColorModulator.Text = _colorModulator.ToString();
        }
        #endregion

        #region Events + Delegates
        public delegate void MathArtsPropertiesEventHandler(object sender, MathArtsPropertiesEventArgs e);
        public event MathArtsPropertiesEventHandler PropertiesChanged;
        #endregion

        private void tb_ColorModulator_TextChanged(object sender, EventArgs e)
        {
            if (tb_ColorModulator.Text == "") tb_ColorModulator.Text = "1"; 
            int currentValue;
            Int32.TryParse(tb_ColorModulator.Text, out currentValue);
            trb_ColorModulator.Value = currentValue;
            if (PropertiesChanged != null) PropertiesChanged(this, new MathArtsPropertiesEventArgs((uint)currentValue));
        }

        private void trb_ColorModulator_ValueChanged(object sender, EventArgs e)
        {
            tb_ColorModulator.Text = trb_ColorModulator.Value.ToString();
        }

        private void tb_ColorModulator_KeyPress(object sender, KeyPressEventArgs e)
        {
            uint currentValue;
            UInt32.TryParse(tb_ColorModulator.Text + e.KeyChar, out currentValue);
            //if (currentValue > trb_ColorModulator.Maximum && currentValue < trb_ColorModulator.Minimum) 
            if ((!char.IsControl(e.KeyChar) && (!char.IsDigit(e.KeyChar) || currentValue > trb_ColorModulator.Maximum || currentValue < trb_ColorModulator.Minimum))
                || (char.IsControl(e.KeyChar) && tb_ColorModulator.Text.Length==1)) e.Handled = true;
        }
    }

    /// <summary>
    /// Function value changed event args
    /// </summary>
    public class MathArtsPropertiesEventArgs : EventArgs
    {
        private uint colorModulator;

        public uint ColorModulator
        {
            get { return colorModulator; }
            set { colorModulator = value; }
        }

        public MathArtsPropertiesEventArgs(uint _colorModulator)
        {
            this.colorModulator = _colorModulator;
        }

    }
}
