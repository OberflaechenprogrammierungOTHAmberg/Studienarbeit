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

using MathArts.MathArtsColor;
using MathArts.MathArtsFunction;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace MathArts
{
    /// <summary>
    /// Container for all MathArts objects
    /// </summary>
    public partial class Ctl_MathArtsDisp : UserControl
    {
        #region constants
        private const uint COLOR_DIMENSIONS = 3;
        private const uint COLOR_RED = 0;
        private const uint COLOR_GREEN = 1;
        private const uint COLOR_BLUE = 2;
        
        #endregion
        #region member
        private List<Ctl_MathArtsObject> allContainedMathArtsObjects;
        public Bitmap bitMap;
        private byte[,,] valHighArr;
        private byte[, ,] valLowArr;
        private double[,] valFuncArr;

        private uint colorModulator=1;

        public uint ColorModulator
        {
            get { return colorModulator; }
            set { if(value<=256 && value>0) colorModulator=value; }
        }

        //create timer which is running in separate thread
        private static System.Timers.Timer aTimer = new System.Timers.Timer();
        #endregion

        #region constructors
        /// <summary>
        /// Default constructor initializing GUI and MathArts object list
        /// </summary>
        public Ctl_MathArtsDisp()
        {
            InitializeComponent();
            valHighArr = new byte[this.Width, this.Height, COLOR_DIMENSIONS];
            valLowArr = new byte[this.Width, this.Height, COLOR_DIMENSIONS];
            valFuncArr = new double[this.Width, this.Height];

            // Set the Interval to 100ms -> 10 updates per second while moving.
            aTimer.Interval = 100;
            aTimer.Enabled = true;

            this.allContainedMathArtsObjects = new List<Ctl_MathArtsObject>();

            Ctl_MathArtsDisp_ValueChanged(this, EventArgs.Empty);
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
            this.Controls.Add(_object);
            this.allContainedMathArtsObjects.Add(_object);
            _object.subscribeToTimer(aTimer);

            if (_object is Ctl_MathArtsFunction) (_object as Ctl_MathArtsFunction).ValueChanged += Ctl_MathArtsDisp_ValueChanged;
            if (_object is Ctl_MathArtsColor) (_object as Ctl_MathArtsColor).ValueChanged += Ctl_MathArtsDisp_ValueChanged;
            _object.ShapeValueChanged += Ctl_MathArtsDisp_ValueChanged;
            _object.subscribeToTimer(aTimer);

            Ctl_MathArtsDisp_ValueChanged(_object, EventArgs.Empty);

            #region debug
            Tracing_TriggerValueChanged(_object);
            #endregion
        }

        public void ClearWorkspace()
        {
            this.allContainedMathArtsObjects.Clear();
            this.Controls.Clear();

            this.colorModulator = 1;

            valHighArr = new byte[this.Width, this.Height, COLOR_DIMENSIONS];
            valLowArr = new byte[this.Width, this.Height, COLOR_DIMENSIONS];
            valFuncArr = new double[this.Width, this.Height];

            this.UpdateColorArray();
            this.UpdateFuncValArray();
            Ctl_MathArtsDisp_ValueChanged(this, EventArgs.Empty);
        }

        public void LoadMathArtObjects(List<Ctl_MathArtsObject> _lMathArtObjs)
        {
            foreach(Ctl_MathArtsObject _object in _lMathArtObjs)
            {
                this.Controls.Add(_object);
                this.allContainedMathArtsObjects.Add(_object);

               
                if (_object is Ctl_MathArtsFunction) (_object as Ctl_MathArtsFunction).ValueChanged += Ctl_MathArtsDisp_ValueChanged;
                if (_object is Ctl_MathArtsColor) (_object as Ctl_MathArtsColor).ValueChanged += Ctl_MathArtsDisp_ValueChanged;
                _object.ShapeValueChanged += Ctl_MathArtsDisp_ValueChanged;
                _object.subscribeToTimer(aTimer);

                #region debug
                Tracing_TriggerValueChanged(_object);
                #endregion
            }

            this.UpdateColorArray();
            this.UpdateFuncValArray();
            this.Ctl_MathArtsDisp_ValueChanged(this, EventArgs.Empty);

        }

        public void DisplayDemo1()
        {
            ClearWorkspace();

            Ctl_MathArtsColor colblack = new Ctl_MathArtsColor(155, 70);
            colblack.Color = Color.Black;
            colblack.ColType = Ctl_MathArtsColor.ColTypes.High;

            Ctl_MathArtsColor colred = new Ctl_MathArtsColor(155, 140);
            colred.Color = Color.Red;
            colred.ColType = Ctl_MathArtsColor.ColTypes.High;

            Ctl_MathArtsColor colyellow = new Ctl_MathArtsColor(155, 210);
            colyellow.Color = Color.Yellow;
            colyellow.ColType = Ctl_MathArtsColor.ColTypes.High;

            Ctl_MathArtsFunction funcSinCos = new Ctl_MathArtsFunction(5, 5);
            funcSinCos.Width= 325;
            funcSinCos.Height= 280;
            funcSinCos.FuncType = Ctl_MathArtsFunction.FuncTypes.SinCos;
            funcSinCos.FuncInverse = true;

            //  add it to internal list
            this.allContainedMathArtsObjects.Add(colblack);
            this.allContainedMathArtsObjects.Add(colred);
            this.allContainedMathArtsObjects.Add(colyellow);

            this.allContainedMathArtsObjects.Add(funcSinCos);

            //  subscribe to math arts object events
            this.allContainedMathArtsObjects.ForEach(n => n.subscribeToTimer(aTimer));
            this.allContainedMathArtsObjects.ForEach(n => n.ShapeValueChanged += Ctl_MathArtsDisp_ValueChanged);
            this.allContainedMathArtsObjects.Where(n => n is Ctl_MathArtsColor).ToList().ForEach(n => (n as Ctl_MathArtsColor).ValueChanged+= Ctl_MathArtsDisp_ValueChanged);
            this.allContainedMathArtsObjects.Where(n => n is Ctl_MathArtsFunction).ToList().ForEach(n => (n as Ctl_MathArtsFunction).ValueChanged += Ctl_MathArtsDisp_ValueChanged);

            //  add to control list
            this.allContainedMathArtsObjects.ForEach(n => this.Controls.Add(n));
            this.UpdateColorArray();
            this.UpdateFuncValArray();

            #region debug
            this.allContainedMathArtsObjects.ForEach(n => Tracing_TriggerValueChanged(n));
            #endregion

            this.Ctl_MathArtsDisp_ValueChanged(this, EventArgs.Empty);
        }

        public void RefreshDisplay()
        {
            Ctl_MathArtsDisp_ValueChanged(this, EventArgs.Empty);
        }

        /// <summary>
        /// Calculates the color value at a position inside the container
        /// </summary>
        /// <param name="_x">X position</param>
        /// <param name="_y">Y position</param>
        /// <param name="val">Value calculated from product of functions</param>
        public Color ColorFromVal(int _x, int _y, double _val)
        {
            #region slow calculation of color values
            /*
            //need temporary list because explicit cast of lambda expression result from List<Ctl_MathArtsObj> to List<Ctl_MathArtsColor> is not possible
            List<Ctl_MathArtsColor> lMathArtsColors_High = new List<Ctl_MathArtsColor>();
            this.allContainedMathArtsObjects.Where(n => n is Ctl_MathArtsColor && (n as Ctl_MathArtsColor).ColType == Ctl_MathArtsColor.ColTypes.High).ToList().ForEach(n => lMathArtsColors_High.Add(n as Ctl_MathArtsColor));
            Color CHigh=CalculateColor(lMathArtsColors_High,_x,_y);

            List<Ctl_MathArtsColor> lMathArtsColors_Low = new List<Ctl_MathArtsColor>();
            this.allContainedMathArtsObjects.Where(n => n is Ctl_MathArtsColor && (n as Ctl_MathArtsColor).ColType == Ctl_MathArtsColor.ColTypes.Low).ToList().ForEach(n => lMathArtsColors_Low.Add(n as Ctl_MathArtsColor));
            Color CLow = CalculateColor(lMathArtsColors_Low, _x, _y);
            */
            #endregion

            #region old
            //Berechnung
            /*
            double IntensityRateNorm_Maximum = 0.0;
            double RedColorIntensity_Maximum = 0.0;
            double GreenColorIntensity_Maximum = 0.0;
            double BlueColorIntensity_Maximum = 0.0;
            
            double IntensityRateNorm_Minimum = 0.0;
            double RedColorIntensity_Minimum = 0.0;
            double GreenColorIntensity_Minimum = 0.0;
            double BlueColorIntensity_Minimum = 0.0;

            foreach (Ctl_MathArtsObject _object in this.allContainedMathArtsObjects.Where(n => n is Ctl_MathArtsColor).ToList())
            {
                if (((Ctl_MathArtsColor)_object).ColType == Ctl_MathArtsColor.ColTypes.High)
                {
                    double IntensityRate = CalculateIntensityRate(_object as Ctl_MathArtsColor, _x, _y);
                    IntensityRateNorm_Maximum += IntensityRate;
                    RedColorIntensity_Maximum += IntensityRate * (int)((Ctl_MathArtsColor)_object).Color.R;
                    GreenColorIntensity_Maximum += IntensityRate * (int)((Ctl_MathArtsColor)_object).Color.G;
                    BlueColorIntensity_Maximum += IntensityRate * (int)((Ctl_MathArtsColor)_object).Color.B;
                }
                else if (((Ctl_MathArtsColor)_object).ColType == Ctl_MathArtsColor.ColTypes.Low)
                {
                    double IntensityRate = CalculateIntensityRate(_object as Ctl_MathArtsColor, _x, _y);
                    IntensityRateNorm_Minimum += IntensityRate;
                    RedColorIntensity_Minimum += IntensityRate * (int)((Ctl_MathArtsColor)_object).Color.R;
                    GreenColorIntensity_Minimum += IntensityRate * (int)((Ctl_MathArtsColor)_object).Color.G;
                    BlueColorIntensity_Minimum += IntensityRate * (int)((Ctl_MathArtsColor)_object).Color.B;
                }
            }

            double RedColor_Maximum = 0.0;
            double GreenColor_Maximum = 0.0;
            double BlueColor_Maximum = 0.0;

            if (this.allContainedMathArtsObjects.Where(n => n is Ctl_MathArtsColor && ((Ctl_MathArtsColor)n).ColType == Ctl_MathArtsColor.ColTypes.High).ToList().Count > 0)
            {
                RedColor_Maximum = RedColorIntensity_Maximum / IntensityRateNorm_Maximum;
                GreenColor_Maximum = GreenColorIntensity_Maximum / IntensityRateNorm_Maximum;
                BlueColor_Maximum = BlueColorIntensity_Maximum / IntensityRateNorm_Maximum;

            }

            double RedColor_Minimum = 0.0;
            double GreenColor_Minimum = 0.0;
            double BlueColor_Minimum = 0.0;

            if (this.allContainedMathArtsObjects.Where(n => n is Ctl_MathArtsColor && ((Ctl_MathArtsColor)n).ColType == Ctl_MathArtsColor.ColTypes.Low).ToList().Count > 0)
            {
                RedColor_Minimum = RedColorIntensity_Minimum / IntensityRateNorm_Minimum;
                GreenColor_Minimum = GreenColorIntensity_Minimum / IntensityRateNorm_Minimum;
                BlueColor_Minimum = BlueColorIntensity_Minimum / IntensityRateNorm_Minimum;
            }
             * */
            #endregion

            #region slow calculation of color values
            /*
            byte RedColor = (byte)(((int)Math.Abs(_val * CHigh.R + (1 - _val) * CLow.R)) % 256);
            byte GreenColor = (byte)(((int)Math.Abs(_val * CHigh.G + (1 - _val) * CLow.G)) % 256);
            byte BlueColor = (byte)(((int)Math.Abs(_val * CHigh.B + (1 - _val) * CLow.B)) % 256);
            */
            #endregion

            byte RedColor = (byte)(((int)Math.Abs(_val * valHighArr[_x, _y, COLOR_RED] + (1 - _val) * valLowArr[_x, _y, COLOR_RED])) * colorModulator % 256);
            byte GreenColor = (byte)(((int)Math.Abs(_val * valHighArr[_x, _y, COLOR_GREEN] + (1 - _val) * valLowArr[_x, _y, COLOR_GREEN])) * colorModulator % 256);
            byte BlueColor = (byte)(((int)Math.Abs(_val * valHighArr[_x, _y, COLOR_BLUE] + (1 - _val) * valLowArr[_x, _y, COLOR_BLUE])) * colorModulator % 256);

            return Color.FromArgb(RedColor, GreenColor, BlueColor);
        }

        #endregion

        #region private functions

        private void UpdateFuncValArray()
        {
            for (int x = 0; x < this.Width; x++)
            {
                    for (int y = 0; y < this.Height; y++)
                    {
                        valFuncArr[x, y] = CalculateFunctionValue(x, y);
                    }
            }
        }

        private void UpdateColorArray()
        {
            UpdateColorArray(MathArtsColor.Ctl_MathArtsColor.ColTypes.Low);
            UpdateColorArray(MathArtsColor.Ctl_MathArtsColor.ColTypes.High);
        }

        private void UpdateColorArray(MathArtsColor.Ctl_MathArtsColor.ColTypes _coltype)
        {
            if (_coltype == MathArtsColor.Ctl_MathArtsColor.ColTypes.Low)
            {
                List<Ctl_MathArtsColor> lMathArtsColors_Low = new List<Ctl_MathArtsColor>();
                this.allContainedMathArtsObjects.Where(n => n is Ctl_MathArtsColor && (n as Ctl_MathArtsColor).ColType == Ctl_MathArtsColor.ColTypes.Low).ToList().ForEach(n => lMathArtsColors_Low.Add(n as Ctl_MathArtsColor));

                for (int x = 0; x < this.Width; x++)
                {
                    for (int y = 0; y < this.Height; y++)
                    {
                        Color CLow = CalculateColor(lMathArtsColors_Low, x, y);
                        valLowArr[x, y, COLOR_RED] = CLow.R;
                        valLowArr[x, y, COLOR_GREEN] = CLow.G;
                        valLowArr[x, y, COLOR_BLUE] = CLow.B;
                    }
                }
            }
            else if (_coltype == MathArtsColor.Ctl_MathArtsColor.ColTypes.High)
            {
                List<Ctl_MathArtsColor> lMathArtsColors_High = new List<Ctl_MathArtsColor>();
                this.allContainedMathArtsObjects.Where(n => n is Ctl_MathArtsColor && (n as Ctl_MathArtsColor).ColType == Ctl_MathArtsColor.ColTypes.High).ToList().ForEach(n => lMathArtsColors_High.Add(n as Ctl_MathArtsColor));

                for (int x = 0; x < this.Width; x++)
                {
                    for (int y = 0; y < this.Height; y++)
                    {
                        Color CHigh = CalculateColor(lMathArtsColors_High, x, y);
                        valHighArr[x, y, COLOR_RED] = CHigh.R;
                        valHighArr[x, y, COLOR_GREEN] = CHigh.G;
                        valHighArr[x, y, COLOR_BLUE] = CHigh.B;
                    }
                }

            }
        }

        //only public until we have array...
        public double CalculateFunctionValue(int _x, int _y)
        {
            double funcResult = 1.0;

            foreach (Ctl_MathArtsFunction _object in this.allContainedMathArtsObjects.Where(n => n is Ctl_MathArtsFunction).ToList())
            {

                if (_x >= _object.Location.X
                    && _x < _object.Location.X + _object.Width
                    && _y >= _object.Location.Y
                    && _y < _object.Location.Y + _object.Height) funcResult *= _object.GetFuncValFromArray(_x - _object.Location.X, _y - _object.Location.Y);
            }

            return funcResult;
        }

        /// <summary>
        /// calculate color regarding to expression in 4a) for a list of MathArtsColor
        /// </summary>
        /// <param name="_lMathArtsColors">list of math art color objects</param>
        /// <param name="_x">X position</param>
        /// <param name="_y">Y position</param>
        /// <returns></returns>
        private Color CalculateColor(List<Ctl_MathArtsColor> _lMathArtsColors,int _x,int _y)
        {

            if (_lMathArtsColors.Count == 0) return Color.FromArgb(0, 0, 0);

            double NormValue = 0.0;
            double RedColorIntensity = 0.0;
            double GreenColorIntensity = 0.0;
            double BlueColorIntensity = 0.0;

            foreach (Ctl_MathArtsColor _object in _lMathArtsColors)
            {
                double IntensityRate = CalculateIntensityRate(_object, _x, _y);
                NormValue += IntensityRate;
                RedColorIntensity   += IntensityRate * (int)((Ctl_MathArtsColor)_object).Color.R;
                GreenColorIntensity += IntensityRate * (int)((Ctl_MathArtsColor)_object).Color.G;
                BlueColorIntensity  += IntensityRate * (int)((Ctl_MathArtsColor)_object).Color.B;
            }

            return Color.FromArgb(  (byte)((RedColorIntensity   / NormValue) % 256),
                                    (byte)((GreenColorIntensity / NormValue) % 256),
                                    (byte)((BlueColorIntensity  / NormValue) % 256)); ;
        }

        /// <summary>
        /// Calculates the intensity rate at a specific point for the MathArtsColor object.
        /// </summary>
        /// <param name="_object">MathArtsColor object</param>
        /// <param name="_x">X coordinate</param>
        /// <param name="_y">Y coordinate</param>
        private double CalculateIntensityRate(Ctl_MathArtsColor _object, int _x, int _y)
        {
            int midx = _object.Location.X + (_object.Width) / 2 - 1;
            double dx = ((midx * 1.0) - _x) / _object.Width;

            int midy = _object.Location.Y + (_object.Height) / 2 - 1;
            double dy = ((midy * 1.0) - _y) / _object.Height;

            return Math.Exp(-(dx * dx + dy * dy) / 10);
        }


        public XmlDocument SaveMathArtsDisp(XmlDocument _doc)
        {
            XmlNode MathArtsDispNode = _doc.DocumentElement.AppendChild(_doc.CreateElement("MathArtsDisp"));

            MathArtsDispNode.Attributes.Append(_doc.CreateAttribute("Width")).Value = this.Width.ToString();
            MathArtsDispNode.Attributes.Append(_doc.CreateAttribute("Height")).Value = this.Height.ToString();
            MathArtsDispNode.Attributes.Append(_doc.CreateAttribute("ColorModulator")).Value = this.colorModulator.ToString();

            XmlNode MathArtsObjsNode=MathArtsDispNode.AppendChild(_doc.CreateElement("MathArtsObjects"));

            XmlNode currentNode;
            this.allContainedMathArtsObjects.ForEach(n => n.SaveMathArtsObj(_doc, MathArtsObjsNode, out currentNode));

            return _doc;
        }
        #endregion

        #region GUI event methods
        void Ctl_MathArtsDisp_ValueChanged(object sender, EventArgs e)
        {
            //first try - calculate image for whole disp 
            if(bitMap == null) bitMap = new Bitmap(this.Width, this.Height);

            
            if (sender is Ctl_MathArtsColor)
            {
                //if col type has changed 
                if ((e is MathArtsColorValueChangedEventArgs) && (e as MathArtsColorValueChangedEventArgs).ChangeType == MathArtsColorValueChangedEventArgs.ValueChangeTypes.ColType)
                {
                    UpdateColorArray();
                }
                else
                {
                    UpdateColorArray((sender as Ctl_MathArtsColor).ColType);
                }
            }
            else if (sender is Ctl_MathArtsFunction) UpdateFuncValArray();

            for (int x = 0; x < this.Width; x++)
            {
                for (int y = 0; y < this.Height; y++)
                {
                    bitMap.SetPixel(x, y, ColorFromVal(x, y, valFuncArr[x, y]));
                }
            }
            Refresh();
        }

        private void Ctl_MathArtsDisp_Resize(object sender, EventArgs e)
        {
            //first try - calculate image for whole disp 
            //only create new Bitmap if disp size is greater 0 -> main frame minimized
            if (this.Width>0 && this.Height>0)
            {
                bitMap.Dispose();
                bitMap = new Bitmap(this.Width, this.Height);

                valHighArr = new byte[this.Width, this.Height, COLOR_DIMENSIONS];
                valLowArr = new byte[this.Width, this.Height, COLOR_DIMENSIONS];
                valFuncArr = new double[this.Width, this.Height];

                UpdateColorArray();
                UpdateFuncValArray();
                Ctl_MathArtsDisp_ValueChanged(sender, e);
            }

        }

        /// <summary>
        /// Paint method of the container. Paints all MathArts objects inside the container
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ctl_MathArtsDisp_Paint(object sender, PaintEventArgs e)
        {
            #region old
            /*
            foreach (Ctl_MathArtsObject item in this.allContainedMathArtsObjects)
            {
                if (!this.Controls.Contains(item))
                {
                    this.Controls.Add(item); 
                }
            }
            */
            #endregion

            e.Graphics.DrawImage(bitMap, this.Location.X, this.Location.Y);
        }
        #endregion

        #region debug
        private void Ctl_MathArtsDisp_MouseMove(object sender, MouseEventArgs e)
        {
            if (Tracing_ValueChanged != null) Tracing_ValueChanged(this, new MathArts.Debug.Tracing_ValueChangedEventArgs(this,e.X, e.Y));
        }
        
        public delegate void Tracing_ValueChangedEventHandler(object sender, MathArts.Debug.Tracing_ValueChangedEventArgs e);
        public event Tracing_ValueChangedEventHandler Tracing_ValueChanged;

        [ConditionalAttribute("DEBUG")]
        private void Tracing_TriggerValueChanged(Ctl_MathArtsObject _object)
        {
            if (Tracing_ValueChanged != null) Tracing_ValueChanged(this, new MathArts.Debug.Tracing_ValueChangedEventArgs(_object));
        }

        #endregion
    }

}