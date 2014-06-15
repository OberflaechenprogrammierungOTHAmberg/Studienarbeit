namespace MathArts
{
    partial class Frm_MathArtsPropertiesDialog
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
            this.trb_ColorModulator = new System.Windows.Forms.TrackBar();
            this.tb_ColorModulator = new System.Windows.Forms.TextBox();
            this.grb_ColorModulator = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.trb_ColorModulator)).BeginInit();
            this.grb_ColorModulator.SuspendLayout();
            this.SuspendLayout();
            // 
            // trb_ColorModulator
            // 
            this.trb_ColorModulator.Location = new System.Drawing.Point(6, 19);
            this.trb_ColorModulator.Maximum = 256;
            this.trb_ColorModulator.Minimum = 1;
            this.trb_ColorModulator.Name = "trb_ColorModulator";
            this.trb_ColorModulator.Size = new System.Drawing.Size(145, 45);
            this.trb_ColorModulator.TabIndex = 0;
            this.trb_ColorModulator.TickFrequency = 32;
            this.trb_ColorModulator.Value = 1;
            this.trb_ColorModulator.ValueChanged += new System.EventHandler(this.trb_ColorModulator_ValueChanged);
            // 
            // tb_ColorModulator
            // 
            this.tb_ColorModulator.Location = new System.Drawing.Point(157, 19);
            this.tb_ColorModulator.Name = "tb_ColorModulator";
            this.tb_ColorModulator.Size = new System.Drawing.Size(38, 20);
            this.tb_ColorModulator.TabIndex = 1;
            this.tb_ColorModulator.TextChanged += new System.EventHandler(this.tb_ColorModulator_TextChanged);
            this.tb_ColorModulator.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_ColorModulator_KeyPress);
            // 
            // grb_ColorModulator
            // 
            this.grb_ColorModulator.Controls.Add(this.trb_ColorModulator);
            this.grb_ColorModulator.Controls.Add(this.tb_ColorModulator);
            this.grb_ColorModulator.Location = new System.Drawing.Point(12, 12);
            this.grb_ColorModulator.Name = "grb_ColorModulator";
            this.grb_ColorModulator.Size = new System.Drawing.Size(209, 67);
            this.grb_ColorModulator.TabIndex = 3;
            this.grb_ColorModulator.TabStop = false;
            this.grb_ColorModulator.Text = "Farbwertmodulator";
            // 
            // Frm_MathArtsPropertiesDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(230, 87);
            this.Controls.Add(this.grb_ColorModulator);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_MathArtsPropertiesDialog";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Eigenschaften";
            ((System.ComponentModel.ISupportInitialize)(this.trb_ColorModulator)).EndInit();
            this.grb_ColorModulator.ResumeLayout(false);
            this.grb_ColorModulator.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TrackBar trb_ColorModulator;
        private System.Windows.Forms.TextBox tb_ColorModulator;
        private System.Windows.Forms.GroupBox grb_ColorModulator;
    }
}