using System;
using System.Drawing;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Forms;

namespace MetroHangman
{
    public partial class Profiles : MetroForm
    {
        public Profiles()
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
            if (MessageBox.Show("Are you sure you want to reset the statistics for this profile?", "Clearance", MessageBoxButtons.YesNo) == DialogResult.Yes)
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
            Style = Properties.Settings.Default.Theme;
            sMgr.Owner = this;
            sMgr.Style = Properties.Settings.Default.Theme;

            btn_resetStatistics.BackColor = (Color)typeof(MetroColors).GetMethod("get_" + Style).Invoke(new MetroColors(), null);

        }
    }
}
