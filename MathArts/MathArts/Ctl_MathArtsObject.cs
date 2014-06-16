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
        private Point mouseDownLocation;
        protected MouseClickTypes mouseClickType = MouseClickTypes.None;
        

        private bool readyForShapeValueChange=true;

        #region debug
        //creating incremental id for debugging
        public static uint mathArtsCounter = 0;
        private uint mathArts;
        #endregion

        #endregion

        #region constructors
        public Ctl_MathArtsObject()
        {
            InitializeComponent();

            readyForShapeValueChange = true;

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

        #region protected methods
        public virtual XmlDocument SaveMathArtsObj(XmlDocument _doc, XmlNode _mathArtsObjNode, out XmlNode _currentMathArtsObjNode)
        {
            XmlNode MathArtsObjNode = _doc.CreateElement(this.ToString());
            _mathArtsObjNode.AppendChild(MathArtsObjNode);

            MathArtsObjNode.Attributes.Append(_doc.CreateAttribute("Height")).Value = this.Height.ToString();
            MathArtsObjNode.Attributes.Append(_doc.CreateAttribute("Width")).Value = this.Width.ToString();

            MathArtsObjNode.Attributes.Append(_doc.CreateAttribute("X")).Value = this.Location.X.ToString();
            MathArtsObjNode.Attributes.Append(_doc.CreateAttribute("Y")).Value = this.Location.Y.ToString();
            
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

            if (ShapeValueChanged != null) ShapeValueChanged(this, e);
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

            if (this.mouseClickType != MouseClickTypes.None)
            {
                lock (this)
                {
                    if (readyForShapeValueChange)
                    {
                        if (ShapeValueChanged != null) ShapeValueChanged(this, e);
                        readyForShapeValueChange = false;
                    }
                }
            }

            #region debug
            if (ShapeValueChanged != null) Tracing_TriggerShapeValueChanged(e);
            #endregion

            
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
