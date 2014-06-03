/////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// <copyright file="Ctl_MathArtsColor.cs">
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

namespace MathArts.MathArtsColor
{
    /// <summary>
    /// MathArts color object
    /// </summary>
    public partial class Ctl_MathArtsColor : Ctl_MathArtsObject
    {
        #region constants
        //color can not be defined as const 
        static readonly private Color DEFAULT_COLOR = Color.White;
        private const ColTypes DEFAULT_COLTYPE = ColTypes.High;
        #endregion constants

        #region member
        private Color color;
        private ColTypes colType;
        #endregion

        #region constructors
        public Ctl_MathArtsColor()
        {
            InitializeComponent();

            //initialize member and finally use property to trigger ValueChanged event
            this.color = DEFAULT_COLOR;
            this.ColType = DEFAULT_COLTYPE;
        }

        public Ctl_MathArtsColor(int _x,int _y)
            :this()
        {
            this.Location = new Point(_x, _y);
        }
        #endregion

        #region properties
        public Color Color
        {
            get { return color; }
            set 
            {
                if (value != color)
                {
                    color = value;
                    if (ValueChanged != null) ValueChanged(this, EventArgs.Empty);
                    this.Refresh();
                }
            }
        }
        public ColTypes ColType
        {
            get { return colType; }
            set 
            {
                if (value != colType)
                {
                    colType = value;
                    if (ValueChanged != null) ValueChanged(this, EventArgs.Empty);
                    this.Refresh();
                }
            }
        }
        #endregion

        #region Events
        public event EventHandler ValueChanged; 
        #endregion

        #region enums
        public enum ColTypes
        {
            Low, High
        }
        #endregion

        #region GUI event methods
        /// <summary>
        /// Paint event method. Draws a ellipse
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ctl_MathArtsColor_Paint(object sender, PaintEventArgs e)
        {
            //  debug method
            drawFilledEllipse(e, new SolidBrush(this.color), 0, 0, this.Width - 1, this.Height - 1);

            e.Graphics.DrawEllipse(Pens.Green, 0, 0, this.Width - 1, this.Height - 1);
        }

        /// <summary>
        /// Opens a property dialog to change the controls properties
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ctl_MathArtsColor_DoubleClick(object sender, EventArgs e)
        {
            Frm_MathArtsColorDialog coldlg = new Frm_MathArtsColorDialog(this.color, this.colType);
            coldlg.ColorChanged += coldlg_ColorChanged;
            coldlg.ShowDialog();
        }

        /// <summary>
        /// Event method to recognize changes inside the property dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void coldlg_ColorChanged(object sender, ColorChangedEventArgs e)
        {
            this.Color = e.NewColor;
            this.ColType = e.NewColType;
        }
        #endregion

        #region debug methods
        [ConditionalAttribute("DEBUG")]
        private void drawFilledEllipse(PaintEventArgs e,SolidBrush _solidBrush, int _x1, int _y1, int _x2, int _y2)
        {
            e.Graphics.FillEllipse(_solidBrush, _x1, _y1,_x2, _y2);
            //DEBUG
            showDebugInformationColor("C: " + this.Color.ToString() + " CType" + this.ColType.ToString());
        }

        /// <summary>
        /// Displays info string for easier error tracing
        /// </summary>
        /// <param name="_info"></param>
        [ConditionalAttribute("DEBUG")]
        private void showDebugInformationColor(string _info)
        {
            Lbl_DebugInfoColor.Text = _info;
        }
        #endregion debug methods
    }
}
