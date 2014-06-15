namespace MathArts.MathArtsFunction
{
    partial class Frm_MathArtsFunctionDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Cb_Function = new System.Windows.Forms.ComboBox();
            this.Lbl_Inverse = new System.Windows.Forms.Label();
            this.Lbl_Function = new System.Windows.Forms.Label();
            this.Chb_Inverse = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // Cb_Function
            // 
            this.Cb_Function.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cb_Function.FormattingEnabled = true;
            this.Cb_Function.Location = new System.Drawing.Point(91, 6);
            this.Cb_Function.Name = "Cb_Function";
            this.Cb_Function.Size = new System.Drawing.Size(112, 21);
            this.Cb_Function.TabIndex = 9;
            this.Cb_Function.SelectedIndexChanged += new System.EventHandler(this.Cb_Function_SelectedIndexChanged);
            // 
            // Lbl_Inverse
            // 
            this.Lbl_Inverse.AutoSize = true;
            this.Lbl_Inverse.Location = new System.Drawing.Point(12, 45);
            this.Lbl_Inverse.Name = "Lbl_Inverse";
            this.Lbl_Inverse.Size = new System.Drawing.Size(39, 13);
            this.Lbl_Inverse.TabIndex = 8;
            this.Lbl_Inverse.Text = "&Invers:";
            // 
            // Lbl_Function
            // 
            this.Lbl_Function.AutoSize = true;
            this.Lbl_Function.Location = new System.Drawing.Point(12, 9);
            this.Lbl_Function.Name = "Lbl_Function";
            this.Lbl_Function.Size = new System.Drawing.Size(51, 13);
            this.Lbl_Function.TabIndex = 5;
            this.Lbl_Function.Text = "&Funktion:";
            // 
            // Chb_Inverse
            // 
            this.Chb_Inverse.AutoSize = true;
            this.Chb_Inverse.Location = new System.Drawing.Point(91, 44);
            this.Chb_Inverse.Name = "Chb_Inverse";
            this.Chb_Inverse.Size = new System.Drawing.Size(15, 14);
            this.Chb_Inverse.TabIndex = 10;
            this.Chb_Inverse.UseVisualStyleBackColor = true;
            this.Chb_Inverse.CheckedChanged += new System.EventHandler(this.Chb_Inverse_CheckedChanged);
            // 
            // Frm_MathArtsFunctionDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(215, 75);
            this.Controls.Add(this.Chb_Inverse);
            this.Controls.Add(this.Cb_Function);
            this.Controls.Add(this.Lbl_Inverse);
            this.Controls.Add(this.Lbl_Function);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_MathArtsFunctionDialog";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Funktionseigenschaften";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox Cb_Function;
        private System.Windows.Forms.Label Lbl_Inverse;
        private System.Windows.Forms.Label Lbl_Function;
        private System.Windows.Forms.CheckBox Chb_Inverse;


    }
}