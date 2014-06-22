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
            this.components = new System.ComponentModel.Container();
            this.Trb_ColorModulator = new System.Windows.Forms.TrackBar();
            this.Tb_ColorModulator = new System.Windows.Forms.TextBox();
            this.Grb_ColorModulator = new System.Windows.Forms.GroupBox();
            this.grb_TimerProperties = new System.Windows.Forms.GroupBox();
            this.Chb_DefaultTimer = new System.Windows.Forms.CheckBox();
            this.Tb_TimerModulator = new System.Windows.Forms.TextBox();
            this.Trb_TimerModulator = new System.Windows.Forms.TrackBar();
            this.grb_PaintTrigger = new System.Windows.Forms.GroupBox();
            this.Chb_UseTimer = new System.Windows.Forms.CheckBox();
            this.Tt_UseTimer = new System.Windows.Forms.ToolTip(this.components);
            this.Tt_DefaultValueUseTimer = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.Trb_ColorModulator)).BeginInit();
            this.Grb_ColorModulator.SuspendLayout();
            this.grb_TimerProperties.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Trb_TimerModulator)).BeginInit();
            this.grb_PaintTrigger.SuspendLayout();
            this.SuspendLayout();
            // 
            // Trb_ColorModulator
            // 
            this.Trb_ColorModulator.Location = new System.Drawing.Point(6, 19);
            this.Trb_ColorModulator.Maximum = 256;
            this.Trb_ColorModulator.Minimum = 1;
            this.Trb_ColorModulator.Name = "Trb_ColorModulator";
            this.Trb_ColorModulator.Size = new System.Drawing.Size(145, 45);
            this.Trb_ColorModulator.TabIndex = 0;
            this.Trb_ColorModulator.TickFrequency = 32;
            this.Trb_ColorModulator.Value = 1;
            this.Trb_ColorModulator.ValueChanged += new System.EventHandler(this.Trb_ColorModulator_ValueChanged);
            // 
            // Tb_ColorModulator
            // 
            this.Tb_ColorModulator.Location = new System.Drawing.Point(157, 19);
            this.Tb_ColorModulator.Name = "Tb_ColorModulator";
            this.Tb_ColorModulator.Size = new System.Drawing.Size(38, 20);
            this.Tb_ColorModulator.TabIndex = 1;
            this.Tb_ColorModulator.TextChanged += new System.EventHandler(this.Tb_ColorModulator_TextChanged);
            this.Tb_ColorModulator.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Tb_ColorModulator_KeyPress);
            this.Tb_ColorModulator.Leave += new System.EventHandler(this.Tb_ColorModulator_Leave);
            // 
            // Grb_ColorModulator
            // 
            this.Grb_ColorModulator.Controls.Add(this.Trb_ColorModulator);
            this.Grb_ColorModulator.Controls.Add(this.Tb_ColorModulator);
            this.Grb_ColorModulator.Location = new System.Drawing.Point(12, 12);
            this.Grb_ColorModulator.Name = "Grb_ColorModulator";
            this.Grb_ColorModulator.Size = new System.Drawing.Size(222, 67);
            this.Grb_ColorModulator.TabIndex = 0;
            this.Grb_ColorModulator.TabStop = false;
            this.Grb_ColorModulator.Text = "Farbwertmodulator";
            // 
            // grb_TimerProperties
            // 
            this.grb_TimerProperties.Controls.Add(this.Chb_DefaultTimer);
            this.grb_TimerProperties.Controls.Add(this.Tb_TimerModulator);
            this.grb_TimerProperties.Controls.Add(this.Trb_TimerModulator);
            this.grb_TimerProperties.Location = new System.Drawing.Point(6, 43);
            this.grb_TimerProperties.Name = "grb_TimerProperties";
            this.grb_TimerProperties.Size = new System.Drawing.Size(205, 101);
            this.grb_TimerProperties.TabIndex = 1;
            this.grb_TimerProperties.TabStop = false;
            this.grb_TimerProperties.Text = "Zeittakt in ms";
            this.grb_TimerProperties.Visible = false;
            // 
            // Chb_DefaultTimer
            // 
            this.Chb_DefaultTimer.AutoSize = true;
            this.Chb_DefaultTimer.Location = new System.Drawing.Point(14, 18);
            this.Chb_DefaultTimer.Name = "Chb_DefaultTimer";
            this.Chb_DefaultTimer.Size = new System.Drawing.Size(136, 17);
            this.Chb_DefaultTimer.TabIndex = 0;
            this.Chb_DefaultTimer.Text = "Standardwert benutzen";
            this.Chb_DefaultTimer.UseVisualStyleBackColor = true;
            this.Chb_DefaultTimer.CheckedChanged += new System.EventHandler(this.Chb_DefaultTimer_CheckedChanged);
            // 
            // Tb_TimerModulator
            // 
            this.Tb_TimerModulator.Location = new System.Drawing.Point(157, 41);
            this.Tb_TimerModulator.Name = "Tb_TimerModulator";
            this.Tb_TimerModulator.ReadOnly = true;
            this.Tb_TimerModulator.Size = new System.Drawing.Size(38, 20);
            this.Tb_TimerModulator.TabIndex = 2;
            this.Tb_TimerModulator.TextChanged += new System.EventHandler(this.Tb_TimerModulator_TextChanged);
            // 
            // Trb_TimerModulator
            // 
            this.Trb_TimerModulator.Location = new System.Drawing.Point(6, 41);
            this.Trb_TimerModulator.Maximum = 1000;
            this.Trb_TimerModulator.Minimum = 50;
            this.Trb_TimerModulator.Name = "Trb_TimerModulator";
            this.Trb_TimerModulator.Size = new System.Drawing.Size(145, 45);
            this.Trb_TimerModulator.TabIndex = 1;
            this.Trb_TimerModulator.TickFrequency = 100;
            this.Trb_TimerModulator.Value = 100;
            this.Trb_TimerModulator.ValueChanged += new System.EventHandler(this.Trb_TimerModulator_ValueChanged);
            // 
            // grb_PaintTrigger
            // 
            this.grb_PaintTrigger.Controls.Add(this.Chb_UseTimer);
            this.grb_PaintTrigger.Controls.Add(this.grb_TimerProperties);
            this.grb_PaintTrigger.Location = new System.Drawing.Point(12, 86);
            this.grb_PaintTrigger.Name = "grb_PaintTrigger";
            this.grb_PaintTrigger.Size = new System.Drawing.Size(222, 150);
            this.grb_PaintTrigger.TabIndex = 1;
            this.grb_PaintTrigger.TabStop = false;
            this.grb_PaintTrigger.Text = "Aktualisierung";
            // 
            // Chb_UseTimer
            // 
            this.Chb_UseTimer.AutoSize = true;
            this.Chb_UseTimer.Location = new System.Drawing.Point(7, 20);
            this.Chb_UseTimer.Name = "Chb_UseTimer";
            this.Chb_UseTimer.Size = new System.Drawing.Size(109, 17);
            this.Chb_UseTimer.TabIndex = 0;
            this.Chb_UseTimer.Text = "Zeittakt benutzen";
            this.Chb_UseTimer.UseVisualStyleBackColor = true;
            this.Chb_UseTimer.CheckedChanged += new System.EventHandler(this.Chb_UseTimer_CheckedChanged);
            // 
            // Tt_UseTimer
            // 
            this.Tt_UseTimer.AutomaticDelay = 100;
            this.Tt_UseTimer.AutoPopDelay = 2500;
            this.Tt_UseTimer.InitialDelay = 100;
            this.Tt_UseTimer.ReshowDelay = 20;
            // 
            // Tt_DefaultValueUseTimer
            // 
            this.Tt_DefaultValueUseTimer.AutomaticDelay = 100;
            this.Tt_DefaultValueUseTimer.AutoPopDelay = 2500;
            this.Tt_DefaultValueUseTimer.InitialDelay = 100;
            this.Tt_DefaultValueUseTimer.ReshowDelay = 20;
            // 
            // Frm_MathArtsPropertiesDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(247, 245);
            this.Controls.Add(this.grb_PaintTrigger);
            this.Controls.Add(this.Grb_ColorModulator);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_MathArtsPropertiesDialog";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Eigenschaften";
            ((System.ComponentModel.ISupportInitialize)(this.Trb_ColorModulator)).EndInit();
            this.Grb_ColorModulator.ResumeLayout(false);
            this.Grb_ColorModulator.PerformLayout();
            this.grb_TimerProperties.ResumeLayout(false);
            this.grb_TimerProperties.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Trb_TimerModulator)).EndInit();
            this.grb_PaintTrigger.ResumeLayout(false);
            this.grb_PaintTrigger.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TrackBar Trb_ColorModulator;
        private System.Windows.Forms.TextBox Tb_ColorModulator;
        private System.Windows.Forms.GroupBox Grb_ColorModulator;
        private System.Windows.Forms.GroupBox grb_TimerProperties;
        private System.Windows.Forms.TextBox Tb_TimerModulator;
        private System.Windows.Forms.TrackBar Trb_TimerModulator;
        private System.Windows.Forms.CheckBox Chb_DefaultTimer;
        private System.Windows.Forms.GroupBox grb_PaintTrigger;
        private System.Windows.Forms.CheckBox Chb_UseTimer;
        private System.Windows.Forms.ToolTip Tt_UseTimer;
        private System.Windows.Forms.ToolTip Tt_DefaultValueUseTimer;
    }
}