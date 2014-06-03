/////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// <copyright file="Ctl_MathArtsObject.cs">
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
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace MathArts
{
    public partial class Ctl_MathArtsObject : TransParentLib.UserControlTP
    {
        #region members
        private Point mouseDownLocation;
        private MouseClickType mouseClickType = MouseClickType.None;
        #endregion

        #region constructors
        public Ctl_MathArtsObject()
        {
            InitializeComponent();
        }


        //better: use this constructor instead of implement it with default location in each child
        //problem: how to set default values? need to call -> Ctl_MathArtsObject(int _x,int _y) -> Child() 
        /*
        public Ctl_MathArtsObject(int _x,int _y)
        {
            InitializeComponent();
            this.Location = new Point(_x, _y);
        }
        */
        #endregion

        #region enumerations
        private enum MouseClickType
        {
            None, Move, Resize
        }
        #endregion enumerations

        #region Mouse Events
        private void Ctl_MathArtsObject_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                mouseDownLocation = e.Location;
                if (e.X > this.Width - 10 && e.Y > this.Height - 10)
                {
                    this.mouseClickType = MouseClickType.Resize;
                }
                else this.mouseClickType = MouseClickType.Move;
            }

            else this.mouseClickType = MouseClickType.None;
        }

        private void Ctl_MathArtsObject_MouseUp(object sender, MouseEventArgs e)
        {
            this.mouseClickType = MouseClickType.None;
        }

        private void Ctl_MathArtsObject_MouseMove(object sender, MouseEventArgs e)
        {
            this.Cursor = (e.X > this.Width - 10 && e.Y > this.Height - 10) ? Cursors.SizeNWSE : Cursors.SizeAll;

            showDebugInformation(e.X + " " + e.Y + " w/h " + this.Width + " " + this.Height + " p " + this.Location.X + " " + this.Location.Y + "  " + this.mouseClickType.ToString());

            if (this.mouseClickType == MouseClickType.Move)
            {
                this.Left = e.X + this.Left - mouseDownLocation.X;
                this.Top = e.Y + this.Top - mouseDownLocation.Y;
            }
            else if (this.mouseClickType == MouseClickType.Resize)
            {
                this.Width = e.X;
                this.Height = e.Y;
            }
        }

        private void Ctl_MathArtsObject_Resize(object sender, EventArgs e)
        {
            Refresh();
        } 
        #endregion

        #region debug methods
        [ConditionalAttribute("DEBUG")]
        private void showDebugInformation(string info)
        {
            Lbl_DebugInfo.Text = info;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        }
        #endregion debug methods

    }
}
