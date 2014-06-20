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
        #region member 
        private double colorModulator;
        private int timerInterval;
        private uint defaultTimerInterval;
        #endregion

        #region constructors
        public Frm_MathArtsPropertiesDialog(uint _defaultTimerInterval, uint _timerInterval = 200, bool _defaultTimer = true, double _colorModulator = 1.0)
        {
            InitializeComponent();
            colorModulator = (int)_colorModulator;
            Tb_ColorModulator.Text = _colorModulator.ToString();

            defaultTimerInterval = _defaultTimerInterval;
            timerInterval = (int)_timerInterval;
            Chb_DefaultTimer.Checked = _defaultTimer;

            Tb_TimerModulator.Text = Chb_DefaultTimer.Checked ? _defaultTimerInterval.ToString() : _timerInterval.ToString();
        }
        #endregion

        #region Events + Delegates
        public delegate void MathArtsPropertiesEventHandler(object sender, MathArtsPropertiesEventArgs e);
        public event MathArtsPropertiesEventHandler PropertiesChanged;
        #endregion

        private void Tb_ColorModulator_TextChanged(object sender, EventArgs e)
        {
            if (Tb_ColorModulator.Text == "") colorModulator = 1;
            else
            {
                Double.TryParse(Tb_ColorModulator.Text, out colorModulator);
                if (colorModulator < Trb_ColorModulator.Minimum) colorModulator = Trb_ColorModulator.Minimum;
                else if (colorModulator > Trb_ColorModulator.Maximum) colorModulator = Trb_ColorModulator.Maximum;
            }

            Trb_ColorModulator.Value = (int)colorModulator;
            if (PropertiesChanged != null) PropertiesChanged(this, new MathArtsPropertiesEventArgs((uint)colorModulator, (uint)timerInterval, Chb_DefaultTimer.Checked, MathArtsPropertiesEventArgs.ChangeTypes.ColorModulator));
        }

        private void Trb_ColorModulator_ValueChanged(object sender, EventArgs e)
        {
            Tb_ColorModulator.Text = Trb_ColorModulator.Value.ToString();
        }

        private void Tb_ColorModulator_KeyPress(object sender, KeyPressEventArgs e)
        {
            uint currentValue;
            UInt32.TryParse(Tb_ColorModulator.Text + e.KeyChar, out currentValue);
            if ((!char.IsControl(e.KeyChar) && (!char.IsDigit(e.KeyChar) || currentValue > Trb_ColorModulator.Maximum))) e.Handled = true;
        }

        private void Tb_ColorModulator_Leave(object sender, EventArgs e)
        {
            Tb_ColorModulator.Text = colorModulator.ToString();
        }

        private void Chb_DefaultTimer_CheckedChanged(object sender, EventArgs e)
        {
            Tb_TimerModulator.ReadOnly = Chb_DefaultTimer.Checked? true:false;
            Trb_TimerModulator_ValueChanged(this, e);
            if (PropertiesChanged != null) PropertiesChanged(this, new MathArtsPropertiesEventArgs((uint)colorModulator, (uint)timerInterval, Chb_DefaultTimer.Checked, MathArtsPropertiesEventArgs.ChangeTypes.Timer));
        }

        private void Tb_TimerModulator_TextChanged(object sender, EventArgs e)
        {

            if (Tb_TimerModulator.Text == "") timerInterval = (int)defaultTimerInterval;
            else
            {
                Int32.TryParse(Tb_TimerModulator.Text, out timerInterval);
                if (timerInterval < Trb_TimerModulator.Minimum)
                {
                    timerInterval = Trb_TimerModulator.Minimum;
                    Tb_TimerModulator.Text = Trb_TimerModulator.Minimum.ToString();
                }
                else if (timerInterval > Trb_TimerModulator.Maximum)
                {
                    timerInterval = Trb_TimerModulator.Maximum;
                    Tb_TimerModulator.Text = Trb_TimerModulator.Maximum.ToString();
                }
            }
            Trb_TimerModulator.Value = timerInterval;
            if (PropertiesChanged != null) PropertiesChanged(this, new MathArtsPropertiesEventArgs((uint)colorModulator, (uint)timerInterval, Chb_DefaultTimer.Checked, MathArtsPropertiesEventArgs.ChangeTypes.Timer));
        }

        private void Trb_TimerModulator_ValueChanged(object sender, EventArgs e)
        {
            if (Chb_DefaultTimer.Checked)
            {
                Tb_TimerModulator.Text = defaultTimerInterval.ToString();
                Trb_TimerModulator.Value = (int)defaultTimerInterval;
            }
            else
            {
                Tb_TimerModulator.Text = Trb_TimerModulator.Value.ToString();
            }
        }

        private void Tb_TimerModulator_KeyPress(object sender, KeyPressEventArgs e)
        {
            uint currentValue;
            UInt32.TryParse(Tb_ColorModulator.Text + e.KeyChar, out currentValue);
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) || currentValue > Trb_TimerModulator.Maximum) e.Handled = true;
        }
    }

    /// <summary>
    /// math arts property changed event args
    /// </summary>
    public class MathArtsPropertiesEventArgs : EventArgs
    {
        private bool useDefaultTimer;

        public bool UseDefaultTimer
        {
            get { return useDefaultTimer; }
            set { useDefaultTimer = value; }
        }

        private uint colorModulator;

        public uint ColorModulator
        {
            get { return colorModulator; }
            set { colorModulator = value; }
        }

        private uint timerInterval;

        public uint TimerInterval
        {
            get { return timerInterval; }
            set { timerInterval = value; }
        }

        private ChangeTypes changeType;

        public ChangeTypes ChangeType
        {
            get { return changeType; }
            set { changeType = value; }
        }

        public MathArtsPropertiesEventArgs(uint _colorModulator, uint _timerVal,bool _useDefaultTimer,ChangeTypes _changeType)
        {
            this.colorModulator = _colorModulator;
            this.timerInterval = _timerVal;
            this.useDefaultTimer = _useDefaultTimer;
            this.changeType = _changeType;
        }

        public enum ChangeTypes
        {
            ColorModulator,Timer
        }
    }
}
