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
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace MathArts
{
    /// <summary>
    /// Main form for MathArts application containing menu and MathArtsDisp container
    /// </summary>
    public partial class Frm_MathArts : Form
    {
        #region constants
        private const int  DEFAULT_X = 5;
        private const int  DEFAULT_Y = 5;

        private bool saved = false;
        private string saveFileName = "";
        #endregion constants
        
        #region constructors
        /// <summary>
        /// Default constructor initializing GUI
        /// </summary>
        public Frm_MathArts()
        {
            //use custom MathArts icon
            this.Icon = MathArts.Properties.Resources.MathArts;
            InitializeComponent();
        }
        #endregion

        #region properties
        public Ctl_MathArtsDisp MathArtsDispContainer
        {
            get{ return MathArtsDisp_Container; }
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

        private void menuItem_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void menuItem_New_Click(object sender, EventArgs e)
        {
            #region debug
            showTracingDialog();
            #endregion

            this.MathArtsDisp_Container.ClearWorkspace();

            //after clearing workspace we set math arts variables to default value
            this.menuItem_FrameVisible.Checked = true;
            this.saved = false;
            this.saveFileName = "";
        }

        private void menuItem_Save_Click(object sender, EventArgs e)
        {
            //set up save file dialog
            SaveFileDialog fd = new SaveFileDialog();
            fd.Filter = "MathArts Files|*.marts|Image Files|*.bmp";
            fd.FileName = "MathArtsPicture";
            fd.AddExtension = true;
            fd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //depending on choosen file type we either save the image or save all necessary information to .marts file which can be loaded
                if (Path.GetExtension(fd.FileName) == ".bmp")
                {
                    if (this.MathArtsDisp_Container.bitMap != null) this.MathArtsDisp_Container.bitMap.Save(fd.FileName);
                }
                else
                {
                     this.saved = saveToXml(fd.FileName);
                     if (this.saved) this.saveFileName = fd.FileName;
                }
            }

        }

        private void menuItem_Demo1_Click(object sender, EventArgs e)
        {
            #region debug
            showTracingDialog();
            #endregion


            this.MathArtsDisp_Container.DisplayDemo1();

            //after loading demo 1 show math art object frames
            this.menuItem_FrameVisible.Checked = true;
            this.MathArtsDisp_Container.ShowControls(menuItem_FrameVisible.Checked);
            saved = false;
            saveFileName = "";
        }

        private void menuItem_Demo2_Click(object sender, EventArgs e)
        {
            #region debug
            showTracingDialog();
            #endregion

            if (loadFromXml(System.IO.Directory.GetCurrentDirectory() + "\\..\\..\\Demo2.marts"))
            {
                saved = true; 
                saveFileName = System.IO.Directory.GetCurrentDirectory() + "\\..\\..\\Demo2.marts";
            }

            //after loading demo 1 show math art object frames
            this.menuItem_FrameVisible.Checked = true;
            this.MathArtsDisp_Container.ShowControls(menuItem_FrameVisible.Checked);
        }

        private void menuItem_Properties_Click(object sender, EventArgs e)
        {
            Frm_MathArtsPropertiesDialog mathArtsPropertiesDialog = new Frm_MathArtsPropertiesDialog(this.MathArtsDisp_Container.DefaultTimerInterval,this.MathArtsDisp_Container.TimerInterval, this.MathArtsDisp_Container.UseDefaultTimer, this.MathArtsDisp_Container.ColorModulator);
            mathArtsPropertiesDialog.PropertiesChanged += mathArtsPropertiesDialog_PropertiesChanged;

            //hide math arts frames
            bool frameVisability = menuItem_FrameVisible.Checked;
            this.menuItem_FrameVisible.Checked = false;
            this.MathArtsDisp_Container.ShowControls(menuItem_FrameVisible.Checked);

            mathArtsPropertiesDialog.ShowDialog();
            mathArtsPropertiesDialog.PropertiesChanged -= mathArtsPropertiesDialog_PropertiesChanged;

            //recover old math arts frame visability value
            this.menuItem_FrameVisible.Checked = frameVisability;
            this.MathArtsDisp_Container.ShowControls(menuItem_FrameVisible.Checked);

        }

        void mathArtsPropertiesDialog_PropertiesChanged(object sender, MathArtsPropertiesEventArgs e)
        {
            this.MathArtsDisp_Container.ColorModulator = e.ColorModulator;
            this.MathArtsDisp_Container.UseDefaultTimer = e.UseDefaultTimer;
            this.MathArtsDisp_Container.TimerInterval = e.TimerInterval;

            if (e.ChangeType == MathArtsPropertiesEventArgs.ChangeTypes.ColorModulator) this.MathArtsDisp_Container.RefreshDisplay();
        }

        private void menuItem_Open_Click(object sender, EventArgs e)
        {
            //set up open file dialog
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "MathArts Files|*.marts";
            fd.AddExtension = true;
            fd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if(loadFromXml(fd.FileName))
                {
                    saved = true;
                    saveFileName = fd.FileName;
                }
            }
        }
        #endregion

        #region internal private methods
        private bool saveToXml(string martsFile)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                //create xml document
                XmlDocument xmlDoc = new XmlDocument();

                //add doctype
                XmlDocumentType doctype;
                doctype = xmlDoc.CreateDocumentType("MathArts", null, null, "<!ELEMENT MathArts ANY>");
                xmlDoc.AppendChild(doctype);

                //add properties concerning MathArts main frame
                XmlNode MathArtsNode = xmlDoc.AppendChild(xmlDoc.CreateElement("MathArts"));

                MathArtsNode.Attributes.Append(xmlDoc.CreateAttribute("Width")).Value = this.Width.ToString();
                MathArtsNode.Attributes.Append(xmlDoc.CreateAttribute("Height")).Value = this.Height.ToString();

                //display container will add any further properties
                xmlDoc = this.MathArtsDisp_Container.SaveMathArtsDisp(xmlDoc);

                xmlDoc.Save(martsFile);
                this.Cursor = Cursors.Default;
                return true;
            }
            catch
            {
                this.Cursor = Cursors.Default;
                return false;
            }

        }


        private bool loadFromXml(string martsFile)
        {
            //clear old workspace
            this.MathArtsDisp_Container.ClearWorkspace();

            //load xml
            XmlDocument mathArtsXml = new XmlDocument();

            //try to open .marts xml file
            try
            {
                mathArtsXml.Load(martsFile);

                //parse math arts main frame properties
                uint uintVal;
                UInt32.TryParse(mathArtsXml.DocumentElement.Attributes["Width"].InnerText, out uintVal);
                this.Width = (int)uintVal;

                UInt32.TryParse(mathArtsXml.DocumentElement.Attributes["Height"].InnerText, out uintVal);
                this.Height = (int)uintVal;

                foreach (XmlNode mathArtsDispNode in mathArtsXml.DocumentElement.ChildNodes)
                {
                    //parse math arts display properties
                    if (mathArtsDispNode.Name == "MathArtsDisp")
                    {
                        UInt32.TryParse(mathArtsDispNode.Attributes["Width"].InnerText, out uintVal);
                        this.MathArtsDisp_Container.Width = (int)uintVal;

                        UInt32.TryParse(mathArtsDispNode.Attributes["Height"].InnerText, out uintVal);
                        this.MathArtsDisp_Container.Height = (int)uintVal;

                        try
                        {
                            UInt32.TryParse(mathArtsDispNode.Attributes["ColorModulator"].InnerText, out uintVal);
                            this.MathArtsDisp_Container.ColorModulator = uintVal;
                        }
                        catch
                        {
                            this.MathArtsDisp_Container.ColorModulator = 1;
                        }
                        

                        List<Ctl_MathArtsObject> lMathArtsObjs = new List<Ctl_MathArtsObject>();

                        //parse all math art objects and append them to list
                        foreach (XmlNode mathArtsObjNode in mathArtsDispNode.FirstChild.ChildNodes)
                        {
                            if (mathArtsObjNode.Name.Contains("MathArtsObj_"))
                            {
                                int xPosition;
                                int yPosition;

                                uint width;
                                uint height;

                                Int32.TryParse(mathArtsObjNode.Attributes["X"].InnerText, out xPosition);
                                Int32.TryParse(mathArtsObjNode.Attributes["Y"].InnerText, out yPosition);

                                UInt32.TryParse(mathArtsObjNode.Attributes["Width"].InnerText, out width);
                                UInt32.TryParse(mathArtsObjNode.Attributes["Height"].InnerText, out height);

                                if (mathArtsObjNode.Attributes["MathArtsObjType"].InnerText == "Color")
                                {
                                    byte colorRed;
                                    byte colorGreen;
                                    byte colorYellow;

                                    byte.TryParse(mathArtsObjNode.Attributes["Color_R"].InnerText, out colorRed);
                                    byte.TryParse(mathArtsObjNode.Attributes["Color_G"].InnerText, out colorGreen);
                                    byte.TryParse(mathArtsObjNode.Attributes["Color_B"].InnerText, out colorYellow);

                                    Ctl_MathArtsColor newMathArtsColor = new Ctl_MathArtsColor(xPosition, yPosition);

                                    newMathArtsColor.Width = (int)width;
                                    newMathArtsColor.Height = (int)height;

                                    newMathArtsColor.Color = Color.FromArgb(colorRed, colorGreen, colorYellow);

                                    if (mathArtsObjNode.Attributes["ColType"].InnerText == "High") newMathArtsColor.ColType = Ctl_MathArtsColor.ColTypes.High;
                                    else if (mathArtsObjNode.Attributes["ColType"].InnerText == "Low") newMathArtsColor.ColType = Ctl_MathArtsColor.ColTypes.Low;

                                    lMathArtsObjs.Add(newMathArtsColor);
                                }
                                else if (mathArtsObjNode.Attributes["MathArtsObjType"].InnerText == "Function")
                                {
                                    Ctl_MathArtsFunction newMathArtsFunction = new Ctl_MathArtsFunction(xPosition, yPosition);

                                    newMathArtsFunction.Width = (int)width;
                                    newMathArtsFunction.Height = (int)height;

                                    if (mathArtsObjNode.Attributes["FuncType"].InnerText == "SinCos") newMathArtsFunction.FuncType = Ctl_MathArtsFunction.FuncTypes.SinCos;
                                    else if (mathArtsObjNode.Attributes["FuncType"].InnerText == "Gauss") newMathArtsFunction.FuncType = Ctl_MathArtsFunction.FuncTypes.Gauss;
                                    else if (mathArtsObjNode.Attributes["FuncType"].InnerText == "Garbor") newMathArtsFunction.FuncType = Ctl_MathArtsFunction.FuncTypes.Garbor;


                                    if (mathArtsObjNode.Attributes["FuncInverse"].InnerText == "True") newMathArtsFunction.FuncInverse = true;
                                    else if (mathArtsObjNode.Attributes["FuncInverse"].InnerText == "False") newMathArtsFunction.FuncInverse = false;

                                    lMathArtsObjs.Add(newMathArtsFunction);
                                }
                            }
                        }

                        //finally load all math art objects in display
                        this.MathArtsDisp_Container.LoadMathArtObjects(lMathArtsObjs);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Corrupted file - invalid format", "Error opening file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        #endregion

        #region form events

        private void Frm_MathArts_Load(object sender, EventArgs e)
        {
            #region debug
            showTracingDialog();
            #endregion
        }

        private void Frm_MathArts_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.MathArtsDisp_Container.Dispose();
        }

        private void Frm_MathArts_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                if (this.saved && this.saveFileName != "")
                {
                    this.saved = saveToXml(this.saveFileName);
                }
                else
                {
                    this.menuItem_Save_Click(this, EventArgs.Empty);
                }

                e.SuppressKeyPress = true;
            }
        }

        #endregion

        #region debug

        private MathArts.Debug.Frm_MathArtsObjTracing tracingDialog;

        [ConditionalAttribute("DEBUG")]
        private void showTracingDialog()
        {
            if (tracingDialog != null) tracingDialog.Dispose();
            tracingDialog = new MathArts.Debug.Frm_MathArtsObjTracing(this);
            tracingDialog.Show();
        } 
        #endregion
    }
}
