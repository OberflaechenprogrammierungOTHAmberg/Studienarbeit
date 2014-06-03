using MathArts.MathArtsColor;
using MathArts.MathArtsFunction;
using System;
using System.Collections.Generic;
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
        #endregion

        #region member
        private List<Ctl_MathArtsObject> displayedMathArtsObjects;
        private Ctl_MathArtsObject selectedMathArtsObj;
        #endregion 

        #region constructors
        public Frm_MathArtsObjTracing(Ctl_MathArtsDisp mainDisp)
        {
            InitializeComponent();
            mainDisp.Tracing_ValueChanged += mainDisp_Tracing_ValueChanged;
            displayedMathArtsObjects = new List<Ctl_MathArtsObject>();
            selectedMathArtsObj = null;
        }
        #endregion

        void mainDisp_Tracing_ValueChanged(object sender, Tracing_ValueChangedEventArgs e)
        {
            switch(e.ChangeType)
            {
                case(MathArts.Debug.Tracing_ValueChangedEventArgs.ValueChangeTypes.NewMathArtsObj):
                    displayedMathArtsObjects.Add(e.NewMathArtsObject);
                    Lb_MathArtObjs.Items.Add(e.NewMathArtsObject.ToString());

                    //select first item in listbox if only one exists so there will be one selected anytime
                    if (Lb_MathArtObjs.Items.Count == 1) Lb_MathArtObjs.SelectedIndex = 0;

                    break;
                case(MathArts.Debug.Tracing_ValueChangedEventArgs.ValueChangeTypes.MouseMovedDisp):
                    Lbl_MouseX.Text = LBL_MOUSE_X_INFORAMATION + e.X.ToString();
                    Lbl_MouseY.Text = LBL_MOUSE_Y_INFORAMATION + e.Y.ToString();
                    break;
                case (MathArts.Debug.Tracing_ValueChangedEventArgs.ValueChangeTypes.MathArtsObjShapeChanged):
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

                    Lbl_MathArtsObjMouseClickType.Text = LBL_MOUSE_CLICK_TYPE_INFORAMATION + (sender as Ctl_MathArtsObject).GetMouseClickType().ToString();
                    break;
                case (MathArts.Debug.Tracing_ValueChangedEventArgs.ValueChangeTypes.MathArtsObjValueChanged):
                    if (sender is Ctl_MathArtsColor)
                    {
                        Lbl_MathArtsObjTypeSpecific_1.Text = "Farbe:" + (sender as Ctl_MathArtsColor).Color.ToString();
                        Lbl_MathArtsObjTypeSpecific_2.Text = "Farbtyp:" + (sender as Ctl_MathArtsColor).ColType.ToString();
                    }
                    else if (sender is Ctl_MathArtsFunction)
                    {
                        Lbl_MathArtsObjTypeSpecific_1.Text = "Funkionstyp:" + (sender as Ctl_MathArtsFunction).FuncType.ToString();
                        Lbl_MathArtsObjTypeSpecific_2.Text = "Invertiert:" + ((sender as Ctl_MathArtsFunction).FuncInverse ? "Ja":"Nein");
                    }
                    break;
            }
            Refresh();
        }

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
                selectedMathArtsObj.BorderStyle = System.Windows.Forms.BorderStyle.None;

                if (selectedMathArtsObj is Ctl_MathArtsColor)
                {
                    (selectedMathArtsObj as Ctl_MathArtsColor).ValueChanged -= Frm_MathArtsObjTracing_ValueChanged;
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
            if (selectedMathArtsObj is Ctl_MathArtsColor)
            {
                (selectedMathArtsObj as Ctl_MathArtsColor).ValueChanged += Frm_MathArtsObjTracing_ValueChanged;
                Lbl_MathArtsObjTypeSpecific_1.Text = "Farbe:" + (selectedMathArtsObj as Ctl_MathArtsColor).Color.ToString();
                Lbl_MathArtsObjTypeSpecific_2.Text = "Farbtyp:" + (selectedMathArtsObj as Ctl_MathArtsColor).ColType.ToString();
            }
            else if (selectedMathArtsObj is Ctl_MathArtsFunction)
            {
                (selectedMathArtsObj as Ctl_MathArtsFunction).ValueChanged += Frm_MathArtsObjTracing_ValueChanged;
                Lbl_MathArtsObjTypeSpecific_1.Text = "Funkionstyp:" + (selectedMathArtsObj as Ctl_MathArtsFunction).FuncType.ToString();
                Lbl_MathArtsObjTypeSpecific_2.Text = "Invertiert:" + ((selectedMathArtsObj as Ctl_MathArtsFunction).FuncInverse ? "Ja" : "Nein");
            }

            //set all debug values for visualization
            if (selectedMathArtsObj is MathArtsColor.Ctl_MathArtsColor) selectedMathArtsObj.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            //update all shape depening labels
            Lbl_MathArtsObjWidth.Text           = LBL_WIDTH_INFORAMATION            + selectedMathArtsObj.Width.ToString();
            Lbl_MathArtsObjHeight.Text          = LBL_HEIGHT_INFORAMATION           + selectedMathArtsObj.Height.ToString();
            Lbl_MathArtsObjXPosition.Text       = LBL_X_POS_INFORAMATION            + selectedMathArtsObj.Location.X.ToString();
            Lbl_MathArtsObjYPosition.Text       = LBL_Y_POS_INFORAMATION            + selectedMathArtsObj.Location.Y.ToString();
            Lbl_MathArtsObjMouseClickType.Text  = LBL_MOUSE_CLICK_TYPE_INFORAMATION + selectedMathArtsObj.GetMouseClickType().ToString();


            //update all math art object type specific labels
            Lbl_MathArtsObjType.Text = LBL_TYPE_INFORAMATION + (selectedMathArtsObj is Ctl_MathArtsColor ? "MathArtsColor" : "MathArtsFunction");
        }

        void selectedMathArtsObj_ShapeValueChanged(object sender, EventArgs e)
        {
            mainDisp_Tracing_ValueChanged(sender, new Tracing_ValueChangedEventArgs((sender as Ctl_MathArtsObject), MathArts.Debug.Tracing_ValueChangedEventArgs.ValueChangeTypes.MathArtsObjShapeChanged));
        }

        void Frm_MathArtsObjTracing_ValueChanged(object sender, EventArgs e)
        {
            mainDisp_Tracing_ValueChanged(sender, new Tracing_ValueChangedEventArgs((sender as Ctl_MathArtsObject), MathArts.Debug.Tracing_ValueChangedEventArgs.ValueChangeTypes.MathArtsObjValueChanged));
        }

    }
}
