using System;
using System.Windows.Forms;
using MetroHangman.Properties;

namespace MetroHangman
{
    public static class SessionHelper
    {
        public enum Difficulty
        {
            Hard,
            Common
        }
        //Creates an enumeration (list of constants) that is used for identification of the difficulty of singleplayer.
        /// <summary>
        /// Handles starting a new multiplayer session
        /// </summary>
        /// <param name="word">Word to use for new session</param>
        /// <param name="parameters">Instance of class MPParameters</param>
        public static void NewMultiplayer(string word, MpParameters parameters)
        {
            //Starts a new session using parameters and enum values
            new Session(word, Session.GameType.Multi,Difficulty.Common, parameters).Show();
        }
        /// <summary>
        /// Handles starting a new singleplayer session
        /// </summary>
        /// <param name="type">Level of difficulty</param>
        public static void NewSinglePlayer(Difficulty type)
        {
            
            //Starts a new session using difficulty level
            new Session(ChooseWord(type), Session.GameType.Single, type) { StartPosition = FormStartPosition.CenterScreen }.Show();
        }
        /// <summary>
        /// Chooses a random word
        /// </summary>
        /// <param name="type">Level of difficulty</param>
        private static string ChooseWord(Difficulty type)
        {
            //Checks if the difficulty level is normal or hard
            if (type == Difficulty.Common)
            {
                //Creates an array of common words from a file
                string[] commonWords = Resources.Common_Words.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
                //Grabs a word using a randomly generated index of the array
                return commonWords[new Random().Next(commonWords.Length - 1)].Trim().Replace("-", "");
            }
            string[] hardWords = Resources.Harder_Words.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
            return hardWords[new Random().Next(hardWords.Length - 1)].Trim().Replace("-", "");
        }
    }
}
