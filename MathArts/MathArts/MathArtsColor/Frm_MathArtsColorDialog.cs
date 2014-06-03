/////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// <copyright file="Frm_MathArtsColorDialog.cs">
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
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace MathArts.MathArtsColor
{
    /// <summary>
    /// MathArtsColor property dialog
    /// </summary>
    public partial class Frm_MathArtsColorDialog : Form
    {
        #region static member
        private static ColorDialog coldlg = new ColorDialog(); 
        #endregion

        #region constructors
        public Frm_MathArtsColorDialog(Color _currentColor, MathArts.MathArtsColor.Ctl_MathArtsColor.ColTypes _ColType)
        {
            InitializeComponent();

            //initialize default color in color dialog and preview color
            coldlg.Color = _currentColor;
            this.Pnl_ColorPreview.BackColor = _currentColor;

            //load enumeration for combobox dynamically
            Cb_Type.DataSource = Enum.GetNames(typeof(MathArts.MathArtsColor.Ctl_MathArtsColor.ColTypes));

            //initialize current color type
            Cb_Type.SelectedIndex = (int)_ColType;
        } 
        #endregion

        #region Events + Delegates
        public delegate void ColorChangedEventHandler(object sender, ColorChangedEventArgs e);
        public event ColorChangedEventHandler ColorChanged; 
        #endregion

        #region GUI event methods
        /// <summary>
        /// Starts a color dialog on button click to change the object's color. Then fires the event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Color_Click(object sender, EventArgs e)
        {
            if (coldlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //DEBUG
                showDebugInformationColType(Cb_Type.SelectedItem.ToString());

                if (ColorChanged != null) ColorChanged(this, new ColorChangedEventArgs(coldlg.Color,
                    (MathArts.MathArtsColor.Ctl_MathArtsColor.ColTypes)Enum.Parse(typeof(MathArts.MathArtsColor.Ctl_MathArtsColor.ColTypes), Cb_Type.SelectedItem.ToString())));
                
                //adapt preview panel background color
                this.Pnl_ColorPreview.BackColor = coldlg.Color; 
            }
        }

        /// <summary>
        /// Fires value changed event on changing colortype value in combo box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cb_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DEBUG
            showDebugInformationColType(Cb_Type.SelectedItem.ToString());

            if (ColorChanged != null) ColorChanged(this, new ColorChangedEventArgs(coldlg.Color,
                (MathArts.MathArtsColor.Ctl_MathArtsColor.ColTypes)Enum.Parse(typeof(MathArts.MathArtsColor.Ctl_MathArtsColor.ColTypes), Cb_Type.SelectedItem.ToString())));
        }
        #endregion events

        #region debug methods
        [ConditionalAttribute("DEBUG")]
        private void showDebugInformationColType(string _info)
        {
            Lbl_DebugInfoColType.Text = _info;
        }

        #endregion debug methods

    }

    /// <summary>
    /// Color changed EventArgs
    /// </summary>
    public class ColorChangedEventArgs : EventArgs
    {
        private Color newColor;

        public Color NewColor
        {
            get { return newColor; }
            set { newColor = value; }
        }

        private MathArts.MathArtsColor.Ctl_MathArtsColor.ColTypes newColType;

        public MathArts.MathArtsColor.Ctl_MathArtsColor.ColTypes NewColType
        {
            get { return newColType; }
            set { newColType = value; }
        }

        public ColorChangedEventArgs(Color _newColor, MathArts.MathArtsColor.Ctl_MathArtsColor.ColTypes _newColType)
        {
            this.newColor = _newColor;
            this.newColType = _newColType;
        }
    }
}
