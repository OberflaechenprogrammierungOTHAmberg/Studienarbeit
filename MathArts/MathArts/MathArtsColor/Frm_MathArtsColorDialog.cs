using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MathArts.MathArtsColor
{
    public partial class Frm_MathArtsColorDialog : Form
    {
        private static ColorDialog coldlg = new ColorDialog();

        public Frm_MathArtsColorDialog(Color _currentColor)
        {
            InitializeComponent();

            //initialize default color in color dialog and preview color
            coldlg.Color = _currentColor;
            this.Pnl_ColorPreview.BackColor = _currentColor;
            
            //load enumeration dynamically
            Cb_Type.DataSource = Enum.GetNames(typeof(MathArts.MathArtsColor.Ctl_MathArtsColor.ColTypes));
        }

        public delegate void ColorChangedEventHandler(object sender, ColorChangedEventArgs e);
        public event ColorChangedEventHandler ColorChanged;

        private void Btn_Color_Click(object sender, EventArgs e)
        {
            if (coldlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (ColorChanged != null) ColorChanged(this, new ColorChangedEventArgs(coldlg.Color));
                this.Pnl_ColorPreview.BackColor = coldlg.Color; 
            }
        }
    }

    public class ColorChangedEventArgs : EventArgs
    {
        private Color newColor;

        public Color NewColor
        {
            get { return newColor; }
            set { newColor = value; }
        }

        public ColorChangedEventArgs(Color _newColor)
        {
            this.newColor = _newColor;
        }
    }
}
