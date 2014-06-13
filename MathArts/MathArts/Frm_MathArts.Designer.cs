namespace MathArts
{
    partial class Frm_MathArts
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

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menu_MathArts = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem_File = new System.Windows.Forms.MenuItem();
            this.menuItem_New = new System.Windows.Forms.MenuItem();
            this.menuItem_Save = new System.Windows.Forms.MenuItem();
            this.menuItem_Demo1 = new System.Windows.Forms.MenuItem();
            this.menuItem_Demo2 = new System.Windows.Forms.MenuItem();
            this.menuItem_Exit = new System.Windows.Forms.MenuItem();
            this.menuItem_Insert = new System.Windows.Forms.MenuItem();
            this.menuItem_Color = new System.Windows.Forms.MenuItem();
            this.menuItem_Function = new System.Windows.Forms.MenuItem();
            this.menuItem_Settings = new System.Windows.Forms.MenuItem();
            this.menuItem_FrameVisible = new System.Windows.Forms.MenuItem();
            this.MathArtsDisp_Container = new MathArts.Ctl_MathArtsDisp();
            this.SuspendLayout();
            // 
            // menu_MathArts
            // 
            this.menu_MathArts.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem_File,
            this.menuItem_Insert,
            this.menuItem_Settings});
            // 
            // menuItem_File
            // 
            this.menuItem_File.Index = 0;
            this.menuItem_File.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem_New,
            this.menuItem_Save,
            this.menuItem_Demo1,
            this.menuItem_Demo2,
            this.menuItem_Exit});
            this.menuItem_File.Text = "&Datei";
            // 
            // menuItem_New
            // 
            this.menuItem_New.Index = 0;
            this.menuItem_New.Text = "&Neu";
            this.menuItem_New.Click += new System.EventHandler(this.menuItem_New_Click);
            // 
            // menuItem_Save
            // 
            this.menuItem_Save.Index = 1;
            this.menuItem_Save.Text = "&Speichern unter ";
            this.menuItem_Save.Click += new System.EventHandler(this.menuItem_Save_Click);
            // 
            // menuItem_Demo1
            // 
            this.menuItem_Demo1.Index = 2;
            this.menuItem_Demo1.Text = "Demo &1";
            this.menuItem_Demo1.Click += new System.EventHandler(this.menuItem_Demo1_Click);
            // 
            // menuItem_Demo2
            // 
            this.menuItem_Demo2.Index = 3;
            this.menuItem_Demo2.Text = "Demo &2";
            // 
            // menuItem_Exit
            // 
            this.menuItem_Exit.Index = 4;
            this.menuItem_Exit.Text = "&Beenden";
            this.menuItem_Exit.Click += new System.EventHandler(this.menuItem_Exit_Click);
            // 
            // menuItem_Insert
            // 
            this.menuItem_Insert.Index = 1;
            this.menuItem_Insert.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem_Color,
            this.menuItem_Function});
            this.menuItem_Insert.Text = "&Einfügen";
            // 
            // menuItem_Color
            // 
            this.menuItem_Color.Index = 0;
            this.menuItem_Color.Text = "F&arbe";
            this.menuItem_Color.Click += new System.EventHandler(this.menuItem_Color_Click);
            // 
            // menuItem_Function
            // 
            this.menuItem_Function.Index = 1;
            this.menuItem_Function.Text = "&Funktion";
            this.menuItem_Function.Click += new System.EventHandler(this.menuItem_Function_Click);
            // 
            // menuItem_Settings
            // 
            this.menuItem_Settings.Index = 2;
            this.menuItem_Settings.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem_FrameVisible});
            this.menuItem_Settings.Text = "Ein&stellungen";
            // 
            // menuItem_FrameVisible
            // 
            this.menuItem_FrameVisible.Checked = true;
            this.menuItem_FrameVisible.Index = 0;
            this.menuItem_FrameVisible.Text = "&Rahmen sichtbar";
            this.menuItem_FrameVisible.Click += new System.EventHandler(this.menuItem_FrameVisible_Click);
            // 
            // MathArtsDisp_Container
            // 
            this.MathArtsDisp_Container.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MathArtsDisp_Container.Location = new System.Drawing.Point(0, 0);
            this.MathArtsDisp_Container.Name = "MathArtsDisp_Container";
            this.MathArtsDisp_Container.Size = new System.Drawing.Size(509, 262);
            this.MathArtsDisp_Container.TabIndex = 0;
            // 
            // Frm_MathArts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 262);
            this.Controls.Add(this.MathArtsDisp_Container);
            this.Menu = this.menu_MathArts;
            this.MinimumSize = new System.Drawing.Size(500, 300);
            this.Name = "Frm_MathArts";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mathematik Kunst";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MainMenu menu_MathArts;
        private System.Windows.Forms.MenuItem menuItem_File;
        private System.Windows.Forms.MenuItem menuItem_New;
        private System.Windows.Forms.MenuItem menuItem_Demo1;
        private System.Windows.Forms.MenuItem menuItem_Demo2;
        private System.Windows.Forms.MenuItem menuItem_Exit;
        private System.Windows.Forms.MenuItem menuItem_Insert;
        private System.Windows.Forms.MenuItem menuItem_Color;
        private System.Windows.Forms.MenuItem menuItem_Function;
        private System.Windows.Forms.MenuItem menuItem_Settings;
        private System.Windows.Forms.MenuItem menuItem_FrameVisible;
        private Ctl_MathArtsDisp MathArtsDisp_Container;
        private System.Windows.Forms.MenuItem menuItem_Save;
    }
}

