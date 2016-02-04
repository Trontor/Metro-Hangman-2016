namespace MetroHangman
{
    partial class Settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.btn_Color = new MetroFramework.Controls.MetroButton();
            this.btn_Disco = new MetroFramework.Controls.MetroButton();
            this.btn_Statistics = new MetroFramework.Controls.MetroButton();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.disco = new MetroFramework.Controls.MetroLabel();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.tmr_Theme = new System.Windows.Forms.Timer(this.components);
            this.metroStyleManager1 = new MetroFramework.Components.MetroStyleManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Color
            // 
            this.btn_Color.BackColor = System.Drawing.Color.DodgerBlue;
            this.btn_Color.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Color.BackgroundImage")));
            this.btn_Color.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_Color.BorderColors = false;
            this.btn_Color.ForeColor = System.Drawing.SystemColors.Control;
            this.btn_Color.Location = new System.Drawing.Point(23, 63);
            this.btn_Color.Name = "btn_Color";
            this.btn_Color.Size = new System.Drawing.Size(70, 70);
            this.btn_Color.TabIndex = 63;
            this.btn_Color.UseCustomBackColor = true;
            this.btn_Color.UseSelectable = true;
            this.btn_Color.UseStyleColors = true;
            this.btn_Color.UseVisualStyleBackColor = false;
            this.btn_Color.Click += new System.EventHandler(this.btn_Color_Click);
            // 
            // btn_Disco
            // 
            this.btn_Disco.BackColor = System.Drawing.Color.Green;
            this.btn_Disco.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Disco.BackgroundImage")));
            this.btn_Disco.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_Disco.BorderColors = false;
            this.btn_Disco.ForeColor = System.Drawing.SystemColors.Control;
            this.btn_Disco.Location = new System.Drawing.Point(115, 63);
            this.btn_Disco.Name = "btn_Disco";
            this.btn_Disco.Size = new System.Drawing.Size(70, 70);
            this.btn_Disco.TabIndex = 64;
            this.btn_Disco.UseCustomBackColor = true;
            this.btn_Disco.UseSelectable = true;
            this.btn_Disco.UseStyleColors = true;
            this.btn_Disco.UseVisualStyleBackColor = false;
            this.btn_Disco.Click += new System.EventHandler(this.btn_Disco_Click);
            // 
            // btn_Statistics
            // 
            this.btn_Statistics.BackColor = System.Drawing.Color.Black;
            this.btn_Statistics.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Statistics.BackgroundImage")));
            this.btn_Statistics.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_Statistics.BorderColors = false;
            this.btn_Statistics.ForeColor = System.Drawing.SystemColors.Control;
            this.btn_Statistics.Location = new System.Drawing.Point(207, 63);
            this.btn_Statistics.Name = "btn_Statistics";
            this.btn_Statistics.Size = new System.Drawing.Size(70, 70);
            this.btn_Statistics.TabIndex = 65;
            this.btn_Statistics.UseCustomBackColor = true;
            this.btn_Statistics.UseSelectable = true;
            this.btn_Statistics.UseStyleColors = true;
            this.btn_Statistics.UseVisualStyleBackColor = false;
            this.btn_Statistics.Click += new System.EventHandler(this.btn_Statistics_Click);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(32, 136);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(49, 19);
            this.metroLabel1.TabIndex = 66;
            this.metroLabel1.Text = "Colour";
            // 
            // disco
            // 
            this.disco.AutoSize = true;
            this.disco.Location = new System.Drawing.Point(126, 136);
            this.disco.Name = "disco";
            this.disco.Size = new System.Drawing.Size(40, 19);
            this.disco.TabIndex = 67;
            this.disco.Text = "Disco";
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(213, 136);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(57, 19);
            this.metroLabel3.TabIndex = 68;
            this.metroLabel3.Text = "Statistics";
            // 
            // tmr_Theme
            // 
            this.tmr_Theme.Interval = 999;
            this.tmr_Theme.Tick += new System.EventHandler(this.tmr_Theme_Tick);
            // 
            // metroStyleManager1
            // 
            this.metroStyleManager1.Owner = this;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 161);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.disco);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.btn_Statistics);
            this.Controls.Add(this.btn_Disco);
            this.Controls.Add(this.btn_Color);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.None;
            this.Text = "Settings";
            this.TextAlign = MetroFramework.Forms.MetroFormTextAlign.Center;
            this.Load += new System.EventHandler(this.Settings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroButton btn_Color;
        private MetroFramework.Controls.MetroButton btn_Disco;
        private MetroFramework.Controls.MetroButton btn_Statistics;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel disco;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private System.Windows.Forms.Timer tmr_Theme;
        private MetroFramework.Components.MetroStyleManager metroStyleManager1;
    }
}