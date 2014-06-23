/////////////////////////////////////////////////////////////////////////////
// <copyright file="Frm_MathArtsFunctionDialog.cs">
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

            //initialize default func Inverse value
            Chb_Inverse.Checked = _funcInverse;

            //load enumeration for combobox dynamically
            Cb_Function.DataSource = EnumExtensions.GetDescriptionsToList(typeof(MathArts.MathArtsFunction.Ctl_MathArtsFunction.FuncTypes)); 

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
                Cb_Function.SelectedItem.ToString().ToEnum<MathArts.MathArtsFunction.Ctl_MathArtsFunction.FuncTypes>()));
        }

        /// <summary>
        /// Fires the value FunctionChanged event on changing the check box value.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Chb_Inverse_CheckedChanged(object sender, EventArgs e)
        {
            if (FunctionChanged != null) FunctionChanged(this, new FunctionChangedEventArgs(Chb_Inverse.Checked,
                Cb_Function.SelectedItem.ToString().ToEnum<MathArts.MathArtsFunction.Ctl_MathArtsFunction.FuncTypes>()));
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
