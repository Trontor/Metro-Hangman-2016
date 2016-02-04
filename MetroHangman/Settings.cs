using System;
using System.Drawing;
using MetroFramework;
using MetroFramework.Forms;

namespace MetroHangman
{
    public partial class Settings : MetroForm
    {
        public Settings()
        {
            InitializeComponent();
            StyleManager = metroStyleManager1;
            SetCurrentStyle();
            disco.Text = $"Disco [{(!Properties.Settings.Default.DiscoOn ? "Off" : "On")}]";
        }

        private void btn_Color_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.DiscoOn = false;
            disco.Text = "Disco [OFF]";
            ((Start)ParentForm)?.ProcessTick();
            RandomStyle();
        }

        public void RandomStyle()
        {
            var m = new Random();
            int next = m.Next(3, 13);
            SetColour((MetroColorStyle) next);
        }

        private void SetColour(MetroColorStyle style)
        {
            Properties.Settings.Default.Theme = style;
            Properties.Settings.Default.Save();
            SetCurrentStyle();
            Invalidate();
        }

        private void SetCurrentStyle()
        {
            Style = Properties.Settings.Default.Theme;
            btn_Color.BackColor = (Color)typeof(MetroColors).GetMethod("get_" + Properties.Settings.Default.Theme.ToString()).Invoke(new MetroColors(), null);
            StyleManager.Style = Properties.Settings.Default.Theme;
        }

        private void tmr_Theme_Tick(object sender, EventArgs e)
        {
            SetCurrentStyle();
        }

        private void btn_Disco_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.DiscoOn)
            {
                Properties.Settings.Default.DiscoOn = false;
                disco.Text = "Disco [OFF]";
            }
            else
            {
                Properties.Settings.Default.DiscoOn = true;
                disco.Text = "Disco [ON]";
            }
            Properties.Settings.Default.Save();
        }

        private void btn_Statistics_Click(object sender, EventArgs e)
        {
            new Profiles().Show();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
        }

        private void tmr_Disco_Tick(object sender, EventArgs e)
        {

        }
    }
}