namespace MathArts.MathArtsColor
{
    partial class Frm_MathArtsColorDialog
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
            this.Lbl_Color = new System.Windows.Forms.Label();
            this.Lbl_Type = new System.Windows.Forms.Label();
            this.Pnl_ColorPreview = new System.Windows.Forms.Panel();
            this.Btn_Color = new System.Windows.Forms.Button();
            this.Cb_Type = new System.Windows.Forms.ComboBox();
            this.Lbl_DebugInfoColType = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Lbl_Color
            // 
            this.Lbl_Color.AutoSize = true;
            this.Lbl_Color.Location = new System.Drawing.Point(12, 9);
            this.Lbl_Color.Name = "Lbl_Color";
            this.Lbl_Color.Size = new System.Drawing.Size(37, 13);
            this.Lbl_Color.TabIndex = 0;
            this.Lbl_Color.Text = "&Farbe:";
            // 
            // Lbl_Type
            // 
            this.Lbl_Type.AutoSize = true;
            this.Lbl_Type.Location = new System.Drawing.Point(12, 45);
            this.Lbl_Type.Name = "Lbl_Type";
            this.Lbl_Type.Size = new System.Drawing.Size(28, 13);
            this.Lbl_Type.TabIndex = 3;
            this.Lbl_Type.Text = "&Typ:";
            // 
            // Pnl_ColorPreview
            // 
            this.Pnl_ColorPreview.Location = new System.Drawing.Point(89, 4);
            this.Pnl_ColorPreview.Name = "Pnl_ColorPreview";
            this.Pnl_ColorPreview.Size = new System.Drawing.Size(31, 23);
            this.Pnl_ColorPreview.TabIndex = 1;
            // 
            // Btn_Color
            // 
            this.Btn_Color.Location = new System.Drawing.Point(126, 4);
            this.Btn_Color.Name = "Btn_Color";
            this.Btn_Color.Size = new System.Drawing.Size(75, 23);
            this.Btn_Color.TabIndex = 2;
            this.Btn_Color.Text = "...";
            this.Btn_Color.UseVisualStyleBackColor = true;
            this.Btn_Color.Click += new System.EventHandler(this.Btn_Color_Click);
            // 
            // Cb_Type
            // 
            this.Cb_Type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cb_Type.FormattingEnabled = true;
            this.Cb_Type.Location = new System.Drawing.Point(89, 42);
            this.Cb_Type.Name = "Cb_Type";
            this.Cb_Type.Size = new System.Drawing.Size(112, 21);
            this.Cb_Type.TabIndex = 4;
            this.Cb_Type.SelectedIndexChanged += new System.EventHandler(this.Cb_Type_SelectedIndexChanged);
            // 
            // Lbl_DebugInfoColType
            // 
            this.Lbl_DebugInfoColType.AutoSize = true;
            this.Lbl_DebugInfoColType.Location = new System.Drawing.Point(86, 66);
            this.Lbl_DebugInfoColType.Name = "Lbl_DebugInfoColType";
            this.Lbl_DebugInfoColType.Size = new System.Drawing.Size(0, 13);
            this.Lbl_DebugInfoColType.TabIndex = 5;
            // 
            // Frm_MathArtsColorDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(215, 75);
            this.Controls.Add(this.Lbl_DebugInfoColType);
            this.Controls.Add(this.Cb_Type);
            this.Controls.Add(this.Btn_Color);
            this.Controls.Add(this.Pnl_ColorPreview);
            this.Controls.Add(this.Lbl_Type);
            this.Controls.Add(this.Lbl_Color);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_MathArtsColorDialog";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Farbeigenschaften";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Lbl_Color;
        private System.Windows.Forms.Label Lbl_Type;
        private System.Windows.Forms.Panel Pnl_ColorPreview;
        private System.Windows.Forms.Button Btn_Color;
        private System.Windows.Forms.ComboBox Cb_Type;
        private System.Windows.Forms.Label Lbl_DebugInfoColType;
    }
}