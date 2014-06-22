using System;
using System.Drawing;

namespace MathArts.Debug
{
    public class Tracing_ValueChangedEventArgs : EventArgs
    {
        private Ctl_MathArtsObject newMathArtsObject = null;
        private ValueChangeTypes changeType;
        private int x = 0;
        private int y = 0;
        private Point mousePosition = new Point(0,0);
        private Ctl_MathArtsDisp displayContainer;

        public Ctl_MathArtsDisp DisplayContainer 
        { 
            get{return displayContainer;}
            set{displayContainer=value;}
        }
        

        public Ctl_MathArtsObject NewMathArtsObject
        {
            get { return newMathArtsObject; }
            set { newMathArtsObject = value; }
        }

        public int X
        {
            get { return x; }
            set { x = value; }
        }

        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        public ValueChangeTypes ChangeType
        {
            get { return changeType; }
            set { changeType = value; }
        }

        public Point MousePosition
        {
            get { return mousePosition; }
            set { mousePosition = value; }
        }

        public Tracing_ValueChangedEventArgs(Ctl_MathArtsObject _newMathArtsObject)
        {
            this.NewMathArtsObject = _newMathArtsObject;
            this.ChangeType = ValueChangeTypes.NewMathArtsObj;
            this.displayContainer = null;
        }

        public Tracing_ValueChangedEventArgs(Ctl_MathArtsObject _newMathArtsObject,ValueChangeTypes _newValueChangeType)
        {
            this.NewMathArtsObject = _newMathArtsObject;
            this.ChangeType = _newValueChangeType;
            this.displayContainer = null;
        }

        public Tracing_ValueChangedEventArgs(Ctl_MathArtsObject _newMathArtsObject, ValueChangeTypes _newValueChangeType,Point _newMousePosition)
        {
            this.NewMathArtsObject = _newMathArtsObject;
            this.ChangeType = _newValueChangeType;
            this.MousePosition = _newMousePosition;
            this.displayContainer = null;
        }

        public Tracing_ValueChangedEventArgs(Ctl_MathArtsDisp _disp,int _x,int _y)
        {
            this.ChangeType = ValueChangeTypes.MouseMovedDisp;
            this.newMathArtsObject = null;
            this.x = _x;
            this.y = _y;
            this.displayContainer = _disp;
        }

        public enum ValueChangeTypes
        {
            NewMathArtsObj          =0,
            MathArtsObjValueChanged =1,
            MathArtsObjShapeChanged =2,
            MouseMovedDisp          =3
        }
    }
}
