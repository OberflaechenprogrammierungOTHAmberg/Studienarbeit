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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MathArts.MathArtsFunction
{
    public partial class Frm_MathArtsFunctionDialog : Form
    {
        public Frm_MathArtsFunctionDialog(bool _funcInverse,MathArts.MathArtsFunction.Ctl_MathArtsFunction.FuncTypes _funcType)
        {
            InitializeComponent();

            Chb_Inverse.Checked = _funcInverse;

            Cb_Function.DataSource = Enum.GetNames(typeof(MathArts.MathArtsFunction.Ctl_MathArtsFunction.FuncTypes));

            //initialize current color type
            Cb_Function.SelectedIndex = (int)_funcType;
        }



        public delegate void FunctionChangedEventHandler(object sender, FunctionChangedEventArgs e);
        public event FunctionChangedEventHandler FunctionChanged;

        private void Cb_Function_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FunctionChanged != null) FunctionChanged(this, new FunctionChangedEventArgs(Chb_Inverse.Checked,
                (MathArts.MathArtsFunction.Ctl_MathArtsFunction.FuncTypes)Enum.Parse(typeof(MathArts.MathArtsFunction.Ctl_MathArtsFunction.FuncTypes), Cb_Function.SelectedItem.ToString())));
        }

        private void Chb_Inverse_CheckedChanged(object sender, EventArgs e)
        {
            if (FunctionChanged != null) FunctionChanged(this, new FunctionChangedEventArgs(Chb_Inverse.Checked,
                (MathArts.MathArtsFunction.Ctl_MathArtsFunction.FuncTypes)Enum.Parse(typeof(MathArts.MathArtsFunction.Ctl_MathArtsFunction.FuncTypes), Cb_Function.SelectedItem.ToString())));
        }
    }

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
