using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Controls;
using MetroFramework.Forms;

namespace MetroHangman
{
    public sealed partial class Session : MetroForm
    {
        private bool _systemClose = true;
        public enum GameType
        {
            Single,
            Multi
        }
        //Creates an enumeration list (list of constants) that is used for identification of the gamemode type.

        /// <summary>
        /// Initialising variables
        /// </summary>
        private readonly GameType _gameType;
        private readonly MpParameters _multiplayerParams;
        private bool _gameRunning = true;
        private readonly List<char> _wrongLetters = new List<char>();
        private readonly List<char> _correctLetters = new List<char>();
        private readonly string _word;
        private bool _baseDrawn;
        private int _phase = 1;
        private readonly Dictionary<int, string> _phaseNames = new Dictionary<int, string>();
        private readonly SessionHelper.Difficulty _levelDifficulty;

        /// <summary>
        /// Function called when form is loaded
        /// </summary>
        /// <param name="word"></param>
        /// <param name="type"></param>
        /// <param name="difficulty"></param>
        /// <param name="parameters"></param>
        public Session(string word, GameType type, SessionHelper.Difficulty difficulty = SessionHelper.Difficulty.Common, MpParameters parameters = null)
        {
            InitializeComponent();
            Height = 520
                ;
            //converts word to upper case just in case it isn't already
            _word = word.ToUpper();
            _gameType = type;

            //Initialising phase names for each letter incorrect
            _phaseNames.Add(1, "Base");
            _phaseNames.Add(2, "Post");
            _phaseNames.Add(3, "Support 1");
            _phaseNames.Add(4, "Support 2");
            _phaseNames.Add(5, "Overhead");
            _phaseNames.Add(6, "Noose");
            _phaseNames.Add(7, "Head");
            _phaseNames.Add(8, "Body");
            _phaseNames.Add(9, "Left Arm");
            _phaseNames.Add(10, "Right Arm");
            _phaseNames.Add(11, "Left Leg");
            _phaseNames.Add(12, "Right Leg (R.I.P)");
            _phaseNames.Add(13, "RIP IN PEPPERONIES");
            if (_gameType == GameType.Multi)
            {
                _multiplayerParams = parameters;
                if (_multiplayerParams != null)
                {
                    chk_RevealWord.Checked = _multiplayerParams.RevealWord;
                    chk_hideInput.Checked = _multiplayerParams.HideWord;
                }
            }
            _levelDifficulty = difficulty;
            //Sets text for top of form
            Text = "Game Session - " + _levelDifficulty + " Words";

        }

        /// <summary>
        /// Dynamically generates the keypad of all possible letters
        /// </summary>
        private void LoadKeyPad()
        {
            //Ensures all elements that can interfere with the keypad are hidden for the time being
            pnl_Loss.Visible = false;
            pnl_Win.Visible = false;
            pnl_NewMulti.Visible = false;
            //Creates a character array of all the letters for the keypad.
            char[] array = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray(); //Converts string to a character array
            int num = 10; //Padding for each letter
            int num2 = 10;// Y value for letters on the second row
            char[] array2 = array;

            //Loops through all the letters 
            foreach (char c in array2)
            {
                //Creates a new button
                MetroButton button = new MetroButton
                {
                    BackColor = Color.White,
                    UseCustomBackColor = true,
                    FontSize = MetroButtonSize.Small,
                    FontWeight = MetroButtonWeight.Regular,
                    Width = pnl_KeyPad.Width/13 - num,
                    Height = 30,
                    Text = c.ToString()
                };
                //Using ratios, calculates the width of the letter for the keypad, accounts for padding amount
                //Assigns the letter to the button
                //Creates an event handler for when the letter is pressed
                button.Click += button_Click;
                //Creates a variable to handle the x value position for the button
                int x;
                if (pnl_KeyPad.Controls.Count > 12)
                {
                    //Creates x value for button in second row
                    x = (pnl_KeyPad.Controls.Count - 13) * (button.Width + num);
                }
                else
                {
                    x = pnl_KeyPad.Controls.Count * (button.Width + num);
                }
                //Places button onto keypad.
                button.Location = new Point(x, pnl_KeyPad.Controls.Count >= 13 ? button.Height + num2 : 0);
                pnl_KeyPad.Controls.Add(button);
            }
            pnl_KeyPad.Width -= num;
        }

        /// <summary>
        /// Dynamically updates the letters revealed and yet to be revealed
        /// </summary>
        private void UpdateLetters()
        {
            int num = 0;
            int height = 60;
            int num2 = 50;
            Font font;

            // Optimises font size to use the maximum amount of space as possible.
            if (_word.Length < 13)
            {
                font = new Font("Segoe UI", 35f, FontStyle.Underline);
            }
            else
            {
                num2 = (int)(-1.64 * _word.Length + 70.0);
                height = 60;
                font = new Font("Segoe UI", (float)(0.89 * num2 - 5.05), FontStyle.Underline);
            }
            //Checks if the base is not drawn. The base is the lines without any letters revealed.
            if (!_baseDrawn)
            {
                //Loops through each letter in the word and creates a placeholder for each letter.
                for (int i = 0; i < _word.Length; i++)
                {
                    Label label = new Label
                    {
                        Name = _word[i].ToString().ToUpper(),
                        Height = height,
                        Width = num2,
                        Font = font,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Text = " ",
                        ForeColor = Color.Black
                    };
                    letterbase.Controls.Add(label);
                    label.Location = new Point(i * label.Width, 0);
                    num += label.Width;
                }
                letterbase.Width = num;
                letterbase.Height = height;
                letterbase.Left = (letterbase.Parent.Width - letterbase.Width) / 2 + 10;
                letterbase.Top = (letterbase.Parent.Height - letterbase.Height) / 2;
                _baseDrawn = true;
            }
            //If the base is already drawn
            else
            {
                //Loops through each letter placeholder
                foreach (Label label in letterbase.Controls)
                {
                    //Checks if the letter for the placeholder has been guessed yet
                    if (_correctLetters.Contains(label.Name.ToArray()[0]))
                    {
                        //if the letter has been guessed, the letter is shown to the user
                        label.Font = new Font(font, FontStyle.Regular);
                        label.Text = label.Name.ToArray()[0].ToString();
                    }
                }
            }
            //Checks if all the letters have been revealed
            if (_correctLetters.Count == RemoveDuplicates(_word).Length)
            {
                ShowWin();
            }
        }

        /// <summary>
        /// Handles the letter on the keypad being pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Click(object sender, EventArgs e)
        {
            //Calls the function to handle the pressing of a letter
            HandleButtonPress(sender);
            metroProgressBar1.Focus();
        }

        /// <summary>
        /// Handles the guessing of a letter either through the keypad
        /// </summary>
        /// <param name="sender"></param>
        private void HandleButtonPress(object sender)
        {
            //Checks if the game is still running
            if (_gameRunning)
            {
                //Converts the calling button of the function into a local button
                MetroButton ambianceButton = sender as MetroButton;
                //Checks if the text 'Click or Type your first letter guess.' label is visible.
                if (lbl_PROMPT.Visible)
                {
                    //Hides the 'Click or Type your first letter guess.' label.
                    lbl_PROMPT.Visible = false;
                }
                //Checks if the button of the letter pressed is still visible (meaning it has not been guessed yet).
                var metroButton = sender as MetroButton;
                if (metroButton != null && metroButton.Visible)
                {
                    //Checks if the word contains the letter the user pressed.
                    if (_word.Contains(ambianceButton.Text))
                    {
                        //Calls the RightChoice function, passing the correct letter pressed.
                        RightChoice(ambianceButton.Text[0]);
                    }
                    else
                    {
                        //Calls the WrongChoice function because the letter guessed was not in the word.
                        WrongChoice(ambianceButton.Text[0]);
                    }
                    //Makes the button invisible so the user cannot guess the letter again
                    ((MetroButton) sender).Visible = false;
                }
                //Calculates the statistics shown on the form to the user
                CalculateStatistics();
            }
        }
        /// <summary>
        /// Strips the word of duplicate letters in order to get the amount of guesses the user needs to win.
        /// </summary>
        /// <param name="word">The word the duplicate letters need to be removed</param>
        /// <returns></returns>
        private string RemoveDuplicates(string word)
        {
            //Uses LinQ  to remove duplicates
            return new string(word.ToCharArray().Distinct().ToArray());
        }
        /// <summary>
        /// Handles when the user makes a wrong guess.
        /// </summary>
        /// <param name="letter">The incorrect letter</param>
        private void WrongChoice(char letter)
        {
            //Adds the letter to a list of all incorrect guesses the user has made.
            _wrongLetters.Add(letter);
            //Draws next phase of the hangman animation.
            pictureBox1.Image = DrawHangman(_phase);
            //Increments the phase variable so next time the proceeding phase is drawn.
            _phase++;
            //Checks if all phases have been complete.
            if (_phase == 13)
            {
                //If so, the loss procedure is called.
                ShowLoss();
            }
        }

        /// <summary>
        /// Handles when the user guesses a letter correctly.
        /// </summary>
        /// <param name="letter"></param>
        private void RightChoice(char letter)
        {
            //Adds the letter to the list of correctly guessed letters so the letter is revealed to the user.
            _correctLetters.Add(letter);
            //Adjusts the progress bar to account for the newly guessed letter.
            metroProgressBar1.Value = (int)(_correctLetters.Count / (float)RemoveDuplicates(_word).Length * 100f);
            //Redraws the letter base, therefore revealing the letter to the user.
            UpdateLetters();
        }

        /// <summary>
        /// Calculates the statistics shown at the top of the form.
        /// </summary>
        private void CalculateStatistics()
        {
            //Checks if the game is still running.
            if (_gameRunning)
            {
                lbl_IncorrectGuesses.Text = _wrongLetters.Count.ToString();
                lbl_AttemptsLeft.Text = (12 - _wrongLetters.Count).ToString();
            }
            if (_phase == 13)
            {
                //Finalises the text showing the phases
                lbl_PlaceHolderNextPhase.Text = "Man is dead.";
                lbl_PlaceHolderNextPhase.Location = new Point(lbl_PlaceHolderNextPhase.Location.X + 4, lbl_PlaceHolderNextPhase.Location.Y);
                lbl_PlaceHolderNextPhase.Text = "";
            }
            //Shows description of next phase
            else
                lbl_PlaceHolderNextPhase.Text = _phaseNames[_phase];

        }

        /// <summary>
        /// Recursive function that draws the hangman and returns as an Image object.
        /// </summary>
        /// <param name="phase">The phase to draw up till.</param>
        /// <returns></returns>
        private Image DrawHangman(int phase)
        {
            //Variable to store hangman
            Image image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            //Ratio of head of man to width of picturebox
            int num = 20;
            if (phase > 1)
                image = new Bitmap(DrawHangman(phase - 1)); //Draws all other phases before the current one, then continues.
            using (Graphics graphics = Graphics.FromImage(image))
            {
                //Smooths the Hangman drawn
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (Pen pen = new Pen(Properties.Settings.Default.DiscoOn?
                     (Color)typeof(MetroColors).GetMethod("get_" + Style).Invoke(new MetroColors(), null) :
                Color.Black, 2f))
                {
                    switch (phase)
                    {
                        //The generated image is dynamic, which means that if the form is resized, the hangman is resized proportionally.
                        case 1:
                            //Creates the base for the rest of the phases.
                            graphics.DrawLine(pen, new Point(0, pictureBox1.Height - 2), new Point(pictureBox1.Width, pictureBox1.Height - 2));
                            break;
                        case 2:
                            graphics.DrawLine(pen, new Point(pictureBox1.Width / 2, pictureBox1.Height), new Point(pictureBox1.Width / 2, 0));
                            break;
                        case 3:
                            graphics.DrawLine(pen, new Point(pictureBox1.Width / 2, 0), new Point((pictureBox1.Width / 2 + 19 * pictureBox1.Width / 32) / 2, pictureBox1.Height / 8));
                            break;
                        case 4:
                            graphics.DrawLine(pen, new Point(pictureBox1.Width / 2, pictureBox1.Height / 4), new Point(19 * pictureBox1.Width / 32, 0));
                            break;
                        case 5:
                            graphics.DrawLine(pen, new Point(pictureBox1.Width / 2, 0), new Point(pictureBox1.Width / 2 + pictureBox1.Width / 4, 0));
                            break;
                        case 6:
                            graphics.DrawLine(pen, new Point(3 * pictureBox1.Width / 4, 0), new Point(3 * pictureBox1.Width / 4, pictureBox1.Height / 4));
                            break;
                        case 7:
                            graphics.DrawEllipse(pen, 3 * pictureBox1.Width / 4 - pictureBox1.Width / num / 2, pictureBox1.Height / 4, pictureBox1.Width / num, pictureBox1.Width / num);
                            break;
                        case 8:
                            graphics.DrawLine(pen, new Point(3 * pictureBox1.Width / 4, pictureBox1.Height / 4 + pictureBox1.Width / num), new Point(3 * pictureBox1.Width / 4, pictureBox1.Height / 4 + pictureBox1.Width / num + (pictureBox1.Height - pictureBox1.Width / num - pictureBox1.Height / 2)));
                            break;
                        case 9:
                            {
                                Point pt = new Point(3 * pictureBox1.Width / 4, pictureBox1.Height / 4 + pictureBox1.Width / num + pictureBox1.Height / 10);
                                graphics.DrawLine(pen, pt, new Point(18 * pictureBox1.Width / 25, pt.Y));
                                break;
                            }
                        case 10:
                            {
                                Point pt2 = new Point(3 * pictureBox1.Width / 4, pictureBox1.Height / 4 + pictureBox1.Width / num + pictureBox1.Height / 10);
                                graphics.DrawLine(pen, pt2, new Point(39 * pictureBox1.Width / 50, pt2.Y));
                                break;
                            }
                        case 11:
                            graphics.DrawLine(pen, new Point(3 * pictureBox1.Width / 4, pictureBox1.Height / 4 + pictureBox1.Width / num + (pictureBox1.Height - pictureBox1.Width / num - pictureBox1.Height / 2)), new Point(18 * pictureBox1.Width / 25, pictureBox1.Height - pictureBox1.Height / 8));
                            break;
                        case 12:
                            graphics.DrawLine(pen, new Point(3 * pictureBox1.Width / 4, pictureBox1.Height / 4 + pictureBox1.Width / num + (pictureBox1.Height - pictureBox1.Width / num - pictureBox1.Height / 2)), new Point(39 * pictureBox1.Width / 50, pictureBox1.Height - pictureBox1.Height / 8));
                            break;
                    }
                }
            }
            return image;
        }

        /// <summary>
        /// Loads the Keypad, draws the letter base and centers the form to the screen.
        /// </summary>
        private void Session_Load(object sender, EventArgs e)
        {
            LoadKeyPad();
            UpdateLetters();
            CenterForm();
            Style = Properties.Settings.Default.Theme;
            styleManager.Style = Style;
        }
        /// <summary>
        /// Centers form to screen
        /// </summary>
        private void CenterForm()
        {
            Location = new Point(Screen.PrimaryScreen.WorkingArea.Size.Width / 2 - Width / 2, Screen.PrimaryScreen.WorkingArea.Size.Height / 2 - Height / 2);
        }

        /// <summary>
        /// Shows the footer, which appears when the game is over (win or loss).
        /// </summary>
        private void ShowFooter()
        {
            if (_gameType == GameType.Single)
                btn_definition.Visible = true;
            pnl_KeyPad.Visible = false;
            pnl_NewMulti.Visible = _gameType == GameType.Multi;
            pnl_NewSingle.Visible = !pnl_NewMulti.Visible;
            if (pnl_NewMulti.Visible)
                txt_newCustomWord.Focus();
            Height = pnl_NewMulti.Visible ? 704 : 597;
            ResumeLayout();
            CenterForm();
        }

        /// <summary>
        /// Handles when the user loses.
        /// </summary>
        private void ShowLoss()
        {
            //Word list of comical sub-text to be randomly chosen.
            List<string> wordBase = new List<string>
			{
				"Your valiant efforts have not gone unnoticed in your efforts in finding the word",
				"Better luck next time. I suggest you go to a dictionary and find the word",
				"I bid you better luck next time. Maybe the word will be easier than"
			};
            //Word list of comical ending text to be randomly chosen.
            List<string> wordBase2 = new List<string>
			{
				"Error 404: Word not found.",
				"Sorry, you did not figure out the word!",
				"Not everyone can win!"
			};
            // Shows the panel describing the loss
            pnl_Loss.Visible = true;
            //Shows the correct word.
            loss_CorrectWord.Text = _word;
            //Handles a multiplayer loss
            if (_gameType == GameType.Multi)
            {
                //Checks if Player 1 wishes to reveal the word to Player 2.
                if (!_multiplayerParams.RevealWord)
                {
                    loss_CorrectWord.Text = "Player 1 chose to not reveal the word to you. Sorry!";
                    loss_CorrectWord.Font = new Font("Segoe UI", 15, FontStyle.Bold);
                    loss_CorrectWord.ForeColor = Color.Black;
                }
            }
            else
            {
                //Adds a loss to the user's profile.
                Profiler.AddLoss(_levelDifficulty);
            }
            //Shows randomised ending.
            loss_LookingFor.Text = RandomWord(wordBase);
            //Shows randomised sub-text.
            loss_LossMessage.Text = RandomWord(wordBase2);
            //Shows the footer to the user, subsequently showing the loss panel.
            ShowFooter();
            //Sets variable 'GameRunning' to false to notify other functions that the game is not in progress.
            _gameRunning = false;
        }

        /// <summary>
        /// Handles the procedure when the user wins.
        /// </summary>
        private void ShowWin()
        {
            //Checks if the win is in the singleplayer mode
            if (_gameType == GameType.Single)
            {
                //Adds win to the profile only if singleplayer (combats profile win farming).
                Profiler.AddWin(_levelDifficulty);
            }
            //Shows the 'Win' panel.
            pnl_Win.Visible = true;
            //Shows the word that was guessed correctly.
            loss_CorrectWord.Text = _word;
            //List of text to show to the user congratulating them.
            win_Main.Text = RandomWord(new List<string>
			{
				"Great Job! You won!",
				"Congratulations! You won!",
				"I'd give you a ribbon if I could. Good Job on Winning!"
			});
            //Calculates the degree of excitedness to present to the user based off how many guesses they made.
            var attemptEvaluation = AttemptEval(_wrongLetters.Count);
            //Sets the colour for the text in respect to their closeness to losing.
            win_Attempts.ForeColor = attemptEvaluation.Item2;
            //Tells the user how many incorrect guesses they made.
            win_Attempts.Text = $"You {attemptEvaluation.Item1} {_wrongLetters.Count} incorrect guesses.";
            //Shows the footer to the user, subsequently showing the loss panel.
            ShowFooter();
            //Sets variable 'GameRunning' to false to notify other functions that the game is not in progress.
            _gameRunning = false;
        }

        /// <summary>
        /// Evalutes the succesfulness of the user based on the amount of attempts they made.
        /// </summary>
        /// <param name="attempts">The amount of attempts the user made.</param>
        /// <returns>A Tuple object, which is just an object that holds two other objects.</returns>
        private Tuple<string, Color> AttemptEval(int attempts)
        {
            string result;
            Color color;
            if (attempts > 10)
            {
                result = "barely scraped through with";
                color = Color.OrangeRed;
            }
            else if (attempts > 5)
            {
                result = "did well. You had";
                color = Color.Orange;
            }
            else
            {
                result = RandomWord(new List<string>
				{
					"smashed through this session with a mere",
					"did very well as you only had",
					"are pretty smart as you only had",
					"completed faster than normal with only",
					"broke some record somewhere as you only had"
				});

                color = Color.DarkGreen;
            }
            return new Tuple<string, Color>(result, color);
        }

        /// <summary>
        /// Chooses a random string from a list of string
        /// </summary>
        /// <param name="wordBase"></param>
        /// <returns></returns>
        private string RandomWord(List<string> wordBase)
        {
            //Returns the word at a randomly generated index.
            return wordBase[new Random().Next(wordBase.Count)];
        }

        /// <summary>
        /// Allows the user to navigate back to the main menu.
        /// </summary>
        private void ChangeGameOptions()
        {
            // If the game is not running, it directly takes the user back to the main menu.
            if (!_gameRunning)
                CloseForm();
            else
            {
                //If the game is running, it will ask the user to confirm that they want to leave the current game and discard their progress.
                DialogResult diag = MessageBox.Show("You have pressed the escape button while you are in a session.\nWould you like to exit this session (your progress will not be saved)?", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (diag == DialogResult.Yes)
                    CloseForm();
            }
        }
        /// <summary>
        /// Handles a key press anywhere on the form (regardless of what control is in focus).
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                ChangeGameOptions();
            }
            //Checks if the key pressed is a letter.
            if ((keyData < Keys.F1 || keyData > Keys.F12) && char.IsLetter((char)keyData))
            {
                foreach (MetroButton ambianceButton in pnl_KeyPad.Controls)
                {
                    if (ambianceButton.Text == keyData.ToString())
                    {
                        HandleButtonPress(ambianceButton);
                    }
                }
            }
            if (keyData == Keys.Enter)
                if (pnl_NewMulti.Visible)
                    NewMultiPlayer();
                else if (pnl_NewSingle.Visible)
                    NewSinglePlayer();
            return base.ProcessCmdKey(ref msg, keyData);
        }
        /// <summary>
        /// Initialises a new Single Player session.
        /// </summary>
        private void NewSinglePlayer()
        {
            SessionHelper.NewSinglePlayer(_levelDifficulty);
            CloseForm();
        }

        /// <summary>
        /// Initialises a new Multi Player session.
        /// </summary>
        private void NewMultiPlayer()
        {
            if (txt_newCustomWord.Text.Length == 0)
                MessageBox.Show("Before you can begin a new multiplayer session you must type a word for Player 2 to guess.", "Invalid Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
            SessionHelper.NewMultiplayer(txt_newCustomWord.Text, new MpParameters(chk_RevealWord.Checked, chk_hideInput.Checked));
            CloseForm();
        }

        private void btn_changeMode_Click(object sender, EventArgs e)
        {
            //Closes the form if the user wishes to change the gamemode.
            CloseForm();
        }

        private void btn_StartNewSession_Click(object sender, EventArgs e)
        {
            //Starts a new singleplayer session using the same level of difficulty.
            SessionHelper.NewSinglePlayer(_levelDifficulty);
            //Closes form.
            CloseForm();
        }

        private void CloseForm()
        {
            _systemClose = false;
            Close();
        }
        private void btn_ChangeMode1_Click(object sender, EventArgs e)
        {
            //Closes form.
            CloseForm();
        }

        private void btn_go_Click(object sender, EventArgs e)
        {
            //Starts a new multiplayer session.
            NewMultiPlayer();
        }

        /// <summary>
        /// Handles when the user inputs text into the 'Custom Word' textbox for multiplayer.
        /// </summary>
        private void txt_newCustomWord_TextChanged(object sender, EventArgs e)
        {
            if (txt_newCustomWord.SelectedText != txt_newCustomWord.Text) txt_newCustomWord.Select(txt_newCustomWord.Text.Length, 1);
            //Concatenates (removes) characters that are not letters.
            txt_newCustomWord.Text =
                string.Concat(
                txt_newCustomWord.Text.Where(
                char.IsLetter));
            txt_newCustomWord.Text = txt_newCustomWord.Text.ToUpper();
        }
        /// <summary>
        ///  Handles 'showinput' button being held down.
        /// </summary>
        private void btn_showInput_MouseDown(object sender, MouseEventArgs e)
        {
            txt_newCustomWord.UseSystemPasswordChar = false;
        }

        /// <summary>
        ///  Handles 'showinput' button being held released.
        /// </summary>
        private void btn_showInput_MouseUp(object sender, MouseEventArgs e)
        {
            txt_newCustomWord.UseSystemPasswordChar = chk_RevealWord.Checked;
        }

        /// <summary>
        ///  Handles when the 'hide input' button is checked or unchecked.
        /// </summary>
        private void chk_hideInput_CheckedChanged(object sender, EventArgs e)
        {
            txt_newCustomWord.UseSystemPasswordChar = !txt_newCustomWord.UseSystemPasswordChar;
            if (txt_newCustomWord.UseSystemPasswordChar)
            {
                txt_newCustomWord.Width -= btn_showInput.Width + 3;
                btn_showInput.Visible = true;
            }
            else
            {
                btn_showInput.Visible = false;
                txt_newCustomWord.Width = 332;
            }
        }

        private void Session_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_systemClose)
                foreach (Control c in Application.OpenForms[0].Controls)
                {
                    if (c is MetroLabel)
                    {
                        (c as MetroLabel).Focus();
                        Application.OpenForms[0].BringToFront();
                    }
                }

        }


/*
        private void RandomStyle()
        {
            var m = new Random();
            int next = m.Next(3, 13);
            SetColour((MetroColorStyle)next);

        }
*/
/*
        private void SetColour(MetroColorStyle style)
        {
            styleManager.Style = style;
            Style = styleManager.Style;
            Properties.Settings.Default.Theme = Style;
            Properties.Settings.Default.Save();

            //            btn_color.BackColor = (Color)theMethod.Invoke(c, null);
            Invalidate();
        }
*/


        private void tmr_Disco_Tick(object sender, EventArgs e)
        {
            styleManager.Style = Properties.Settings.Default.Theme;
            Style = styleManager.Style;
            pictureBox1.Image = DrawHangman(_phase - 1);
        }

        private void pnl_NewMulti_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_definition_Click(object sender, EventArgs e)
        {
            new Definition(_word).ShowDialog();
        }
    }
}
