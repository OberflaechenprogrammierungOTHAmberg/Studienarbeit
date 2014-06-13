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
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using MathArts.MathArtsColor;
using MathArts.MathArtsFunction;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace MathArts
{
    /// <summary>
    /// Main form for MathArts application containing menu and MathArtsDisp container
    /// </summary>
    public partial class Frm_MathArts : Form
    {
        #region constants
        private const bool NEED_TO_BE_REVIEWED = false;
        private const int  DEFAULT_X = 5;
        private const int  DEFAULT_Y = 5;
        #endregion constants
        
        #region constructors
        /// <summary>
        /// Default constructor initializing GUI
        /// </summary>
        public Frm_MathArts()
        {
            InitializeComponent();

            #region debug
            showTracingDialog(); 
            #endregion
        }
        #endregion

        #region properties
        public Ctl_MathArtsDisp MathArtsDispContainer
        {
            get { return MathArtsDisp_Container; }
            set{}
        }
        #endregion

        #region menu event methods
        /// <summary>
        /// Adds a color MathArts object to the container
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem_Color_Click(object sender, EventArgs e)
        {
            //create math arts color object at default location
            Ctl_MathArtsColor color = new Ctl_MathArtsColor(DEFAULT_X, DEFAULT_Y);

            this.MathArtsDisp_Container.AddMathArtsObject(color as Ctl_MathArtsObject);
        }

        /// <summary>
        /// Adds a function MathArts object to the container
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem_Function_Click(object sender, EventArgs e)
        {
            //create math arts color object at default location
            Ctl_MathArtsFunction func = new Ctl_MathArtsFunction(DEFAULT_X, DEFAULT_Y);

            this.MathArtsDisp_Container.AddMathArtsObject(func as Ctl_MathArtsObject);
        }

        /// <summary>
        /// Changes the visibility of all Math Arts object inside the container
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem_FrameVisible_Click(object sender, EventArgs e)
        {
            this.menuItem_FrameVisible.Checked = !this.menuItem_FrameVisible.Checked;
            this.MathArtsDisp_Container.ShowControls(menuItem_FrameVisible.Checked);
        }
        #endregion

        #region debug
        [ConditionalAttribute("DEBUG")]
        private void showTracingDialog()
        {
            MathArts.Debug.Frm_MathArtsObjTracing tracingDialog = new MathArts.Debug.Frm_MathArtsObjTracing(this);
            tracingDialog.Show();
        } 
        #endregion

        private void menuItem_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void menuItem_New_Click(object sender, EventArgs e)
        {
            this.MathArtsDisp_Container.ClearWorkspace();
        }

        private void menuItem_Save_Click(object sender, EventArgs e)
        {
            if (this.MathArtsDisp_Container.bitMap != null)
            {
                SaveFileDialog fd = new SaveFileDialog();
                fd.Filter = "Image Files | *.bmp";
                fd.DefaultExt = "bmp";
                fd.FileName = "MathArtsPicture.bmp";
                fd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

                if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.MathArtsDisp_Container.bitMap.Save(fd.FileName);
                }
                
            }
        }

        private void menuItem_Demo1_Click(object sender, EventArgs e)
        {
            this.MathArtsDisp_Container.DisplayDemo1();
        }
    }
}
