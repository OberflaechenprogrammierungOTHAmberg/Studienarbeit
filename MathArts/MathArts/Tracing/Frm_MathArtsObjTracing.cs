using MathArts.MathArtsColor;
using MathArts.MathArtsFunction;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MathArts.Debug
{
    public partial class Frm_MathArtsObjTracing : Form
    {
        #region constants
        private const string LBL_TYPE_INFORAMATION = "Type:";
        private const string LBL_MOUSE_X_INFORAMATION = "MausX:";
        private const string LBL_MOUSE_Y_INFORAMATION = "MausY:";
        private const string LBL_WIDTH_INFORAMATION = "Breite:";
        private const string LBL_HEIGHT_INFORAMATION = "Höhe:";
        private const string LBL_X_POS_INFORAMATION = "X:";
        private const string LBL_Y_POS_INFORAMATION = "Y:";
        private const string LBL_MOUSE_CLICK_TYPE_INFORAMATION = "Maus Kommando:";
        private const uint HIGHLIGHT_ALPHA_CHANNEL = 20;
        private const string LBL_BITMAP_INFORAMATION = "Bitmap";
        private const string LBL_COLOR_INFORAMATION = "Farbe:";

        private const string LBL_FUNC_RES_INFORAMATION = "Funktionsergebnis:";
        #endregion

        #region member
        private List<Ctl_MathArtsObject> displayedMathArtsObjects;
        private Ctl_MathArtsObject selectedMathArtsObj;
        #endregion 

        #region constructors
        /// <summary>
        /// Constructor for tracing receving math arts form which contains all objects taht should be traced
        /// </summary>
        /// <param name="mathArtsForm"></param>
        public Frm_MathArtsObjTracing(Frm_MathArts mathArtsForm)
        {
            this.Location = new Point(0, -mathArtsForm.Location.Y);
            InitializeComponent();
            mathArtsForm.MathArtsDispContainer.Tracing_ValueChanged += mainDisp_Tracing_ValueChanged;
            displayedMathArtsObjects = new List<Ctl_MathArtsObject>();
            selectedMathArtsObj = null;
        }
        #endregion


        #region event handler
        /// <summary>
        /// Main event handler for MathArtsObjTracing class 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void mainDisp_Tracing_ValueChanged(object sender, Tracing_ValueChangedEventArgs e)
        {

            //check which chnageType is selected by fired event
            switch (e.ChangeType)
            {
                case (MathArts.Debug.Tracing_ValueChangedEventArgs.ValueChangeTypes.NewMathArtsObj):
                    //if new MathArtsObj is created add it to internal list and to list box
                    displayedMathArtsObjects.Add(e.NewMathArtsObject);
                    Lb_MathArtObjs.Items.Add(e.NewMathArtsObject.ToString());

                    //select first item in listbox if only one exists so there will be one selected anytime
                    if (Lb_MathArtObjs.Items.Count == 1) Lb_MathArtObjs.SelectedIndex = 0;
                    break;

                case (MathArts.Debug.Tracing_ValueChangedEventArgs.ValueChangeTypes.MouseMovedDisp):
                    //update mouse tracing for MathArtsDisp
                    Lbl_MouseX.Text = LBL_MOUSE_X_INFORAMATION + e.X.ToString();
                    Lbl_MouseY.Text = LBL_MOUSE_Y_INFORAMATION + e.Y.ToString();

                    Lbl_MathArtsDispBitmapWidth.Text = LBL_BITMAP_INFORAMATION + " " + LBL_WIDTH_INFORAMATION + e.DisplayContainer.Width + " " + e.DisplayContainer.bitMap.Width;
                    Lbl_MathArtsDispBitmapHeight.Text = LBL_BITMAP_INFORAMATION + " " + LBL_HEIGHT_INFORAMATION + e.DisplayContainer.Height + " " + e.DisplayContainer.bitMap.Height;

                    Lbl_MathArtsDispColor.Text = LBL_COLOR_INFORAMATION + e.DisplayContainer.ColorFromVal(e.X, e.Y, e.DisplayContainer.GetFuncValue(e.X, e.Y));
                    Lbl_MathArtsDispFuncResult.Text = LBL_FUNC_RES_INFORAMATION + e.DisplayContainer.GetFuncValue(e.X, e.Y);
                    break;

                case (MathArts.Debug.Tracing_ValueChangedEventArgs.ValueChangeTypes.MathArtsObjShapeChanged):
                    //depending on mouse click type update labels
                    if ((sender as Ctl_MathArtsObject).GetMouseClickType() == MathArts.Ctl_MathArtsObject.MouseClickTypes.Resize)
                    {
                        Lbl_MathArtsObjWidth.Text = LBL_WIDTH_INFORAMATION + (sender as Ctl_MathArtsObject).Width.ToString();
                        Lbl_MathArtsObjHeight.Text = LBL_HEIGHT_INFORAMATION + (sender as Ctl_MathArtsObject).Height.ToString();
                    }
                    else if ((sender as Ctl_MathArtsObject).GetMouseClickType() == MathArts.Ctl_MathArtsObject.MouseClickTypes.Move)
                    {
                        Lbl_MathArtsObjXPosition.Text = LBL_X_POS_INFORAMATION + (sender as Ctl_MathArtsObject).Location.X.ToString();
                        Lbl_MathArtsObjYPosition.Text = LBL_Y_POS_INFORAMATION + (sender as Ctl_MathArtsObject).Location.Y.ToString();
                    }

                    //update shape informations
                    Lbl_MathArtsObjMouseClickType.Text = LBL_MOUSE_CLICK_TYPE_INFORAMATION + (sender as Ctl_MathArtsObject).GetMouseClickType().ToString();
                    Lbl_MathArtsObjMouseX.Text = LBL_MOUSE_X_INFORAMATION + e.MousePosition.X.ToString();
                    Lbl_MathArtsObjMouseY.Text = LBL_MOUSE_Y_INFORAMATION + e.MousePosition.Y.ToString();

                    //override mouse position displayed for MathArtsDisp while mouse is over selected math art object
                    Lbl_MouseX.Text = LBL_MOUSE_X_INFORAMATION + ((sender as Ctl_MathArtsObject).Location.X + e.MousePosition.X).ToString();
                    Lbl_MouseY.Text = LBL_MOUSE_Y_INFORAMATION + ((sender as Ctl_MathArtsObject).Location.Y + e.MousePosition.Y).ToString();

                    //update function value if math arts object is function and object is not resizing or moving currently
                    if (sender is Ctl_MathArtsFunction
                        && (sender as Ctl_MathArtsObject).GetMouseClickType() == MathArts.Ctl_MathArtsObject.MouseClickTypes.None) Lbl_MathArtsObjTypeSpecific_3.Text = "Funktionswert:" + (sender as Ctl_MathArtsFunction).GetFuncValFromArray(e.MousePosition.X, e.MousePosition.Y).ToString("0.######");
                    break;

                case (MathArts.Debug.Tracing_ValueChangedEventArgs.ValueChangeTypes.MathArtsObjValueChanged):
                    //depending on math arts object type update specific math art object labels
                    if (sender is Ctl_MathArtsColor)
                    {
                        Lbl_MathArtsObjTypeSpecific_1.Text = LBL_COLOR_INFORAMATION + (sender as Ctl_MathArtsColor).Color.ToString();
                        Lbl_MathArtsObjTypeSpecific_2.Text = "Farbtyp:" + (sender as Ctl_MathArtsColor).ColType.ToString();
                        Lbl_MathArtsObjTypeSpecific_3.Text = "";
                    }
                    else if (sender is Ctl_MathArtsFunction)
                    {
                        Lbl_MathArtsObjTypeSpecific_1.Text = "Funkionstyp:" + (sender as Ctl_MathArtsFunction).FuncType.ToString();
                        Lbl_MathArtsObjTypeSpecific_2.Text = "Invertiert:" + ((sender as Ctl_MathArtsFunction).FuncInverse ? "Ja" : "Nein");
                    }
                    break;
            }
                
                Refresh();
        }

        /// <summary>
        /// Event handler handling ShapeValueChange events parsing it to mainDisp_Tracing_ValueChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void selectedMathArtsObj_ShapeValueChanged(object sender, EventArgs e)
        {
            mainDisp_Tracing_ValueChanged(sender, new Tracing_ValueChangedEventArgs((sender as Ctl_MathArtsObject), MathArts.Debug.Tracing_ValueChangedEventArgs.ValueChangeTypes.MathArtsObjShapeChanged, new Point((e as MouseEventArgs).X, (e as MouseEventArgs).Y)));
        }

        /// <summary>
        /// Event handler handling ValueChanged events parsing it to mainDisp_Tracing_ValueChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Frm_MathArtsObjTracing_ValueChanged(object sender, EventArgs e)
        {
            mainDisp_Tracing_ValueChanged(sender, new Tracing_ValueChangedEventArgs((sender as Ctl_MathArtsObject), MathArts.Debug.Tracing_ValueChangedEventArgs.ValueChangeTypes.MathArtsObjValueChanged));
        }
        #endregion 

        #region GUI events
        /// <summary>
        /// Event handling if selected item in list box changes - subscripe to its ValueChanged and ShapeValueChanged events 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Lv_MathArtObjs_SelectedIndexChanged(object sender, EventArgs e)
        {
            int mathArtsObjIndex;
            int.TryParse(Lb_MathArtObjs.SelectedItem.ToString().Split('_').Last(), out mathArtsObjIndex);
            Gb_MathArtsObjProperties.Text = Lb_MathArtObjs.SelectedItem.ToString();

            //reset all debug value for visualization and values if an object was selected before and
            //unsubscribe events
            if (selectedMathArtsObj != null)
            {
                selectedMathArtsObj.ShapeValueChanged -= selectedMathArtsObj_ShapeValueChanged;
                //selectedMathArtsObj.BorderStyle = System.Windows.Forms.BorderStyle.None;
                selectedMathArtsObj.BackColor = Color.Transparent;

                if (selectedMathArtsObj is Ctl_MathArtsColor)
                {
                    (selectedMathArtsObj as Ctl_MathArtsColor).ValueChanged -= Frm_MathArtsObjTracing_ValueChanged;
                    Lbl_MathArtsObjTypeSpecific_3.Text = "";
                }
                else if (selectedMathArtsObj is Ctl_MathArtsFunction)
                {
                    (selectedMathArtsObj as Ctl_MathArtsFunction).ValueChanged -= Frm_MathArtsObjTracing_ValueChanged;
                }
            }

            //get selected math arts object by chosen index 
            selectedMathArtsObj=displayedMathArtsObjects[mathArtsObjIndex];

            //subscribe to new math arts object events and set specific value labels
            selectedMathArtsObj.ShapeValueChanged += selectedMathArtsObj_ShapeValueChanged;

            //highlight by semi transparent backcolor
            selectedMathArtsObj.BackColor = Color.FromArgb((int)HIGHLIGHT_ALPHA_CHANNEL, 255, 255, 0);

            if (selectedMathArtsObj is Ctl_MathArtsColor)
            {
                (selectedMathArtsObj as Ctl_MathArtsColor).ValueChanged += Frm_MathArtsObjTracing_ValueChanged;
                Lbl_MathArtsObjTypeSpecific_1.Text = "Farbe:"   + (selectedMathArtsObj as Ctl_MathArtsColor).Color.ToString();
                Lbl_MathArtsObjTypeSpecific_2.Text = "Farbtyp:" + (selectedMathArtsObj as Ctl_MathArtsColor).ColType.ToString();
                Lbl_MathArtsObjTypeSpecific_3.Text = "";
            }
            else if (selectedMathArtsObj is Ctl_MathArtsFunction)
            {
                (selectedMathArtsObj as Ctl_MathArtsFunction).ValueChanged += Frm_MathArtsObjTracing_ValueChanged;
                Lbl_MathArtsObjTypeSpecific_1.Text = "Funkionstyp:"     + (selectedMathArtsObj as Ctl_MathArtsFunction).FuncType.ToString();
                Lbl_MathArtsObjTypeSpecific_2.Text = "Invertiert:"      + ((selectedMathArtsObj as Ctl_MathArtsFunction).FuncInverse ? "Ja" : "Nein");
                Lbl_MathArtsObjTypeSpecific_3.Text = "Funktionswert:"   + ((selectedMathArtsObj as Ctl_MathArtsFunction).GetFuncValFromArray(selectedMathArtsObj.Location.X,selectedMathArtsObj.Location.Y).ToString());
            }

            //set all debug values for visualization
            //if (selectedMathArtsObj is MathArtsColor.Ctl_MathArtsColor) selectedMathArtsObj.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            //update all shape depening labels
 
            Lbl_MathArtsObjWidth.Text = LBL_WIDTH_INFORAMATION + selectedMathArtsObj.Width.ToString();
            Lbl_MathArtsObjHeight.Text = LBL_HEIGHT_INFORAMATION + selectedMathArtsObj.Height.ToString();
            Lbl_MathArtsObjXPosition.Text = LBL_X_POS_INFORAMATION + selectedMathArtsObj.Location.X.ToString();
            Lbl_MathArtsObjYPosition.Text = LBL_Y_POS_INFORAMATION + selectedMathArtsObj.Location.Y.ToString();
            Lbl_MathArtsObjMouseClickType.Text = LBL_MOUSE_CLICK_TYPE_INFORAMATION + selectedMathArtsObj.GetMouseClickType().ToString();


            //update all math art object type specific labels
            Lbl_MathArtsObjType.Text = LBL_TYPE_INFORAMATION + (selectedMathArtsObj is Ctl_MathArtsColor ? "MathArtsColor" : "MathArtsFunction");
            }
    }
        #endregion
}

