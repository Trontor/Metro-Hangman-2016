using MetroFramework;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hangman
{
    public partial class profiles : MetroForm
    {
        string LoggedInAs = Environment.UserName.ToLower();
        public profiles()
        {
            InitializeComponent();
            LoadStatistics();
        }
        private void LoadStatistics()
        {
            lbl_commonWins.Text = Profiler.GetWins(true);
            lbl_hardWins.Text = Profiler.GetWins(false);
            lbl_commonLosses.Text = Profiler.GetLosses(true);
            lbl_hardLosses.Text = Profiler.GetLosses(false);
        }

        private void btn_resetStatistics_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to reset the statistics for this profile?", "Clearance", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                Profiler.ResetStatistics();

            LoadStatistics();
        }

        private void lbl_commonWins_Click(object sender, EventArgs e)
        {

        }
        private void tmr_Theme_Tick(object sender, EventArgs e)
        {
            ProcessTick();
        }
        private void ProcessTick()
        {
            this.Style = MetroHangman.Properties.Settings.Default.Theme;
            sMgr.Owner = this;
            sMgr.Style = MetroHangman.Properties.Settings.Default.Theme;

            btn_resetStatistics.BackColor = (Color)typeof(MetroColors).GetMethod("get_" + Style.ToString()).Invoke(new MetroColors(), null);

        }
    }
}
