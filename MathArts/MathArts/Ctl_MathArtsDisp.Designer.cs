namespace MathArts
{
    partial class Ctl_MathArtsDisp
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Ctl_MathArtsDisp
            // 
            this.Name = "Ctl_MathArtsDisp";
            this.Size = new System.Drawing.Size(316, 192);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Ctl_MathArtsDisp_Paint);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Ctl_MathArtsDisp_MouseMove);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
