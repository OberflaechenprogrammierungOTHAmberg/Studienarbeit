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
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace MathArts
{
    /// <summary>
    /// Parent object for all MathArts objects
    /// </summary>
    public partial class Ctl_MathArtsObject : TransParentLib.UserControlTP
    {
        #region members
        private Point mouseDownLocation;
        protected MouseClickTypes mouseClickType = MouseClickTypes.None;

        #region debug
        //creating incremental id for debugging
        private static uint mathArtsCounter = 0;
        private uint mathArts;
        #endregion

        #endregion

        #region constructors
        public Ctl_MathArtsObject()
        {
            InitializeComponent();
            #region debug
            mathArts = mathArtsCounter++;
            #endregion debug
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
        public enum MouseClickTypes
        {
            None, Move, Resize
        }
        #endregion enumerations

        #region Mouse Events
        /// <summary>
        /// Executes on mouse down. Sets the mouseclick type to decide whether the users resizes or moves the control (or does nothing)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ctl_MathArtsObject_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                mouseDownLocation = e.Location;
                if (e.X > this.Width - 10 && e.Y > this.Height - 10)
                {
                    this.mouseClickType = MouseClickTypes.Resize;
                    this.Cursor = Cursors.SizeNWSE;
                }
                else
                {
                    this.mouseClickType = MouseClickTypes.Move;
                    this.Cursor = Cursors.SizeAll;
                }
            }

            else this.mouseClickType = MouseClickTypes.None;
        }

        /// <summary>
        /// Executes on mouse up. Resets the mousclick type to none
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ctl_MathArtsObject_MouseUp(object sender, MouseEventArgs e)
        {
            this.mouseClickType = MouseClickTypes.None;
            //reset cursor
            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Changes the cursor on hover to resize or move cursor. Also moves the object or resizes the object when the mouseclick type is set.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ctl_MathArtsObject_MouseMove(object sender, MouseEventArgs e)
        {
            //only change "mouse over" cursor type if left mouse button is not pressed
            if(e.Button != System.Windows.Forms.MouseButtons.Left) this.Cursor = (e.X > this.Width - 10 && e.Y > this.Height - 10) ? Cursors.SizeNWSE : Cursors.SizeAll;

            if (this.mouseClickType == MouseClickTypes.Move)
            {
                this.Left = e.X + this.Left - mouseDownLocation.X;
                this.Top = e.Y + this.Top - mouseDownLocation.Y;
            }
            else if (this.mouseClickType == MouseClickTypes.Resize)
            {
                this.Width = e.X;
                this.Height = e.Y;
            }

            if (this.mouseClickType != MouseClickTypes.None) ShapeValueChanged(this, e);
            
            #region debug
            Tracing_TriggerShapeValueChanged(e);
            #endregion
            
        }

        /// <summary>
        /// Refreshes the object on resize
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ctl_MathArtsObject_Resize(object sender, EventArgs e)
        {
            Refresh();
        }
        #endregion

        #region debug
        public event EventHandler ShapeValueChanged;

        //[ConditionalAttribute("DEBUG")]
        //not possible only for debug - workaround or MouseClickType() property only for debug issues?
        public MouseClickTypes GetMouseClickType()
        {
            return this.mouseClickType;
        }

        [ConditionalAttribute("DEBUG")]
        private void Tracing_TriggerShapeValueChanged(EventArgs e)
        {
            if (ShapeValueChanged != null)
            {
                ShapeValueChanged(this, e);
            }
        }

        public override string ToString()
        {
            return "MathArtsObj_" + this.mathArts;
        }
        #endregion
    }
}
