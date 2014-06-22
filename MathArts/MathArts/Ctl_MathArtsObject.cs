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
using System.Diagnostics;
using System.Drawing;
using System.Timers;
using System.Windows.Forms;
using System.Xml;

namespace MathArts
{
    /// <summary>
    /// Parent object for all MathArts objects
    /// </summary>
    public partial class Ctl_MathArtsObject : TransParentLib.UserControlTP
    {
        #region members

        #region mouse

        private Point mouseDownLocation;
        protected MouseClickTypes mouseClickType = MouseClickTypes.None;

        #endregion

        #region timer

        // flag for indicating new timer tick after sending last ShapeValueChanged event
        private bool readyForShapeValueChange;
        public static bool UseTimer = false;

        #endregion

        #region debug
        //creating incremental id for debugging and saving
        public static uint mathArtsCounter = 0;
        private uint mathArts;
        #endregion

        #endregion

        #region constructors

        public Ctl_MathArtsObject()
        {
            InitializeComponent();

            #region timer 
            readyForShapeValueChange = true;
            #endregion

            // increment math art object id
            mathArts = mathArtsCounter++;
        }

        #endregion

        #region virtual methods

        /// <summary>
        /// generic methods for saving math art object properties to xml (add to _mathArtsObjNode in XMLDocument _doc 
        /// and returning current xml node via out _currentMathArtsObjNode)
        /// </summary>
        /// <param name="_doc"></param>
        /// <param name="_mathArtsObjNode"></param>
        /// <param name="_currentMathArtsObjNode"></param>
        /// <returns></returns>
        public virtual XmlDocument SaveMathArtsObj(XmlDocument _doc, XmlNode _mathArtsObjNode, out XmlNode _currentMathArtsObjNode)
        {
            // create and append new node to xml document
            XmlNode MathArtsObjNode = _doc.CreateElement(this.ToString());
            _mathArtsObjNode.AppendChild(MathArtsObjNode);

            // add math art object properties as xml attributes
            MathArtsObjNode.Attributes.Append(_doc.CreateAttribute("Height")).Value = this.Height.ToString();
            MathArtsObjNode.Attributes.Append(_doc.CreateAttribute("Width")).Value = this.Width.ToString();

            MathArtsObjNode.Attributes.Append(_doc.CreateAttribute("X")).Value = this.Location.X.ToString();
            MathArtsObjNode.Attributes.Append(_doc.CreateAttribute("Y")).Value = this.Location.Y.ToString();
            
            // set current node to created
            _currentMathArtsObjNode=MathArtsObjNode;
            return _doc;
        }

        #endregion

        #region enumerations

        public enum MouseClickTypes
        {
            None, Move, Resize
        }

        #endregion enumerations

        #region mouse events
        /// <summary>
        /// Executes on mouse down. Sets the mouseclick type to decide whether the users resizes or moves the control (or does nothing)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ctl_MathArtsObject_MouseDown(object sender, MouseEventArgs e)
        {
            // set mouse click type and cursor if left mouse button is pressed
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                // save current mouse location
                mouseDownLocation = e.Location;
                
                // in lower right corner prepare resizing
                if (e.X > this.Width - 10 && e.Y > this.Height - 10)
                {
                    this.mouseClickType = MouseClickTypes.Resize;
                    this.Cursor = Cursors.SizeNWSE;
                }
                // elsewhere prepare moving
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
            // reset cursor and mouse click type
            this.mouseClickType = MouseClickTypes.None;
            this.Cursor = Cursors.Default;

            // send ShapeValueChanged event
            if (ShapeValueChanged != null) ShapeValueChanged(this, e);
        }

        /// <summary>
        /// Changes the cursor on hover to resize or move cursor. Also moves the object or resizes the object when the mouseclick type is set.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ctl_MathArtsObject_MouseMove(object sender, MouseEventArgs e)
        {
            // only change "mouse over" cursor type if left mouse button is not pressed
            if(e.Button != System.Windows.Forms.MouseButtons.Left) this.Cursor = (e.X > this.Width - 10 && e.Y > this.Height - 10) ? Cursors.SizeNWSE : Cursors.SizeAll;

            
            if (this.mouseClickType == MouseClickTypes.Move)
            {
                // relocate math art object
                this.Left = e.X + this.Left - mouseDownLocation.X;
                this.Top = e.Y + this.Top - mouseDownLocation.Y;
            }
            else if (this.mouseClickType == MouseClickTypes.Resize)
            {
                // resize math art object
                this.Width = e.X;
                this.Height = e.Y;
            }

            // send shape value changed event
            if (this.mouseClickType != MouseClickTypes.None)
            {
                // only send it if timer is used and has been ticked since last sending of ShapeValueChanged event
                if (Ctl_MathArtsObject.UseTimer)
                {
                    // because timer is running in a separate thread we need to lock the critical section
                    lock (this)
                    {
                        if (readyForShapeValueChange)
                        {
                            if (ShapeValueChanged != null) ShapeValueChanged(this, e);
                            readyForShapeValueChange = false;
                        }
                    }
                }
                // if timer usage is disabled always send ShapeValueChanged event
                else
                {
                    if (ShapeValueChanged != null) ShapeValueChanged(this, e);
                } 
            }
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            //do something with the timer
            lock (this)
            {
                readyForShapeValueChange = true;
            }
            
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

        public void subscribeToTimer(System.Timers.Timer aTimer)
        {
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
        }

        #region debug
        public event EventHandler ShapeValueChanged;

        //[ConditionalAttribute("DEBUG")]
        //not possible only for debug - workaround or MouseClickType() property only for debug issues?
        public MouseClickTypes GetMouseClickType()
        {
            return this.mouseClickType;
        }

        public override string ToString()
        {
            return "MathArtsObj_" + this.mathArts;
        }
        #endregion
    }
}
