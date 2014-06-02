namespace MathArts
{
    partial class Ctl_MathArtsObject
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
            this.Lbl_DebugInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Lbl_DebugInfo
            // 
            this.Lbl_DebugInfo.AutoSize = true;
            this.Lbl_DebugInfo.Location = new System.Drawing.Point(29, 67);
            this.Lbl_DebugInfo.Name = "Lbl_DebugInfo";
            this.Lbl_DebugInfo.Size = new System.Drawing.Size(0, 13);
            this.Lbl_DebugInfo.TabIndex = 0;
            // 
            // Ctl_MathArtsObject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Lbl_DebugInfo);
            this.Name = "Ctl_MathArtsObject";
            this.Size = new System.Drawing.Size(152, 152);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Ctl_MathArtsObject_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Ctl_MathArtsObject_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Ctl_MathArtsObject_MouseUp);
            this.Resize += new System.EventHandler(this.Ctl_MathArtsObject_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Lbl_DebugInfo;
    }
}
