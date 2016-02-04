namespace MetroHangman
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
            return common ? Properties.Settings.Default.commonwins.ToString() : Properties.Settings.Default.hardwins.ToString();
        }

        public static string GetLosses(bool common)
        {
            return common ? Properties.Settings.Default.commonlosses.ToString() : Properties.Settings.Default.hardlosses.ToString();
        }

        public static void ResetStatistics()
        {
            Properties.Settings.Default.commonwins = 0;
            Properties.Settings.Default.commonlosses = 0;
            Properties.Settings.Default.hardlosses = 0;
            Properties.Settings.Default.hardwins = 0;
            Save();
        }
        public static void AddWin(SessionHelper.Difficulty diff)
        {
            if (diff == SessionHelper.Difficulty.Common)
                Properties.Settings.Default.commonwins += 1;
            else
                Properties.Settings.Default.hardwins += 1;
            Save();
        }
        public static void AddLoss(SessionHelper.Difficulty diff)
        {
            if (diff == SessionHelper.Difficulty.Common)
                Properties.Settings.Default.commonlosses += 1;
            else
                Properties.Settings.Default.hardlosses += 1;
            Save();
        }
        private static void Save()
        {
            Properties.Settings.Default.Save();
        }
    }
}
