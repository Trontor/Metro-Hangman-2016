using MetroFramework;
using MetroFramework.Forms;
using MetroHangman;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hangman
{
    public partial class Start : MetroForm
    {
        enum ModeType
        {
            Single,
            Multi
        }
        //Creates an enumeration list (list of constants) that is used for identification of the gamemode type.

        /// <summary>
        /// Calls an external function to hide the caret when the username control is selected.
        /// </summary>
        /// <param name="hWnd">Control ID to hide caret on</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        static extern bool HideCaret(IntPtr hWnd);

        /// <summary>
        /// Main function, called when form is first initialised.
        /// </summary>
        public Start()
        {
            //Loads controls onto form
            InitializeComponent();
            //Makes all letters capitals to ensure consistency.

            //Initalises 'Profiler' class, which handles all user profiles.
            Profiler.Initialise();

            chk_hideInput.Checked = true;
            this.FocusMe();
        }
        /// <summary>
        /// Shows respective controls for each mode when mode type is changed. 
        /// </summary>
        /// <param name="type">Uses the enumeration 'ModeType' to identify the game type (single or multiplayer).</param>
        private void ChooseMode(ModeType type)
        {
            //Checks if the mode is multiplayer
            if (type == ModeType.Multi)
            {
                //Shows label prompt notifying user that Multiplayer has been chosen
                txtdynamic_Option2.Text = "2. Enter word for next player";
                //Hides and shows controls pertaining to multiplayer
                pnl_wordPacks.Visible = false;
                pnl_MultiDisplayer.Visible = true;
                pnl_MultiOptions.Visible = true;
                //Focuses the textbox so the user does not have to click it
                txt_CustomWord.Focus();
            }
            // Handles the only other option - that the mode is single player
            else
            {
                //Shows label prompt notifying user that Singleplayer has been chosen
                txtdynamic_Option2.Text = "2. Choose Word Pack";
                //Hides and shows controls pertaining to singleplayer
                pnl_wordPacks.Visible = true;
                pnl_MultiDisplayer.Visible = false;
                pnl_MultiOptions.Visible = false;
            }
        }

        /// <summary>
        /// Handles when the 'Single Player' button is clicked.
        /// </summary>
        private void btn_SinglePlayer_Click(object sender, EventArgs e)
        {
            //Calls method to handle button click, passes parameter as Single, meaning Singleplayer.
            ChooseMode(ModeType.Single);
        }

        /// <summary>
        /// Handles when the 'Multiplayer' button is clicked.
        /// </summary>
        private void btn_MultiPlayer_Click(object sender, EventArgs e)
        {
            //Calls method to handle button click, passes parameter as Multi, meaning Multiplayer.
            ChooseMode(ModeType.Multi);
        }
        /// <summary>
        /// Handles when the begin button is pressed
        /// </summary>
        private void btn_Start_Click(object sender, EventArgs e)
        {
            //Calls the NewSession function
            NewSession();
        }
        /// <summary>
        /// Hooks onto key presses anywhere on the form (regardless of what control has focus)
        /// </summary>
        /// <param name="keyData">What key is pressed</param>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //Checks if key pressed is the 'Enter Key'
            if (keyData == Keys.Enter)
            {
                NewSession();
            }
            else if (keyData == Keys.Tab)
            {
                if (pnl_wordPacks.Visible)
                {
                    ChooseMode(ModeType.Multi);
                }
                else ChooseMode(ModeType.Single);
            }

            // Call the base class
            return base.ProcessCmdKey(ref msg, keyData);
        }
        /// <summary>
        /// Performs logical checks before allowing the user to proceed to the game.
        /// </summary>
        private void NewSession()
        {
            //Checks if multiplayer is selected, and if so - whether or not a word was typed for player 2
            if (pnl_MultiDisplayer.Visible && txt_CustomWord.Text.Length == 0)
            {
                MessageBox.Show("Before you can begin a multiplayer session you must type a word for Player 2 to guess.", "Invalid Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                //Checks if word pack options are visible, meaning the user wants to play a single player game
                if (pnl_wordPacks.Visible)
                    //Starts new Singleplayer game
                    SessionHelper.NewSinglePlayer(rad_Common.Checked ? SessionHelper.Difficulty.Common : SessionHelper.Difficulty.Hard);
                //If no word packs are visible, it means that the user wants to start a multiplayer game
                else
                    //Starts new Multiplayer game
                    SessionHelper.NewMultiplayer(txt_CustomWord.Text, new MPParameters(chk_hideInput.Checked, chk_RevealWord.Checked));
        }
        /// <summary>
        ///  Handles when the 'hide input' button is checked or unchecked.
        /// </summary>
        private void chk_hideInput_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_hideInput.Checked)
            {
                //Changes width of textbox to account for the show input button
                txt_CustomWord.Width = 294;
                //Shows 'showinput' button
                btn_showInput.Visible = true;
                //Uses dots instead of text
                txt_CustomWord.UseSystemPasswordChar = true;
            }
            else
            {
                //Changes width of textbox to account for the lack of the show input button
                txt_CustomWord.Width = 327;
                //Hides 'showinput' button 
                btn_showInput.Visible = false;
                //Uses text instead of dots
                txt_CustomWord.UseSystemPasswordChar = false;
            }
        }
        /// <summary>
        /// Handles when the user inputs text into the 'Custom Word' textbox for multiplayer.
        /// </summary>
        private void txt_CustomWord_TextChanged(object sender, EventArgs e)
        {
            if (txt_CustomWord.Text.Length != string.Concat(txt_CustomWord.Text.Where(char.IsLetter)).Length)
            {
                MessageBox.Show("You cannot enter characters that are not letters.", "Invalid Input");

            }

            if (txt_CustomWord.SelectedText != txt_CustomWord.Text) txt_CustomWord.Select(txt_CustomWord.Text.Length, 1);
            //Concatenates (removes) characters that are not letters.
            txt_CustomWord.Text =
                string.Concat(
                txt_CustomWord.Text.Where(
                char.IsLetter));
            txt_CustomWord.Text = txt_CustomWord.Text.ToUpper();
            // 
            //  
            //Changes the position of the caret to the end (bug fix)
            //txt_CustomWord.SelectedText = txt_CustomWord.Text.Length;
        }

        /// <summary>
        ///  Handles 'showinput' button being held down.
        /// </summary>
        private void btn_showInput_MouseDown(object sender, MouseEventArgs e)
        {
            //Uses text instead of dots.
            txt_CustomWord.UseSystemPasswordChar = false;
        }

        /// <summary>
        ///  Handles 'showinput' button being held released.
        /// </summary>
        private void btn_showInput_MouseUp(object sender, MouseEventArgs e)
        {
            //Uses dots instead of text.
            txt_CustomWord.UseSystemPasswordChar = true;
        }

        /// <summary>
        ///  Handles the clicking of the '?' button.
        /// </summary>
        private void btn_profile_Click(object sender, EventArgs e)
        {
            //Explains to the user the what a 'profile' is.
            MessageBox.Show("A profile is just an object that stores information about your Hangman games (such as wins and losses).");
        }
        /// <summary>
        /// Handles the clicking of the 'Manage' button
        /// </summary>
        private void btn_manageProfile_Click(object sender, EventArgs e)
        {
            //Opens profile management form.
            new profiles().ShowDialog();
        }

        private void Start_Load(object sender, EventArgs e)
        {
            styleManager.Style = MetroHangman.Properties.Settings.Default.Theme;

            ProcessTick();
        }



        //private void tmr_Disco_Tick(object sender, EventArgs e)
        //{
        //    if (MetroHangman.Properties.Settings.Default.DiscoOn == false)
        //        tmr_Disco.Stop();
        //    RandomStyle();
        //}


        public void RandomStyle()
        {
            var m = new Random();
            int next = m.Next(3, 13);
            SetColour((MetroColorStyle)next);
        }
        private void SetColour(MetroColorStyle style)
        {
            MetroHangman.Properties.Settings.Default.Theme = style;
            MetroHangman.Properties.Settings.Default.Save();
            Invalidate();
        }
        private void tmr_Theme_Tick(object sender, EventArgs e)
        {
            ProcessTick();
        }
        private void ProcessTick()
        {
            if (MetroHangman.Properties.Settings.Default.DiscoOn)
            {
                RandomStyle();
            }
            styleManager.Style = MetroHangman.Properties.Settings.Default.Theme;
            this.Style = styleManager.Style;
            Type thisType = typeof(MetroColors);
            MethodInfo theMethod = thisType.GetMethod("get_" + Style.ToString());
            MetroColors c = new MetroColors();
            btn_Settings.BackColor = (Color)theMethod.Invoke(c, null);
            styleManager.Update();
        }

        private void btn_Settings_Click(object sender, EventArgs e)
        {
            new Settings().Show(this);
        }

    }
}
