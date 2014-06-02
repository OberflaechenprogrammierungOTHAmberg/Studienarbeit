using MathArts.MathArtsColor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MathArts
{
    public partial class Frm_MathArts : Form
    {
        public Frm_MathArts()
        {
            InitializeComponent();
        }

        #region MenuEvents
        private void menuItem_Color_Click(object sender, EventArgs e)
        {
            Ctl_MathArtsColor color = new Ctl_MathArtsColor();
            color.Location = new Point(5, 5);

            this.MathArtsDisp_Container.AddMathArtsObject(color as Ctl_MathArtsObject);
        }

        private void menuItem_FrameVisible_Click(object sender, EventArgs e)
        {
            this.menuItem_FrameVisible.Checked = !this.menuItem_FrameVisible.Checked;
            this.MathArtsDisp_Container.ShowControls(menuItem_FrameVisible.Checked);
        }
        #endregion
    }
}
