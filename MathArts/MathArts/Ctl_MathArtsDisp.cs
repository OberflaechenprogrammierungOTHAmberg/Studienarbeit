/////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// <copyright file="Ctl_MathArtsDisp.cs">
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

using System.Collections.Generic;
using System.Windows.Forms;

namespace MathArts
{
    /// <summary>
    /// Container for all MathArts objects
    /// </summary>
    public partial class Ctl_MathArtsDisp : UserControl
    {
        #region member
        private List<Ctl_MathArtsObject> allContainedMathArtsObjects;
        #endregion

        #region constructors
        /// <summary>
        /// Default constructor initializing GUI and MathArts object list
        /// </summary>
        public Ctl_MathArtsDisp()
        {
            InitializeComponent();
            this.allContainedMathArtsObjects = new List<Ctl_MathArtsObject>();
        }
        #endregion

        #region public methods
        /// <summary>
        /// Changes the visibility of all MathArts objects inside the container
        /// </summary>
        /// <param name="showControls">Visibility value for the objects</param>
        public void ShowControls(bool showControls)
        {
            this.allContainedMathArtsObjects.ForEach(n => n.Visible = showControls);
        }
   
        /// <summary>
        /// Adds a new MathArts object to the container
        /// </summary>
        /// <param name="_object">MathArts object to add</param>
        public void AddMathArtsObject(Ctl_MathArtsObject _object)
        {
            this.allContainedMathArtsObjects.Add(_object);
            Refresh();
        }

        /// <summary>
        /// Calculates the color value at a position inside the container
        /// </summary>
        /// <param name="_x">X position</param>
        /// <param name="_y">Y position</param>
        /// <param name="val">Value calculated from product of functions</param>
        public void ColorFromVal(int _x,int _y,double val)
        {
            //TODO
            Refresh();
        }
        #endregion

        #region GUI event methods
        /// <summary>
        /// Paint method of the container. Paints all MathArts objects inside the container
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ctl_MathArtsDisp_Paint(object sender, PaintEventArgs e)
        {
            foreach (Ctl_MathArtsObject item in this.allContainedMathArtsObjects)
            {
                if (!this.Controls.Contains(item))
                {
                    this.Controls.Add(item); 
                }
            }
        }
        #endregion
    }
}
