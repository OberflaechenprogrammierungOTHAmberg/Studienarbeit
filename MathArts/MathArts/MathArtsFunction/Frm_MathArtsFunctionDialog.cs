/////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// <copyright file="Frm_MathArtsFunctionDialog.cs">
// Copyright (c) 2014
// </copyright>
//
// <author>Betting Pascal, Schneider Mathias, Schlemelch Manuel</author>
// <date>02-06-2014</date>
//
// <professor>Prof. Dr. Josef Poesl</professor>
// <studyCourse>Angewandte Informatik</studyCourse>
// <branchOfStudy>Industrieinformatik</branchOfStudy>
// <subject>Oberflaechenprogrammierung</subject>
//
// <summary></summary>
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Windows.Forms;

namespace MathArts.MathArtsFunction
{
    /// <summary>
    /// MathArts function property dialog
    /// </summary>
    public partial class Frm_MathArtsFunctionDialog : Form
    {
        #region constructors
        public Frm_MathArtsFunctionDialog(bool _funcInverse, MathArts.MathArtsFunction.Ctl_MathArtsFunction.FuncTypes _funcType)
        {
            InitializeComponent();

            Chb_Inverse.Checked = _funcInverse;

            Cb_Function.DataSource = Enum.GetNames(typeof(MathArts.MathArtsFunction.Ctl_MathArtsFunction.FuncTypes));

            //initialize current color type
            Cb_Function.SelectedIndex = (int)_funcType;
        } 
        #endregion

        #region Events + delegates
        public delegate void FunctionChangedEventHandler(object sender, FunctionChangedEventArgs e);
        public event FunctionChangedEventHandler FunctionChanged; 
        #endregion

        #region GUI event methods
        /// <summary>
        /// Fires the value FunctionChanged event on changing the combo box value.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cb_Function_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FunctionChanged != null) FunctionChanged(this, new FunctionChangedEventArgs(Chb_Inverse.Checked,
                (MathArts.MathArtsFunction.Ctl_MathArtsFunction.FuncTypes)Enum.Parse(typeof(MathArts.MathArtsFunction.Ctl_MathArtsFunction.FuncTypes), Cb_Function.SelectedItem.ToString())));
        }

        /// <summary>
        /// Fires the value FunctionChanged event on changing the check box value.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Chb_Inverse_CheckedChanged(object sender, EventArgs e)
        {
            if (FunctionChanged != null) FunctionChanged(this, new FunctionChangedEventArgs(Chb_Inverse.Checked,
                (MathArts.MathArtsFunction.Ctl_MathArtsFunction.FuncTypes)Enum.Parse(typeof(MathArts.MathArtsFunction.Ctl_MathArtsFunction.FuncTypes), Cb_Function.SelectedItem.ToString())));
        } 
        #endregion
    }

    /// <summary>
    /// Function value changed event args
    /// </summary>
    public class FunctionChangedEventArgs : EventArgs
    {
        private bool newInverseValue;

        public bool NewInverseValue
        {
            get { return newInverseValue; }
            set { newInverseValue = value; }
        }

        private MathArts.MathArtsFunction.Ctl_MathArtsFunction.FuncTypes newFuncType;

        public MathArts.MathArtsFunction.Ctl_MathArtsFunction.FuncTypes NewFuncType
        {
            get { return newFuncType; }
            set { newFuncType = value; }
        }

        public FunctionChangedEventArgs(bool _newInverseValue, MathArts.MathArtsFunction.Ctl_MathArtsFunction.FuncTypes _newFuncType)
        {
            this.newInverseValue = _newInverseValue;
            this.newFuncType = _newFuncType;
        }

    }
}
