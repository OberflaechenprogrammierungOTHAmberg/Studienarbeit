/////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// <copyright file="Frm_MathArts.cs">
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
// <summary>Main form for MathArts application</summary>
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using MathArts.MathArtsColor;
using MathArts.MathArtsFunction;
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
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // <summary>Main form for MathArts application containing menu and MathArtsDisp container</summary>
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public partial class Frm_MathArts : Form
    {
        #region constants
        private const bool NEED_TO_BE_REVIEWED = false;
        private const int  DEFAULT_X = 5;
        private const int  DEFAULT_Y = 5;

        #endregion constants

        public Frm_MathArts()
        {
            InitializeComponent();
        }

        #region MenuEvents
        private void menuItem_Color_Click(object sender, EventArgs e)
        {
            //create math arts color object at default location
            Ctl_MathArtsColor color = new Ctl_MathArtsColor(DEFAULT_X, DEFAULT_Y);

            this.MathArtsDisp_Container.AddMathArtsObject(color as Ctl_MathArtsObject);
        }

        private void menuItem_Function_Click(object sender, EventArgs e)
        {
            //create math arts color object at default location
            Ctl_MathArtsFunction func = new Ctl_MathArtsFunction(DEFAULT_X, DEFAULT_Y);

            this.MathArtsDisp_Container.AddMathArtsObject(func as Ctl_MathArtsObject);
        }

        private void menuItem_FrameVisible_Click(object sender, EventArgs e)
        {
            this.menuItem_FrameVisible.Checked = !this.menuItem_FrameVisible.Checked;
            this.MathArtsDisp_Container.ShowControls(menuItem_FrameVisible.Checked);
        }
        #endregion


    }
}
