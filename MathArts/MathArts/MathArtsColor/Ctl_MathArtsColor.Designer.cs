namespace MathArts.MathArtsColor
{
    partial class Ctl_MathArtsColor
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
            this.Lbl_DebugInfoColor = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Lbl_DebugInfoColor
            // 
            this.Lbl_DebugInfoColor.AutoSize = true;
            this.Lbl_DebugInfoColor.Location = new System.Drawing.Point(29, 91);
            this.Lbl_DebugInfoColor.Name = "Lbl_DebugInfoColor";
            this.Lbl_DebugInfoColor.Size = new System.Drawing.Size(0, 13);
            this.Lbl_DebugInfoColor.TabIndex = 1;
            // 
            // Ctl_MathArtsColor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Lbl_DebugInfoColor);
            this.Name = "Ctl_MathArtsColor";
            this.Size = new System.Drawing.Size(154, 154);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Ctl_MathArtsColor_Paint);
            this.DoubleClick += new System.EventHandler(this.Ctl_MathArtsColor_DoubleClick);
            this.Controls.SetChildIndex(this.Lbl_DebugInfoColor, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Lbl_DebugInfoColor;
    }
}
