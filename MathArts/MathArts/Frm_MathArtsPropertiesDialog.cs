/////////////////////////////////////////////////////////////////////////////
// <copyright file="Frm_MathArtsPropertiesDialog.cs">
// Copyright (c) 2014
// </copyright>
//
// <author>Betting Pascal, Schneider Mathias, Schlemelch Manuel</author>
// <date>22-06-2014</date>
//
// <professor>Prof. Dr. Josef Poesl</professor>
// <studyCourse>Angewandte Informatik</studyCourse>
// <branchOfStudy>Industrieinformatik</branchOfStudy>
// <subject>Oberflaechenprogrammierung</subject>
/////////////////////////////////////////////////////////////////////////////

using System;
using System.Windows.Forms;

namespace MathArts
{
    /// <summary>
    /// Property dialog for the MathArts form to change the color modulator value and/or the usage of a timer for updating the container 
    /// </summary>
    public partial class Frm_MathArtsPropertiesDialog : Form
    {
        #region member
        private double colorModulator;
        private int timerInterval;
        private uint defaultTimerInterval;
        private bool hiddenTimerProperties = true;
        #endregion

        #region constructors
        public Frm_MathArtsPropertiesDialog(uint _defaultTimerInterval, uint _timerInterval = 200, bool _defaultTimer = true,bool _useTimer = false, double _colorModulator = 1.0)
        {
            InitializeComponent();
            colorModulator = (int)_colorModulator;
            Tb_ColorModulator.Text = _colorModulator.ToString();

            defaultTimerInterval = _defaultTimerInterval;
            timerInterval = (int)_timerInterval;
            Chb_DefaultTimer.Checked = _defaultTimer;
            
            Chb_UseTimer.Checked = _useTimer;
            Chb_UseTimer_CheckedChanged(this, EventArgs.Empty);

            Tb_TimerModulator.Text = Chb_DefaultTimer.Checked ? _defaultTimerInterval.ToString() : _timerInterval.ToString();
            Tt_UseTimer.SetToolTip(this.Chb_UseTimer, "Aktualisierung der Oberfläche bei Bewegen bzw. Größenänderung der dargstellten Objekte\nnur im Rhythmus des eingestellten Zeitgeber-Intervalls");
            Tt_StandardValueUseTimer.SetToolTip(this.Chb_DefaultTimer, "Benutzung eines Zeittakt in Abhängigkeit der Fenstergröße");
        }
        #endregion

        #region Events + Delegates
        public delegate void MathArtsPropertiesEventHandler(object sender, MathArtsPropertiesEventArgs e);
        public event MathArtsPropertiesEventHandler PropertiesChanged;
        #endregion

        #region form events
        /// <summary>
        /// color modulator value in textbox changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            if (PropertiesChanged != null) PropertiesChanged(this, new MathArtsPropertiesEventArgs((uint)colorModulator, (uint)timerInterval, Chb_DefaultTimer.Checked, Chb_UseTimer.Checked, MathArtsPropertiesEventArgs.ChangeTypes.ColorModulator));
        }

        /// <summary>
        /// color modulator value in trackbar changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Trb_ColorModulator_ValueChanged(object sender, EventArgs e)
        {
            Tb_ColorModulator.Text = Trb_ColorModulator.Value.ToString();
        }

        /// <summary>
        /// Limits the handling of pressed keys to allow only useful value inside the textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tb_ColorModulator_KeyPress(object sender, KeyPressEventArgs e)
        {
            uint currentValue;
            UInt32.TryParse(Tb_ColorModulator.Text + e.KeyChar, out currentValue);
            if ((!char.IsControl(e.KeyChar) && (!char.IsDigit(e.KeyChar) || currentValue > Trb_ColorModulator.Maximum))) e.Handled = true;
        }

        /// <summary>
        /// Updates the textbox on leaving
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tb_ColorModulator_Leave(object sender, EventArgs e)
        {
            Tb_ColorModulator.Text = colorModulator.ToString();
        }

        /// <summary>
        /// Fires property changed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Chb_DefaultTimer_CheckedChanged(object sender, EventArgs e)
        {
            Trb_TimerModulator_ValueChanged(this, e);
            if (PropertiesChanged != null) PropertiesChanged(this, new MathArtsPropertiesEventArgs((uint)colorModulator, (uint)timerInterval, Chb_DefaultTimer.Checked, Chb_UseTimer.Checked, MathArtsPropertiesEventArgs.ChangeTypes.Timer));
        }

        /// <summary>
        /// timer modulator value in textbox changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            if (PropertiesChanged != null) PropertiesChanged(this, new MathArtsPropertiesEventArgs((uint)colorModulator, (uint)timerInterval, Chb_DefaultTimer.Checked, Chb_UseTimer.Checked, MathArtsPropertiesEventArgs.ChangeTypes.Timer));
        }

        /// <summary>
        /// timer modulator value in trackbar changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// adapts property dialog shape depending on usage of timer and fires property changed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Chb_UseTimer_CheckedChanged(object sender, EventArgs e)
        {
            grb_TimerProperties.Visible = Chb_UseTimer.Checked;
            if (Chb_UseTimer.Checked && hiddenTimerProperties)
            {
                grb_PaintTrigger.Height += grb_TimerProperties.Height;
                this.Height += grb_TimerProperties.Height;
                hiddenTimerProperties = false;
            }
            else
            {
                grb_PaintTrigger.Height -= grb_TimerProperties.Height;
                this.Height -= grb_TimerProperties.Height;
                hiddenTimerProperties = true;
            }
            if (PropertiesChanged != null) PropertiesChanged(this,
                new MathArtsPropertiesEventArgs((uint)colorModulator,
                    (uint)timerInterval, Chb_DefaultTimer.Checked,
                    Chb_UseTimer.Checked,
                    MathArtsPropertiesEventArgs.ChangeTypes.Timer));
        } 
        #endregion
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

        private bool useTimer;

        public bool UseTimer
        {
            get { return useTimer; }
            set { useTimer = value; }
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

        public MathArtsPropertiesEventArgs(uint _colorModulator, uint _timerVal,bool _useDefaultTimer,bool _useTimer,ChangeTypes _changeType)
        {
            this.colorModulator = _colorModulator;
            this.timerInterval = _timerVal;
            this.useDefaultTimer = _useDefaultTimer;
            this.changeType = _changeType;
            this.useTimer = _useTimer;
        }

        public enum ChangeTypes
        {
            ColorModulator,Timer
        }
    }
}
