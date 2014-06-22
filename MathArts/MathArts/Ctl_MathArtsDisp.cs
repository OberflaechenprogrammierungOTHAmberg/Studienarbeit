/////////////////////////////////////////////////////////////////////////////
// <copyright file="Ctl_MathArtsDisp.cs">
// Copyright (c) 2014
// </copyright>
//
// <author>Betting Pascal, Schneider Mathias, Schlemelch Manuel</author>
// <date>22-06-2014</date>
//
// <professor>Prof. Dr. Josef Poesl</professor>
// <studyCourse>Angewandte Informatik</studyCourse>
// <branchOfStudy>Industrieinformatik</branchOfStudy>
// <subject>Oberflaechenprogrammierung</subject>
/////////////////////////////////////////////////////////////////////////////

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
        private const int DEFAULT_TIMERINTERVAL=50;
        private const int DEFAULT_WIDTH = 352;
        private const int DEFAULT_HEIGHT= 351;
        #endregion

        #region member
        private List<Ctl_MathArtsObject> allContainedMathArtsObjects;
        public Bitmap bitMap;
        private double[, ,] valHighArr;
        private double[, ,] valLowArr;
        private double[,] valFuncArr;
        private double colorModulator=1;
        //create timer which is running in separate thread
        private static System.Timers.Timer aTimer = new System.Timers.Timer();
        private bool useDefaultTimer = true;
        private uint timerInterval;
        private uint defaultTimerInterval;
        private bool useTimer = false;
        #endregion

        #region constructors
        /// <summary>
        /// Default constructor initializing GUI and MathArts object list
        /// </summary>
        public Ctl_MathArtsDisp()
        {
            InitializeComponent();
            valHighArr = new double[this.Width, this.Height, COLOR_DIMENSIONS];
            valLowArr = new double[this.Width, this.Height, COLOR_DIMENSIONS];
            valFuncArr = new double[this.Width, this.Height];

            useTimer = false;
            useDefaultTimer = true;
            
            // Set the timer depeding on math arts display size
            TimerInterval = (uint)(DEFAULT_TIMERINTERVAL + 0.5 * DEFAULT_TIMERINTERVAL * (this.Width / DEFAULT_WIDTH) * (this.Height / DEFAULT_HEIGHT));
            defaultTimerInterval = timerInterval;
            aTimer.Enabled = true;

            this.allContainedMathArtsObjects = new List<Ctl_MathArtsObject>();

            Ctl_MathArtsDisp_ValueChanged(this, EventArgs.Empty);
        }
        #endregion

        #region properties
        public double ColorModulator
        {
            get { return colorModulator; }
            set { if (value <= 256 && value > 0) colorModulator = value; }
        }
        public bool UseDefaultTimer
        {
            get { return useDefaultTimer; }
            set { useDefaultTimer = value; }
        }
        public uint TimerInterval
        {
            get { return timerInterval; }
            set
            {
                if (useDefaultTimer)
                {
                    aTimer.Interval = (int)(DEFAULT_TIMERINTERVAL + 0.5 * DEFAULT_TIMERINTERVAL * (this.Width * 1.0 / DEFAULT_WIDTH * 1.0) * (this.Height * 1.0 / DEFAULT_HEIGHT * 1.0));
                    timerInterval = (uint)aTimer.Interval;
                }
                else
                {
                    aTimer.Interval = value;
                    timerInterval = value;
                }
            }
        }
        public uint DefaultTimerInterval
        {
            get { return defaultTimerInterval; }
            set { }
        }
        public bool UseTimer
        {
            get { return useTimer; }
            set
            {
                if (useTimer != value)
                {
                    useTimer = value;
                    Ctl_MathArtsObject.UseTimer = value;
                }

            }
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

            //  subscribe to events
            if (_object is Ctl_MathArtsFunction) (_object as Ctl_MathArtsFunction).ValueChanged += Ctl_MathArtsDisp_ValueChanged;
            if (_object is Ctl_MathArtsColor) (_object as Ctl_MathArtsColor).ValueChanged += Ctl_MathArtsDisp_ValueChanged;
            _object.ShapeValueChanged += Ctl_MathArtsDisp_ValueChanged;
            _object.subscribeToTimer(aTimer);

            Ctl_MathArtsDisp_ValueChanged(_object, EventArgs.Empty);

            #region debug
            Tracing_TriggerValueChanged(_object);
            #endregion
        }

        /// <summary>
        /// Adds a list of new MathArts objects to the container
        /// </summary>
        /// <param name="_lMathArtsObjs">list of MathArts objects to add</param>
        public void AddMathArtsObject(List<Ctl_MathArtsObject> _lMathArtObjs)
        {
            foreach (Ctl_MathArtsObject _object in _lMathArtObjs)
            {
                this.Controls.Add(_object);
                this.allContainedMathArtsObjects.Add(_object);

                //  subscribe to events
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

        /// <summary>
        /// Clears the current workspace (removes all objects)
        /// </summary>
        public void ClearWorkspace()
        {
            this.allContainedMathArtsObjects.Clear();
            this.Controls.Clear();
            
            //reset id counter for math art objs
            Ctl_MathArtsObject.mathArtsCounter = 0;

            this.colorModulator = 1;

            //reset timer to default interval
            useDefaultTimer = true;
            aTimer.Interval = (int)(DEFAULT_TIMERINTERVAL + 0.5 * DEFAULT_TIMERINTERVAL * (this.Width * 1.0 / DEFAULT_WIDTH * 1.0) * (this.Height * 1.0 / DEFAULT_HEIGHT * 1.0));
            timerInterval = (uint)aTimer.Interval;

            valHighArr = new double[this.Width, this.Height, COLOR_DIMENSIONS];
            valLowArr = new double[this.Width, this.Height, COLOR_DIMENSIONS];
            valFuncArr = new double[this.Width, this.Height];

            this.UpdateColorArray();
            this.UpdateFuncValArray();
            Ctl_MathArtsDisp_ValueChanged(this, EventArgs.Empty);
        }

        /// <summary>
        /// Displays Demo1 (list of hardcoded math arts objects)
        /// </summary>
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

        /// <summary>
        /// Displays Demo1 (list of hardcoded math arts objects)
        /// </summary>
        public void DisplayDemo2()
        {
            ClearWorkspace();

            Ctl_MathArtsColor col1 = new Ctl_MathArtsColor(16, 185);
            col1.Color = Color.FromArgb(192,192,192);
            col1.ColType = Ctl_MathArtsColor.ColTypes.Low;

            Ctl_MathArtsColor col2 = new Ctl_MathArtsColor(120, 90);
            col2.Height=130;
            col2.Width=128;
            col2.Color = Color.FromArgb(0,0,64);
            col2.ColType = Ctl_MathArtsColor.ColTypes.High;

            Ctl_MathArtsColor col3 = new Ctl_MathArtsColor(160, 149);
            col3.Color = Color.FromArgb(255, 255, 255);
            col3.ColType = Ctl_MathArtsColor.ColTypes.High;

            Ctl_MathArtsFunction func1 = new Ctl_MathArtsFunction(101, 79);
            func1.Height = 152;
            func1.Width = 152;
            func1.FuncType = Ctl_MathArtsFunction.FuncTypes.Garbor;
            func1.FuncInverse = true;

            Ctl_MathArtsColor col4 = new Ctl_MathArtsColor(16, 19);
            col4.Color = Color.FromArgb(255, 0, 0);
            col4.ColType = Ctl_MathArtsColor.ColTypes.High;

            Ctl_MathArtsColor col5 = new Ctl_MathArtsColor(65, 17);
            col5.Color = Color.FromArgb(255, 255, 255);
            col5.ColType = Ctl_MathArtsColor.ColTypes.High;

            Ctl_MathArtsColor col6 = new Ctl_MathArtsColor(9, 99);
            col6.Color = Color.FromArgb(0, 255, 0);
            col6.ColType = Ctl_MathArtsColor.ColTypes.High;

            Ctl_MathArtsFunction func2 = new Ctl_MathArtsFunction(6, 46);
            func2.Height = 204;
            func2.Width = 334;
            func2.FuncType = Ctl_MathArtsFunction.FuncTypes.SinCos;
            func2.FuncInverse = true;

            Ctl_MathArtsColor col7 = new Ctl_MathArtsColor(186, 26);
            col7.Color = Color.FromArgb(128, 0, 255);
            col7.ColType = Ctl_MathArtsColor.ColTypes.High;

            Ctl_MathArtsColor col8 = new Ctl_MathArtsColor(344, 253);
            col8.Color = Color.FromArgb(255, 255, 255);
            col8.ColType = Ctl_MathArtsColor.ColTypes.High;

            Ctl_MathArtsColor col9 = new Ctl_MathArtsColor(3, 260);
            col9.Color = Color.FromArgb(0, 255, 0);
            col9.ColType = Ctl_MathArtsColor.ColTypes.High;

            Ctl_MathArtsFunction func3 = new Ctl_MathArtsFunction(-42, 125);
            func3.Height = 72;
            func3.Width = 424;
            func3.FuncType = Ctl_MathArtsFunction.FuncTypes.Garbor;
            func3.FuncInverse = true;

            Ctl_MathArtsColor col10 = new Ctl_MathArtsColor(244, 237);
            col10.Color = Color.FromArgb(255, 255, 0);
            col10.ColType = Ctl_MathArtsColor.ColTypes.Low;

            Ctl_MathArtsColor col11 = new Ctl_MathArtsColor(293, 101);
            col11.Color = Color.FromArgb(255, 255, 255);
            col11.ColType = Ctl_MathArtsColor.ColTypes.Low;

            //  add it to internal list
            this.allContainedMathArtsObjects.Add(col1);
            this.allContainedMathArtsObjects.Add(col2);
            this.allContainedMathArtsObjects.Add(col3);
            this.allContainedMathArtsObjects.Add(func1);
            this.allContainedMathArtsObjects.Add(col4);
            this.allContainedMathArtsObjects.Add(col5);
            this.allContainedMathArtsObjects.Add(col6);
            this.allContainedMathArtsObjects.Add(func2);
            this.allContainedMathArtsObjects.Add(col7);
            this.allContainedMathArtsObjects.Add(col8);
            this.allContainedMathArtsObjects.Add(col9);
            this.allContainedMathArtsObjects.Add(func3);
            this.allContainedMathArtsObjects.Add(col10);
            this.allContainedMathArtsObjects.Add(col11);

            //  subscribe to math arts object events
            this.allContainedMathArtsObjects.ForEach(n => n.subscribeToTimer(aTimer));
            this.allContainedMathArtsObjects.ForEach(n => n.ShapeValueChanged += Ctl_MathArtsDisp_ValueChanged);
            this.allContainedMathArtsObjects.Where(n => n is Ctl_MathArtsColor).ToList().ForEach(n => (n as Ctl_MathArtsColor).ValueChanged += Ctl_MathArtsDisp_ValueChanged);
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

        /// <summary>
        /// Refreshes the container
        /// </summary>
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
            byte RedColor = (byte)(((double)Math.Abs(_val * valHighArr[_x, _y, COLOR_RED] + (1 - _val) * valLowArr[_x, _y, COLOR_RED])) * colorModulator % 256);
            byte GreenColor = (byte)(((double)Math.Abs(_val * valHighArr[_x, _y, COLOR_GREEN] + (1 - _val) * valLowArr[_x, _y, COLOR_GREEN])) * colorModulator % 256);
            byte BlueColor = (byte)(((double)Math.Abs(_val * valHighArr[_x, _y, COLOR_BLUE] + (1 - _val) * valLowArr[_x, _y, COLOR_BLUE])) * colorModulator % 256);

            return Color.FromArgb(RedColor, GreenColor, BlueColor);
        }
        #endregion

        #region private functions
        /// <summary>
        /// Updates all values inside the function value array
        /// </summary>
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

        /// <summary>
        /// Updates the array containing the calculated value for high and low colors
        /// </summary>
        private void UpdateColorArray()
        {
            UpdateColorArray(MathArtsColor.Ctl_MathArtsColor.ColTypes.Low);
            UpdateColorArray(MathArtsColor.Ctl_MathArtsColor.ColTypes.High);
        }

        /// <summary>
        /// Updates the array containing the calculated value either for high or low colors depending on the color type
        /// </summary>
        /// <param name="_coltype">color type of objects to update</param>
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
                        double[] colArr = CalculateColor(lMathArtsColors_Low, x, y);
                        valLowArr[x, y, COLOR_RED] = colArr[COLOR_RED];
                        valLowArr[x, y, COLOR_GREEN] = colArr[COLOR_GREEN];
                        valLowArr[x, y, COLOR_BLUE] = colArr[COLOR_BLUE];
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
                        double[] colArr = CalculateColor(lMathArtsColors_High, x, y);
                        valHighArr[x, y, COLOR_RED] = colArr[COLOR_RED];
                        valHighArr[x, y, COLOR_GREEN] = colArr[COLOR_GREEN];
                        valHighArr[x, y, COLOR_BLUE] = colArr[COLOR_BLUE];
                    }
                }

            }
        }

        /// <summary>
        /// Calculates the function value at a specific position
        /// </summary>
        /// <param name="_x">X position</param>
        /// <param name="_y">Y position</param>
        /// <returns>calculated function value</returns>
        private double CalculateFunctionValue(int _x, int _y)
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
        /// <returns>calculated color as double array</returns>
        private double[] CalculateColor(List<Ctl_MathArtsColor> _lMathArtsColors,int _x,int _y)
        {
            //initialize array (all values should be 0.0) 
            double[] colorArr = new double[COLOR_DIMENSIONS];
            colorArr[COLOR_RED] = 0.0;
            colorArr[COLOR_GREEN] = 0.0;
            colorArr[COLOR_BLUE] = 0.0;

            if (_lMathArtsColors.Count == 0) return colorArr;

            double NormValue = 0.0;


            foreach (Ctl_MathArtsColor _object in _lMathArtsColors)
            {
                double IntensityRate = CalculateIntensityRate(_object, _x, _y);
                NormValue += IntensityRate;
                colorArr[COLOR_RED]     += IntensityRate * (double)((Ctl_MathArtsColor)_object).Color.R * 1.0;
                colorArr[COLOR_GREEN]   += IntensityRate * (double)((Ctl_MathArtsColor)_object).Color.G * 1.0;
                colorArr[COLOR_BLUE]    += IntensityRate * (double)((Ctl_MathArtsColor)_object).Color.B * 1.0;
            }
            colorArr[COLOR_RED]     /= NormValue;
            colorArr[COLOR_GREEN]   /= NormValue;
            colorArr[COLOR_BLUE]    /= NormValue;

            return colorArr;
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
        
        /// <summary>
        /// Saves the container's properties and the objects' properties to xml document
        /// </summary>
        /// <param name="_doc">xml document</param>
        /// <returns>modified xml document</returns>
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
        /// <summary>
        /// Event method for value changed of all objects contained inside the container
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Ctl_MathArtsDisp_ValueChanged(object sender, EventArgs e)
        {
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

        /// <summary>
        /// Event method for resizing of the container
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ctl_MathArtsDisp_Resize(object sender, EventArgs e)
        {
            //only create new Bitmap if disp size is greater 0 -> main frame minimized
            if (this.Width>0 && this.Height>0)
            {
                bitMap.Dispose();
                bitMap = new Bitmap(this.Width, this.Height);

                valHighArr = new double[this.Width, this.Height, COLOR_DIMENSIONS];
                valLowArr = new double[this.Width, this.Height, COLOR_DIMENSIONS];
                valFuncArr = new double[this.Width, this.Height];

                UpdateColorArray();
                UpdateFuncValArray();
                Ctl_MathArtsDisp_ValueChanged(sender, e);

                if (useDefaultTimer)
                {
                    aTimer.Interval = (int)(DEFAULT_TIMERINTERVAL + 0.5 * DEFAULT_TIMERINTERVAL * (this.Width * 1.0 / DEFAULT_WIDTH * 1.0) * (this.Height * 1.0 / DEFAULT_HEIGHT * 1.0));
                    timerInterval = (uint)aTimer.Interval;
                    defaultTimerInterval = timerInterval;
                }
            }
        }

        /// <summary>
        /// Paint method of the container. Paints all MathArts objects inside the container
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ctl_MathArtsDisp_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(bitMap, this.Location.X, this.Location.Y);
        }
        #endregion

        #region debug
        public double GetFuncValue(int _x,int _y){ return valFuncArr[_x,_y];}

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