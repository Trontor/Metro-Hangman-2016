using System.ComponentModel;
using System.Windows.Forms;
using MetroFramework.Components;
using MetroFramework.Controls;

namespace MetroHangman
{
    sealed partial class Session
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Session));
            this.letterbase = new System.Windows.Forms.Panel();
            this.pnl_Win = new System.Windows.Forms.Panel();
            this.win_Attempts = new System.Windows.Forms.Label();
            this.win_Main = new System.Windows.Forms.Label();
            this.pnl_KeyPad = new System.Windows.Forms.Panel();
            this.pnl_Loss = new System.Windows.Forms.Panel();
            this.loss_LossMessage = new System.Windows.Forms.Label();
            this.loss_LookingFor = new System.Windows.Forms.Label();
            this.loss_CorrectWord = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_showInput = new System.Windows.Forms.Button();
            this.pnl_NewMulti = new System.Windows.Forms.Panel();
            this.btn_go = new MetroFramework.Controls.MetroButton();
            this.btn_changeMode2 = new MetroFramework.Controls.MetroButton();
            this.pnl_MultiOptions = new System.Windows.Forms.Panel();
            this.chk_RevealWord = new MetroFramework.Controls.MetroCheckBox();
            this.chk_hideInput = new MetroFramework.Controls.MetroCheckBox();
            this.ambiance_Label1 = new AmbianceLabel();
            this.lbl_placeHolderNewWord = new AmbianceLabel();
            this.txt_newCustomWord = new MetroFramework.Controls.MetroTextBox();
            this.pnl_NewSingle = new System.Windows.Forms.Panel();
            this.btn_changeMode1 = new MetroFramework.Controls.MetroButton();
            this.btn_StartNewSession = new MetroFramework.Controls.MetroButton();
            this.ambiance_Label5 = new AmbianceLabel();
            this.ambiance_Label3 = new AmbianceLabel();
            this.ambiance_Label2 = new AmbianceLabel();
            this.ambiance_Label4 = new AmbianceLabel();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.lbl_AttemptsLeft = new MetroFramework.Controls.MetroLabel();
            this.lbl_PlaceHolderNextPhase = new MetroFramework.Controls.MetroLabel();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.lbl_IncorrectGuesses = new MetroFramework.Controls.MetroLabel();
            this.metroLabel6 = new MetroFramework.Controls.MetroLabel();
            this.metroProgressBar1 = new MetroFramework.Controls.MetroProgressBar();
            this.styleManager = new MetroFramework.Components.MetroStyleManager(this.components);
            this.tmr_Theme = new System.Windows.Forms.Timer(this.components);
            this.btn_definition = new MetroFramework.Controls.MetroButton();
            this.lbl_PROMPT = new AmbianceLabel();
            this.ambiance_Separator2 = new AmbianceSeparator();
            this.ambiance_Separator1 = new AmbianceSeparator();
            this.pnl_Win.SuspendLayout();
            this.pnl_Loss.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.pnl_NewMulti.SuspendLayout();
            this.pnl_MultiOptions.SuspendLayout();
            this.pnl_NewSingle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.styleManager)).BeginInit();
            this.SuspendLayout();
            // 
            // letterbase
            // 
            this.letterbase.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.letterbase.Location = new System.Drawing.Point(6, 1);
            this.letterbase.Name = "letterbase";
            this.letterbase.Size = new System.Drawing.Size(55, 31);
            this.letterbase.TabIndex = 9;
            // 
            // pnl_Win
            // 
            this.pnl_Win.BackColor = System.Drawing.Color.Transparent;
            this.pnl_Win.Controls.Add(this.win_Attempts);
            this.pnl_Win.Controls.Add(this.win_Main);
            this.pnl_Win.Location = new System.Drawing.Point(17, 433);
            this.pnl_Win.Name = "pnl_Win";
            this.pnl_Win.Size = new System.Drawing.Size(695, 97);
            this.pnl_Win.TabIndex = 25;
            this.pnl_Win.Visible = false;
            // 
            // win_Attempts
            // 
            this.win_Attempts.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.win_Attempts.ForeColor = System.Drawing.Color.DarkGreen;
            this.win_Attempts.Location = new System.Drawing.Point(0, 47);
            this.win_Attempts.Name = "win_Attempts";
            this.win_Attempts.Size = new System.Drawing.Size(695, 44);
            this.win_Attempts.TabIndex = 13;
            this.win_Attempts.Text = "You only took x attempts";
            this.win_Attempts.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // win_Main
            // 
            this.win_Main.Font = new System.Drawing.Font("Segoe UI", 24.25F, System.Drawing.FontStyle.Bold);
            this.win_Main.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.win_Main.Location = new System.Drawing.Point(0, 0);
            this.win_Main.Name = "win_Main";
            this.win_Main.Size = new System.Drawing.Size(695, 44);
            this.win_Main.TabIndex = 12;
            this.win_Main.Text = "Congratulations! You have won.";
            this.win_Main.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnl_KeyPad
            // 
            this.pnl_KeyPad.BackColor = System.Drawing.Color.Transparent;
            this.pnl_KeyPad.Location = new System.Drawing.Point(23, 430);
            this.pnl_KeyPad.Name = "pnl_KeyPad";
            this.pnl_KeyPad.Size = new System.Drawing.Size(695, 97);
            this.pnl_KeyPad.TabIndex = 17;
            // 
            // pnl_Loss
            // 
            this.pnl_Loss.BackColor = System.Drawing.Color.Transparent;
            this.pnl_Loss.Controls.Add(this.loss_LossMessage);
            this.pnl_Loss.Controls.Add(this.loss_LookingFor);
            this.pnl_Loss.Controls.Add(this.loss_CorrectWord);
            this.pnl_Loss.Location = new System.Drawing.Point(17, 433);
            this.pnl_Loss.Name = "pnl_Loss";
            this.pnl_Loss.Size = new System.Drawing.Size(695, 97);
            this.pnl_Loss.TabIndex = 18;
            this.pnl_Loss.Visible = false;
            // 
            // loss_LossMessage
            // 
            this.loss_LossMessage.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loss_LossMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.loss_LossMessage.Location = new System.Drawing.Point(4, 1);
            this.loss_LossMessage.Name = "loss_LossMessage";
            this.loss_LossMessage.Size = new System.Drawing.Size(689, 27);
            this.loss_LossMessage.TabIndex = 12;
            this.loss_LossMessage.Text = "Sorry, you have lost this session.";
            this.loss_LossMessage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // loss_LookingFor
            // 
            this.loss_LookingFor.Font = new System.Drawing.Font("Segoe UI", 10.25F, System.Drawing.FontStyle.Bold);
            this.loss_LookingFor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.loss_LookingFor.Location = new System.Drawing.Point(3, 25);
            this.loss_LookingFor.Name = "loss_LookingFor";
            this.loss_LookingFor.Size = new System.Drawing.Size(689, 19);
            this.loss_LookingFor.TabIndex = 13;
            this.loss_LookingFor.Text = "The word you were looking for was";
            this.loss_LookingFor.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // loss_CorrectWord
            // 
            this.loss_CorrectWord.Font = new System.Drawing.Font("Segoe UI Semibold", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loss_CorrectWord.ForeColor = System.Drawing.Color.Black;
            this.loss_CorrectWord.Location = new System.Drawing.Point(3, 35);
            this.loss_CorrectWord.Name = "loss_CorrectWord";
            this.loss_CorrectWord.Size = new System.Drawing.Size(689, 69);
            this.loss_CorrectWord.TabIndex = 14;
            this.loss_CorrectWord.Text = "LIFE";
            this.loss_CorrectWord.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(16, 95);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(698, 201);
            this.pictureBox1.TabIndex = 21;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.letterbase);
            this.panel1.Location = new System.Drawing.Point(16, 316);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(698, 71);
            this.panel1.TabIndex = 23;
            // 
            // btn_showInput
            // 
            this.btn_showInput.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_showInput.BackgroundImage")));
            this.btn_showInput.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(241)))), ((int)(((byte)(243)))));
            this.btn_showInput.FlatAppearance.BorderSize = 0;
            this.btn_showInput.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_showInput.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_showInput.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_showInput.Location = new System.Drawing.Point(491, 42);
            this.btn_showInput.Name = "btn_showInput";
            this.btn_showInput.Size = new System.Drawing.Size(24, 24);
            this.btn_showInput.TabIndex = 21;
            this.btn_showInput.UseVisualStyleBackColor = true;
            this.btn_showInput.Visible = false;
            this.btn_showInput.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_showInput_MouseDown);
            this.btn_showInput.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_showInput_MouseUp);
            // 
            // pnl_NewMulti
            // 
            this.pnl_NewMulti.BackColor = System.Drawing.Color.Transparent;
            this.pnl_NewMulti.Controls.Add(this.btn_go);
            this.pnl_NewMulti.Controls.Add(this.btn_changeMode2);
            this.pnl_NewMulti.Controls.Add(this.btn_showInput);
            this.pnl_NewMulti.Controls.Add(this.pnl_MultiOptions);
            this.pnl_NewMulti.Controls.Add(this.ambiance_Label1);
            this.pnl_NewMulti.Controls.Add(this.lbl_placeHolderNewWord);
            this.pnl_NewMulti.Controls.Add(this.txt_newCustomWord);
            this.pnl_NewMulti.Location = new System.Drawing.Point(16, 527);
            this.pnl_NewMulti.Name = "pnl_NewMulti";
            this.pnl_NewMulti.Size = new System.Drawing.Size(697, 176);
            this.pnl_NewMulti.TabIndex = 26;
            this.pnl_NewMulti.Paint += new System.Windows.Forms.PaintEventHandler(this.pnl_NewMulti_Paint);
            // 
            // btn_go
            // 
            this.btn_go.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.btn_go.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.btn_go.FontWeight = MetroFramework.MetroButtonWeight.Light;
            this.btn_go.Location = new System.Drawing.Point(526, 42);
            this.btn_go.Name = "btn_go";
            this.btn_go.Size = new System.Drawing.Size(54, 27);
            this.btn_go.TabIndex = 60;
            this.btn_go.Text = "GO";
            this.btn_go.UseCustomBackColor = true;
            this.btn_go.UseSelectable = true;
            this.btn_go.UseVisualStyleBackColor = false;
            this.btn_go.Click += new System.EventHandler(this.btn_go_Click);
            // 
            // btn_changeMode2
            // 
            this.btn_changeMode2.BackColor = System.Drawing.Color.White;
            this.btn_changeMode2.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.btn_changeMode2.FontWeight = MetroFramework.MetroButtonWeight.Light;
            this.btn_changeMode2.Location = new System.Drawing.Point(248, 131);
            this.btn_changeMode2.Name = "btn_changeMode2";
            this.btn_changeMode2.Size = new System.Drawing.Size(207, 37);
            this.btn_changeMode2.TabIndex = 37;
            this.btn_changeMode2.Text = "Change Game Mode";
            this.btn_changeMode2.UseSelectable = true;
            this.btn_changeMode2.UseVisualStyleBackColor = false;
            this.btn_changeMode2.Click += new System.EventHandler(this.btn_changeMode_Click);
            // 
            // pnl_MultiOptions
            // 
            this.pnl_MultiOptions.Controls.Add(this.chk_RevealWord);
            this.pnl_MultiOptions.Controls.Add(this.chk_hideInput);
            this.pnl_MultiOptions.Location = new System.Drawing.Point(183, 75);
            this.pnl_MultiOptions.Name = "pnl_MultiOptions";
            this.pnl_MultiOptions.Size = new System.Drawing.Size(351, 32);
            this.pnl_MultiOptions.TabIndex = 20;
            // 
            // chk_RevealWord
            // 
            this.chk_RevealWord.AutoSize = true;
            this.chk_RevealWord.Location = new System.Drawing.Point(192, 10);
            this.chk_RevealWord.Name = "chk_RevealWord";
            this.chk_RevealWord.Size = new System.Drawing.Size(140, 15);
            this.chk_RevealWord.TabIndex = 61;
            this.chk_RevealWord.Text = "Reveal word on failure";
            this.chk_RevealWord.UseSelectable = true;
            // 
            // chk_hideInput
            // 
            this.chk_hideInput.AutoSize = true;
            this.chk_hideInput.Location = new System.Drawing.Point(11, 10);
            this.chk_hideInput.Name = "chk_hideInput";
            this.chk_hideInput.Size = new System.Drawing.Size(147, 15);
            this.chk_hideInput.TabIndex = 60;
            this.chk_hideInput.Text = "Hide word when typing";
            this.chk_hideInput.UseSelectable = true;
            this.chk_hideInput.CheckedChanged += new System.EventHandler(this.chk_hideInput_CheckedChanged);
            // 
            // ambiance_Label1
            // 
            this.ambiance_Label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ambiance_Label1.AutoSize = true;
            this.ambiance_Label1.BackColor = System.Drawing.Color.Transparent;
            this.ambiance_Label1.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.ambiance_Label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(76)))), ((int)(((byte)(77)))));
            this.ambiance_Label1.Location = new System.Drawing.Point(338, 104);
            this.ambiance_Label1.Name = "ambiance_Label1";
            this.ambiance_Label1.Size = new System.Drawing.Size(30, 25);
            this.ambiance_Label1.TabIndex = 18;
            this.ambiance_Label1.Text = "or";
            // 
            // lbl_placeHolderNewWord
            // 
            this.lbl_placeHolderNewWord.AutoSize = true;
            this.lbl_placeHolderNewWord.BackColor = System.Drawing.Color.Transparent;
            this.lbl_placeHolderNewWord.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.lbl_placeHolderNewWord.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(76)))), ((int)(((byte)(77)))));
            this.lbl_placeHolderNewWord.Location = new System.Drawing.Point(112, 10);
            this.lbl_placeHolderNewWord.Name = "lbl_placeHolderNewWord";
            this.lbl_placeHolderNewWord.Size = new System.Drawing.Size(477, 25);
            this.lbl_placeHolderNewWord.TabIndex = 17;
            this.lbl_placeHolderNewWord.Text = "Type a word for the next player then press enter or \'GO\'";
            // 
            // txt_newCustomWord
            // 
            this.txt_newCustomWord.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txt_newCustomWord.Lines = new string[0];
            this.txt_newCustomWord.Location = new System.Drawing.Point(183, 42);
            this.txt_newCustomWord.MaxLength = 28;
            this.txt_newCustomWord.Name = "txt_newCustomWord";
            this.txt_newCustomWord.PasswordChar = '\0';
            this.txt_newCustomWord.PromptText = "Type Word for Player 2";
            this.txt_newCustomWord.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txt_newCustomWord.SelectedText = "";
            this.txt_newCustomWord.Size = new System.Drawing.Size(332, 26);
            this.txt_newCustomWord.TabIndex = 59;
            this.txt_newCustomWord.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_newCustomWord.UseSelectable = true;
            this.txt_newCustomWord.TextChanged += new System.EventHandler(this.txt_newCustomWord_TextChanged);
            // 
            // pnl_NewSingle
            // 
            this.pnl_NewSingle.BackColor = System.Drawing.Color.Transparent;
            this.pnl_NewSingle.Controls.Add(this.btn_changeMode1);
            this.pnl_NewSingle.Controls.Add(this.btn_StartNewSession);
            this.pnl_NewSingle.Controls.Add(this.ambiance_Label5);
            this.pnl_NewSingle.Controls.Add(this.ambiance_Label3);
            this.pnl_NewSingle.Controls.Add(this.ambiance_Label2);
            this.pnl_NewSingle.Controls.Add(this.ambiance_Label4);
            this.pnl_NewSingle.Location = new System.Drawing.Point(16, 527);
            this.pnl_NewSingle.Name = "pnl_NewSingle";
            this.pnl_NewSingle.Size = new System.Drawing.Size(692, 70);
            this.pnl_NewSingle.TabIndex = 27;
            this.pnl_NewSingle.Visible = false;
            // 
            // btn_changeMode1
            // 
            this.btn_changeMode1.BackColor = System.Drawing.Color.White;
            this.btn_changeMode1.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.btn_changeMode1.FontWeight = MetroFramework.MetroButtonWeight.Light;
            this.btn_changeMode1.Location = new System.Drawing.Point(412, 9);
            this.btn_changeMode1.Name = "btn_changeMode1";
            this.btn_changeMode1.Size = new System.Drawing.Size(207, 37);
            this.btn_changeMode1.TabIndex = 36;
            this.btn_changeMode1.Text = "change your game options";
            this.btn_changeMode1.UseSelectable = true;
            this.btn_changeMode1.UseVisualStyleBackColor = false;
            this.btn_changeMode1.Click += new System.EventHandler(this.btn_ChangeMode1_Click);
            // 
            // btn_StartNewSession
            // 
            this.btn_StartNewSession.BackColor = System.Drawing.Color.White;
            this.btn_StartNewSession.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.btn_StartNewSession.FontWeight = MetroFramework.MetroButtonWeight.Light;
            this.btn_StartNewSession.Location = new System.Drawing.Point(217, 9);
            this.btn_StartNewSession.Name = "btn_StartNewSession";
            this.btn_StartNewSession.Size = new System.Drawing.Size(166, 37);
            this.btn_StartNewSession.TabIndex = 35;
            this.btn_StartNewSession.Text = "start a new game";
            this.btn_StartNewSession.UseSelectable = true;
            this.btn_StartNewSession.UseVisualStyleBackColor = false;
            this.btn_StartNewSession.Click += new System.EventHandler(this.btn_StartNewSession_Click);
            // 
            // ambiance_Label5
            // 
            this.ambiance_Label5.AutoSize = true;
            this.ambiance_Label5.BackColor = System.Drawing.Color.Transparent;
            this.ambiance_Label5.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.ambiance_Label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(76)))), ((int)(((byte)(77)))));
            this.ambiance_Label5.Location = new System.Drawing.Point(381, 9);
            this.ambiance_Label5.Name = "ambiance_Label5";
            this.ambiance_Label5.Size = new System.Drawing.Size(34, 30);
            this.ambiance_Label5.TabIndex = 27;
            this.ambiance_Label5.Text = "or";
            // 
            // ambiance_Label3
            // 
            this.ambiance_Label3.AutoSize = true;
            this.ambiance_Label3.BackColor = System.Drawing.Color.Transparent;
            this.ambiance_Label3.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.ambiance_Label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(76)))), ((int)(((byte)(77)))));
            this.ambiance_Label3.Location = new System.Drawing.Point(264, 43);
            this.ambiance_Label3.Name = "ambiance_Label3";
            this.ambiance_Label3.Size = new System.Drawing.Size(66, 21);
            this.ambiance_Label3.TabIndex = 25;
            this.ambiance_Label3.Text = "(ENTER)";
            // 
            // ambiance_Label2
            // 
            this.ambiance_Label2.AutoSize = true;
            this.ambiance_Label2.BackColor = System.Drawing.Color.Transparent;
            this.ambiance_Label2.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.ambiance_Label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(76)))), ((int)(((byte)(77)))));
            this.ambiance_Label2.Location = new System.Drawing.Point(487, 43);
            this.ambiance_Label2.Name = "ambiance_Label2";
            this.ambiance_Label2.Size = new System.Drawing.Size(47, 21);
            this.ambiance_Label2.TabIndex = 23;
            this.ambiance_Label2.Text = "(ESC)";
            // 
            // ambiance_Label4
            // 
            this.ambiance_Label4.AutoSize = true;
            this.ambiance_Label4.BackColor = System.Drawing.Color.Transparent;
            this.ambiance_Label4.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.ambiance_Label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(76)))), ((int)(((byte)(77)))));
            this.ambiance_Label4.Location = new System.Drawing.Point(80, 9);
            this.ambiance_Label4.Name = "ambiance_Label4";
            this.ambiance_Label4.Size = new System.Drawing.Size(134, 30);
            this.ambiance_Label4.TabIndex = 26;
            this.ambiance_Label4.Text = "You can now";
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(16, 60);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(90, 19);
            this.metroLabel1.TabIndex = 28;
            this.metroLabel1.Text = "Attempts Left:";
            // 
            // lbl_AttemptsLeft
            // 
            this.lbl_AttemptsLeft.AutoSize = true;
            this.lbl_AttemptsLeft.Location = new System.Drawing.Point(107, 60);
            this.lbl_AttemptsLeft.Name = "lbl_AttemptsLeft";
            this.lbl_AttemptsLeft.Size = new System.Drawing.Size(16, 19);
            this.lbl_AttemptsLeft.TabIndex = 29;
            this.lbl_AttemptsLeft.Text = "0";
            // 
            // lbl_PlaceHolderNextPhase
            // 
            this.lbl_PlaceHolderNextPhase.AutoSize = true;
            this.lbl_PlaceHolderNextPhase.Location = new System.Drawing.Point(389, 60);
            this.lbl_PlaceHolderNextPhase.Name = "lbl_PlaceHolderNextPhase";
            this.lbl_PlaceHolderNextPhase.Size = new System.Drawing.Size(36, 19);
            this.lbl_PlaceHolderNextPhase.TabIndex = 31;
            this.lbl_PlaceHolderNextPhase.Text = "Base";
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.Location = new System.Drawing.Point(303, 60);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(77, 19);
            this.metroLabel4.TabIndex = 30;
            this.metroLabel4.Text = "Next Phase:";
            // 
            // lbl_IncorrectGuesses
            // 
            this.lbl_IncorrectGuesses.AutoSize = true;
            this.lbl_IncorrectGuesses.Location = new System.Drawing.Point(698, 60);
            this.lbl_IncorrectGuesses.Name = "lbl_IncorrectGuesses";
            this.lbl_IncorrectGuesses.Size = new System.Drawing.Size(16, 19);
            this.lbl_IncorrectGuesses.TabIndex = 33;
            this.lbl_IncorrectGuesses.Text = "0";
            // 
            // metroLabel6
            // 
            this.metroLabel6.AutoSize = true;
            this.metroLabel6.Location = new System.Drawing.Point(586, 60);
            this.metroLabel6.Name = "metroLabel6";
            this.metroLabel6.Size = new System.Drawing.Size(112, 19);
            this.metroLabel6.TabIndex = 32;
            this.metroLabel6.Text = "Incorrect Guesses:";
            // 
            // metroProgressBar1
            // 
            this.metroProgressBar1.Location = new System.Drawing.Point(16, 299);
            this.metroProgressBar1.Name = "metroProgressBar1";
            this.metroProgressBar1.Size = new System.Drawing.Size(698, 23);
            this.metroProgressBar1.TabIndex = 34;
            // 
            // styleManager
            // 
            this.styleManager.Owner = this;
            // 
            // tmr_Theme
            // 
            this.tmr_Theme.Enabled = true;
            this.tmr_Theme.Tick += new System.EventHandler(this.tmr_Disco_Tick);
            // 
            // btn_definition
            // 
            this.btn_definition.BackColor = System.Drawing.Color.White;
            this.btn_definition.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btn_definition.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btn_definition.Location = new System.Drawing.Point(304, 393);
            this.btn_definition.Name = "btn_definition";
            this.btn_definition.Size = new System.Drawing.Size(127, 25);
            this.btn_definition.TabIndex = 37;
            this.btn_definition.Text = "Request Definition";
            this.btn_definition.UseSelectable = true;
            this.btn_definition.UseVisualStyleBackColor = false;
            this.btn_definition.Visible = false;
            this.btn_definition.Click += new System.EventHandler(this.btn_definition_Click);
            // 
            // lbl_PROMPT
            // 
            this.lbl_PROMPT.AutoSize = true;
            this.lbl_PROMPT.BackColor = System.Drawing.Color.Transparent;
            this.lbl_PROMPT.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.lbl_PROMPT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(76)))), ((int)(((byte)(77)))));
            this.lbl_PROMPT.Location = new System.Drawing.Point(217, 184);
            this.lbl_PROMPT.Name = "lbl_PROMPT";
            this.lbl_PROMPT.Size = new System.Drawing.Size(305, 25);
            this.lbl_PROMPT.TabIndex = 20;
            this.lbl_PROMPT.Text = "Click or Type your first letter guess.";
            // 
            // ambiance_Separator2
            // 
            this.ambiance_Separator2.Location = new System.Drawing.Point(16, 300);
            this.ambiance_Separator2.Name = "ambiance_Separator2";
            this.ambiance_Separator2.Size = new System.Drawing.Size(698, 10);
            this.ambiance_Separator2.TabIndex = 19;
            this.ambiance_Separator2.Text = "ambiance_Separator2";
            // 
            // ambiance_Separator1
            // 
            this.ambiance_Separator1.Location = new System.Drawing.Point(16, 417);
            this.ambiance_Separator1.Name = "ambiance_Separator1";
            this.ambiance_Separator1.Size = new System.Drawing.Size(698, 10);
            this.ambiance_Separator1.TabIndex = 16;
            this.ambiance_Separator1.Text = "ambiance_Separator1";
            // 
            // Session
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 702);
            this.Controls.Add(this.btn_definition);
            this.Controls.Add(this.metroProgressBar1);
            this.Controls.Add(this.lbl_IncorrectGuesses);
            this.Controls.Add(this.metroLabel6);
            this.Controls.Add(this.lbl_PlaceHolderNextPhase);
            this.Controls.Add(this.metroLabel4);
            this.Controls.Add(this.lbl_AttemptsLeft);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.pnl_KeyPad);
            this.Controls.Add(this.pnl_Win);
            this.Controls.Add(this.pnl_Loss);
            this.Controls.Add(this.lbl_PROMPT);
            this.Controls.Add(this.ambiance_Separator2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ambiance_Separator1);
            this.Controls.Add(this.pnl_NewSingle);
            this.Controls.Add(this.pnl_NewMulti);
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Session";
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.None;
            this.Style = MetroFramework.MetroColorStyle.Default;
            this.Text = "Game Session";
            this.TextAlign = MetroFramework.Forms.MetroFormTextAlign.Center;
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Session_FormClosing);
            this.Load += new System.EventHandler(this.Session_Load);
            this.pnl_Win.ResumeLayout(false);
            this.pnl_Loss.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.pnl_NewMulti.ResumeLayout(false);
            this.pnl_NewMulti.PerformLayout();
            this.pnl_MultiOptions.ResumeLayout(false);
            this.pnl_MultiOptions.PerformLayout();
            this.pnl_NewSingle.ResumeLayout(false);
            this.pnl_NewSingle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.styleManager)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel letterbase;
        private Panel pnl_Win;
        private Label win_Attempts;
        private Label win_Main;
        private Panel pnl_Loss;
        private Label loss_LossMessage;
        private Label loss_LookingFor;
        private Label loss_CorrectWord;
        private AmbianceLabel ambiance_Label5;
        private AmbianceLabel lbl_PROMPT;
        private AmbianceSeparator ambiance_Separator2;
        private Panel pnl_KeyPad;
        private PictureBox pictureBox1;
        private Panel panel1;
        private Button btn_showInput;
        private Panel pnl_NewMulti;
        private AmbianceLabel ambiance_Label1;
        private AmbianceLabel lbl_placeHolderNewWord;
        private AmbianceLabel ambiance_Label2;
        private AmbianceLabel ambiance_Label4;
        private AmbianceSeparator ambiance_Separator1;
        private Panel pnl_NewSingle;
        private AmbianceLabel ambiance_Label3;
        private MetroLabel metroLabel1;
        private MetroLabel lbl_AttemptsLeft;
        private MetroLabel lbl_PlaceHolderNextPhase;
        private MetroLabel metroLabel4;
        private MetroLabel lbl_IncorrectGuesses;
        private MetroLabel metroLabel6;
        private MetroProgressBar metroProgressBar1;
        private MetroTextBox txt_newCustomWord;
        private MetroButton btn_StartNewSession;
        private MetroButton btn_changeMode1;
        private MetroButton btn_changeMode2;
        private MetroButton btn_go;
        private Panel pnl_MultiOptions;
        private MetroCheckBox chk_RevealWord;
        private MetroCheckBox chk_hideInput;
        private MetroStyleManager styleManager;
        private Timer tmr_Theme;
        private MetroButton btn_definition;

    }
}