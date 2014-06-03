namespace MathArts.MathArtsFunction
{
    partial class Ctl_MathArtsFunction
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
            this.Lbl_DebugInfoFuncVal = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Lbl_DebugInfoFuncVal
            // 
            this.Lbl_DebugInfoFuncVal.AutoSize = true;
            this.Lbl_DebugInfoFuncVal.Location = new System.Drawing.Point(3, 87);
            this.Lbl_DebugInfoFuncVal.Name = "Lbl_DebugInfoFuncVal";
            this.Lbl_DebugInfoFuncVal.Size = new System.Drawing.Size(0, 13);
            this.Lbl_DebugInfoFuncVal.TabIndex = 1;
            // 
            // Ctl_MathArtsFunction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Lbl_DebugInfoFuncVal);
            this.Name = "Ctl_MathArtsFunction";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Ctl_MathArtsFunction_Paint);
            this.DoubleClick += new System.EventHandler(this.Ctl_MathArtsFunction_DoubleClick);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Ctl_MathArtsFunction_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Ctl_MathArtsFunction_MouseUp);
            this.Controls.SetChildIndex(this.Lbl_DebugInfoFuncVal, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Lbl_DebugInfoFuncVal;

    }
}
