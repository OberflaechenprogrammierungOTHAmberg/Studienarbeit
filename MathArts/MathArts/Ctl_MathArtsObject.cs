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

namespace MathArts
{
    public partial class Ctl_MathArtsObject : TransParentLib.UserControlTP
    {
        #region members
        private Point mouseDownLocation;
        private MouseClickType mouseClickType = MouseClickType.None;
        #endregion

        #region constructors
        public Ctl_MathArtsObject()
        {
            InitializeComponent();
        }
        #endregion

        private enum MouseClickType
        {
            None, Move, Resize
        }

        #region Mouse Events
        private void Ctl_MathArtsObject_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                mouseDownLocation = e.Location;
                if (e.X > this.Width - 10 && e.Y > this.Height - 10)
                {
                    this.mouseClickType = MouseClickType.Resize;
                }
                else this.mouseClickType = MouseClickType.Move;
            }

            else this.mouseClickType = MouseClickType.None;
        }

        private void Ctl_MathArtsObject_MouseUp(object sender, MouseEventArgs e)
        {
            this.mouseClickType = MouseClickType.None;
        }

        private void Ctl_MathArtsObject_MouseMove(object sender, MouseEventArgs e)
        {
            this.Cursor = (e.X > this.Width - 10 && e.Y > this.Height - 10) ? Cursors.SizeNWSE : Cursors.SizeAll;

            showDebugInformation(e.X + " " + e.Y + " w/h " + this.Width + " " + this.Height + " p " + this.Location.X + " " + this.Location.Y + "  " + this.mouseClickType.ToString());

            if (this.mouseClickType == MouseClickType.Move)
            {
                this.Left = e.X + this.Left - mouseDownLocation.X;
                this.Top = e.Y + this.Top - mouseDownLocation.Y;
            }
            else if (this.mouseClickType == MouseClickType.Resize)
            {
                this.Width = e.X;
                this.Height = e.Y;
            }
        }

        private void Ctl_MathArtsObject_Resize(object sender, EventArgs e)
        {
            Refresh();
        } 
        #endregion


        [ConditionalAttribute("DEBUG")]
        private void showDebugInformation(string info)
        {
            Lbl_DebugInfo.Text = info;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        }

    }
}
