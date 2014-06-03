using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathArts.Debug
{
    public class Tracing_ValueChangedEventArgs : EventArgs
    {
        private Ctl_MathArtsObject newMathArtsObject = null;
        private ValueChangeTypes changeType;
        private int xPosition = 0;
        private int yPosition = 0;

        public Ctl_MathArtsObject NewMathArtsObject
        {
            get { return newMathArtsObject; }
            set { newMathArtsObject = value; }
        }

        public int X
        {
            get { return xPosition; }
            set { xPosition = value; }
        }

        public int Y
        {
            get { return yPosition; }
            set { yPosition = value; }
        }

        public ValueChangeTypes ChangeType
        {
            get { return changeType; }
            set { changeType = value; }
        }

        public Tracing_ValueChangedEventArgs(Ctl_MathArtsObject _newMathArtsObject)
        {
            this.NewMathArtsObject = _newMathArtsObject;
            this.ChangeType = ValueChangeTypes.NewMathArtsObj;
        }

        public Tracing_ValueChangedEventArgs(Ctl_MathArtsObject _newMathArtsObject,ValueChangeTypes _newValueChangeType)
        {
            this.NewMathArtsObject = _newMathArtsObject;
            this.ChangeType = _newValueChangeType;
        }

        public Tracing_ValueChangedEventArgs(int _x,int _y)
        {
            this.ChangeType = ValueChangeTypes.MouseMovedDisp;
            this.newMathArtsObject = null;
            xPosition = _x;
            yPosition = _y;
        }

        public enum ValueChangeTypes
        {
            NewMathArtsObj          =0,
            MathArtsObjValueChanged =1,
            MathArtsObjShapeChanged = 2,
            MouseMovedDisp          =3
        }
    }
}
