using System.ComponentModel;
using System.Windows.Forms;
using MetroFramework.Components;
using MetroFramework.Controls;

namespace MetroHangman
{
    partial class Start
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Start));
            this.label1 = new System.Windows.Forms.Label();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.btn_SinglePlayer = new MetroFramework.Controls.MetroButton();
            this.btn_MultiPlayer = new MetroFramework.Controls.MetroButton();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel5 = new MetroFramework.Controls.MetroLabel();
            this.txtdynamic_Option2 = new MetroFramework.Controls.MetroLabel();
            this.pnl_MultiOptions = new System.Windows.Forms.Panel();
            this.chk_RevealWord = new MetroFramework.Controls.MetroCheckBox();
            this.chk_hideInput = new MetroFramework.Controls.MetroCheckBox();
            this.pnl_MultiDisplayer = new System.Windows.Forms.Panel();
            this.btn_showInput = new System.Windows.Forms.Button();
            this.txt_CustomWord = new MetroFramework.Controls.MetroTextBox();
            this.pnl_wordPacks = new System.Windows.Forms.Panel();
            this.rad_Hard = new MetroFramework.Controls.MetroRadioButton();
            this.rad_Common = new MetroFramework.Controls.MetroRadioButton();
            this.metroLabel6 = new MetroFramework.Controls.MetroLabel();
            this.btn_Start = new MetroFramework.Controls.MetroButton();
            this.styleManager = new MetroFramework.Components.MetroStyleManager(this.components);
            this.tmr_Disco = new System.Windows.Forms.Timer(this.components);
            this.ambiance_Separator1 = new MetroHangman.AmbianceSeparator();
            this.tmr_Theme = new System.Windows.Forms.Timer(this.components);
            this.btn_Settings = new MetroFramework.Controls.MetroButton();
            this.pnl_MultiOptions.SuspendLayout();
            this.pnl_MultiDisplayer.SuspendLayout();
            this.pnl_wordPacks.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.styleManager)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Goldenrod;
            this.label1.Location = new System.Drawing.Point(186, 130);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(243, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "WELCOME TO HANGMAN";
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel1.Location = new System.Drawing.Point(36, 60);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(296, 25);
            this.metroLabel1.TabIndex = 47;
            this.metroLabel1.Text = "Welcome to Rohyl\'s Hangman 2015";
            this.metroLabel1.UseStyleColors = true;
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(54, 85);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(268, 19);
            this.metroLabel2.TabIndex = 48;
            this.metroLabel2.Text = "Modify your game options then press Begin.";
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(22, 120);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(142, 19);
            this.metroLabel3.TabIndex = 49;
            this.metroLabel3.Text = "1. Choose Playing Type";
            // 
            // btn_SinglePlayer
            // 
            this.btn_SinglePlayer.Location = new System.Drawing.Point(22, 143);
            this.btn_SinglePlayer.Name = "btn_SinglePlayer";
            this.btn_SinglePlayer.Size = new System.Drawing.Size(156, 41);
            this.btn_SinglePlayer.TabIndex = 50;
            this.btn_SinglePlayer.Text = "Single Player";
            this.btn_SinglePlayer.UseCustomBackColor = true;
            this.btn_SinglePlayer.UseSelectable = true;
            this.btn_SinglePlayer.UseStyleColors = true;
            this.btn_SinglePlayer.Click += new System.EventHandler(this.btn_SinglePlayer_Click);
            // 
            // btn_MultiPlayer
            // 
            this.btn_MultiPlayer.Location = new System.Drawing.Point(192, 143);
            this.btn_MultiPlayer.Name = "btn_MultiPlayer";
            this.btn_MultiPlayer.Size = new System.Drawing.Size(156, 41);
            this.btn_MultiPlayer.TabIndex = 51;
            this.btn_MultiPlayer.Text = "Two Player";
            this.btn_MultiPlayer.UseCustomBackColor = true;
            this.btn_MultiPlayer.UseSelectable = true;
            this.btn_MultiPlayer.UseStyleColors = true;
            this.btn_MultiPlayer.Click += new System.EventHandler(this.btn_MultiPlayer_Click);
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.FontSize = MetroFramework.MetroLabelSize.Small;
            this.metroLabel4.Location = new System.Drawing.Point(59, 187);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(82, 15);
            this.metroLabel4.TabIndex = 52;
            this.metroLabel4.Text = "Random Word";
            // 
            // metroLabel5
            // 
            this.metroLabel5.AutoSize = true;
            this.metroLabel5.FontSize = MetroFramework.MetroLabelSize.Small;
            this.metroLabel5.Location = new System.Drawing.Point(231, 187);
            this.metroLabel5.Name = "metroLabel5";
            this.metroLabel5.Size = new System.Drawing.Size(78, 15);
            this.metroLabel5.TabIndex = 53;
            this.metroLabel5.Text = "Custom Word";
            // 
            // txtdynamic_Option2
            // 
            this.txtdynamic_Option2.AutoSize = true;
            this.txtdynamic_Option2.Location = new System.Drawing.Point(22, 209);
            this.txtdynamic_Option2.Name = "txtdynamic_Option2";
            this.txtdynamic_Option2.Size = new System.Drawing.Size(134, 19);
            this.txtdynamic_Option2.TabIndex = 54;
            this.txtdynamic_Option2.Text = "2. Choose Word Pack";
            // 
            // pnl_MultiOptions
            // 
            this.pnl_MultiOptions.Controls.Add(this.chk_RevealWord);
            this.pnl_MultiOptions.Controls.Add(this.chk_hideInput);
            this.pnl_MultiOptions.Location = new System.Drawing.Point(23, 260);
            this.pnl_MultiOptions.Name = "pnl_MultiOptions";
            this.pnl_MultiOptions.Size = new System.Drawing.Size(333, 26);
            this.pnl_MultiOptions.TabIndex = 56;
            this.pnl_MultiOptions.Visible = false;
            // 
            // chk_RevealWord
            // 
            this.chk_RevealWord.AutoSize = true;
            this.chk_RevealWord.Location = new System.Drawing.Point(185, 6);
            this.chk_RevealWord.Name = "chk_RevealWord";
            this.chk_RevealWord.Size = new System.Drawing.Size(140, 15);
            this.chk_RevealWord.TabIndex = 59;
            this.chk_RevealWord.Text = "Reveal word on failure";
            this.chk_RevealWord.UseSelectable = true;
            // 
            // chk_hideInput
            // 
            this.chk_hideInput.AutoSize = true;
            this.chk_hideInput.Location = new System.Drawing.Point(4, 6);
            this.chk_hideInput.Name = "chk_hideInput";
            this.chk_hideInput.Size = new System.Drawing.Size(147, 15);
            this.chk_hideInput.TabIndex = 58;
            this.chk_hideInput.Text = "Hide word when typing";
            this.chk_hideInput.UseSelectable = true;
            this.chk_hideInput.CheckedChanged += new System.EventHandler(this.chk_hideInput_CheckedChanged);
            // 
            // pnl_MultiDisplayer
            // 
            this.pnl_MultiDisplayer.Controls.Add(this.btn_showInput);
            this.pnl_MultiDisplayer.Controls.Add(this.txt_CustomWord);
            this.pnl_MultiDisplayer.Location = new System.Drawing.Point(23, 228);
            this.pnl_MultiDisplayer.Name = "pnl_MultiDisplayer";
            this.pnl_MultiDisplayer.Size = new System.Drawing.Size(333, 31);
            this.pnl_MultiDisplayer.TabIndex = 57;
            this.pnl_MultiDisplayer.Visible = false;
            // 
            // btn_showInput
            // 
            this.btn_showInput.BackColor = System.Drawing.Color.Transparent;
            this.btn_showInput.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_showInput.BackgroundImage")));
            this.btn_showInput.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(241)))), ((int)(((byte)(243)))));
            this.btn_showInput.FlatAppearance.BorderSize = 0;
            this.btn_showInput.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_showInput.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_showInput.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_showInput.Location = new System.Drawing.Point(303, 2);
            this.btn_showInput.Name = "btn_showInput";
            this.btn_showInput.Size = new System.Drawing.Size(24, 24);
            this.btn_showInput.TabIndex = 18;
            this.btn_showInput.UseVisualStyleBackColor = false;
            this.btn_showInput.Visible = false;
            this.btn_showInput.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_showInput_MouseDown);
            this.btn_showInput.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_showInput_MouseUp);
            // 
            // txt_CustomWord
            // 
            this.txt_CustomWord.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txt_CustomWord.Lines = new string[0];
            this.txt_CustomWord.Location = new System.Drawing.Point(4, 3);
            this.txt_CustomWord.MaxLength = 28;
            this.txt_CustomWord.Name = "txt_CustomWord";
            this.txt_CustomWord.PasswordChar = '\0';
            this.txt_CustomWord.PromptText = "Type Word for Player 2";
            this.txt_CustomWord.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txt_CustomWord.SelectedText = "";
            this.txt_CustomWord.Size = new System.Drawing.Size(326, 26);
            this.txt_CustomWord.TabIndex = 58;
            this.txt_CustomWord.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_CustomWord.UseSelectable = true;
            this.txt_CustomWord.TextChanged += new System.EventHandler(this.txt_CustomWord_TextChanged);
            this.txt_CustomWord.Click += new System.EventHandler(this.txt_CustomWord_TextChanged);
            // 
            // pnl_wordPacks
            // 
            this.pnl_wordPacks.Controls.Add(this.rad_Hard);
            this.pnl_wordPacks.Controls.Add(this.rad_Common);
            this.pnl_wordPacks.Font = new System.Drawing.Font("Tahoma", 9F);
            this.pnl_wordPacks.Location = new System.Drawing.Point(24, 241);
            this.pnl_wordPacks.Name = "pnl_wordPacks";
            this.pnl_wordPacks.Size = new System.Drawing.Size(332, 31);
            this.pnl_wordPacks.TabIndex = 55;
            this.pnl_wordPacks.Text = "ambiance_Panel1";
            // 
            // rad_Hard
            // 
            this.rad_Hard.AutoSize = true;
            this.rad_Hard.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.rad_Hard.Location = new System.Drawing.Point(197, 6);
            this.rad_Hard.Name = "rad_Hard";
            this.rad_Hard.Size = new System.Drawing.Size(98, 19);
            this.rad_Hard.TabIndex = 61;
            this.rad_Hard.Text = "Hard Words";
            this.rad_Hard.UseSelectable = true;
            // 
            // rad_Common
            // 
            this.rad_Common.AutoSize = true;
            this.rad_Common.Checked = true;
            this.rad_Common.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.rad_Common.Location = new System.Drawing.Point(12, 6);
            this.rad_Common.Name = "rad_Common";
            this.rad_Common.Size = new System.Drawing.Size(125, 19);
            this.rad_Common.TabIndex = 60;
            this.rad_Common.TabStop = true;
            this.rad_Common.Text = "Common Words";
            this.rad_Common.UseSelectable = true;
            // 
            // metroLabel6
            // 
            this.metroLabel6.AutoSize = true;
            this.metroLabel6.Location = new System.Drawing.Point(22, 305);
            this.metroLabel6.Name = "metroLabel6";
            this.metroLabel6.Size = new System.Drawing.Size(104, 19);
            this.metroLabel6.TabIndex = 58;
            this.metroLabel6.Text = "3. Press Enter or";
            // 
            // btn_Start
            // 
            this.btn_Start.ForeColor = System.Drawing.SystemColors.Control;
            this.btn_Start.Location = new System.Drawing.Point(145, 296);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(155, 41);
            this.btn_Start.TabIndex = 59;
            this.btn_Start.Text = "Begin";
            this.btn_Start.UseCustomBackColor = true;
            this.btn_Start.UseSelectable = true;
            this.btn_Start.UseStyleColors = true;
            this.btn_Start.Click += new System.EventHandler(this.btn_Start_Click);
            // 
            // styleManager
            // 
            this.styleManager.Owner = this;
            // 
            // tmr_Disco
            // 
            this.tmr_Disco.Interval = 1000;
            // 
            // ambiance_Separator1
            // 
            this.ambiance_Separator1.Location = new System.Drawing.Point(22, 107);
            this.ambiance_Separator1.Name = "ambiance_Separator1";
            this.ambiance_Separator1.Size = new System.Drawing.Size(326, 10);
            this.ambiance_Separator1.TabIndex = 31;
            this.ambiance_Separator1.Text = "ambiance_Separator1";
            // 
            // tmr_Theme
            // 
            this.tmr_Theme.Enabled = true;
            this.tmr_Theme.Interval = 1000;
            this.tmr_Theme.Tick += new System.EventHandler(this.tmr_Theme_Tick);
            // 
            // btn_Settings
            // 
            this.btn_Settings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_Settings.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Settings.BackgroundImage")));
            this.btn_Settings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_Settings.BorderColors = false;
            this.btn_Settings.Location = new System.Drawing.Point(0, 5);
            this.btn_Settings.Name = "btn_Settings";
            this.btn_Settings.Size = new System.Drawing.Size(30, 30);
            this.btn_Settings.TabIndex = 60;
            this.btn_Settings.UseCustomBackColor = true;
            this.btn_Settings.UseSelectable = true;
            this.btn_Settings.UseStyleColors = true;
            this.btn_Settings.UseVisualStyleBackColor = false;
            this.btn_Settings.Click += new System.EventHandler(this.btn_Settings_Click);
            // 
            // Start
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(371, 350);
            this.Controls.Add(this.btn_Settings);
            this.Controls.Add(this.btn_Start);
            this.Controls.Add(this.metroLabel6);
            this.Controls.Add(this.pnl_wordPacks);
            this.Controls.Add(this.txtdynamic_Option2);
            this.Controls.Add(this.metroLabel5);
            this.Controls.Add(this.metroLabel4);
            this.Controls.Add(this.btn_MultiPlayer);
            this.Controls.Add(this.btn_SinglePlayer);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.ambiance_Separator1);
            this.Controls.Add(this.pnl_MultiDisplayer);
            this.Controls.Add(this.pnl_MultiOptions);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(371, 350);
            this.MinimumSize = new System.Drawing.Size(371, 350);
            this.Name = "Start";
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.DropShadow;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Style = MetroFramework.MetroColorStyle.Default;
            this.Text = "Hangman";
            this.TextAlign = MetroFramework.Forms.MetroFormTextAlign.Center;
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.Load += new System.EventHandler(this.Start_Load);
            this.pnl_MultiOptions.ResumeLayout(false);
            this.pnl_MultiOptions.PerformLayout();
            this.pnl_MultiDisplayer.ResumeLayout(false);
            this.pnl_wordPacks.ResumeLayout(false);
            this.pnl_wordPacks.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.styleManager)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private AmbianceSeparator ambiance_Separator1;
        private MetroLabel metroLabel1;
        private MetroLabel metroLabel2;
        private MetroLabel metroLabel3;
        private MetroButton btn_SinglePlayer;
        private MetroButton btn_MultiPlayer;
        private MetroLabel metroLabel4;
        private MetroLabel metroLabel5;
        private MetroLabel txtdynamic_Option2;
        private Panel pnl_MultiOptions;
        private Panel pnl_MultiDisplayer;
        private Button btn_showInput;
        private Panel pnl_wordPacks;
        private MetroTextBox txt_CustomWord;
        private MetroCheckBox chk_hideInput;
        private MetroCheckBox chk_RevealWord;
        private MetroLabel metroLabel6;
        private MetroButton btn_Start;
        private MetroRadioButton rad_Common;
        private MetroRadioButton rad_Hard;
        private MetroStyleManager styleManager;
        private Timer tmr_Disco;
        private Timer tmr_Theme;
        private MetroButton btn_Settings;
    }
}

