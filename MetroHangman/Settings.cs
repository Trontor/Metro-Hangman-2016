using Hangman;
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

namespace MetroHangman
{
    public partial class Settings : MetroForm
    {
        public Settings()
        {
            InitializeComponent();
            this.StyleManager = metroStyleManager1;
            SetCurrentStyle();
            disco.Text = "Disco [" + (!Properties.Settings.Default.DiscoOn ? "Off" : "On") + "]";
        }

        private void btn_Color_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.DiscoOn = false;
            disco.Text = "Disco [OFF]";
            RandomStyle();
        }

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
            SetCurrentStyle();
            Invalidate();
        }
        private void SetCurrentStyle()
        {
            this.Style = Properties.Settings.Default.Theme;
          //  btn_Color.BackColor = (Color)typeof(MetroColors).GetMethod("get_" + Properties.Settings.Default.Theme.ToString()).Invoke(new MetroColors(), null);
            StyleManager.Style = Properties.Settings.Default.Theme;
        }
        private void tmr_Theme_Tick(object sender, EventArgs e)
        {
            SetCurrentStyle();
        }
        private void btn_Disco_Click(object sender, EventArgs e)
        {
            if (MetroHangman.Properties.Settings.Default.DiscoOn)
            {
                MetroHangman.Properties.Settings.Default.DiscoOn = false;
                disco.Text = "Disco [OFF]";
            }
            else
            {
                MetroHangman.Properties.Settings.Default.DiscoOn = true;
                disco.Text = "Disco [ON]";
            }
            MetroHangman.Properties.Settings.Default.Save();
        }

        private void btn_Statistics_Click(object sender, EventArgs e)
        {
            new profiles().Show();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
        }
    }
}
