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
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

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

            //  Set default color and color type
            this.color = DEFAULT_COLOR;
            this.colType = DEFAULT_COLTYPE;
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
                    if (ValueChanged != null) ValueChanged(this, new MathArtsColorValueChangedEventArgs(color));
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
                    if (ValueChanged != null) ValueChanged(this, new MathArtsColorValueChangedEventArgs(colType));
                    this.Refresh();
                }
            }
        }
        #endregion

        #region public methods
        /// <summary>
        /// Saves MathArts color properties as xml document (.marts file)
        /// </summary>
        /// <param name="_doc">xml document</param>
        /// <param name="_mathArtsObjNode">parent of new node</param>
        /// <param name="currentNode">new created node</param>
        /// <returns>Modified xml document</returns>
        public override XmlDocument SaveMathArtsObj(XmlDocument _doc, XmlNode _mathArtsObjNode,out XmlNode currentNode)
        {
            XmlNode currentMathArtsObjNode;

            //call base for common math art object attributes
            _doc = base.SaveMathArtsObj(_doc, _mathArtsObjNode, out currentMathArtsObjNode);

            //add specific color attributes to node
            currentMathArtsObjNode.Attributes.Append(_doc.CreateAttribute("MathArtsObjType")).Value = "Color";
            currentMathArtsObjNode.Attributes.Append(_doc.CreateAttribute("Color_R")).Value = this.Color.R.ToString();
            currentMathArtsObjNode.Attributes.Append(_doc.CreateAttribute("Color_G")).Value = this.Color.G.ToString();
            currentMathArtsObjNode.Attributes.Append(_doc.CreateAttribute("Color_B")).Value = this.Color.B.ToString();

            currentMathArtsObjNode.Attributes.Append(_doc.CreateAttribute("ColType")).Value = this.ColType.ToString();
            
            currentNode=currentMathArtsObjNode;
            return _doc;
        }
        #endregion

        #region Events
        public event EventHandler ValueChanged;
        #endregion

        #region enums
        public enum ColTypes
        {
            [Description("Minimum")]
            Low = 0,
            [Description("Maximum")]
            High = 1,
        }
        #endregion

        #region GUI event methods
        /// <summary>
        /// Paint event method. Draws a ellipse and fills with selected color
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ctl_MathArtsColor_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillEllipse(new SolidBrush(this.Color),0, 0, this.Width - 2, this.Height - 2);
            e.Graphics.DrawEllipse(Pens.Red, 0, 0, this.Width - 2, this.Height - 2);
        }

        /// <summary>
        /// Opens a property dialog to change the control's properties
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
    }

    /// <summary>
    /// Event arguments from color value changes transmitting color and color type and type of modification
    /// </summary>
    public class MathArtsColorValueChangedEventArgs : EventArgs
    {
        private ValueChangeTypes changeType;
        private Color color;
        private MathArts.MathArtsColor.Ctl_MathArtsColor.ColTypes colType;

        public ValueChangeTypes ChangeType
        {
            get { return changeType; }
            set { changeType = value; }
        }

        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        public MathArts.MathArtsColor.Ctl_MathArtsColor.ColTypes ColType
        {
            get { return colType; }
            set { colType = value; }
        }

        public MathArtsColorValueChangedEventArgs (Color _color)
        {
            ChangeType = ValueChangeTypes.Color;
            Color = _color;
        }

        public MathArtsColorValueChangedEventArgs (MathArts.MathArtsColor.Ctl_MathArtsColor.ColTypes _colType)
        {
            ChangeType = ValueChangeTypes.ColType;
            ColType = _colType;
        }

        public enum ValueChangeTypes
        {
            Color = 0,
            ColType = 1,
        }
    }
}
