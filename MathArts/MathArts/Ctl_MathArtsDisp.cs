using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MathArts
{
    public partial class Ctl_MathArtsDisp : UserControl
    {
        #region member
        private List<Ctl_MathArtsObject> allContainedMathArtsObjects;
        #endregion

        #region constructors
        public Ctl_MathArtsDisp()
        {
            InitializeComponent();
            this.allContainedMathArtsObjects = new List<Ctl_MathArtsObject>();
        }
        #endregion

        #region intern methods
        public void ShowControls(bool showControls)
        {
            this.allContainedMathArtsObjects.ForEach(n => n.Visible = showControls);
        }
        #endregion

        public void AddMathArtsObject(Ctl_MathArtsObject _object)
        {
            this.allContainedMathArtsObjects.Add(_object);
            Refresh();
        }

        private void Ctl_MathArtsDisp_Paint(object sender, PaintEventArgs e)
        {
            foreach (Ctl_MathArtsObject item in this.allContainedMathArtsObjects)
            {
                if (!this.Controls.Contains(item))
                {
                    this.Controls.Add(item); 
                }
            }
        }
    }
}
