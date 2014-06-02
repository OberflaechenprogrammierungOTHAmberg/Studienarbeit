using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MathArts.MathArtsFunction
{
    public partial class Ctl_MathArtsFunction : Ctl_MathArtsObject
    {
        #region member
        private FuncType objectFuncType;
        
        #endregion

        #region constructors
        public Ctl_MathArtsFunction()
        {
            InitializeComponent();
        }
        #endregion

        #region enums
        public enum FuncType
        {
            SinCos, Gauss, Garbor
        }
        #endregion
    }
}
