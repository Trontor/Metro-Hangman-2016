using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Hangman
{
    public static class UserStatistics
    {
        public static string CommonWins { get; set; }
        public static string CommonLosses { get; set; }
        public static string HardWins { get; set; }
        public static string HardLosses { get; set; }
    }
    public static class Profiler
    {
        public static void Initialise()
        {
        }


        public static string GetWins(bool common)
        {
            if (common)
            return MetroHangman.Properties.Settings.Default.commonwins.ToString();
            else
                return MetroHangman.Properties.Settings.Default.hardwins.ToString();
        }
        public static string GetLosses(bool common)
        {
            if (common)
                return MetroHangman.Properties.Settings.Default.commonlosses.ToString();
            else
                return MetroHangman.Properties.Settings.Default.hardlosses.ToString();
        }
        public static void ResetStatistics()
        {
            MetroHangman.Properties.Settings.Default.commonwins = 0;
            MetroHangman.Properties.Settings.Default.commonlosses = 0;
            MetroHangman.Properties.Settings.Default.hardlosses = 0;
            MetroHangman.Properties.Settings.Default.hardwins = 0;
            Save();
        }
        public static void AddWin(SessionHelper.Difficulty diff)
        {
            if (diff == SessionHelper.Difficulty.Common)
                MetroHangman.Properties.Settings.Default.commonwins += 1;
            else
                MetroHangman.Properties.Settings.Default.hardwins += 1;
            Save();
        }
        public static void AddLoss(SessionHelper.Difficulty diff)
        {
            if (diff == SessionHelper.Difficulty.Common)
                MetroHangman.Properties.Settings.Default.commonlosses += 1;
            else
                MetroHangman.Properties.Settings.Default.hardlosses += 1;
            Save();
        }
        private static void Save()
        {
            MetroHangman.Properties.Settings.Default.Save();
        }
    }
}
