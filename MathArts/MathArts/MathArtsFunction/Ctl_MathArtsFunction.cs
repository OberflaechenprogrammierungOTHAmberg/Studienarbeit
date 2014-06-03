/////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// <copyright file="Ctl_MathArtsFunction.cs">
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
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace MathArts.MathArtsFunction
{
    /// <summary>
    /// MathArts function object
    /// </summary>
    public partial class Ctl_MathArtsFunction : Ctl_MathArtsObject
    {
        #region constants
        private const bool DEFAULT_FUNC_INVERSE = false;
        private const FuncTypes DEFAULT_COLTYPE = FuncTypes.SinCos;
        #endregion constants

        #region member
        private FuncTypes funcType;
        private bool funcInverse;
        private double[,] valArr;
        #endregion

        #region constructors
        public Ctl_MathArtsFunction()
        {
            InitializeComponent();

            //initialize member and finally use property to trigger ValueChanged event
            this.funcInverse = DEFAULT_FUNC_INVERSE;
            this.FuncType = DEFAULT_COLTYPE;

            updateFuncValArr();
        }

        public Ctl_MathArtsFunction(int _x, int _y)
            : this()
        {
            this.Location = new Point(_x, _y);
        }
        #endregion

        #region properties
        public FuncTypes FuncType
        {
            get { return funcType; }
            set
            {
                if (funcType != value)
                {
                    funcType = value;
                    if (ValueChanged != null) ValueChanged(this, EventArgs.Empty);
                }
            }
        }

        public bool FuncInverse
        {
            get { return funcInverse; }
            set
            {
                if (funcInverse != value)
                {
                    funcInverse = value;
                    if (ValueChanged != null) ValueChanged(this, EventArgs.Empty);
                }
            }
        }
        #endregion

        #region events
        public event EventHandler ValueChanged;
        #endregion

        #region enums
        public enum FuncTypes
        {
            SinCos, Gauss, Garbor
        }
        #endregion

        #region internal methods
        /// <summary>
        /// Redefines the value array for the fast verion
        /// </summary>
        private void updateFuncValArr()
        {
            valArr = new double[this.Width, this.Height];

            for (int i = 0; i < this.Width; i++)
            {
                for (int j = 0; j < this.Height; j++)
                {
                    double normX = ((2.0 * i) / this.Width * 1.0) - 1.0;
                    double normY = ((2.0 * j) / this.Height * 1.0) - 1.0;

                    //set Gauss as default because needed in all funtions
                    valArr[i, j] = Math.Exp(-4 * (normX * normX + normY * normY));

                    switch (funcType)
                    {
                        case (FuncTypes.SinCos):
                            valArr[i, j] = valArr[i, j] * Math.Abs((Math.Cos(2 * Math.PI * (normX + normY)) * Math.Sin(2 * Math.PI * normY)));
                            break;
                        case (FuncTypes.Gauss):
                            //pass
                            break;
                        case (FuncTypes.Garbor):
                            valArr[i, j] = valArr[i, j] * Math.Abs(Math.Sin(8 * Math.PI * (normX * normX + normY * normY)));
                            break;
                        default:
                            //will not occur
                            break;
                    }

                    if (funcInverse) valArr[i, j] = 1 - valArr[i, j];
                }
            }
        }
        #endregion internal methods

        #region GUI event methods
        /// <summary>
        /// Paint event method. Draws a ellipse
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ctl_MathArtsFunction_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(Pens.Green, 0, 0, this.Width - 1, this.Height - 1);
        }

        /// <summary>
        /// Mouse up event method. Updates the function array.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ctl_MathArtsFunction_MouseUp(object sender, MouseEventArgs e)
        {
            updateFuncValArr();
        }

        /// <summary>
        /// Opens the property dialog to change functype and inverse
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ctl_MathArtsFunction_DoubleClick(object sender, EventArgs e)
        {
            Frm_MathArtsFunctionDialog funcDlg = new Frm_MathArtsFunctionDialog(this.funcInverse, this.funcType);
            funcDlg.FunctionChanged += funcDlg_FunctionChanged;
            funcDlg.ShowDialog();
        }

        /// <summary>
        /// Event method to recognize changes inside the property dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void funcDlg_FunctionChanged(object sender, FunctionChangedEventArgs e)
        {
            this.FuncInverse = e.NewInverseValue;
            this.FuncType = e.NewFuncType;
        } 
        #endregion

        #region public methods
        /// <summary>
        /// Calculates func value at a specific position (slow version)
        /// </summary>
        /// <param name="_x">X position</param>
        /// <param name="_y">Y position</param>
        /// <returns></returns>
        public double GetFuncVal(int _x, int _y)
        {
            double funcVal = 0.0;

            if (_x < 0
                || _x >= this.Width
                || _y < 0
                || _y >= this.Height) return funcVal;

            double normX = ((2.0 * _x) / this.Width*1.0) - 1.0;
            double normY = ((2.0 * _y) / this.Height *1.0) - 1.0;

            //set Gauss as default because needed in all funtions
            funcVal = Math.Exp(-4 * (normX * normX + normY * normY));

            switch (funcType)
            {
                case (FuncTypes.SinCos):
                    funcVal = funcVal * Math.Abs((Math.Cos(2 * Math.PI * (normX  + normY))*Math.Sin(2*Math.PI*normY)));
                    break;
                case (FuncTypes.Gauss):
                    //pass
                    break;
                case (FuncTypes.Garbor):
                    funcVal = funcVal * Math.Abs(Math.Sin(8 * Math.PI * (normX * normX + normY * normY)));
                    break;
                default:
                    //will not occur
                    break;
            }

            if (funcInverse) funcVal = 1 - funcVal;

            return funcVal;
        }

        /// <summary>
        /// Calculates func values by returning value array (fast version)
        /// </summary>
        /// <param name="_x">X postion</param>
        /// <param name="_y">Y position</param>
        /// <returns></returns>
        public double GetFuncValFromArray(int _x, int _y)
        {
            return valArr[_x, _y];
        }
        #endregion

        #region debug methods/events
        [ConditionalAttribute("DEBUG")]
        private void showDebugInformationFunctionValue(string _info)
        {
            Lbl_DebugInfoFuncVal.Text = _info;
        }

        private void Ctl_MathArtsFunction_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Left)
            {
                showDebugInformationFunctionValue(GetFuncValFromArray(e.X, e.Y).ToString());
            }
        }      
        #endregion debug methods
    }
}
