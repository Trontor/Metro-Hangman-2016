namespace Hangman
{
    partial class profiles
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(profiles));
            this.lbl_hardLosses = new Ambiance.Ambiance_Label();
            this.lbl_hardWins = new Ambiance.Ambiance_Label();
            this.lbl_commonLosses = new Ambiance.Ambiance_Label();
            this.lbl_commonWins = new Ambiance.Ambiance_Label();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel5 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel6 = new MetroFramework.Controls.MetroLabel();
            this.tmr_Theme = new System.Windows.Forms.Timer(this.components);
            this.sMgr = new MetroFramework.Components.MetroStyleManager(this.components);
            this.btn_resetStatistics = new MetroFramework.Controls.MetroButton();
            ((System.ComponentModel.ISupportInitialize)(this.sMgr)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_hardLosses
            // 
            this.lbl_hardLosses.BackColor = System.Drawing.Color.Transparent;
            this.lbl_hardLosses.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lbl_hardLosses.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(76)))), ((int)(((byte)(77)))));
            this.lbl_hardLosses.Location = new System.Drawing.Point(140, 205);
            this.lbl_hardLosses.Name = "lbl_hardLosses";
            this.lbl_hardLosses.Size = new System.Drawing.Size(46, 20);
            this.lbl_hardLosses.TabIndex = 9;
            this.lbl_hardLosses.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_hardWins
            // 
            this.lbl_hardWins.BackColor = System.Drawing.Color.Transparent;
            this.lbl_hardWins.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lbl_hardWins.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(76)))), ((int)(((byte)(77)))));
            this.lbl_hardWins.Location = new System.Drawing.Point(79, 205);
            this.lbl_hardWins.Name = "lbl_hardWins";
            this.lbl_hardWins.Size = new System.Drawing.Size(41, 20);
            this.lbl_hardWins.TabIndex = 8;
            this.lbl_hardWins.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_commonLosses
            // 
            this.lbl_commonLosses.BackColor = System.Drawing.Color.Transparent;
            this.lbl_commonLosses.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lbl_commonLosses.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(76)))), ((int)(((byte)(77)))));
            this.lbl_commonLosses.Location = new System.Drawing.Point(139, 117);
            this.lbl_commonLosses.Name = "lbl_commonLosses";
            this.lbl_commonLosses.Size = new System.Drawing.Size(46, 20);
            this.lbl_commonLosses.TabIndex = 4;
            this.lbl_commonLosses.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_commonWins
            // 
            this.lbl_commonWins.BackColor = System.Drawing.Color.Transparent;
            this.lbl_commonWins.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lbl_commonWins.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(76)))), ((int)(((byte)(77)))));
            this.lbl_commonWins.Location = new System.Drawing.Point(79, 117);
            this.lbl_commonWins.Name = "lbl_commonWins";
            this.lbl_commonWins.Size = new System.Drawing.Size(41, 20);
            this.lbl_commonWins.TabIndex = 3;
            this.lbl_commonWins.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_commonWins.Click += new System.EventHandler(this.lbl_commonWins_Click);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel1.Location = new System.Drawing.Point(59, 73);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(145, 25);
            this.metroLabel1.TabIndex = 43;
            this.metroLabel1.Text = "Common Words";
            this.metroLabel1.UseStyleColors = true;
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel2.Location = new System.Drawing.Point(83, 98);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(33, 19);
            this.metroLabel2.TabIndex = 44;
            this.metroLabel2.Text = "Win";
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel3.Location = new System.Drawing.Point(143, 98);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(36, 19);
            this.metroLabel3.TabIndex = 45;
            this.metroLabel3.Text = "Loss";
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel4.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel4.Location = new System.Drawing.Point(77, 158);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(109, 25);
            this.metroLabel4.TabIndex = 46;
            this.metroLabel4.Text = "Hard Words";
            this.metroLabel4.UseStyleColors = true;
            // 
            // metroLabel5
            // 
            this.metroLabel5.AutoSize = true;
            this.metroLabel5.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel5.Location = new System.Drawing.Point(143, 183);
            this.metroLabel5.Name = "metroLabel5";
            this.metroLabel5.Size = new System.Drawing.Size(36, 19);
            this.metroLabel5.TabIndex = 48;
            this.metroLabel5.Text = "Loss";
            // 
            // metroLabel6
            // 
            this.metroLabel6.AutoSize = true;
            this.metroLabel6.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel6.Location = new System.Drawing.Point(83, 183);
            this.metroLabel6.Name = "metroLabel6";
            this.metroLabel6.Size = new System.Drawing.Size(33, 19);
            this.metroLabel6.TabIndex = 47;
            this.metroLabel6.Text = "Win";
            // 
            // tmr_Theme
            // 
            this.tmr_Theme.Enabled = true;
            this.tmr_Theme.Tick += new System.EventHandler(this.tmr_Theme_Tick);
            // 
            // sMgr
            // 
            this.sMgr.Owner = this;
            // 
            // btn_resetStatistics
            // 
            this.btn_resetStatistics.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_resetStatistics.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_resetStatistics.BackgroundImage")));
            this.btn_resetStatistics.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_resetStatistics.BorderColors = false;
            this.btn_resetStatistics.Location = new System.Drawing.Point(0, 211);
            this.btn_resetStatistics.Name = "btn_resetStatistics";
            this.btn_resetStatistics.Size = new System.Drawing.Size(30, 30);
            this.btn_resetStatistics.TabIndex = 61;
            this.btn_resetStatistics.UseCustomBackColor = true;
            this.btn_resetStatistics.UseSelectable = true;
            this.btn_resetStatistics.UseStyleColors = true;
            this.btn_resetStatistics.Click += new System.EventHandler(this.btn_resetStatistics_Click);
            // 
            // profiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(261, 241);
            this.Controls.Add(this.btn_resetStatistics);
            this.Controls.Add(this.lbl_hardLosses);
            this.Controls.Add(this.lbl_hardWins);
            this.Controls.Add(this.metroLabel5);
            this.Controls.Add(this.metroLabel6);
            this.Controls.Add(this.metroLabel4);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.lbl_commonLosses);
            this.Controls.Add(this.lbl_commonWins);
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(261, 65);
            this.Name = "profiles";
            this.Style = MetroFramework.MetroColorStyle.Default;
            this.Text = "Statistics";
            this.TextAlign = MetroFramework.Forms.MetroFormTextAlign.Center;
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            ((System.ComponentModel.ISupportInitialize)(this.sMgr)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Ambiance.Ambiance_Label lbl_hardLosses;
        private Ambiance.Ambiance_Label lbl_hardWins;
        private Ambiance.Ambiance_Label lbl_commonLosses;
        private Ambiance.Ambiance_Label lbl_commonWins;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private MetroFramework.Controls.MetroLabel metroLabel5;
        private MetroFramework.Controls.MetroLabel metroLabel6;
        private System.Windows.Forms.Timer tmr_Theme;
        private MetroFramework.Components.MetroStyleManager sMgr;
        private MetroFramework.Controls.MetroButton btn_resetStatistics;

    }
}