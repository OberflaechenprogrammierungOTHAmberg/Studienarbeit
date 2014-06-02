using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace MathArts.MathArtsColor
{
    public partial class Ctl_MathArtsColor : Ctl_MathArtsObject
    {
        #region member
        private Color color;
        private ColTypes colType;
        #endregion

        #region constructors
        public Ctl_MathArtsColor()
        {
            InitializeComponent();
            this.color = Color.White;
            this.colType = ColTypes.Low;
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
                    if (ValueChanged != null) ValueChanged(this, EventArgs.Empty);
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
                    if (ValueChanged != null) ValueChanged(this, EventArgs.Empty);
                    this.Refresh();
                }
            }
        }
        #endregion

        #region events
        public event EventHandler ValueChanged;
        #endregion

        #region enums
        public enum ColTypes
        {
            Low, High
        }
        #endregion

        private void Ctl_MathArtsColor_Paint(object sender, PaintEventArgs e)
        {
            drawFilledEllipse(e, new SolidBrush(this.color), 0, 0, this.Width - 1, this.Height - 1);

            e.Graphics.DrawEllipse(Pens.Green, 0, 0, this.Width - 1, this.Height - 1);
        }

        private void Ctl_MathArtsColor_DoubleClick(object sender, EventArgs e)
        {
            Frm_MathArtsColorDialog coldlg = new Frm_MathArtsColorDialog(this.color);
            coldlg.ColorChanged += coldlg_ColorChanged;
            coldlg.ShowDialog();
        }

        void coldlg_ColorChanged(object sender, ColorChangedEventArgs e)
        {
            this.Color = e.NewColor;
        }

        [ConditionalAttribute("DEBUG")]
        private void drawFilledEllipse(PaintEventArgs e,SolidBrush _solidBrush, int _x1, int _y1, int _x2, int _y2)
        {
            e.Graphics.FillEllipse(_solidBrush, _x1, _y1,_x2, _y2);
        }
    }
}
